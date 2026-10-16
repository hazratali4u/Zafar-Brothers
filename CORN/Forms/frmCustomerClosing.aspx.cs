using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;


public partial class Forms_frmCustomerClosing : System.Web.UI.Page
{   
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadRegion();
            LoadSaleForce();
            LoadCreditCustomer();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           
        }
    }


    private void LoadSaleForce()
    {

        DrpSalePerson.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectDistributorSalesForceType(int.Parse(drpDistributor.SelectedValue.ToString()),Constants.SALES_FORCE_SALESPERSON, true);
        DrpSalePerson.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpSalePerson, m_dt, 0, 6);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }
    protected void LoadRegion()
    {
        DrpRegion.Items.Clear();
        GeoHierarchyController GControler = new GeoHierarchyController();

        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDropDownList(this.DrpRegion, dt, 0, 4, false);

    }
    /// <summary>
    /// Loads Credit Customers To Customer Combo
    /// </summary>
    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        CustomerDataController mController = new CustomerDataController();
        DataTable dt = mController.SelectAllCustomer(int.Parse (drpDistributor.SelectedValue),int.Parse(DrpRegion .SelectedValue), Constants.IntNullValue);
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "CUSTOMER_ADDRESS");
        if (DrpCustomer.Items.Count <= 0)
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    /// <summary>
    /// Shows Credit Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Credit Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Shows Credit Report Either in PDF Or in Excel
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DocumentPrintController mController = new DocumentPrintController();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ReportDocument CrpReport = new ReportDocument();
        if (rdbType.SelectedValue == "1")
        {
            CrpReport = new CrpCustomerCreditSummary2();
        }
        else
        {

            CrpReport = new CrpCustomerCreditSummaryDetail();
        }
        DataSet ds = RptCustomerCtl.SelectCustomerCreditDetail(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text)
            , int.Parse(Session["UserId"].ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()), Convert.ToInt32(rdbType.SelectedValue), int.Parse(DrpRegion.SelectedValue), int.Parse(DrpSalePerson.SelectedValue));        
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Region", DrpRegion.SelectedItem.Text);
        CrpReport.SetParameterValue("SP", DrpSalePerson.SelectedItem.Text);
        CrpReport.SetParameterValue("To_date", txtStartDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", p_Report_Type);
        const string url = "'Default.aspx'";
        const  string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void DrpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCreditCustomer();
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
        LoadCreditCustomer();
    }


}