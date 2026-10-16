using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;


public partial class Forms_rptCostofGoodSoldStatement : System.Web.UI.Page
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
         //   LoadPrincipal();
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
    /// Loads Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
    }

    /// <summary>
    /// Shows SKU Wise Branch Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {

        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetCostofGoodSold(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpCostOfGoodSoldStatement CrpReport = new CORNBusinessLayer.Reports.CrpCostOfGoodSoldStatement();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
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
    /// Shows SKU Wise Branch Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExce_Click(object sender, EventArgs e)
    {

        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetCostofGoodSold(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));


        CORNBusinessLayer.Reports.CrpCostOfGoodSoldStatement CrpReport = new CORNBusinessLayer.Reports.CrpCostOfGoodSoldStatement();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
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
}