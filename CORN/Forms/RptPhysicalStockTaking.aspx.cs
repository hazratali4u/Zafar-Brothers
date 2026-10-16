
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For Physical Stock Report
/// </summary>
public partial class Forms_RptPhysicalStockTaking : System.Web.UI.Page
{
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();

    private DataTable _mDt;
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadLocation();
            LoadCategory();
            LoadGender();
            LoadSeason();
            this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
      private void LoadSeason()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();
    chbSeason.Items.Clear();

        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
    // DrpSeason.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

    clsWebFormUtil.FillListBox(chbSeason, dt, 0, 3, false);
        for (int i = 0; i<chbSeason.Items.Count; i++)
        {
            chbSeason.Items[i].Selected = true;
        }
    }
    private void LoadGender()
    {
        chbGender.Items.Clear();
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(chbGender, dt, 0, 3, false);
        for (int i = 0; i < chbGender.Items.Count; i++)
        {
            chbGender.Items[i].Selected = true;
        }
    }
    protected void LoadPrincipal()
    {
        try
        {
            chbPrincipal.Items.Clear();

            if (RblType.SelectedIndex == 0)
            {
                SKUPriceDetailController PController = new SKUPriceDetailController();
                DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
               
                clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, "Company_Id", "Company_Name");
                for (int i = 0; i < chbPrincipal.Items.Count; i++)
                {
                    chbPrincipal.Items[i].Selected = true;
                }
            }
            else
            {
                SKUPriceDetailController PController = new SKUPriceDetailController();
                DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, "Company_Id", "Company_Name");
                for (int i = 0; i < chbPrincipal.Items.Count; i++)
                {
                    chbPrincipal.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DrpLocation.Items.Clear();

            if (RblType.SelectedIndex == 0)
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
               
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
            }
            else
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                this.DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void ShowReport(int type)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            if (RblType.SelectedIndex == 0)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                string FromDate = null;
                CORNBusinessLayer.Reports.CrpPhysicalStockTaking CrpReport = new CORNBusinessLayer.Reports.CrpPhysicalStockTaking();
                DataSet ds = null;
                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                FromDate = parsed_date_fromdate.ToShortDateString();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                #region selection
                string Principal_IDs = null;
                for (int i = 0; i < chbPrincipal.Items.Count; i++)
                {
                    if (chbPrincipal.Items[i].Selected == true)
                    {
                        Principal_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
                    }
                }
                if (Principal_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
                    return;
                }
                string Category_IDs = null;
                for (int i = 0; i < chbCategory.Items.Count; i++)
                {
                    if (chbCategory.Items[i].Selected == true)
                    {
                        Category_IDs += chbCategory.Items[i].Value.ToString() + ",";
                    }
                }
                if (Category_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Category.');", true);
                    return;
                }

                string Season_IDs = null;
                for (int i = 0; i < chbSeason.Items.Count; i++)
                {
                    if (chbSeason.Items[i].Selected == true)
                    {
                        Season_IDs += chbSeason.Items[i].Value.ToString() + ",";
                    }
                }
                if (Season_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
                    return;
                }

                string Gender_IDs = null;
                for (int i = 0; i < chbGender.Items.Count; i++)
                {
                    if (chbGender.Items[i].Selected == true)
                    {
                        Gender_IDs += chbGender.Items[i].Value.ToString() + ",";
                    }
                }
                if (Gender_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Gender.');", true);
                    return;
                }
                #endregion
                ds = RptInventoryCtl.PhysicalStockTaking(Principal_IDs.ToString(), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Category_IDs.ToString(), Season_IDs.ToString(), Gender_IDs.ToString());

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", "All");
                CrpReport.SetParameterValue("Category", "All");
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("Zero_Eli", cbZero.Checked);



                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", type);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;

                CORNBusinessLayer.Reports.RptPhysicalStockSummary CrpReport = new CORNBusinessLayer.Reports.RptPhysicalStockSummary();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();

                #region selection
                string Principal_IDs = null;
                for (int i = 0; i < chbPrincipal.Items.Count; i++)
                {
                    if (chbPrincipal.Items[i].Selected == true)
                    {
                        Principal_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
                    }
                }
                if (Principal_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
                    return;
                }
                //string Category_IDs = null;
                //for (int i = 0; i < chbCategory.Items.Count; i++)
                //{
                //    if (chbCategory.Items[i].Selected == true)
                //    {
                //        Category_IDs += chbCategory.Items[i].Value.ToString() + ",";
                //    }
                //}
                //if (Category_IDs == null)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Category.');", true);
                //    return;
                //}
                string Season_IDs = null;
                for (int i = 0; i < chbSeason.Items.Count; i++)
                {
                    if (chbSeason.Items[i].Selected == true)
                    {
                        Season_IDs += chbSeason.Items[i].Value.ToString() + ",";
                    }
                }
                if (Season_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
                    return;
                }

                string Gender_IDs = null;
                for (int i = 0; i < chbGender.Items.Count; i++)
                {
                    if (chbGender.Items[i].Selected == true)
                    {
                        Gender_IDs += chbGender.Items[i].Value.ToString() + ",";
                    }
                }
                if (Gender_IDs == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Gender.');", true);
                    return;
                }
                #endregion
                ds = RptInventoryCtl.PhysicalStockTakingValueWise(Principal_IDs.ToString(), int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(this.Session["UserId"].ToString()), null, Season_IDs.ToString(), Gender_IDs.ToString());

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();


                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("CATEGORY", "All");
                CrpReport.SetParameterValue("Zero_Eli", cbZero.Checked);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", type);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Loads Principals And Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RblType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        LoadLocation();
        if (RblType.SelectedIndex == 1)
        {
            pnlCategory.Visible = false;
            lblcategory.Visible = false;
            ChbAllCategory.Visible = false;
        }
        else
        {
            pnlCategory.Visible = true;
            lblcategory.Visible = true;
            ChbAllCategory.Visible = true;
        }
    }

    private void LoadCategory()
    {
        chbCategory.Items.Clear();
        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillListBox(chbCategory, _mDt, 0, 3, false);
        for (int i = 0; i < chbCategory.Items.Count; i++)
        {
            chbCategory.Items[i].Selected = true;
        }
    }

}
