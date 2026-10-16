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
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From to Add, Edit, Delete Geo Heirarical Data
/// </summary>
public partial class Forms_frmGeoHierarchyData : System.Web.UI.Page
{
    GeoHierarchyController GControler = new GeoHierarchyController();
    private static int  ZoneId, TerritoryId, TownId;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadRegion();
            this.LoadTerritory();
            this.LoadZoneTerritory();
            this.LoadZone();
            //this.LoadTownZone();
            //this.LoadTownTerritory();
            //this.LoadTown();
            //AppMaster master = new AppMaster();
            //master = (AppMaster)this.Master;
            //Panel panel = new Panel();
            //panel = master.FindControl("searchpanel") as Panel;
            //panel.Visible = true;
            CORNCommon.Classes.Configuration.DistributorId = 1;
            btnRegionSave.Attributes.Add("onclick", "return ValidatRegion()");
           
            btnSaveTerritory.Attributes.Add("onclick", "return ValidateTerritory()");
      

        }
    }
    
    #region Region Tab

    /// <summary>
    /// Loads Existing Regions To All Region Combos And Region Grid Of Region Tab.
    /// </summary>
    protected void LoadRegion()
    {
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
       // clsWebFormUtil.FillDropDownList(this.DrpRegion, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(this.drpRegionTerritory, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(this.DrpZoneRegion, dt, 0, 4, true);
        //  clsWebFormUtil.FillDropDownList(this.DrpTownRegion, dt, 0, 4, true);
        grdRegionData.EditIndex = -1;
        grdRegionData.DataSource = dt;
        grdRegionData.DataBind();
    }

    /// <summary>
    /// Sets Region Tab Data For Edit. This Function Runs When An Existing Region Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdRegionData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HdnRegionID.Value =(grdRegionData.Rows[e.NewEditIndex].Cells[0].Text);
        txtRegionCode.Text = grdRegionData.Rows[e.NewEditIndex].Cells[1].Text;
        txtRegionName.Text = grdRegionData.Rows[e.NewEditIndex].Cells[2].Text;
        ChIsActive.Checked = bool.Parse(grdRegionData.Rows[e.NewEditIndex].Cells[3].Text);
        btnRegionSave.Text = "Update";
        for (int i = 0; i < grdRegionData.Rows.Count; i++)
        {
            grdRegionData.Rows[i].Cells[4].Enabled = false;
        }
    }

    /// <summary>
    /// Saves Or Updates A Region.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRegionSave_Click(object sender, EventArgs e)
    {
        if (btnRegionSave.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(Constants.IntNullValue, txtRegionCode.Text, txtRegionName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Region, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            //if (ChIsActive.Checked == false)
            //{
            //    DataTable dt = GControler.SelectDistributorHierachyWithType(1, int.Parse(HdnRegionID.Value), null);
            //    if (dt.Rows.Count > 0)
            //    {
            //        lblErrorMsgRegion.Text = "An Active Zone Exists...";
            //        return;
            //    }
            //}
            GControler.UpdateHierarchy(int.Parse(HdnRegionID.Value), Constants.IntNullValue, txtRegionCode.Text, txtRegionName.Text, true, ChIsActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Region, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        this.LoadRegion();
        txtRegionCode.Text = "";
        txtRegionName.Text = "";
        btnRegionSave.Text = "Save";
        lblErrorMsgRegion.Text = "";
        HdnRegionID.Value = "";
    }

    #endregion

    #region Zone Tab

    ///// <summary>
    ///// Loads Existing Zones To Zone Grid of Zone Tab
    ///// </summary>
    //protected void LoadZone()
    //{
    //    if (DrpRegion.Items.Count > 0)
    //    {
    //        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
    //        grdZoneData.DataSource = dt;
    //        grdZoneData.DataBind();
    //    }
    //}

    ///// <summary>
    ///// Loads Zones To Zone Grid Of Zone Tab.
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void DrpRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadZone();
    //}

    ///// <summary>
    ///// Sets Zone Tab Data For Edit. This Function Runs When An Existing Zone Needs To Be Edited
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">GridViewEditEventArgs</param>
    //protected void grdZoneData_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    ZoneId = int.Parse(grdZoneData.Rows[e.NewEditIndex].Cells[1].Text);
    //    txtZoneCode.Text = grdZoneData.Rows[e.NewEditIndex].Cells[2].Text;
    //    txtZoneName.Text = grdZoneData.Rows[e.NewEditIndex].Cells[3].Text;
    //    chbIsZoneActive.Checked = bool.Parse(grdZoneData.Rows[e.NewEditIndex].Cells[4].Text);
    //    btnSaveZone.Text = "Update";
    //}

    ///// <summary>
    ///// Saves Or Updates A Zone.
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void btnSaveZone_Click(object sender, EventArgs e)
    //{
    //    if (btnSaveZone.Text == "Save")
    //    {
    //        string InsertValue = GControler.InsertHierarchy(int.Parse(DrpRegion.SelectedValue.ToString()), txtZoneCode.Text, txtZoneName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
    //    }
    //    else
    //    {
    //        if (chbIsZoneActive.Checked == false)
    //        {
    //            DataTable dt = GControler.SelectDistributorHierachyWithType(2, int.Parse(DrpRegion.SelectedValue.ToString()), ZoneId, Constants.IntNullValue);
    //            if (dt.Rows.Count > 0)
    //            {
    //                lblErrorMsgDivsion.Text = "An Active Territory Exists...";
    //                return;
    //            }
    //        }
    //        GControler.UpdateHierarchy(ZoneId, int.Parse(DrpRegion.SelectedValue.ToString()), txtZoneCode.Text, txtZoneName.Text, true, chbIsZoneActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
    //    }

    //    this.LoadZone();
    //    this.LoadTerritoryZone();
    //    this.LoadTownZone();
    //    txtZoneCode.Text = "";
    //    txtZoneName.Text = "";
    //    btnSaveZone.Text = "Save";
    //    lblErrorMsgDivsion.Text = "";
    //}

    #endregion

    #region Territory Tab

    /// <summary>
    /// Loads Existing Territories To Territory Grid of Territory Tab
    /// </summary>
    protected void LoadTerritory()
    {
        if (drpRegionTerritory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpRegionTerritory.SelectedValue), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            grdTerritoryData.EditIndex = -1;
            grdTerritoryData.DataSource = dt;
            grdTerritoryData.DataBind();
        }
    }


    protected void drpRegionTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        this.LoadTerritory();
    }

  
    protected void grdTerritoryData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritoryData.PageIndex = e.NewPageIndex;
        this.LoadTerritory();
    }
    
    protected void grdTerritoryData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HdnTerritoryID.Value = (grdTerritoryData.Rows[e.NewEditIndex].Cells[1].Text);
        txtTerritoryCode.Text = grdTerritoryData.Rows[e.NewEditIndex].Cells[2].Text;
        txtTerritoryName.Text = grdTerritoryData.Rows[e.NewEditIndex].Cells[3].Text;
        chbIsTerritoryActive.Checked = bool.Parse(grdTerritoryData.Rows[e.NewEditIndex].Cells[4].Text);
        btnSaveTerritory.Text = "Update";
        for (int i = 0; i < grdTerritoryData.Rows.Count; i++)
        {
            grdTerritoryData.Rows[i].Cells[5].Enabled = false;
        }
    }
    
    protected void btnSaveTerritory_Click(object sender, EventArgs e)
    {
        if (btnSaveTerritory.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(int.Parse(drpRegionTerritory.SelectedValue), txtTerritoryCode.Text, txtTerritoryName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Territory, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
        //    if (chbIsTerritoryActive.Checked == false)
        //    {
        //        DataTable dt = GControler.SelectDistributorHierachyWithType(3, int.Parse(DrpRegion.SelectedValue.ToString()), int.Parse(DrpZone.SelectedValue.ToString()), TerritoryId);
        //        if (dt.Rows.Count > 0)
        //        {
        //            lblErrorZone.Text = "An Active Town Exists...";
        //            return;
        //        }
        //    }
            GControler.UpdateHierarchy(int.Parse(HdnTerritoryID.Value), int.Parse(drpRegionTerritory.SelectedValue.ToString()), txtTerritoryCode.Text, txtTerritoryName.Text, true, chbIsTerritoryActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Territory, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        this.LoadTerritory();
       this.LoadRegion();
        txtTerritoryCode.Text = "";
        txtTerritoryName.Text = "";
        btnSaveTerritory.Text = "Save";
        lblErrorTerritory.Text = "";
    }
    #endregion

    #region Town Tab

    ///// <summary>
    ///// Loads Existing Towns To Town Grid of Town Tab
    ///// </summary>
    //protected void LoadTown()
    //{
    //    if (DrpTerritory.Items.Count > 0)
    //    {
    //        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
    //        grdTownData.DataSource = dt;
    //        grdTownData.DataBind();
    //    }
    //}

    ///// <summary>
    ///// Loads Zones To Zone Combo Of Town Tab.
    ///// </summary>
    //private void LoadTownZone()
    //{
    //    if (DrpTownRegion.Items.Count > 0)
    //    {
    //        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
    //        clsWebFormUtil.FillDropDownList(this.DrpTownZone, dt, 0, 4, true);

    //    }
    //}

    ///// <summary>
    ///// Loads Territories To Territory Grid Of Town Tab.
    ///// </summary>
    //private void LoadTownTerritory()
    //{
    //    if (DrpTownZone.Items.Count > 0)
    //    {
    //        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
    //        clsWebFormUtil.FillDropDownList(this.DrpTerritory, dt, 0, 4, true);
    //    }
    //}

    ///// <summary>
    ///// Loads Zones To Zone Combo,Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void DrpTownRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadTownZone();
    //    this.LoadTownTerritory();
    //    this.LoadTown();
    //}

    ///// <summary>
    ///// Loads Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void DrpTownZone_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadTownTerritory();
    //    this.LoadTown();
    //}

    ///// <summary>
    ///// Loads Towns To Town Grid of Town Tab
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void DrpTerritory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadTown();
    //}

    ///// <summary>
    ///// Sets Town Tab Data For Edit. This Function Runs When An Existing Town Needs To Be Edited
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">GridViewEditEventArgs</param>
    //protected void grdTownData_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    TownId = int.Parse(grdTownData.Rows[e.NewEditIndex].Cells[1].Text);
    //    txtTownCode.Text = grdTownData.Rows[e.NewEditIndex].Cells[2].Text;
    //    txtTownName.Text = grdTownData.Rows[e.NewEditIndex].Cells[3].Text;
    //    chbIsTownActive.Checked = bool.Parse(grdTownData.Rows[e.NewEditIndex].Cells[4].Text);
    //    btnSaveTown.Text = "Update";
    //}

    ///// <summary>
    ///// Sets PageIndex Of Town Grid On Town Tab
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">GridViewPageEventArgs</param>
    //protected void grdTownData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdTownData.PageIndex = e.NewPageIndex;
    //    this.LoadTown();
    //}

    ///// <summary>
    ///// Saves Or Updates A Town.
    ///// </summary>
    ///// <param name="sender">object</param>
    ///// <param name="e">EventArgs</param>
    //protected void btnSaveTown_Click(object sender, EventArgs e)
    //{
    //    if (btnSaveTown.Text == "Save")
    //    {
    //        string InsertValue = GControler.InsertHierarchy(int.Parse(DrpTerritory.SelectedValue.ToString()), txtTownCode.Text, txtTownName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Town, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
    //    }
    //    else
    //    {
    //        if (chbIsTownActive.Checked == false)
    //        {
    //            DataTable dt = GControler.SelectDistributorHierachyWithType(9, TownId, null);
    //            if (dt.Rows.Count > 0)
    //            {
    //                lblErrorZone1.Text = "This Town is Assigned...";
    //                return;
    //            }
    //        }
    //        GControler.UpdateHierarchy(TownId, int.Parse(DrpTerritory.SelectedValue.ToString()), txtTownCode.Text, txtTownName.Text, true, chbIsTownActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Town, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
    //    }
    //    this.LoadTown();
    //    txtTownCode.Text = "";
    //    txtTownName.Text = "";
    //    btnSaveTown.Text = "Save";
    //    lblErrorZone1.Text = "";
    //}

    #endregion


    #region Zone
    protected void LoadZoneTerritory()
    {
        if (DrpZoneRegion.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpZoneRegion.SelectedValue), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.drpZoneTerrotory, dt, 0, 4, true);
        }
    }
    protected void LoadZone()
    {
        if (drpZoneTerrotory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpZoneTerrotory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            GrdZoneData.EditIndex = -1;
            GrdZoneData.DataSource = dt;
            GrdZoneData.DataBind();
        }
    }
    protected void GrdZoneData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hdnZoneID.Value = int.Parse(GrdZoneData.Rows[e.NewEditIndex].Cells[1].Text).ToString();
        btnSaveZone.Text = "Update";
        txtZoneCode.Text = GrdZoneData.Rows[e.NewEditIndex].Cells[2].Text;
        txtZoneName.Text = GrdZoneData.Rows[e.NewEditIndex].Cells[3].Text;
        chbzoneActive.Checked = bool.Parse(GrdZoneData.Rows[e.NewEditIndex].Cells[4].Text);
        for (int i = 0; i < GrdZoneData.Rows.Count; i++)
        {
            GrdZoneData.Rows[i].Cells[5].Enabled = false;
        }
    }
    protected void DrpZoneRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadZoneTerritory();
        LoadZone();
    }

    protected void drpZoneTerrotory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadZone();
    }

    protected void btnSaveZone_Click(object sender, EventArgs e)
    {
        if (btnSaveZone.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(int.Parse(drpZoneTerrotory.SelectedValue), txtZoneCode.Text, txtZoneName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            //    if (chbIsTerritoryActive.Checked == false)
            //    {
            //        DataTable dt = GControler.SelectDistributorHierachyWithType(3, int.Parse(DrpRegion.SelectedValue.ToString()), int.Parse(DrpZone.SelectedValue.ToString()), TerritoryId);
            //        if (dt.Rows.Count > 0)
            //        {
            //            lblErrorZone.Text = "An Active Town Exists...";
            //            return;
            //        }
            //    }
            GControler.UpdateHierarchy(int.Parse(hdnZoneID.Value), int.Parse(drpZoneTerrotory.SelectedValue.ToString()), txtZoneCode.Text, txtZoneName.Text, true, chbzoneActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), CORNCommon.Classes.Configuration.DistributorId, null);
        }
        LoadZone();
        txtZoneCode.Text = "";
        txtZoneName.Text = "";
        btnSaveZone.Text = "Save";
        lblZoneError.Text = "";

    }


    #endregion



    protected void GrdZoneData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
