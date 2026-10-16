using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.Services;

public partial class Forms_frmSaleOrderNew : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();
    DataTable Dtsku_Price = new DataTable();

    private void loadOrder(long OrderId)
    {
        DataTable dtOrder = oeCtrl.SelectPendingOrder2(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.Order_Pending_Id, 0, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        DataRow[] foundRows = dtOrder.Select("SALE_ORDER_ID  = '" + OrderId + "'");

        if (foundRows.Length > 0)
        {
            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
            
            CustomerLedger();
            DataTable dtAddress = (DataTable)Session["dtCustomer"];
            DataRow[] foundRows2 = dtAddress.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

            if (foundRows2.Length > 0)
            {
                lblAdress.Text = "Address";
                HdnSaleExectiveID.Value = _dc.chkNull_0(foundRows2[0]["ASSIGNED_USER_ID"].ToString());
                lblCustomerAddress.Text = foundRows2[0]["ShowRoomAddress"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
            }
            ExistenOrderDetail(Convert.ToInt64(Session["SaleOrderId"]));
            txtRemarks.Text = foundRows[0]["remarks"].ToString();
            EnableDisableController(false);
            try
            {
                drpAssignedDistributor.SelectedValue = foundRows[0]["ASSIGNED_DISTRIBUTOR_ID"].ToString();
            }
            catch (Exception EX)
            {

            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.CreateTable();
            this.LoadDistributors();
            this.LoadAssingned();
            this.LoadCustomers();
            if (Session["SaleOrderId"] != null)
            {
                this.loadOrder(Convert.ToInt64(Session["SaleOrderId"]));
            }
            else
            {
                this.CustomerLedger();
            }
            ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
        }
    }

    private void LoadDistributors()
    {
        DistributorController distCtrl = new DistributorController();
        DataTable dt = distCtrl.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                clsWebFormUtil.FillDropDownList(ddlDistributor, dt, 0, 2);
            }
        }
    }

    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssingned();
        LoadCustomers();
        CustomerLedger();
        ddlCustomer.Focus();
    }
    private void LoadAssingned()
    {
        drpAssignedDistributor.Items.Clear();
        if (ddlDistributor.Items.Count > 0 )
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectDisToDisAssignment(int.Parse(ddlDistributor.SelectedValue.ToString()), Constants.IntNullValue, 1, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(drpAssignedDistributor, dt, 0, 2);
        }
       
    }
    private void LoadCustomers()
    {
        ddlCustomer.Items.Clear();
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer_SO(int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), 0, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {
                lblCustomerAddress.Text = dtCustomer.Rows[0]["ShowRoomAddress"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");
                Session.Add("dtCustomer", dtCustomer);
                HdnSaleExectiveID.Value = _dc.chkNull_0(dtCustomer.Rows[0]["ASSIGNED_USER_ID"].ToString());
            }else
            {
                lblCustomerAddress.Text = "";
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
                Session.Add("dtCustomer", dtCustomer);
                HdnSaleExectiveID.Value ="0";


            }
        }
    }

    private void CustomerLedger()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            decimal Opening = 0;
            DataTable dt = _customerCtrl.SelectCustomerCreditBalance(long.Parse(ddlCustomer.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), Constants.Advance_PaymentOrder_id);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Opening = Opening + decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));
                    lblLedgerBalance.Text = string.Format("{0:0,0.00}", Opening);
                }
                else
                {
                    lblLedgerBalance.Text = Convert.ToString(Opening);
                }
            }
            else
            {
                lblLedgerBalance.Text = Convert.ToString(Opening);
            }
        }
        else
        {
            lblLedgerBalance.Text = "0";
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CustomerLedger();
            lblCustomerAddress.Text = "";
            DataTable dtCustomer = (DataTable)Session["dtCustomer"];
            foreach (DataRow dr in dtCustomer.Rows)
            {
                if (dr["CUSTOMER_ID"].ToString() == ddlCustomer.SelectedValue)
                {
                    lblCustomerAddress.Text = dr["ShowRoomAddress"].ToString();
                    if (lblCustomerAddress.Text == "")
                    {
                        lblCustomerAddress.Text = "N/A";
                    }
                    HdnSaleExectiveID.Value = _dc.chkNull_0(dr["ASSIGNED_USER_ID"].ToString());
                    break;
                }
            }
            if (cbScan.Checked)
            {
                ScriptManager.GetCurrent(Page).SetFocus(txtSKUScan);
            }
            else
            {
                ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    #region Detail Panel

    DataTable orderSku = new DataTable();
    private static int RowNo;

    private void CreateTable()
    {
        orderSku = new DataTable();
        orderSku.Columns.Add("orderDetailId", typeof(long));
        orderSku.Columns.Add("SKU_ID", typeof(int));
        orderSku.Columns.Add("skuName", typeof(string));
        orderSku.Columns.Add("BAR_CODE", typeof(string));
        orderSku.Columns.Add("COLOR", typeof(string));
        orderSku.Columns.Add("price", typeof(decimal));
        orderSku.Columns.Add("qty", typeof(int));
        orderSku.Columns.Add("Ctn", typeof(int));
        orderSku.Columns.Add("Unit", typeof(int));
        orderSku.Columns.Add("amount", typeof(decimal));
        orderSku.Columns.Add("PrevRcdQty", typeof(int));
        Session.Add("orderSku", orderSku);
    }

    private bool CheckDublicateSKU()
    {
        orderSku = (DataTable)Session["orderSku"];
        DataRow[] foundRows;
        if (!cbScan.Checked)
        {
            foundRows = orderSku.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        }
        else
        {
            foundRows = orderSku.Select("BAR_CODE  = '" + txtSKUScan.Text.Trim().Substring(0, 12) + "'");
        }
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if(drpAssignedDistributor.Items.Count >0)
            {
                // move ahead
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Maped Distributor Not Found !');", true);
                return;
            }
            SKUPriceDetailController PController = new SKUPriceDetailController();
            if (cbScan.Checked)
            {
                Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(ddlDistributor.SelectedValue), txtSKUScan.Text.Trim().Substring(0, 12));
            }
            else
            {
                Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt32(hfSKUID.Value));
            }
            if (Dtsku_Price.Rows.Count > 0)
            {
                if (decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) > 0)
                {
                    orderSku = (DataTable)Session["orderSku"];
                    var qty = 0;
                    if (!cbScan.Checked)
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                    }
                    else
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                    }
                    if (btnSave.Text == "Add")
                    {
                        if (CheckDublicateSKU())
                        {
                            DataRow dr = orderSku.NewRow();
                           
                            if (CheckStock(qty, int.Parse(Dtsku_Price.Rows[0]["SKU_ID"].ToString()), Dtsku_Price.Rows[0]["SKU_NAME"].ToString(), int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString()))))
                            {
                                getstockprint((Dtsku_Price.Rows[0]["SKU_ID"].ToString()), Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt32(drpAssignedDistributor.SelectedValue),qty,0);
                                dr["SKU_ID"] = Dtsku_Price.Rows[0]["SKU_ID"];
                                dr["skuName"] = Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"];
                                dr["price"] = Dtsku_Price.Rows[0]["TRADE_PRICE"];
                                dr["BAR_CODE"] = Dtsku_Price.Rows[0]["BAR_CODE"];
                                dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                                dr["COLOR"] = txtColor.Text + " ";

                                if (!cbScan.Checked)
                                {
                                    dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                    dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                    dr["amount"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                                }
                                else
                                {
                                    dr["Unit"] = Convert.ToInt32(_dc.chkNull_0(txtQuantity.Text)) * 1;
                                    dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                    dr["amount"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                                }
                                orderSku.Rows.InsertAt(dr,0);
                            }
                            else
                            { return; }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + txtSKU.Text + " Already Exists ');", true);
                            return;
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        DataRow dr = orderSku.Rows[RowNo];
                        if (CheckStock(qty, int.Parse(Dtsku_Price.Rows[0]["SKU_ID"].ToString()), Dtsku_Price.Rows[0]["SKU_NAME"].ToString(), int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString()))))
                        {
                            getstockprint((Dtsku_Price.Rows[0]["SKU_ID"].ToString()), Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt32(drpAssignedDistributor.SelectedValue),qty, int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString())));

                            dr["SKU_ID"] = Dtsku_Price.Rows[0]["SKU_ID"];
                            dr["skuName"] = Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"];
                            dr["price"] = Dtsku_Price.Rows[0]["TRADE_PRICE"];
                            dr["BAR_CODE"] = Dtsku_Price.Rows[0]["BAR_CODE"];
                            dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                            dr["COLOR"] = txtColor.Text+" ";
                            if (!cbScan.Checked)
                            {
                                dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["amount"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                            else
                            {
                                dr["CurrentRcdQtyUnit"] = Convert.ToInt32(_dc.chkNull_0(txtQuantity.Text)) * 1;
                                dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["amount"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                        }
                        else
                        { return; }
                    }
                    Session.Add("orderSku", orderSku);
                    ClearDetail();
                    LoadGird();
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
                    }
                    else
                    {
                        ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Item Price!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
        }
    }

    protected void grdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;

        hfSKUID.Value = grdOrder.Rows[RowNo].Cells[0].Text;
        btnSave.Text = "Update";


        txtSKU.Text = grdOrder.Rows[RowNo].Cells[1].Text;
        txtSKU.Enabled = false;
        txtColor.Text= grdOrder.Rows[RowNo].Cells[2].Text;
        txtCtn.Text = grdOrder.Rows[RowNo].Cells[4].Text;
        txtQuantity.Text = grdOrder.Rows[RowNo].Cells[5].Text;
        txtCtn.Focus();
        cbScan.Checked = false;
        cbScan.Enabled = false;

    }

    protected void grdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        orderSku = (DataTable)Session["orderSku"];
        if (orderSku.Rows.Count > 0)
        {
            orderSku.Rows.RemoveAt(e.RowIndex);
            Session.Add("orderSku", orderSku);
            LoadGird();
        }
    }

    private void LoadGird()
    {
        int TotalValue = 0;
        int TotalValueCtn = 0;
        decimal TotalAmount = 0;
        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        foreach (DataRow dr in orderSku.Rows)
        {
            TotalValue += int.Parse(dr["Unit"].ToString());
            TotalValueCtn += int.Parse(dr["Ctn"].ToString());
            TotalAmount += decimal.Parse(dr["amount"].ToString());

        }
        txtTotalQty.Text = TotalValueCtn.ToString();
        txtTotalQtyUnit.Text = TotalValue.ToString();
        txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
        drpAssignedDistributor.Enabled = true;
        if (grdOrder.Rows.Count > 0)
        {
            drpAssignedDistributor.Enabled = false;
        }
    }

    private void ClearDetail()
    {
        txtQuantity.Text = "";
        txtCtn.Text = "0";
        txtSKU.Text = string.Empty;
        btnSave.Text = "Add";
        cbScan.Enabled = true;
        txtSKU.Enabled = true;
        txtColor.Text = "";
      //  lblstock.Text = "0-0";
    }
    #endregion
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
            orderSku = (DataTable)Session["orderSku"];
            if (orderSku.Rows.Count > 0)
            {
                decimal mTotalAmount = 0;
                int mTotalQty = 0;
                bool flagStock = true;
                foreach (DataRow dr in orderSku.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["amount"].ToString());
                    mTotalQty += int.Parse(dr["qty"].ToString());

                    if (!CheckStock(int.Parse(dr["qty"].ToString()), int.Parse(dr["SKU_ID"].ToString()), dr["skuName"].ToString(), int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString()))))
                    {
                        flagStock = false;
                        break;
                    }
                }

                if (!flagStock)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Check stock for all items');", true);
                    return;
                }
                int subDealerId = 0;

                int SaleExectiveID = Constants.IntNullValue;
                try
                {
                    if (HdnSaleExectiveID.Value != "" || HdnSaleExectiveID.Value != null)
                    {
                        SaleExectiveID = int.Parse(HdnSaleExectiveID.Value);
                    }
                }
                catch (Exception ex)
                { }
                if (Session["SaleOrderId"] == null)
                {
                    long sId = oeCtrl.AddOrder(int.Parse(ddlDistributor.SelectedValue), null, subDealerId, long.Parse(ddlCustomer.SelectedValue)
                    , 214, mTotalAmount, mTotalQty, Constants.Order_Pending_Id, orderSku, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, SaleExectiveID, int.Parse(drpAssignedDistributor.SelectedValue));
                    if (sId > 0)
                    {
                        GetOrder(sId);
                        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
                    }
                }
                else
                {
                    bool flag = oeCtrl.UpdateOrder(long.Parse(Session["SaleOrderId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                        subDealerId, int.Parse(ddlDistributor.SelectedValue), 214, null, long.Parse(ddlCustomer.SelectedValue), mTotalAmount, mTotalQty, orderSku, int.Parse(Session["UserId"].ToString()), txtRemarks.Text, SaleExectiveID, int.Parse(drpAssignedDistributor.SelectedValue));

                    if (flag)
                    {
                        Session.Remove("SaleOrderId");
                        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        // CreateTable();
        //LoadGird();
        ClearDetail();
        Session.Remove("SaleOrderId");
        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");

    }

    private void GetOrder(long sId)
    {
        try
        {
            #region Order
            DataSet ds = null;
            DocumentPrintController DPrint = new DocumentPrintController();

            DataTable dt = DPrint.SelectReportTitle(int.Parse(ddlDistributor.SelectedValue));
             RptSaleController RptSaleCtl = new RptSaleController();
            ds = RptSaleCtl.SelectDocument(sId, 1);

            ReportDocument crpCustomer = new ReportDocument();
            crpCustomer = new CrpPrintCustomerDocument();

            crpCustomer.SetDataSource(ds);
            crpCustomer.Refresh();

            crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpCustomer.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());

            Session.Add("crpCustomer", crpCustomer);
            Session.Add("ReportTypeCustomer", 0);

            const string url = "'RptGatePassCustomer.aspx'";
            //  Response.Write("<script>window.open(" + url + ",'_blank');</script>");

            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow1", script);

            ReportDocument crpCustomer2 = new ReportDocument();
            crpCustomer2 = new CrpPrintCustomerDocumentOffice();

            crpCustomer2.SetDataSource(ds);
            crpCustomer2.Refresh();

            crpCustomer2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpCustomer2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpCustomer2.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());

            //Session.Add("crpCustomer", crpCustomer);
            //Session.Add("ReportTypeCustomer", 0);


            Session.Add("ReportType", 0);
            Session.Add("CrpReport", crpCustomer2);
            const string url2 = "'Default.aspx'";
            // Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");
            const string script2 = "<script language='JavaScript' type='text/javascript'> window.open(" + url2 + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            cs.RegisterStartupScript(cstype, "OpenWindow2", script2);
            #endregion



            //DocumentPrintController DPrint = new DocumentPrintController();


            //DataTable dt = DPrint.SelectReportTitle(int.Parse(ddlDistributor.SelectedValue));


            //DataSet ds = null;

            //ds = oeCtrl.SelectOrderforView(sId, 0);


            ////if (ds.Rows.Count <= 0) return;

            //CrpOrderPrinting crpOrder = new CrpOrderPrinting();
            //crpOrder.SetDataSource(ds);
            //crpOrder.Refresh();

            //crpOrder.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
            //crpOrder.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            //crpOrder.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            ////---------------------------------------------------------------------------------\\

            ////---------------------------------------------------------------------------------\\


            ////Session.Add("CrpReport", crpOrder);
            //// Session.Add("ReportType", 0);

            //// const string url = "'Default.aspx'";
            //// Response.Write("<script>window.open(" + url + ",'_blank');</script>");
            //Session.Add("CrpReport", crpOrder);
            //Session.Add("ReportType", 0);
            //const string url = "'Default.aspx'";
            //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(cstype, "OpenWindow", script);


        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    #region Update Panel

    private void ExistenOrderDetail(long OrderId)
    {
        orderSku = oeCtrl.SelectOrderDetail(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.DateNullValue, 0);
        Session.Add("orderSku", orderSku);
        LoadGird();

    }

    private void EnableDisableController(bool CValue)
    {
        if (CValue == true)
        {
            ddlCustomer.Enabled = true;
            ddlDistributor.Enabled = true;
        }
        else
        {
            ddlCustomer.Enabled = false;
            ddlDistributor.Enabled = false;
        }
    }

    private void ClearMasterALL()
    {
        EnableDisableController(true);
        Session.Remove("orderSku");
        CreateTable();

        LoadGird();
        txtRemarks.Text = "";

        RowNo = 0;

        ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
    }

    #endregion

    private  void getstockprint(string skuid,int distId, int assigndistId,int qty,int PrevQty)
    {
        lblstock.Text = "0-0";
        PurchaseController mController = new PurchaseController();
        if (skuid != null && skuid != "" && assigndistId != Constants.IntNullValue && assigndistId != 0)
        {
            DataTable dtstock = mController.GetSKUClosingStockWebNew(distId,assigndistId, int.Parse(skuid.ToString()));
            if (dtstock.Rows.Count > 0)
            {
                lblstock.Text = ((int.Parse(dtstock.Rows[0][0].ToString()))+ PrevQty-qty).ToString();
            }
        } 
    }
    private bool CheckStock(int Qty, int SkuId, string SkuName, int PrevRcdQty)
    {

        if (Qty > 0)
        {
            PurchaseController mController = new PurchaseController();
            DataTable dtstock = mController.GetSKUClosingStockWebNew(int.Parse(ddlDistributor.SelectedValue),int.Parse(drpAssignedDistributor.SelectedValue), SkuId);
            if (dtstock.Rows.Count > 0)
            {
                if (Math.Round(decimal.Parse(dtstock.Rows[0][0].ToString())) + PrevRcdQty < Qty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " No Stock Found');", true);
                return false;
            }
        }
        return true;
    }
    [WebMethod]
    public static string getstock(string skuid, int distId,int assigndisId)
    {
        PurchaseController mController = new PurchaseController();
        if (skuid != null && skuid != "" && distId != Constants.IntNullValue && distId != 0)
        {
            DataTable dtstock = mController.GetSKUClosingStockWebNew(distId,assigndisId, int.Parse(skuid.ToString()));
            if (dtstock.Rows.Count > 0)
            {
                return Math.Round(decimal.Parse(dtstock.Rows[0][0].ToString())).ToString();
            }
        }
        return "0-0";
    }
}