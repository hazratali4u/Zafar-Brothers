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
public partial class Forms_frmSalePersonPerformance : System.Web.UI.Page
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
            LoadDistributor();
            LoadSaleForce();
            LoadPrincipal();
            LoadRegion();
            LoadSeason();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
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
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
      //  Drpprincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);

        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            chbPrincipal.Items[i].Selected = true;
        }
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
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        
        drpDistributor.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), 2, 1, int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
    }
    
    /// <summary>
    /// Shows Sale Person DSR in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Sale Person DSR in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
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

        ds = RptSaleCtl.SelectPerformanceReport(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SalePerson_IDs .ToString(), 0, Region_IDs.ToString(),Season_IDs.ToString(), PRINCIPAL_IDs.ToString());
      

        CORNBusinessLayer.Reports.CrpSalemanPerformance CrpReport = new CORNBusinessLayer.Reports.CrpSalemanPerformance();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("SalePerson", SalePersons.ToString());
        CrpReport.SetParameterValue("Principal", principal.ToString());
        CrpReport.SetParameterValue("Season", Seasons.ToString());
        CrpReport.SetParameterValue("Region", Regions.ToString());

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", p_ReprotType);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }


    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }
}
