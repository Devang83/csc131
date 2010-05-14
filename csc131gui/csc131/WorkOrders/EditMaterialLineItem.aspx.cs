using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite.WorkOrders
{
    public partial class EditMaterialLineItem : System.Web.UI.Page
    {
        //protected QuickPM.WorkOrderInvoice invoice;
        protected int index;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*DatabaseSettings.UpdateDatabaseConnectionString(Context.Profile, Request);
            long id;
            invoice = new QuickPM.WorkOrderInvoice();
            if (Request["id"] != null && long.TryParse(Request["Id"], out id) && Request["Index"] != null && 
                int.TryParse(Request["Index"], out index))
            {
                invoice = new QuickPM.WorkOrderInvoice(id);
            }
            if(!IsPostBack && invoice.Materials.Count > index)
            {
                TextBoxChargeback.Text = invoice.Materials[index].ChargeBack.ToString("c");
                TextBoxDescription.Text = invoice.Materials[index].Description;
                TextBoxPurchaseCost.Text = invoice.Materials[index].Cost.ToString("c");
                TextBoxQuantityUsed.Text = invoice.Materials[index].QuantityUsed.ToString("c");                
            }*/
        }

        protected void Finish_Click(object sender, EventArgs e)
        {
            /*if (invoice.Materials.Count > index)
            {
                invoice.Materials[index].ChargeBack = decimal.Parse(TextBoxChargeback.Text);
                invoice.Materials[index].Description = TextBoxDescription.Text;
                invoice.Materials[index].Cost = decimal.Parse(TextBoxPurchaseCost.Text);
                invoice.Materials[index].QuantityUsed = long.Parse(TextBoxQuantityUsed.Text);
                invoice.Save();
                Response.Redirect(Request.ServerVariables["HTTP_REFERRER"]);
            }*/
        }
    }
}
