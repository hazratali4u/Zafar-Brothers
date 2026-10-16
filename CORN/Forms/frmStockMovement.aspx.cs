using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// 
/// Form For Sale Person DSR Report
/// </summary>
public partial class frmStockMovement : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCountryGrid();
            LoadDistributor();
            LoadSaleForce();
            LoadPrincipal();
            LoadRegion();
            LoadSeason();
            LoadCategory();
            LoadGender();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    private void LoadCountryGrid()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        DataTable dt = _mHerController.SelectSkuHierarchy_ZB(Constants.SKUCountry, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillListBox(chbCountry, dt, 1, 4, false);
        for (int i = 0; i < chbCountry.Items.Count; i++)
        {
            chbCountry.Items[i].Selected = true;
        }
    }
    private void LoadGender()
    {
        ChbGender.Items.Clear();
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbGender, dt, 0, 3, false);
        for (int i = 0; i < ChbGender.Items.Count; i++)
        {
            ChbGender.Items[i].Selected = true;
        }
    }
    private void LoadCategory()
    {
        ChbCategory.Items.Clear();
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbCategory, dt, 0, 3, false);
        for (int i = 0; i < ChbCategory.Items.Count; i++)
        {
            ChbCategory.Items[i].Selected = true;
        }
    }

    private void LoadSeason()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        ChbSeason.Items.Clear();

        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        // DrpSeason.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillListBox(ChbSeason, dt, 0, 3, false);
        for (int i = 0; i < ChbSeason.Items.Count; i++)
        {
            ChbSeason.Items[i].Selected = true;
        }
    }
    protected void LoadRegion()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillListBox(this.chbRegion, dt, 0, 4, true);
        for (int i = 0; i < chbRegion.Items.Count; i++)
        {
            chbRegion.Items[i].Selected = true;
        }
    }
    private void LoadPrincipal()
    {
        string COUNTRY_IDs = null;
        for (int i = 0; i < chbCountry.Items.Count; i++)
        {
            if (chbCountry.Items[i].Selected == true)
            {
                COUNTRY_IDs += chbCountry.Items[i].Value.ToString() + ",";

            }
        }
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice4(COUNTRY_IDs, int.Parse(this.Session["UserId"].ToString()));
        //  Drpprincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);

        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            chbPrincipal.Items[i].Selected = true;
        }
        //  SKUPriceDetailController PController = new SKUPriceDetailController();
        //  DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        ////  Drpprincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //  clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);

        //  for (int i = 0; i < chbPrincipal.Items.Count; i++)
        //  {
        //      chbPrincipal.Items[i].Selected = true;
        //  }
    }
    private void LoadSaleForce()
    {
        chbSalePerson.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(0, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        //  drpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillListBox(chbSalePerson, m_dt, 0, 1);
        for (int i = 0; i < chbSalePerson.Items.Count; i++)
        {
            chbSalePerson.Items[i].Selected = true;
        }
    }


    private void LoadDistributor()
    {

        drpDistributor.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), 2, 1, int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
    }


    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }


    private void ShowReport(int p_ReprotType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        DataSet ds = null;
        DataControl dc = new DataControl();
        #region Reagon
        string Region_IDs = null;
        string Regions = null;
        for (int i = 0; i < chbRegion.Items.Count; i++)
        {
            if (chbRegion.Items[i].Selected == true)
            {
                Region_IDs += chbRegion.Items[i].Value.ToString() + ",";
                Regions += chbRegion.Items[i].Text.ToString() + ",";
            }
        }
        if (Region_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Region.');", true);
            return;
        }
        #endregion
        #region Sale Person
        string SalePerson_IDs = null;
        string SalePersons = null;
        for (int i = 0; i < chbSalePerson.Items.Count; i++)
        {
            if (chbSalePerson.Items[i].Selected == true)
            {
                SalePerson_IDs += chbSalePerson.Items[i].Value.ToString() + ",";
                SalePersons += chbSalePerson.Items[i].Text.ToString() + ",";
            }
        }
        if (SalePerson_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Sale Person.');", true);
            return;
        }
        #endregion

        #region Season
        string Season_IDs = null;
        string Seasons = null;
        for (int i = 0; i < ChbSeason.Items.Count; i++)
        {
            if (ChbSeason.Items[i].Selected == true)
            {
                Season_IDs += ChbSeason.Items[i].Value.ToString() + ",";
                Seasons += ChbSeason.Items[i].Text.ToString() + ",";
            }
        }
        if (Season_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
            return;
        }
        #endregion

        #region Principal
        string PRINCIPAL_IDs = null;
        string principal = null;
        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            if (chbPrincipal.Items[i].Selected == true)
            {
                PRINCIPAL_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
                principal += chbPrincipal.Items[i].Text.ToString() + ",";
            }
        }
        if (PRINCIPAL_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
            return;
        }
        #endregion

        #region Category
        string Category_IDs = null;
        string Category = null;
        for (int i = 0; i < ChbCategory.Items.Count; i++)
        {
            if (ChbCategory.Items[i].Selected == true)
            {
                Category_IDs += ChbCategory.Items[i].Value.ToString() + ",";
                Category += ChbCategory.Items[i].Text.ToString() + ",";
            }
        }
        if (Category_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Category.');", true);
            return;
        }
        #endregion


        #region Gender
        string Gender_IDs = null;
        string Gender = null;
        for (int i = 0; i < ChbGender.Items.Count; i++)
        {
            if (ChbGender.Items[i].Selected == true)
            {
                Gender_IDs += ChbGender.Items[i].Value.ToString() + ",";
                Gender += ChbGender.Items[i].Text.ToString() + ",";
            }
        }
        if (Gender_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Gender.');", true);
            return;
        }
        #endregion
        if (chbWithImage.Checked == false)
        {
            ds = RptSaleCtl.SelectStockMovementReport(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SalePerson_IDs.ToString(), 0, Region_IDs.ToString(), Season_IDs.ToString(), PRINCIPAL_IDs.ToString(), Category_IDs.ToString(), Gender_IDs.ToString(), 0, 0);


            CORNBusinessLayer.Reports.CrpStockMovement CrpReport = new CORNBusinessLayer.Reports.CrpStockMovement();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Sale_per", dc.chkNull_0(txtsalePer.Text));
            CrpReport.SetParameterValue("Days", dc.chkNull_0(txtDays.Text));
            //CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_ReprotType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            ds = RptSaleCtl.SelectStockMovementReport2(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SalePerson_IDs.ToString(), 0, Region_IDs.ToString(), Season_IDs.ToString(), PRINCIPAL_IDs.ToString(), Category_IDs.ToString(), Gender_IDs.ToString(), 0, 0);


            CORNBusinessLayer.Reports.CrpStockMovement2 CrpReport = new CORNBusinessLayer.Reports.CrpStockMovement2();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Sale_per", dc.chkNull_0(txtsalePer.Text));
            CrpReport.SetParameterValue("Days", dc.chkNull_0(txtDays.Text));
            CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_ReprotType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }

    }


    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }

    protected void chbCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        chbPrincipal.Items.Clear();
        string COUNTRY_IDs = null;
        for (int i = 0; i < chbCountry.Items.Count; i++)
        {
            if (chbCountry.Items[i].Selected == true)
            {
                COUNTRY_IDs += chbCountry.Items[i].Value.ToString() + ",";

            }
        }
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice4(COUNTRY_IDs, int.Parse(this.Session["UserId"].ToString()));
        //  Drpprincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);

        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            chbPrincipal.Items[i].Selected = true;
        }
    }
}