using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public partial class ImportChecks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    protected void ButtonImport_Click(object sender, EventArgs e)
    {
        string[] checkData = TextBoxChecks.Text.Split(new char[] { '\n' });        
        Dictionary<string, int> columnNames = new Dictionary<string,int>();
        columnNames.Add("TenantNumber", 0);
        columnNames.Add("TenantName", 1);
        columnNames.Add("CheckDate", 2);
        columnNames.Add("ReceivedDate", 3);
        columnNames.Add("CheckNumber", 4);
        columnNames.Add("CheckAmount", 5);
        List<string> skippedLines = new List<string>();
        List<List<string>> importedChecks = new List<List<string>>();
        foreach (string line in checkData)
        {
            string[] columns = line.Split(new char[] { '\t' });
            string tenantNumber = columns[columnNames["TenantNumber"]];
            if (!QuickPM.Util.TryFormatTenantId(tenantNumber, out tenantNumber))
			{
				skippedLines.Add(line);
			}
            string checkDate = columns[columnNames["CheckDate"]];
            string receivedDate = columns[columnNames["ReceivedDate"]];
            string checkNumber = columns[columnNames["CheckNumber"]];
            string checkAmount = columns[columnNames["CheckAmount"]];
            QuickPM.Period period1 = new QuickPM.Period(DateTime.Now.Year, DateTime.Now.Month);
            QuickPM.Period period2 = period1.SubtractMonth();
            List<QuickPM.Check> checks1 = QuickPM.Database.GetChecks(tenantNumber, period1);
            List<QuickPM.Check> checks2 = QuickPM.Database.GetChecks(tenantNumber, period2);
            checks1.AddRange(checks2);
            bool skip = false;
            foreach (QuickPM.Check check in checks1)
            {
                if (check.Number.Trim() == checkNumber.Trim() && check.CheckDate == DateTime.Parse(checkDate))
                {
                    skip = true;
                    break;
                }
            }

            List<string> checkInfo = new List<string>();
            checkInfo.Add(checkNumber.Trim());
            if (skip)
            {
                checkInfo.Add("Yes");
            }
            else
            {
                checkInfo.Add("No");
            }
            
            if (skip)
            {

                importedChecks.Add(checkInfo);
                continue;
            }

            QuickPM.Check c = new QuickPM.Check();
            c.TenantId = tenantNumber;
            c.Number = checkNumber.Trim();
            c.CheckDate = DateTime.Parse(checkDate);
            c.ReceivedDate = DateTime.Parse(receivedDate);
            c.Amount = Decimal.Parse(checkAmount, System.Globalization.NumberStyles.Any);
            c.ARRecordDate = new DateTime(period1.Year, period1.Month, 1);            
            c.AutoApply(period1);
            c.Save();
            importedChecks.Add(checkInfo);
        }
        Session["ImportedChecks"] = importedChecks;
        Session.Add("SkippedLines", skippedLines);
    }
}
