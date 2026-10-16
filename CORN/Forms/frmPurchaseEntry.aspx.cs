using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseEntry : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly DataControl _dc = new DataControl();
    private static int RowNo;
    private static decimal PrivouseQty, FreePrivousQty;
    DataTable PurchaseSKUS;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
           
            CreatTable();
            GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            if (DrpDocumentType.SelectedIndex == 0)
            {
                lblfromLocation.Text = "Transfer From";
                lbltoLocation.Text = "";
                drpDistributor.Enabled = true;
                drpPrincipal.Enabled = true;
                drpPrincipal.Visible = false;
                LoadToDistributor();
                DrpTransferFor.Visible = true;
                lblInvoice.Text = "Driver Name";
                Label4.Visible = true;
                Label4.Text = "Transfer To";
                GetDocumentNo();
            }
            LoadSkuDetail();
        }
    }
    
    private void CreatTable()
    {
        PurchaseSKUS = new DataTable();
        PurchaseSKUS.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKUS.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKUS.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKUS.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKUS.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKUS.Columns.Add("Quantity", typeof(decimal));
        PurchaseSKUS.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKUS.Columns.Add("Ctn", typeof(int));
        PurchaseSKUS.Columns.Add("Unit", typeof(int));
        PurchaseSKUS.Columns.Add("RACK_NO", typeof(string));
        PurchaseSKUS.Columns.Add("BAR_CODE", typeof(string));
        PurchaseSKUS.Columns.Add("PURCHASE_PRICE", typeof(decimal));
        Session.Add("PurchaseSKUS", PurchaseSKUS);      
    }
    
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblInvoice.Text = "INV/DC  No";
        //if (DrpDocumentType.SelectedIndex == 0)
        //{
        //    lbltoLocation.Text = "Supplier";
        //    lblfromLocation.Text = "Purchase For";
        //    drpDistributor.Enabled = true;
        //    drpPrincipal.Enabled = true;
        //    DrpTransferFor.Visible = false;
        //    Label4.Visible = false;
        //    GetDocumentNo();
        //}
        if (DrpDocumentType.SelectedIndex == 0)
        {
            lblfromLocation.Text = "Transfer From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            LoadToDistributor();
            DrpTransferFor.Visible = true;
            lblInvoice.Text = "Driver Name";
            Label4.Visible = true;
            Label4.Text = "Transfer To";
            GetDocumentNo();
            lbltoLocation.Text = "";
            drpPrincipal.Visible = false;
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {

            lblInvoice.Text = "INV/DC  No";
            lbltoLocation.Text = "Supplier";
            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;

            drpPrincipal.Visible = true;
            drpPrincipal.Enabled = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;
            GetDocumentNo();
        }
        //else if (DrpDocumentType.SelectedIndex == 2)
        //{
        //    lblfromLocation.Text = "Transfer to";
        //    drpDistributor.Enabled = true;
        //    drpPrincipal.Enabled = true;
        //    lbltoLocation.Text = "Supplier";
        //    drpPrincipal.Visible = true;
        //    LoadToDistributor();
        //    DrpTransferFor.Visible = true;
        //    Label4.Visible = true;
        //    Label4.Text = "Transfer From";
        //    GetDocumentNo();
        //}
        else
        {
            lblInvoice.Text = "INV/DC  No";

            lbltoLocation.Text = "Supplier";
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            drpPrincipal.Visible = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;
            GetDocumentNo();
        }
        LoadPrincipal();
        LoadSkuDetail();
    }
    
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();   
        
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()),Constants.IntNullValue,Constants.LongNullValue,int.Parse(Session["UserId"].ToString()),0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));          
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }
    
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKUS", PurchaseSKUS);
            LoadGird();
            ClearAll();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            DrpDocumentType.Enabled = false;
            LoadDocumentDetail();
            //LoadSkuDetail();
        }
    }
    
    private void LoadPrincipal()
    {
        drpPrincipal.Items.Clear();
        if (DrpDocumentType.SelectedValue == "6" || DrpDocumentType.SelectedValue == "10")
        {
            DataTable m_dt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, false);
        }else
        {
            DataTable m_dt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, false);
        }
    }
    
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.Session["Dtsku_Price"] != null)
        {
            this.Session.Remove("Dtsku_Price");
        }
        LoadSkuDetail();
    }
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2,true);
    }
    
    private void LoadToDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpTransferFor, dt, 0, 2, true);
    }
    
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            if (DrpDocumentType.SelectedIndex == 0)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 1)
            {
                drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
               
                txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
           
            else
            {
                //DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                //drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                //txtDocumentNo.Text = dt.Rows[0][2].ToString();
                //txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                if (dt.Rows[0]["PRINCIPAL_ID"].ToString() == "")
                { }
                else
                {
                    drpPrincipal.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                }
                    drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            PurchaseSKUS = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKUS", PurchaseSKUS);
            LoadGird();
        }
    }
        
    private void LoadGird()
    {
        decimal TotalValue = 0;
        PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
        grdOrder.EditIndex = -1;
        grdOrder.DataSource = PurchaseSKUS;
        grdOrder.DataBind();
        foreach (DataRow dr in PurchaseSKUS.Rows)
        {
            TotalValue += decimal.Parse(dr["Quantity"].ToString());

        }
        txtTotalQuantity.Text = TotalValue.ToString(); 
    }

    protected void grdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        hfSKUID.Value = grdOrder.Rows[RowNo].Cells[0].Text;
        btnSave.Text = "Update";

        txtSKU.Text = grdOrder.Rows[RowNo].Cells[1].Text;
        txtSKU.Enabled = false;
        txtCtn.Text = grdOrder.Rows[RowNo].Cells[3].Text;
        txtQuantity.Text = grdOrder.Rows[RowNo].Cells[4].Text;
        PrivouseQty = Math.Round(decimal.Parse(grdOrder.Rows[RowNo].Cells[5].Text),0);
        txtCtn.Focus();
        cbScan.Checked = false;
        cbScan.Enabled = false;
        for (int i = 0; i < grdOrder.Rows.Count; i++)
        {
            grdOrder.Rows[i].Cells[7].Enabled = false;
            grdOrder.Rows[i].Cells[8].Enabled = false;

        }
    }
    
    protected void grdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
        if (PurchaseSKUS.Rows.Count > 0)
        {
            PurchaseSKUS.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKUS", PurchaseSKUS);
            LoadGird();
        }
    }
    
    private void LoadSkuDetail()
    {
    //    ddlSKuCde.Items.Clear();
    //    if (DrpDocumentType.SelectedIndex == 0)
    //    {
         
    //        DataTable dtskuPrice = _pController.SelectDataPrice_stockregister(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 5, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
    //        if (dtskuPrice == null)
    //        {
    //            return;
    //        }           
    //        clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 3, true);
    //        Session.Add("Dtsku_Price", dtskuPrice);
    //    }
    //    else
    //    {
    //        if (drpPrincipal.Items.Count > 0)
    //        {
    //            DataTable dtskuPrice = _pController.SelectDataPrice_stockregister(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 5, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
    //            if (dtskuPrice == null) return;
               
    //            clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 3, true);
    //            Session.Add("Dtsku_Price", dtskuPrice);
    //        }
    //    }
        
        
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = new DataTable();

        SKUPriceDetailController PController = new SKUPriceDetailController();
        if (cbScan.Checked)
        {
            Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(drpDistributor.SelectedValue), txtSKUScan.Text.Trim().Substring(0, 12));
        }
        else
        {
            Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfSKUID.Value));
        }
        if (Dtsku_Price.Rows.Count > 0)
        {
            if (decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) > 0)
            {
                var qty = 0;
                if (!cbScan.Checked)
                {
                    qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                }
                else
                {
                    qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                }

                PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
                int qt = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));

                decimal currentStock = CheckStockStatus(int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["SKU_ID"].ToString())), qt);

                if (btnSave.Text == "Add")
                {
                    if (CheckDublicateSku())
                    {
                        if (currentStock == -1)
                        {
                            DataRow dr = PurchaseSKUS.NewRow();
                            dr["SKU_ID"] = Dtsku_Price.Rows[0]["SKU_ID"];
                            dr["SKU_Code"] = Dtsku_Price.Rows[0]["SKU_CODE"];
                            dr["SKU_Name"] = Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"];
                            dr["PRICE"] = Dtsku_Price.Rows[0]["TRADE_PRICE"];
                            dr["PURCHASE_PRICE"] = Dtsku_Price.Rows[0]["PURCHASE_PRICE"];
                            dr["BAR_CODE"] = Dtsku_Price.Rows[0]["BAR_CODE"];
                            dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                            if (!cbScan.Checked)
                            {
                                dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["QUANTITY"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                            else
                            {
                                dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["QUANTITY"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                                dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                            }
                            PurchaseSKUS.Rows.Add(dr);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"] + " Current closing Stock is " + currentStock + "');", true);

                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"] + " Already Exists ');", true);

                        return;
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    if (currentStock == -1)
                    {
                        DataRow dr = PurchaseSKUS.Rows[RowNo];
                        dr["SKU_ID"] = Dtsku_Price.Rows[0]["SKU_ID"];
                        dr["SKU_Code"] = Dtsku_Price.Rows[0]["SKU_CODE"];
                        dr["SKU_Name"] = Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"];
                        dr["PRICE"] = Dtsku_Price.Rows[0]["TRADE_PRICE"];
                        dr["PURCHASE_PRICE"] = Dtsku_Price.Rows[0]["PURCHASE_PRICE"];
                        dr["BAR_CODE"] = Dtsku_Price.Rows[0]["BAR_CODE"];
                        dr["Ctn"] = int.Parse(_dc.chkNull_0(txtCtn.Text));
                        if (!cbScan.Checked)
                        {
                            dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["QUANTITY"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                        }
                        else
                        {
                            dr["Unit"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["QUANTITY"] = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString())) * ((int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString()))) + int.Parse(_dc.chkNull_0(txtQuantity.Text)));
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + Dtsku_Price.Rows[0]["SKU_CODE"] + "-" + Dtsku_Price.Rows[0]["SKU_NAME"] + "-" + Dtsku_Price.Rows[0]["BAR_CODE"] + "-" + Dtsku_Price.Rows[0]["TRADE_PRICE"] + "-" + Dtsku_Price.Rows[0]["Units_In_Case"] + "Current closing Stock is " + currentStock + "');", true);

                        return;
                    }
                }


                Session.Add("PurchaseSKUS", PurchaseSKUS);
                ClearAll();
                LoadGird();
                DisAbaleOption(true);
                var scriptManager = ScriptManager.GetCurrent(Page);
                if (scriptManager != null) scriptManager.SetFocus(txtSKU);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item Select');", true);
        }
    }
    
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
        if (DrpDocumentType.SelectedIndex == 0)
        {
            if (drpDistributor.SelectedValue.ToString() == DrpTransferFor.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' Transfer to Location must be different ');", true);
                return;
            }
        }
        PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
        if (PurchaseSKUS.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    //ShowReport();
                    PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
                    PurchaseSKUS.Rows.Clear();
                    Session.Add("PurchaseSKUS", PurchaseSKUS);
                    LoadGird();
                    GetDocumentNo();
                    drpDistributor.Enabled = true;
                    drpPrincipal.Enabled = true;
                    DrpDocumentType.Enabled = true;
                    ClearAll();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtDocumentNo.Text = "";
                    hfClosingDate.Value = "";
                    DisAbaleOption(false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        CreatTable();
        LoadGird();
        ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = ""; 
    }

    private bool CheckDublicateSku()
    {

        PurchaseSKUS = (DataTable)Session["PurchaseSKUS"];
        DataRow[] foundRows;

        if (!cbScan.Checked)
        {
            foundRows = PurchaseSKUS.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        }
        else
        {
            foundRows = PurchaseSKUS.Select("BAR_CODE  = '" + txtSKUScan.Text.Trim().Substring(0, 12) + "'");
        }
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;

    }
    
    private bool CalculatePurchase(DateTime mWorkDate)
    {
        hfClosingDate.Value = mWorkDate.ToString();
        var mController = new PurchaseController();
        var dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];
        decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
        bool mResult;
        //if (DrpDocumentType.SelectedIndex == 0)
        //{
        //    if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        //    {
        //        mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //        , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue.ToString()), mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
               
        //    }
        //    else
        //    {
        //         mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
        //        , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue), mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
        //        return mResult;
        //    }
        //    //dtPurchaseDetail
        //    return mResult;
        //}
         if (DrpDocumentType.SelectedIndex == 0)//Transfer out
        {
            bool mResult2 = true;
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                
                long DocumentNO = mController.InsertPurchaseDocument2(int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(DrpTransferFor.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
                if (DocumentNO != Constants.LongNullValue)
                {
                    PrintReport(DocumentNO);
                    return mResult2;
                }
            }
            else
            {
                 
                  mResult2 = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(DrpTransferFor.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
                if (mResult2)
                {

                    PrintReport(long.Parse(drpDocumentNo.SelectedValue));
                    
                }
               
            }
            return mResult2;
            
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                 mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpPrincipal.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
               
            }
            else
            {
                 mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpPrincipal.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
               
            }
            return mResult;
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                 mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(DrpTransferFor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
                
            }
            else
            {
                mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(DrpTransferFor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
                
            }
            return mResult;
        }
        else
        {
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                 mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpDistributor.SelectedValue),  int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
               
            }
            else
            {
                 mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue)
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
               
            }
            return mResult;
        }
    }

    private void ShowReport()
    {
        //try
        //{
        //    LatestDataSet ds = new LatestDataSet();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("BarCode", typeof(string));
        //    dt.Columns.Add("Description", typeof(string));
        //    dt.Columns.Add("QTY", typeof(decimal));
        //    dt.Columns.Add("From", typeof(string));
        //    dt.Columns.Add("To", typeof(string));
        //    dt.Columns.Add("DriverName", typeof(string));
        //    dt.Columns.Add("TransferDate", typeof(DateTime));
        //    int i = 0;
        //    DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];
        //    foreach (DataRow dr in dtPurchaseDetail.Rows)
        //    {
        //        dt.Rows.Add();
        //        dt.Rows[i]["BarCode"] = dr["SKU_Code"];
        //        dt.Rows[i]["Description"] = dr["SKU_Name"];
        //        dt.Rows[i]["QTY"] = Convert.ToDecimal(dr["Quantity"]);
        //        dt.Rows[i]["From"] = DrpTransferFor.SelectedItem.Text;
        //        dt.Rows[i]["To"] = drpDistributor.SelectedItem.Text;
        //        dt.Rows[i]["DriverName"] = txtDocumentNo.Text;
        //        dt.Rows[i]["TransferDate"] = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        //        i += 1;
        //    }
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ds.Tables["StockRegisterTransferOut"].ImportRow(dr);
        //    }
           
        //    var crpReport = new CRPStockRegisterTransferOut();
        //    crpReport.SetDataSource(ds);
        //    crpReport.Refresh();


          


        //    Session.Add("CrpReport", crpReport);
        //    Session.Add("ReportType", 0);
        //    const string url = "'Default.aspx'";
        //    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //    Type cstype = GetType();
        //    ClientScriptManager cs = Page.ClientScript;
        //    cs.RegisterStartupScript(cstype, "OpenWindow1", script);
        //}
        //catch(Exception ex)
        //{
        //    ex.Message.ToString();
        //}
    }

    private decimal CheckStockStatus(int skuId, int Quantity)
    {

        var mController = new PhaysicalStockController();
        DataTable dt = mController.SelectSKUClosingStock2(int.Parse(drpDistributor.SelectedValue.ToString()), skuId, "N/A", DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        if (dt.Rows.Count > 0)
        {
            var x = decimal.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty;
            if (decimal.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty >= Quantity)
            {
                return -1;
            }
            return decimal.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty;
        }
        else
        {
            return 0;
        }
    }
    
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            DrpDocumentType.Enabled = true;
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }
    
    private void ClearAll()
    {
        txtQuantity.Text = "";
        txtCtn.Text = "1";
        txtSKU.Text = string.Empty;
        btnSave.Text = "Add";
        cbScan.Enabled = true;
        txtSKU.Enabled = true;
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }

    private void PrintReport(long p_Document_ID)
    {
        var mController = new DocumentPrintController();
        var rptInventoryCtl = new RptInventoryController();
        var crpReport = new CrpTransferDocument();
        var dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        var ds = rptInventoryCtl.SelectTransferDocument2(p_Document_ID, int.Parse(drpDistributor.SelectedValue),
                                                        int.Parse(drpPrincipal.SelectedValue), DateTime.Parse(hfClosingDate.Value),
                                                        DateTime.Parse(hfClosingDate.Value),int.Parse(DrpDocumentType.SelectedValue));
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", "Transfer Out Document");
        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
      

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
         const string url = "'Default.aspx'";
         const  string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
          Type cstype = this.GetType();
         ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        // Response.Write(script);
       // Response.Write("<script>window.open(" + url + ",'_blank');</script>");
       
       
    }

}