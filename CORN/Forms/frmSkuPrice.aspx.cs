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


public partial class SkuPrice : System.Web.UI.Page
{

    readonly SkuController _mSkuController = new SkuController();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();

    private DataTable _mDt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadGender();
            LoadCategory();

            LoadSkus();
           

            btnSave.Attributes.Add("onclick", "return ValidateForm()");

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromdate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        DataTable dt2 = dt.Clone();
        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 5");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt2, 0, 2, true);
        clsWebFormUtil.FillListBox(ChbDistributorList, dt2, 0, 2, true);

    }

    private void LoadPrincipal()
    {
        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, _mDt, 0, 3, true);
    }

    private void LoadGender()
    {

        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpGender, _mDt, 0, 3, true);

    }

    private void LoadCategory()
    {
        drpcategory.Items.Clear();

        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpcategory, _mDt, 0, 3, true);

    }

    private void LoadSkus()
    {
        var mSkuController = new SkuController();
        DataTable mDt = mSkuController.SelectSkuPrice(int.Parse(DrpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), int.Parse(drpGender.SelectedValue), int.Parse(drpcategory.SelectedValue), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        this.Session.Add("mDt", mDt);
        if (mDt.Rows.Count == 0)
        {
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
        else
        {
            Grid_pricedetails.DataSource = mDt.DefaultView;
            Grid_pricedetails.DataBind();
        }
    }
    private void LoadGrid(DataTable mDt)
    {

        if (mDt.Rows.Count == 0)
        {
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
        else
        {
            Grid_pricedetails.DataSource = mDt.DefaultView;
            Grid_pricedetails.DataBind();
        }


    }

   protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            Calculate();
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();

            int count = 0;
            bool result = false;

            var mDt = (DataTable)this.Session["mDt"];
            for (int i = 0; i < ChbDistributorList.Items.Count; i++)
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    foreach (DataRow dr in mDt.Rows)
                    {

                        count++;
                        result = mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), int.Parse(dr["Sku_ID"].ToString()), 1, decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_DISCOUNT"].ToString())), decimal.Parse(dc.chkNull_0(dr["TAX_PRICE"].ToString())), decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_PRICE"].ToString())),
                             decimal.Parse(dc.chkNull_0(dr["TRADE_PRICE"].ToString())), decimal.Parse(dc.chkNull_0(dr["RETAIL_PRICE"].ToString())), DateTime.Parse(dr["DATE_EFFECTED"].ToString()), decimal.Parse(dc.chkNull_0(dr["SED_TAX"].ToString())),Convert.ToDecimal(dc.chkNull_0(dr["PURCHASE_PRICE"].ToString())));
                    }
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select any Location');", true);
                return;
            }
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Updated !');", true);
                LoadSkus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Error ! Please try Again.');", true);
            }
        }
    }

    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkus();

    }

    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkus();

    }

    protected void drpGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkus();

    }

    protected void drpcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkus();

    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        var mDt = (DataTable)this.Session["mDt"];
        foreach(DataRow dr in mDt.Rows)
        {
            dr["Date_effected"] = DateTime.Parse(txtFromdate.Text).ToShortDateString();

        }

        this.Session.Add("mDt", mDt);
        LoadGrid(mDt);
    }

    protected void Calculate()
    {
        var mDt = (DataTable)this.Session["mDt"];
        foreach (GridViewRow gvr in Grid_pricedetails.Rows)
        {
            foreach (DataRow dr in mDt.Rows)
            {
                if (dr["Sku_ID"].ToString() == gvr.Cells[0].Text)
                {
                    TextBox txtPURCHASE_PRICE = (TextBox)gvr.FindControl("txtPURCHASE_PRICE");
                    TextBox txtDISTRIBUTOR_PRICE = (TextBox)gvr.FindControl("txtDISTRIBUTOR_PRICE");
                    TextBox txtTRADE_PRICE = (TextBox)gvr.FindControl("txtTRADE_PRICE");
                    TextBox txtTax_price = (TextBox)gvr.FindControl("txtTax_price");
                    TextBox txtDate_effected = (TextBox)gvr.FindControl("txtDate_effected");
                    dr["DISTRIBUTOR_PRICE"] = txtDISTRIBUTOR_PRICE.Text;
                    dr["TRADE_PRICE"] = txtTRADE_PRICE.Text;
                    dr["Tax_price"] = txtTax_price.Text;
                    dr["date_effected"] = txtDate_effected.Text;
                    dr["PURCHASE_PRICE"] = txtPURCHASE_PRICE.Text;
                    break;
                }
            }
        }

        this.Session.Add("mDt", mDt);

    }
}