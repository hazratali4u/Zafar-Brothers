using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form For Credit Aging Report
/// </summary>
public partial class Forms_RptCreditAging : System.Web.UI.Page
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
            this.DistributorType();
            this.LoadAssingned();
            this.LoadSaleForce();
            this.LoadRegion();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtDocmentDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Location Types To LocationType Combo
    /// </summary>
    private void DistributorType()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddDistributorType, dt, 0, 2);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadAssingned()
    {
        if (ddDistributorType.Items.Count > 0)
        {
            drpDistributor.Items.Clear();
            if (rbtSotBy.SelectedIndex == 1)
            {
                UserController mUserController = new UserController();
                DataTable dt = mUserController.SelectUserAssignment(int.Parse(this.Session["UserId"].ToString()), int.Parse(ddDistributorType.SelectedValue.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
                //drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
                
            }
            else
            {
                UserController mUserController = new UserController();
                DataTable dt = mUserController.SelectUserAssignment(int.Parse(this.Session["UserId"].ToString()), int.Parse(ddDistributorType.SelectedValue.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
                drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
            }
        }
    }

    private void LoadSaleForce()
    {
        DrpsaleForce.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(0, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        DrpsaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpsaleForce, m_dt, 0, 1);
    }

    protected void LoadRegion()
    {
        DrpRegion.Items.Clear();
           GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpRegion, dt, 0, 4);
        
    }

    /// <summary>
    /// Shows Credit Aging Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void ShowReport(int type)
    {
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataSet ds = RptCustomerCtl.GetCreditAgingReport(int.Parse(this.ddDistributorType.SelectedItem.Value), int.Parse(DrpsaleForce.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), rbtSotBy.SelectedIndex, DateTime.Parse(txtDocmentDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpRegion.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpCreditAgingReport CrpReport = new CORNBusinessLayer.Reports.CrpCreditAgingReport();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("UptoDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Principal", DrpsaleForce.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportType", rbtSotBy.SelectedIndex.ToString());
        CrpReport.SetParameterValue("ChannelType", DrpRegion.SelectedItem.Text);
        CrpReport.SetParameterValue("location", drpDistributor.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
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
        this.LoadAssingned();
        LoadSaleForce();
    }

    /// <summary>
    /// Shows Credit Aging Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {

        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataSet ds = RptCustomerCtl.GetCreditAgingReport(int.Parse(this.ddDistributorType.SelectedItem.Value), int.Parse(DrpsaleForce.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), rbtSotBy.SelectedIndex, DateTime.Parse(txtDocmentDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"),int.Parse(DrpRegion.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpCreditAgingReport CrpReport = new CORNBusinessLayer.Reports.CrpCreditAgingReport();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("UptoDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Principal", DrpsaleForce.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportType", rbtSotBy.SelectedIndex.ToString());
        CrpReport.SetParameterValue("ChannelType", DrpRegion.SelectedItem.Text);
        CrpReport.SetParameterValue("location", drpDistributor.SelectedItem.Text);

        string path = CORNCommon.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }               
    }

    /// <summary>
    /// Sets Visibility of Controls as Per Rpeort Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void rbtSotBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssingned();
        if (rbtSotBy.SelectedIndex == 2)
        {
            txtDocmentDate.Visible = true;
            ibtnStartDate.Visible = true;
            Label3.Visible = true; 
        }
        else
        {
            txtDocmentDate.Visible = false;
            ibtnStartDate.Visible = false;
            Label3.Visible = false; 
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }
}
