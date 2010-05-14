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
using QuickPM;

public partial class Property_PropertyControl : System.Web.UI.UserControl
{    

    bool AddingProperty
    {
        get;
        set;
    }	
	
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxPropertyId.Enabled = AddingProperty;
        if (!IsPostBack && !AddingProperty)
        {
            LoadProperty();
            bool canWrite = GetProperty().ACL.CanWrite(QuickPM.Database.GetUserId());
            ButtonAddRentType.Visible = canWrite;
            ButtonAddRentType.Enabled = canWrite;
            ButtonSubmit.Visible = canWrite;
            ButtonSubmit.Enabled = canWrite;
            if (!canWrite)
            {				           
				QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
            }
        }
    }

    protected QuickPM.Property GetProperty()
    {
        if (AddingProperty)
        {
            return new Property();
        }
        if (Request["PropertyId"] == null)
        {
            Session["PropertyErrorMessage"] = "Please provide a property number";
            return new Property();
        }
        Property property = null;
        try
        {
            property = new Property(long.Parse(Request["PropertyId"]));
            return property;
        }
        catch (Exception e)
        {
            Session["PropertyErrorMessage"] = "Error finding Property#" + Request["PropertyId"] +
                "<br/>" + "Details:<br/>" + e.Message;
            return new Property();
        }        
    }

    protected void ButtonAddRentType_Click(object sender, EventArgs e)
    {
        string rentTypeName = TextBoxRentTypeName.Text.Trim();
        if (!QuickPM.Property.ValidRentType(rentTypeName))
        {
            Session["PropertyErrorMessage"] = "<font color=\"red\">Only use letters, numbers, and % in the rent name</font>";
            return;
        }
        int chartOfAccount = 0;
        if (TextBoxRentTypeChartOfAccount.Text.Trim() != "")
        {
            if (!Int32.TryParse(TextBoxRentTypeChartOfAccount.Text, out chartOfAccount))
            {
                Session["PropertyErrorMessage"] = "<font color=\"red\">Please enter a number for the chart of account</font>";
                return;
            }            
        }
        QuickPM.Property property = new QuickPM.Property(Int32.Parse(Request["PropertyId"]));
        if (property.RentTypes.IndexOf(rentTypeName) != -1)
        {
            Session["PropertyErrorMessage"] = "<font color=\"red\">That rent name already exists!</font>";
            return;
        }
        property.AddRentType(rentTypeName);
        if (TextBoxRentTypeChartOfAccount.Text.Trim() != "")
        {
            property.ChartOfAccounts.Add(property.RentTypes.Count - 1, chartOfAccount);
        }
        property.Save();
    }

    private void LoadProperty()
    {
        if (Request["PropertyId"] == null)
        {
            Session["PropertyErrorMessage"] = "Please provide a property number";
            return;
        }
        Property property = null;
        try
        {
            property = new Property(long.Parse(Request["PropertyId"]));
        }
        catch (Exception e)
        {
            Session["PropertyErrorMessage"] = "Error finding Property#" + Request["PropertyId"] + 
                "<br/>" + "Details:<br/>" + e.Message;
            return;
        }
        TextBoxPropertyId.Text = property.Id.ToString();
        TextBoxPropertyName.Text = property.Name;
        TextBoxLegalName.Text = property.LegalName;
        TextBoxPhysicalAddress.Text = property.Address;
		RadioButtonActive.Checked = property.Active;
		RadioButtonInactive.Checked = !property.Active;
        List<QuickPM.Person> contacts = QuickPM.Person.GetContacts(property);
        foreach (QuickPM.Person contact in contacts)
        {
            if (contact.Name == "" && contact.CellPhone == "" && contact.Email == "" && contact.Fax == "" &&
                contact.HomePhone == "" && contact.OfficePhone == "" && contact.Title == "")
            {
                contact.Delete();
            }
        
        }
        contacts = QuickPM.Person.GetContacts(property);
        RemitInfo remit = null;		
        try
        {
            remit = new RemitInfo((int)property.Id);
        }
        catch (QuickPM.Exceptions.RemitInfoException ex)
        {
            if (ex.ExceptionType == QuickPM.Exceptions.RemitInfoExceptionType.NoData)
            {
                remit = new RemitInfo();
                remit.Id = (int)property.Id;
                remit.AddToDatabase();
            }
        }
        TextBoxRemitAddress.Text = remit.Address;
        TextBoxRemitCity.Text = remit.City;
        TextBoxRemitEmail.Text = remit.Email;
        TextBoxRemitFax.Text = remit.Fax;
        TextBoxRemitName.Text = remit.Name;
        TextBoxRemitState.Text = remit.State;
        TextBoxRemitTelephone.Text = remit.Telephone;
        TextBoxRemitZip.Text = remit.Zip;

    }
    

    private void AddProperty()
    {
        Property property = new Property();
        property.Name = TextBoxPropertyName.Text;
		property.Active = true;
        property.LegalName = TextBoxLegalName.Text;
        property.Id = Int32.Parse(TextBoxPropertyId.Text);
        List<Property> properties = Property.Util.GetProperties();
        foreach (Property prop in properties)
        {
            if (prop.Id == property.Id)
            {
                Session["PropertyErrorMessage"] = "<h2>Property number " + property.Id + " is already used.</h2>";
                return;
            }
        }
        try
        {
            property.Active = true;
            property.Address = TextBoxPhysicalAddress.Text;
            
            RemitInfo remit = new RemitInfo();
            remit.Address = TextBoxRemitAddress.Text;
            remit.City = TextBoxRemitCity.Text;
            remit.Email = TextBoxRemitEmail.Text;
            remit.Fax = TextBoxRemitFax.Text;
            remit.Name = TextBoxRemitName.Text;
            remit.State = TextBoxRemitState.Text;
            remit.Telephone = TextBoxRemitTelephone.Text;
            remit.Zip = TextBoxRemitZip.Text;
            remit.Id = (int)property.Id;
            remit.Save();            
            property.Save();
        }
        catch (Exception ex)
        {
            Session["PropertyErrorMessage"] = ex.Message;
            return;
        }
        Session["AddPropertyFinished"] = true;
    }

	
	protected void RadioButtonActive_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButtonActive.Checked)
        {
            RadioButtonInactive.Checked = false;
        }
		SubmitChanges();
      
    }
    protected void RadioButtonInactive_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButtonInactive.Checked)
        {
            RadioButtonActive.Checked = false;
        }
      	SubmitChanges();
    }

	
    private void SubmitChanges()
    {
        Property property = new Property(long.Parse(TextBoxPropertyId.Text));
        property.Name = TextBoxPropertyName.Text;
        property.LegalName = TextBoxLegalName.Text;
		property.Active = RadioButtonActive.Checked;
        try
        {
            //property.Active = true;
            property.Address = TextBoxPhysicalAddress.Text;                        
            RemitInfo remit = new RemitInfo((int)property.Id);
            remit.Address = TextBoxRemitAddress.Text;
            remit.City = TextBoxRemitCity.Text;
            remit.Email = TextBoxRemitEmail.Text;
            remit.Fax = TextBoxRemitFax.Text;
            remit.Name = TextBoxRemitName.Text;
            remit.State = TextBoxRemitState.Text;
            remit.Telephone = TextBoxRemitTelephone.Text;
            remit.Zip = TextBoxRemitZip.Text;
            remit.Id = (int)property.Id;
            remit.Save();
            property.Save();
        }
        catch (Exception ex)
        {
            Session["PropertyErrorMessage"] = ex.Message;
            return;
        }
        Session["PropertyChangesSubmitted"] = true;
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (AddingProperty)
        {
            AddProperty();
        }
        else
        {
            SubmitChanges();   
        }
    }
}
