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

public partial class Tenant_Insurance : System.Web.UI.Page
{
    protected QuickPM.Tenant tenant = null;
    protected List<QuickPM.InsurancePolicy> policies = new List<QuickPM.InsurancePolicy>();

    public void AddFunction(String icId, long policyId)
    {
        QuickPM.InsuranceCertificate ic = new QuickPM.InsuranceCertificate(long.Parse(icId));
        ic.PolicyIds.Add(policyId);
        ic.Save();
        this.LoadTenantValues();
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;
        LoadTenantValues();
        List<QuickPM.InsuranceCertificate> ics = QuickPM.InsuranceCertificate.GetInsuranceCertificates(tenant);        
		if (tenant != null)
        {
            if (ics.Count > 0)
            {
                QuickPM.InsuranceCertificate ic = ics[0];
                if (ic != null)
                {
                    ((Documents_DocumentsControl)DocumentsIC).DocumentIds = ic;
                }
            }
            else
            {
                QuickPM.InsuranceCertificate ic = new QuickPM.InsuranceCertificate();                
				ic.SetAssociatedId(tenant);
                ic.Save();
                ((Documents_DocumentsControl)DocumentsIC).DocumentIds = ic;
            }

            if (!tenant.ACL.CanWrite(QuickPM.Database.GetUserId()))
            {
                QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
                ButtonSubmit.Visible = false;
                ButtonSubmit.Enabled = false;
            }
        }
    }


    protected string CreateInsurancePoliciesHtml(QuickPM.InsurancePolicy.PolicyCategories category)
    {
        string html = "<table cellpadding=\"10px\">";
        html += "<tr>";
        html += "<th>Insurer</th>";
        html += "<th>Insurance Type</th>";
        html += "<th>Policy Number</th>";
        html += "<th>Policy Period</th>";
        html += "<th>Limits</th>";
        html += "</tr>";
        if (policies != null)
           {
               foreach (QuickPM.InsurancePolicy ip in policies)
               {
                   if (ip.PolicyCategory != category)
                   {
                       continue;
                   }                      
                   
                   html += "<tr>";
                   html += "<td>";
                   html += ip.Insurer.Trim();
                   html += "</td>";
                   html += "<td>";
                   html += ip.PolicyDescription;
                   html += "</td>";
                   html += "<td>";
                   html += ip.PolicyNumber;
                   html += "</td>";
                   html += "<td>";
                   html += ip.BeginDate.ToShortDateString() + "-" + ip.EndDate.ToShortDateString();
                   html += "</td>";
                   html += "<td>";
                   html += ip.LimitAmount.ToString("c");
                   html += "<br />";
                   html += ip.LimitDescription;
                   html += "</td>";
                   html += "</tr>";
               
              }
           }
        html += "</table>";
        return html;        
    }

    private void LoadTenantValues()
    {
        LoadTenant();
        List<QuickPM.InsuranceCertificate> ics = QuickPM.InsuranceCertificate.GetInsuranceCertificates(tenant);
        QuickPM.InsuranceCertificate ic = null;
        if (ics.Count > 0)
        {


            ic = ics[0];
        }
        else
        {
            ic = new QuickPM.InsuranceCertificate();            
			ic.SetAssociatedId(tenant);
            ic.Save();
        }
        /*this.InsurancePolicyAuto.ParentId = ic.Id.ToString();
        this.InsurancePolicyAuto.SetAddFunction(AddFunction);
        this.InsurancePolicyAuto.PolicyCategory = QuickPM.InsurancePolicy.PolicyCategories.Automobile;
        SetPolicyAndLimitDescription(this.InsurancePolicyAuto.PolicyDescription(), this.InsurancePolicyAuto.LimitDescription(), this.InsurancePolicyAuto.PolicyCategory);

        this.InsurancePolicyGarage.ParentId = ic.Id.ToString();
        this.InsurancePolicyGarage.SetAddFunction(AddFunction);
        this.InsurancePolicyGarage.PolicyCategory = QuickPM.InsurancePolicy.PolicyCategories.Garage;
        SetPolicyAndLimitDescription(InsurancePolicyGarage.PolicyDescription(), InsurancePolicyGarage.LimitDescription(), QuickPM.InsurancePolicy.PolicyCategories.Garage);

        this.InsurancePolicyGeneral.ParentId = ic.Id.ToString();
        this.InsurancePolicyGeneral.SetAddFunction(AddFunction);
        this.InsurancePolicyGeneral.PolicyCategory = QuickPM.InsurancePolicy.PolicyCategories.General;
        SetPolicyAndLimitDescription(InsurancePolicyGeneral.PolicyDescription(), InsurancePolicyGeneral.LimitDescription(), QuickPM.InsurancePolicy.PolicyCategories.General);

        this.InsurancePolicyUmbrella.ParentId = ic.Id.ToString();
        this.InsurancePolicyUmbrella.SetAddFunction(AddFunction);
        this.InsurancePolicyUmbrella.PolicyCategory = QuickPM.InsurancePolicy.PolicyCategories.Umbrella;
        SetPolicyAndLimitDescription(InsurancePolicyUmbrella.PolicyDescription(), InsurancePolicyUmbrella.LimitDescription(), QuickPM.InsurancePolicy.PolicyCategories.Umbrella);
        */
        if (IsPostBack)
        {
            return;
        }
       

        
        TextBoxICEndDate.Text = ic.EndDate.ToShortDateString();
        TextBoxICBeginDate.Text = ic.BeginDate.ToShortDateString();
        TextBoxAdditionalInsured.Text = ic.AdditionalInsured;
        TextBoxInsured.Text = ic.InsuredLocation;
        List<long> policyIds = ic.PolicyIds;
        policies = new List<QuickPM.InsurancePolicy>();
        foreach (long policyId in policyIds)
        {
            QuickPM.InsurancePolicy ip = new QuickPM.InsurancePolicy(policyId);
            policies.Add(ip);
        }
        

    }


