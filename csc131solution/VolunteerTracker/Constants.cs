using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker
{
    public static class Constants
    {
        public static char RentTypesSeparator = '^';        
        public static char ChartOfAccountsSeparator = RentTypesSeparator;

        /// <summary>
        /// Header used at the top of html pages to conform with the W3 standard.
        /// </summary>        
        public static string header = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\n"
                            + "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n"
                            + "<head>\n"
                            + "<title>Accounts Receivable Ledger</title>\n"
                            + "<caption></caption>"
                            + "</head>\n"
                            + "<body>\n"
                            + "<STYLE TYPE=\"text/css\">"
                            + "P.breakhere {page-break-after: always}"
                            + "</STYLE>";
        
        /// <summary>
        /// Closing footer for the end of an html page.
        /// </summary>
        public static string footer = "</body>\n</html>\n";

        public static string[] months = {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        public static string[] ARRecordBillingFields = {
           "BalanceForward",
           "CurrentBilling",
           "Adjustment",
           "ReceivedCurrent",
           "ReceivedPrior",
           "NSF",
           "OutstandingBalance"
        };
        
        public static string[] AccountsReceivableRecordFields = {
	       "TenantId",			   
	       "Month",			   
	       "Year",		   
	       "Posted",		   
	       "Memo",		   
	       "Adjustments",	   
	       "ReceivedCurrent"    
	    };        
    }          
}	       