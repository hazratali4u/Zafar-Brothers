using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmPurchaseOrderAdd2 : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly PurchaseController _mPurchase = new PurchaseController();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly DataControl _dc = new DataControl();
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadPrincipal();
                LoadDistributor();
                LoadGender();
                LoadCategory();
                LoadSkuDetail();
                //  CreatTable();
                LoadGird();
                Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
                txtArrivalDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
                txtInvoiceDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

                btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
                //if (Session["orderId"] != null)
                //{
                //    LoadDocumentDetail(long.Parse(Session["orderId"].ToString()));
                //}
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error occure in Data. Please Contact to Administration!');", true);
                            

            }
        }
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


    #endregion

    #region Load

    private void LoadGender()
    {
        drpGender.Items.Clear();
      var  _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        this.drpGender.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpGender, _mDt, 0, 3, false);
        drpGender.SelectedIndex = 1;// as by All ,So many skus populate which make page unresponsible

    }

    private void LoadCategory()
    {
        drpcategory.Items.Clear();

       var _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        this.drpcategory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpcategory, _mDt, 0, 3, false);
        drpcategory.SelectedIndex = 1;// as by All ,So many skus populate which make page unresponsible
    }

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

    #region Grid Operations

    ///// <summary>
    ///// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">GridViewEditEventArgs</param>
    //protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    //{

    //    _rowNo = e.NewEditIndex;
    //    ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;


    //    //if (RbUnitType.SelectedValue == "0")
    //    //{
    //    txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
    //   // txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
    //  //  txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    //    // }
    //    //else
    //    //{
    //    //DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text + "'");

    //    //int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
    //    //int qty = Convert.ToInt32(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);
    //    //txtQuantity.Text = Convert.ToString(qty / mPackSize);
    //    //qty=Convert.ToInt32(txtQuantity.Text); 

    //    //txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    //    //decimal amount= Convert.ToDecimal(GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text);
    //    //txtRate.Text =Convert.ToString(amount / qty);
    //    //txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    //    // }



    //    txtQuantity.Focus();

    //    btnSave.Text = "Update Item";

    //}

    ///// <summary>
    ///// Deletes A Document Detail
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">GridViewEditEventArgs</param>
    //protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DataTable _purchaseSku = (DataTable)Session["PurchaseSKU"];
    //    if (_purchaseSku.Rows.Count > 0)
    //    {
    //        _purchaseSku.Rows.RemoveAt(e.RowIndex);
    //        Session.Add("PurchaseSKU", _purchaseSku);
    //        LoadGird();
    //    }
    //}

    #endregion

    private void LoadGird()
    {
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();

        decimal totalValue = 0;
        
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        GrdPurchase.DataSource = Dtsku_Price;
        GrdPurchase.DataBind();
        foreach (DataRow dr in Dtsku_Price.Rows)
        {
            totalValue += Math.Round(decimal.Parse(dr["Quantity"].ToString()));
          
        }

        txtTotalQty.Text = totalValue.ToString();
       
    }


    private void LoadGird(DataTable Dtsku_Price)
    {
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        decimal totalValue = 0;

       // DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        GrdPurchase.DataSource = Dtsku_Price;
        GrdPurchase.DataBind();
        foreach (DataRow dr in Dtsku_Price.Rows)
        {
            totalValue += Math.Round(decimal.Parse(_dc.chkNull_0(dr["Quantity"].ToString())));

        }

        txtTotalQty.Text = totalValue.ToString();

    }



    private void LoadSkuDetail()
    {

        if (drpPrincipal.Items.Count > 0 && drpDistributor .Items.Count>0)
        {
          DataTable dtskuPrice = _pController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, int.Parse(drpcategory.SelectedValue), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 7, DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(drpGender.SelectedValue));
        //  clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 3, true);
          Session.Add("Dtsku_Price", dtskuPrice);
        }
    }

    #endregion



    #region Click OPerations

   

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        var Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtQty = gvr.FindControl("txtQty") as TextBox;
            GridViewRow item = (GridViewRow)txtQty.NamingContainer;
            if (int.Parse(_dc.chkNull_0(txtQty.Text)) > 0)
            {
                Dtsku_Price.Rows[item.RowIndex]["Quantity"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtQty.Text)));
            }


        }
        Session.Add("Dtsku_Price", Dtsku_Price);
        LoadGird(Dtsku_Price);

    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        btnCalculate_Click(null, null);
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
            int TotalQty = 0;
            foreach (DataRow dr in Dtsku_Price.Rows)
            {
                TotalQty += int.Parse(dr["QUANTITY"].ToString());
            }
            if (TotalQty < 1)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Enter Quantity');", true);
                return;
            }

           if (Dtsku_Price.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                    Dtsku_Price.Rows.Clear();
                    Session.Add("Dtsku_Price", Dtsku_Price);
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
        LoadGird();
    }

    #endregion


    private bool CalculatePurchase(DateTime cWorkDate)
    {
        
        bool flag = false;

        var Dtsku_Price = (DataTable)Session["Dtsku_Price"];
      

        if (Session["orderId"] == null)
        {
            if (!IsBillBookNoExist())
            {
                long mResult = _mPurchase.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), txtContainerNo.Text
                , txtRemarks.Text, DateTime.Parse(txtArrivalDate.Text), cWorkDate, int.Parse(Session["UserId"].ToString()), Dtsku_Price, DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(txtInvoiceDate.Text),txtBag.Text);
                if (mResult != -2)
                {
                   // Print(mResult);
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
            , txtRemarks.Text, DateTime.Parse(txtArrivalDate.Text), int.Parse(Session["UserId"].ToString()), Dtsku_Price, DateTime.Parse(txtInvoiceDate.Text),txtBag.Text);

            if (mResult != -2)
            {


                //Print(mResult);
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
    
    protected void drpGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
        LoadGird();
    }

    protected void drpcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
        LoadGird();
    }
}