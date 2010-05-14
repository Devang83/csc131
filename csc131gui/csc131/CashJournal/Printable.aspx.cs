using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashJournal_Printable : System.Web.UI.Page
{
    protected List<QuickPM.Deposit> deposits = null;
    protected int month = 0, year = 0, PropertyId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        
        /*if (Request["Month"] != null && Request["Year"] != null && Request["PropertyId"] != null)
        {
            if (!Int32.TryParse(Request["Month"], out month) || !Int32.TryParse(Request["Year"], out year) ||
                !Int32.TryParse(Request["PropertyId"], out PropertyId))
            {
                return;
            }
        }
        deposits = QuickPM.Deposit.GetDeposits(PropertyId, year, month);*/
		DownloadPdf();
    }
	
	protected void DownloadPdf()
	{
		QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        DateTime date = DateTime.Today;
        int year = DateTime.Today.Year;        
        int month = DateTime.Today.Month;

        if (Int32.TryParse(Request["Year"], out year) && Int32.TryParse(Request["Month"], out month))
        {
           	date = new DateTime(year, month, 1);
        }
        int propertyId;
        if (!Int32.TryParse(Request["PropertyId"], out propertyId))
        {
           	//return null;
        }
        //QuickPM.Property property = new QuickPM.Property(propertyId);        
        
        string fileName = Request.PhysicalApplicationPath + "/App_Data/Tmp/" + "Deposits.pdf";
        //System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create);




		QuickPMWebsite.CashJournalPdfReport.GenerateCashJournalPdf(propertyId, new QuickPM.Period(date.Year, date.Month), 
		                                                           new QuickPM.Period(date.Year, date.Month), fileName);        
        //doc.Data = new UTF8Encoding(true).GetBytes("test");
        //file.Write(doc.Data, 0, doc.Data.Length);
        //long fileLength = file.Length;
        //file.Close();
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "Deposits.pdf");
        //Response.AddHeader("Content-Length", fileLength.ToString());
        Response.ContentType = "application/pdf";			
        Response.WriteFile(fileName);
        Response.End();   	
	}
}
