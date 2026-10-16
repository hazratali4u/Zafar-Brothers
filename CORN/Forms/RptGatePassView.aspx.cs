using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptGatePassView : System.Web.UI.Page
{
    readonly RptSaleController RptSaleCtl = new RptSaleController();
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtStartDate.Attributes.Add("readonly", "readonly");
            LoadGrid();
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

    private void LoadGrid()
    {
        grdInvoices.DataSource = null;
        grdInvoices.DataBind();

        DataTable table = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue
                          , Constants.IntNullValue, 7
                          , DateTime.Parse(txtStartDate.Text + " 00:00:00"), Constants.DateNullValue);

        if (table.Rows.Count > 0)
        {
            grdInvoices.DataSource = table;
            grdInvoices.DataBind();
        }
        
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
   
    private void ShowReport(int p_ReportType)
    {
        string InvoiceId = "";

        foreach (GridViewRow dr in grdInvoices.Rows)
        {
            CheckBox chbSelect = (CheckBox)(dr.Cells[0].FindControl("ChbInvoice"));
            if (chbSelect.Checked)
            {
                InvoiceId = InvoiceId + "," + dr.Cells[1].Text;
            }
        }

        DataSet ds = null;

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));



        if (InvoiceId != "")
        {
            ds = RptSaleCtl.SelectDocumentforPrint(InvoiceId, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue
                              , Constants.IntNullValue, 8
                              , DateTime.Parse(txtStartDate.Text + " 00:00:00"), Constants.DateNullValue);

            DataTable table = ds.Tables["spGetPrintSaleDocument"];
            if (table.Rows.Count > 0)
            {
                CrpPrintGatePassWareHouse crpGatePass = new CrpPrintGatePassWareHouse();
                crpGatePass.SetDataSource(ds);
                crpGatePass.Refresh();

                crpGatePass.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                crpGatePass.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                crpGatePass.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());


                Session.Add("CrpReport", crpGatePass);
                Session.Add("ReportType", 0);

                const string url = "'Default.aspx'";
                Response.Write("<script>window.open(" + url + ",'_blank');</script>");
            }
            ds = RptSaleCtl.SelectDocumentforPrint(InvoiceId, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue
                             , Constants.IntNullValue, 9
                             , DateTime.Parse(txtStartDate.Text + " 00:00:00"), Constants.DateNullValue);
            
                DataTable table2 = ds.Tables["spGetPrintSaleDocument"];
                if (table2.Rows.Count > 0)
                {
                    CrpPrintGatePassWareHouse2 crpGatePass2 = new CrpPrintGatePassWareHouse2();
                    crpGatePass2.SetDataSource(ds);
                    crpGatePass2.Refresh();

                    crpGatePass2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpGatePass2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpGatePass2.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

                    Session.Add("CrpReport2", crpGatePass2);
                    Session.Add("ReportType2", 0);

                    const string url2 = "'Default2.aspx'";
                    Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");
                }
        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    
}