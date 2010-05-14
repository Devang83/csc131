using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tenant_SendBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        int year = DateTime.Now.Year;
        for (int y = year - 5; y <= year + 5; y++)
        {
            ListItem item = new ListItem(y.ToString());

            if ((y == DateTime.Now.Year && DateTime.Today.Month < 12) || (y == DateTime.Now.Year + 1 && DateTime.Today.Month == 12))
            {
                item.Selected = true;
            }
            DropDownListYear.Items.Add(item);
        }

        DropDownListMonth.SelectedIndex = DateTime.Today.Month < 12 ? DateTime.Today.Month - 1 : 0;
        if (Request["Year"] != null)
        {
            int ryear;
            if (int.TryParse(Request["Year"], out ryear))
            {
                if (Math.Abs(year - ryear) < 5)
                {
                    DropDownListYear.SelectedIndex = year - ryear + 5;
                }
            }
        }

        if (Request["Month"] != null)
        {
            int month;
            if (int.TryParse(Request["Month"], out month))
            {
                if (month > 0 && month <= 12)
                {
                    DropDownListMonth.SelectedIndex = month - 1;
                }
            }
        }
    }

    protected string GetTenantId()
    {
        return Request["TenantId"];
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {

        QuickPM.Tenant tenant = new QuickPM.Tenant(GetTenantId());
        int year = Int32.Parse(DropDownListYear.SelectedValue);
        int month = QuickPM.Util.ConvertMonthToInt(DropDownListMonth.SelectedValue);
        PrintTenantBilling.url = "http://cmd.quickpm.net" + "/Public/Tenant.aspx";
        PDFjet.NET.PDF document = PrintBillings.GetPdf(tenant.TenantId, year, month);
        document.Save(Request.PhysicalApplicationPath + "/Billings/Billings.pdf");
        //Response.Redirect("~/Billings/ViewPrint.aspx");
        
        
        string fileName = Request.PhysicalApplicationPath + "/Billings/Billings.pdf";
        System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
        //doc.Data = new UTF8Encoding(true).GetBytes("test");        
        long fileLength = file.Length;
		string attachmentFileName = tenant.Name + " " + new QuickPM.Period(year, month).ToString() + " Billing Statement";
        file.Close();
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + attachmentFileName + ".pdf\"");
        Response.AddHeader("Content-Length", fileLength.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(fileName);
        Response.End();        
    }

    protected void ButtonEmail_Click(object sender, EventArgs e)
    {
        QuickPM.Tenant tenant = new QuickPM.Tenant(GetTenantId());
        string html = "<table id=\"email\" cellspacing=\"0px\" cellpadding=\"10px\">" + @"
            <tr>
                <th>Tenant Name/Id</th><th>Email</th><th>Status</th>
            </tr>
        ";
        int year = Int32.Parse(DropDownListYear.SelectedValue);
        int month = QuickPM.Util.ConvertMonthToInt(DropDownListMonth.SelectedValue);
        QuickPM.Period period = new QuickPM.Period(year, month);
        string email = tenant.BillingEmail;

        string status = "";

        if (email.Trim() != "")
        {
            PrintTenantBilling.url = "http://cmd.quickpm.net" + "/Public/Tenant.aspx";
            PDFjet.NET.PDF document = PrintBillings.GetPdf(tenant.TenantId, year, month);
            string filePath = Request.PhysicalApplicationPath + "\\App_Data\\tmpbilling.pdf";
            document.Save(filePath);
            System.IO.Stream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
			QuickPM.RemitInfo remitInfo = new QuickPM.RemitInfo(tenant.Property);
			status = "Sent";
			string questions = remitInfo.Email.Trim() != "" ? "\r\nIf you have a question about your billing statement please email " + remitInfo.Email : "";
			string message = "Please see the attached pdf for your billing statement.\r\nThis is an automated message, do not respond. " + questions;
                
            try
            {
                SendEmail.Send(email,  period.ToString() + " " + "Billing Statement", message,
                    Request.PhysicalApplicationPath, stream, "BillingStatement.pdf");
            }
            catch (Exception ex)
            {
                if (ex is System.Net.Mail.SmtpFailedRecipientException)
                {
                    status = "Failed to send email";
                }
                else if (ex is System.Net.Mail.SmtpFailedRecipientsException)
                {
                    status = "Failed to send email";
                }
                else
                {
                    status = "Unknown error";
                }

            }
            finally
            {
                stream.Close();
                System.IO.File.Delete(filePath);
            }
        }

        html += "<tr>";
        string billingemail = tenant.BillingEmail.Trim() != "" ? tenant.BillingEmail.Trim() : "No Email";
        html += "<td>" + "<a href=" + QuickPMWebsite.AppCode.Link.LinkTo(tenant, this, HttpContext.Current.Profile.IsAnonymous) + ">" + tenant.Name + " (#" + tenant.TenantId + ")" + "</a>" + "</td>" +
                "<td>" + billingemail + "</td>" +
                "<td>" + status + "</td>";
        html += "</tr>";


        
        html += "</table>";
        Session["email"] = html;
    }
}
