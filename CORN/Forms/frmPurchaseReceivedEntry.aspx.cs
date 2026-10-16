using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using OnBarcode.Barcode;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseReceivedEntry : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    DataTable _dt = new DataTable();
    readonly SkuController _cn = new SkuController();
    readonly PurchaseController _purchaseCtrl = new PurchaseController();

    readonly RptInventoryController _rptInventoryCtl = new RptInventoryController();
    readonly DocumentPrintController _dPrint = new DocumentPrintController();

    DataSet _orderedSku;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Attributes.Add("readonly", "readonly");
            LoadDistributor();
            GetDocumentNo();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            if (Session["grnId"] != null)
            {
                drpDocumentNo.Enabled = false;
                LoadDocumentDetail2();
            }
            else
            {
                LoadDocumentDetail();
            }
        }
    }

    #region Load

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();

        //  DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, Constants.IntNullValue, 1, Convert.ToDateTime(Session["CURRENTWORKDATE"]));
        DataSet ds = _purchaseCtrl.SelectPurchaseOrderNo(Constants.LongNullValue, 1, Constants.DateNullValue);
        if (ds == null) return;
        drpDocumentNo.Items.Add(new ListItem("-------Select-------", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, ds.Tables[0], 11, 0, false);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        bool flag = true;

        if (drpDocumentNo.Items.Count > 0)
        {

            if (drpDocumentNo.SelectedItem.Text != "-------Select-------")
            {
                var arr = drpDocumentNo.SelectedItem.Text.Split('-');
                var arr2 = drpDocumentNo.SelectedItem.Value.Split('-');
                DataSet dt = _purchaseCtrl.SelectPurchaseOrderNo(Convert.ToInt64(arr[0]), 1, Constants.DateNullValue);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    drpDistributor.SelectedValue = dt.Tables[0].Rows[0]["SOLD_TO"].ToString();
                    hfPrincipalName.Value = dt.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                    txtRemarks.Text = dt.Tables[0].Rows[0]["REMARKS"].ToString();
                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {
                        if (Convert.ToString(arr2[1]) != "")
                        {
                            var arr3 = Convert.ToString(dr["PURCHASE_MASTER_ID"]).Split('-');

                            if (Convert.ToString(arr3[1]) == Convert.ToString(arr2[1]))
                            {

                                hf_ID.Value = Convert.ToString(arr3[1]);
                                hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Tables[0].Rows[0]["SOLD_FROM"].ToString()));

                                _orderedSku = _purchaseCtrl.SelectPurchaseOrderNo(long.Parse(arr3[1]), 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

                            }
                            flag = true;
                            break;
                        }
                        else
                        {
                            hf_ID.Value = Convert.ToString(Constants.LongNullValue);
                            hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Tables[0].Rows[0]["SOLD_FROM"].ToString()));

                            _orderedSku = _purchaseCtrl.SelectPurchaseOrderNo(long.Parse(arr[0]), 0, Constants.DateNullValue);

                            flag = false;

                        }
                    }
                    Session.Add("OrderedSKU", _orderedSku);
                    LoadGird();
                }
            }
        }
    }
    private void LoadDocumentDetail2()
    {

        var arr = Session["grnId"].ToString().Split('-');
        var arr2 = Session["grnId"].ToString().Split('-');

        DataSet dt = _purchaseCtrl.SelectPurchaseOrderNo(Convert.ToInt64(arr[0]), 1, Constants.DateNullValue);

        if (dt.Tables[0].Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Tables[0].Rows[0]["SOLD_TO"].ToString();
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                if (Convert.ToString(arr2[1]) != "")
                {
                    var arr3 = Convert.ToString(dr["PURCHASE_MASTER_ID"]).Split('-');

                    if (Convert.ToString(arr3[1]) == Convert.ToString(arr2[1]))
                    {

                        hf_ID.Value = Convert.ToString(arr3[1]);
                        hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Tables[0].Rows[0]["SOLD_FROM"].ToString()));

                        _orderedSku = _purchaseCtrl.SelectPurchaseOrderNo(long.Parse(arr3[1]), 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

                    }

                    break;
                }
                hf_ID.Value = Convert.ToString(Constants.LongNullValue);
                hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Tables[0].Rows[0]["SOLD_FROM"].ToString()));

                _orderedSku = _purchaseCtrl.SelectPurchaseOrderNo(long.Parse(arr[0]), 0, Constants.DateNullValue);
            }
            Session.Add("OrderedSKU", _orderedSku);
            LoadGird();


        }
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {

        decimal totalValue = 0;
        decimal totalcurqty = 0;
        decimal totalamount = 0;
        _orderedSku = (DataSet)Session["OrderedSKU"];
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        if (hf_ID.Value == "")
        {
            if (_orderedSku.Tables.Count > 0)
            {
                GrdPurchase.DataSource = _orderedSku.Tables[2];
                GrdPurchase.DataBind();
                foreach (DataRow dr in _orderedSku.Tables[2].Rows)
                {

                    totalValue += decimal.Parse(dr["Quantity"].ToString());
                    totalcurqty += decimal.Parse(dr["CurrentRcdQty"].ToString());
                    totalamount += decimal.Parse(dr["Amount"].ToString());

                }
            }
            else
            {
                GrdPurchase.DataSource = null;
                GrdPurchase.DataBind();
            }
        }
        else
        {
            if (_orderedSku.Tables.Count > 0)
            {
                GrdPurchase.DataSource = _orderedSku.Tables[1];
                GrdPurchase.DataBind();
                foreach (DataRow dr in _orderedSku.Tables[1].Rows)
                {

                    totalValue += decimal.Parse(dr["Quantity"].ToString());
                    totalcurqty += decimal.Parse(dr["CurrentRcdQty"].ToString());
                    totalamount += decimal.Parse(dr["Amount"].ToString());

                }
            }
            else
            {
                GrdPurchase.DataSource = null;
                GrdPurchase.DataBind();
            }
        }
        txtTotalQuantity.Text = Math.Round(totalValue).ToString();
        txtTotalCurRecQty.Text = Math.Round(totalcurqty).ToString();
        txtTotalCurRecAmount.Text = Math.Round(totalamount).ToString();
    }

    #endregion

    #region Click OPerations

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DataTable PurchasedPSKU = new DataTable();
        PurchasedPSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        PurchasedPSKU.Columns.Add("RACK_NO", typeof(string));
        PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("TRADE_PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("PURCHASE_PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("Quantity", typeof(decimal));
        PurchasedPSKU.Columns.Add("FREE_SKU", typeof(decimal));
        PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchasedPSKU.Columns.Add("TIME_STAMP", typeof(DateTime));
        PurchasedPSKU.Columns.Add("DATE_EFFECTED", typeof(DateTime));

        decimal OdereQty = 0;
        decimal reciveiveQTY = 0;
        int i = 0;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtRackNo = gvr.FindControl("txtRackNo") as TextBox;
            if (txtRackNo.Text == "")
            {
                txtRackNo.Text = "N/A";
            }
            if (txtCurrentRecQty.Text.Length > 0)
            {
                _orderedSku = (DataSet)Session["OrderedSKU"];
                OdereQty = decimal.Parse(_orderedSku.Tables[1].Rows[i]["Quantity"].ToString());
                reciveiveQTY = Convert.ToDecimal(txtRecQty.Text) + Convert.ToDecimal(txtCurrentRecQty.Text);
                if (reciveiveQTY > OdereQty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Received quantity greater then Order quantity.');", true);
                }
                i += 1;
            }
        }
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtRackNo = gvr.FindControl("txtRackNo") as TextBox;
            TextBox txtPurchasePrice = gvr.FindControl("txtPurchasePrice") as TextBox;
            TextBox txtPurchasePriceNew = gvr.FindControl("txtPurchasePriceNew") as TextBox;
            TextBox txtTradePrice = gvr.FindControl("txtTradePrice") as TextBox;
            TextBox txtMfgDate = gvr.FindControl("txtMfgDate") as TextBox;
            DateTime dtExpDate = Constants.DateNullValue;
            DateTime dtMfgDate = Constants.DateNullValue;

            if (txtMfgDate.Text.Length > 0)
            {
                try
                {
                    dtMfgDate = Convert.ToDateTime(txtMfgDate.Text);
                }
                catch (Exception ex)
                {
                }
            }

            DataRow dr = PurchasedPSKU.NewRow();
            dr["SKU_ID"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[0].Text));
            dr["RACK_NO"] = txtRackNo.Text;
            dr["FREE_SKU"] = Convert.ToDecimal(_dc.chkNull_0(txtCurrentRecQty.Text));
            dr["Quantity"] = Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[4].Text));
            dr["PURCHASE_PRICE"] = Convert.ToDecimal(_dc.chkNull_0(txtPurchasePriceNew.Text));
            dr["PRICE"] = Convert.ToDecimal(_dc.chkNull_0(txtPurchasePrice.Text));
            dr["TRADE_PRICE"] = Convert.ToDecimal(_dc.chkNull_0(txtTradePrice.Text));
            dr["AMOUNT"] = Convert.ToDecimal(_dc.chkNull_0(txtTradePrice.Text)) * Convert.ToDecimal(_dc.chkNull_0(txtCurrentRecQty.Text));
            dr["TIME_STAMP"] = dtExpDate;
            dr["DATE_EFFECTED"] = dtMfgDate;
            PurchasedPSKU.Rows.Add(dr);
        }


        if (PurchasedPSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (IsDayClosed())
                {
                    var userCtl = new UserController();
                    userCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                    Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    long voucherNo = CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()), PurchasedPSKU);
                    if (voucherNo != -2)
                    {
                        LoadDistributor();
                        GetDocumentNo();
                        GrdPurchase.DataSource = null;
                        GrdPurchase.DataBind();
                        ClearAll();
                        hfClosingDate.Value = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('GRN saved successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured.');", true);
                    }
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

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGird();
    }

    #endregion

    private long CalculatePurchase(DateTime mWorkDate, DataTable purchasedPsku)
    {
        hfClosingDate.Value = mWorkDate.ToString();
        decimal mTotalAmount = purchasedPsku.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
        var arr = drpDocumentNo.SelectedItem.Text.Split('-');
        if (Convert.ToInt64(hf_ID.Value) > 0)
        {
            long mResult = _purchaseCtrl.UpdatePurchaseDocument2(long.Parse(hf_ID.Value), int.Parse(drpDistributor.SelectedValue), int.Parse(hf_PID.Value), mTotalAmount, 2
                , mWorkDate, purchasedPsku, 0, int.Parse(Session["UserId"].ToString()), txtRemarks.Text);
            if (mResult == -2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
            }
            else
            {
                Print(mResult);

            }
            return mResult;
        }
        else
        {
            long mResult = _purchaseCtrl.InsertPurchaseDocument2(long.Parse(arr[0]), int.Parse(drpDistributor.SelectedValue), int.Parse(hf_PID.Value), mTotalAmount, 2
            , mWorkDate, false, purchasedPsku, 0, int.Parse(Session["UserId"].ToString()), txtRemarks.Text);
            if (mResult == -2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again!');", true);
            }
            else
            {
                Print(mResult);

            }
            return mResult;

        }
    }

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            _orderedSku = new DataSet();
            Session.Add("OrderedSKU", _orderedSku);
            LoadGird();

        }
        else
        {
            LoadDocumentDetail();

        }
    }


    private bool IsDayClosed()
    {
        //var distrCtl = new DistributorController();
        //DataTable dtDayClose = distrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        //bool flag = Convert.ToDateTime(Session["CurrentWorkDate"]) != Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]);

        //return flag;
        return false;
    }
    private void ClearAll()
    {
        drpDocumentNo.Enabled = true;
        txtTotalQuantity.Text = "";
        txtTotalCurRecAmount.Text = "";
        txtTotalCurRecQty.Text = "";
        Session.Remove("grnId");

    }


    private void Print(long mResult)
    {

        var crpReport = new CrpPurchaseVoucher();
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = _rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, DateTime.Parse(hfClosingDate.Value), DateTime.Parse(hfClosingDate.Value), 2);

        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", "Purchase Voucher");
        crpReport.SetParameterValue("Principal", hfPrincipalName.Value);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport2", crpReport);
        Session.Add("ReportType2", 0);
        const string url = "'Default2.aspx'";
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }

    protected void txtDate_OnTextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtMfgDate = gvr.FindControl("txtMfgDate") as TextBox;

            txtMfgDate.Text = txtDate.Text;

        }
    }
    protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMfgDate = e.Row.FindControl("txtMfgDate") as TextBox;

            if (txtMfgDate.Text == "")
            {
                txtMfgDate.Text = txtDate.Text;
            }
        }

    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        _orderedSku = (DataSet)Session["OrderedSKU"];
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtPurchasePrice = gvr.FindControl("txtPurchasePrice") as TextBox;
            TextBox txtPurchasePriceNew = gvr.FindControl("txtPurchasePriceNew") as TextBox;
            TextBox txtTradePrice = gvr.FindControl("txtTradePrice") as TextBox;

            GridViewRow item = (GridViewRow)txtCurrentRecQty.NamingContainer;
            if (hf_ID.Value == "")
            {
                _orderedSku.Tables[2].Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                _orderedSku.Tables[2].Rows[item.RowIndex]["Amount"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Decimal.Parse(_dc.chkNull_0(txtPurchasePrice.Text)));
                _orderedSku.Tables[2].Rows[item.RowIndex]["DISTRIBUTOR_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtPurchasePrice.Text)));
                _orderedSku.Tables[2].Rows[item.RowIndex]["PURCHASE_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtPurchasePriceNew.Text)));
                _orderedSku.Tables[2].Rows[item.RowIndex]["TRADE_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtTradePrice.Text)));
            }
            else
            {
                _orderedSku.Tables[1].Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                _orderedSku.Tables[1].Rows[item.RowIndex]["Amount"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * Decimal.Parse(_dc.chkNull_0(txtPurchasePrice.Text)));
                _orderedSku.Tables[1].Rows[item.RowIndex]["DISTRIBUTOR_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtPurchasePrice.Text)));
                _orderedSku.Tables[1].Rows[item.RowIndex]["PURCHASE_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtPurchasePriceNew.Text)));
                _orderedSku.Tables[1].Rows[item.RowIndex]["TRADE_PRICE"] = Convert.ToString(decimal.Parse(_dc.chkNull_0(txtTradePrice.Text)));

            }
        }
        Session.Add("OrderedSKU", _orderedSku);
        LoadGird();

    }
    protected void btnGenerateSingle_Click(object sender, EventArgs e)
    {
        _dt = new DataTable();
        _dt.Columns.Add("SKU_ID", typeof(int));
        _dt.Columns.Add("SKU_Code", typeof(string));
        _dt.Columns.Add("BAR_Code", typeof(string));
        _dt.Columns.Add("COLOR", typeof(string));
        _dt.Columns.Add("SKU_Name", typeof(string));
        _dt.Columns.Add("Quantity", typeof(decimal));
        _dt.Columns.Add("PRICE", typeof(decimal));
        _dt.Columns.Add("Code", typeof(byte[]));
        DataRow dr;
        TextBox txtPurchasePrice;
        TextBox txtTradePrice;
        TextBox txtBCodeQty;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            txtPurchasePrice = (TextBox)gvr.FindControl("txtPurchasePrice");
            txtTradePrice = (TextBox)gvr.FindControl("txtTradePrice");
            txtBCodeQty = (TextBox)gvr.FindControl("txtBCodeQty");
            dr = _dt.NewRow();
            dr["SKU_ID"] = gvr.Cells[0].Text;
            dr["SKU_Code"] = gvr.Cells[1].Text;
            dr["BAR_Code"] = gvr.Cells[2].Text;
            dr["COLOR"] = gvr.Cells[13].Text;
            dr["SKU_Name"] = gvr.Cells[3].Text;
            dr["PRICE"] = txtTradePrice.Text;
            dr["Quantity"] = txtBCodeQty.Text;
            _dt.Rows.Add(dr);
        }

        DataTable _insertDt = _dt.Clone();
        foreach (DataRow dr2 in _dt.Rows)
        {
            var oBitmap = Generatecode(dr2["bar_code"].ToString());
            var ms = new MemoryStream();
            oBitmap.Save(ms, ImageFormat.Png);
            var bytearray = ms.ToArray();
            _insertDt.ImportRow(dr2);
            
            if (this.Session["Distributor_Id"].ToString() == "5" || this.Session["Distributor_Id"].ToString() == "6" || this.Session["Distributor_Id"].ToString() == "7")
            {
                _cn.InsertBarcode("SALMAS", bytearray, _insertDt);
            }
            else
            {
                _cn.InsertBarcode("ZBG", bytearray, _insertDt);
            }
            _insertDt.Rows.Clear();
        }

        var sk = new SkuController();
        _dt = sk.SelectSkuBarcode();
        var crpReport = new CrpBarcodeSingle();
        crpReport.SetDataSource(_dt);
        crpReport.Refresh();
        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        _dt = new DataTable();
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        _dt = new DataTable();
        _dt.Columns.Add("SKU_ID", typeof(int));
        _dt.Columns.Add("SKU_Code", typeof(string));
        _dt.Columns.Add("BAR_Code", typeof(string));
        _dt.Columns.Add("COLOR", typeof(string));
        _dt.Columns.Add("SKU_Name", typeof(string));
        _dt.Columns.Add("Quantity", typeof(decimal));
        _dt.Columns.Add("PRICE", typeof(decimal));
        _dt.Columns.Add("Code", typeof(byte[]));
        DataRow dr;
        TextBox txtPurchasePrice;
        TextBox txtTradePrice;
        TextBox txtBCodeQty;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            txtPurchasePrice = (TextBox)gvr.FindControl("txtPurchasePrice");
            txtTradePrice = (TextBox)gvr.FindControl("txtTradePrice");
            txtBCodeQty = (TextBox)gvr.FindControl("txtBCodeQty");
            dr = _dt.NewRow();
            dr["SKU_ID"] = gvr.Cells[0].Text;
            dr["SKU_Code"] = gvr.Cells[1].Text;
            dr["BAR_Code"] = gvr.Cells[2].Text;
            dr["COLOR"] = gvr.Cells[13].Text;
            dr["SKU_Name"] = gvr.Cells[3].Text;
            dr["PRICE"] = txtTradePrice.Text;
            dr["Quantity"] = txtBCodeQty.Text;
            _dt.Rows.Add(dr);
        }

        DataTable _insertDt = _dt.Clone();
        foreach (DataRow dr2 in _dt.Rows)
        {
            var oBitmap = Generatecode(dr2["bar_code"].ToString());
            var ms = new MemoryStream();
            oBitmap.Save(ms, ImageFormat.Png);
            var bytearray = ms.ToArray();
            _insertDt.ImportRow(dr2);
            if (this.Session["Distributor_Id"].ToString() == "5" || this.Session["Distributor_Id"].ToString() == "6" || this.Session["Distributor_Id"].ToString() == "7")
            {
                _cn.InsertBarcode("SALMAS", bytearray, _insertDt);
            }
            else
            {
                _cn.InsertBarcode("ZBG", bytearray, _insertDt);
            }
            _insertDt.Rows.Clear();
        }

        var sk = new SkuController();
        _dt = sk.SelectSkuBarcode();
        var crpReport = new CrpBarcode();
        crpReport.SetDataSource(_dt);
        crpReport.Refresh();
        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        _dt = new DataTable();
    }
    private Bitmap Generatecode(string Bar_Code)
    {
        Linear barcode = new Linear();
        barcode.Type = BarcodeType.EAN13;
        barcode.Data = Bar_Code;
        barcode.LeftMargin = 0;
        barcode.RightMargin = 0;
        barcode.TopMargin = 0;
        barcode.BottomMargin = 0;
        barcode.Resolution = 72;
        barcode.UOM = UnitOfMeasure.PIXEL;
        byte[] barcodeInBytes = barcode.drawBarcodeAsBytes();
        Bitmap barcodeInBitmap = barcode.drawBarcode();
        return barcodeInBitmap;
    }
}