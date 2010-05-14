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

public partial class Insurance_InsurancePolicy : System.Web.UI.UserControl
{
    public bool Adding
    {
        get;
        set;
    }

    public string ParentId
    {
        get;
        set;
    }

    public QuickPM.InsurancePolicy.PolicyCategories PolicyCategory
    {
        get;
        set;
    }
    public delegate void AddFunction(string parentId, long policyId);
    private AddFunction addFunc;
    public void SetAddFunction(AddFunction addFunc)
    {
        this.addFunc = addFunc;
    }
    QuickPM.InsurancePolicy policy = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        if (!Adding)
        {
            LoadPolicy();
            if (!policy.ACL.CanWrite(QuickPM.Database.GetUserId()))
            {
                QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
                ButtonSubmit.Enabled = false;
                ButtonSubmit.Visible = false;
            }
            return;
        }


    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (Adding)
        {
            AddPolicy();
        }
        SavePolicy();
        TextBoxInsurer.Text = "";
        TextBoxBeginPeriod.Text = "";
        TextBoxEndPeriod.Text = "";
        TextBoxLimitAmount.Text = "";
        TextBoxPolicyNumber.Text = "";
        Response.Redirect(Request.Url.PathAndQuery);
    }

    private void AddPolicy()
    {
        policy = new QuickPM.InsurancePolicy();
                
    }

    private void SavePolicy()
    {
        policy.BeginDate = DateTime.Parse(TextBoxBeginPeriod.Text);
        policy.EndDate = DateTime.Parse(TextBoxEndPeriod.Text);
        policy.LimitAmount = decimal.Parse(TextBoxLimitAmount.Text);
        policy.LimitDescription = DropDownListLimitDescription.Text;
        policy.PolicyDescription = DropDownListPolicyDescription.Text;
        policy.Insurer = TextBoxInsurer.Text.Trim();
        policy.PolicyNumber = TextBoxPolicyNumber.Text.Trim();
        policy.PolicyCategory = PolicyCategory;
        policy.Save();
        if (Adding)
        {
            addFunc(ParentId, policy.Id); 
        }
        //Response.Redirect(Request.Url.ToString());
    }
    private void LoadPolicy()
    {
        string policyId = Request["PolicyId"];
        if (policyId == null)
        {
            return;
        }

        policy = new QuickPM.InsurancePolicy(long.Parse(policyId));
    }
}
