using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport : System.Web.UI.Page
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
            this.LoadPrincipal();
            LoadDistributor();
            LoadAccountDetail();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
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
    /// Loads Accont Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
       // if (drpDistributor.SelectedValue == Constants.IntNullValue.ToString())
       // {
            DataTable dtHead = mAccountController.SelectAccountHead(Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_HEAD", true);
        //}
        //else
       // {
         //   DataTable dtHead = mAccountController.SelectAccountHead(Constants.IntNullValue, Constants.LongNullValue, 0);
           // clsWebFormUtil.FillDropDownList(ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);

            //DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, Constants.LongNullValue, Convert.ToInt32(drpDistributor.SelectedValue));
            //clsWebFormUtil.FillDropDownList(ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);
    //    }
    }

    /// <summary>
    /// Shows General Ledger in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        DataControl dc = new DataControl();
        ds = RptAccountCtl.GeneralLedger_View(int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0,int .Parse(this.Session["UserID"].ToString()));
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal OpeningValue = RptAccountCtl.GeneralLedgerOpening(int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0,int.Parse(this.Session["UserID"].ToString()));
        CrpLedgerView CrpReport = new CrpLedgerView();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("OpeningValue", OpeningValue.ToString());

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows General Ledger in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        ds = RptAccountCtl.GeneralLedger_View(int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                   DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0, int.Parse(this.Session["UserID"].ToString()));
        decimal OpeningValue = RptAccountCtl.GeneralLedgerOpening(int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt64(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0, int.Parse(this.Session["UserID"].ToString()));
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CrpLedgerView CrpReport = new CrpLedgerView();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("OpeningValue", OpeningValue.ToString());
        string path = Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";
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
    //protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    // //   LoadAccountDetail();
    //}
    
}