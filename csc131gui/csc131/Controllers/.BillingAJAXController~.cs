
using System;
using System.Web.Mvc;

namespace QuickPMWebsite.Controllers
{


	public class TenantsController : Controller
	{

		public ActionResult BillingAJAX(long id)
        {
			QuickPM.BillingRecord record = new QuickPM.BillingRecord(id);			
			DateTime startDate = DateTime.Parse(Request["StartDate"]);
			DateTime endDate = DateTime.Parse(Request["EndDate"]);
			decimal amount = decimal.Parse(Request["Amount"]);
			string notes = Request["Notes"];
			record.StartDate = startDate;
			record.EndDate = endDate;
			record.Amount = amount;
			record.Notes = notes;
			record.Save();			
            ViewData["Message"] = "Saved";
            return View();
        }
	}
}
