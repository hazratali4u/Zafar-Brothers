using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;


public partial class Forms_RptSaleReturn : System.Web.UI.Page
{
    readonly RptSaleController RptSaleCtl = new RptSaleController();
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadSaleForce();
            LoadCustomer();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");

        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        DataTable dt2 = dt.Clone();
        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 5");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        clsWebFormUtil.FillDropDownList(drpDistributor, dt2, 0, 2, true);
    }
    private void LoadSaleForce()
    {
        drpSaleForce.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(drpReportType.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        drpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpSaleForce, m_dt, 0, 1);
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3, false);

        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }
    protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpReportType.SelectedValue == "1")
        {
            chkBranch.Visible = true;
            chkFinance.Visible = true;
            chkCustomer.Visible = true;
        }
        else
        {
            chkBranch.Visible = false;
            chkFinance.Visible = false;
            chkCustomer.Visible = false;
        }
    }
    private void ShowReport(int p_ReportType)
    {
        try
        {

            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            DataTable table = null;
            
            if (dt.Rows.Count > 0)
            {
                if (drpReportType.SelectedValue == "1")
                {
                    //------------if Document NO exist on txtDocumentNo.Text

                    if (txtDocumentNo.Text != "")
                    {
                        table = RptSaleCtl.SelectReturnDocumentNo(long.Parse(txtDocumentNo.Text.Trim()), 1);
                    }
                    
                    else
                    {
                        table = RptSaleCtl.SelectReturnDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
                            , Convert.ToInt32(drpSaleForce.SelectedValue), 1
                            , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
                    }
                    if (table != null)
                    {
                        if (table.Rows.Count > 0)
                        {

                            if (chkCustomer.Checked)
                            {
                                ReportDocument crpCustomer = new ReportDocument();
                                crpCustomer = new CORNBusinessLayer.Reports.CrpPrintCustomerSaleReturn();

                                crpCustomer.SetDataSource(table);
                                crpCustomer.Refresh();

                                crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                                crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                                crpCustomer.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

                                Session.Add("crpCustomer", crpCustomer);
                                Session.Add("ReportTypeCustomer", 0);

                                const string url = "'RptGatePassCustomer.aspx'";
                                Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                            }
                            if (chkBranch.Checked)
                            {
                                CrpPrintSaleReturn crpBranch = new CrpPrintSaleReturn();
                                crpBranch.SetDataSource(table);
                                crpBranch.Refresh();

                                crpBranch.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                                crpBranch.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                                crpBranch.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

                                Session.Add("CrpReport", crpBranch);
                                Session.Add("ReportType", 0);

                                const string url2 = "'Default.aspx'";
                                Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");

                            }
                            if (chkFinance.Checked)
                            {
                                CrpPrintFinanceSaleReturn crpFinance = new CrpPrintFinanceSaleReturn();
                                crpFinance.SetDataSource(table);
                                crpFinance.Refresh();

                                crpFinance.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                                crpFinance.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                                crpFinance.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

                                Session.Add("crpFinance", crpFinance);
                                Session.Add("ReportTypeFinance", 0);

                                const string url3 = "'RptGatePassFinance.aspx'";
                                Response.Write("<script>window.open(" + url3 + ",'_blank');</script>");
                            }
                        }
                    }
                }

                else if (drpReportType.SelectedValue == "2") //Gate Pass
                {
                    //------------if Document NO exist on txtDocumentNo.Text

                    if (txtDocumentNo.Text != "")
                    {
                        table = RptSaleCtl.SelectReturnDocumentNo(long.Parse(txtDocumentNo.Text.Trim()),2);

                    }
                    //------------------------------------------------------------------\\
                    else
                    {
                        table = RptSaleCtl.SelectReturnDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
                            , Convert.ToInt32(drpSaleForce.SelectedValue), 2
                            , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

                    }
                    if (table != null)
                    {
                        if (table.Rows.Count > 0)
                        {
                            CrpPrintGatePassReturn crpGatePass = new CrpPrintGatePassReturn();
                            crpGatePass.SetDataSource(table);
                            crpGatePass.Refresh();

                            crpGatePass.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                            crpGatePass.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                            crpGatePass.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());


                            Session.Add("CrpReport", crpGatePass);
                            Session.Add("ReportType", 0);

                            const string url = "'Default.aspx'";
                            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}