using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_CashJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        ((CashJournal_CashJournalControl)this.CashJournal).CanSelectProperty = false;
    }
}
