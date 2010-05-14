
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QuickPMWebsite
{


	public partial class RentRoll : System.Web.UI.UserControl
	{
		protected Guid tableId;	
		protected string time1 = "";
		protected string time2 = "";
		protected string time3 = "";
		protected string time4 = "";
    	protected void Page_Load(object sender, EventArgs e)
    	{
        	QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        	DropDownListProperty.Visible = Request["PropertyId"] == null;		
			if (IsPostBack)
        	{	
            	return;
        	}
        	for (int year = DateTime.Today.Year - 5; year <= DateTime.Today.Year + 5; year++)
        	{
            	ListItem item = new ListItem(year.ToString(), year.ToString());
            	if (year == DateTime.Today.Year)
            	{
                	item.Selected = true;
            	}	
            	DropDownListYear.Items.Add(item);
        	}
			List<QuickPM.Property> properties = QuickPM.Property.Find<QuickPM.Property>();
			foreach (QuickPM.Property p in properties) 
			{	
				DropDownListProperty.Items.Add(new ListItem(p.Name + " (#" + p.Id + ")", p.Id.ToString()));
			}
			if (DropDownListProperty.Items.Count > 0) 
			{
				DropDownListProperty.Items[0].Selected = true;
			}
			
    	}
    	delegate string Del(string str);
    	
		public long GetPropertyId()
		{
			long propertyId;
        	if (!long.TryParse(Request["PropertyId"], out propertyId) && !long.TryParse(DropDownListProperty.SelectedValue, out propertyId))
        	{
            	return -1;
        	}
			return propertyId;
        	
		}
		
		protected string GenerateHtml()
    	{
        	DateTime date = DateTime.Today;
        	int year = DateTime.Today.Year;
        	int month = DateTime.Today.Month;
        	if (Int32.TryParse(DropDownListYear.SelectedValue, out year) && Int32.TryParse(DropDownListMonth.SelectedValue, out month))
        	{
            	date = new DateTime(year, month, 1);
        	}else
			{
				year = DateTime.Today.Year;
				month = DateTime.Today.Month;
			}
        	int propertyId;
        	if (!Int32.TryParse(Request["PropertyId"], out propertyId) && !Int32.TryParse(DropDownListProperty.SelectedValue, out propertyId))
        	{
            	return null;
        	}
        	QuickPM.Property property = new QuickPM.Property(propertyId);
        	string html = "";
        	html += "<h2>Rent Roll for " + date.ToShortDateString() + "</h2>";        
        	tableId = Guid.NewGuid();
        	html += "<fieldset>";
        	html += "<legend>Rent Roll</legend>";
        	html += "<table border=\"0px\" cellspacing=\"0px\" cellpadding=\"10px\" id=" + tableId + ">\n";
        	List<string> rentTypes = new List<string>();
        	List<decimal> rentTotals = new List<decimal>();
        	decimal completeTotal = 0m;		
			DateTime t1 = DateTime.Now;					
        	List<string> tenantIds = new List<string>(QuickPM.Database.GetPropertyTenantIds(property.Id, new QuickPM.Period(date.Year, date.Month)));
        	TimeSpan l1 = DateTime.Now - t1;
			time1= l1.TotalSeconds.ToString();

			DateTime t2 = DateTime.Now;
        	foreach (string tenantId in tenantIds)
        	{                                   	
            	foreach (string rentType in property.RentTypes)
            	{
               		if (rentTypes.Contains(rentType))
                   		continue;               
					QuickPM.Bill bill = QuickPM.Bill.GetBill(tenantId, property.RentTypes.IndexOf(rentType), year, month);                                
               		if (bill.Amount == 0m)
                   		continue;
               		rentTypes.Add(rentType);
               		rentTotals.Add(0m);
            	}
        	}
			time2 = (DateTime.Now - t2).TotalSeconds.ToString();
		
        	bool even = true;
        	string color = even ? "#fff" : "#ddd";
        	html += "<tr style=\"background-color:" + color + "\">";
        	even = !even;
        	Del head = str => "<th>" + str + "</th>";
        	html += head("Tenant Number");
        	html += head("Name");
        	html += head("Phone");
        	html += head("Unit #");
        	html += head("Unit Size");
        	
        	html += head("Next Rent Adjustment");
        	foreach (string rentType in rentTypes)
        	{
            	html += head(rentType);
        	}
        	html += head("Total Rent");
        	html += "</tr>";
        	int unitSquareFeetTotal = 0;
        	string vacantId = QuickPM.Util.FormatTenantId(property.Id.ToString() + "-0");
        	string propertyPart = vacantId.Split(new char[] { '-' })[0];
        	string tenantPart = vacantId.Split(new char[] { '-' })[1];
        	tenantPart = tenantPart.Replace("0", "*");
        	DateTime t3 = DateTime.Now;
			List<QuickPM.PropertyUnit> units = QuickPM.PropertyUnit.FindUnits(property.Id);		
			DateTime date1 = new DateTime(year, month, 1);
			DateTime date2 = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        	Dictionary<long, string> tIds = QuickPM.PropertyUnit.GetTenantIds(property.Id, date1, date2);
			time3 = (DateTime.Now - t3).TotalSeconds.ToString();
			
			QuickPMWebsite.RentRollCSV.MergeUnits(units, tIds);
			List<QuickPM.PropertyUnit> mergedUnits = units;
			Dictionary<long, List<long>> associatedUnits = QuickPMWebsite.RentRollCSV.GetAssociatedUnits(mergedUnits, tIds);
			DateTime t4 = DateTime.Now;        
        	foreach (QuickPM.PropertyUnit unit in mergedUnits)
        	{

            	color = even ? "#fff" : "#ddd";
            	if (tIds[unit.Id] == "")
            	{

	                html += "<tr style=\"background-color:" + color + "\">";

                	html += "<td>" + propertyPart + "-" + tenantPart + "</td>";//tenantid
                	string name = "Vacant";
                	if (unit.Notes.Trim() != "")
                	{
                    	name = unit.Notes.Trim();
                	}
                	html += "<td>" + name + "</td>";//name
                	html += "<td></td>";//phone
                	html += "<td>" + unit.UnitNumber + "</td>";
                	html += "<td>" + unit.SqFt.ToString("n0") + "</td>";
                	html += "<td>None</td>";//next rent adjustment date.
                	for (int i = 0; i < rentTypes.Count; i++) 
                	{
                    	html += "<td>" + (0m).ToString("c") + "</td>";
                	}
                	html += "<td>" + (0m).ToString("c") + "</td>";//total rent
                	html += "</tr>";
                	unitSquareFeetTotal += unit.SqFt;
            	}
            	else
            	{
                	QuickPM.Tenant prof = new QuickPM.Tenant(tIds[unit.Id]);

                	string profileIdLink = "<a href = \"" + ResolveUrl("~/Tenants/TenantPage/" + prof.Id) + "\">" + prof.TenantId + "</a>";
                	string name = prof.Name;
                	string phone = prof.Phone;

			string unitNumber = unit.UnitNumber;
			int sqFt = unit.SqFt;
					
			foreach(long id in associatedUnits[unit.Id])
			{
				QuickPM.PropertyUnit u = new QuickPM.PropertyUnit(id);
				unitNumber += ", " + u.UnitNumber;
				sqFt += u.SqFt;
			}
                	string unitSquareFeet = sqFt.ToString("n0");                	                	
                	unitSquareFeetTotal += sqFt;

                	string nextAdjustment = "None.";
					DateTime? nextBillingChange = prof.GetNextBillingChange(date);
                	if (nextBillingChange.HasValue)
                    	nextAdjustment = nextBillingChange.Value.ToShortDateString();
                	List<string> rents = new List<string>();
                	decimal total = 0m;
                	for (int i = 0; i < rentTypes.Count; i++)
                    {
                       	string rentType = rentTypes[i];
                       	QuickPM.Bill bill = QuickPM.Bill.GetBill(prof.TenantId, prof.RentTypes.IndexOf(rentType), year, month);
                       	rents.Add(bill.Amount.ToString("c"));
                       	rentTotals[i] += bill.Amount;
                       	total += bill.Amount;
                    }
                    
                	List<string> columns = new List<string>();
                	columns.Add(profileIdLink);
                	columns.Add(name);
                	columns.Add(phone);
                	columns.Add(unitNumber);
                	columns.Add(unitSquareFeet);
                
                	columns.Add(nextAdjustment);
                	foreach (string rent in rents)
                	{
                    	columns.Add(rent);
                	}
                	columns.Add(total.ToString("c"));
                	completeTotal += total;
                	html += "<tr style=\"background-color:" + color + "\">";
                
                	foreach (string column in columns)
                	{
                    	html += "<td>" + column + "</td>";
                	}
                	html += "</tr>";   
            	}
            	even = !even;
        	}
		
			time4 = (DateTime.Now - t4).TotalSeconds.ToString();

        	color = even ? "#fff" : "#ddd";
        	html += "<tr style=\"background-color:" + color + "\">";
        	even = !even;
        	html += "<td><b>Totals</b></td>";
        	html += "<td></td><td></td><td></td>";
        	html += "<td>" + unitSquareFeetTotal.ToString("n0") + "</td>";
        	html += "<td></td>";
        	for (int i = 0; i < rentTypes.Count; i++)
        	{
            	html += "<td>" + rentTotals[i].ToString("c") + "</td>";
        	}
        	html += "<td>" + completeTotal.ToString("c") + "</td>";
        	html += "</tr>";
        	html += "</table>";
        	html += "</fieldset>";
        	return html;
    	}				
    
    	protected void ButtonSubmit_Click(object sender, EventArgs e)
    	{
        	Session["RentRollHtml"] = GenerateHtml();
    	}
	}
}
