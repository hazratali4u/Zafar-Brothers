using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmOrderPOS : System.Web.UI.Page
{
    #region Variables

    readonly DistributorController _mDist = new DistributorController();
    readonly RptSaleController _rptSaleCtl = new RptSaleController();
    readonly OrderEntryController _or = new OrderEntryController();
    readonly SKUGroupController _groupCtl = new SKUGroupController();
    readonly RptCustomerController rcc = new RptCustomerController();

    string ExpiryDate;

    DataTable PurchaseSKU;
   
  
    DataControl _dc = new DataControl();
  

   
    public int onhold = 0;
    public long newCustomerId;
    long saleInvoiceID = 0;    
    public int printType;
    public static string CompanyName;
    public static string CompanyPhonNmbr;
    private static string _hirarchyNameQuiz;





    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadProduct();
            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
            txtskuCode.Focus();
            LoadSaleForce();
            txtstartDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            txtEndDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            lblCurrentWorkingDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            hfCurrentWorkDate.Value = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            hfUserID.Value = Session["UserId"].ToString();
            hfDistributorID.Value = Session["DISTRIBUTOR_ID"].ToString();
            LoadloginDetail();
            LoadCustomerData();            
            CreditLimit();
            txtGrossAmount.Attributes.Add("readonly", "readonly");
            numTxtTotalGST.Attributes.Add("readonly", "readonly");
            numTxtTotlAmnt.Attributes.Add("readonly", "readonly");
            txtBalance.Attributes.Add("readonly", "readonly");
            txtDiscount.Attributes.Add("readonly", "readonly");
            hfMaxId.Value = OrderEntryController.GetMaxInvoiceId(Convert.ToInt32(Session["DISTRIBUTOR_ID"]));
            this.LoadPromotion();
        }        
    }
    
    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="cValue">Value</param>
    private void EnableDisableController(bool cValue)
    {
        try
        {
            if (cValue)
            {
                if (txtGrossAmount.Text != "")
                {
                    if (decimal.Parse(txtGrossAmount.Text) > 0)
                    {
                        ddlCustomer.Enabled = true;
                       
                    }
                }
                else
                {
                    ddlCustomer.Enabled = false;
                  
                }
            }
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    private void DisableControls()
    {
        //txtOutletCode.Enabled = false;
        //txtOutletName.Enabled = false;
        //txtCashRecieved2.Enabled = false;
        //numTxtTotalGST.Enabled = false;
        //txtGrossAmount.Enabled = false;
        //numTxtTotlAmnt.Enabled = false;
        //txtDiscount.Enabled = false;
        //DrpPayMode.Enabled = false;
        //DrpDiscount.Enabled = false;
        //txtskuCode.Enabled = false;
        //btnCancel.Enabled = false;
        //btnVoid.Enabled = false;
        //btnSaveOrder.Enabled = false;
        //btnRePrint.Enabled = false;

        //PnlPrintInvoice.Visible = true;
        //btn_exitprint.Visible = true;
        ((LinkButton)Master.FindControl("btnEditinvoice")).Enabled = false;
        ((LinkButton)Master.FindControl("btnHold")).Enabled = false;
        ((LinkButton)Master.FindControl("btnUnhold")).Enabled = false;
        ((LinkButton)Master.FindControl("btnpricelookup")).Enabled = false;

    }
    
    #region Clear

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearDetail()
    {
        try
        {
            txtskuCode.Text = "";
            txtskuName.Text = "";
            txtUnitRate.Text = "";
            hfToggleMode.Value = "SALE MODE";
            if (hfToggleMode.Value != "SALE MODE")
            {
                txtQuantity.Text = "-1";
            }
            else
            {
                txtQuantity.Text = "1";
            }
            txtDiscount.Text = "0";
            txtcolor.Text = "";
            txtsize.Text = "";

          //  btnSave.ToolTip = "Add Sku";
            btnSaveOrder.Enabled = true;

            txtskuCode.Enabled = true;
            txtskuCode.Focus();

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }

    }

    private void ClearAll()
    {
        try
        {
            txtskuCode.Text = "";
            txtskuName.Text = "";
            txtUnitRate.Text = "";
            hfToggleMode.Value = "SALE MODE";
            if (hfToggleMode.Value != "SALE MODE")
            {
                txtQuantity.Text = "-1";
            }
            else
            {
                txtQuantity.Text = "1";
            }
            txtDiscount.Text = "0";
            txtcolor.Text = "";
            txtsize.Text = "";
            txtCashRecieved2.Text = "";
          //  btnSave.ToolTip = "Add Sku";
            btnSaveOrder.Enabled = true;

            txtskuCode.Enabled = true;

            txtskuCode.Focus();
            txtAuthorisedBy.Text = "";
            txtBalance.Text = "";




        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterAll()
    {
        try
        {
            Session.Remove("CustName");
            Session.Remove("CustCode");
            txtGrossAmount.Text = "";
            numtxtTotalExtraDiscnt.Text = "";
            txtBalance.Text = "";
            numTxtTotalGST.Text = "";
            numTxtTotlAmnt.Text = "";
            txtCashRecieved2.Text = "";
            txtAuthorisedBy.Text = "";
            hfToggleMode.Value = "SALE MODE";
            if (hfToggleMode.Value != "SALE MODE")
            {
                txtQuantity.Text = "-1";
            }
            else
            {
                txtQuantity.Text = "1";
            }
            if (DrpPayMode.SelectedValue == "215" || DrpPayMode.SelectedValue == "218")
            {
                txtCashRecieved2.Text = "";
                txtBalance.Text = numTxtTotlAmnt.Text;
                txtCashRecieved2.Attributes.Add("readonly", "readonly");
            }
            else {
               txtCashRecieved2.ReadOnly = false;
            }      
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    #endregion

    #region Click OPerations


    [WebMethod]
    [ScriptMethod]
    public static string InsertInvoice(string orderedProducts, string amountDue, string author, string discount, string netAmount, string paidIn,
        string payType, string Gst, string manualId, string customerId, string saleForce, string strcurrentWorkDate,string strUsserID,
        string strDistributorID)
    {
        try
        {
            long lngID = 0;
            DataControl _dc = new DataControl();
            var manualId2 = manualId == "SALE MODE" ? "2" : "1";
            DateTime currentWorkDate = Convert.ToDateTime(strcurrentWorkDate);
            int userId = int.Parse(strUsserID);
            int distributerId = int.Parse(strDistributorID);
            var SaleSKU = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));
            DataTable dtTradeOffer = (DataTable)HttpContext.Current.Session["dtTradeOffer"];
            if (SaleSKU != null && SaleSKU.Rows.Count > 0)
            {
                if (HttpContext.Current.Session["MANUAL_DISCOUNT"].ToString() == "False" && Convert.ToInt32(SaleSKU.Rows[0]["QUANTITY_UNIT"]) > 0)
                {
                    if (dtTradeOffer != null)
                    {
                        foreach (DataRow dr2 in dtTradeOffer.Rows)
                        {
                            foreach (DataRow dr in SaleSKU.Rows)
                            {
                                if (dr["SKU_ID"].ToString() == dr2["SKU_ID"].ToString())
                                {
                                    dr["STANDARD_DISCOUNT"] = dr2["STANDARD_DISCOUNT"];
                                    break;
                                }
                            }
                        }
                    }
                    HttpContext.Current.Session.Remove("dtTradeOffer");
                    lngID = OrderEntryController.Add_InvoicePOSAutoDiscount(distributerId, manualId2, 0, long.Parse(customerId), long.Parse(customerId), 0, Convert.ToInt32(saleForce), 0,
                        decimal.Parse(_dc.chkNull_0(amountDue)), decimal.Parse(_dc.chkNull_0(discount)), 0, decimal.Parse(_dc.chkNull_0(Gst)), Decimal.Parse(_dc.chkNull_0(netAmount)), 0, int.Parse(payType),
                        SaleSKU, userId, 0, currentWorkDate, 0, 0, _dc.chkNull_0(author));
                }
                else
                {                    
                    lngID = OrderEntryController.Add_InvoicePOS(distributerId, manualId2, 0, long.Parse(customerId), long.Parse(customerId), 0, Convert.ToInt32(saleForce), 0,
                        decimal.Parse(_dc.chkNull_0(amountDue)), decimal.Parse(_dc.chkNull_0(discount)), 0, decimal.Parse(_dc.chkNull_0(Gst)), Decimal.Parse(_dc.chkNull_0(netAmount)), 0, int.Parse(payType),
                        SaleSKU, userId, 0, currentWorkDate, 0, 0, _dc.chkNull_0(author));
                }
            }
            return lngID.ToString();
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
    
    protected void btnUpdateLimit_Click(object sender, EventArgs e)//cancel button click 
    {
       
            CreditLimit();
        
    }

    protected void btnNewCustomer_Click(object sender, EventArgs e)
    {
        
        Server.Transfer("frmNewCustomerPOS.aspx");
    }
    protected void btnVoid_Click(object sender, EventArgs e)//cancel button click 
    {
        ClearAll();
        ClearMasterAll();
        btnToggleMode.Disabled = false;
        hfIsCalculate.Value = "0";
        txtskuCode.Focus();
    }

   
    /// <summary>
    /// OPerate on Exit Button ...Clear Sessions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session.RemoveAll();
        Session.Remove("PurchaseSKU");
        Session.Remove("dtFreeSKU");
        Session.Remove("CustName");
        Session.Remove("CustCode");
        Response.Redirect("Home.aspx?");
        hfIsCalculate.Value = "0";
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        decimal ObjGrossSales = 0;
        decimal ObjStandardDiscount = 0;
        decimal ObjExtraDiscount = 0;

        DataControl dc = new DataControl();
        OrderEntryController or = new OrderEntryController();
        SKUGroupController GroupCtl = new SKUGroupController();
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(tab.Value, (typeof(DataTable)));
        foreach (DataRow dr in dt.Rows)
        {
            dr["STANDARD_DISCOUNT"] = 0;
        }
        PromotionCollections_Controller Arpc = (PromotionCollections_Controller)this.Session["Arpc"];
        if (dt.Rows.Count > 0)
        {
            ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, 88, Convert.ToInt32(ddlCustomer.SelectedValue), dt, false);
            for (int i = 0; i < ARlistOffer.Count; i++)
            {
                PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];
                if (pofferCol.Is_Claimable)
                {
                    if (pofferCol.SKU_ID > 0)
                    {
                        #region slab on Item
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region slab on Item Group

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    if (pofferCol.SKU_ID > 0)
                    {
                        #region slab on Item
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region slab on Item Group

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }

            #region Recalulate Products

            foreach (DataRow dr in dt.Rows)
            {
                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                ObjStandardDiscount += decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
            }
            #endregion
        }
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
            string manualId = null;
            if (hfToggleMode.Value == "SALE MODE")
            {
                manualId = "2";//for order invoice
            }
            else
            {
                manualId = "1";//for sale return
            }
            DataTable PurchaseSKU = (DataTable)JsonConvert.DeserializeObject(tab.Value, (typeof(DataTable)));
            OrderEntryController mOrderController = new OrderEntryController();
            if (PurchaseSKU.Rows.Count > 0)
            {
                if (btnSaveOrder.ToolTip == "Save")
                {
                    saleInvoiceID = 0;// mOrderController.Add_Invoice2(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), manualId, mTownId, 0, int.Parse(Session["PRINCIPAL_ID"].ToString()), long.Parse(Session["CUSTOMER_ID"].ToString()), long.Parse(Session["CUSTOMER_ID"].ToString()), 0, Convert.ToInt32(ddsalesForce.SelectedValue), 0,
                    if (saleInvoiceID == -2 || saleInvoiceID == -1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion Failed!!!.');", true);
                    }
                    else
                    {
                        ClearMasterAll();
                        btnToggleMode.Disabled = false;
                        ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);
                        CORNBusinessLayer.Reports.CrpPrintInvoice crpReport = new CORNBusinessLayer.Reports.CrpPrintInvoice();
                        DataSet ds = null;
                        ds = rcc.PrintInvoice(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["PRINCIPAL_ID"].ToString()), 2, int.Parse(saleInvoiceID.ToString()), Constants.DateNullValue, Constants.DateNullValue);
                        crpReport.SetDataSource(ds);
                        crpReport.Refresh();
                        if (string.IsNullOrEmpty(_hirarchyNameQuiz))
                        {
                            _hirarchyNameQuiz = "Need";
                        }
                        crpReport.SetParameterValue("COMPANY_NAME", _hirarchyNameQuiz);
                        crpReport.SetParameterValue("INVOICENO", Convert.ToString(saleInvoiceID));
                        crpReport.SetParameterValue("LOCATION", lblLoacation.Text);
                        crpReport.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);
                        crpReport.SetParameterValue("CASHIER", lbluserlogin.Text);
                        Session.Add("CrpReport", crpReport);
                        Session.Add("ReportType", 0);
                        const string url = "'Default.aspx'";
                        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=500,height=550,left=20,top=20\");</script>";
                        Type cstype = GetType();
                        var cs = Page.ClientScript;
                        cs.RegisterStartupScript(cstype, "OpenWindow", script);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No product added');", true);
            }
            ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);
        }
        catch (Exception eee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
        }
    }
    
    //For Print INvoices again
  
    protected void btnRePrint_Click(object sender, EventArgs e)
    {
        //mainPOS.Attributes.Add("style", "display:none;");
        ////  ClearAll();
        //// ClearMasterAll();

        //DisableControls();
        //PnlPrintInvoice.Visible = true;

        //LoadGridInvoices();

    }

    #endregion
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CreditLimit();

        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtUnitRate.Text = "";
       
        
        txtDiscount.Text = "0";
       
        txtsize.Text = "";
        
       
      
        txtGrossAmount.Text = "";
        numTxtTotlAmnt.Text = "";
        numTxtTotalGST.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        txtBalance.Text = "";


        txtCashRecieved2.Text = "";

        txtskuCode.Focus();
    }

    private void CreditLimit()
    {
        CustomerDataController cdCtrl = new CustomerDataController();
       
        lblCreditLimit.Text = "0";
        lblLedgerBalance.Text = "0";
        lblAllowLimit.Text = "0";

        if (ddlCustomer.Items.Count > 0)
        {
           // --working on both options credit, credit and cash  
            DataTable dt = cdCtrl.SelectCustomerCreditBalance(long.Parse(ddlCustomer.SelectedValue), Convert.ToInt32(Session["DISTRIBUTOR_ID"].ToString()), Constants.Credit);
           
            if (dt == null) return;

            if (dt.Rows.Count < 0) return;
            {
                //This Limit is AllowLimit + Ledger Balance
                lblAllowLimit.Text = Convert.ToString(decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString())));

                //This Limit is entered by user while adding customer
                lblCreditLimit.Text = Convert.ToString(decimal.Parse(_dc.chkNull_0(dt.Rows[0][1].ToString())));

                lblLedgerBalance.Text = Convert.ToString(decimal.Parse(_dc.chkNull_0(dt.Rows[0][2].ToString())));
            }

        }
    }

    #region Load Functions

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSku()
    {
        bool flag = true;

        PurchaseSKU = (DataTable)Session["PurchaseSKU"];

        DataRow[] foundRows2 = PurchaseSKU.Select("SKU_CODE = '" + txtskuCode.Text + "'");

        if (foundRows2.Length != 0)
        {
            foreach (GridViewRow dr2 in GrdPurchase.Rows)
            {
                string code = dr2.Cells[1].Text;
                int chkdel = Convert.ToInt32(dr2.Cells[10].Text);

                if ((txtskuCode.Text == code) && (chkdel == 1))
                {
                    int index = dr2.RowIndex;
                    PurchaseSKU.Rows.RemoveAt(index);
                    flag = true;
                    break;
                }
                else if ((txtskuCode.Text == code) && (chkdel == 0))
                {
                    flag = false;
                    break;
                }
            }
        }
        return flag;
    }
    private static bool FindCustomer(string customerCode)
    {

        DataTable dtCustomer = (DataTable)HttpContext.Current.Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + customerCode.Trim() + "'");
        if (foundRows.Length > 0)
        {
            HttpContext.Current.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
            
          
            return true;
        }
        return false;
    }
    /// <summary>
    /// Verifies Customer Code
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    /// 
    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);            
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }
    
    private void LoadDocumentDetail(long docId, int typeId)
    {
        try
        {
            OrderEntryController ord = new OrderEntryController();
            PurchaseSKU = ord.GetDocumentDetail(docId, typeId);
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    private void LoadloginDetail()
    {
        try
        {
            UserController userControl = new UserController();
            DataTable dt = userControl.SelectSlashUser2(int.Parse(Session["UserId"].ToString()));
            lbllogintimedate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");//DateTime.Now.ToString("MM/dd/yyyy");
            lbluserlogin.Text = dt.Rows[0]["USER_NAME"].ToString();

            lblLoacation.Text = dt.Rows[0]["DISTRIBUTOR_NAME"].ToString();
            hfLocationName.Value = lblLoacation.Text;
            CompanyName = dt.Rows[0]["COMPANY_NAME"].ToString();
            hfCompanyName.Value = CompanyName;
            CompanyPhonNmbr = dt.Rows[0]["CONTACT_NUMBER"].ToString();//Location Contact Number
            hfContactNo.Value = "PH: "+CompanyPhonNmbr;
            

            AutoComplete.ContextKey = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            Session.Add("DISTRIBUTOR_ID", dt.Rows[0]["DISTRIBUTOR_ID"].ToString());
            
            DataTable dt2 = userControl.SelectUserPrincipal(int.Parse(Session["UserId"].ToString()));
            if (dt2 != null)
            {
                _hirarchyNameQuiz = dt2.Rows[0]["SKU_HIE_NAME"].ToString();
            }

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {

      //  ListCustomer.Items.Clear();
        CustomerDataController mController = new CustomerDataController();//int.Parse(Session["DISTRIBUTOR_ID"].ToString()),int.Parse(Session["AreaId"].ToString()), int.Parse(Session["PrincipalId"].ToString())
        DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL", true);
        //if (Session["CustName"] == null)
        //{
        //    if (dtCustomer.Rows.Count > 0)
        //    {
        //        txtOutletCode.Text = dtCustomer.Rows[0][2].ToString();
        //        txtOutletName.Text = dtCustomer.Rows[0][3].ToString();
        //    }
        //}
        //clsWebFormUtil.FillListBox(ListCustomer, dtCustomer, "CUSTOMER_DETAIL", "CUSTOMER_DETAIL", true);
        Session.Add("dtCustomer", dtCustomer);


    }

    /// <summary>
    /// Enables/Disables Discount Fields For Manual And Auto Promotion And Loads Promotion Controler For Auto Promotion
    /// </summary>
  
    private void LoadSaleForce()
    {
        Distributor_UserController UController = new Distributor_UserController();
        
        DataTable dt = UController.SelectDistributorUser(37, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["CompanyId"].ToString()));//37 is the ref id  for sale force 
        clsWebFormUtil.FillDropDownList(ddsalesForce, dt, "USER_ID", "USER_NAME");
        ddsalesForce.SelectedValue = Convert.ToString(Session["UserId"]);
        ddl_saleforce2.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddl_saleforce2, dt, "USER_ID", "USER_NAME", false);

    }
    public void LoadOnHoldInvoicNumber()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 0, 0, 0, 0, Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        ((TextBox)(Master.FindControl("lblonHold"))).Text = Convert.ToString(dtOrder.Rows.Count);

    }
    private void LoadProduct()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable dtProduct = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            if (dtProduct != null && dtProduct.Rows.Count > 0)
            {
                hfProduct.Value = GetJson(dtProduct);
                Session.Add("Dtsku_Price", dtProduct);
            }           
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    [WebMethod]
    public static string GetMaxID()
    {
        return OrderEntryController.GetMaxInvoiceId(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()));
    }

    [WebMethod]
    public static string GetDiscount(string orderedProducts)
    {        
        DataControl dc = new DataControl();
        OrderEntryController or = new OrderEntryController();
        SKUGroupController GroupCtl = new SKUGroupController();
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

        PromotionCollections_Controller Arpc = (PromotionCollections_Controller)HttpContext.Current.Session["Arpc"];
        if (dt.Rows.Count > 0)
        {
            ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, 88, Constants.IntNullValue, dt, false);
            for (int i = 0; i < ARlistOffer.Count; i++)
            {
                PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];
                if (pofferCol.Is_Claimable)
                {
                    if (pofferCol.SKU_ID > 0)
                    {
                        #region slab on Item
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region slab on Item Group

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    if (pofferCol.SKU_ID > 0)
                    {
                        #region slab on Item
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = (decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100)).ToString();
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region slab on Item Group

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                            {
                                if (pofferCol.Is_And == true)
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                    }
                                }
                                else
                                {
                                    if (pofferCol.Offer_Value > 0)
                                    {
                                        dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                    }
                                    if (pofferCol.Discount > 0)
                                    {
                                        dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }

            #region Recalulate Products

            foreach (DataRow dr in dt.Rows)
            {
                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())));
            }
            #endregion
        }
        HttpContext.Current.Session.Add("dtTradeOffer", dt);
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    private void LoadPromotion()
    {
        if (Session["MANUAL_DISCOUNT"].ToString() == "False")
        {
            OrderEntryController orderController = new OrderEntryController();
            PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Convert.ToDateTime(this.Session["CurrentWorkDate"]));
            this.Session.Add("Arpc", Arpc);
            btnCalculate.Visible = true;
            hfIsCalculate.Value = "0";
            hfIsManual.Value = "0";
        }
    }

    #endregion

    #region Sale Report

    protected void btnViewSalesReport_Click(object sender, EventArgs e)
    {

        ClearAll();
        ClearMasterAll();

        if (ddlReportType.SelectedValue == "2")
        {
            try
            {

                CORNBusinessLayer.Reports.crpSalesReportPos CrpReport = new CORNBusinessLayer.Reports.crpSalesReportPos();

                DataSet ds = null;

                ds = _rptSaleCtl.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(ddl_saleforce2.SelectedValue), DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text), Constants.LongNullValue);

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                if (string.IsNullOrEmpty(_hirarchyNameQuiz))
                {
                    _hirarchyNameQuiz = "Need";
                }
                CrpReport.SetParameterValue("COMPANY_NAME", _hirarchyNameQuiz);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtstartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("USER_NAME", Convert.ToString(ddl_saleforce2.SelectedItem.Text));
                CrpReport.SetParameterValue("LOCATION", lblLoacation.Text);
                CrpReport.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 0);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=450,height=450,left=40,top=40\");</script>";
                Type cstype = GetType();
                var cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        else
        {
            try
            {


                CORNBusinessLayer.Reports.CRPSalesReportSummary CrpReport = new CORNBusinessLayer.Reports.CRPSalesReportSummary();

                DataSet ds = null;

                ds = _rptSaleCtl.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(ddl_saleforce2.SelectedValue), DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text), -1);

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                if (string.IsNullOrEmpty(_hirarchyNameQuiz))
                {
                    _hirarchyNameQuiz = "Need";
                }
                CrpReport.SetParameterValue("COMPANY_NAME", _hirarchyNameQuiz);
                CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtstartDate.Text));
                CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("USER_NAME", Convert.ToString(ddl_saleforce2.SelectedItem.Text));
                CrpReport.SetParameterValue("LOCATION", lblLoacation.Text);
                CrpReport.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", 0);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=450,height=450,left=40,top=40\");</script>";
                Type cstype = GetType();
                var cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
    }
   
    #endregion
}