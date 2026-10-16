using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form For Sales & Closing Stock Report
/// </summary>
public partial class Forms_RptDistributorReports : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DistributorType();
            LoadAssingned();
            LoadPrincipal();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Location Types
    /// </summary>
    private void DistributorType()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddDistributorType, dt, 0, 2);
    }

    /// <summary>
    /// Loads User Assigned Locations To Location Combo
    /// </summary>
    private void LoadAssingned()
    {
        if (ddDistributorType.Items.Count > 0)
        {
            drpDistributor.Items.Clear();    
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), int.Parse(ddDistributorType.SelectedValue.ToString()), 1, int.Parse(Session["CompanyId"].ToString()));
            drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Shows Sales & Closing Stock in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetRegionSaleDetail(int.Parse(ddDistributorType.SelectedItem.Value), int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), Constants.IntNullValue, DrpReportType.SelectedIndex, DrpUnitType.SelectedIndex, int.Parse(Session["UserId"].ToString()));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        CORNBusinessLayer.Reports.RegionWiseSaleReport CrpReport = new CORNBusinessLayer.Reports.RegionWiseSaleReport();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("fromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("ReportTitle", "Daily " + DrpReportType.SelectedItem.Text + " In " + DrpUnitType.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);  
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script); 

    }

    /// <summary>
    /// Loads Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
    }

    /// <summary>
    /// Shows Sales & Closing Stock in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetRegionSaleDetail(int.Parse(ddDistributorType.SelectedItem.Value), int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), Constants.IntNullValue, DrpReportType.SelectedIndex, DrpUnitType.SelectedIndex, int.Parse(Session["UserId"].ToString()));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        CORNBusinessLayer.Reports.RegionWiseSaleReport CrpReport = new CORNBusinessLayer.Reports.RegionWiseSaleReport();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("fromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("todate", txtEndDate.Text);
        CrpReport.SetParameterValue("ReportTitle", "Daily Depot " + DrpReportType.SelectedItem.Text + " In " + DrpUnitType.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 1);  
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script); 
    }
}