using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmPurchaseReceivedView : System.Web.UI.Page
{
    readonly PurchaseController _purCtl = new PurchaseController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            Session.Remove("grnId");
           
            GetGrn(Constants.LongNullValue, int.Parse(Session["Distributor_Id"].ToString()),1, Constants.DateNullValue);
        
            FillRepeater();
        }
    }

    private void FillRepeater()
    {
        rReserveSample.DataSource = null;
        rReserveSample.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        var pagedResults = new PagedDataSource {AllowPaging = true, PageSize = 20 };

        var dt = (DataTable)Session["dtOrder"];

        DataView view = new DataView(dt);
        DataTable dt2 = view.ToTable(true, "PURCHASE_ORDER_ID", "SUPPLIER", "DISTRIBUTOR", "ProductDetail", "DOCUMENT_DATE", "UpdatedBy", "LAST_UPDATE");

        pagedResults.DataSource = dt2.DefaultView;
        if (pagedResults.Count > 0)
        {
            pagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !pagedResults.IsLastPage;
            linkbtnprev.Enabled = !pagedResults.IsFirstPage;
            rReserveSample.DataSource = pagedResults;
            rReserveSample.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (pagedResults.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            return (int)objview;
        }
        set
        {
            ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }

    public DataSet GetGrn(long pPurchaseMasterId, int pDistributorId, int pTypeId, DateTime pDocumentDate)
    {
        DataSet dtOrder = _purCtl.GetGrn(pPurchaseMasterId, int.Parse(Session["Distributor_Id"].ToString()),1, Constants.DateNullValue);
        Session.Add("dtOrder", dtOrder.Tables[0]);
        return dtOrder;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEnquiryView.aspx?TopMenu=" + Request.QueryString["TopMenu"] + "&LevelID=" + Request.QueryString["LevelID"]);
    }

    protected void btnReserveFort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtOrder"];
        DataView dv = new DataView(dt);

        if (Session["RESERVE_FOR"] != null)
        {
            if (Session["RESERVE_FOR"].ToString() == "DESC")
            {
                dv.Sort = "RESERVE_FOR DESC";
                Session.Add("RESERVE_FOR", "ASC");
            }
            else
            {
                dv.Sort = "RESERVE_FOR ASC";
                Session.Add("RESERVE_FOR", "DESC");
            }
        }
        else
        {
            dv.Sort = "RESERVE_FOR ASC";
            Session.Add("RESERVE_FOR", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtOrder", dt);
        FillRepeater();

    }

    protected void btnDateSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtOrder"];
        DataView dv = new DataView(dt);

        if (Session["Date"] != null)
        {
            if (Session["Date"].ToString() == "DESC")
            {
                dv.Sort = "DOCUMENT_DATE DESC";
                Session.Add("Date", "ASC");
            }
            else
            {
                dv.Sort = "DOCUMENT_DATE ASC";
                Session.Add("Date", "DESC");
            }
        }
        else
        {
            dv.Sort = "DOCUMENT_DATE ASC";
            Session.Add("Date", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtOrder", dt);
        FillRepeater();

    }

    protected void btnLastUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtOrder"];
        DataView dv = new DataView(dt);

        if (Session["LAST_UPDATE"] != null)
        {
            if (Session["LAST_UPDATE"].ToString() == "DESC")
            {
                dv.Sort = "LAST_UPDATE DESC";
                Session.Add("LAST_UPDATE", "ASC");
            }
            else
            {
                dv.Sort = "LAST_UPDATE ASC";
                Session.Add("LAST_UPDATE", "DESC");
            }
        }
        else
        {
            dv.Sort = "LAST_UPDATE ASC";
            Session.Add("LAST_UPDATE", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtOrder", dt);
        FillRepeater();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {



        DataSet dt = _purCtl.GetGrn(Constants.LongNullValue, int.Parse(Session["Distributor_Id"].ToString()), 1, Constants.DateNullValue);
        
        DataTable dtFilter = dt.Tables[0].Clone();
        DataRow[] foundRows = dt.Tables[0].Select("SOLD_FROM LIKE '%" + txtProduct.Text + "%'");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dtFilter.ImportRow(t);
            }
        }


        Session.Add("dtOrder", dtFilter);
        FillRepeater();
    }
    protected void rReserveSample_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            long grnId = Convert.ToInt64(e.CommandArgument);
            Session.Add("grnId", grnId);
            
            Response.Redirect("frmPurchaseReceivedEntry.aspx?LevelType=3&LevelID=151");
        }
    }
}