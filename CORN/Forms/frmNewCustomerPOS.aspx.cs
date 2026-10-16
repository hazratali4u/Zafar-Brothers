using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmNewCustomerPOS : System.Web.UI.Page
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
         //   LoadDistributor();
            //LoadTown();
            // LoadRoute();
            // LoadMarket();
            // LoadChannelType();
            //  LoadBusinessType();
            LoadVolumeType();

            txtCreditLimit.Attributes.Add("readonly", "readonly");

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
             txtRegdate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
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
            DataTable dt = mController.UspSelectCustomer(Constants.IntNullValue, ddSearchType.SelectedValue.ToString(), txtSeach.Text);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
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

        //DrpDistributor.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[1].Text;
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
        btnSave.Text = "Update";
        SetTableSorter();
    }

    /// <summary>
    /// Sets PageIndex Of Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
  

    /// <summary>
    /// Enables GST No TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    //protected void ChbIsRegister_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (ChbIsRegister.Checked == true)
    //    {
    //        txtIsRegister.Text = "";
    //        txtIsRegister.Enabled = true;
    //    }
    //    else
    //    {
    //        txtIsRegister.Text = "";
    //        txtIsRegister.Enabled = false;
    //    }
    //    SetTableSorter();
    //}

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerDataController mController = new CustomerDataController();

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
                mController.InsertCustomer(CustomerId, false, chkIsActive.Checked, Constants.IntNullValue, int.Parse(DrpVolumeClass.SelectedValue.ToString()),
                    Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                    0, "", "", txtPhoneNo.Text, txtEmailAddress.Text, StrCode, txtCustomerName.Text,
                    txtAddress.Text, DateTime.Parse(txtRegdate.Text), 1, 1, txtCNIC.Text, "", decimal.Parse(txtCreditLimit.Text));
                Session.Add("CustomerId", CustomerId);
                
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Insert');", true);
               
            }
        }
        else
        {
            mController.UpdateCustomer(long.Parse(Session["CustomerId"].ToString()), false, chkIsActive.Checked,
            Constants.IntNullValue, int.Parse(DrpVolumeClass.SelectedValue.ToString()),
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, 0, "", "",
            txtPhoneNo.Text, txtEmailAddress.Text, null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue, 1, 1, txtCNIC.Text, "", decimal.Parse(txtCreditLimit.Text));
           
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
        //  txtContactPerson.Text = "";
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtSeach.Text = "";
        // txtIsRegister.Text = "";
        //  txtNTN.Text = string.Empty;
        txtCNIC.Text = string.Empty;
        txtEmailAddress.Text = "";
        txtCreditLimit.Text = "";
        btnSave.Text = "Save";
        Grid_users.DataSource = null;
        Grid_users.DataBind();

    }
   
   
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmOrderPOS.aspx");
    }
}
