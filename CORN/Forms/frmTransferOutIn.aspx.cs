using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Transfer In Stock
/// </summary>
public partial class Forms_frmTransferOutIn : System.Web.UI.Page
{
    readonly DistributorController _dController = new DistributorController();
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly PurchaseController _mController = new PurchaseController();
    private static int _rowNo;
    DataTable _purchaseSkus;

    private void CreateTable()
    {
        _purchaseSkus = new DataTable();
        _purchaseSkus.Columns.Add("SKU_ID", typeof(int));
        _purchaseSkus.Columns.Add("SKU_Code", typeof(string));
        _purchaseSkus.Columns.Add("SKU_Name", typeof(string));
        _purchaseSkus.Columns.Add("Quantity", typeof(int));
        Session.Add("PurchaseSKUS", _purchaseSkus);
    }
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!Page.IsPostBack)
        {
            LoadToDistributor();
            LoadDistributor();   
            GetDocumentNo();
            LoadDocumentDetail();
        }
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();

        DataTable dt = _mController.SelectTransferDocumentNo(Constants.Document_Transfer_Out, int.Parse (drpDistributor .SelectedValue ), Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue,int.Parse (DrpTransferFor.SelectedValue), 0, Constants.DateNullValue);
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDocumentDetail();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        LoadDocumentDetail();
    }
    /// <summary>
    ///  Loads Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        try
        {
            if (drpDocumentNo.Items.Count > 0)
            {
                DataTable dt = _mController.SelectTransferDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTransferFor.SelectedValue), 0,Constants .DateNullValue);
                if (dt.Rows.Count > 0)
                {
                    txtDocumentNo.Text = dt.Rows[0]["ORDER_NUMBER"].ToString();
                    txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                    drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                    DataTable purchaseSkus = _mController.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
                    GrdPurchase.DataSource = purchaseSkus;
                    GrdPurchase.DataBind();
                    Session.Add("PurchaseSKUS", purchaseSkus);
                }
                else
                {
                    txtDocumentNo.Text = "";
                    txtBuiltyNo.Text = "";
                    drpDistributor.SelectedIndex = 0;
                    Session.Remove("PurchaseSKUS");
                    GrdPurchase.DataSource = null;
                    GrdPurchase.DataBind();
                }
            }
            else
            {
                txtDocumentNo.Text = "";
                txtBuiltyNo.Text = "";

                drpDistributor.SelectedIndex = 0;
                Session.Remove("PurchaseSKUS");
                GrdPurchase.DataSource = null;
                GrdPurchase.DataBind();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
        }
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Locations To Location To Combo
    /// </summary>
    private void LoadToDistributor()
    {
       
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpTransferFor, dt, 0, 2, true);
    }
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnTransferIn_Click(object sender, EventArgs e)
    {
        try
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(DrpTransferFor.SelectedValue.ToString()));

            if (dt.Rows.Count > 0)
            {

                DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];
                decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));

                bool mResult = _mController.InsertTransferIn(int.Parse(drpDocumentNo.SelectedValue), int.Parse(DrpTransferFor.SelectedValue.ToString()), txtDocumentNo.Text, Constants.Document_Transfer_In,
                       DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                      , mTotalAmount, false, dtPurchaseDetail, 1, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()),1, 1);

                if (mResult == true)
                {
                    GrdPurchase.DataSource = null;
                    GrdPurchase.DataBind();
                    Session.Remove("PurchaseSKUS");
                    LoadDistributor();
                    GetDocumentNo();
                    LoadDocumentDetail();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
        }
    }

    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _rowNo = e.NewEditIndex;
        txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;

        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[4].Enabled = false;
        }
        txtQuantity.Focus();
        btnSave.Text = "Update Item";

    }
    private void ClearAll()
    {
        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtQuantity.Text = "";
     
       
        btnSave.Text = "Add Item";
       
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
        

            
            if (btnSave.Text == "Update Item")
            {
              
                    DataRow dr = _purchaseSkus.Rows[_rowNo];
                   
                    dr["SKU_Code"] =txtskuCode.Text;
                    dr["SKU_Name"] = txtskuName.Text;
                  
                    dr["Quantity"] = int.Parse(txtQuantity.Text);
                       
            }
                else
                {
                   // ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + txtskuCode.Text + "Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    //txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                  //  return;
                }
            
            Session.Add("PurchaseSKUS", _purchaseSkus);
            ClearAll();
            LoadGird();  
       
    }
    private void LoadGird()
    {
        //int TotalValue = 0;
        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = _purchaseSkus;
        GrdPurchase.DataBind();
        //foreach (DataRow dr in PurchaseSKUS.Rows)
        //{
        //    TotalValue += int.Parse(dr["Quantity"].ToString());

        //}
        //txtTotalQuantity.Text = TotalValue.ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      //  Respo
    }

   
}
