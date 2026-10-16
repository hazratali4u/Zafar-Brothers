using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmDiscountAuthority : System.Web.UI.Page
{
    readonly DistributorController mController = new DistributorController();
    readonly DataControl _dc = new DataControl();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        {
            LoadRepeater();
        }
    }
  
    private void LoadRepeater() 
    {
        
        DataTable dt = mController.SelectDiscountAuthority(0, int.Parse(drpDiscountOn.SelectedValue),int.Parse(rbldiscountType.SelectedValue));
         
        rptDiscount.DataSource = null;
        rptDiscount.DataBind();

        DataView view = new DataView(dt);
        DataTable dt2 = view.ToTable(true, "designationId", "designation", "discountAmount");

        rptDiscount.DataSource = dt2;
        rptDiscount.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable _discountAuthority = new DataTable();

            _discountAuthority.Columns.Add("designationId", typeof(int));
            _discountAuthority.Columns.Add("discountAmount", typeof(decimal));


            foreach (RepeaterItem gvr in rptDiscount.Items)
            {
                Label lblDesgId = (Label)gvr.FindControl("lblDesgId");
                TextBox discountAmount = (TextBox)gvr.FindControl("txtDiscountAmount");



                DataRow dr = _discountAuthority.NewRow();

                dr["designationId"] = int.Parse(lblDesgId.Text);
                dr["discountAmount"] = Convert.ToDecimal(_dc.chkNull_0(discountAmount.Text));

                _discountAuthority.Rows.Add(dr);
            }


            bool flag = mController.UpdateDiscountAuthority(int.Parse(drpDiscountOn.SelectedValue), int.Parse(rbldiscountType.SelectedValue)
                , int.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 2, _discountAuthority);
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insert successfully.');", true);
                LoadRepeater();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion failed!');", true);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion failed!');", true);
        }
    }

    protected void drpDiscountOn_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRepeater();
    }
    protected void rbldiscountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRepeater();
    }
}