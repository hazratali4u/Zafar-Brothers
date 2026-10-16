using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// 
/// Form For Sale Person DSR Report
/// </summary>
public partial class Forms_frmSalenRecoveryCustomerWise : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadSaleForce();
            LoadRegion();
            LoadTerritory();
            LoadZone();
            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    protected void LoadRegion()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillListBox(this.chbRegion, dt, 0, 4, true);
        for (int i = 0; i < chbRegion.Items.Count; i++)
        {
            chbRegion.Items[i].Selected = true;
        }
    }
    protected void LoadTerritory()
    {
        chbTerritory.Items.Clear();
        for (int i = 0; i < chbRegion.Items.Count; i++)
        {
            if (chbRegion.Items[i].Selected == true)
            {

                GeoHierarchyController GControler = new GeoHierarchyController();
                DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(chbRegion.Items[i].Value), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));

                clsWebFormUtil.FillListBox(this.chbTerritory, dt, 0, 4, false);
            }
        }
        
        for (int i = 0; i < chbTerritory.Items.Count; i++)
        {
            chbTerritory.Items[i].Selected = true;
        }
    }

    protected void LoadZone()
    {
        chbZone.Items.Clear();
        for (int i = 0; i < chbTerritory.Items.Count; i++)
        {
            if (chbTerritory.Items[i].Selected == true)
            {

                GeoHierarchyController GControler = new GeoHierarchyController();
                DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(chbTerritory.Items[i].Value), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));

                clsWebFormUtil.FillListBox(this.chbZone, dt, 0, 4, false);
            }
        }

        for (int i = 0; i < chbZone.Items.Count; i++)
        {
            chbZone.Items[i].Selected = true;
        }
    }
    private void LoadSaleForce()
    {
        chbSaleForce.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        for (int i = 0; i < chbLocation.Items.Count; i++)
        {
            if (chbLocation.Items[i].Selected == true)
            {
                DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(0, Constants.IntNullValue, int.Parse(chbLocation.Items[i].Value));

                clsWebFormUtil.FillListBox(this.chbSaleForce, m_dt, 0, 1, false);
            }
        }

        for (int i = 0; i < chbSaleForce.Items.Count; i++)
        {
            chbSaleForce.Items[i].Selected = true;
        }
    }
    
  
    private void LoadDistributor()
    {
        
        chbLocation.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectUserAssignment(int.Parse(Session["UserId"].ToString()), 2, 1, int.Parse(Session["CompanyId"].ToString()));
       
        clsWebFormUtil.FillListBox(this.chbLocation, dt, 0, 1, true);
        for (int i = 0; i < chbLocation.Items.Count; i++)
        {
            chbLocation.Items[i].Selected = true;
        }
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

   
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }


    private void ShowReport(int p_ReprotType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(this.Session["Distributor_Id"].ToString()));
        DataSet ds = null;
        DataControl dc = new DataControl();
        #region Region
        string Region_IDs = null;
        string regions = null;
        for (int i = 0; i < chbRegion.Items.Count; i++)
        {
            if (chbRegion.Items[i].Selected == true)
            {
                Region_IDs += chbRegion.Items[i].Value.ToString() + ",";
                regions += chbRegion.Items[i].Text.ToString() + ",";
            }
        }
        if (Region_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Region.');", true);
            return;
        }
        if (chbselectAllRegion.Checked == true)
        {
            regions = "All";
        }
        #endregion
        #region Territory
        string Territory_IDs = null;
        string teritorys = null;
        for (int i = 0; i < chbTerritory.Items.Count; i++)
        {
            if (chbTerritory.Items[i].Selected == true)
            {
                Territory_IDs += chbTerritory.Items[i].Value.ToString() + ",";
                teritorys += chbTerritory.Items[i].Text.ToString() + ",";
            }
        }
        if (Territory_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Territory.');", true);
            return;
        }
        if (chbselectAllTerritory.Checked == true)
        {
            teritorys = "All";
        }
        #endregion

        #region Zone
        string Zone_IDs = null;
        for (int i = 0; i < chbZone.Items.Count; i++)
        {
            if (chbZone.Items[i].Selected == true)
            {
                Zone_IDs += chbZone.Items[i].Value.ToString() + ",";
            }
        }
        if (Zone_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Zone.');", true);
            return;
        }
        #endregion

        #region Location
        string Location_IDs = null;
        string Locations = null;

        for (int i = 0; i < chbLocation.Items.Count; i++)
        {
            if (chbLocation.Items[i].Selected == true)
            {
                Location_IDs += chbLocation.Items[i].Value.ToString() + ",";
                Locations += chbLocation.Items[i].Text.ToString() + ",";
            }
        }
        if (chbLocation == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Location.');", true);
            return;
        }
        if (ChbSelectAllLocation.Checked == true)
        {
            Locations = "All";
        }
        #endregion
        #region sale person
        string SaleForce_IDs = null;
        string Salepersons = null;
        for (int i = 0; i < chbSaleForce.Items.Count; i++)
        {
            if (chbSaleForce.Items[i].Selected == true)
            {
                SaleForce_IDs += chbSaleForce.Items[i].Value.ToString() + ",";
                Salepersons += chbSaleForce.Items[i].Text.ToString() + ",";
            }
        }
        if (SaleForce_IDs == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select AtLeast One Sale Person.');", true);
            return;
        }
        if (ChbSelectAllSalePerson.Checked == true)
        {
            Salepersons = "All";
        }
        #endregion

        int zero_el = 0;
        if(chbZeroElimination.Checked)
        { zero_el = 1; }

        ds = RptSaleCtl.SelectSaleRecoveryCustomerWise(Location_IDs.ToString(), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SaleForce_IDs.ToString(), 0, Region_IDs.ToString(), Territory_IDs.ToString(), Zone_IDs.ToString(), int.Parse(DrpType.SelectedValue), zero_el);
        var data = RptSaleCtl.SelectSaleRecoverytable(Location_IDs.ToString(), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), SaleForce_IDs.ToString(), 1, Region_IDs.ToString(), Territory_IDs.ToString(), Zone_IDs.ToString());

        ReportDocument CrpReport = new ReportDocument();
        CrpReport = new CORNBusinessLayer.Reports.CrpSaleNrecoveryCustomerWise();
      

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Distributor_Name", Locations.ToString());
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("SalePerson", Salepersons.ToString());
        CrpReport.SetParameterValue("regeons", regions);
        CrpReport.SetParameterValue("teritorys", teritorys);

        CrpReport.SetParameterValue("type", DrpType.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("zero_ele", chbZeroElimination.Checked.ToString());
        if (data != null)
        {

            try
            {
                if (data.Tables.Count > 0)
                {
                    CrpReport.SetParameterValue("BalanceBF", decimal.Parse(data.Tables[0].Rows[0]["BalanceBF"].ToString()));
                    CrpReport.SetParameterValue("Recovery_By_OtherSP", decimal.Parse(data.Tables[1].Rows[0]["Recovery_AgaintOterhSP"].ToString()));
           
                }
            }
            catch (Exception ex)
            {
                CrpReport.SetParameterValue("BalanceBF", 0);
                CrpReport.SetParameterValue("Recovery_By_OtherSP", 0);
                ExceptionPublisher.PublishException(ex);
            }
        }
        else
        {
            CrpReport.SetParameterValue("BalanceBF", 0);
            CrpReport.SetParameterValue("Recovery_By_OtherSP", 0);
           
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        }
        else
        {
            CrpReport.SetParameterValue("CompanyName", "");
        }

        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", p_ReprotType);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }


    protected void chbLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }

    protected void chbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTerritory();
        LoadZone();
    }

    protected void chbTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadZone();

    }

    protected void chbselectAllRegion_CheckedChanged(object sender, EventArgs e)
    {
        LoadTerritory();
        LoadZone();
    }

    protected void chbselectAllTerritory_CheckedChanged(object sender, EventArgs e)
    {
        LoadZone();
    }

    protected void ChbSelectAllLocation_CheckedChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }
}
