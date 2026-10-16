using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;
using System.Data;
using CORNBusinessLayer.Reports;

public partial class Forms_frmSaleOrderView : System.Web.UI.Page
{

    readonly OrderEntryController oeCtrl = new OrderEntryController();
   
    private DataTable m_SKUDt;
    private DataTable dtInvoice;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("orderId");
            Session.Remove("invoiceId");
            LoadData();
            LoadInvoice();
            LoadGrid();
        }
    }

    private void LoadData()
    {
       // m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DataTable m_SKUDt = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 1, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
   
        Session.Add("m_SKUDt", m_SKUDt);
    }

    private void LoadInvoice()
    {
        DataTable dtInvoice = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 2, int.Parse(Session["UserID"].ToString()), Constants.DateNullValue);

        Session.Add("dtInvoice", dtInvoice);

        grdInvoice.DataSource = dtInvoice;
        grdInvoice.DataBind();
    }
    
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)Session["m_SKUDt"];
        if (m_SKUDt != null)
        {

            switch (ddSearchType.SelectedIndex)
            {

                case 1:
                    m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    m_SKUDt.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdSaleOrder.DataSource = m_SKUDt.DefaultView;
            grdSaleOrder.DataBind();
        }


        dtInvoice = (DataTable)Session["dtInvoice"];

        if (dtInvoice != null)
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dtInvoice.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdInvoice.DataSource = dtInvoice.DefaultView;
            grdInvoice.DataBind();
        }
        
    }
    
        
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
        
    }

    private void GetInvoice(long sId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(Convert.ToInt32(Session["Distributor_id"].ToString()));

            DataSet ds = null;

            ds = oeCtrl.SelectOrderforView(sId, 1);
            //if (ds.Rows.Count <= 0) return;

            CrpInvoicePrinting crpOrder = new CrpInvoicePrinting();
            crpOrder.SetDataSource(ds);
            crpOrder.Refresh();

            crpOrder.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
            crpOrder.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpOrder.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //---------------------------------------------------------------------------------\\

            //---------------------------------------------------------------------------------\\


            Session.Add("CrpReport", crpOrder);
            Session.Add("ReportType", 0);

            //const string url = "'Default.aspx'";
            //Response.Write("<script>window.open(" + url + ",'_blank');</script>");
            Session.Add("CrpReport", crpOrder);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);


        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void grdSaleOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "edt")
        {
            Session.Add("orderId", e.CommandArgument.ToString());
            Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&Type=2");
        }
        else if (e.CommandName == "edt2")
        {
            Session.Add("orderId", e.CommandArgument.ToString());
            Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&Type=1");
        }
        else if (e.CommandName == "edt3")
        {
            long sale_order_Id = long.Parse(e.CommandArgument.ToString());
            int distributor_ID = Convert.ToInt32(Session["Distributor_id"].ToString());

            bool flag = oeCtrl.DeleteOrder(sale_order_Id, distributor_ID, false, 1);
            if (flag)
            {
                LoadData();
                LoadInvoice();
                LoadGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error. Please Try Again !')", true);

            }
        }

    }

    protected void grdInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session.Add("invoiceId", e.CommandArgument.ToString());
        if (e.CommandName == "edt")
        {
            Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&Type=2");
        }
        else
        {
            Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&Type=1");
        }
    }
}