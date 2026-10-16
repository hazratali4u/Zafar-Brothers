using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_frmSaleOrder : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    private void loadOrder(long OrderId)
    {
        DataTable dtOrder = oeCtrl.SelectPendingOrder2(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.Order_Pending_Id, 0, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        DataRow[] foundRows = dtOrder.Select("SALE_ORDER_ID  = '" + OrderId + "'");

        if (foundRows.Length > 0)
        {
            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
            rdbCustomerAddress.SelectedValue = foundRows[0]["MANUAL_ORDER_ID"].ToString();
            CustomerLedger();

            if (rdbCustomerAddress.SelectedValue == "2")
            {
                CustomerAddress();
                rdbSubDealers.SelectedValue = foundRows[0]["DELIVERYMAN_ID"].ToString();
            }
            else
            {
                DataTable dtAddress = (DataTable)Session["dtCustomer"];

                DataRow[] foundRows2 = dtAddress.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

                if (foundRows2.Length > 0)
                {
                    lblAdress.Text = "Address";
                    HdnSaleExectiveID.Value = _dc.chkNull_0(foundRows2[0]["ASSIGNED_USER_ID"].ToString());
                    if (rdbCustomerAddress.SelectedValue == "0")
                    {
                        lblCustomerAddress.Text = foundRows2[0]["ShowRoomAddress"].ToString();
                        if (lblCustomerAddress.Text == "")
                        {
                            lblCustomerAddress.Text = "N/A";
                        }

                    }
                    else if (rdbCustomerAddress.SelectedValue == "1")
                    {
                        lblCustomerAddress.Text = foundRows2[0]["WareHouseAddress"].ToString();
                        if (lblCustomerAddress.Text == "")
                        {
                            lblCustomerAddress.Text = "N/A";
                        }
                    }
                }
            }


            ExistenOrderDetail(Convert.ToInt64(Session["SaleOrderId"]));
            txtRemarks.Text = foundRows[0]["remarks"].ToString();

            EnableDisableController(false);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            this.CreateTable();
            this.LoadDistributors();
            this.LoadCustomers();
            if (Session["SaleOrderId"] != null)
            {
                this.loadOrder(Convert.ToInt64(Session["SaleOrderId"]));
            }
            else
            {
                this.CustomerLedger();
            }
            this.LoadSKUDetail();
            if (Request.QueryString["Type"].ToString() == "2")
            {
                ddlSKus.Visible = false;
                txtSKU.Visible = true;                
                ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
            }
            else
            {
                ScriptManager.GetCurrent(Page).SetFocus(ddlSKus);
            }
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
        LoadCustomers();
        CustomerLedger();
        rdbCustomerAddress_OnSelectedIndexChanged(null, null);
        LoadSKUDetail();
        ddlCustomer.Focus();
    }

    private void LoadCustomers()
    {
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
            }
        }
    }
    private void CustomerLedger()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            decimal Opening = 0;


            //DataTable dtOpening = _customerCtrl.GetCustomerOpening(Constants.LongNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue), 1);

            //if (dtOpening.Rows.Count > 0)
            //{
            //    Opening = -1 * decimal.Parse(_dc.chkNull_0(dtOpening.Rows[0]["BALANCE"].ToString()));
            //    //  lblOpeningType.Text = dtOpening.Rows[0]["openingType"].ToString();
            //}

            DataTable dt = _customerCtrl.SelectCustomerCreditBalance(long.Parse(ddlCustomer.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), Constants.Advance_PaymentOrder_id);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Opening = Opening + decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));
                    //  Opening = decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));

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
            //if (decimal.Parse(_dc.chkNull_0(lblLedgerBalance.Text.ToString())) > 0)
            //{
            //    lblOpeningType.Text = "CR";
            //}
            //else if (decimal.Parse(_dc.chkNull_0(lblLedgerBalance.Text.ToString())) < 0)
            //{
            //    lblOpeningType.Text = "DR";
            //}
            //else
            //{
            //    lblOpeningType.Text = "";
            //}
        }
    }
    private void CustomerAddress()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            DataTable dt = _customerCtrl.GetCustomerDealer(Constants.IntNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    lblAdress.Text = "Name & Address";
                    rdbSubDealers.DataTextField = "dealerDetail";
                    rdbSubDealers.DataValueField = "CUSTOMER_DEALER_ID";
                    rdbSubDealers.DataSource = dt;
                    rdbSubDealers.DataBind();
                    // lblCustomerAddress.Text = dt.Rows[0][3].ToString() + " - " + dt.Rows[0][4].ToString();
                    // if (lblCustomerAddress.Text == "")
                    // {
                    //    lblCustomerAddress.Text = "N/A";
                    //}
                    rdbSubDealers.SelectedIndex = 0;
                    lblCustomerAddress.Text = "";
                    fsSubDealer.Visible = true;
                }
                else
                {
                    fsSubDealer.Visible = false;
                    lblCustomerAddress.Text = "N/A";
                }
            }
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomerLedger();
        rdbCustomerAddress_OnSelectedIndexChanged(null, null);
        LoadSKUDetail();

    }
    protected void rdbCustomerAddress_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        if (rdbCustomerAddress.SelectedValue == "2")
        {
            CustomerAddress();
        }
        else if (rdbCustomerAddress.SelectedValue == "0")
        {
            fsSubDealer.Visible = false;
            DataTable dt = (DataTable)Session["dtCustomer"];
            DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

            if (foundRows.Length > 0)
            {
                lblAdress.Text = "Address";

                lblCustomerAddress.Text = foundRows[0]["ShowRoomAddress"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
                HdnSaleExectiveID.Value = dc.chkNull_0(foundRows[0]["ASSIGNED_USER_ID"].ToString());
            }
        }
        else
        {

            fsSubDealer.Visible = false;

            DataTable dt = (DataTable)Session["dtCustomer"];
            DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

            if (foundRows.Length > 0)
            {
                lblAdress.Text = "Address";
                lblCustomerAddress.Text = foundRows[0]["WareHouseAddress"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
            }
        }

    }

    #region Detail Panel

    DataTable orderSku = new DataTable();
    private static int RowNo;

    private void LoadSKUDetail()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            if (ddlCustomer.SelectedItem.Text.Contains("|"))
            {
                string[] Customer = ddlCustomer.SelectedItem.Text.Split('|');
                if (Customer[1].ToString().Trim() == "Default")
                {
                    SKUPriceDetailController PController = new SKUPriceDetailController();
                    DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "BarCodeSkuDetail", true);
                    }
                    Session.Add("Dtsku_Price", Dtsku_Price);
                }
                else
                {
                    SKUPriceDetailController PController = new SKUPriceDetailController();
                    DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "BarCodeSkuDetail", true);
                    }
                    Session.Add("Dtsku_Price", Dtsku_Price);
                }
            }
        }
    }
    private void CreateTable()
    {
        orderSku = new DataTable();
        orderSku.Columns.Add("orderDetailId", typeof(long));
        orderSku.Columns.Add("skuId", typeof(int));
        orderSku.Columns.Add("skuName", typeof(string));
        orderSku.Columns.Add("BAR_CODE", typeof(string));
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
        if (Request.QueryString["Type"].ToString() == "1")
        {
            foundRows = orderSku.Select("skuId  = '" + ddlSKus.SelectedValue + "'");
        }
        else
        {
            foundRows = orderSku.Select("BAR_CODE  = '" + txtSKU.Text.Trim().Substring(0, 12) + "'");
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
            DataRow[] foundRows;
            DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
            if (Request.QueryString["Type"].ToString() == "1")
            {
                foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKus.SelectedValue + "'");
            }
            else
            {
                foundRows = Dtsku_Price.Select("BAR_CODE  = '" + txtSKU.Text.Trim().Substring(0,12) + "'");
            }
            if (decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) > 0)
            {
                if (foundRows.Length > 0)
                {
                    orderSku = (DataTable)Session["orderSku"];
                    var qty = 0;
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                    }
                    else
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) - int.Parse(_dc.chkNull_0(txtQuantity.Text));
                    }
                    if (btnSave.Text == "Add")
                    {
                        if (CheckDublicateSKU())
                        {
                            DataRow dr = orderSku.NewRow();
                            if (CheckStock(qty, int.Parse(foundRows[0]["SKU_ID"].ToString()), foundRows[0]["SKU_NAME"].ToString(), int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString()))))
                            {
                            }
                            dr["skuId"] = foundRows[0]["SKU_ID"];
                            dr["skuName"] = foundRows[0]["SKU_NAME"]; //AS SKUnMAE
                            dr["price"] = foundRows[0]["TRADE_PRICE"];
                            dr["BAR_CODE"] = foundRows[0]["BAR_CODE"];
                            dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                            if (Request.QueryString["Type"].ToString() == "1")
                            {
                                dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                            else
                            {
                                dr["Unit"] = Convert.ToInt32(_dc.chkNull_0(txtQuantity.Text)) * -1;
                                dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) - int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) - int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                            orderSku.Rows.Add(dr);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKus.SelectedItem.Text + " Already Exists ');", true);
                            return;
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        DataRow dr = orderSku.Rows[RowNo];
                        if (CheckStock(qty, int.Parse(foundRows[0]["SKU_ID"].ToString()), foundRows[0]["SKU_NAME"].ToString(), int.Parse(_dc.chkNull_0(dr["PrevRcdQty"].ToString()))))
                        {
                        }
                        dr["skuId"] = foundRows[0]["SKU_ID"];
                        dr["skuName"] = foundRows[0]["SKU_NAME"];
                        dr["price"] = foundRows[0]["TRADE_PRICE"];
                        dr["BAR_CODE"] = foundRows[0]["BAR_CODE"];
                        dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                        if (Request.QueryString["Type"].ToString() == "1")
                        {
                            dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                        }
                        else
                        {
                            dr["CurrentRcdQtyUnit"] = Convert.ToInt32(_dc.chkNull_0(txtQuantity.Text)) * -1;
                            dr["qty"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) - int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(foundRows[0]["Units_In_Case"].ToString()))) - int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                        }
                    }
                    Session.Add("orderSku", orderSku);
                    ClearDetail();
                    LoadGird();
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        ScriptManager.GetCurrent(Page).SetFocus(ddlSKus);
                    }
                    else
                    {
                        ScriptManager.GetCurrent(Page).SetFocus(txtSKU);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item Select');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Item Price!');", true);
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

        ddlSKus.SelectedValue = grdOrder.Rows[RowNo].Cells[0].Text;
        txtCtn.Text = grdOrder.Rows[RowNo].Cells[4].Text;
        txtQuantity.Text = grdOrder.Rows[RowNo].Cells[5].Text;

        ddlSKus.Enabled = false;
        txtCtn.Focus();
        btnSave.Text = "Update";

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
    }
    private void ClearDetail()
    {

        txtQuantity.Text = "";
        txtCtn.Text = "1";
        txtSKU.Text = string.Empty;
        ddlSKus.Enabled = true;
        btnSave.Text = "Add";

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
                foreach (DataRow dr in orderSku.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["amount"].ToString());
                    mTotalQty += int.Parse(dr["qty"].ToString());
                }
                int subDealerId = 0;

                if (rdbCustomerAddress.SelectedValue == "2")
                {
                    subDealerId = int.Parse(rdbSubDealers.SelectedValue);
                }

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
                    long sId = oeCtrl.AddOrder(int.Parse(ddlDistributor.SelectedValue), rdbCustomerAddress.SelectedValue, subDealerId, long.Parse(ddlCustomer.SelectedValue)
                    , 214, mTotalAmount, mTotalQty, Constants.Order_Pending_Id, orderSku, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, SaleExectiveID,Constants.IntNullValue);
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
                        subDealerId, int.Parse(ddlDistributor.SelectedValue), 214, rdbCustomerAddress.SelectedValue,
                        long.Parse(ddlCustomer.SelectedValue), mTotalAmount, mTotalQty, orderSku, int.Parse(Session["UserId"].ToString()), txtRemarks.Text, SaleExectiveID,Constants.IntNullValue);

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
            ds = RptSaleCtl.SelectDocument(sId,1);
           
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
    private bool CheckStock(int Qty, int SkuId, string SkuName, int PrevRcdQty)
    {

        if (Qty > 0)
        {
            PurchaseController mController = new PurchaseController();
            DataTable dtstock = mController.GetSKUClosingStockWeb(int.Parse(ddlDistributor.SelectedValue), SkuId);
            if (dtstock.Rows.Count > 0)
            {
                //if (ViewState["PrevRcdQty"] != null)
                //{
                //    if (int.Parse(dtstock.Rows[0][0].ToString()) + int.Parse(ViewState["PrevRcdQty"].ToString()) < Qty)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                //        return false;
                //    }
                //}
                //else
                //{

                if (Math.Round(decimal.Parse(dtstock.Rows[0][0].ToString())) + PrevRcdQty < Qty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    return false;
                }
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " No Stock Found');", true);
                return false;
            }
        }
        return true;
    }   
}