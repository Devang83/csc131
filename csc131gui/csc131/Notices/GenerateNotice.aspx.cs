using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Notices_GenerateNotice : System.Web.UI.Page
{
    protected List<string> rentTypes = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            string[] tenantIds = QuickPM.Database.GetTenantIds();
            foreach (string tenantId in tenantIds)
            {
                QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                DropDownListTenants.Items.Add(new ListItem(tenantId + " (" + tenant.GetShortName() + ")", tenant.TenantId));
            }
            if (DropDownListTenants.Items.Count > 0)
            {
                DropDownListTenants.Items[0].Selected = true;
                string id = DropDownListTenants.Items[0].Value;
                QuickPM.Tenant tenant = new QuickPM.Tenant(id);
                List<string> rentTypes = tenant.RentTypes;
                for (int i = 0; i < rentTypes.Count; i++)
                {
                    DropDownListRentTypes.Items.Add(new ListItem(rentTypes[i], i.ToString()));
                }
                
                if (DropDownListRentTypes.Items.Count > 0)
                {
                    DropDownListRentTypes.Items[0].Selected = true;
                }
            }

        }
    }

    protected void DropDownListTenants_IndexChanged(object sender, EventArgs e)
    {
        string tenantId = DropDownListTenants.SelectedValue;
        DropDownListRentTypes.Items.Clear();
        QuickPM.Property property = new QuickPM.Property(QuickPM.Util.GetPropertyId(tenantId));
        for (int i = 0; i < property.RentTypes.Count; i++)
        {
            DropDownListRentTypes.Items.Add(new ListItem(property.RentTypes[i], i.ToString()));
        }
        if (DropDownListRentTypes.Items.Count > 0)
        {
            DropDownListRentTypes.Items[0].Selected = true;
        }
    }

    protected void ButtonCreateNotice_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> vars = new Dictionary<string, string>();
        
        int days = Convert.ToInt32(DropDownListDays.SelectedValue);
        int rentTypeIndex = Convert.ToInt32(DropDownListRentTypes.SelectedValue);
        vars["intDays"] = days.ToString();
        vars["strDays"] = NumberToEnglish.EnglishFromNumber(days);
        /*if (days == 3)
        {
            vars["strDays"] = "Three";
        }
        else if (days == 5)
        {
            vars["strDays"] = "Five";
        }
        else
        {
            vars["strDays"] = days.ToString();
        }*/
        QuickPM.Tenant tenant = new QuickPM.Tenant(QuickPM.Util.FormatTenantId(DropDownListTenants.SelectedValue));
        vars["tenantName"] = tenant.Name;
        vars["tenantAddress"] = tenant.Address + ", " + tenant.City + ", " + tenant.State + " " + tenant.Zip;
        //vars["amountDue"] = "Not finished yet";
        vars["owner"] = TextBoxCreditorName.Text.Trim();
        vars["deliveryLocation"] = TextBoxDeliveryLocation.Text.Trim();
        vars["date"] = DateTime.Today.ToShortDateString();
        vars["agentTelephone"] = TextBoxAgentTelephone.Text.Trim();
        Notice notice = new Notice(Request.PhysicalApplicationPath + "/App_Data/NoticeTemplates/Pay" + Request["Notice"] + "NoticeTemplate.rtf", tenant.TenantId, rentTypeIndex, vars);
        string noticeText = notice.GenerateNotice();
        //Session["NoticeText"] = noticeText;
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "Notice.rtf");
        //Response.AddHeader("Content-Length", );
        Response.ContentType = "application/rtf";
		Response.Write(noticeText);        
        Response.End();
    }
}
