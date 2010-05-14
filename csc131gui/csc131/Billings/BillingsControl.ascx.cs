using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Billings_BillingsControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
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

        DropDownListMonth.SelectedIndex = DateTime.Today.Month < 12 ? DateTime.Today.Month : 0;
        
        long[] propertyIds = QuickPM.Util.GetPropertyIds();
        foreach (int propertyId in propertyIds)
        {
            QuickPM.Property property = new QuickPM.Property(propertyId);
            DropDownListPropertyId.Items.Add(new ListItem(property.Name + " (#" + propertyId + ")", propertyId.ToString()));
        }
        if (DropDownListPropertyId.Items.Count > 0)
        {
            DropDownListPropertyId.Items[0].Selected = true;
        }
    }

    protected long GetPropertyId()
    {
        
        if (Request["PropertyId"] == null)
        {
            return Int32.Parse(DropDownListPropertyId.SelectedValue);
        }
        else
        {
            return Convert.ToInt32(Request["PropertyId"]);
        }   
        
    }

    protected void ButtonEmail_Click(object sender, EventArgs e)
    {        
        QuickPM.Property property = new QuickPM.Property(GetPropertyId());
        List<string> tenantIds = property.GetTenantIds();
        string html = "<table id=\"email\" cellspacing=\"0px\" cellpadding=\"10px\">" + @"
            <tr>
                <th>Tenant Name/Id</th><th>Email</th><th>Status</th>
            </tr>
        ";
        int year = Int32.Parse(DropDownListYear.SelectedValue);
        int month = QuickPM.Util.ConvertMonthToInt(DropDownListMonth.SelectedValue);
        
        foreach (string tenantId in tenantIds)
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            string email = tenant.BillingEmail;
            
            string status = "";

            if (email.Trim() != "")
            {
                PrintTenantBilling.url = "http://cmd.quickpm.net" + "/Public/Tenant.aspx";
                PDFjet.NET.PDF document = PrintBillings.GetPdf(tenantId, year, month);
				QuickPM.RemitInfo remitInfo = new QuickPM.RemitInfo(tenant.Property);
                string filePath = Request.PhysicalApplicationPath + "\\App_Data\\tmpbilling.pdf";
                document.Save(filePath);
                System.IO.Stream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                status = "Sent";
				string questions = remitInfo.Email.Trim() != "" ? "\r\nIf you have a question about your billing statement please email " + remitInfo.Email : "";
				string message = "Please see the attached pdf for your billing statement.\r\nThis is an automated message, do not respond. " + questions;
            	try
                {
                    SendEmail.Send(email, new QuickPM.Period(year, month).ToString() + " " + "Billing Statement", message,
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

                }finally
                {
                    stream.Close();
                    System.IO.File.Delete(filePath);
                }
            }
            
            html += "<tr>";
            string billingemail = tenant.BillingEmail.Trim() != "" ? tenant.BillingEmail.Trim() : "No Email";
            html += "<td>" + "<a href=" + QuickPMWebsite.AppCode.Link.LinkTo(tenant, this, HttpContext.Current.Profile.IsAnonymous) + ">" + tenant.Name + " (#" + tenantId + ")" + "</a>" + "</td>" +
                    "<td>" + billingemail + "</td>" +
                    "<td>" + status + "</td>";
            html += "</tr>";
                

        }
        html += "</table>";
        Session["email"] = html;
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        long PropertyId = GetPropertyId();
        int year = Int32.Parse(DropDownListYear.SelectedValue);
        int month = QuickPM.Util.ConvertMonthToInt(DropDownListMonth.SelectedValue);
        PrintTenantBilling.url = "http://cmd.quickpm.net" + "/Public/Tenant.aspx";        
        PDFjet.NET.PDF document = PrintBillings.GetPdf(PropertyId, year, month);        
		string filePathName = Request.PhysicalApplicationPath + "/Billings/" + "Billings.pdf";
		document.Save(filePathName); 
		
		byte[] fileBytes = System.IO.File.ReadAllBytes(filePathName);
        //Response.Redirect("~/Billings/ViewPrint.aspx");
        QuickPM.Period period = new QuickPM.Period(year, month);
		Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"#" + PropertyId.ToString() + " " + new QuickPM.Property(PropertyId).Name + " " + period.ToString() + ".pdf\"");
        Response.AddHeader("Content-Length", fileBytes.Length.ToString());
        Response.ContentType = "application/octet-stream";        
		//byte[] byteBuffer = document.GetData().GetBuffer();
		//char[] charBuffer = System.Text.Encoding.ASCII.GetChars(byteBuffer);
        //Response.Write(charBuffer, 0, charBuffer.Length);		
        Response.WriteFile(filePathName);
		Response.End();        
    }

    protected string GenerateBilling(string tenantId, int year, int month)
    {        
        QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
        string html = "";
        html += "<hr/><br/>";
        html += "<p class=\"breakhere\"/>";
        html += "<table width=\"80%\">";
        html += "<tr>";
        html += "<td></td><td>Billing Date:" + DateTime.Now.ToShortDateString() + "</td>";
        html += "</tr>";
        html += "<td>Name:</td><td>" + tenant.Name + "</td></tr>";
        html += "<tr><td>Account#</td><td>" + tenant.TenantId + "</td></tr></table>";
        html += "</table>";
        html += "<br/>";
        html += "<br/>";
        html += "<table width=\"80%\">";
        html += "<tr>";
        html += "<table cellpadding=\"5px\">" + "<br/>";
        string billName = tenant.Name;
        string billAddress = tenant.Address;
        string billCity = tenant.City;
        string billState = tenant.State;
        string billZip = tenant.Zip;
        if (!tenant.BillSame)
        {
            billName = tenant.BillName;
            billAddress = tenant.BillAddress;
            billCity = tenant.BillCity;
            billState = tenant.BillState;
            billZip = tenant.BillZip;
        }
        html += "<tr>";
        html += "<td>Billing:</td><td>" + billName + "</td>" + "<td>Regarding:</td><td>" + tenant.Name + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td></td><td>" + billAddress + "</td>" + "<td></td><td>" + tenant.Address + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td></td><td>" + billCity + ", " + billState + "  " + billZip + "</td>";
        html += "<td></td><td>" + tenant.City + ", " + tenant.State + "  " + tenant.Zip + "</td>";
        html += "</tr>";
        html += "</table>";
        html += "<table border=\"1px\">";
        Dictionary<string, QuickPM.Bill> billingRecords = QuickPM.Bill.GetBills(tenantId, year, month);
        html += "<tr><th>Description</th><th>Past Amounts Due</th><th>Current Billing</th><th>Total Due</th></tr>";
        QuickPM.ARRecord arRecord = new QuickPM.ARRecord(tenantId, year, month);
        Dictionary<string, decimal> balanceForward = arRecord.BalanceForward();
        decimal totalBalanceForward = 0m;
        decimal totalCurrentBilling = 0m;
        decimal totalOutstandingBalance = 0m;
        foreach (string rentType in tenant.RentTypes)
        {
            decimal balForward = balanceForward[rentType];
            totalBalanceForward += balForward;
            totalCurrentBilling += billingRecords[rentType].Amount;
            totalOutstandingBalance += balForward + billingRecords[rentType].Amount;
            html += "<tr>\n";
            html += "<td>";
            html += rentType;
            html += "</td>";
            html += "<td>";
            html += "$" + balForward.ToString("n");
            html += "</td>";
            html += "<td>";
            html += "$" + billingRecords[rentType].Amount.ToString("n");

            html += "</td>";
            html += "<td>";
            html += "$" + (balForward + billingRecords[rentType].Amount).ToString("n");
            html += "</td>";
            html += "</tr>";

        }
        html += "<tr>";
        html += "<td>Total</td>";
        html += "<td>" + "$" + totalBalanceForward.ToString("n") + "</td>";
        html += "<td>" + "$" + totalCurrentBilling.ToString("n") + "</td>";
        html += "<td>" + "$" + totalOutstandingBalance.ToString("n") + "</td>";
        html += "</tr>";
        html += "</table>";
        return html;
    }
}
