using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmSaleOrderView1 : System.Web.UI.Page
{
    readonly OrderEntryController oeCtrl = new OrderEntryController();
    private DataTable m_SKUDt;
    private DataTable dtInvoice;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("SaleOrderId");
            ltrlNew.Text = "<a href='frmSaleOrderNew.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26&Type=1'>Add Sale Order</a>";
            LoadData();           
            LoadGrid();           
        }
    }
    private void LoadData()
    {
        DataTable m_SKUDt = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.LongNullValue, Constants.Order_Pending_Id, 0, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));      
        Session.Add("m_SKUDt2", m_SKUDt);
    }    
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)Session["m_SKUDt2"];
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


    }
    protected void grdSaleOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Remove("m_SKUDt2");

        Session.Add("SaleOrderId", grdSaleOrder.Rows[e.NewEditIndex].Cells[0].Text);

        Response.Redirect("frmSaleOrderNew.aspx?LevelID="+ Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString()+ "&TopID=26&Type=1");
    }
    protected void grdSaleOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        long sale_order_Id = long.Parse(grdSaleOrder.Rows[e.RowIndex].Cells[0].Text);
        int distributor_ID = int.Parse(grdSaleOrder.Rows[e.RowIndex].Cells[1].Text);


         bool flag = oeCtrl.DeleteOrder(sale_order_Id,distributor_ID, true,0);
        if (flag)
        {
            this.LoadData();
            this.LoadGrid();
        }else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error. Please Try Again !')", true);

        }

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();

    }
}