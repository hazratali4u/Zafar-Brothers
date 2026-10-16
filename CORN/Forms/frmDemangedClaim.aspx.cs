using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    readonly LedgerController LController = new LedgerController();
    readonly DataControl dc = new DataControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadClaimType();
            LoadAccountHead();
           
            LoadCustomer();
            LoadGrid();

            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            
        }
    }

    #region Load

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;         
    }
    private void LoadAccountHead()
    {        
        DrpAccountHead.Items.Clear();
        //if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        //{
        //    DrpAccountHead.Items.Add(new ListItem("SECURITY DEPOSIT BOTTLE", "424"));
        //}
        //else if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        //{
        //    DrpAccountHead.Items.Add(new ListItem("Adv. against Empty Bottle", "511"));
        //    DrpAccountHead.Items.Add(new ListItem("Sales Return (Supplier Wise)", "152"));
        //    DrpAccountHead.Items.Add(new ListItem("Sale Discount - Claimable", "493"));
        //}
        #region Commited Code
        
            //AccountHeadController mAccountController = new AccountHeadController();
            //DataTable dt = mAccountController.SelectClaimHead(int.Parse(RbdClaimType.SelectedValue.ToString()));
            //clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 2, true);

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
        #endregion
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();

            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4, true);
        }
        else
        {
            DrpCustomer.Items.Clear();   
        }
    }

    #endregion

    #region Sel/Index Change
   
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {  
        LoadCustomer();
        LoadGrid();
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer(); 
    }
    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadAccountHead();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        LoadGrid();
    }

    #endregion
    private void LoadGrid()
    {
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();
        if (drpDistributor.Items.Count > 0)
        {
            LedgerController LController = new LedgerController();
            DataTable dt = LController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(RbdClaimType.SelectedValue.ToString()),
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        

        int row = e.NewEditIndex;

        DrpCustomer.SelectedValue = GrdOrder.Rows[row].Cells[0].Text;
        hfVoucherType.Value = GrdOrder.Rows[row].Cells[1].Text;
        hfVoucherNo.Value = GrdOrder.Rows[row].Cells[4].Text;
        txtAmount.Text = GrdOrder.Rows[row].Cells[5].Text;
        txtRemarks.Text = GrdOrder.Rows[row].Cells[6].Text.Replace("&nbsp;","");
        DrpAccountHead.SelectedValue= GrdOrder.Rows[row].Cells[7].Text;
        //LoadGrid();
        btnAddNew.Text = "Update";
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       if(!LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[1].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[4].Text)), Constants.LongNullValue  , decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[5].Text))))
       {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');",true);
       }
        
        LoadGrid();
       
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));

            if (btnAddNew.Text == "Save")
            {
                if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
                {
                    LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                       DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                        null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
                    InsertGLDebit(MaxDocumentId);
                }
                else
                {
                    LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                        DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                         null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
                    InsertGLCredit(MaxDocumentId);
                }
            }
            else
            {
                if (LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(hfVoucherType.Value)),
                      int.Parse(dc.chkNull_0(hfVoucherNo.Value)), Constants.LongNullValue, decimal.Parse(dc.chkNull_0(txtAmount.Text))))
                {

                    if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
                    {
                        LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                           DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                            null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
                        InsertGLDebit(MaxDocumentId);
                    }
                    else
                    {
                        LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                            DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                             null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
                        InsertGLCredit(MaxDocumentId);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                }
            }
            txtAmount.Text = "";
            txtRemarks.Text = "";
            btnAddNew.Text = "Save";
            LoadGrid();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');",true);
        }
    }
    private void InsertGLDebit(string vno)
    {
        CORNCommon.Classes.Configuration.GetAccountHead();

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        //   dtVoucher.Columns.Add("VoucherNo",typeof(string));
        DataRow dr = dtVoucher.NewRow();
        dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable;
        // dr["ACCOUNT_HEAD_ID"] = 352;//B3002120004           Cash in hand - accounts
        dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue); //Account receivable party wise// CORNCommon.Classes.Configuration.MainCashSaled_Head;
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);
        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(RbdClaimType.SelectedValue),vno, "Customer Debit Claim Voucher | " + txtRemarks.Text, Convert.ToDateTime(Session["CurrentWorkDate"]), null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue);

    }
    private void InsertGLCredit(string vno)
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
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
        // dr["ACCOUNT_HEAD_ID"] = 352;//B3002120004           Cash in hand - accounts
        dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr1["ACCOUNT_HEAD_ID"] = CORNCommon.Classes.Configuration.AccountReceivable; //Account receivable party wise// CORNCommon.Classes.Configuration.MainCashSaled_Head;
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);
        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(RbdClaimType.SelectedValue), vno, "Customer Credit Claim Voucher | " + txtRemarks.Text, Convert.ToDateTime(Session["CurrentWorkDate"]), null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue);

    }


}
