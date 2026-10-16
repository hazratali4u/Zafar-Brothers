using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form For Customer Ledger Report
/// </summary>
public partial class Forms_RptCustomerLedger : System.Web.UI.Page
{
    public static string opType="CR";//opType
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
         //   LoadArea();
            LoadData();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {      
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);        
    }

    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadData()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "CUSTOMER_ADDRESS");         
        }
    }

    /// <summary>
    /// Gets Customer Opening Balance
    /// </summary>
    private decimal LoadCustomerOpBalance()
    {
        if (drpDistributor.Items.Count > 0 && DrpCustomer.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomerOpBalance(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), int.Parse(DrpPrincipal.SelectedValue.ToString()));

            if (Convert.ToDecimal(dt.Rows[0][0]) < 0)
            {
                opType = "DR";
               
            }
            else if (Convert.ToDecimal(dt.Rows[0][0]) == 0)
            {
                opType = "";
               
            }
                return decimal.Parse(dt.Rows[0][0].ToString());
            
        }


        return 0;
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
       // LoadArea();
        LoadData();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  LoadData();
    }

    /// <summary>
    /// Shows Customer Ledger Either in PDF or in Excel
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        if (DrpCustomer.Items.Count > 0)
        {
            DataControl dc = new DataControl();
            CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
            RptCustomerController RptCustomerCtl = new RptCustomerController();
            DataSet ds;
            ds = RptCustomerCtl.CustomerLedger_View(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            CORNBusinessLayer.Reports.CrpCustomerLedger CrpReport = new CORNBusinessLayer.Reports.CrpCustomerLedger();
            CrystalDecisions.CrystalReports.Engine.ReportDocument SubReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            SubReport = CrpReport.OpenSubreport("srCustomerLedger");
            CrpReport.SetDataSource(ds);
            SubReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Op_Balance", LoadCustomerOpBalance());
            CrpReport.SetParameterValue("Op_Type", opType);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_Report_Type);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Customer Ledger Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Customer Ledger Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}