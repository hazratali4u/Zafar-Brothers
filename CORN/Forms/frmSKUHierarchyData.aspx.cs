using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete Item Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{
    readonly SkuHierarchyController _mController = new SkuHierarchyController();
   readonly SETTINGS_TABLE_Controller _autoCode = new SETTINGS_TABLE_Controller(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            LoadPrincipal();
            LoadGender();
          
            LoadCategroy();
           
            LoadCountry();
            LoadCountryGrid();
          
            LoadColor();
       
            LoadType();

            LoadSeason();
           
            LoadSize();
            TabContainer1.ActiveTabIndex = 0;
            
        }
    }
    #region Principal Tab

    /// <summary>
    /// Loads Principals To Principal Grid On Principal Tab And To All Principal Combos On Form
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable dt = _mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(Session["CompanyId"].ToString()));
        GrdPrincipal.EditIndex = -1;
        GrdPrincipal.DataSource = dt;
        GrdPrincipal.DataBind();

    }

  

    /// <summary>
    /// Sets Principal Data For Edit. This Function Runs When An Existing Principal Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFPrincipalID.Value = (GrdPrincipal.Rows[e.NewEditIndex].Cells[0].Text);
        txtPrincipalCode.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[1].Text;
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[2].Text.Replace("&nbsp;", ""); ;
        //ChIsMunalDiscount.Checked = GrdPrincipal.Rows[e.NewEditIndex].Cells[3].Text;


        txtAddress.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtNTN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[5].Text;
        txtSTRN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[6].Text;
        if (GrdPrincipal.Rows[e.NewEditIndex].Cells[7].Text != "")
        {
            drpCountry.SelectedValue = GrdPrincipal.Rows[e.NewEditIndex].Cells[7].Text;
        }

        txtEmail.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", ""); ;
        txtPhoneNo.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", ""); ;
        txtSkype.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", ""); ;
        txtContactPerson.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", ""); ;

        txtPrincipalName.Enabled = true;
        txtAddress.Enabled = true;
        txtNTN.Enabled = true;
        txtSTRN.Enabled = true;
        txtPrincipalCode.Enabled = true;
        txtEmail.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtSkype.Enabled = true;
        txtContactPerson.Enabled = true;
        btnSavePrincipal.Text = "Update";
        for (int i = 0; i < GrdPrincipal.Rows.Count; i++)
        {
            GrdPrincipal.Rows[i].Cells[13].Enabled = false;
            GrdPrincipal.Rows[i].Cells[14].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HFPrincipalID.Value = (GrdPrincipal.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUDivision, int.Parse(HFPrincipalID.Value));
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsg.Text = "Wrong Command: first delete associated division";
        //}
        //else
        //{
            _mController.UpdateHierarchy(Constants.SKUPrincipal, int.Parse(HFPrincipalID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
            LoadPrincipal();
            lblErrorMsgPrincipal.Text = "";
        //}
    }

    /// <summary>
    /// Sets PageIndex Of Principal Grid On Principal Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdPrincipal.PageIndex = e.NewPageIndex;
        LoadPrincipal();

    }

    /// <summary>
    /// Save Or Updates a Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {
        lblErrorMsgPrincipal.Text = "";
        if (btnSavePrincipal.Text == "New")
        {
            txtPrincipalCode.Text = GetAutoCode("SC", 0);
            txtPrincipalName.Enabled = true;
            txtAddress.Enabled = true;
            txtNTN.Enabled = true;
            txtSTRN.Enabled = true;
            txtPrincipalCode.Enabled = true;
            txtEmail.Enabled = true;
            txtContactPerson.Enabled = true;
            txtPhoneNo.Enabled = true;
            txtSkype.Enabled = true;

            txtPrincipalCode.Focus();
            btnSavePrincipal.Text = "Save";
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(txtPrincipalCode);
        }
        else if (btnSavePrincipal.Text == "Save")
        {
            if (txtPrincipalName.Text.Length == 0 || txtPrincipalCode.Text.Length == 0)
            {
                lblErrorMsgPrincipal.Text = "Must Enter Supplier Name and Code";
                return;
            }
            _mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtNTN.Text, txtSTRN.Text
                , int.Parse(drpCountry.SelectedValue), txtEmail.Text, txtPhoneNo.Text, txtSkype.Text, txtContactPerson.Text);
            SetAutoCode("SC", long.Parse(txtPrincipalCode.Text.Substring(2)));
            ClearPrincipal();
            LoadPrincipal();

        }
        else if (btnSavePrincipal.Text == "Update")
        {
            if (txtPrincipalName.Text.Length == 0)
            {
                lblErrorMsgPrincipal.Text = "Must Enter Supplier Name";
                return;
            }
            _mController.UpdatePrincipal(Constants.SKUPrincipal, int.Parse(HFPrincipalID.Value), Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtNTN.Text, txtSTRN.Text
                , int.Parse(drpCountry.SelectedValue), txtEmail.Text, txtPhoneNo.Text, txtSkype.Text, txtContactPerson.Text);
            ClearPrincipal();
            LoadPrincipal();
        }

    }

    private void ClearPrincipal()
    {
        btnSavePrincipal.Text = "New";
        txtPrincipalCode.Text = "";
        txtPrincipalName.Text = "";
        txtAddress.Text = "";
        txtNTN.Text = "";
        txtSTRN.Text = "";
        txtEmail.Text = "";
        txtPhoneNo.Text = "";
        txtSkype.Text = "";
        txtContactPerson.Text = "";
        txtPrincipalName.Enabled = false;
        txtAddress.Enabled = false;
        txtNTN.Enabled = false;
        txtSTRN.Enabled = false;
        txtEmail.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtSkype.Enabled = false;
        txtContactPerson.Enabled = false;

        txtPrincipalCode.Enabled = false;
        LoadCountry();
    }

    #endregion
    #region Country Tab

    protected void LoadCountry()
    {

     //   DataTable dt = _mskuController.SelectCountry(0);
        DataTable dt = _mController.SelectSkuHierarchy_ZB(Constants.SKUCountry, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
          
        clsWebFormUtil.FillDropDownList(drpCountry, dt, 1, 4, true);
    }
  
    #endregion
    #region Gender Tab

    /// <summary>
    /// Loads Divisions To Division Grid On Division Tab
    /// </summary>
    private void LoadGender()
    {
        DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue,Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        GrdGender.EditIndex = -1;
        GrdGender.DataSource = dt;
       // GrdGender.Columns[0].Visible = false;
        GrdGender.DataBind();
       // GrdGender.Columns[0].Visible = true;

    }

    /// <summary>
    /// Sets Division Data For Edit. This Function Runs When An Existing Division Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdGender_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFGenderID.Value = (GrdGender.Rows[e.NewEditIndex].Cells[0].Text);
        txtGenderCode.Text = GrdGender.Rows[e.NewEditIndex].Cells[1].Text;
        txtGenderName.Text = GrdGender.Rows[e.NewEditIndex].Cells[2].Text;
        txtGenderName.Enabled = true;
        txtGenderCode.Enabled = true;
        btnSaveGender.Text = "Update";
        for (int i = 0; i < GrdGender.Rows.Count; i++)
        {
            GrdGender.Rows[i].Cells[3].Enabled = false;
            GrdGender.Rows[i].Cells[4].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdGender_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HFGenderID .Value = (GrdGender.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUGender, int.Parse(HFGenderID .Value));
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsgGender.Text = "Wrong Command: first delete associated category";
        //}
        //else
        //{
            _mController.UpdateHierarchy(Constants.SKUGender, int.Parse(HFGenderID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
            LoadGender();
            lblErrorMsgGender.Text = "";
        //}
    }

    /// <summary>
    /// Sets PageIndex Of Division Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdGender_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdGender.PageIndex = e.NewPageIndex;
        LoadGender();
    }

    /// <summary>
    /// Save Or Updates a Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveGender_Click(object sender, EventArgs e)
    {
        if (btnSaveGender.Text == "New")
        {
           txtGenderCode.Text = GetAutoCode("GEN", 0);
            txtGenderName.Enabled = true;
            txtGenderCode.Enabled = true;
            txtGenderName.Focus();
            btnSaveGender.Text = "Save";
          //  ScriptManager.GetCurrent(Page).SetFocus(dddivisonPrincipal);
        }
        else if (btnSaveGender.Text == "Save")
        {
            if (txtGenderCode.Text.Length == 0 || txtGenderName.Text.Length == 0)
            {
                lblErrorMsgGender.Text = "Must Entry Gender Name or Code";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUGender, Constants.IntNullValue, txtGenderCode.Text, txtGenderName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            SetAutoCode("GEN", long.Parse(txtGenderCode.Text.Substring(3)));
            btnSaveGender.Text = "New";
            txtGenderCode.Text = "";
            txtGenderName.Text = "";
            
            txtGenderName.Enabled = false;
            LoadGender();
            lblErrorMsgGender.Text = "";
          
        }
        else if (btnSaveGender.Text == "Update")
        {
            if (txtGenderName.Text.Length == 0 || txtGenderCode.Text.Length == 0)
            {
                lblErrorMsgGender.Text = "Must Enter Gender Name or Code";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUGender, int.Parse(HFGenderID.Value), Constants.IntNullValue, txtGenderCode.Text, txtGenderName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            btnSaveGender.Text = "New";
            txtGenderName.Text = "";
           txtGenderCode.Text = "";
            txtGenderCode.Enabled = false;
            txtGenderName.Enabled = false;
            LoadGender();
        
        }

    }

    #endregion
    #region Category Tab

    private void LoadCategroy()
    {
       // if (ddCategoryDivision.Items.Count > 0)
        //{
        DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        GrdCategory.EditIndex = -1;
        GrdCategory.DataSource = dt;
           
            GrdCategory.DataBind();
            
        //}
    }

    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        HFCategoryID.Value = (GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        //ddCategoryDivision.SelectedValue = GrdCategory.Rows[e.NewEditIndex].Cells[3].Text;
        txtCategoryName.Enabled = true;
        txtCategoryCode.Enabled = true;
        btnSaveCategory.Text = "Update";
        for (int i = 0; i < GrdCategory.Rows.Count; i++)
        {
            GrdCategory.Rows[i].Cells[4].Enabled = false;
            GrdCategory.Rows[i].Cells[5].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HFCategoryID.Value =(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUSubCategory, int.Parse(HFCategoryID.Value));
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsgCategory.Text = "Wrong Command: first delete associated Sub Category";
        //}
        //else
        //{
            _mController.UpdateHierarchy(Constants.SKUCategory, int.Parse(HFCategoryID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
            LoadCategroy();
       // }
    }

    
    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCategory.PageIndex = e.NewPageIndex;
        LoadCategroy();
    }

    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        lblErrorMsgCategory.Text = "";
        if (btnSaveCategory.Text == "New")
        {
            txtCategoryCode.Text = GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryCode.Enabled = true;
            txtCategoryCode.Focus();
            btnSaveCategory.Text = "Save";
           // ScriptManager.GetCurrent(Page).SetFocus(ddCategoryDivision);
        }
        else if (btnSaveCategory.Text == "Save")
        {
            if (txtCategoryName.Text.Length <= 0 || txtCategoryCode.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category NAme or Code";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUCategory,Constants.IntNullValue, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            txtCategoryCode.Enabled = false;
            LoadCategroy();
            lblErrorMsgCategory.Text = "";
      
        }
        else if (btnSaveCategory.Text == "Update")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUCategory, int.Parse(HFCategoryID.Value), Constants.IntNullValue, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryCode.Enabled = false;
            txtCategoryName.Enabled = false;
            LoadCategroy();
         
        }
    }

    #endregion
    #region Type Tab

  
    private void LoadType()
    {
       
            DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUType, Constants.IntNullValue,Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        GrdType.EditIndex = -1;
            GrdType.DataSource = dt;
            GrdType.DataBind();

    }


    protected void GrdType_RowEditing(object sender, GridViewEditEventArgs e)
    {

        HFTypeID.Value = (GrdType.Rows[e.NewEditIndex].Cells[0].Text);
        txtTypeCode.Text = GrdType.Rows[e.NewEditIndex].Cells[1].Text;
        txtTypeName.Text = GrdType.Rows[e.NewEditIndex].Cells[2].Text;
     
        txtTypeName.Enabled = true;
        txtTypeCode.Enabled = true;
        for (int i = 0; i < GrdType.Rows.Count; i++)
        {
            GrdType.Rows[i].Cells[4].Enabled = false;
            GrdType.Rows[i].Cells[5].Enabled = false;

        }
        btnSaveType.Text = "Update";
    }


    protected void GrdType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HFTypeID.Value = (GrdType.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUCategory, _subCategoryId);
        // if (dt.Rows.Count > 0)
        //  {
        //     lblErrorMsgCategory.Text = "Wrong Command: first delete associated Category";
        //  }
        //   else
        //  {
        _mController.UpdateHierarchy(Constants.SKUType, int.Parse(HFTypeID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
        
        LoadType();
        // }
    }


    protected void btnSaveType_Click(object sender, EventArgs e)
    {
        lblErrorMsgType.Text = "";
        if (btnSaveType.Text == "New")
        {
             txtTypeCode.Text = GetAutoCode("TYP", 0);
            txtTypeName.Enabled = true;
            txtTypeCode.Enabled = true;
            txtTypeName.Focus();
            btnSaveType.Text = "Save";
            // ScriptManager.GetCurrent(Page).SetFocus(ddCategoryDivision);
        }
        else if (btnSaveType.Text == "Save")
        {
            if (txtTypeName.Text.Length <= 0 || txtTypeCode.Text.Length <= 0)
            {
                lblErrorMsgType.Text = "Must Enter Type Name or Code";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUType, Constants.IntNullValue, txtTypeCode.Text, txtTypeName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            SetAutoCode("TYP", long.Parse(txtTypeCode.Text.Substring(3)));
            btnSaveType.Text = "New";
            txtTypeCode.Text = "";
            txtTypeName.Text = "";
            txtTypeName.Enabled = false;
            txtTypeCode.Enabled = false;

            LoadType();
            lblErrorMsgType.Text = "";
          
        }
        else if (btnSaveType.Text == "Update")
        {
            if (txtTypeName.Text.Length <= 0 || txtTypeCode.Text.Length <= 0)
            {
                lblErrorMsgType.Text = "Must Enter Type Name or Code";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUType, int.Parse(HFTypeID.Value), Constants.IntNullValue, txtTypeCode.Text, txtTypeName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            btnSaveType.Text = "New";
            txtTypeCode.Text = "";
            txtTypeName.Text = "";
            txtTypeCode.Enabled = false;
            txtTypeName.Enabled = false;
            LoadType();
            lblErrorMsgType.Text = "";
        }
    }

    #endregion
    #region Country Tab



    private void LoadCountryGrid()
    {
        // if (ddBrandCategory.Items.Count > 0)
      //  {
            DataTable dt = _mController.SelectSkuHierarchy_ZB(Constants.SKUCountry, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()),Constants.IntNullValue);
        grdColor.EditIndex = -1;
        GrdCountry.DataSource = dt;
          //  GrdCountry.Columns[0].Visible = true;
            GrdCountry.DataBind();
          //  GrdCountry.Columns[0].Visible = false;
        //}
    }


    protected void GrdCountry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFCountryID.Value = (GrdCountry.Rows[e.NewEditIndex].Cells[0].Text);
        HFCountryTypeID.Value = (GrdCountry.Rows[e.NewEditIndex].Cells[1].Text);
        txtCountryCode.Text = GrdCountry.Rows[e.NewEditIndex].Cells[2].Text;
        txtCountryName.Text = GrdCountry.Rows[e.NewEditIndex].Cells[3].Text;
        txtCountryName.Enabled = true;
        txtCountryCode.Enabled = true;
        btnSaveCountry.Text = "Update";
        for (int i = 0; i < GrdCountry.Rows.Count; i++)
        {
            GrdCountry.Rows[i].Cells[5].Enabled = false;
            GrdCountry.Rows[i].Cells[6].Enabled = false;

        }
    }


    protected void GrdCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       // SkuController msku = new SkuController();
        HFCountryID.Value = (GrdCountry.Rows[e.RowIndex].Cells[0].Text);
        HFCountryTypeID.Value = (GrdCountry.Rows[e.RowIndex].Cells[1].Text);
       // DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(HFCountryID.Value), Constants.IntNullValue);
       // if (dt.Rows.Count > 0)
       /// {
         //   lblErrorMsgCountry.Text = "Wrong Command: first delete associated SKUS";
       // }
       // else
       // {
        _mController.UpdateHierarchy_ZB(Constants.SKUCountry, int.Parse(HFCountryID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()), int.Parse(HFCountryTypeID.Value));
            LoadCountryGrid();
       // }
    }

    protected void GrdCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCountry.PageIndex = e.NewPageIndex;
        LoadCountryGrid();
    }

    protected void btnSaveCountry_Click(object sender, EventArgs e)
    {
        lblErrorMsgCountry.Text = "";
        if (btnSaveCountry.Text == "New")
        {
            txtCountryCode.Text = GetAutoCode("CON", 0);
            txtCountryName.Enabled = true;
            txtCountryCode.Enabled = true;
            txtCountryName.Focus();
            btnSaveCountry.Text = "Save";
            //ScriptManager.GetCurrent(Page).SetFocus(ddBrandCategory);
        }
        else if (btnSaveCountry.Text == "Save")
        {
            if (txtCountryName.Text.Length == 0 ||txtCountryCode.Text.Length == 0 )
            {
                lblErrorMsgCountry.Text = "Must Enter Country Name or Code";
                return;
            }
            _mController.InsertHierarchy_ZB(Constants.SKUCountry, Constants.IntNullValue, txtCountryCode.Text, txtCountryName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), int.Parse(txtCountryCode.Text.Substring(3)));
              SetAutoCode("CON", long.Parse(txtCountryCode.Text.Substring(3)));
            btnSaveCountry.Text = "New";
            txtCountryCode.Text = "";
            txtCountryName.Text = "";
            txtCountryCode.Enabled = false;
            txtCountryName.Enabled = false;
            
            lblErrorMsgCountry.Text = "";
           HFCountryID.Value="";
           LoadCountryGrid();
        }
        else if (btnSaveCountry.Text == "Update")
        {
            if (txtCountryName.Text.Length == 0|| txtCountryCode.Text.Length == 0)
            {
                lblErrorMsgCountry.Text = "Must Enter Country Name or code";
                return;
            }
            _mController.UpdateHierarchy_ZB(Constants.SKUCountry, int.Parse(HFCountryID.Value), Constants.IntNullValue, txtCountryCode.Text, txtCountryName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), int.Parse(HFCountryTypeID.Value));
            btnSaveCountry.Text = "New";
            txtCountryCode.Text = "";
            txtCountryName.Text = "";
            txtCountryCode.Enabled = false;
            txtCountryName.Enabled = false;
            LoadCountryGrid();
        }
    }

    #endregion
    #region Color

   
    private void LoadColor()
    {
        //if (drpTagType.Items.Count>0)
        {
            DataTable dt = _mController.SelectSkuHierarchy_ZB(Constants.SKUColor, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
            grdColor.EditIndex = -1;
            grdColor.DataSource = dt;
        //    grdColor.Columns[0].Visible = true;
            grdColor.DataBind();
          //  grdColor.Columns[0].Visible = false;
        }
    }


    protected void grdColor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFColorID.Value = (grdColor.Rows[e.NewEditIndex].Cells[0].Text);
        HfColorTypeID.Value = (grdColor.Rows[e.NewEditIndex].Cells[4].Text);
        txtColorCode.Text = grdColor.Rows[e.NewEditIndex].Cells[2].Text;
        txtColorName.Text = grdColor.Rows[e.NewEditIndex].Cells[3].Text;
        txtColorCode.Enabled = true;
        txtColorName.Enabled = true;
        btnSaveColor.Text = "Update";
        for (int i = 0; i < grdColor.Rows.Count; i++)
        {
            grdColor.Rows[i].Cells[5].Enabled = false;
            grdColor.Rows[i].Cells[6].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdColor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        HFColorID.Value = (grdColor.Rows[e.RowIndex].Cells[0].Text);
        HfColorTypeID.Value = (grdColor.Rows[e.RowIndex].Cells[4].Text);
        //DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, _tagId, Constants.IntNullValue);
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsgBrand.Text = "Wrong Command: first delete associated SKUS";
        //}
        //else
        //{
        _mController.UpdateHierarchy_ZB(Constants.SKUColor, int.Parse(HFColorID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()), int.Parse(HfColorTypeID.Value));
            LoadColor();
        //}
    }

    /// <summary>
    /// Sets PageIndex Of Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void grdColor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdColor.PageIndex = e.NewPageIndex;
        LoadColor();
    }

    /// <summary>
    /// Save Or Updates a Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveColor_Click(object sender, EventArgs e)
    {
        lblErrorMsgColor.Text = "";
        if (btnSaveColor.Text == "New")
        {
            txtColorCode.Text = GetAutoCode("COL", 0);
            txtColorCode.Enabled = true;
            txtColorName.Focus();
            txtColorName.Enabled = true;
            btnSaveColor.Text = "Save";
           // ScriptManager.GetCurrent(Page).SetFocus(drpTagType);
        }
        else if (btnSaveColor.Text == "Save")
        {
            if (txtColorName.Text.Length == 0||txtColorCode.Text.Length==0)
            {
                lblErrorMsgColor.Text = "Must Enter Color name and Code";
                return;
            }
            _mController.InsertHierarchy_ZB(Constants.SKUColor, Constants.IntNullValue, txtColorCode.Text, txtColorName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), int.Parse(txtColorCode.Text.Substring(3)));
            SetAutoCode("COL", long.Parse(txtColorCode.Text.Substring(3)));
            btnSaveColor.Text = "New";
            txtColorCode.Text = "";
            txtColorName.Text = "";
            txtColorCode.Enabled = false;
            txtColorName.Enabled = false;
            LoadColor();
            lblErrorMsgColor.Text = "";
        }
        else if (btnSaveColor.Text == "Update")
        {
            if (txtColorName.Text.Length == 0 || txtColorCode.Text.Length == 0)
            {
                lblErrorMsgColor.Text = "Must Enter Color name and Code";
                return;
            }
            _mController.UpdateHierarchy_ZB(Constants.SKUColor, int.Parse(HFColorID.Value), Constants.IntNullValue, txtColorCode.Text, txtColorName.Text, null, true, int.Parse(Session["CompanyId"].ToString()), int.Parse(HfColorTypeID.Value));
            btnSaveColor.Text = "New";
            txtColorCode.Text = "";
            txtColorName.Text = "";
            txtColorCode.Enabled = false;
            txtColorName.Enabled = false;
            LoadColor();
            lblErrorMsgColor.Text = "";
        }
    }

    #endregion
    #region Season

    private void LoadSeason()
    {
        //if (drpTagType.Items.Count>0)
        {
            DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
            GrdSeason.EditIndex = -1;
            GrdSeason.DataSource = dt;
         //   GrdSeason.Columns[0].Visible = true;
            GrdSeason.DataBind();
          //  GrdSeason.Columns[0].Visible = false;
        }
    }
    protected void BtnSaveSeason_Click(object sender, EventArgs e)
    {
        lblErrorMsgSeason.Text = "";
        if (BtnSaveSeason.Text == "New")
        {
            txtSeasonCode.Text = GetAutoCode("SEN", 0);
            txtSeasonCode.Enabled = true;
            txtSeasonName.Focus();
            txtSeasonName.Enabled = true;
            BtnSaveSeason.Text = "Save";
        }
        else if (BtnSaveSeason.Text == "Save")
        {
            if (txtSeasonName.Text.Length == 0 || txtSeasonCode.Text.Length == 0)
            {
                lblErrorMsgSeason.Text = "Must Enter Season name or Code";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUSeason, Constants.IntNullValue, txtSeasonCode.Text, txtSeasonName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            SetAutoCode("SEN", long.Parse(txtSeasonCode.Text.Substring(3)));
            BtnSaveSeason.Text = "New";
            txtSeasonCode.Text = "";
            txtSeasonName.Text = "";
            txtSeasonCode.Enabled = false;
            txtSeasonName.Enabled = false;
            LoadSeason();
            lblErrorMsgSeason.Text = "";
        }
        else if (BtnSaveSeason.Text == "Update")
        {
            if (txtSeasonName.Text.Length == 0 || txtSeasonCode.Text.Length == 0)
            {
                lblErrorMsgSeason.Text = "Must Enter Season name or Code";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUSeason, int.Parse(HFSeasonID.Value), Constants.IntNullValue, txtSeasonCode.Text, txtSeasonName .Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            BtnSaveSeason.Text = "New";
            txtSeasonCode.Text = "";
            txtSeasonName.Text = "";
            txtSeasonCode.Enabled = false;
            txtSeasonName.Enabled = false;
            LoadSeason();
            lblErrorMsgSeason.Text = "";
            HFSeasonID.Value = "";
        }
    }
    protected void GrdSeason_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFSeasonID.Value = (GrdSeason.Rows[e.NewEditIndex].Cells[0].Text);
        txtSeasonCode.Text = GrdSeason.Rows[e.NewEditIndex].Cells[1].Text;
        txtSeasonName.Text = GrdSeason.Rows[e.NewEditIndex].Cells[2].Text;
        txtSeasonCode.Enabled = true;
        txtSeasonName.Enabled = true;
        BtnSaveSeason.Text = "Update";
        for (int i = 0; i < GrdSeason.Rows.Count; i++)
        {
            GrdSeason.Rows[i].Cells[3].Enabled = false;
            GrdSeason.Rows[i].Cells[4].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdSeason_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  SkuController msku = new SkuController();
        HFSeasonID.Value = (GrdSeason.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(HFFabricID.Value), Constants.IntNullValue);
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsgFabric.Text = "Wrong Command: first delete associated SKUS";
        //}
        //else
        //{
        _mController.UpdateHierarchy(Constants.SKUSeason, int.Parse(HFSeasonID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
        LoadSeason();
        // }
    }

    protected void GrdSeason_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdSeason.PageIndex = e.NewPageIndex;
        LoadSeason();
    }

    #endregion
    #region Size

    private void LoadSize()
    {
        //if (drpTagType.Items.Count>0)
        {
            DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUSize, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
            GrdSize.EditIndex = -1;
            GrdSize.DataSource = dt;
       //     GrdSize.Columns[0].Visible = true;
            GrdSize.DataBind();
       //     GrdSize.Columns[0].Visible = false;
        }
    }
    protected void BtnSaveSize_Click(object sender, EventArgs e)
    {
        lblErrorMsgSize.Text = "";
        if (BtnSaveSize.Text == "New")
        {
            txtSizeCode.Text = GetAutoCode("SIZ", 0);
            txtSizeCode.Enabled = true;
            txtSizeName.Focus();
            txtSizeName.Enabled = true;
            BtnSaveSize.Text = "Save";
        }
        else if (BtnSaveSize.Text == "Save")
        {
            if (txtSizeName.Text.Length == 0 || txtSizeCode.Text.Length == 0)
            {
                lblErrorMsgSize.Text = "Must Enter Size name or Code";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUSize, Constants.IntNullValue, txtSizeCode.Text, txtSizeName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            SetAutoCode("SIZ", long.Parse(txtSizeCode.Text.Substring(3)));
            BtnSaveSize.Text = "New";
            txtSizeCode.Text = "";
            txtSizeName.Text = "";
            txtSizeCode.Enabled = false;
            txtSizeName.Enabled = false;
            LoadSize();
            lblErrorMsgSize.Text = "";
        }
        else if (BtnSaveSize.Text == "Update")
        {
            if (txtSizeName.Text.Length == 0 || txtSizeCode.Text.Length == 0)
            {
                lblErrorMsgSize.Text = "Must Enter Size name or Code";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUSize, int.Parse(HFSizeID.Value), Constants.IntNullValue, txtSizeCode.Text, txtSizeName.Text, null, true, int.Parse(Session["CompanyId"].ToString()));
            BtnSaveSize.Text = "New";
            txtSizeCode.Text = "";
            txtSizeName.Text = "";
            txtSizeCode.Enabled = false;
            txtSizeName.Enabled = false;
            LoadSize();
            lblErrorMsgSize.Text = "";
            HFSizeID.Value = "";
        }
    }
    protected void GrdSize_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HFSizeID.Value = (GrdSize.Rows[e.NewEditIndex].Cells[0].Text);
        txtSizeCode.Text = GrdSize.Rows[e.NewEditIndex].Cells[1].Text;
        txtSizeName.Text = GrdSize.Rows[e.NewEditIndex].Cells[2].Text;
        txtSizeCode.Enabled = true;
        txtSizeName.Enabled = true;
        BtnSaveSize.Text = "Update";
        for (int i = 0; i < GrdSize.Rows.Count; i++)
        {
            GrdSize.Rows[i].Cells[3].Enabled = false;
            GrdSize.Rows[i].Cells[4].Enabled = false;

        }
    }

    /// <summary>
    /// Deletes A Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdSize_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  SkuController msku = new SkuController();
        HFSizeID.Value = (GrdSize.Rows[e.RowIndex].Cells[0].Text);
        //DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(HFFabricID.Value), Constants.IntNullValue);
        //if (dt.Rows.Count > 0)
        //{
        //    lblErrorMsgFabric.Text = "Wrong Command: first delete associated SKUS";
        //}
        //else
        //{
        _mController.UpdateHierarchy(Constants.SKUSize, int.Parse(HFSizeID.Value), Constants.IntNullValue, null, null, null, false, int.Parse(Session["CompanyId"].ToString()));
        LoadSize();
        // }
    }

    protected void GrdSize_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdSize.PageIndex = e.NewPageIndex;
        LoadSize();
    }

    #endregion
    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="codeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string preeFix,int codeType)
    {
        
        return _autoCode.GetAutoCode(preeFix, codeType, Constants.LongNullValue);
    }
    
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="cValue">Value</param>
    private void SetAutoCode(string preeFix, long cValue)
    {
        
        string result = _autoCode.GetAutoCode(preeFix, 1, cValue);
    }   
}