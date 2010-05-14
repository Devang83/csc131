using System;
using System.Collections;
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

public partial class People_DeletePerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, this.Request);
        if (Request["Id"] != null)
        {
            long id;
            if (long.TryParse(Request["Id"], out id))
            {
                VolunteerTracker.Person person = new VolunteerTracker.Person(id);                
                if (person.Delete() != 1)
                {
                    throw new Exception("Error deleting contact");                
                }
            }
            else
            {
                throw new Exception("Can't delete contact");
            }
            
        }
        if (Request["return"] != null)
        {
            Response.Redirect(Request["return"]);
        }
    }
}
