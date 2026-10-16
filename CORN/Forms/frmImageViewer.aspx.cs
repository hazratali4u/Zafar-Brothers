using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using System.IO;
using Ionic.Zip;
using System.Collections.Generic;

/// <summary>
/// 
/// Form For Sale Person DSR Report
/// </summary>
public partial class frmImageViewer : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadGender();
            LoadPrincipal();
            LoadCategory();
            LoadSeason();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
        }
    }

    private void LoadSeason()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        ChbSeason.Items.Clear();

        DataTable dt = _mHerController.SelectSkuHierarchy(Constants.SKUSeason, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        // DrpSeason.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillListBox(ChbSeason, dt, 0, 3, false);
        for (int i = 0; i < ChbSeason.Items.Count; i++)
        {
            ChbSeason.Items[i].Selected = true;
        }
    }
    protected void LoadCategory()
    {
        SkuHierarchyController _mHerController = new SkuHierarchyController();
        DataTable _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillListBox(this.chbcategory, _mDt, 0, 3, true);
        for (int i = 0; i < chbcategory.Items.Count; i++)
        {
            chbcategory.Items[i].Selected = true;
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
      //  Drpprincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillListBox(this.chbPrincipal, m_dt, 0, 1);

        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            chbPrincipal.Items[i].Selected = true;
        }
    }
    private void LoadGender()
    {
        chbGender.Items.Clear();
        
         SkuHierarchyController _mHerController = new SkuHierarchyController();
        var _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUGender, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(chbGender, _mDt, 0,3);
        for (int i = 0; i < chbGender.Items.Count; i++)
        {
            chbGender.Items[i].Selected = true;
        }
    }
    
   
    private void LoadDistributor()
    {
        
        drpDistributor.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), 2, 1, int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
    }
    
  
    private void ShowReport(int p_ReprotType)
    {
        //DocumentPrintController DPrint = new DocumentPrintController();
        //RptSaleController RptSaleCtl = new RptSaleController();
        //DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        //DataSet ds = null;
        //DataControl dc = new DataControl();
        //string Gender_IDs = null;
        //string Genders = null;
        //for (int i = 0; i < chbGender.Items.Count; i++)
        //{
        //    if (chbGender.Items[i].Selected == true)
        //    {
        //        Gender_IDs += chbGender.Items[i].Value.ToString() + ",";
        //        Genders += chbGender.Items[i].Text.ToString() + ",";
        //    }
        //}
        //if (Gender_IDs == null)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Region.');", true);
        //    return;
        //}

        //#region category
        //string Category_IDs = null;
        //string Categorys = null;
        //for (int i = 0; i < chbcategory.Items.Count; i++)
        //{
        //    if (chbcategory.Items[i].Selected == true)
        //    {
        //        Category_IDs += chbcategory.Items[i].Value.ToString() + ",";
        //        Categorys += chbcategory.Items[i].Text.ToString() + ",";
        //    }
        //}
        //if (Category_IDs == null)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Sale Person.');", true);
        //    return;
        //}
        //#endregion

        //#region Season
        //string Season_IDs = null;
        //string Seasons = null;
        //for (int i = 0; i < ChbSeason.Items.Count; i++)
        //{
        //    if (ChbSeason.Items[i].Selected == true)
        //    {
        //        Season_IDs += ChbSeason.Items[i].Value.ToString() + ",";
        //        Seasons += ChbSeason.Items[i].Text.ToString() + ",";
        //    }
        //}
        //if (Season_IDs == null)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
        //    return;
        //}
        //#endregion


        //#region Principal
        //string PRINCIPAL_IDs = null;
        //string principal = null;
        //for (int i = 0; i < chbPrincipal.Items.Count; i++)
        //{
        //    if (chbPrincipal.Items[i].Selected == true)
        //    {
        //        PRINCIPAL_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
        //        principal += chbPrincipal.Items[i].Text.ToString() + ",";
        //    }
        //}
        //if (PRINCIPAL_IDs == null)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
        //    return;
        //}
        //#endregion

        //ds = RptSaleCtl.SelectPerformanceReport(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SalePerson_IDs .ToString(), 0, Region_IDs.ToString(),Season_IDs.ToString(), PRINCIPAL_IDs.ToString());
      

        //CORNBusinessLayer.Reports.CrpSalemanPerformance CrpReport = new CORNBusinessLayer.Reports.CrpSalemanPerformance();
        //CrpReport.SetDataSource(ds);
        //CrpReport.Refresh();

        //CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        //CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        //CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        //CrpReport.SetParameterValue("SalePerson", SalePersons.ToString());
        //CrpReport.SetParameterValue("Principal", principal.ToString());
        //CrpReport.SetParameterValue("Season", Seasons.ToString());
        //CrpReport.SetParameterValue("Region", Regions.ToString());

        //CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        //Session.Add("CrpReport", CrpReport);
        //Session.Add("ReportType", p_ReprotType);
        //string url = "'Default.aspx'";
        //string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //Type cstype = GetType();
        //ClientScriptManager cs = Page.ClientScript;
        //cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadSkuDetail();
    }

   
    private void loadSkuDetail()
    {
        #region gender
        string Gender_IDs = null;
        string Genders = null;
        for (int i = 0; i < chbGender.Items.Count; i++)
        {
            if (chbGender.Items[i].Selected == true)
            {
                Gender_IDs += chbGender.Items[i].Value.ToString() + ",";
                Genders += chbGender.Items[i].Text.ToString() + ",";
            }
        }
        if (Gender_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Region.');", true);
            return;
        }
        #endregion

        #region category
        string Category_IDs = null;
        string Categorys = null;
        for (int i = 0; i < chbcategory.Items.Count; i++)
        {
            if (chbcategory.Items[i].Selected == true)
            {
                Category_IDs += chbcategory.Items[i].Value.ToString() + ",";
                Categorys += chbcategory.Items[i].Text.ToString() + ",";
            }
        }
        if (Category_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Sale Person.');", true);
            return;
        }
        #endregion

        #region Season
        string Season_IDs = null;
        string Seasons = null;
        for (int i = 0; i < ChbSeason.Items.Count; i++)
        {
            if (ChbSeason.Items[i].Selected == true)
            {
                Season_IDs += ChbSeason.Items[i].Value.ToString() + ",";
                Seasons += ChbSeason.Items[i].Text.ToString() + ",";
            }
        }
        if (Season_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Season.');", true);
            return;
        }
        #endregion


        #region Principal
        string PRINCIPAL_IDs = null;
        string principal = null;
        for (int i = 0; i < chbPrincipal.Items.Count; i++)
        {
            if (chbPrincipal.Items[i].Selected == true)
            {
                PRINCIPAL_IDs += chbPrincipal.Items[i].Value.ToString() + ",";
                principal += chbPrincipal.Items[i].Text.ToString() + ",";
            }
        }
        if (PRINCIPAL_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Principal.');", true);
            return;
        }
        #endregion

        DataControl dc = new DataControl();
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectSkusImages(int.Parse(drpDistributor.SelectedValue), PRINCIPAL_IDs.ToString(), Season_IDs.ToString(), Gender_IDs.ToString(), Category_IDs.ToString(), DateTime.Parse(txtStartDate.Text), 0, Convert.ToInt32(Session["UserID"]), int.Parse(dc.chkNull_0(txtMiminumStock.Text)));
        if (Dtsku_Price != null)
        {
            GrdImage.DataSource = Dtsku_Price;
            GrdImage.DataBind();
           
        }
        else
        {
            
        }
    }

    protected void btnDownLoadall_Click(object sender, EventArgs e)
    {
        try
        {   
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Pics");
                foreach (GridViewRow row in GrdImage.Rows)
                {
                    if ((row.FindControl("ChbSku") as CheckBox).Checked)
                    {
                        string filePath = Server.MapPath("~/Pics/") + GrdImage.DataKeys[row.RowIndex].Values["PicUrl"].ToString();
                        if (File.Exists(filePath))
                        {
                            zip.AddFile(filePath, "Pics");
                        }
                    }
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    protected void GrdImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            var imagepath = "~/Pics/" + GrdImage.Rows[e.RowIndex].Cells[1].Text;
            var imageName = GrdImage.Rows[e.RowIndex].Cells[1].Text;
            string filename = MapPath(imagepath);
            Response.ContentType = "image/JPEG";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + imageName + "");
            Response.TransmitFile(filename);
            Response.End();
        }
        catch(Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    protected void GrdImage_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            var imagepath = "~/Pics/" + GrdImage.Rows[e.NewEditIndex].Cells[1].Text;
            this.Session.Add("ImgPath", imagepath);

            string url = "'ImageViewer.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
}
