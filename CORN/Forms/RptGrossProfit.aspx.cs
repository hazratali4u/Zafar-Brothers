using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For Gross Profit Report
/// </summary>
public partial class Forms_RptGrossProfit : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            LoadDistributor();

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        DataSet ds = _dPrint.SelectRptGrossProfit(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        DataTable dt = null;
        if (drpDistributor.SelectedIndex == 0)
        { dt = _dPrint.SelectReportTitle(Constants.IntNullValue); }
        else
        { dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString())); }

        var crpReport = new CrpGrossProfitReport();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);


        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        DataSet ds = _dPrint.SelectRptGrossProfit(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()),
                       DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        DataTable dt = null;
        if (drpDistributor.SelectedIndex == 0)
        { dt = _dPrint.SelectReportTitle(Constants.IntNullValue); }
        else
        { dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue)); }

        var crpReport = new CrpGrossProfitReport();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        string path = Configuration.GetAppInstallationPath() + "\\Exported.xls";
        crpReport.SetDatabaseLogon("sa", "123");
        crpReport.ExportToDisk(ExportFormatType.Excel, path);
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