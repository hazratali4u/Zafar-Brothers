using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class SKU_Price : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            Populate_drpSKUCompany();
            Populate_drpSKUDivisions();
            Populate_drpSKUCategory();
            Populate_drpSKUSubCategory();
            Populate_drpSKUBrand();
            LoadSkus();
            LoadGrid();
            LoadDistributor();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromdate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void Populate_drpSKUCompany()
    {
      
        var mHerController = new SkuHierarchyController();
        DataTable mDt = mHerController.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskuPrincipal, mDt, 0, 3, true);
    }

    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUDivisions();
        Populate_drpSKUCategory();
       
        Populate_drpSKUSubCategory();
        LoadSkus();
        LoadGrid();
    }

    /// <summary>
    /// Loads Divisions To Division Combo
    /// </summary>
    private void Populate_drpSKUDivisions()
    {
       
        if (ddskuPrincipal.Items.Count > 0)
        {
            var mHerController = new SkuHierarchyController();
            if (ddskuPrincipal.Items.Count   > 0)
            {
                DataTable mDt = mHerController.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(ddskuDivision, mDt, 0, 3, true);
            }
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo, Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        Populate_drpSKUSubCategory();
        LoadSkus();
        LoadGrid();
    }
    
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        ddskuCategory.Items.Clear();
        if (ddskuDivision.Items.Count > 0)
        {
            var mHerController = new SkuHierarchyController();
            if (ddskuDivision.Items.Count   > 0)
            {
                DataTable mDt = mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddskuDivision.SelectedValue), null, null, true, int.Parse(Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(ddskuCategory, mDt, 0, 3, true);
            }
        }
    }
    private void Populate_drpSKUSubCategory()
    {
        ddskuSubCategory.Items.Clear();
        if (ddskuDivision.Items.Count > 0)
        {
            var mHerController = new SkuHierarchyController();
            if (ddskuCategory.Items.Count > 0)
            {
                DataTable mDt = mHerController.SelectSkuHierarchy(Constants.SKUSubCategory, Constants.IntNullValue, int.Parse(ddskuCategory.SelectedValue), null, null, true, int.Parse(Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(ddskuSubCategory, mDt, 0, 3, true);
            }
        }
    }
    /// <summary>
    /// Loads Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUSubCategory();
       
        LoadSkus();
        LoadGrid();
    }
    protected void ddskuSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkus();
        LoadGrid();
    }
    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
       
        if (ddskuCategory.Items.Count > 0)
        {
            var mHerController = new SkuHierarchyController();
            string strSkuCategoryId = ddskuCategory.SelectedItem.Value;

            if (int.Parse(strSkuCategoryId) > 0)
            {
                DataTable mDt = mHerController.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(ddskuBrand, mDt, 0, 3, true);
            }
        }
    }

    /// <summary>
    /// Loads SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  LoadSkus();
      //  LoadGrid();
    }
   
    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSkus()
    {
        
            var mSkuController = new SkuController();
       
            DataTable mDt = mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddskuName, mDt, 0, 2, true);

            if (mDt.Rows.Count == 0)
            {
                Grid_pricedetails.DataSource = null;
                Grid_pricedetails.DataBind();
            }
       
        
    }

    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    
    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid()
    {
        var mSkuController = new SKUPriceDetailController();
        if (ddskuName.Items.Count > 0)
        {
            DataTable dtsku = mSkuController.SelectSKuCurrentPrice(Constants.IntNullValue,int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue,
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(ddskuName.SelectedValue));
            Grid_pricedetails.DataSource = dtsku.DefaultView;
            Grid_pricedetails.DataBind();
        }
        
    }
    
    /// <summary>
    /// Loads Locations To Location ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        DataTable dt2=dt.Clone();
        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 5");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        clsWebFormUtil.FillListBox(ChbDistributorList, dt2, 0, 2, true);
    }

    /// <summary>
    /// Save Or Updates an SKU Price
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();

            int count = 0;
            bool result = false;
            for(int i  = 0;i<ChbDistributorList.Items.Count;i++)   
            {
                    
                if(ChbDistributorList.Items[i].Selected == true)
                {
                    count++;
                   result= mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), int.Parse(ddskuName.SelectedValue), 1, decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)), decimal.Parse(dc.chkNull_0(txtTaxPrices.Text)), decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)),  
                        decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), decimal.Parse(dc.chkNull_0(txtRetailPrice.Text)), DateTime.Parse(txtFromdate.Text),decimal.Parse (dc.chkNull_0(txtSADTax.Text)));
                }
            
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select any Location');", true);
            }   
            LoadGrid();
            if (result)
            {
               
                

                txtTaxPrices.Text = "0";
                txtRetailPrice.Text = "";
                txtTradePrice.Text = "";
                txtDistributorPrice.Text = "";



                CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
                txtFromdate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

               
                LoadGrid();
           
            }
           
        }
    }
}
