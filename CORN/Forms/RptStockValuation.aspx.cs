using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form For Stock Valuation Report
/// </summary>
public partial class Forms_RptStockReconcilation : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
        readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadGender();
            this.LoadCategory();
            this.LoadSeason();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadSeason()
    {
        
        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(chbSeason, dt, 0, 3, true);

    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        // DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);
    }
    private void LoadGender()
    {

       var _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(chbGender, _mDt, 0, 3, true);

    }

    private void LoadCategory()
    {
       

       var _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(chbCategory, _mDt, 0, 3, true);

    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Shows Stock Valuation Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Stock Valuation Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Shows Stock Valuation Report in Either PDF Or in Excel
    /// </summary>
    /// <param name="p_Report_Type"></param>
    private void ShowReport(int p_Report_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        #region Principal
        string Principal_IDs = null;
        string Principal = null;

        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            if (chbPrincipal.Items[i].Selected == true)
            {
                Principal_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
                Principal += chbPrincipal.Items[i].Text.ToString() + ",";
            }
        }
        if (chbPrincipal == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
            return;
        }
        #endregion
        #region Category
        string Categoru_IDs = null;
        string Categorys = null;

        for (int i = 0; i < chbCategory.Items.Count; i++)
        {
            if (chbCategory.Items[i].Selected == true)
            {
                Categoru_IDs += chbCategory.Items[i].Value.ToString() + ",";
                Categorys += chbCategory.Items[i].Text.ToString() + ",";
            }
        }
        if (chbCategory == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Category.');", true);
            return;
        }
        #endregion
        #region Gender
        string Gender_IDs = null;
        string Genders = null;

        for (int i = 0; i < chbGender.Items.Count; i++)
        {
            if (chbGender.Items[i].Selected == true)
            {
                Gender_IDs += chbGender.Items[i].Value.ToString() + ",";
                Genders += chbGender.Items[i].Text.ToString() + ",";
            }
        }
        if (chbGender == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Gender.');", true);
            return;
        }
        #endregion
        #region Season
        string Season_IDs = null;
        string Seasons = null;

        for (int i = 0; i < chbSeason.Items.Count; i++)
        {
            if (chbSeason.Items[i].Selected == true)
            {
                Season_IDs += chbSeason.Items[i].Value.ToString() + ",";
                Seasons += chbSeason.Items[i].Text.ToString() + ",";
            }
        }
        if (chbSeason == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
            return;
        }
        #endregion

        DataSet ds = RptInventoryCtl.SelectStockValuation(DateTime.Parse(txtEndDate.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Principal_IDs.ToString(), int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(rblReportType.SelectedValue), Categoru_IDs.ToString(), Gender_IDs.ToString(), Season_IDs.ToString());

        if (drpDistributor.SelectedItem.Text == "All")
        {
            CORNBusinessLayer.Reports.CrpStockValuationDetailAllLocations CrpReportDetailAll = new CORNBusinessLayer.Reports.CrpStockValuationDetailAllLocations();
            CrpReportDetailAll.SetDataSource(ds);
            CrpReportDetailAll.Refresh();
            CrpReportDetailAll.SetParameterValue("division", drpDistributor.SelectedItem.Text);
            CrpReportDetailAll.SetParameterValue("todate", txtEndDate.Text);
            CrpReportDetailAll.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReportDetailAll.SetParameterValue("Principal", Principal.ToString());
            CrpReportDetailAll.SetParameterValue("Price", rblReportType.SelectedItem.Text);
            this.Session.Add("CrpReport", CrpReportDetailAll);
            this.Session.Add("ReportType", p_Report_Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }

        else
        {
            CORNBusinessLayer.Reports.CrpStockValuationDetailSingleLocations CrpReportDetailSingle = new CORNBusinessLayer.Reports.CrpStockValuationDetailSingleLocations();
            CrpReportDetailSingle.SetDataSource(ds);
            CrpReportDetailSingle.Refresh();
            CrpReportDetailSingle.SetParameterValue("division", drpDistributor.SelectedItem.Text);
            CrpReportDetailSingle.SetParameterValue("todate", txtEndDate.Text);
            CrpReportDetailSingle.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReportDetailSingle.SetParameterValue("Principal", Principal.ToString());
            CrpReportDetailSingle.SetParameterValue("Price", rblReportType.SelectedItem.Text);
            this.Session.Add("CrpReport", CrpReportDetailSingle);
            this.Session.Add("ReportType", p_Report_Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
    }
}
