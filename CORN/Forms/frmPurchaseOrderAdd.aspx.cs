using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmPurchaseOrderAdd : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly PurchaseController _mPurchase = new PurchaseController();

    readonly DataControl _dc = new DataControl();
    private static int _rowNo;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadPrincipal();
                LoadDistributor();
                LoadSkuDetail();
                CreatTable();
                Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
                txtArrivalDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
                txtInvoiceDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
                btnSave.Attributes.Add("onclick", "return ValidateForm();");
                if (Session["orderId"] != null)
                {
                    LoadDocumentDetail(long.Parse(Session["orderId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error occure in Data. Please Contact to Administration!');", true);                           
            }
        }
    }

    private void CreatTable()
    {
        DataTable _purchaseSku = new DataTable();
        _purchaseSku.Columns.Add("PURCHASE_ORDER_DETAIL_ID", typeof(long));
        _purchaseSku.Columns.Add("SKU_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_Code", typeof(string));
        _purchaseSku.Columns.Add("BAR_Code", typeof(string));
        _purchaseSku.Columns.Add("SKU_Name", typeof(string));
        _purchaseSku.Columns.Add("Quantity", typeof(decimal));
        //_purchaseSku.Columns.Add("QUANTITY_CTN", typeof(int));
        //   _purchaseSku.Columns.Add("PRICE", typeof(decimal));
        // _purchaseSku.Columns.Add("AMOUNT", typeof(decimal));
        Session.Add("PurchaseSKU", _purchaseSku);

    }

    #region Check_Duplication

    //Stop replication for ref no
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        //if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || Session["hfBillBookNo"].ToString() != txtRefNo.Text) && txtRefNo.Text.Trim().Length > 0)
        //{
        //    OrderEntryController OEC = new OrderEntryController();
        //    DataTable dtBillBookNo = OEC.SelectBillBookNo(int.Parse(drpDistributor.SelectedValue), txtRefNo.Text, 0);
        //    if (dtBillBookNo.Rows.Count > 0)
        //    {
        //        flag = true;
        //    }
        //}
        return flag;
    }

    private bool CheckDublicateSku()
    {
        DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = _purchaseSku.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Load
 
    private void LoadPrincipal()
    {
        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, mDt, 0, 1, true);
    }
    
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        //if (Session["orderId"] == null)//For by Default wareHOuse Selection
        //{
        //    DataRow[] fndRow = dt.Select("SUBZONE_ID = 3");
        //    var ABC = fndRow[0][0].ToString();
        //    drpDistributor.SelectedValue = ABC;
        //}

    }

    private void LoadDocumentDetail(long orderId)
    {
        try
        {
            DataSet ds = _mPurchase.SelectPurchaseOrderNo(orderId, 0, Constants.DateNullValue);
            if (ds.Tables[0].Rows.Count > 0)
            {

                drpPrincipal.Enabled = false;
                drpDistributor.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtContainerNo.Text = ds.Tables[0].Rows[0]["CONTAINER_NO"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                txtBag.Text = ds.Tables[0].Rows[0]["BAG_NO"].ToString();
                drpPrincipal.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
                txtArrivalDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ARRIVAL_DATE"]).ToString("dd-MMM-yyyy");
                txtInvoiceDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["INVOICE_DATE"]).ToString("dd-MMM-yyyy");

                DataTable _purchaseSku = ds.Tables[1];
                Session.Add("PurchaseSKU", _purchaseSku);
                LoadSkuDetail();
                LoadGird();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        decimal totalValue = 0;
        //decimal totalAmountFc = 0;
        DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = _purchaseSku;
        GrdPurchase.DataBind();
        foreach (DataRow dr in _purchaseSku.Rows)
        {
            totalValue += Math.Round(decimal.Parse(dr["Quantity"].ToString()));
           // totalAmountFc += Convert.ToDecimal(dr["AMOUNT"]);

        }

        txtTotalQty.Text = totalValue.ToString();
        //txtAmountFC.Text = Math.Round(totalAmountFc, 3).ToString();
        ////txtAmountRs.Text = Math.Round((TotalAmountFc * Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text))), 3).ToString();
    }
    private void LoadSkuDetail()
    {

        if (drpPrincipal.Items.Count > 0 && drpDistributor .Items.Count>0)
        {
          DataTable dtskuPrice = _pController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 5, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
          clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 3, true);
          Session.Add("Dtsku_Price", dtskuPrice);
        }
    }

    #endregion

    #region Grid Operations

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

        _rowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;


        //if (RbUnitType.SelectedValue == "0")
        //{
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
       // txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
      //  txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        // }
        //else
        //{
        //DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text + "'");

        //int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
        //int qty = Convert.ToInt32(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);
        //txtQuantity.Text = Convert.ToString(qty / mPackSize);
        //qty=Convert.ToInt32(txtQuantity.Text); 

        //txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        //decimal amount= Convert.ToDecimal(GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text);
        //txtRate.Text =Convert.ToString(amount / qty);
        //txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        // }



        txtQuantity.Focus();

        btnSave.Text = "Update Item";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
        if (_purchaseSku.Rows.Count > 0)
        {
            _purchaseSku.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }

    #endregion

    #region Click OPerations

    //Grid Operation
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");


        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Item")
            {
                if (CheckDublicateSku())
                {
                    DataRow dr = _purchaseSku.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["BAR_Code"] = foundRows[0]["BAR_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                    dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                    


                    _purchaseSku.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU Already Exists ');", true);
                    return;
                }
            }
            else if (btnSave.Text == "Update Item")
            {
                DataRow dr = _purchaseSku.Rows[_rowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["BAR_Code"] = foundRows[0]["BAR_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));

               

            }
            Session.Add("PurchaseSKU", _purchaseSku);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
      
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));

        if (dt.Rows.Count > 0)
        {

            DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
            if (_purchaseSku.Rows.Count > 0)
            {

                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    _purchaseSku = (DataTable)Session["PurchaseSKU"];
                    _purchaseSku.Rows.Clear();
                    Session.Add("PurchaseSKU", _purchaseSku);
                    LoadGird();
                    LoadDistributor();
                    LoadPrincipal();
                    LoadSkuDetail();
                    ClearAll();
                    txtContainerNo.Text = "";
                    txtRemarks.Text = "";
                    txtBag.Text = string.Empty;
                    txtTotalQty.Text = "";
                    DisAbaleOption(false);
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record save successfully');", true);
                    Session.Remove("orderId");
                    Response.Redirect("frmPurchaseOrder.aspx?LevelID=150&LevelType=3", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        CreatTable();
        LoadGird();
        ClearAll();
        txtRemarks.Text = "";
        txtBag.Text = string.Empty;
        txtContainerNo.Text = "";
    }

    #endregion

    #region Index/Change

   
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadSkuDetail();

    }

    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadSkuDetail();
    }

    #endregion
    private bool CalculatePurchase(DateTime cWorkDate)
    {        
        bool flag = false;
        var dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
        if (Session["orderId"] == null)
        {
            if (!IsBillBookNoExist())
            {
                long mResult = _mPurchase.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), txtContainerNo.Text
                , txtRemarks.Text, DateTime.Parse(txtArrivalDate.Text), cWorkDate, int.Parse(Session["UserId"].ToString()), dtPurchaseDetail, DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(txtInvoiceDate.Text), txtBag.Text);
                if (mResult != -2)
                {                   
                    flag = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('This Ref No already exist,Kindly enter different Ref No');", true);
            }
        }
        else
        {
            long mResult = _mPurchase.UpdatePurchaseOrderDetail(long.Parse(Session["orderId"].ToString()), int.Parse(drpDistributor.SelectedValue), txtContainerNo.Text
            , txtRemarks.Text, DateTime.Parse(txtArrivalDate.Text), int.Parse(Session["UserId"].ToString()), dtPurchaseDetail, DateTime.Parse(txtInvoiceDate.Text), txtBag.Text);
            if (mResult != -2)
            {
                flag = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
            }
        }
        return flag;
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="isDisable">bool</param>
    private void DisAbaleOption(bool isDisable)
    {
        if (isDisable)
        {
            drpPrincipal.Enabled = false;
           
            

           

        }
        else
        {
            drpPrincipal.Enabled = true;
        }
    }

    private void ClearAll()
    {

        txtQuantity.Text = "";
     

        btnSave.Text = "Add Item";
        
        
    }

    private void Print(long mResult)
    {
        //var mController = new DocumentPrintController();
        //var rptInventoryCtl = new RptInventoryController();

        //var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument2();
        //DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        //DataSet ds = rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), DateTime.Parse(txtPODate.Text), DateTime.Parse(txtPODate.Text), 1);
        //crpReport.SetDataSource(ds);
        //crpReport.Refresh();
        //crpReport.SetParameterValue("DocumentType", "Purchase Order" + " Document");
        //crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
        //crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        //Session.Add("CrpReport", crpReport);
        //Session.Add("ReportType", 0);
        //const string url = "'Default.aspx'";
        ////const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        ////Type cstype = GetType();
        ////ClientScriptManager cs = Page.ClientScript;
        ////cs.RegisterStartupScript(cstype, "OpenWindow", script);
        //Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
}