    public void SetPolicyAndLimitDescription(DropDownList policyDescription, DropDownList limitDescription, QuickPM.InsurancePolicy.PolicyCategories policyCategory)
    {
        string[] autoPolicyDescriptions = new string[] { "Any Auto", "All Owned Autos", "Scheduled Autos", "Hired Autos", "Non-Owned Autos" };
        string[] autoLimitDescriptions = new string[] { "Combined Single Limit (Ea Accident)", "Bodily Injury (Per Person)", "Property Damage (Per Accident)" };
        
        string[] garagePolicyDescriptions = new string[] { "Any Auto" };
        string[] garageLimitDescriptions = new string[] { "Auto Only - Ea Accident", "Other Than Auto Ea Accident", "Other Than Auto Only Agg" };

        string[] generalPolicyDescriptions = new string[] { "Commercial General Liability" };
        string[] generalLimitDescriptions = new string[] { "Each Occurrence", "Damage To Rented Premises (Ea Occurence)", "Med Exp (Any one person)", "Personal & Adv Injury", "General Aggreate", "Products - Comp/Op Agg" };
        
        string[] umbrellaPolicyDescriptions = new string[] { "Occur" };
        string[] umbrellaLimitDescriptions = new string[] { "Each Occurence", "Aggragate" };

        string[] policyDescriptions = null;
        string[] limitDescriptions = null;
        policyDescription.Items.Clear();
        limitDescription.Items.Clear();
        ListItemCollection policyItems = policyDescription.Items;
        ListItemCollection limitItems = limitDescription.Items;
        switch (policyCategory)
        {
            case QuickPM.InsurancePolicy.PolicyCategories.Automobile:
                policyDescriptions = autoPolicyDescriptions;
                limitDescriptions = autoLimitDescriptions;
                break;
            case QuickPM.InsurancePolicy.PolicyCategories.Garage:
                policyDescriptions = garagePolicyDescriptions;
                limitDescriptions = garageLimitDescriptions;
                break;
            case QuickPM.InsurancePolicy.PolicyCategories.General:
                policyDescriptions = generalPolicyDescriptions;
                limitDescriptions = generalLimitDescriptions;
                break;
            case QuickPM.InsurancePolicy.PolicyCategories.Umbrella:
                policyDescriptions = umbrellaPolicyDescriptions;
                limitDescriptions = umbrellaLimitDescriptions;
                break;
            default:
                break;
        }

        foreach (string _policyDescription in policyDescriptions)
        {
            policyItems.Add(_policyDescription);
        }

        foreach (string _limitDescription in limitDescriptions)
        {
            limitItems.Add(_limitDescription);
        }    
    }


    public void SaveTenant()
    {

        string tenantid = Request["TenantId"];
        if (tenantid == null)
        {
            return;
        }
        tenantid = QuickPM.Util.FormatTenantId(tenantid);
        QuickPM.InsuranceCertificate ic = null;
        List<QuickPM.InsuranceCertificate> ics = QuickPM.InsuranceCertificate.GetInsuranceCertificates(tenant);
        if (ics.Count == 0)
        {
            ic = new QuickPM.InsuranceCertificate();
			ic.SetAssociatedId(tenant);            
            ic.BeginDate = DateTime.Now;
            ic.EndDate = DateTime.Now;                        
        }
        else
        {
            ic = ics[0];
        }
        DateTime beginIC = DateTime.Now;
        DateTime endIC = DateTime.Now;
        if(!DateTime.TryParse(TextBoxICBeginDate.Text, out beginIC))
        {
            beginIC = DateTime.Now;
        }
        if (!DateTime.TryParse(TextBoxICEndDate.Text, out endIC))
        {
            endIC = DateTime.Now;
        }
        ic.AdditionalInsured = TextBoxAdditionalInsured.Text.Trim();
        ic.InsuredLocation = TextBoxInsured.Text.Trim();
        ic.BeginDate = beginIC;
        ic.EndDate = endIC;
        ic.Save();        
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        SaveTenant();
        Session["Message"] = "<font color=\"red\">Successfully Saved</font>";
    }



    protected void LoadTenant()
    {
        string tenantid = Request["TenantId"];
        if (tenantid == null)
        {
            return;
        }
        tenantid = QuickPM.Util.FormatTenantId(tenantid);
        tenant = new QuickPM.Tenant(tenantid);
    }
}
