using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;    

public partial class Forms_frmPysicalStockteken : System.Web.UI.Page
{
    DataTable PurchaseSKU;
    DataControl dc = new DataControl();
    private static int RowNo;

    DataTable Dtsku_Price = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            LoadDistributor();
            //LoadSkuDetail();
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);

      
    }

    

    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    ////private void LoadSkuDetail()
    ////{
    ////    if (drpPrincipal.Items.Count > 0)
    ////    {
    ////        SKUPriceDetailController pController = new SKUPriceDetailController();
    ////        DataTable dtskuPrice = pController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 5, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
    ////        clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 3, true);
    ////        Session.Add("Dtsku_Price", dtskuPrice);
    ////    }
    ////}

    /// <summary>
    ///  Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        PhaysicalStockController MController = new PhaysicalStockController();
        DataTable dt = MController.SelectPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), 0,Constants.IntNullValue);
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = dt;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PhaysicalStockController MController = new PhaysicalStockController();
        MController.DELETEPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[0].Text), int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[5].Text));
        LoadGird();
    }

    /// <summary>
    /// Sets Document Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    //protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    RowNo = e.NewEditIndex;
    //    txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
    //    txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
    //    txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
    //    txtusaleableqty.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
    //    txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    //    txtQuantity.Focus();
    //    btnSave.Text = "Update Sku";
    //}




    /// <summary>
    /// Saves/Updates Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EvemtArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl _dc = new DataControl();
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        SKUPriceDetailController PController = new SKUPriceDetailController();
        if (cbScan.Checked)
        {
            Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(drpDistributor.SelectedValue), txtSKUScan.Text.Trim().Substring(0, 12));
        }
        else
        {
            Dtsku_Price = PController.GetSKUDetail(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfSKUID.Value));
        }
        
        if (dt.Rows.Count > 0)
        {
            var dtskuPrice = (DataTable)Session["Dtsku_Price"];
            // PurchaseSKU = (DataTable)Session["PurchaseSKU"];
          //  DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");


            if (Dtsku_Price.Rows .Count > 0)
            {

                var mController = new PhaysicalStockController();
                if (btnSave.Text == "Save")
                {
                    var qty = 0;

                    if (rbUnitType.SelectedValue == "1")// Unit wise
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)));
                    }
                    else // Ctn Wise
                    {
                        qty = (int.Parse(_dc.chkNull_0(txtCtn.Text)) * int.Parse(_dc.chkNull_0(Dtsku_Price.Rows[0]["Units_In_Case"].ToString())));
                    }

                    int Saleable = 0;
                    int unsaleable = 0;
                    if (rbDocTpe.SelectedValue == "1")//saleable
                    { Saleable = qty; }
                    else
                    { unsaleable = qty; }
                    mController.InsertPysicalStock(int.Parse(drpDistributor.SelectedValue), DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()), int.Parse(Dtsku_Price.Rows[0]["SKU_ID"].ToString()), decimal.Parse(Saleable.ToString()), decimal.Parse(unsaleable.ToString()), decimal.Parse(Dtsku_Price.Rows[0]["TRADE_PRICE"].ToString()), 0, Constants.IntNullValue,int.Parse(rbDocTpe.SelectedValue));
                }

                else
                {
                    //    mController.UpdatePysicalStock(int.Parse(drpDistributor.SelectedValue), DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()), int.Parse(Dtsku_Price.Rows[0]["SKU_ID"].ToString()), decimal.Parse(dc.chkNull_0(txtCtn.Text)), decimal.Parse(dc.chkNull_0(txtusaleableqty.Text)), decimal.Parse(txtUnitRate.Text), 0, int.Parse(drpPrincipal.SelectedValue));

                }
                LoadGird();
                ClearAll();

                var scriptManager = ScriptManager.GetCurrent(Page);
                if (scriptManager != null) scriptManager.SetFocus(txtSKU);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        //txtskuCode.Text = "";
        //txtskuName.Text = "";
        //txtQuantity.Text = "";
        //txtusaleableqty.Text = "";
        //txtUnitRate.Text = "0";
        //txtskuCode.Enabled = true;
        btnSave.Text = "Save";
    }


    protected void btnGetData_Click(object sender, EventArgs e)
    {
        LoadGird();
    }
}
