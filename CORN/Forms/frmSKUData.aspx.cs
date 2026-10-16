using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.IO;

/// <summary>
/// From To Add, Edit, Delete Item
/// </summary>
public partial class Forms_frmSKUData : Page
{
    readonly SkuController _mSkuController = new SkuController();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    
    private DataTable _mDt, _mSkuDt;
    
    private static int _mSkuId;
    
    private bool CheckDublicateSku()
    {
        //_mSkuDt = (DataTable)Session["_mSkuDt"];

        //DataRow[] foundRows = _mSkuDt.Select("SKU_CODE  = '" + txtstyleCode.Text + "'");
        //if (foundRows.Length == 0)
        //{
        //    return true;
        //}

        return true;

    }
    
    private bool CheckDublicateSku2()
    {
        return true;

        //_mSkuDt = (DataTable)Session["_mSkuDt"];

        //var foundRows = _mSkuDt.Select("SKU_CODE ='" + txtbarcode.Text + "' AND SKU_ID <> '" + _mSkuId + "'");
        //if (foundRows.Length == 0)
        //{
        //    return true;
        //}
        //return false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            LoadPrincipal();
           LoadGender();
           LoadCountry();
           LoadCategory();
           LoadType();
           LoadColor();
          // LoadFabric();
           LoadSize();
          // LoadBrand();
           LoadSeason();
           LoadData();
           LoadGrid();
            Page.Form.Enctype = "multipart/form-data";
        }
    }

    //#region Load

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

    protected void LoadType()
    {

        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUType, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drptype, _mDt, 0, 3, true);
    }

    protected void LoadCountry()
    {

        _mDt = _mHerController.SelectSkuHierarchy_ZB(Constants.SKUCountry, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(drpCountry, _mDt, 1, 4, true);
    }
    private void LoadColor()
    {

        chkColor.Items.Clear();


        DataTable dt = _mHerController.SelectSkuHierarchy_ZB(Constants.SKUColor, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
          //  clsWebFormUtil.FillDropDownList(drpColor, dt, 0, 3, true);
            clsWebFormUtil.FillListBox(chkColor, dt, 1, 4);
       
    }
    //private void LoadFabric()
    //{

    //    drpfabric.Items.Clear();


    //    DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUFabric, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
    //    clsWebFormUtil.FillDropDownList(drpfabric, dt, 0, 3, true);

    //}
    private void LoadSize()
    {
        DrpSize.Items.Clear();

        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSize, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpSize, dt, 0, 3, true);

    }
    //private void LoadBrand()
    //{
    //    DrpBrand.Items.Clear();

    //    DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
    //    clsWebFormUtil.FillDropDownList(DrpBrand, dt, 0, 3, true);

    //}
    private void LoadSeason()
    {
        DrpSeason.Items.Clear();

        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpSeason, dt, 0, 3, true);

    }
    //private void LoadUom()
    //{

    //    DataTable dtUoMs = _mSkuController.SelectUOMs(Constants.IntNullValue, null);
    //    DataTable dtUoMs2 = _mSkuController.SelectUOMs(Constants.IntNullValue, null);
    //    clsWebFormUtil.FillDropDownList(drpUom, dtUoMs, 0, 1, true);
    //    clsWebFormUtil.FillDropDownList(drpUom2, dtUoMs2, 0, 1, true);
    //}
    private void LoadData()
    {
        _mSkuDt = _mSkuController.SelectSkuInfo2(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
        Session.Add("_mSkuDt", _mSkuDt);
    }
    private void LoadGrid()
    {
        _mSkuDt = (DataTable)Session["_mSkuDt"];
        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            case 5:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            case 6:
                _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                _mSkuDt.DefaultView.RowFilter = "SKU_CODE" + " like '%" + "" + "%'";
                break;
        }
        grdSKUData.EditIndex = -1;
        grdSKUData.DataSource = _mSkuDt.DefaultView;
        grdSKUData.DataBind();
    }

    //#endregion

    //#region Sel/Index Change

    //protected void ddskudivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SkuCategory();
    //    SkuSubCategory();
    //}
    //protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SkuSubCategory();
    //}


    //#endregion

    //#region Grid OPerations

    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HdnSku_ID.Value =(grdSKUData.Rows[e.NewEditIndex].Cells[0].Text);
        drpPrincipal.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[1].Text;
        drpGender.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[2].Text;
        drpcategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[3].Text;
        drptype.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text;
        txtBrandName.Text = grdSKUData.Rows[e.NewEditIndex].Cells[14].Text.Replace("&nbsp;", "");
       
       
        DrpSeason.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[5].Text;
        chkColor.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[7].Text;
        chkColor.Enabled = false;
        DrpSize.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[6].Text;
        drpCountry.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[18 ].Text;
        
        txtbarcode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
        txtstyleCode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
        txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        txtUnitInCase.Text = grdSKUData.Rows[e.NewEditIndex].Cells[15].Text.Replace("&nbsp;", "");
        txtRemarks.Text = grdSKUData.Rows[e.NewEditIndex].Cells[16].Text.Replace("&nbsp;", "");
       
        if (grdSKUData.Rows[e.NewEditIndex].Cells[17].Text.Replace("&nbsp;", "").Length > 0)
        {
            imgProduct.ImageUrl = "~/Pics/" + grdSKUData.Rows[e.NewEditIndex].Cells[17].Text;
        }
        for (int i = 0; i < grdSKUData.Rows.Count; i++)
        {
            grdSKUData.Rows[i].Cells[19].Enabled = false;
            grdSKUData.Rows[i].Cells[20].Enabled = false;

        }

        btnSave.Text = "Update";


  
    }

    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { 
        string result = _mSkuController.UpdateSKUS(false, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[0].Text), int.Parse(Session["UserId"].ToString()));

       LoadData();
       LoadGrid();
    }

    //#endregion

    //#region Click OPeration

    //private string PieceType()
    //{
    //    string perPc = "";

    //    if (chbDecore.Checked)
    //    {
    //        perPc = chbDecore.Text;
    //        if (chbMosaic.Checked)
    //        {
    //            perPc = perPc + '-' + chbMosaic.Text;
    //        }
    //    }
    //    else if (chbMosaic.Checked)
    //    {
    //        perPc = chbMosaic.Text;
    //    }

    //    return perPc;
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {

        #region Sku Name 
        if (drpGender.Items.Count > 0 && drpcategory.Items.Count > 0 && drptype.Items .Count >0)
        {
            txtskuname.Text = drpGender.SelectedItem.Text + " " + drpcategory.SelectedItem.Text + " " + drptype.SelectedItem.Text + " " + txtBrandName.Text;
        }
        else
        {
            txtskuname.Text = txtBrandName.Text;
        }
        #endregion
        #region Max Sku Id
        SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();

        string strcode = "";
       // long UserId = long.Parse(mAutoCode.GetAutoSkuCode().ToString());
       int UserId = int.Parse(mAutoCode.GetAutoCustomerCode("SKU", 0, Constants.LongNullValue).ToString());
        if (UserId.ToString().Length == 1)
        {
            strcode = "00000" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 2)
        {
            strcode = "0000" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 3)
        {
            strcode = "000" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 4)
        {
            strcode = "00" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 5)
        {
            strcode = "0" + UserId.ToString();
        }
        else
        {
            strcode = UserId.ToString();
        }
                      

        //2 Digit of Country ID
        var Tempcountid = drpCountry.SelectedValue;
        var country = Tempcountid.Substring(Math.Max(0, Tempcountid.Length - 2));
        if (country.Length == 1)
        {
            country = "0" + country;
        }
        //

        var CountID = "";
        var Tempcount = 0;
        for (int i = 0; i < chkColor.Items.Count; i++)
        {
            if (chkColor.Items[i].Selected == true)
            {
                Tempcount = Tempcount + 1;
               
            }
        }
        if (Tempcount.ToString().Length == 1)
        {
            CountID = "0" + Tempcount.ToString();
        }
        else
        {
            CountID = Tempcount.ToString();
        }
        #endregion


        string fileName = null;
        if (fuImage.HasFile)
        {
            string path = Server.MapPath("~/Pics");
            string fExtension = "";
            FileInfo oFileInfo = new FileInfo(fuImage.PostedFile.FileName);
            fExtension = Path.GetExtension(fuImage.FileName);
            fileName = "pic_" + Session["UserID"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fExtension;
            string fullFileName = path + "\\" + fileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuImage.PostedFile.SaveAs(fullFileName);
        }
       // string perPc = PieceType();
        string perPc ="";
        if (btnSave.Text == "Save")
        {
            string skuid = "";
            
            for (int i = 0; i < chkColor.Items.Count; i++)
            {
                if (chkColor.Items[i].Selected == true)
                {
                    if (CheckDublicateSku())
                    {
                        #region Bar code
                        //2 Digit of Color ID
                        var Color = "";
                        var ColorID = chkColor.Items[i].Value;
                         Color = ColorID.Substring(Math.Max(0, ColorID.Length - 2));
                         if (Color.Length == 1)
                         {
                             Color = "0" + Color;
                         }

                        txtbarcode.Text = country + CountID + Color + strcode;
                        #endregion

                        string tempskuid=_mSkuController.InsertSKUSZB(true,
                            int.Parse(drpPrincipal.SelectedValue), int.Parse(drpGender.SelectedValue),
                           int.Parse(drpCountry.SelectedValue), int.Parse(drpcategory.SelectedValue), int.Parse(drptype.SelectedValue), txtbarcode.Text.ToUpper(), txtstyleCode.Text, txtskuname.Text,
                            int.Parse(chkColor.Items[i].Value), Constants.IntNullValue, int.Parse(DrpSize.SelectedValue), txtBrandName.Text, int.Parse(DrpSeason.SelectedValue),
                            short.Parse(txtUnitInCase.Text),
                            int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()),
                              fileName, cbDisplay.Checked, txtRemarks.Text, chkColor.Items[i].Text);
                        if (tempskuid == "-1")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                  "alert('Item Style Code  " + txtstyleCode.Text + "  already exists !.');", true);
                            return;
                        }
                        skuid = skuid + "," + tempskuid;
                        //if (skuid.Length > 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        //   "alert('Data Updated !.');", true);
                        //}
                        // LoadData();
                    }
                }
            }
            if (skuid.Length > 0)
            {
                mAutoCode.GetAutoCustomerCode("SKU", 1, long.Parse(UserId.ToString()));
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                   "alert('Sku ID's "+ skuid+" Inserted !.');", true);
            }
            ClearAll();
            LoadData();
            LoadGrid();

        }
    
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
    //                "alert('Item already exist of that Barcode.');", true);
    //        }
    //    }
        else if (btnSave.Text == "Update")
        {
            if (CheckDublicateSku2())
            {
                var skuid = _mSkuController.UpdateSKUSZB(int.Parse(HdnSku_ID.Value), true,
                             int.Parse(drpPrincipal.SelectedValue), int.Parse(drpGender.SelectedValue),
                            int.Parse(drpCountry.SelectedValue), int.Parse(drpcategory.SelectedValue), int.Parse(drptype.SelectedValue),null, txtstyleCode.Text.ToUpper(), txtskuname.Text,
                             Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpSize.SelectedValue), txtBrandName.Text, int.Parse(DrpSeason.SelectedValue),
                             short.Parse(txtUnitInCase.Text),
                             int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()),
                               fileName, cbDisplay.Checked, txtRemarks.Text, null);
               if (skuid.Length > 0)
               {
                   ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                  "alert('Sku ID " + skuid + " Updated !.');", true);
                   ClearAll();
                   LoadData();
                   LoadGrid();
                  
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                 "alert('Please Try Again !');", true);
               }

            }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
            //                "alert('Item already exist of that Barcode.');", true);
            //        }
        }
    //    imgProduct.ImageUrl = "~/images/noimage.jpg";
    //    cbDisplay.Checked = false;
   
    }


    protected void BtnGenerateCode_Click(object sender, EventArgs e)
    {
        if (drpGender.Items.Count > 0 && drpcategory.Items.Count > 0 && drptype .Items .Count >0)
        {
            txtskuname.Text = drpGender.SelectedItem.Text + " " + drpcategory.SelectedItem.Text + " "+ drptype.SelectedItem.Text +" "+ txtBrandName.Text;
        }
        else
        {
            txtskuname.Text = txtBrandName.Text;
        }


    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }

    //#endregion

    private void ClearAll()
    {
        chkColor.Enabled = true;
        txtstyleCode.Text = "";
        txtskuname.Text = "";
        txtbarcode.Text = "";
        txtRemarks.Text = "";
        txtUnitInCase.Text = "";
        btnSave.Text = "Save";
        LoadColor();

        imgProduct.ImageUrl = "~/images/noimage.jpg";
    }

    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        LoadGrid();
    }
}