using System;
using System.Data;
using System.IO;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// Form To Import Route, Market,  Customer, Item And Item Price Data From Text Files
/// </summary>
public partial class Forms_frmImportData : System.Web.UI.Page
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
           // this.LoadTown();
            LoadDistributor();
            LoadPrincipal();
            cboFileTypes_SelectedIndexChanged(null, null);
        }
    }
   
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt= mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }

   
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
        
    /// <summary>
    /// Imports File Data To Database
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
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
                //cboFileTypes.SelectedIndex = 0;
                return;
            }
            else
            {
                txtFile.PostedFile.SaveAs(Constants.fldOtherDataFolder + filename);
                path = Constants.fldOtherDataFolder + filename;
                int index = cboFileTypes.SelectedIndex;
                this.lblErrorMessage.Text = "";

                if (cboFileTypes.SelectedValue == "2")// Customer
                {
                    CustomerDataController cus = new CustomerDataController();
                    flag = cus.ImportCustomer(Constants .IntNullValue,int.Parse(DrpDistributor.SelectedValue.ToString()), path);
                    if (!flag)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                        return;
                    }
                }



                if (cboFileTypes.SelectedValue == "3")
                {
                    SkuController SKUCtl = new SkuController();
                    flag = SKUCtl.ImportSKUS_ZB(int.Parse(DrpDistributor.SelectedValue.ToString()), path, int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(Session["UserID"]));
                   if (!flag)
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                       return;
                   }
                }
                else if (cboFileTypes.SelectedValue == "4")
                {
                   
                    SKUPriceDetailController SKUPriceCtl = new SKUPriceDetailController();
                    flag=SKUPriceCtl.ImportSKUPrices(int.Parse(DrpDistributor.SelectedValue.ToString()), path, int.Parse(DrpPrincipal.SelectedValue.ToString()));
                    if (!flag)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                        return;
                    }
                }
                else if (cboFileTypes.SelectedValue == "7")
                {
                    PurchaseController PurCtl = new PurchaseController();
                    DistributorController DistrCtl = new DistributorController();
                    DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(DrpDistributor.SelectedValue), 3);
                   flag= PurCtl.ImportOpeningStock(int.Parse(DrpDistributor.SelectedValue.ToString()), path, int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]), Convert.ToInt32(Session["UserID"]));
                   if (!flag)
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                      return;
                  }
                }
                else if (cboFileTypes.SelectedValue == "5") //For Purchase
                {
                    PurchaseController PurCtl = new PurchaseController();
                    DistributorController DistrCtl = new DistributorController();
                    DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(DrpDistributor.SelectedValue), 3);
                   flag= PurCtl.ImportPurchaseStock(int.Parse(DrpDistributor.SelectedValue.ToString()), path, int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]), Convert.ToInt32(Session["UserID"]));
                   if (!flag)
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
                       return;
                   }
                }
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record insert successfully.');", true);
               
                LoadDistributor();
                this.LoadPrincipal();
                cboFileTypes.SelectedIndex = 0;
            }
            
        }
        catch (Exception excp)
        {
            lblErrorMessage.Text = excp.ToString();
            cboFileTypes.SelectedIndex = 0;
            return;
        }
    }

    protected void cboFileTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cboFileTypes.SelectedValue=="3")// skus
        {
            lblprincipal.Visible = false;
            DrpPrincipal.Visible = false;

        }
        else
        {
           // lblprincipal.Visible = true;
           // DrpPrincipal.Visible = true;
        }
    }
}
