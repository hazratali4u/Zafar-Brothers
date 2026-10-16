using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmDistributorCustomer : System.Web.UI.Page
{
    GeoHierarchyController GControler = new GeoHierarchyController();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadVolumeType();
            LoadRegion();
            LoadTerritory();
            LoadZone();
            LoadUser();

            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtRegdate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    protected void LoadZone()
    {
        if (drpTerritory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpZone, dt, 0, 4, true);
        }
    }
    private void LoadUser()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(Constants.SALES_FORCE_SALESPERSON, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpsalePerson, dt, 0, 6, true);
        }
    }
    protected void LoadRegion()
    {
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        
        clsWebFormUtil.FillDropDownList(this.drpRegion, dt, 0, 4, true);
       
    }

    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.LoadTerritory();
        LoadZone();
    }
    protected void LoadTerritory()
    {
        if (drpRegion.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpRegion.SelectedValue), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));

            clsWebFormUtil.FillDropDownList(this.drpTerritory, dt, 0, 4, true);
        }
    }


    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));



        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }


    /// <summary>
    /// Loads Towns To Town Combo, Routes To Routes Comb And Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
        SetTableSorter();
    }



    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {

        SetTableSorter();
    }

    private void LoadVolumeType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerVolumeClassType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpVolumeClass, dt, 0, 2, true);
        DrpVolumeClass.SelectedValue = "88";
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {

        CustomerDataController mController = new CustomerDataController();
        DataTable dt = mController.UspSelectCustomer(int.Parse(DrpDistributor.SelectedValue), ddSearchType.SelectedValue.ToString(), txtSeach.Text);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                Grid_users.EditIndex = -1;
                Grid_users.DataSource = dt;
                Grid_users.DataBind();

                lblNoRecords.Visible = false;
            }
            else
            {
                lblNoRecords.Visible = true;
            }
        }

    }

    /// <summary>
    /// Sets Customer Data For Edit. This Function Runs When An Existing Customer Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Session.Add("CustomerId", long.Parse(Grid_users.Rows[e.NewEditIndex].Cells[0].Text));
        btnSave.Text = "Update";
        DrpDistributor.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[1].Text;
        // DrpBusinessType.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[2].Text;
        DrpVolumeClass.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[3].Text;

        txtCustomerName.Text = Grid_users.Rows[e.NewEditIndex].Cells[9].Text.Replace("amp;", "");
        //  txtContactPerson.Text = Grid_users.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        txtPhoneNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
        txtEmailAddress.Text = Grid_users.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");
        txtAddress.Text = Grid_users.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
        // txtIsRegister.Text = Grid_users.Rows[e.NewEditIndex].Cells[14].Text.Replace("&nbsp;", "");
        if (Grid_users.Rows[e.NewEditIndex].Cells[14].Text.Trim() == "&nbsp;")
        {
            //   txtIsRegister.Text = "";
            //  ChbIsRegister.Checked = false;
        }
        else
        {
            //  ChbIsRegister.Checked = true;
            // txtIsRegister.Text = Grid_users.Rows[e.NewEditIndex].Cells[14].Text;
        }
        chkIsActive.Checked = bool.Parse(Grid_users.Rows[e.NewEditIndex].Cells[19].Text);
        txtRegdate.Text = Grid_users.Rows[e.NewEditIndex].Cells[20].Text.Replace("&nbsp;", "");
        txtCNIC.Text = Grid_users.Rows[e.NewEditIndex].Cells[23].Text.Replace("&nbsp;", "");
        //  txtNTN.Text = Grid_users.Rows[e.NewEditIndex].Cells[24].Text.Replace("&nbsp;", "");
        txtCreditLimit.Text = Grid_users.Rows[e.NewEditIndex].Cells[25].Text.Replace("&nbsp;", "0");
        txtCreditDays.Text = Grid_users.Rows[e.NewEditIndex].Cells[26].Text.Replace("&nbsp;", "0");
        txtMobileNo. Text = Grid_users.Rows[e.NewEditIndex].Cells[27].Text.Replace("&nbsp;", "");
        txtContactperson.Text = Grid_users.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
       
        try
        {
           
            drpRegion.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[29].Text;
            LoadTerritory();
            drpTerritory.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[28].Text;
            LoadZone();
            DrpClassification.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[30].Text.Replace("&nbsp;", "0");
            DrpCustomerType .SelectedValue= Grid_users.Rows[e.NewEditIndex].Cells[31].Text.Replace("&nbsp;", "0");
            DrpsalePerson.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[32].Text;
            DrpZone.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[33].Text;
            for (int i = 0; i < Grid_users.Rows.Count; i++)
            {
                Grid_users.Rows[i].Cells[34].Enabled = false;

            }
        }
        catch (Exception ex)
        {
        }

       
        SetTableSorter();
        DrpDistributor.Enabled = false;
    }

 
    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(DrpZone.Items.Count>0)
        { }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Zone Not found !');", true);
            return; }
        CustomerDataController mController = new CustomerDataController();
        int SaleperonsID = Constants.IntNullValue;
        if (DrpsalePerson.Items.Count > 0)
        {
            SaleperonsID = int.Parse(DrpsalePerson.SelectedValue);
        }
        if (btnSave.Text == "Save")
        {
            SETTINGS_TABLE_Controller mSettingsTableControl = new SETTINGS_TABLE_Controller();
            DataTable dtSettingsTable = mSettingsTableControl.Select_SETTINGS_TABLE("CUSTOMER", "CUSTOMER_ID", 0);

            if (dtSettingsTable.Rows.Count > 0)
            {
                long CustomerId = long.Parse(dtSettingsTable.Rows[0]["Value"].ToString()) + 1;
                string StrCode = "";

                if (CustomerId.ToString().Length == 1)
                {
                    StrCode = "OT0000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 2)
                {
                    StrCode = "OT000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 3)
                {
                    StrCode = "OT00" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 4)
                {
                    StrCode = "OT0" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 5)
                {
                    StrCode = "OT" + CustomerId.ToString();
                }
                if (txtCreditLimit.Text == "")
                {
                    txtCreditLimit.Text = "0";
                }
             
               
                    mController.InsertCustomer(CustomerId, false, chkIsActive.Checked, Constants.IntNullValue,
                    int.Parse(DrpVolumeClass.SelectedValue.ToString()),
                    Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                    int.Parse(DrpDistributor.SelectedValue), "", txtContactperson.Text, txtPhoneNo.Text, txtEmailAddress.Text, StrCode, txtCustomerName.Text,
                    txtAddress.Text, DateTime.Parse(txtRegdate.Text), 1, 1, txtCNIC.Text, "", decimal.Parse(txtCreditLimit.Text),int.Parse(txtCreditDays.Text),int.Parse(drpRegion.SelectedValue), int.Parse(drpTerritory.SelectedValue), txtMobileNo.Text,int.Parse(DrpClassification .SelectedValue), int.Parse (DrpCustomerType .SelectedValue), SaleperonsID, int.Parse(DrpZone.SelectedValue));
                Session.Add("CustomerId", CustomerId);

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Insert');", true);

            }
        }
        else
        {
            mController.UpdateCustomer(long.Parse(Session["CustomerId"].ToString()), false, chkIsActive.Checked,
            Constants.IntNullValue, int.Parse(DrpVolumeClass.SelectedValue.ToString()),
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue), "", txtContactperson.Text,
            txtPhoneNo.Text, txtEmailAddress.Text, null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue, 1, 1, txtCNIC.Text, "", decimal.Parse(txtCreditLimit.Text)
            , int.Parse(txtCreditDays.Text), int.Parse(drpRegion.SelectedValue), int.Parse(drpTerritory.SelectedValue), txtMobileNo.Text,int.Parse(DrpClassification.SelectedValue),int.Parse(DrpCustomerType .SelectedValue),SaleperonsID,int.Parse(DrpZone.SelectedValue));

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Update');", true);
        }


        ClearAll();
    }


    /// <summary>
    /// Clears All Controls Through ClearAll() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    /// <summary>
    /// Filters Customer From Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadCustomer();
        SetTableSorter();
    }

    /// <summary>
    /// Set Customer Grid For JQuery Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Clears All  Constrols
    /// </summary>
    private void ClearAll()
    {
        txtCustomerName.Text = "";
          txtContactperson.Text = "";
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtSeach.Text = "";
        // txtIsRegister.Text = "";
        //  txtNTN.Text = string.Empty;
        txtCNIC.Text = string.Empty;
        txtEmailAddress.Text = "";
        txtCreditLimit.Text = "";

        txtCreditDays.Text = "";
        txtMobileNo.Text = "";

        btnSave.Text = "Save";
        Grid_users.DataSource = null;
        Grid_users.DataBind();
        DrpDistributor.Enabled = true;
        DrpClassification.SelectedIndex = 0;
    }



    protected void drpTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadZone();
    }
}
