using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For Transfer In/Out Report
/// </summary>
public partial class Forms_rptTransferIn : System.Web.UI.Page
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
            //LoadPrincipal();
            LoadLocation();
            txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            txtToDate.Text   = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            drpPrincipal.DataSource = m_dt;
            drpPrincipal.DataTextField = "Company_Name";
            drpPrincipal.DataValueField = "Company_Id";
            drpPrincipal.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {        
        DrpLocation.Items.Clear();
        if (DrpReportType.SelectedIndex == 2)
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpLocation, dt, 0, 2);
        }
        else
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpLocation, dt, 0, 2);
        }
        
    }

    /// <summary>
    /// Shows Transfer In/Out Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            if (DrpReportType.SelectedIndex == 2)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferInOutValues CrpReport = new CORNBusinessLayer.Reports.CrpTransferInOutValues();
                DateTime parsed_date_fromdate = DateTime.Parse(txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DataSet ds = RptInventoryCtl.TransferInOutValue(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), RbTransferType.SelectedItem.Text, int.Parse(Session["UserId"].ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                FromDate = txtFromDate.Text;
                ToDate = txtToDate.Text;
                if (RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report value wise";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report value wise";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferType);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferIn CrpReport = new CORNBusinessLayer.Reports.CrpTransferIn();
                DateTime parsed_date_fromdate = DateTime.Parse(txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferIn(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), RbTransferType.SelectedItem.Text, DrpReportType.SelectedIndex);
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("BranchName", DrpLocation.SelectedItem.Text.ToString());
                FromDate = txtFromDate.Text;
                ToDate = txtToDate.Text;
                if (RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("TransferType", TransferType);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferTo_FromHeader);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Transfer In/Out Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (DrpReportType.SelectedIndex == 2)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();

                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferInOutValues CrpReport = new CORNBusinessLayer.Reports.CrpTransferInOutValues();
                DateTime parsed_date_fromdate = DateTime.Parse(txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferInOutValue(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), RbTransferType.SelectedItem.Text, int.Parse(Session["UserId"].ToString()));
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();


                FromDate = txtFromDate.Text;
                ToDate = txtToDate.Text;
                if (RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report value wise";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report value wise";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferType);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                RptInventoryController RptInventoryCtl = new RptInventoryController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;
                string ToDate = null;
                string TransferType = null;
                string TransferTo_FromHeader = null;
                CORNBusinessLayer.Reports.CrpTransferIn CrpReport = new CORNBusinessLayer.Reports.CrpTransferIn();
                DateTime parsed_date_fromdate = DateTime.Parse(txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(txtToDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                ToDate = parsed_date_todate.ToShortDateString();

                DataSet ds = RptInventoryCtl.TransferIn(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), RbTransferType.SelectedItem.Text, DrpReportType.SelectedIndex);

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("BranchName", DrpLocation.SelectedItem.Text.ToString());
                FromDate = txtFromDate.Text;
                ToDate = txtToDate.Text;
                if (RbTransferType.SelectedItem.Text == "Transfer In")
                {
                    TransferType = "Branch Transfer In Report";
                    TransferTo_FromHeader = "Transfer From";
                }
                else
                {
                    TransferType = "Branch Transfer Out Report";
                    TransferTo_FromHeader = "Transfer To";
                }
                CrpReport.SetParameterValue("FromDate", FromDate);
                CrpReport.SetParameterValue("ToDate", ToDate);
                CrpReport.SetParameterValue("TransferType", TransferType);
                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("TransferTo_FromHeader", TransferTo_FromHeader);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocation();
    }

}
