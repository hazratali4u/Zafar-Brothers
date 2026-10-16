using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using System.Data;
using CORNCommon.Classes;
using System.Text;
using System.Collections;
public partial class Forms_frmDelieveryOrderAdd : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
     OrderEntryController oeCtrl = new OrderEntryController();
    private static int _mCustomerTypeId;
    private static int _mCustomerVolClassId;
    DataTable _dtFreeSku;
    protected void Calculate()
    {
        try
        {

            decimal ObjGrossSales = 0;
            decimal ObjStandardDiscount = 0;
            decimal ObjDiscountPrice = 0;
            decimal ProductStdDist = 0;
            decimal TotalAmount = 0;
            decimal TotalAmount2 = 0;

            OrderEntryController or = new OrderEntryController();
            SKUGroupController GroupCtl = new SKUGroupController();

            DataTable dt = new DataTable();

            dt = (DataTable)Session["orderSku"];
            foreach(GridViewRow gvr in grdOrder.Rows)
            {
                TextBox txtCurrentRecQty = (TextBox)gvr.Cells[6].FindControl("txtCurrentRecQty");
                TextBox txtCurrentRecQtyUnit = (TextBox)gvr.Cells[7].FindControl("txtCurrentRecQtyUnit");
                TotalAmount2 += decimal.Parse(_dc.chkNull_0(gvr.Cells[8].Text)) * ((int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * int.Parse(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)));
                gvr.Cells[13].Text = "0";
                gvr.Cells[14].Text = "0";
            }

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["standardDiscount"] = 0;
            //    dr["standardDiscountPer"] = 0;
            //    dr["extraDiscount"] = 0;

            //    dr["amount"] = decimal.Parse(_dc.chkNull_0(dr["price"].ToString())) * ((int.Parse(dr["CurrentRcdQty"].ToString()) * int.Parse(dr["Units_in_case"].ToString())) + int.Parse(dr["CurrentRcdQtyUnit"].ToString()));

            //    dr["discountedPrice"] = 0;

            //    TotalAmount2 += decimal.Parse(dr["amount"].ToString());
            //}
            decimal gstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)));
            decimal addGstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfAddGst.Value)));

            decimal Removediscount = CalculateDiscount(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)));
            decimal creditAmount = 0;
            decimal Freight = Convert.ToDecimal(_dc.chkNull_0(txtFreight.Text));

            if (Removediscount < TotalAmount2)
            {
                creditAmount = TotalAmount2 - Removediscount + gstAmount + addGstAmount+Freight;
                txtTotalAmount.Text = string.Format("{0:0.00}", creditAmount);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Discount should be less than Total Amount');", true);
            }
           
            PromotionCollections_Controller Arpc = (PromotionCollections_Controller)Session["Arpc"];
           

            if (dt.Rows.Count > 0)
            {
                if (Arpc != null)
                {
                    ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, _mCustomerVolClassId, 135, dt, false);

                    for (int i = 0; i < ARlistOffer.Count; i++)
                    {
                        PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];

                        if (pofferCol.SKU_ID > 0)
                        {
                            #region slab on SKU
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (int.Parse(dr["skuId"].ToString()) == pofferCol.SKU_ID)
                                {

                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + pofferCol.Offer_Value;

                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["amount"].ToString()));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["standardDiscountPer"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        }
                                    }
                                    #region free sku
                                    /*
                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(_dtFreeSku, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = _dtFreeSku.NewRow();
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Name"] = foundRows[0]["SkuPriceDetail"];
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["amount"].ToString());
                                            
                                                _dtFreeSku.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = _dtFreeSku.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["amount"].ToString());

                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Name"] = foundRows[0]["SkuPriceDetail"];
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                   // dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(dr1["Quantity"].ToString());
                                               
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["amount"].ToString());
                                            }

                                        }
                                    }
                                    Session.Add("_dtFreeSku", _dtFreeSku);
                                    LoadFreeGrid();
                                   */
                                    #endregion
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region slab on SKU Group

                            foreach (DataRow dr in dt.Rows)
                            {
                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["skuId"].ToString())))
                                {


                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["amount"].ToString()) - decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["standardDiscountPer"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        }
                                    }
                                }
                                
                                 
                                #region Free SKU
                                /*
                                if (pofferCol.Free_SKU_ID > 0)
                                {
                                    DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                                    DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                    int RowId = FreeSKUExist(_dtFreeSku, pofferCol.Free_SKU_ID);
                                    if (foundRows.Length > 0)
                                    {
                                        if (RowId < 0)
                                        {
                                            DataRow dr1 = _dtFreeSku.NewRow();
                                            dr1["P_SKU_ID"] = dr["SKU_ID"];
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {

                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["amount"].ToString());
                                                dr1["TST_AMOUNT"] = 0;

                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["amount"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            _dtFreeSku.Rows.Add(dr1);
                                        }
                                        else
                                        {

                                            DataRow dr1 = _dtFreeSku.Rows[RowId];
                                            ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["amount"].ToString());
                                            ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                            dr1["P_SKU_ID"] = dr["SKU_ID"];
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {

                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["amount"].ToString());
                                                dr1["TST_AMOUNT"] = 0;

                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["amount"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                        }
                                        Session.Add("_dtFreeSku", _dtFreeSku);
                                        LoadFreeGrid();

                                    }
                                }
                                */
                                #endregion
                                 
                                


                            }
                            #endregion
                        }

                    }
                }
                #region Recalculate Products

                foreach (DataRow dr in dt.Rows)
                {
                    ProductStdDist = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["amount"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                    if (ProductStdDist > 0)
                    {
                        dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["amount"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                        dr["discountedPrice"] = (decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) / (int.Parse(_dc.chkNull_0(dr["CurrentRcdQty"].ToString())) * int.Parse(_dc.chkNull_0(dr["Units_in_case"].ToString()))) + int.Parse(_dc.chkNull_0(dr["CurrentRcdQtyUnit"].ToString()))); //- decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                        dr["discountedPrice"] = (decimal.Parse(_dc.chkNull_0(dr["price"].ToString())) - decimal.Parse(dr["discountedPrice"].ToString()));
                      }
                    ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["amount"].ToString()));
                    ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString()));
                   
                  

                    #region GST Calculation
                    
                    decimal TempAmount = decimal.Parse(_dc.chkNull_0(dr["amount"].ToString()));
                    decimal TempDiscount = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString()));
                    dr["netAmount"] = (TempAmount  - TempDiscount);
                   
                    #endregion
                    TotalAmount += (TempAmount - TempDiscount);
                }
              
                //dt = (DataTable)Session["orderSku"];
                 gstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)));
                 addGstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfAddGst.Value)));

                 Removediscount = CalculateDiscount(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)));
                 decimal Freight2 = Convert.ToDecimal(_dc.chkNull_0(txtFreight.Text));

                if (Removediscount < TotalAmount2)
                {
                    creditAmount = TotalAmount2 - Removediscount + gstAmount + addGstAmount+Freight2;
                    txtTotalAmount.Text = string.Format("{0:0.00}", creditAmount);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Discount should be less than Total Amount');", true);
                }
                Session.Add("orderSku",dt);
             
                #endregion
            }
            hfStandardDiscount.Value =Convert.ToString(ObjStandardDiscount);
          
        }

        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Type"].ToString() == "1")
            {
                rblType.SelectedValue = "1";
                txtBarCode.Visible = false;
            }
            rblType.Enabled = false;
            LoadDistributors();
            LoadAssingned();
            LoadCustomers();
            if (Session["invoiceId"] != null)
            {
                loadInvoice();
                Session.Add("orderSku", orderSku);
            }
            if (Session["orderId"] != null)
            {
                loadOrder();
                Session.Add("orderSku", orderSku);
            }
            LoadPromotion();
            LoadChequeInformation();
            EnableDisableController(false);
            LoadGird();
            LoadTurnOverRatioDueAmount();
        }
    }
    #region Load

    private void loadInvoice()
    {
        var id = Session["invoiceId"].ToString().Split('-');
        lblDONo.Text = "DO No: " + id[1];
        lblOrderNo.Text = "Order No: " + id[0];
        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(id[1]), 2);
        if (dt.Rows.Count > 0)
        {
            hfOrderQty.Value = dt.Rows[0]["orderQty"].ToString();
            ddlDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            LoadAssingned();
            try
            {
                drpAssignedDistributor.SelectedValue = dt.Rows[0]["ASSIGNED_DISTRIBUTOR_ID"].ToString();
            }
            catch (Exception ex)
            { }
            ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            txtTotalAmount.Text = string.Format("{0:0.00}", dt.Rows[0]["TOTAL_AMOUNT"]);
            txtNetAmount.Text = string.Format("{0:0.00}", dt.Rows[0]["TOTAL_NET_AMOUNT"]);
            lblDate.Text = "SO Date: " + Convert.ToDateTime(dt.Rows[0]["SODate"].ToString()).ToString("dd-MM-yyyy");
            _mCustomerTypeId = int.Parse(_dc.chkNull_0(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString()));
            _mCustomerVolClassId = int.Parse(_dc.chkNull_0(dt.Rows[0]["VOLUME_CLASS_ID"].ToString()));
            hfIsGstReg.Value = _dc.chkNull_0(dt.Rows[0]["isGstRegister"].ToString());
            hfSaleExecutiveID.Value = _dc.chkNull_0(dt.Rows[0]["SALE_EXECUTIVE_ID"].ToString());
            try
            {
                rbtnDeliveryMethd.SelectedValue = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["Delivery_Method_ID"].ToString()));
            }
            catch (Exception ex)
            {
            }
            /// <summary>
            /// Customer Deal 
            /// </summary>

            DataTable dtCustomer = _customerCtrl.GetCustomerByID_ZO(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue), 1);
            if (dtCustomer != null)
            {
                if (dtCustomer.Rows[0]["businessDealType"].ToString() != "")
                {
                    string BDealtype = dtCustomer.Rows[0]["businessDealType"].ToString();

                    #region on Cash
                    if (BDealtype == "0")//On cAsh
                    {
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() != "0")
                        {
                            lblSaleLimit.Text = dtCustomer.Rows[0]["SALE_LIMIT"].ToString();
                        }
                        lblBusinessDeal.Text = "On Cash";

                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "0")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Advance" + "</b>";
                        }
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "1")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> WithIn Month" + "</b>";
                        }
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "2")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Supply Basis" + "</b>";
                        }
                        else
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Sale Basis" + "</b>";
                        }

                    }
                    #endregion
                    #region on Credit Period
                    else if (BDealtype == "1")
                    {

                        lblSaleLimit.Text = dtCustomer.Rows[0]["SALE_LIMIT"].ToString();
                        lblBusinessDeal.Text = "On Credit Period";

                        StringBuilder stb = new StringBuilder();

                        stb.Append("<p><b>" + "Credit Percent(%): " + "</b></p>");
                        stb.Append("<p><b>" + "No. Of Days: " + "</b></p>");
                        stb.Append("<p><b>" + "Payment Schedule:" + "</b></p>");
                        stb.Append("<p><b>" + "Start Date: " + "</b></p>");

                        ltrBusinessDealText.Text = stb.ToString();

                        StringBuilder stb2 = new StringBuilder();
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["CREDIT_PERCENTAGE"].ToString() + "</b></p>");
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["NoOfDays"].ToString() + "</b></p>");
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["PAYMENT_SCHEDULE"].ToString() + "</b></p>");

                        stb2.Append("<p><b>" + string.Format("{0:dd-MMM-yyyy}", dtCustomer.Rows[0]["DATE_PERIOD"]) + "</b></p>");

                        ltrBusinessDeal.Text = stb2.ToString();

                    }
                    #endregion
                    #region On Credit Limit
                    else if (BDealtype == "2")
                    {
                        lblSaleLimit.Text = dtCustomer.Rows[0]["CREDIT_LIMIT"].ToString();
                        lblBusinessDeal.Text = "On Credit Limits";

                        StringBuilder stb = new StringBuilder();

                        stb.Append("<p><b>" + "Turnover Ratio: " + "</b></p>");
                        stb.Append("<p><b>" + "Start Date: " + "</b></p>");

                        ltrBusinessDealText.Text = stb.ToString();

                        StringBuilder stb2 = new StringBuilder();
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["Turnover_Ratio"].ToString() + " %" + "</b></p>");
                        stb2.Append("<p><b>" + string.Format("{0:dd-MMM-yyyy}", dtCustomer.Rows[0]["DATE_LIMIT"]) + "</b></p>");

                        ltrBusinessDeal.Text = stb2.ToString();

                    }
                    #endregion
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Activate Customer first!');", true);
                return;
            }

            hfZoneId.Value = dtCustomer.Rows[0]["zoneId"].ToString();

            hfGst.Value = dt.Rows[0]["GstPage"].ToString();
            hfAddGst.Value = dt.Rows[0]["AddGstPage"].ToString();

            CustomerLedger();
            lblCustomer.Text = ddlCustomer.SelectedItem.Text;
            //------------------------------------------------------------------------\\

            txtDriverName.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["driverName"].ToString()));
            txtCellNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["cellNo"].ToString()));
            txtTruckNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["truckNo"].ToString()));
            txtBuilty.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["builtyNo"].ToString()));
            rdbDiscountType.SelectedValue = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["freight"].ToString()));
            if (dt.Rows[0]["discount"] != null)
            {
                rdbDiscountType.SelectedValue = "1";
                txtinvoiceDiscount.Text = string.Format("{0:0.00}", dt.Rows[0]["discount"]);
            }
            txtRemarks.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["remarks"].ToString()));

            txtFreight.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["freightAmount"].ToString()));

            orderSku = oeCtrl.SelectOrderDetail_ZO(Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(id[1]), long.Parse(id[0]), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 2);
        }
    }
    private void loadOrder()
    {
        lblOrderNo.Text = "Order No: " + Session["orderId"].ToString();

        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(Session["orderId"]), 1);
        if (dt.Rows.Count > 0)
        {
            hfOrderQty.Value = dt.Rows[0]["orderQty"].ToString();
            ddlDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            LoadAssingned();
            try
            {
                drpAssignedDistributor.SelectedValue = dt.Rows[0]["ASSIGNED_DISTRIBUTOR_ID"].ToString();
            }
            catch(Exception ex)
            { }
            
            ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            lblDate.Text = "SO Date: " + dt.Rows[0]["DOCUMENT_DATE"].ToString();
            txtTotalAmount.Text = string.Format("{0:0.00}", dt.Rows[0]["TOTAL_NET_AMOUNT"]);
            _mCustomerTypeId = int.Parse(_dc.chkNull_0(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString()));
            _mCustomerVolClassId = int.Parse(dt.Rows[0]["VOLUME_CLASS_ID"].ToString());

            hfIsGstReg.Value = _dc.chkNull_0(dt.Rows[0]["isGstRegister"].ToString());
            hfSaleExecutiveID.Value = _dc.chkNull_0(dt.Rows[0]["SALE_EXECUTIVE_ID"].ToString());

            DataTable dtCustomer = _customerCtrl.GetCustomerByID_ZO(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue), 1);
            if (dtCustomer != null)
            {
                if (dtCustomer.Rows[0]["businessDealType"].ToString() != "")
                {

                    string BDealtype = dtCustomer.Rows[0]["businessDealType"].ToString();
                    ///<Summary>
                    ///Customer Deal
                    ///  <Summary>
                    #region On Cash
                    if (BDealtype == "0")
                    {
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() != "0")
                        {
                            lblSaleLimit.Text = dtCustomer.Rows[0]["SALE_LIMIT"].ToString();
                        }
                        lblBusinessDeal.Text = "On Cash";


                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "0")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Advance" + "</b>";
                        }
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "1")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> WithIn Month" + "</b>";
                        }
                        if (dtCustomer.Rows[0]["DealTypeOnCash"].ToString() == "2")
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Supply Basis" + "</b>";
                        }
                        else
                        {
                            ltrBusinessDeal.Text = "<b>" + ">> Sale Basis" + "</b>";
                        }

                    }
                    #endregion
                    #region on Credit Period

                    else if (BDealtype == "1") //On Credit Period
                    {

                        lblSaleLimit.Text = dtCustomer.Rows[0]["SALE_LIMIT"].ToString();
                        lblBusinessDeal.Text = "On Credit Period";

                        StringBuilder stb = new StringBuilder();

                        stb.Append("<p><b>" + "Credit Percent(%): " + "</b></p>");
                        stb.Append("<p><b>" + "No. Of Days: " + "</b></p>");
                        stb.Append("<p><b>" + "Payment Schedule:" + "</b></p>");
                        stb.Append("<p><b>" + "Start Date: " + "</b></p>");

                        ltrBusinessDealText.Text = stb.ToString();

                        StringBuilder stb2 = new StringBuilder();
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["CREDIT_PERCENTAGE"].ToString() + "</b></p>");
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["NoOfDays"].ToString() + "</b></p>");
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["PAYMENT_SCHEDULE"].ToString() + "</b></p>");

                        stb2.Append("<p><b>" + string.Format("{0:dd-MMM-yyyy}", dtCustomer.Rows[0]["DATE_PERIOD"]) + "</b></p>");

                        ltrBusinessDeal.Text = stb2.ToString();
                    }

                    #endregion
                    #region On Credit Limit

                    else if (BDealtype == "2")
                    {
                        lblSaleLimit.Text = dtCustomer.Rows[0]["CREDIT_LIMIT"].ToString();
                        lblBusinessDeal.Text = "On Credit Limits";

                        StringBuilder stb = new StringBuilder();

                        stb.Append("<p><b>" + "Turnover Ratio: " + "</b></p>");
                        stb.Append("<p><b>" + "Start Date: " + "</b></p>");

                        ltrBusinessDealText.Text = stb.ToString();

                        StringBuilder stb2 = new StringBuilder();
                        stb2.Append("<p><b>" + dtCustomer.Rows[0]["Turnover_Ratio"].ToString() + " %" + "</b></p>");
                        stb2.Append("<p><b>" + string.Format("{0:dd-MMM-yyyy}", dtCustomer.Rows[0]["DATE_LIMIT"]) + "</b></p>");

                        ltrBusinessDeal.Text = stb2.ToString();
                    }
                    #endregion
                }
            }

            hfZoneId.Value = dtCustomer.Rows[0]["zoneId"].ToString();
            hfGst.Value = dtCustomer.Rows[0]["TAX_RATE"].ToString();
            hfAddGst.Value = dtCustomer.Rows[0]["TAX_RATE2"].ToString();

            CustomerLedger();

            //------------------------------------------------------------------\\
            txtRemarks.Text = dt.Rows[0]["remarks"].ToString();

            lblCustomer.Text = ddlCustomer.SelectedItem.Text;
            orderSku = oeCtrl.SelectOrderDetail_ZO(Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(Session["orderId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 0);

            if (rblType.SelectedValue == "2")
            {
                foreach (DataRow dr in orderSku.Rows)
                {
                    dr["CurrentRcdQty"] = 0;
                    dr["CurrentRcdQtyUnit"] = 0;
                }
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
    private void LoadCustomers()
    {
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer_SO(int.Parse(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {

                if (dtCustomer.Rows[0]["ShowRoomAddress"] != null)
                {
                    lblCustomerAddress.Text = dtCustomer.Rows[0]["ShowRoomAddress"].ToString();
                    if (lblCustomerAddress.Text == "")
                    {
                        lblCustomerAddress.Text = "N/A";
                    }
                }
                else
                {
                    lblCustomerAddress.Text = "N/A";
                }
                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");
                Session.Add("dtCustomer", dtCustomer);
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

                    lblLedgerBalance.Text = string.Format("{0:0,0.00}", Opening * -1);
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
                }
                else
                {
                    lblCustomerAddress.Text = "N/A";
                }
            }
        }
    }
    private bool CreditLimit()
    {
        if (!chkLimit.Checked)
        {
            decimal ledgerBal = decimal.Parse(_dc.chkNull_0(lblLedgerBalance.Text));
            decimal SaleLimit = decimal.Parse(_dc.chkNull_0(lblSaleLimit.Text));
            if (decimal.Parse(_dc.chkNull_0(txtTotalAmount.Text)) > ledgerBal + SaleLimit)
            {
                return false;
            }
            return true;
        }
        return true;
    }
    private void LoadChequeInformation()
    {
        //Security Cheque Amount = Security Chq's + Rec Chq's + Deposit Chq's - LedgerBalance

        ChequeEntryController CController = new ChequeEntryController();
        decimal chqAmount = 0; decimal chqReceived = 0; decimal chqDeposit = 0;

        //Security Chq's
        DataTable dtChequeAmount = _customerCtrl.GetCustomerCheque(Constants.IntNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue));
        if (dtChequeAmount.Rows.Count > 0)
        {

            foreach (DataRow dr in dtChequeAmount.Rows)
            {
                chqAmount += Convert.ToDecimal(dr["amount"].ToString());
            }
        }

        //Rec Chq's
        DataSet dsReceived = CController.SelectCheques_ZO(527, DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(ddlDistributor.SelectedValue), int.Parse(ddlCustomer.SelectedValue), Constants.IntNullValue);
        DataTable dtReceived = dsReceived.Tables[2];
        if (dtReceived.Rows.Count > 0)
        {
            chqReceived = Convert.ToDecimal(dtReceived.Rows[0][0].ToString());
        }

        //Deposit Chq's
        DataSet dsDeposit = CController.SelectCheques_ZO(528, DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(ddlDistributor.SelectedValue), int.Parse(ddlCustomer.SelectedValue), Constants.IntNullValue);
        DataTable dtDeposit = dsDeposit.Tables[2];
        if (dtDeposit.Rows.Count > 0)
        {
            chqDeposit = Convert.ToDecimal(dtDeposit.Rows[0][0].ToString());
        }


        string fmt2 = "#,##0.00;(#,##0.00)";
        decimal totalAmount = 0;

        if (decimal.Parse(_dc.chkNull_0(lblLedgerBalance.Text)) < 0)
        {
            totalAmount = (chqAmount + chqReceived + chqDeposit) + Convert.ToDecimal(_dc.chkNull_0(lblLedgerBalance.Text));
            lblChequeAmount.Text = totalAmount.ToString(fmt2);
        }
        else
        {
            totalAmount = (chqAmount + chqReceived + chqDeposit) - Convert.ToDecimal(_dc.chkNull_0(lblLedgerBalance.Text));
            lblChequeAmount.Text = totalAmount.ToString(fmt2);
        }

    }
    private void LoadTurnOverRatioDueAmount()
    {
        DataTable dt = _customerCtrl.TurnRatioOrDueAmount(long.Parse(ddlCustomer.SelectedValue), Convert.ToInt32(ddlDistributor.SelectedValue)
            , DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["UserID"].ToString()),1);

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                string fmt2 = "#,##0.00;(#,##0.00)";
                

                lblTurnoverRatio.Text = decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString())).ToString(fmt2) + " %";

                lblDueAmount.Text = decimal.Parse(_dc.chkNull_0(dt.Rows[0][1].ToString())).ToString(fmt2);
 
            }
        }
    }
    private void LoadPromotion()
    {
        //if (int.Parse(Session["PRINCIPAL_ID"].ToString()) > 0)
        //{
        //    SKUPriceDetailController PController = new SKUPriceDetailController();

        //    DataTable dt = PController.SelectDataPrice(int.Parse(this.Session["PRINCIPAL_ID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));

        //    if ((bool.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) == false))
        //    {
                OrderEntryController orderController = new OrderEntryController();
                PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Convert.ToDateTime(this.Session["CurrentWorkDate"]));

                Session.Add("Arpc", Arpc);
            //}
            //else
            //{
            //    Session.Remove("Arpc");
                


            //}
        //}
    }

    #endregion
    private void LoadAssingned()
    {
        drpAssignedDistributor.Items.Clear();
        if (ddlDistributor.Items.Count > 0)
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectDisToDisAssignment(int.Parse(ddlDistributor.SelectedValue.ToString()), Constants.IntNullValue, 1, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(drpAssignedDistributor, dt, 0, 2);
        }

    }
    private void EnableDisableController(bool CValue)
    {
        if (CValue == true)
        {
            ddlCustomer.Enabled = true;
            ddlDistributor.Enabled = true;
            drpAssignedDistributor.Enabled = true;
        }
        else
        {
            ddlCustomer.Enabled = false;
            ddlDistributor.Enabled = false;
            drpAssignedDistributor.Enabled = false;
        }
    }
    protected void rdbCustomerAddress_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbCustomerAddress.SelectedValue == "2")
        {
            CustomerAddress();
        }
        else if (rdbCustomerAddress.SelectedValue == "0")
        {
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
            }
        }
        else
        {
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
   
    private void LoadGird()
    {
        decimal TotalValue = 0;
        decimal TotalSale = 0;
        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        if (Session["invoiceId"] == null)
        {
            foreach (DataRow dr in orderSku.Rows)
            {
                TotalValue += decimal.Parse(dr["qty"].ToString());
                TotalSale += decimal.Parse(dr["RcdQty"].ToString());
            }
        }
        else
        {
        int i = 0;
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
          
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            if (txtCurrentRecQty.Text.Length > 0)
            {
                TotalSale += decimal.Parse(txtCurrentRecQty.Text);

            }
            i += 1;
            }
        txtTotalQty.Text = TotalSale.ToString();
         //   txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
        }
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // orderSku = (DataTable)Session["orderSku"];
        if (e.Row.RowIndex == 0)
        {
            //Set the focus to control on the edited row
            e.Row.FindControl("txtCurrentRecQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCurrentRecQty = (TextBox)e.Row.FindControl("txtCurrentRecQty");
            
        }

    }
    private bool CheckStock(int Qty, int SkuId, string SkuName, int PrevRcdQty)
    {

        if (Qty > 0)
        {
            PurchaseController mController = new PurchaseController();
            DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpAssignedDistributor.SelectedValue), SkuId);
            if (dtstock.Rows.Count > 0)
            {
                if (Math.Round(decimal.Parse(dtstock.Rows[0][0].ToString())) + PrevRcdQty < Qty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName.Replace("'","\\\'") + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName.Replace("'", "\\\'") + " No Stock Found');", true);
                return false;
            }
        }
        return true;
    }
    protected void txtCurrentRecQty_TextChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtCurrentRecQtyUnit = gvr.FindControl("txtCurrentRecQtyUnit") as TextBox;


            // TextBox txtRecQty = sender as TextBox;

            if (txtCurrentRecQty == null) return;
            GridViewRow item = (GridViewRow)txtCurrentRecQty.NamingContainer;

            decimal recieveQty = 0;

            int TotalQty = (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * int.Parse(item.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text));
            if (!string.IsNullOrEmpty(txtCurrentRecQty.Text))
            { //prev rec qty
                recieveQty = ((decimal.Parse(item.Cells[10].Text) * int.Parse(item.Cells[16].Text)) + decimal.Parse(item.Cells[11].Text))
                    + (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * int.Parse(item.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text));

            }

            DataTable order = (DataTable)Session["orderSku"];
            
            decimal orderQty = (decimal.Parse(order.Rows[item.RowIndex]["qty"].ToString()) * int.Parse(order.Rows[item.RowIndex]["UNITS_IN_CASE"].ToString())) + decimal.Parse(order.Rows[item.RowIndex]["qtyUnit"].ToString());
            
            if (recieveQty > orderQty)
            {
                //txtCurrentRecQty.Focus();
                //ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch Qty should not be greater than Order Qty!');", true);

            }
            else
            {
                order.Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                if (rblType.SelectedValue == "1")
                {
                    order.Rows[item.RowIndex]["CurrentRcdQtyUnit"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)));
                }
                else
                {
                    order.Rows[item.RowIndex]["CurrentRcdQtyUnit"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)));
                }
                //   order.Rows[item.RowIndex]["amount"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * decimal.Parse(order.Rows[item.RowIndex]["price"].ToString()));
                order.Rows[item.RowIndex]["amount"] = Convert.ToString(TotalQty * decimal.Parse(order.Rows[item.RowIndex]["price"].ToString()));



                if (item.RowIndex + 1 < grdOrder.Rows.Count)
                {
                    Session.Add("orderSku", order);
                    Calculate();

                    grdOrder.Rows[item.RowIndex + 1].Cells[4].Focus();
                }
                else
                {
                    Session.Add("orderSku", order);
                    Calculate();
                    txtTotalQty.Focus();
                }
            }
            // }
            //else
            //{
            //    txtCurrentRecQty.Focus();
            //}
            //  }
        }
    }


    #endregion
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        bool flag = true;
        int i = 0;
        DataTable dtOrderDetail = new DataTable();
        dtOrderDetail.Columns.Add("CurrentRcdQty", typeof(int));
        dtOrderDetail.Columns.Add("CurrentRcdQtyUnit", typeof(int));
        dtOrderDetail.Columns.Add("SKU_ID", typeof(int));
        dtOrderDetail.Columns.Add("UNITS_IN_CASE", typeof(int));
        dtOrderDetail.Columns.Add("ORDER_QUANTITY", typeof(int));
        dtOrderDetail.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtOrderDetail.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        dtOrderDetail.Columns.Add("AMOUNT", typeof(decimal));
        dtOrderDetail.Columns.Add("NET_AMOUNT", typeof(decimal));
        dtOrderDetail.Columns.Add("TECH_ID", typeof(int));
        dtOrderDetail.Columns.Add("DISTRIBUTOR_PRICE", typeof(decimal));
        decimal GrossAmount = 0;
        int NoOfProducts = 0;
        int OrderQty = 0;
        int DispatchQty = 0;
        bool bStockCheck = true;
        string ItemName = "";
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtCurrentRecQtyUnit = gvr.FindControl("txtCurrentRecQtyUnit") as TextBox;
            OrderQty = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[2].Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + Convert.ToInt32(_dc.chkNull_0(gvr.Cells[3].Text));
            DispatchQty = Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text));
            if(DispatchQty > OrderQty)
            {
                ItemName = gvr.Cells[1].Text;
                bStockCheck = false;
                break;
            }
            if (Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) > 0 || Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)) > 0)
            {
                NoOfProducts++;
            }
        }

        if(!bStockCheck)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('"+ ItemName + " Dispatch Qty can not be greater than Ordered Qty');", true); 
            return;
        }

        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtCurrentRecQtyUnit = gvr.FindControl("txtCurrentRecQtyUnit") as TextBox;
            if (Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) > 0 || Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)) > 0)
            {
                GrossAmount = 0;                
                TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
                TextBox txtRecQtyUnit = gvr.FindControl("txtRecQtyUnit") as TextBox;
                if(rblType.SelectedValue == "1")
                {
                    GrossAmount = (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text));
                }
                else
                {
                    GrossAmount = (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) - int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text));
                }
                DataRow dr = dtOrderDetail.NewRow();
                dr["CurrentRcdQty"] = txtCurrentRecQty.Text;
                if (rblType.SelectedValue == "1")
                {
                    dr["CurrentRcdQtyUnit"] = Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text));
                }
                else
                {
                    dr["CurrentRcdQtyUnit"] = Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)) * -1;
                }                
                dr["SKU_ID"] = gvr.Cells[0].Text;
                dr["UNITS_IN_CASE"] = gvr.Cells[16].Text;
                dr["ORDER_QUANTITY"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[2].Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text));
                dr["UNIT_PRICE"] = gvr.Cells[8].Text;
                if (Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)) > 0)
                {
                    if (rdbDiscountType.SelectedValue == "1")
                    {
                        dr["STANDARD_DISCOUNT"] = Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)) / NoOfProducts;
                    }
                    else
                    {
                        dr["STANDARD_DISCOUNT"] = GrossAmount * Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)) / 100;
                    }
                }
                else
                {
                    dr["STANDARD_DISCOUNT"] = 0;
                }
                dr["AMOUNT"] = GrossAmount;
                dr["NET_AMOUNT"] = GrossAmount - Convert.ToDecimal(dr["STANDARD_DISCOUNT"]);
                dr["TECH_ID"] = gvr.Cells[19].Text;
                dr["DISTRIBUTOR_PRICE"] = gvr.Cells[20].Text;
                dtOrderDetail.Rows.Add(dr);

                int TotalpreRec = 0;
                int curpreEnteredQty = 0;
                if (Session["invoiceId"] != null)
                {
                    TotalpreRec = (Convert.ToInt32(_dc.chkNull_0(txtRecQty.Text)) * int.Parse(gvr.Cells[16].Text))
                    + Convert.ToInt32(_dc.chkNull_0(txtRecQtyUnit.Text));
                    curpreEnteredQty = int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text));
                }
                int currqty = ((Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) * int.Parse(gvr.Cells[16].Text))
                    + Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)));
                if (CheckStock(currqty, int.Parse(gvr.Cells[0].Text), gvr.Cells[1].Text, (TotalpreRec + curpreEnteredQty)))
                {
                    i += 1;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
        }
        if (flag)
        {
            if (CreditLimit())
            {
                long IsValidInsert = -1;
                decimal mTotalAmount = dtOrderDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
                int mTotalQty = dtOrderDetail.Rows.Cast<DataRow>().Sum(dr => ((int.Parse(dr["CurrentRcdQty"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString())) + int.Parse(dr["CurrentRcdQtyUnit"].ToString())));
                decimal gstAmount = 0;// CalculateGst(mTotalAmount, Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)));
                decimal addGstAmount = 0;// CalculateGst(mTotalAmount, Convert.ToDecimal(_dc.chkNull_0(hfAddGst.Value)));
                decimal Invoicediscount = dtOrderDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()));
                decimal creditAmount = 0;
                decimal Freight = Convert.ToDecimal(_dc.chkNull_0(txtFreight.Text));
                if (Invoicediscount < mTotalAmount)
                {
                    creditAmount = mTotalAmount - Invoicediscount + gstAmount + addGstAmount + Freight;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Discount should be less than Total Amount');", true);
                }
                if (Session["orderId"] != null)
                {
                    IsValidInsert = oeCtrl.Add_Invoice(int.Parse(ddlDistributor.SelectedValue), int.Parse(hfOrderQty.Value),
                        long.Parse(ddlCustomer.SelectedValue), long.Parse(Session["orderId"].ToString()), mTotalAmount,
                      Invoicediscount, 0, gstAmount, creditAmount, addGstAmount, dtOrderDetail
                    , int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    , mTotalQty, txtDriverName.Text, txtTruckNo.Text, Convert.ToString(rdbDiscountType.SelectedItem.Value), txtCellNo.Text, txtBuilty.Text
                    , Constants.DateNullValue, txtRemarks.Text, decimal.Parse(_dc.chkNull_0(hfGst.Value)), decimal.Parse(_dc.chkNull_0(hfAddGst.Value))
                    , Convert.ToInt32(hfZoneId.Value), Convert.ToInt32(bool.Parse(hfIsGstReg.Value)), decimal.Parse(_dc.chkNull_0(txtFreight.Text)), int.Parse(rbtnDeliveryMethd.SelectedItem.Value), false, int.Parse(hfSaleExecutiveID.Value), int.Parse(drpAssignedDistributor.SelectedValue));
                    if (IsValidInsert > 0)
                    {
                        Response.Redirect("frmSaleOrderView.aspx?LevelID=161&LevelType=3");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                    }
                }
                else
                {
                    var id = Session["invoiceId"].ToString().Split('-');
                    if (oeCtrl.Update_Invoice(Convert.ToInt64(id[1]), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    , int.Parse(ddlDistributor.SelectedValue), "0", long.Parse(ddlCustomer.SelectedValue), 0,
                    mTotalAmount, Invoicediscount, 0, 0, gstAmount, creditAmount, addGstAmount, 0, 0, dtOrderDetail, int.Parse(this.Session["UserId"].ToString())
                    , 0, 0, 0, mTotalQty, txtDriverName.Text, txtTruckNo.Text, rdbDiscountType.SelectedItem.Value, txtCellNo.Text, txtBuilty.Text
                    , Constants.DateNullValue, txtRemarks.Text, decimal.Parse(_dc.chkNull_0(hfGst.Value)), decimal.Parse(_dc.chkNull_0(hfAddGst.Value))
                    , Convert.ToInt32(hfZoneId.Value), Convert.ToInt32(bool.Parse(hfIsGstReg.Value)), decimal.Parse(_dc.chkNull_0(txtFreight.Text)), int.Parse(rbtnDeliveryMethd.SelectedItem.Value),false, int.Parse(hfSaleExecutiveID.Value),int.Parse(drpAssignedDistributor.SelectedValue)))
                    {
                        Response.Redirect("frmSaleOrderView.aspx?LevelID=161&LevelType=3");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Check Customer Business Limit');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Select At least one Product');", true);
        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        decimal GrossAmount = 0;
        decimal Discount = 0;
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtCurrentRecQtyUnit = gvr.FindControl("txtCurrentRecQtyUnit") as TextBox;
            gvr.Cells[9].Text = "0";
            if (Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) > 0 || Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQtyUnit.Text)) > 0)
            {   
                TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
                TextBox txtRecQtyUnit = gvr.FindControl("txtRecQtyUnit") as TextBox;
                if(rblType.SelectedValue == "1")
                {
                    GrossAmount += (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text));
                    gvr.Cells[9].Text = ((int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text))).ToString();
                }
                else
                {
                    GrossAmount += (int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text));
                    gvr.Cells[9].Text = ((int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Convert.ToInt32(_dc.chkNull_0(gvr.Cells[16].Text)) + int.Parse(_dc.chkNull_0(txtCurrentRecQtyUnit.Text))) * Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[8].Text))).ToString();
                }                
            }
        }
        if (Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)) > 0)
        {
            if (rdbDiscountType.SelectedValue == "1")
            {
                Discount = Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text));
            }
            else
            {
                Discount = GrossAmount * Convert.ToDecimal(_dc.chkNull_0(txtinvoiceDiscount.Text)) / 100;
            }
        }
        txtTotalAmount.Text = GrossAmount.ToString();
        txtNetAmount.Text = (GrossAmount - Discount + Convert.ToDecimal(_dc.chkNull_0(txtFreight.Text))).ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("orderId");
        Session.Remove("invoiceId");

        Response.Redirect("frmSaleOrderView.aspx?LevelID=161&LevelType=3");
    }
    private decimal CalculateGst(decimal mTotalAmount, decimal gst)
    {
        decimal gstAmount = mTotalAmount * (gst / 100);
        return gstAmount;
    }
    private decimal CalculateAdditionGst(decimal mTotalAmount, decimal gst)
    {

        decimal gstAmount = mTotalAmount * (gst / 100);
        return gstAmount;
        
    }
    private decimal CalculateDiscount(decimal mTotalAmount, decimal discount)
    {
        if (discount > 0)
        {
            if (rdbDiscountType.SelectedValue == "0")
            {

                 discount = mTotalAmount * (discount / 100);                
            }
        }
        return discount;
    }
    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBarCode.Visible = false;
        if(rblType.SelectedValue == "2")
        {
            txtBarCode.Visible = true;
            ScriptManager.GetCurrent(Page).SetFocus(txtBarCode);
        }
        if (Session["orderId"] != null)
        {
            loadOrder();
            Session.Add("orderSku", orderSku);
            LoadGird();
        }
        rblType.Enabled = false;        
    }
    protected void btnDummay_Click(object sender, EventArgs e)
    {        
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            if (gvr.Cells[17].Text.Trim().Length > 11 && txtBarCode.Text.Trim().Length > 11)
            {
                if (gvr.Cells[17].Text.Trim().Substring(0, 12) == txtBarCode.Text.Trim().Substring(0, 12))
                {
                    TextBox txtCurrentRecQty = (TextBox)gvr.Cells[6].FindControl("txtCurrentRecQty");
                    txtCurrentRecQty.Text = (Convert.ToInt32(txtCurrentRecQty.Text) + 1).ToString();
                    txtBarCode.Text = string.Empty;
                    rblType.Enabled = false;
                    gvr.FindControl("txtCurrentRecQty").Focus();
                    break;
                }
            }
            else
            {
                if (gvr.Cells[17].Text.Trim() == txtBarCode.Text.Trim())
                {
                    TextBox txtCurrentRecQty = (TextBox)gvr.Cells[6].FindControl("txtCurrentRecQty");
                    txtCurrentRecQty.Text = (Convert.ToInt32(txtCurrentRecQty.Text) + 1).ToString();
                    txtBarCode.Text = string.Empty;
                    rblType.Enabled = false;
                    gvr.FindControl("txtCurrentRecQty").Focus();
                    break;
                }
            }
        }        
        //ScriptManager.GetCurrent(Page).SetFocus(txtBarCode);
    }
}