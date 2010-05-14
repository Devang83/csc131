using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

/// <summary>
/// Summary description for Contact
/// </summary>
public class Contact
{
	public Contact()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    /// <summary>
    /// Please note that the database connection must be properly set up before calling this function
    /// </summary>
    /// <param name="contactId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string DisplayContact(long contactId, System.Web.UI.Page page)
    {
        VolunteerTracker.Person person = new VolunteerTracker.Person(contactId);
        string editLink = "<a href=\"" + page.ResolveUrl("~/People/Person.aspx?id=" + person.Id) + "&return=" + System.Uri.EscapeUriString(page.Request.RawUrl) + "\">Edit</a>";
        string deleteLink = "<a href=\"" + page.ResolveUrl("~/People/DeletePerson.aspx?id=" + person.Id) + "&return=" + System.Uri.EscapeUriString(page.Request.RawUrl) + "\" onclick=\"javascript: return confirm('Delete?')\">Delete</a>";
		if(!person.ACL.CanWrite(VolunteerTracker.Database.GetUserId()))
        {
            editLink = "";
            deleteLink = "";
        }

        string html = @"<fieldset>
                <legend>
                    Key Contact " + (editLink != "" ?  "(" + editLink + ", " + deleteLink + ")" : "") + 
                @"</legend>";
        
        if (person.ACL.NoAccess(VolunteerTracker.Database.GetUserId()))
        {
            html = @"<fieldset>
                        <legend>
                            Key Contact
                        </legend>
                        We're sorry you do not have have permission to view this contact. Please ask your account administrator for help
                        </fieldset>";
            return html;
        }
        
        if(person.Name != ""){
            html += "<b>Name</b> &nbsp;" + person.Name;
        }
        
        if (person.Title != "")
        {
            html += "<br/><b>Title</b> &nbsp;" + person.Title;
        }        
        if(person.OfficePhone != ""){
        html += "<br/><b>Bus. Tele.</b> &nbsp;" + person.OfficePhone;
        }
        if(person.CellPhone != ""){
        html += "<br/><b>Cell Phone</b> &nbsp;" + person.CellPhone;
        }
        if(person.HomePhone != ""){
        html += "<br/><b>Home Phone</b> &nbsp;" + person.HomePhone;
        }
        if(person.Fax != ""){
        html += "<br/><b>Fax Tele.</b> &nbsp;" + person.Fax;
        }
        if(person.Email != ""){
        html += "<br/><b>Email</b> &nbsp;" + person.Email;
        }
        if (person.Address != "")
        {
            html += "<br/><b>Address</b> &nbsp;" + person.Address;
        }

        html += "</fieldset>";
        return html;
    }    

    /// <summary>
    /// Please note that the database connection must be properly set up before calling this function
    /// </summary>
    /// <param name="PropertyId"></param>
    /// <returns></returns>
    public static string DisplayContacts(VolunteerTracker.ActiveRecord obj, System.Web.UI.Page page)
    {
        List<VolunteerTracker.Person> contacts = VolunteerTracker.Person.GetContacts(obj);
        string html = "";
        foreach (VolunteerTracker.Person contact in contacts)
        {
            html += DisplayContact(contact.Id, page);
            html += "<br/>";
        }
        return html;
    }
}
