using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_RptInvoiceWiseSale : System.Web.UI.Page
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
            LoadPrincipal();
            LoadDistributor();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

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
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();

       
            //DistributorController DController = new DistributorController();
            //DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            //drpDistributor.Items.Add(new ListItem("All", "0"));
            //clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);

            
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(this.Session["UserId"].ToString()), 2, 1, int.Parse(this.Session["CompanyId"].ToString()));
            drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
       
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales Either in PDF Or in Excel
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        string InvoiceId = "0";
       // string DistributorId = "0";

        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();

            foreach (GridViewRow dr in Grid_users.Rows)
            {
                CheckBox chbSelect = (CheckBox)(dr.Cells[0].FindControl("ChbCustomer"));
                if (chbSelect.Checked)
                {
                    InvoiceId = InvoiceId + "," + dr.Cells[1].Text;                   
                }
            }

            DataSet ds = RptCustomerCtl.SelectInvoiceDetail(Constants.IntNullValue, InvoiceId,int.Parse(drpDistributor.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1,int .Parse (this.Session["UserID"].ToString()));
            DataTable dt = mDocumentPrntControl.SelectReportTitle(Constants.IntNullValue);
            CORNBusinessLayer.Reports.CrpBillInvoiceWiseReport CrpReport = new CORNBusinessLayer.Reports.CrpBillInvoiceWiseReport();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("DISTRIBUTOR", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("PRINCIPAL", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_Report_Type);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
      //  }
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void BtnViewPdf_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Loads Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RblCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistributor();
    }

    protected void btnGetData_Click(object sender, EventArgs e)
    {
        RptCustomerController RptCustomerCtl = new RptCustomerController();

        DataTable dt2 = RptCustomerCtl.SelectInvoiceDetail2(Constants.IntNullValue, null, int.Parse(drpDistributor.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 2, int.Parse(this.Session["UserID"].ToString()));

        Grid_users.DataSource = dt2;
        Grid_users.DataBind();
    }
}