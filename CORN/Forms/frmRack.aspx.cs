using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;    

public partial class Forms_frmRack : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();
  
    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAisle();
            LoadAisleGird();
           
            LoadGirdRack();

            /////////////////
            LoadDistributor();
            LoadSkus();
            LoadRack();
            
            LoadShelf();
            LoadRackShelf();
            LoadGirdShelf();
            LoadPlacementGrid();
        }
    }


    #region Aisle Defination


    private void LoadAisleGird()
    {

        DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue, Constants.Aisle, Constants.IntNullValue);



        //switch (ddSearchType.SelectedIndex)
        //{
        //    case 1:
        //        dtRack.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;

        //    default:
        //        dtRack.DefaultView.RowFilter = "rackNo" + " like '%" + "" + "%'";
        //        break;
        //}
        GrdAsile.EditIndex = -1;
        GrdAsile.DataSource = dtRack;
        GrdAsile.DataBind();
    }
    private bool checkDuplicateAisle()
    {
        DataTable dtAisle = mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue,Constants .Aisle,Constants .IntNullValue);

        if (BtnAisleSave.Text == "Save")
        {
            DataRow[] dr = dtAisle.Select("rackNo='" + txtAisle.Text.Trim() + "'");
            if (dr.Length > 0)
            {
                return false;
            }
        }
        //else
        //{
        //    DataRow[] dr = dtAisle.Select("rackNo <>'" + txtAisle.Text.Trim() + "'");
        //    if (dr.Length > 0)
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    protected void BtnAisleSave_Click(object sender, EventArgs e)
    {
        if (checkDuplicateAisle())
        {
            bool isflag = false;
            int status = 1;
            if (!ChbAisle.Checked)
            {
                status = 0;
            }
            if (BtnAisleSave.Text == "Save")
            {
                isflag = mController.InsertRack(txtAisle.Text, status, int.Parse(Session["UserId"].ToString()), Constants.Aisle, Constants.IntNullValue);

            }
            else
            {
                isflag = mController.UpdateRack(int.Parse(HdnAisleID.Value), txtAisle.Text, status, int.Parse(Session["UserId"].ToString()), Constants.Aisle, Constants.IntNullValue);
            }
            if (isflag)
            {
                lblerrorAisle.Text = "Insert Successfully.";
                ClearAllAisle();
                LoadAisleGird();
               
                LoadAisle();
               
            }
            else
            {
                lblerrorAisle.Text = "Some error occurred.";
            }
        }
        else
        {
            lblerrorAisle.Text = "Aisle No already exist.";
        }
    }
    protected void BtnAisleCancel_Click(object sender, EventArgs e)
    {
        ClearAllAisle();
    }
    

    protected void GrdAsile_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HdnAisleID .Value = int.Parse(GrdAsile.Rows[e.NewEditIndex].Cells[0].Text).ToString();
        BtnAisleSave.Text = "Update";
        txtAisle.Text = GrdAsile.Rows[e.NewEditIndex].Cells[1].Text;

        ChbAisle.Checked = Convert.ToBoolean(GrdAsile.Rows[e.NewEditIndex].Cells[2].Text);

        for (int i = 0; i < GrdAsile.Rows.Count; i++)
        {
            GrdAsile.Rows[i].Cells[3].Enabled = false;
        }

    }

    private void ClearAllAisle()
    {
        txtAisle.Text = "";
        lblerrorAisle.Text = "";
        HdnAisleID.Value = "";
        BtnAisleSave.Text = "Save";

    }

    #endregion



    #region Rack Defination

   
    private void LoadGirdRack()
    {
        grdRack.DataSource = null;
        grdRack.DataBind();
        if (DrpAisleRack.Items.Count > 0)
        {
            DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue, Constants.Rack, int.Parse(DrpAisleRack.SelectedValue));
            grdRack.EditIndex = -1;
            grdRack.DataSource = dtRack;
            grdRack.DataBind();
        }
    }
    private bool checkDuplicateRack()
    {
        DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue,Constants .Rack ,Constants .IntNullValue );

        if (btnSaveRack.Text == "Save")
        {
            DataRow[] dr = dtRack.Select("rackNo='" + txtRackNo.Text.Trim() + "'");
            if (dr.Length > 0)
            {
             //   return false;
            }
        }
        //else
        //{
        //    DataRow[] dr = dtRack.Select("rackNo <>'" + txtRackNo.Text.Trim() + "'");
        //    if (dr.Length > 0)
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    protected void btnSaveRack_Click(object sender, EventArgs e)
    {
        if (checkDuplicateRack())
        {
            bool isflag = false;
            int status = 1;
            if (!ChbRack.Checked)
            {
                status = 0;
            }
            if (btnSaveRack.Text == "Save")
            {
                isflag = mController.InsertRack(txtRackNo.Text, status, int.Parse(Session["UserId"].ToString()),Constants.Rack,int.Parse (DrpAisleRack .SelectedValue));

            }
            else
            {
                isflag = mController.UpdateRack(int.Parse (HdnRackID .Value), txtRackNo.Text, status, int.Parse(Session["UserId"].ToString()),Constants .Rack,int.Parse(DrpAisleRack.SelectedValue));
            }
            if (isflag)
            {
                lblErrorMsgRack.Text = "Insert Successfully.";
                ClearAllRack();
                LoadGirdRack();
               // LoadAisle();
                LoadRack();
            }
            else
            {
                lblErrorMsgRack.Text = "Some error occurred.";
            }
        }
        else
        {
            lblErrorMsgRack.Text = "Rack No already exist.";
        }
    }
    protected void btnCancelRack_Click(object sender, EventArgs e)
    {
        ClearAllRack();
    }
  
   
    protected void grdRack_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            HdnRackID.Value = int.Parse(grdRack.Rows[e.NewEditIndex].Cells[0].Text).ToString();
            btnSaveRack.Text = "Update";
            txtRackNo.Text = grdRack.Rows[e.NewEditIndex].Cells[1].Text;
            ChbRack.Checked = Convert.ToBoolean(grdRack.Rows[e.NewEditIndex].Cells[2].Text);
            DrpAisleRack.SelectedValue = grdRack.Rows[e.NewEditIndex].Cells[3].Text;
            for (int i = 0; i < grdRack.Rows.Count; i++)
            {
                grdRack.Rows[i].Cells[4].Enabled = false;
            }
        }
        catch
        (Exception ex)
        {

        }

    }

    private void ClearAllRack()
    {
        txtRackNo.Text = "";
        lblErrorMsgRack.Text = "";
        HdnRackID.Value = "";
        btnSaveRack.Text = "Save";
       // DrpAisleRack.SelectedIndex = 0;

    }

    #endregion


    #region Shelf Defination

    private void LoadRackShelf()
    {
        DrpRackShelf.Items.Clear();
        if (DrpAisleShelf.Items.Count > 0)
        {
            DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, 1, Constants.Rack, int.Parse(DrpAisleShelf.SelectedValue));
            if (dtRack.Rows.Count <= 0) return;
            {

                DrpRackShelf.DataTextField = "rackNo";
                DrpRackShelf.DataValueField = "rackId";

                DrpRackShelf.DataSource = dtRack;
                DrpRackShelf.DataBind();


                // clsWebFormUtil.FillDropDownList(ddlRack, dtRack, 0, 1, true);
            }
        }else
        {
            DrpRackShelf.Items.Clear();
            
        }
    }
    private void LoadGirdShelf()
    {
        GrdShelf.DataSource = null;
        GrdShelf.DataBind();
        if (DrpRackShelf.Items.Count > 0)
        {
            DataTable dtShelf = mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue, Constants.Shelf, int.Parse(DrpRackShelf.SelectedValue));
            GrdShelf.EditIndex = -1;
            GrdShelf.DataSource = dtShelf;
            GrdShelf.DataBind();
        }
        else
        {
            GrdShelf.DataSource = null;
            GrdShelf.DataBind();
        }
    }
    private bool checkDuplicateShelf()
    {
        DataTable dtshelf= mController.SelectRackDefination(Constants.IntNullValue, null, Constants.IntNullValue, Constants.Shelf, Constants.IntNullValue);

 if (BtnSaveShelf.Text == "Save")
      {
            DataRow[] dr = dtshelf.Select("rackNo='" + txtRackNo.Text.Trim() + "'");
            if (dr.Length > 0)
            {
           //     return false;
            }
    }
      //  else
        //{
          //  DataRow[] dr = dtshelf.Select("rackNo <>'" + txtRackNo.Text.Trim() + "'");
            //if (dr.Length > 0)
            //{
             //   return false;
          //  }
       // }
        return true;
    }
    protected void btnSaveShelf_Click(object sender, EventArgs e)
    {
        if (checkDuplicateShelf())
        {
            bool isflag = false;
            int status = 1;
            if (!chbShelf.Checked)
            {
                status = 0;
            }
            if (BtnSaveShelf.Text == "Save")
            {
                isflag = mController.InsertRack(txtShelf.Text, status, int.Parse(Session["UserId"].ToString()), Constants.Shelf, int.Parse (DrpRackShelf .SelectedValue));

            }
            else
            {
                isflag = mController.UpdateRack(int.Parse(HdnShelfID.Value), txtShelf.Text, status, int.Parse(Session["UserId"].ToString()), Constants.Shelf, int.Parse (DrpRackShelf .SelectedValue));
            }
            if (isflag)
            {
                lblErrorShelf.Text = "Insert Successfully.";
                ClearAllShelf();
                LoadGirdShelf();
                LoadShelf();
               // LoadRackShelf();
            }
            else
            {
                lblErrorShelf.Text = "Some error occurred.";
            }
        }
        else
        {
            lblErrorShelf.Text = "Shelf No already exist.";
        }
    }
    protected void BtncancelShelf_Click(object sender, EventArgs e)
    {
        ClearAllShelf();
    }
    
    protected void GrdShelf_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            HdnShelfID.Value = int.Parse(GrdShelf.Rows[e.NewEditIndex].Cells[0].Text).ToString();
            BtnSaveShelf.Text = "Update";
            txtShelf.Text = GrdShelf.Rows[e.NewEditIndex].Cells[1].Text;
            chbShelf.Checked = Convert.ToBoolean(GrdShelf.Rows[e.NewEditIndex].Cells[2].Text);
            DrpRackShelf.SelectedValue = GrdShelf.Rows[e.NewEditIndex].Cells[3].Text;
            for (int i = 0; i < GrdShelf.Rows.Count; i++)
            {
                GrdShelf.Rows[i].Cells[4].Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }

    }

    private void ClearAllShelf()
    {
        txtShelf.Text = "";
        lblErrorShelf.Text = "";
        HdnShelfID.Value = "";
        BtnSaveShelf.Text = "Save";
        
    }

    #endregion

    #region Item Placement

    private void LoadDistributor()
    {

        drpDistributor.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        DataTable dt2 = dt.Clone();
        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 5");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        drpDistributor.DataTextField = "DISTRIBUTOR_NAME";
        drpDistributor.DataValueField = "DISTRIBUTOR_ID";

        drpDistributor.DataSource = dt2;
        drpDistributor.DataBind();

        //clsWebFormUtil.FillDropDownList(drpDistributor, dt2, 0, 2, true);
    }
    private void LoadRack()
    {
        DrpRack.Items.Clear();
        if (DrpAisle.Items.Count > 0)
        {
            DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, 1, Constants.Rack, int.Parse(DrpAisle.SelectedValue));
            if (dtRack.Rows.Count <= 0) return;
            {
                DrpRack.DataTextField = "rackNo";
                DrpRack.DataValueField = "rackId";
                DrpRack.DataSource = dtRack;
                DrpRack.DataBind();
            }
        }
        else
        {
            DrpRack.Items.Clear();
        }
    }
    private void LoadAisle()
    {
        DrpAisle.Items.Clear();
        DrpAisleRack.Items.Clear();
        DrpAisleShelf.Items.Clear();

        DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, 1, Constants.Aisle, Constants.IntNullValue);
        if (dtRack.Rows.Count <= 0) return;
        {

            DrpAisle.DataTextField = "rackNo";
            DrpAisle.DataValueField = "rackId";

            DrpAisle.DataSource = dtRack;
            DrpAisle.DataBind();


            DrpAisleRack.DataTextField = "rackNo";
            DrpAisleRack.DataValueField = "rackId";

            DrpAisleRack.DataSource = dtRack;
            DrpAisleRack.DataBind();


            DrpAisleShelf.DataTextField = "rackNo";
            DrpAisleShelf.DataValueField = "rackId";

            DrpAisleShelf.DataSource = dtRack;
            DrpAisleShelf.DataBind();

            // clsWebFormUtil.FillDropDownList(ddlRack, dtRack, 0, 1, true);
        }
    }
    private void LoadShelf()
    {
        DrpShelf.Items.Clear();
        if (DrpRack.Items.Count > 0)
        {
            DataTable dtRack = mController.SelectRackDefination(Constants.IntNullValue, null, 1, Constants.Shelf, int.Parse(DrpRack.SelectedValue));
            if (dtRack.Rows.Count <= 0) return;
            {

                DrpShelf.DataTextField = "rackNo";
                DrpShelf.DataValueField = "rackId";

                DrpShelf.DataSource = dtRack;
                DrpShelf.DataBind();

                // clsWebFormUtil.FillDropDownList(ddlRack, dtRack, 0, 1, true);
            }
        }
        else
        {
            DrpShelf.Items.Clear();
        }
    }
    private void LoadSkus()
    {
        ddlSkus.Items.Clear();
        var mSkuController = new SKUPriceDetailController();

        DataTable mDt = mSkuController.SelectDataPrice2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 3, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

        ddlSkus.DataTextField = "SkuDetail";
        ddlSkus.DataValueField = "SKU_CODE";

        ddlSkus.DataSource = mDt;
        ddlSkus.DataBind();

        // clsWebFormUtil.FillDropDownList(ddlSkus, mDt, 0, 9, true);
 
    }

    private void LoadPlacementGrid()
    {
        grdPlacement.DataSource = null;
        grdPlacement.DataBind();
        if (DrpShelf.Items.Count > 0)
        {
            DataTable dtRackPlace = mController.SelectRackPlacement(Constants.IntNullValue, int.Parse(DrpShelf.SelectedValue), int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);

            switch (ddSearchType2.SelectedIndex)
            {
                case 1:
                    dtRackPlace.DefaultView.RowFilter = ddSearchType2.SelectedValue.ToString() + " like '%" + txtSearchPlacement.Text + "%'";
                    break;
                case 2:
                    dtRackPlace.DefaultView.RowFilter = ddSearchType2.SelectedValue.ToString() + " like '%" + txtSearchPlacement.Text + "%'";
                    break;
                default:
                    dtRackPlace.DefaultView.RowFilter = "rackNo" + " like '%" + "" + "%'";
                    break;
            }
            grdPlacement.EditIndex = -1;
            grdPlacement.DataSource = dtRackPlace;
            grdPlacement.DataBind();
        }
    }
    protected void btnSavePlacement_Click(object sender, EventArgs e)
    {
        if (checkDuplicate())
        {
            bool isflag = false;

            int status = 1;
            if (!chkIsStatus.Checked)
            {
                status = 0;
            }
            if (btnSavePlacement.Text == "Save")
            {
                isflag = mController.InsertRackPlacement(int.Parse(DrpRack.SelectedValue), int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, status, int.Parse(Session["UserId"].ToString()), int.Parse(DrpAisle.SelectedValue), int.Parse(DrpShelf.SelectedValue),ddlSkus.SelectedValue);

            }
            else
            {
                isflag = mController.UpdateRackPlacement(int.Parse(HdnrackPlacementID .Value), int.Parse(DrpRack.SelectedValue), int.Parse(drpDistributor.SelectedValue), int.Parse(ddlSkus.SelectedValue), status, int.Parse(Session["UserId"].ToString()),int.Parse(DrpAisle.SelectedValue), int.Parse(DrpShelf.SelectedValue), ddlSkus.SelectedValue);
            }
            if (isflag)
            {
                lblErrorMsg2.Text = "Insert Successfully.";
                ClearPlacement();
                LoadPlacementGrid();
            }
            else
            {
                lblErrorMsg2.Text = "Some error occurred.";
            }
        }
        else
        {
            lblErrorMsg2.Text = "Item Already exist on this Rack.";
        }
    }
    protected void btnCancelPlacement_Click(object sender, EventArgs e)
    {
        ClearPlacement();
        LoadPlacementGrid();
    }
    protected void btnFilterPlacement_Click(object sender, EventArgs e)
    {
        LoadPlacementGrid();
    }


    protected void grdPlacement_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HdnrackPlacementID.Value = int.Parse(grdPlacement.Rows[e.NewEditIndex].Cells[3].Text).ToString();
        btnSavePlacement.Text = "Update";
        drpDistributor.SelectedValue = grdPlacement.Rows[e.NewEditIndex].Cells[0].Text;
        DrpRack.SelectedValue = grdPlacement.Rows[e.NewEditIndex].Cells[1].Text;
       
      

        drpDistributor.Enabled = false;
        ddlSkus.Enabled = false;
        chkIsStatus.Checked = Convert.ToBoolean(grdPlacement.Rows[e.NewEditIndex].Cells[6].Text);

        DrpShelf.SelectedValue = grdPlacement.Rows[e.NewEditIndex].Cells[7].Text;
        DrpAisle.SelectedValue = grdPlacement.Rows[e.NewEditIndex].Cells[8].Text;
        ddlSkus.SelectedValue = grdPlacement.Rows[e.NewEditIndex].Cells[9].Text;
        for (int i = 0; i < grdPlacement.Rows.Count; i++)
        {
            grdPlacement.Rows[i].Cells[10].Enabled = false;
        }

    }

    private void ClearPlacement()
    {
        lblErrorMsg2.Text = "";
        //DrpShelf.SelectedIndex = 0;
        //DrpAisle.SelectedIndex = 0;
        //DrpRack.SelectedIndex = 0;
        //ddlSkus.SelectedIndex = 0;

        btnSavePlacement.Text = "Save";
        drpDistributor.Enabled = true;
        ddlSkus.Enabled = true;
    }

    private bool checkDuplicate()
    {
        DataTable dtRackPlace = mController.SelectRackPlacement(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
       // if (btnSavePlacement.Text == "Save")
       // {
            DataRow[] dr = dtRackPlace.Select("rackId='" + DrpRack.SelectedValue + "' and sku_Code='" + ddlSkus.SelectedValue  + "' and ShelfId='" + DrpShelf.SelectedValue  + "' and AisleId='" + DrpAisle.SelectedValue + "'");
            if (dr.Length > 0)
            {
                return false;
            }
        //}
        //else {
        //    DataRow[] dr = dtRackPlace.Select("rackId <>'" + DrpRack.SelectedValue + "' and skuId='" + ddlSkus.SelectedValue  + "' and ShelfId='" + DrpShelf.SelectedValue + "' and AisleId='" + DrpAisle.SelectedValue + "'");
        //    if (dr.Length == 0)
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    #endregion

 

    protected void DrpAisleRack_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGirdRack();
    }

    protected void DrpAisleShelf_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRackShelf();
        LoadShelf();
        LoadGirdShelf();
    }

    protected void DrpRackShelf_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGirdShelf();
        LoadShelf();
    }

    protected void DrpAisle_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRack();
        LoadShelf();
        LoadPlacementGrid();
    }

    protected void DrpRack_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadShelf();
        LoadPlacementGrid();
    }

    protected void DrpShelf_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadPlacementGrid();
    }
}