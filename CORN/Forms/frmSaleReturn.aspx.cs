using System;
using System.Data;
using System.Collections;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CrystalDecisions.Shared;
using System.Web;
using CORNBusinessLayer.Reports;
using System.Globalization;

public partial class Forms_frmSaleReturn : System.Web.UI.Page
{
    #region Variables

    readonly DistributorController _mDist = new DistributorController();
    readonly OrderEntryController _or = new OrderEntryController();
    RptCustomerController rptCustomerController = new RptCustomerController();

    DataTable PurchaseSKU;
    readonly DataControl _dc = new DataControl();

    #endregion

    private void CreateTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));

        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT_BOX", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT_SQM", typeof(decimal));

        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_TEMP", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));

        PurchaseSKU.Columns.Add("UOM", typeof(int));
       
        PurchaseSKU.Columns.Add("TRADE_PRICE", typeof(string));
        PurchaseSKU.Columns.Add("DISCOUNT_TYPE", typeof(int));
        PurchaseSKU.Columns.Add("SALE_INVOICE_ID", typeof(long));

        PurchaseSKU.Columns.Add("returnQty", typeof(decimal));
        PurchaseSKU.Columns.Add("returnDiscount", typeof(decimal));
        PurchaseSKU.Columns.Add("returnAmount", typeof(decimal));
        PurchaseSKU.Columns.Add("AgeDiscount", typeof(decimal));

        Session.Add("PurchaseSKU", PurchaseSKU);
    }
 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CreateTable();
            
            txtGrossAmount.Attributes.Add("readonly", "readonly");
            numtxtTotalExtraDiscnt.Attributes.Add("readonly", "readonly");
            numtxtDiscount.Attributes.Add("readonly", "readonly");
            numTxtTotalGST.Attributes.Add("readonly", "readonly");
            numTxtTotlAmnt.Attributes.Add("readonly", "readonly");
            txtBalance.Attributes.Add("readonly", "readonly");
            txtAuthorisedBy.Attributes.Add("readonly", "readonly");
            txtCashRecieved.Attributes.Add("readonly", "readonly");
            txtGrossReturn.Attributes.Add("readonly", "readonly");
            txtNetReturn.Attributes.Add("readonly", "readonly");
            txtBalanceReturn.Attributes.Add("readonly", "readonly");
         
            if (Session["srId"] != null)
            {
                
                lblInvoices.Text = "Invoice No" + " - " + Session["srId"].ToString();
                lblInvoices.Visible = true;
                
                DataTable piDt = rptCustomerController.SelectCustomerInvoicePrint(long.Parse(Session["srId"].ToString()),Constants.IntNullValue,3, Constants.DateNullValue);

                if (piDt == null) return;
                if (piDt.Rows.Count > 0)
                {
                    DataRow[] foundRows = piDt.Select("SALE_INVOICE_ID  = '" + Session["srId"] + "'");

                    if (foundRows.Length > 0)
                    {

                        hfCustomerId.Value = foundRows[0]["CUSTOMER_ID"].ToString();

                        DrpPayMode.SelectedValue = foundRows[0]["PAYMENT_MODE"].ToString();
                        txtGrossAmount.Text = String.Format("{0:0.00}", foundRows[0]["TOTAL_AMOUNT"]);
                        numtxtDiscount.Text = String.Format("{0:0.00}", foundRows[0]["EXTRA_DISCOUNT"]);
                        numtxtTotalExtraDiscnt.Text = String.Format("{0:0.00}", foundRows[0]["DISCOUNT"]);
                        numTxtTotalGST.Text = String.Format("{0:0.00}", foundRows[0]["GST"]);
                       
                        numTxtTotlAmnt.Text = String.Format("{0:0.00}", foundRows[0]["NET_AMOUNT"]);
                        txtCashRecieved.Text = String.Format("{0:0.00}", foundRows[0]["CASH_RECIEVED"]);
                        txtAuthorisedBy.Text = foundRows[0]["AUTHORISED_PERSON"].ToString();
                        txtExtraDiscountReturnAge.Text = String.Format("{0:0.00}", foundRows[0]["EXTRA_DISCOUNT_AGE"]);
                        

                        if (int.Parse(DrpPayMode.SelectedValue) != Constants.CustomerCredit_Id)
                        {
                            txtBalance.Text = Convert.ToString(decimal.Parse(txtCashRecieved.Text) - (decimal.Parse(numTxtTotlAmnt.Text)));
                        }
                        
                        
                    }
                }
                
                DataTable dt = _or.GetDocumentDetail(long.Parse(Session["srId"].ToString()), 6, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] foundRows2 = dt.Select("SALE_INVOICE_ID  = '" + Session["srId"] + "'");
                    if (foundRows2.Length > 0)
                    {
                        txtGrossReturn.Text = String.Format("{0:0.00}", foundRows2[0]["GrossReturn"]);
                        txtDiscountReturn.Text = String.Format("{0:0.00}", foundRows2[0]["ItemDiscountReturn"]);
                        txtExtraDiscountReturn.Text = String.Format("{0:0.00}", foundRows2[0]["ExtraDiscountReturn"]);
                        txtNetReturn.Text = String.Format("{0:0.00}", foundRows2[0]["NetReturn"]);
                        txtCashReturn.Text = String.Format("{0:0.00}", foundRows2[0]["cashRecieved"]);                      
                        txtNarration.Text = foundRows2[0]["remarks"].ToString();                        
                        hfInvoiceId.Value = foundRows2[0]["SALES_RETURN_ID"].ToString();
                        hfSaleForceId.Value = foundRows2[0]["SALE_FORCE"].ToString();
                        lblCustomerName.Text = foundRows2[0]["CUSTOMER_NAME"].ToString();
                        txtBalanceReturn.Text = Convert.ToString(decimal.Parse(txtCashReturn.Text) - (decimal.Parse(txtNetReturn.Text)));
                        btnSaveOrder.Text = "Update";
                    }
                    LoadDocumentDetail(long.Parse(Session["srId"].ToString()), 7);
                }                
            }
            else
            {
                lblInvoices.Visible = true;
                txtInvoiceNo.Visible = true;
                btnSearch.Visible = true;
            }
        }
    }


    #region Sel/check Changed OPerations

    protected void txtreturnQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txtreturnQty = sender as TextBox;
        if (txtreturnQty == null) return;
        GridViewRow item = (GridViewRow)txtreturnQty.NamingContainer;

        decimal returnQty = 0;
        if (!string.IsNullOrEmpty(txtreturnQty.Text))
        {
            returnQty =  decimal.Parse(txtreturnQty.Text);
        }

        DataTable dtOrderDetail = (DataTable)Session["PurchaseSKU"];
        if (returnQty >= 0)
        {
            decimal qty = decimal.Parse(dtOrderDetail.Rows[item.RowIndex]["QUANTITY_UNIT"].ToString());
            if (qty >= returnQty  + decimal.Parse(item.Cells[21].Text))
            {
               decimal price = decimal.Parse(dtOrderDetail.Rows[item.RowIndex]["UNIT_PRICE"].ToString());
               decimal Discount = decimal.Parse(dtOrderDetail.Rows[item.RowIndex]["AgeDiscount"].ToString());

               decimal hSqm = 1;
               decimal hBox = 1;
               decimal hPcs = 1;

                if(dtOrderDetail.Rows[item.RowIndex]["sqm"].ToString()!="")
                {

                    hSqm = Convert.ToDecimal(dtOrderDetail.Rows[item.RowIndex]["sqm"].ToString());
                }
                if (dtOrderDetail.Rows[item.RowIndex]["box"].ToString() != "")
                {
                     hBox = Convert.ToDecimal(dtOrderDetail.Rows[item.RowIndex]["box"].ToString());
                }

                if (dtOrderDetail.Rows[item.RowIndex]["pcs"].ToString() != "")
                {
                    hPcs = Convert.ToDecimal(dtOrderDetail.Rows[item.RowIndex]["pcs"].ToString());
                }

            double returnQty2 = 0;

                //uom2 IS INVENTORYuNIT
                //uom2 IS SALEuNIT


                if(dtOrderDetail.Rows[item.RowIndex]["UOM2"].ToString()=="1")// pcs
                {
                    if (dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "2")//box
                    {
                        returnQty = returnQty / hPcs;
                       
                    }
                    else if(dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "3")//sqm
                    {
                        returnQty2 = Convert.ToDouble(returnQty / hPcs);

                        returnQty2 = Math.Round((returnQty2 * Convert.ToDouble(hSqm)), MidpointRounding.ToEven);
                        returnQty = decimal.Parse(returnQty2.ToString());
                    
                    }
                }
                else if (dtOrderDetail.Rows[item.RowIndex]["UOM2"].ToString() == "2")//box
                {
                    if (dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "1")//pcs
                    {
                        returnQty2 = Convert.ToDouble(returnQty * hSqm);

                        returnQty2 = Math.Round((returnQty2 * Convert.ToDouble(hPcs)), MidpointRounding.ToEven);
                        returnQty = decimal.Parse(returnQty2.ToString());
                        
                    }
                    else if (dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "3")//sqm
                    {
                        returnQty = returnQty * hSqm;
                        
                    }
                }
                else if(dtOrderDetail.Rows[item.RowIndex]["UOM2"].ToString() == "3")//sqm
                {
                    if (dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "1")//pcs
                    {
                        returnQty2 = Convert.ToDouble(returnQty / hSqm);
                        returnQty2 = Math.Round((returnQty2 * Convert.ToDouble(hPcs)), MidpointRounding.ToEven); // Rounds "up"

                        returnQty = decimal.Parse(returnQty2.ToString());
                        
                    }
                    else if (dtOrderDetail.Rows[item.RowIndex]["UOM"].ToString() == "2")//box
                    {
                       returnQty = returnQty / hSqm;
                        
                        
                    }
                }

                Discount = (price *  returnQty) * Discount / 100;
                dtOrderDetail.Rows[item.RowIndex]["returnDiscount"] =string.Format("{0:0.00}",Discount);
                dtOrderDetail.Rows[item.RowIndex]["returnQty"] =  returnQty;
                dtOrderDetail.Rows[item.RowIndex]["returnAmount"] = price * returnQty;
               
                if (item.RowIndex + 1 < GrdPurchase.Rows.Count)
                {
                    GrdPurchase.Rows[item.RowIndex + 1].Cells[13].Focus();
                }
                else
                {
                    txtCashReturn.Focus();
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Return Qty should be less than Sale Qty!');", true);
                txtreturnQty.Text = "0.00";
                txtreturnQty.Focus();

            }
            Session["PurchaseSKU"] = dtOrderDetail;
            Calculate();
        }

    }
    
    #endregion
  
    #region Load Functions

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
            PurchaseSKU = _or.GetDocumentDetail(docId, typeId, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

   
    #endregion


   protected void Calculate()
    {
        DataTable dt = new DataTable();

        decimal ObjGrossSales = 0;
        decimal ObjStandardDiscount = 0;
        decimal ObjTotalGST = 0;
        decimal ObjTotalTST = 0;
        decimal ObjFreeGSTAmount = 0;
       

        dt = (DataTable)Session["PurchaseSKU"];


        if (dt.Rows.Count > 0)
        {
            #region Reversecalulate Products
            

                ObjGrossSales = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["returnAmount"].ToString()));
                    ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["returnDiscount"].ToString()));
                }
            
            #endregion

        }

        #region Set Total Values

        txtGrossReturn.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
       // decimal ExtraDiscount = Convert.ToDecimal(_dc.chkNull_0(numtxtDiscount.Text));
        decimal ExtraDiscount = Math.Round((ObjGrossSales - ObjStandardDiscount) * (decimal.Parse(_dc.chkNull_0(txtExtraDiscountReturnAge.Text)) / 100), 2);
           
        if (ObjGrossSales != 0)
        {

            txtDiscountReturn.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
            txtExtraDiscountReturn.Text = Convert.ToString(Math.Round(ExtraDiscount,2));           
            txtNetReturn.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjStandardDiscount - ExtraDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST, 2));
            txtBalanceReturn.Text = (Math.Round(Convert.ToDecimal(_dc.chkNull_0(txtCashReturn.Text)) - (Convert.ToDecimal(_dc.chkNull_0(txtNetReturn.Text))), 2)).ToString();
        }
        else
        {

            txtDiscountReturn.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
            txtExtraDiscountReturn.Text = Convert.ToString(Math.Round(ExtraDiscount, 2));   
           // numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST + ObjFreeGSTAmount, 2));
            txtNetReturn.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjStandardDiscount - ExtraDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST, 2));
            txtBalanceReturn.Text = (Math.Round(Convert.ToDecimal(_dc.chkNull_0(txtNetReturn.Text)) - (Convert.ToDecimal(_dc.chkNull_0(txtCashReturn.Text))), 2)).ToString();

        }

        #endregion

    }

   protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dtOrderDetail = (DataTable)Session["PurchaseSKU"];
        if (e.Row.RowIndex == 0)
        {   
            //Set the focus to control on the edited row
            e.Row.FindControl("txtreturnQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtreturnQty = (TextBox)e.Row.FindControl("txtreturnQty");
     
        }

    }

    #region Click OPerations

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearMasterAll();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable piDt = rptCustomerController.SelectCustomerInvoicePrint(long.Parse(txtInvoiceNo.Text.Trim()),int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        
        if (piDt == null) return;

        if (piDt.Rows.Count > 0)
        {

            DataRow[] foundRows = piDt.Select("SALE_INVOICE_ID  = '" + txtInvoiceNo.Text.Trim() + "'");

            if (foundRows.Length > 0)
            {
                hfCustomerId.Value = foundRows[0]["CUSTOMER_ID"].ToString();
                hfAssignedDistributorID.Value = foundRows[0]["ASSIGNED_DISTRIBUTOR_ID"].ToString();
                //DrpPayMode.SelectedValue = foundRows[0]["PAYMENT_MODE"].ToString();
                txtGrossAmount.Text = String.Format("{0:0.00}", foundRows[0]["TOTAL_AMOUNT"]);
                numtxtDiscount.Text = String.Format("{0:0.00}", foundRows[0]["EXTRA_DISCOUNT"]);
                numtxtTotalExtraDiscnt.Text = String.Format("{0:0.00}", foundRows[0]["DISCOUNT"]);
                numTxtTotalGST.Text = String.Format("{0:0.00}", foundRows[0]["GST"]);
                txtFreightAmount.Text = String.Format("{0:0.00}", foundRows[0]["freightAmount"]);
                numTxtTotlAmnt.Text = String.Format("{0:0.00}", foundRows[0]["NET_AMOUNT"]);
                txtCashRecieved.Text = String.Format("{0:0.00}", foundRows[0]["CASH_RECIEVED"]);
                txtAuthorisedBy.Text = foundRows[0]["AUTHORISED_PERSON"].ToString();
                lblCustomerName.Text=  foundRows[0]["CUSTOMER_NAME"].ToString(); 
                hfSaleForceId.Value = foundRows[0]["SALE_FORCE"].ToString();
                txtNarration.Text = foundRows[0]["REMARKS"].ToString();
                txtExtraDiscountReturn.Text = String.Format("{0:0.00}", foundRows[0]["EXTRA_DISCOUNT"]);
                txtExtraDiscountReturnAge.Text = String.Format("{0:0.00}", foundRows[0]["EXTRA_DISCOUNT_AGE"]);
                txtGrossReturn.Text = "";
                txtDiscountReturn.Text = "";
                txtExtraDiscountReturn.Text = "";
                txtNetReturn.Text = "";
                txtCashReturn.Text = "";
                txtBalanceReturn.Text = "";
                txtNarration.Text = "";
                LoadDocumentDetail(Convert.ToInt64(txtInvoiceNo.Text.Trim()), 4);
                btnSaveOrder.ToolTip = "Update";
                if (int.Parse(DrpPayMode.SelectedValue) == Constants.CustomerCredit_Id)
                {   
                }
                else
                {
                    txtBalance.Text = Convert.ToString(decimal.Parse(txtCashRecieved.Text) - (decimal.Parse(numTxtTotlAmnt.Text)));
                }
            }
            btnSaveOrder.Enabled = true;
        }
        else
        {
            GrdPurchase.DataSource = null;
            GrdPurchase.DataBind();
            btnSaveOrder.Enabled = false;
            ClearDetail();
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz check your Location or Invoice No!');", true);
        }
    }
    private bool CheckPayType()
    {
        //if (DrpPayMode.SelectedValue == "214")
        //{
        //    if (Convert.ToDecimal(_dc.chkNull_0(txtBalance.Text)) < 0)
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckPayType())
            {

                PurchaseSKU = (DataTable)Session["PurchaseSKU"];

                if (PurchaseSKU.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No product added');", true);

                }
                if (btnSaveOrder.Text == "Update")
                {
                    bool IsValidInsert = _or.Update_SaleReturn(long.Parse(hfInvoiceId.Value),int.Parse(Session["DISTRIBUTOR_ID"].ToString()), txtNarration.Text,
                    long.Parse(hfCustomerId.Value), int.Parse(hfSaleForceId.Value),long.Parse(Session["srId"].ToString()),
                     decimal.Parse(_dc.chkNull_0(txtGrossReturn.Text)), decimal.Parse(_dc.chkNull_0(txtExtraDiscountReturn.Text)),
                     decimal.Parse(_dc.chkNull_0(txtDiscountReturn.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text)),
                     decimal.Parse(_dc.chkNull_0(txtNetReturn.Text)), decimal.Parse(_dc.chkNull_0(txtCashReturn.Text)), DrpPayMode.SelectedValue.ToString(), PurchaseSKU,
                     int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), lblCustomerName.Text,int.Parse(hfAssignedDistributorID.Value));
                    if (IsValidInsert)
                    {
                        //GetInvoice(long.Parse(hfInvoiceId.Value));
                        //GetGatePass(long.Parse(hfInvoiceId.Value));
                        ClearMasterAll();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion Failed!!!.');", true);
                    }
                }
                else
                {
                    
                    long IsValidInsert = _or.Add_SaleReturn(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), txtNarration.Text,
                        long.Parse(hfCustomerId.Value), int.Parse(hfSaleForceId.Value), long.Parse(txtInvoiceNo.Text.Trim()),
                    decimal.Parse(_dc.chkNull_0(txtGrossReturn.Text)), decimal.Parse(_dc.chkNull_0(txtExtraDiscountReturn.Text)),
                    decimal.Parse(_dc.chkNull_0(txtDiscountReturn.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text)),
                    decimal.Parse(_dc.chkNull_0(txtNetReturn.Text)), decimal.Parse(_dc.chkNull_0(txtCashReturn.Text)), DrpPayMode.SelectedValue.ToString(), PurchaseSKU,
                    int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), lblCustomerName.Text, int.Parse(hfAssignedDistributorID.Value));
                    if (IsValidInsert > 0)
                    {

                        //GetInvoice(IsValidInsert);
                        //GetGatePass(IsValidInsert);
                        ClearMasterAll();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion Failed!!!.');", true);
                    }

                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Balance should be zero');", true);
                txtCashReturn.Focus();
            }

        }
        catch (Exception eee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
        }
    }

    #endregion
   
    #region PrintINvoice

    protected void GetInvoice(long returnId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();

            DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));

            RptSaleController rptSaleCtl = new RptSaleController();
            CrpSaleReturnDocument crpReport = new CrpSaleReturnDocument();
            DataSet ds = null;

            ds = rptSaleCtl.SelectSaleReport(returnId, 1);

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpReport.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //


            CrpCustomerReturnDocument crpCustomer = new CrpCustomerReturnDocument();
            crpCustomer.SetDataSource(ds);
            crpCustomer.Refresh();

            crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpCustomer.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //---------------------------------------------------------------------------------\\
            CrpFinanceReturnDocument crpFinance = new CrpFinanceReturnDocument();
            crpFinance.SetDataSource(ds);
            crpFinance.Refresh();

            crpFinance.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpFinance.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpFinance.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

            Session.Add("crpCustomer", crpCustomer);
            Session.Add("ReportTypeCustomer", 0);

            const string url2 = "'RptGatePassCustomer.aspx'";
            Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);

            const string url = "'Default.aspx'";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");


            Session.Add("crpFinance", crpFinance);
            Session.Add("ReportTypeFinance", 0);

            const string url3 = "'RptGatePassFinance.aspx'";
            Response.Write("<script>window.open(" + url3 + ",'_blank');</script>");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void GetGatePass(long returnId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            RptSaleController rptSaleCtl = new RptSaleController();

            DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));


            DataSet ds = null;

            ds = rptSaleCtl.SelectSaleReport(returnId, 2);

            DataTable table = ds.Tables["spGetSaleReturnDocument"];
            if (table.Rows.Count <= 0) return;

            CrpGatePassReturn crpWarehouse = new CrpGatePassReturn();
            crpWarehouse.SetDataSource(ds);
            crpWarehouse.Refresh();

            crpWarehouse.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            crpWarehouse.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpWarehouse.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //---------------------------------------------------------------------------------\\

            //---------------------------------------------------------------------------------\\


            Session.Add("CrpReport2", crpWarehouse);
            Session.Add("ReportType2", 0);

            const string url = "'Default2.aspx'";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");



        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

   
    #endregion

    private void ClearMasterAll()
    {
        try
        {

            Session.Remove("PurchaseSKU");

            Server.Transfer("frmSaleReturnView.aspx?LevelType=3&LevelID=156");

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    private void ClearDetail()
    {
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        txtAuthorisedBy.Text = "";
        numtxtDiscount.Text = "";
        numTxtTotlAmnt.Text = "";
        txtCashRecieved.Text = "";
        txtBalance.Text = "";


        txtGrossReturn.Text = "";
        txtDiscountReturn.Text = "";
        txtExtraDiscountReturn.Text = "";
        txtNetReturn.Text = "";
        txtCashReturn.Text = "";
        txtBalanceReturn.Text = "";
        txtNarration.Text = "";
    }
}