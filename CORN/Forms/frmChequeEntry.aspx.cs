using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form To Add, Edit Cheques
/// </summary>
public partial class Forms_frmChequeEntry : System.Web.UI.Page
{

    readonly LedgerController LController = new LedgerController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpStatus.Items.Add(new ListItem("Cheque Received", "527"));
            DrpStatus.Items.Add(new ListItem("Cheque Deposit", "528"));
            DrpStatus.Items.Add(new ListItem("Cheque Realize", "529"));
            DrpStatus.Items.Add(new ListItem("Cheque Bounce", "530"));
            DrpStatus.Items.Add(new ListItem("Cheque Cancel", "560"));
            
            LoadPrincipal();
            LoadDistributor();
            LoadAccountHead();
            
            LoadData();
            SelectCreditInvoice();
            LoadReceviedCheque();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");

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
        CustomerDataController mController = new CustomerDataController();

        if (DrpChequeType.SelectedIndex == 1)
        {
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dtCustomer, 0, 4, true);
            // DrpRoute.Enabled = true;
        }
        else
        {
            if (drpDistributor.Items.Count > 0)
            {
                LedgerController LedgerCtl = new LedgerController();
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(DrpCustomer, dtCredit, 0, 1, true);

                //  DrpRoute.Enabled = false;
            }
        }
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
    }

    private void LoadReceviedCheque()
    {
        if (DrpStatus.SelectedValue.ToString() != Constants.Cheque_Clear.ToString())
        {
            ChequeEntryController CController = new ChequeEntryController();
            DataTable dt = CController.SelectChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, DrpChequeType.SelectedIndex);
            Session.Add("dt", dt);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
        else
        {
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();
        }
    }

    private void SelectCreditInvoice()
    {
        LedgerController CDC = new LedgerController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 && DrpChequeType.SelectedIndex != 1)
        {
            DataTable dtCredit = CDC.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), 0);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }

    private void LoadAccountHead()
    {
        CORNCommon.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt= mAccountController.SelectAccountHeadLocation(Constants.IntNullValue, long.Parse(CORNCommon.Classes.Configuration.BankDefaultType), Convert.ToInt32(drpDistributor.SelectedValue));
        clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);
    }

    #endregion

    private void ChequeRealization()
    {
        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        Session.Add("VoucherNo", MaxDocumentId);
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;//For Advance
        

        if (DrpChequeType.SelectedIndex == 0)
        {
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                    {
                        realizeAmount += OfferAmount;

                        if (decimal.Parse(dr.Cells[3].Text) == OfferAmount)
                        {
                            if (dr.Cells[4].Text == "219")
                            {
                                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                               DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                               txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "Clear");

                                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                                DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                                txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "Clear");
                            }
                            else
                            {
                                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                                                         DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                                                         txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                                DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                                txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");
                       
                            }
                        }
                        else
                        {
                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");
                        }
                        OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;
                        LController.UpdateSaleInvoice(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);
                      
                        break;
                    }
                    else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                    {
                        realizeAmount += decimal.Parse(dr.Cells[3].Text);


                        if (dr.Cells[4].Text == "219")
                        {
                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dr.Cells[3].Text),
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "Clear");

                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dr.Cells[3].Text), 0,
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "Clear");
                        }

                        else
                        {
                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dr.Cells[3].Text),
                           DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                           txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dr.Cells[3].Text), 0,
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), dr.Cells[1].Text, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                        }
                        OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                        LController.UpdateSaleInvoice(long.Parse(dr.Cells[1].Text), int.Parse(drpDistributor.SelectedValue.ToString()), 0);

                        
                    }
                }
            }
            if (realizeAmount < decimal.Parse(txtAmount.Text))
            {
                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                         DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                         txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Realization (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

            }
        }
        else //Cheque Advance
        {
            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Advance", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, 20, "");

            LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Cheque Advance", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, 20, "");
        }

    }
    private void InsertGL()
    {
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
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedValue);
        dr["REMARKS"] ="Received from " + DrpCustomer.SelectedItem.Text;
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

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

        //Using Payee's Name for reference between Ledger and GL

        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Bank_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Default Voucher, "+txtRemarks.Text, Convert.ToDateTime(Session["CurrentWorkDate"]), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue);
        PrintVoucher(MaxDocumentId);
    }

    #region Sel/Index Change

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadArea();
        LoadData();
        SelectCreditInvoice();
        LoadReceviedCheque();
        LoadAccountHead();
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        SelectCreditInvoice();
        LoadReceviedCheque();
    }

    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            LoadReceviedCheque();
        }
    }

    protected void DrpChequeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        LoadReceviedCheque();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        SelectCreditInvoice();
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            ChequeEntryController CController = new ChequeEntryController();
            DateTime ChequeDate;
            try
            {
                if (txtStartDate.Text.Length == 10)
                {
                    ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

                }
                else
                {
                    ChequeDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cheque Date Pattern is DD/MM/YYYY.');", true);
                return;
            }
            if (DrpChequeType.SelectedIndex == 0)//on Cash Realization check Invoice Selection
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
            }
            if (btnSave.Text == "Save")
            {
                if (DrpStatus.SelectedIndex == 0)
                {
                    HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedValue.ToString()), 0, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate,
                        DateTime.Parse(txtReceivedDate.Text + " 00:00:00"), Constants.DateNullValue, Constants.DateNullValue, decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Now, DrpChequeType.SelectedIndex, txtSlipNo.Text, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));


                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                }
            }
            else
            {
                if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Pending)
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), int.Parse(drpDistributor.SelectedValue.ToString()), 0, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate,Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue,
                       decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));

                    CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                }
                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Deposit)
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.DateNullValue,
                         Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));
                }
                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Cancel)
                {
                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));

                }

                else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Clear)
                {

                    CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, DrpChequeType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));

                    CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]));
                        }
                    }
                    ChequeRealization();
                    InsertGL();
                    Session.Remove("VoucherNo");
                    LoadData();
                    SelectCreditInvoice();
                }
            }
            ClearAll();
            LoadReceviedCheque();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                break;
        }
        GrdOrder.DataSource = dt.DefaultView;
        GrdOrder.DataBind();
    }

    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ChequeEntryController Ccontroller = new ChequeEntryController();
            HFChqueProcessId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
            //  DrpRoute.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;

            LoadData();

            DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;

            txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
            txtBankName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
            txtStartDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
            txtReceivedDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
            txtReceivedDate.Enabled = false;
            ImgBntToDate.Enabled = false;
            txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
            txtSlipNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;","");
            txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
            DrpBankAccount.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
            try
            {
                //   DrpDeliveryMan.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[14].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Sale Force not found.');", true);
            }

            btnSave.Text = "Update";
            SelectCreditInvoice();

            DataTable dt = Ccontroller.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 0);

            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                foreach (DataRow dbr in dt.Rows)
                {

                    if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]) == Convert.ToInt64(dbr["SALE_INVOICE_ID"]))
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        chRelized.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Invoice not found for selected cheque');", true);
        }
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ChequeEntryController CController = new ChequeEntryController();
        CController.DeleteChequeEntry(long.Parse(GrdOrder.Rows[e.RowIndex].Cells[0].Text));
        LoadReceviedCheque();
    }

    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = "";
        btnSave.Text = "Save";
        txtReceivedDate.Text = "";
        txtSlipNo.Text = "";
        txtRemarks.Text = "";
        txtReceivedDate.Enabled = true;
        ImgBntToDate.Enabled = true;
    }
    private void PrintVoucher(string VoucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        crpVoucherView CrpReport = new crpVoucherView();
        int VoucherType = Constants.Bank_Voucher;

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
        CrpReport.SetParameterValue("VoucherType", "Bank Voucher");
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}