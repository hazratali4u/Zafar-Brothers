using System;
using System.Data;
using System.IO;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form To Import Route, Market,  Customer, Item And Item Price Data From Text Files
/// </summary>
public partial class frmImportPhysicalStock : System.Web.UI.Page
{
    CustomerDataController mCustomer = new CustomerDataController();

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadToDistributor();
        }
    }
   
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt= mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }

    private void LoadToDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlTransferTo, dt, 0, 2, true);
    }

    /// <summary>
    /// Imports File Data To Database
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Text = string.Empty;
        if (ddlType.SelectedValue == "5")
        {
            if (DrpDistributor.SelectedValue.ToString() == ddlTransferTo.SelectedValue.ToString())
            {
                lblErrorMessage.Text = "Transfer to Location must be different.";
                return;
            }
        }

        #region FileValidation
        if (txtFile.PostedFile.ContentLength == 0)
        {
            lblErrorMessage.Text = "Please select a file and then upload";
            return;
        }
        else if (txtFile.PostedFile.ContentType != "text/plain")
        {
            lblErrorMessage.Text = "Only text file are supported";
            return;
        }
        #endregion
        bool flag=true;
        try
        {
            if (!Directory.Exists(Constants.fldOtherDataFolder))
            {
                Directory.CreateDirectory(Constants.fldOtherDataFolder);
            }

            string path = System.IO.Path.GetFullPath(txtFile.PostedFile.FileName);
            string filename = path.Substring(path.LastIndexOf('\\'), path.Length - path.LastIndexOf('\\'));
            if (File.Exists(Constants.fldOtherDataFolder + filename))
            {
                lblErrorMessage.Text = "File already Exist in folder. Save file with other name";
                return;
            }
            else
            {
                txtFile.PostedFile.SaveAs(Constants.fldOtherDataFolder + filename);
                path = Constants.fldOtherDataFolder + filename;
                this.lblErrorMessage.Text = "";
                PurchaseController PurCtl = new PurchaseController();
                DistributorController DistrCtl = new DistributorController();
                if (ddlType.SelectedValue == "1")
                {
                    flag = PurCtl.ImportPhysicalStock(int.Parse(DrpDistributor.SelectedValue.ToString()), path, Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]));
                }
                else
                {
                    int TransferTo = Convert.ToInt32(DrpDistributor.SelectedValue);
                    if (ddlType.SelectedValue == "5")
                    {
                        TransferTo = Convert.ToInt32(ddlTransferTo.SelectedValue);
                    }
                    flag = PurCtl.ImportStock(int.Parse(DrpDistributor.SelectedValue.ToString()), path, Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), TransferTo, Convert.ToInt32(ddlType.SelectedValue));
                }
                if (!flag)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record insert successfully.');", true);
                LoadDistributor();
            }
        }
        catch (Exception excp)
        {
            lblErrorMessage.Text = excp.ToString();
            return;
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        trTransfer.Visible = false;
        if(ddlType.SelectedValue == "5")
        {
            trTransfer.Visible = true;
        }
    }
}
