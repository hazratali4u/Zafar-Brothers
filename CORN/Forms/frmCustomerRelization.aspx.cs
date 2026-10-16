using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For Bank Transaction
/// </summary>
public partial class Forms_frmCustomerRelization : System.Web.UI.Page
{

    readonly LedgerController LController = new LedgerController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CORNCommon.Classes.Configuration.GetAccountHead();
            LoadDistributor();
            LoadPrincipal();
            
           // LoadDeliveryman();
          //  LoadArea();
            LoadData();
            LoadAccountHead();
            LoadGrid();
            SelectCreditInvoice();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SetTableSorter();
        }
    }
    #region Load

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadData()
    {
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();

            if (DrpAccountType.SelectedIndex == 0 || DrpAccountType.SelectedIndex == 5)
            {

                DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4, true);
               // DrpRoute.Enabled = true;
            }
            else
            {
                LedgerController LedgerCtl = new LedgerController();
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(DrpCustomer, dtCredit, 0, 1, true);
                //DrpRoute.Enabled = false;
            }
        }
    }
    
    private void LoadGrid()
    {
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();
        if (drpDistributor.Items.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt1 = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt1.Rows.Count > 0)
            {
                DateTime MWorkDate = DateTime.Parse(dt1.Rows[0]["CLOSING_DATE"].ToString());

                DataTable dt = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToInt32(DrpAccountType.SelectedValue),
                MWorkDate,MWorkDate);
                GrdOrder.DataSource = dt;
                GrdOrder.DataBind();
            }
        }
        
    }
    
    private void LoadAccountHead()
    {
        DrpAccountDetail.Items.Clear();
        if (DrpAccountType.SelectedIndex == 0)
        
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, Convert.ToInt64(CORNCommon.Classes.Configuration.CashDefaultType), Convert.ToInt32(drpDistributor.SelectedValue));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);

        }
        else if (DrpAccountType.SelectedIndex == 1)
        {
            DrpAccountDetail.Items.Clear();
            DrpAccountDetail.Items.Add(new ListItem("Credit Transfer Out", "913"));
        }
        else if (DrpAccountType.SelectedIndex == 2 || DrpAccountType.SelectedIndex == 3)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, long.Parse(CORNCommon.Classes.Configuration.BankDefaultType), Convert.ToInt32(drpDistributor.SelectedValue));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dtHead, 0, 4, true);
        }
        else
        {
            DrpAccountDetail.Items.Clear();
        }

    }

    private void LoadPrincipal()
    {
        DrpPrincipal.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

                SKUPriceDetailController PController = new SKUPriceDetailController();
                DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, MWorkDate);
                clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
            }
        }
    }

    private void LoadSaleFoceCash()
    {
        gvSaleForceCash.DataSource = null;
        gvSaleForceCash.DataBind();
        DistributorController mDayClose = new DistributorController();
        DataTable dt1 = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt1.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt1.Rows[0]["CLOSING_DATE"].ToString());

            SaleForceController mSaleForce = new SaleForceController();
            gvSaleForceCash.DataSource = null;
            gvSaleForceCash.DataBind();

            DataTable dt = mSaleForce.GetSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), 0, Constants.IntNullValue, MWorkDate, MWorkDate);
            gvSaleForceCash.DataSource = dt;
            gvSaleForceCash.DataBind();
            gvSaleForceCash.Visible = true;
            GrdOrder.Visible = false;
        }
    }

    private void SelectCreditInvoice()
    {
        LedgerController CDC = new LedgerController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0)
        {
            DataTable dtCredit = CDC.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), 0);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }

    private void SaleForceCashReceived()
    {
        try
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

                SaleForceController mSaleForce = new SaleForceController();
                Convert.ToDecimal(txtAmount.Text);
                mSaleForce.InsertSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), 0, Constants.IntNullValue, MWorkDate, Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(Session["UserId"]));
            }
            else{
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Amount must be decimal')", true);
        }
    }

    #endregion
    private void CashRealization()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());


            string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            Session.Add("VoucherNo", MaxDocumentID);
            decimal OfferAmount = decimal.Parse(txtAmount.Text);
            decimal realizeAmount = 0;
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    DataTable dtLedger = CreateTable();

                    //Credit from Account Receivable (Party Wise)
                    DataRow drLedger = dtLedger.NewRow();
                    drLedger["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                    drLedger["VOUCHER_NO"] = MaxDocumentID;
                    drLedger["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
                    drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        if (decimal.Parse(dr.Cells[3].Text) == OfferAmount)
                        {
                            if (dr.Cells[4].Text == "219")
                            {
                                drLedger["PayeesName"] = "Clear";
                            }
                            else
                            {
                                drLedger["PayeesName"] = "";
                            }
                        }
                        else
                        {
                            drLedger["PayeesName"] = "";
                        }
                        drLedger["DEBIT"] = 0;
                        drLedger["CREDIT"] = OfferAmount;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        if (dr.Cells[4].Text == "219")
                        {
                            drLedger["PayeesName"] = "Clear";
                        }
                        else
                        {
                            drLedger["PayeesName"] = "";
                        }
                        drLedger["DEBIT"] = 0;
                        drLedger["CREDIT"] = dr.Cells[3].Text;
                    }
                    drLedger["Ledger_Date"] = MWorkDate.ToString();
                    drLedger["Remarks"] = txtRemarks.Text;
                    drLedger["TimeStamp"] = DateTime.Now;
                    drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                    drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                    drLedger["Cheque_NO"] = txtChequeNo.Text;
                    drLedger["UserID"] = Session["UserId"].ToString();
                    drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                    drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                    drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                    drLedger["SlipNo"] = txtSlipNo.Text;
                    drLedger["ChequeDate"] = Constants.DateNullValue;
                    drLedger["PaymentMode"] = DrpAccountType.SelectedValue;

                    dtLedger.Rows.Add(drLedger);

                    //Debit to selected Account Head
                    DataRow drLedger2 = dtLedger.NewRow();
                    drLedger2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                    drLedger2["VOUCHER_NO"] = MaxDocumentID;
                    drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                    drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;

                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        if (decimal.Parse(dr.Cells[3].Text) == OfferAmount)
                        {
                            if (dr.Cells[4].Text == "219")
                            {
                                drLedger2["PayeesName"] = "Clear";
                            }
                            else
                            {
                                drLedger2["PayeesName"] = "";
                            }
                        }
                        else
                        {
                            drLedger2["PayeesName"] = "";
                        }
                        drLedger2["DEBIT"] = OfferAmount;
                        drLedger2["CREDIT"] = 0;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        if (dr.Cells[4].Text == "219")
                        {
                            drLedger2["PayeesName"] = "Clear";
                        }
                        else
                        {
                            drLedger2["PayeesName"] = "";
                        }
                        drLedger2["DEBIT"] = dr.Cells[3].Text;
                        drLedger2["CREDIT"] = 0;
                    }

                    drLedger2["Ledger_Date"] = MWorkDate.ToString();
                    drLedger2["Remarks"] = txtRemarks.Text;
                    drLedger2["TimeStamp"] = DateTime.Now;
                    drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                    drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                    drLedger2["Cheque_NO"] = txtChequeNo.Text;
                    drLedger2["UserID"] = Session["UserId"].ToString();
                    drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                    drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                    drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                    drLedger2["SlipNo"] = txtSlipNo.Text;
                    drLedger2["ChequeDate"] = Constants.DateNullValue;
                    drLedger2["PaymentMode"] = DrpAccountType.SelectedValue;

                    dtLedger.Rows.Add(drLedger2);

                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        realizeAmount += OfferAmount;
                        OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                        LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);

                        break;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        realizeAmount += Convert.ToDecimal(dr.Cells[3].Text);
                        OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                        LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                    }

                }
            }
            if (realizeAmount < decimal.Parse(txtAmount.Text))
            {
                DataTable dtLedger = CreateTable();

                //Credit from Account Receivable (Party Wise)
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger["VOUCHER_NO"] = MaxDocumentID;
                drLedger["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                drLedger["DEBIT"] = 0;
                drLedger["CREDIT"] = OfferAmount;
                drLedger["Ledger_Date"] = MWorkDate.ToString();
                drLedger["Remarks"] = txtRemarks.Text + " Advance";
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = Session["UserId"].ToString();
                drLedger["Document_ID"] = 0;
                drLedger["Manual_Document_ID"] = "0";
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = Constants.Cash_Relization;
                //drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger2["VOUCHER_NO"] = MaxDocumentID;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                drLedger2["DEBIT"] = OfferAmount;
                drLedger2["CREDIT"] = 0;
                drLedger2["Ledger_Date"] = MWorkDate.ToString();
                drLedger2["Remarks"] = txtRemarks.Text + " Advance";
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = Session["UserId"].ToString();
                drLedger2["Document_ID"] = 0;
                drLedger2["Manual_Document_ID"] = "0";
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = Constants.Cash_Relization;
                // drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                LController.PostingCash_Bank_Account(dtLedger);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void CashAdvance()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());


            string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            Session.Add("VoucherNo", MaxDocumentID);
            DataTable dtLedger = CreateTable();

            //Credit from Account Receivable (Party Wise)
            DataRow dr = dtLedger.NewRow();

            dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
            dr["VOUCHER_NO"] = MaxDocumentID;
            dr["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
            dr["Distributor_ID"] = drpDistributor.SelectedValue;
            dr["DEBIT"] = 0;
            dr["CREDIT"] = txtAmount.Text;
            dr["Ledger_Date"] = MWorkDate.ToString();
            dr["Remarks"] = txtRemarks.Text;
            dr["TimeStamp"] = DateTime.Now;
            dr["Customer_ID"] = DrpCustomer.SelectedValue;
            dr["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr["Cheque_NO"] = txtChequeNo.Text;
            dr["UserID"] = Session["UserId"].ToString();
            dr["Document_ID"] = Constants.LongNullValue;
            dr["Manual_Document_ID"] = null;
            dr["DocumentTypeID"] = Constants.IntNullValue;
            dr["SlipNo"] = txtSlipNo.Text;
            dr["ChequeDate"] = Constants.DateNullValue;
            dr["PaymentMode"] = DrpAccountType.SelectedValue;
            dr["PayeesName"] = "";
            dtLedger.Rows.Add(dr);

            //Debit to selected Account Head
            DataRow dr2 = dtLedger.NewRow();

            dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
            dr2["VOUCHER_NO"] = MaxDocumentID;
            dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
            dr2["Distributor_ID"] = drpDistributor.SelectedValue;
            dr2["DEBIT"] = txtAmount.Text;
            dr2["CREDIT"] = 0;
            dr2["Ledger_Date"] = MWorkDate.ToString();
            dr2["Remarks"] = txtRemarks.Text;
            dr2["TimeStamp"] = DateTime.Now;
            dr2["Customer_ID"] = DrpCustomer.SelectedValue;
            dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr2["Cheque_NO"] = txtChequeNo.Text;
            dr2["UserID"] = Session["UserId"].ToString();
            dr2["Document_ID"] = Constants.LongNullValue;
            dr2["Manual_Document_ID"] = null;
            dr2["DocumentTypeID"] = Constants.IntNullValue;
            dr2["SlipNo"] = txtSlipNo.Text;
            dr2["ChequeDate"] = Constants.DateNullValue;
            dr2["PaymentMode"] = DrpAccountType.SelectedValue;
            dr2["PayeesName"] = "";
            dtLedger.Rows.Add(dr2);

            LController.PostingCash_Bank_Account(dtLedger);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void BankDeposit(string p_SaleForceID)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            DataTable dtLedger = CreateTable();

            //Credit from NCS
            DataRow dr = dtLedger.NewRow();
            dr["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
            dr["VOUCHER_NO"] = MaxDocumentID;
            dr["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.CashDefault;
            dr["Distributor_ID"] = drpDistributor.SelectedValue;
            dr["DEBIT"] = 0;
            dr["CREDIT"] = txtAmount.Text;
            dr["Ledger_Date"] = MWorkDate.ToString();
            dr["Remarks"] = txtRemarks.Text;
            dr["TimeStamp"] = DateTime.Now;
            dr["Customer_ID"] = Constants.IntNullValue;
            dr["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr["Cheque_NO"] = txtChequeNo.Text;
            dr["UserID"] = Session["UserId"].ToString();
            dr["Document_ID"] = Constants.LongNullValue;
            dr["Manual_Document_ID"] = null;
            dr["DocumentTypeID"] = Constants.IntNullValue;
            dr["SlipNo"] = txtSlipNo.Text;
            dr["ChequeDate"] = Constants.DateNullValue;
            dr["PaymentMode"] = 22;
            dr["PayeesName"] = p_SaleForceID;
            dtLedger.Rows.Add(dr);

            //Debit to selected Account Head
            DataRow dr2 = dtLedger.NewRow();
            dr2["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
            dr2["VOUCHER_NO"] = MaxDocumentID;
            dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
            dr2["Distributor_ID"] = drpDistributor.SelectedValue;
            dr2["DEBIT"] = txtAmount.Text;
            dr2["CREDIT"] = 0;
            dr2["Ledger_Date"] = MWorkDate.ToString();
            dr2["Remarks"] = txtRemarks.Text;
            dr2["TimeStamp"] = DateTime.Now;
            dr2["Customer_ID"] = Constants.IntNullValue;
            dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr2["Cheque_NO"] = txtChequeNo.Text;
            dr2["UserID"] = Session["UserId"].ToString();
            dr2["Document_ID"] = Constants.LongNullValue;
            dr2["Manual_Document_ID"] = null;
            dr2["DocumentTypeID"] = Constants.IntNullValue;
            dr2["SlipNo"] = txtSlipNo.Text;
            dr2["ChequeDate"] = Constants.DateNullValue;
            dr2["PaymentMode"] = 22;
            dr2["PayeesName"] = p_SaleForceID;
            dtLedger.Rows.Add(dr2);

            LController.PostingCash_Bank_Account(dtLedger);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void PettyCash()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));

            LController.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(MaxDocumentId), long.Parse(CORNCommon.Classes.Configuration.CashDefault), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                     MWorkDate, txtRemarks.Text, DateTime.Now, Constants.IntNullValue, Constants.IntNullValue,
                     txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, int.Parse(DrpAccountType.SelectedValue.ToString()), "");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }     
    private void IncomeTax()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            decimal OfferAmount = decimal.Parse(txtAmount.Text);
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    DataTable dtLedger = CreateTable();

                    //Credit from Account Receivable (Party Wise)
                    DataRow drLedger = dtLedger.NewRow();
                    drLedger["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                    drLedger["VOUCHER_NO"] = MaxDocumentID;
                    drLedger["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
                    drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        drLedger["DEBIT"] = 0;
                        drLedger["CREDIT"] = OfferAmount;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        drLedger["DEBIT"] = 0;
                        drLedger["CREDIT"] = dr.Cells[3].Text;
                    }
                    drLedger["Ledger_Date"] = MWorkDate.ToString();
                    drLedger["Remarks"] = txtRemarks.Text;
                    drLedger["TimeStamp"] = DateTime.Now;
                    drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                    drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                    drLedger["Cheque_NO"] = txtChequeNo.Text;
                    drLedger["UserID"] = Session["UserId"].ToString();
                    drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                    drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                    drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                    drLedger["SlipNo"] = txtSlipNo.Text;
                    drLedger["ChequeDate"] = Constants.DateNullValue;
                    drLedger["PaymentMode"] = DrpAccountType.SelectedValue;
                    //  drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                    dtLedger.Rows.Add(drLedger);

                    //Debit to selected Account Head
                    DataRow drLedger2 = dtLedger.NewRow();
                    drLedger2["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                    drLedger2["VOUCHER_NO"] = MaxDocumentID;
                    drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                    drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        drLedger2["DEBIT"] = OfferAmount;
                        drLedger2["CREDIT"] = 0;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        drLedger2["DEBIT"] = dr.Cells[3].Text;
                        drLedger2["CREDIT"] = 0;
                    }

                    drLedger2["Ledger_Date"] = MWorkDate.ToString();
                    drLedger2["Remarks"] = txtRemarks.Text;
                    drLedger2["TimeStamp"] = DateTime.Now;
                    drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                    drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                    drLedger2["Cheque_NO"] = txtChequeNo.Text;
                    drLedger2["UserID"] = Session["UserId"].ToString();
                    drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                    drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                    drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                    drLedger2["SlipNo"] = txtSlipNo.Text;
                    drLedger2["ChequeDate"] = Constants.DateNullValue;
                    drLedger2["PaymentMode"] = DrpAccountType.SelectedValue;
                    //  drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                    dtLedger.Rows.Add(drLedger2);

                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                        LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                        LController.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                    }
                    break;
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void InsertGL()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            CORNCommon.Classes.Configuration.GetAccountHead();

            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
            dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
            dtVoucher.Columns.Add("DEBIT", typeof(decimal));
            dtVoucher.Columns.Add("CREDIT", typeof(decimal));
            dtVoucher.Columns.Add("REMARKS", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
            // dr["REMARKS"] = DrpAccountDetail.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
            dr["REMARKS"] = "Received from " + DrpCustomer.SelectedItem.Text;

            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr);
            ///Credit Side Entry
            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = 0;
            dr1["REMARKS"] = "Received from " + DrpCustomer.SelectedItem.Text;
            dtVoucher.Rows.Add(dr1);


            string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), MWorkDate);

            //Using Payee's Name for reference between Ledger and GL

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Cash_Voucher,MWorkDate, Constants.Cash_Relization, Session["VoucherNo"].ToString(), "Default Voucher, " + txtRemarks.Text, MWorkDate, txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue);
            PrintVoucher(MaxDocumentId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void InsertGL2()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            CORNCommon.Classes.Configuration.GetAccountHead();

            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
            dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
            dtVoucher.Columns.Add("DEBIT", typeof(decimal));
            dtVoucher.Columns.Add("CREDIT", typeof(decimal));
            dtVoucher.Columns.Add("REMARKS", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = Constants.BadDebtsExpense;
            // dr["REMARKS"] = DrpAccountDetail.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
            dr["REMARKS"] = "Received from " + DrpCustomer.SelectedItem.Text;

            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr);
            // Credit Side Entry
            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = 0;
            dr1["REMARKS"] = "Received from " + DrpCustomer.SelectedItem.Text;
            dtVoucher.Rows.Add(dr1);


            string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), MWorkDate);

            //Using Payee's Name for reference between Ledger and GL

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Cash_Voucher, MWorkDate, Constants.Cash_Relization, Session["VoucherNo"].ToString(), "Default Voucher, " + txtRemarks.Text, MWorkDate, txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue);
            PrintVoucher(MaxDocumentId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    private void AdvanceReturn()
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());


            string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            DataTable dtLedger = CreateTable();

            //Debit to Account Receivable (Party Wise)
            DataRow dr = dtLedger.NewRow();
            dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
            dr["VOUCHER_NO"] = MaxDocumentID;
            dr["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
            dr["Distributor_ID"] = drpDistributor.SelectedValue;
            dr["DEBIT"] = txtAmount.Text;
            dr["CREDIT"] = 0;
            dr["Ledger_Date"] = MWorkDate.ToString();
            dr["Remarks"] = txtRemarks.Text;
            dr["TimeStamp"] = DateTime.Now;
            dr["Customer_ID"] = DrpCustomer.SelectedValue;
            dr["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr["Cheque_NO"] = txtChequeNo.Text;
            dr["UserID"] = Session["UserId"].ToString();
            dr["Document_ID"] = Constants.LongNullValue;
            dr["Manual_Document_ID"] = null;
            dr["DocumentTypeID"] = Constants.IntNullValue;
            dr["SlipNo"] = txtSlipNo.Text;
            dr["ChequeDate"] = Constants.DateNullValue;
            dr["PaymentMode"] = 29;
            dr["PayeesName"] = "";
            dtLedger.Rows.Add(dr);

            //Credit from selected Account Head
            DataRow dr2 = dtLedger.NewRow();
            dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
            dr2["VOUCHER_NO"] = MaxDocumentID;
            dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
            dr2["Distributor_ID"] = drpDistributor.SelectedValue;
            dr2["DEBIT"] = 0;
            dr2["CREDIT"] = txtAmount.Text;
            dr2["Ledger_Date"] = MWorkDate.ToString();
            dr2["Remarks"] = txtRemarks.Text;
            dr2["TimeStamp"] = DateTime.Now;
            dr2["Customer_ID"] = DrpCustomer.SelectedValue;
            dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
            dr2["Cheque_NO"] = txtChequeNo.Text;
            dr2["UserID"] = Session["UserId"].ToString();
            dr2["Document_ID"] = Constants.LongNullValue;
            dr2["Manual_Document_ID"] = null;
            dr2["DocumentTypeID"] = Constants.IntNullValue;
            dr2["SlipNo"] = txtSlipNo.Text;
            dr2["ChequeDate"] = Constants.DateNullValue;
            dr2["PaymentMode"] = 29;
            dr2["PayeesName"] = "";
            dtLedger.Rows.Add(dr2);

            LController.PostingCash_Bank_Account(dtLedger);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Distributor Working Date Not found !')", true);
        }
    }
    #region Sel/Index Change

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            //LoadArea();
            LoadData();
            LoadAccountHead();
            LoadGrid();
            SelectCreditInvoice();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
        else if (DrpAccountType.SelectedIndex == 7)
        {
            LoadData();
            LoadAccountHead();
            LoadSaleFoceCash();
        }
       // LoadDeliveryman();
        SetTableSorter();
    }

    protected void DrpAccountDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
        SetTableSorter();
    }

    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex == 2)
        {
            HideShowControls(false);
        }
        else
        {
            HideShowControls(true);
        }
        LoadAccountHead();
        LoadGrid();
        gvSaleForceCash.Visible = false;
        GrdOrder.Visible = true;
        SetTableSorter();
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        SetTableSorter();
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadData();
            SelectCreditInvoice();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
        else
        {
            LoadSaleFoceCash();
        }
        SetTableSorter();
    }

    protected void DrpDeliveryMan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex == 7)
        {
            LoadSaleFoceCash();
        }
        else
        {
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
    }

    #endregion
    private DataTable CreateTable()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(string));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Update")
        {
            SaleForceController mSaleForce = new SaleForceController();
            mSaleForce.DeleteSaleForceCash(Convert.ToInt32(hfSALE_FORCE_CASH_ID.Value), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPRINCIPAL_ID.Value), Convert.ToInt32(hfDELIVERYMAN_ID.Value));
        }

        if (DrpAccountType.SelectedIndex == 0 || DrpAccountType.SelectedIndex == 3)
        {
            int InvoiceCount = Constants.IntNullValue;
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    InvoiceCount++;
                    break;
                }
            }

            if (InvoiceCount == Constants.IntNullValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Must Select Invoice');", true);
                return;
            }
            else
            {
                CashRealization();
                InsertGL();
                Session.Remove("VoucherNo");
            }
            SelectCreditInvoice();
        }
        else if (DrpAccountType.SelectedIndex == 2)
        {
            BankDeposit("");
           // InsertGL();
        }
        else if (DrpAccountType.SelectedIndex == 1)
        {
            int InvoiceCount = Constants.IntNullValue;
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    InvoiceCount++;
                    break;
                }
            }

            if (InvoiceCount == Constants.IntNullValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Must Select Invoice');", true);
                return;
            }
            else
            {
                CashRealization();
                InsertGL2();
                Session.Remove("VoucherNo");
            }
            SelectCreditInvoice();
        }
        ClearAll();
        LoadGrid();
        SetTableSorter();
    }
    #region Grid Operations

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataControl dc = new DataControl();

        LController.DeleteCashBankTransction(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[10].Text.Replace("&nbsp;", ""))), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)));
        LoadGrid();
        SelectCreditInvoice();
        SetTableSorter();
    }
    /// <summary>
    /// Deletes Sale Force Cash
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSaleForceCash_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaleForceController mSaleForce = new SaleForceController();

        if (mSaleForce.DeleteSaleForceCash(Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[0].Text),
            Convert.ToInt32(drpDistributor.SelectedValue), 0, Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[3].Text)))
        {
            LoadSaleFoceCash();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured. Cash received not deleted.');", true);
        }
        SetTableSorter();
    }

    /// <summary>
    /// Sets Sale Force Cash For Edit. This Function Runs When An Existing Sale Force Cash Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSaleForceCash_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // DrpPrincipal.SelectedValue = gvSaleForceCash.Rows[e.NewEditIndex].Cells[1].Text;
       // DrpDeliveryMan.SelectedValue = gvSaleForceCash.Rows[e.NewEditIndex].Cells[3].Text;
        txtAmount.Text = gvSaleForceCash.Rows[e.NewEditIndex].Cells[6].Text;

        hfSALE_FORCE_CASH_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[0].Text;
        hfPRINCIPAL_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[1].Text;
        hfDELIVERYMAN_ID.Value = gvSaleForceCash.Rows[e.NewEditIndex].Cells[3].Text;

        btnSave.Text = "Update";
        SetTableSorter();
    }

    #endregion
    private void HideShowControls(bool Visible)
    {
        if (DrpAccountType.SelectedIndex == 2)
        {
            GrdCredit.DataSource = null;
            GrdCredit.DataBind();
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = true;
        }
        else
        {
            
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = Visible;
            Label5.Enabled = Visible;
            Label3.Enabled = Visible;
            txtChequeNo.Enabled = Visible;
            txtSlipNo.Enabled = Visible;
            Label9.Enabled = Visible;
            txtRemarks.Enabled = Visible;
            SelectCreditInvoice();
         
        }
    }
    private void SetTableSorter()
    {
        if (GrdOrder.Rows.Count > 1)
        {
            GrdOrder.UseAccessibleHeader = true;
            GrdOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvSaleForceCash.Rows.Count > 1)
        {
            gvSaleForceCash.UseAccessibleHeader = true;
            gvSaleForceCash.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtSlipNo.Text = "";
        btnSave.Text = "Save";
    }
    private void PrintVoucher(string VoucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        crpVoucherView CrpReport = new crpVoucherView();
        int VoucherType = Constants.Cash_Voucher;

        //if (DrpVoucherType.SelectedValue == "16")
        //{
        //    VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
        //}
        //else
        //{
        //    VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
        //}

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), VoucherNo, VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", "Cash Voucher");
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

}