using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Adjust Stock
/// </summary>
public partial class Forms_frmOpeningStock : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;
    readonly DataControl _dc = new DataControl();
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreatTable();
            GetDocumentNo();
            LoadDistributor();
            LoadSkuDetail();
        }
    }

    /// <summary>
    /// Creates Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("RACK_NO", typeof(string));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("Quantity", typeof(decimal));
        PurchaseSKU.Columns.Add("FREE_SKU", typeof(decimal));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("PACKSIZE", typeof(string));
        PurchaseSKU.Columns.Add("COLOR", typeof(string));
        PurchaseSKU.Columns.Add("PURCHASE_PRICE", typeof(decimal));
        Session.Add("PurchaseSKU", PurchaseSKU);
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        
        var mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreatTable();
            LoadGird();
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpDocumentType.Enabled = true;
            ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            LoadDocumentDetail();
            LoadSkuDetail();
        }
    }

    /// <summary>
    /// Loads Supplier Combo
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Item Detail To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }    
    
    /// <summary>
    /// Loads Item Detail To ListBox
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }    
    
    /// <summary>
    /// Loads Item Detail To ListBox
    /// </summary>
    private void LoadSkuDetail()
    {
        ddlSKuCde.Items.Clear();
       // if (drpPrincipal.Items.Count > 0)
      //  {
            DataTable dtskuPrice = PController.SelectDataPrice2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            //clsWebFormUtil.FillListBox(lstCode, dtskuPrice, 9, 9, true);
            clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 9, true);
            Session.Add("Dtsku_Price", dtskuPrice);
       // }
    }
       
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }


    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        //txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        //txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;","");
       // PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text);
       // FreePrivousQty = 0;
        txtsize.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
       // txtcolor.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        // txtFreeSKU.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        //txtBatchNo.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();
        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[8].Enabled = false;
            GrdPurchase.Rows[i].Cells[9].Enabled = false;

        }
        btnSave.Text = "Update Item";

    }

    /// <summary>
    /// Deletes Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
        
    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
           // drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            drpDistributor.Enabled = false;
        }
    }

    /// <summary>
    /// Checks Duplicate Item in Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSku()
    {

        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;

    }
  
    private decimal CheckStockStatus(int skuId)
    {
        if (DrpDocumentType.SelectedItem.Value == "8")
        {

            var mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock2(int.Parse(drpDistributor.SelectedValue.ToString()), skuId, "N/A", DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            if (dt.Rows.Count > 0)
            {
                if (decimal.Parse(dt.Rows[0][0].ToString()) >= decimal.Parse(txtQuantity.Text))
                {
                    return -1;
                }
                return decimal.Parse(dt.Rows[0][0].ToString());
            }
        }
        else
        {
            return -1;
        }
        return 0;
    }
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            decimal currentStock = CheckStockStatus(int.Parse(_dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));
            if (btnSave.Text == "Add Item")
            {
                if (CheckDublicateSku())
                {
                    if (currentStock == -1)
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["COLOR"] = foundRows[0]["COLOR"];
                        dr["PACKSIZE"] = foundRows[0]["PACKSIZE"];
                        dr["PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                        dr["PURCHASE_PRICE"] = foundRows[0]["PURCHASE_PRICE"];
                        dr["Quantity"] = decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Current closing Stock is " + currentStock + "');", true);
                        txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update Item")
            {
                if (currentStock == -1)
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["COLOR"] = foundRows[0]["COLOR"];
                    dr["PACKSIZE"] = foundRows[0]["PACKSIZE"];
                    dr["PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["PURCHASE_PRICE"] = foundRows[0]["PURCHASE_PRICE"];
                    dr["Quantity"] = decimal.Parse(txtQuantity.Text);
                    dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + "Current closing Stock is " + currentStock + "');", true);
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            Session.Add("PurchaseSKU", PurchaseSKU);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item please check in list');", true); 
        }
    }    
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        var mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            DateTime mWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            var mController = new PurchaseController();
            var dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
            decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));

            bool mResult;
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                      , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()),0, mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0);
            }
            else
            {
                 mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                   , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                   , mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0);
            }
            if (mResult)
            {
                lblErrorMsg.Text = "Record Upated";
                PurchaseSKU = (DataTable) Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                Session.Add("PurchaseSKU", PurchaseSKU);
                LoadGird();
                GetDocumentNo();
                drpDistributor.Enabled = true;
                drpPrincipal.Enabled = true;
                DrpDocumentType.Enabled = true;
                ClearAll();
                txtDocumentNo.Text = "";
                DisAbaleOption(false);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location or Unassigne');", true);
        }
    }
   
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        drpDistributor.Enabled = true;
        drpPrincipal.Enabled = true;
        DrpDocumentType.Enabled = true;
        ClearAll();
        txtDocumentNo.Text = "";
        DisAbaleOption(false);
    }


    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
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

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtQuantity.Text = "";
       // txtcolor.Text = "";
        txtsize.Text = "";
      //  txtFreeSKU.Text = "0";
      //  txtBatchNo.Text = "N/A";
        ddlSKuCde.Enabled = true;
        btnSave.Text = "Add Item";
        lblErrorMsg.Text = "";

    }
}