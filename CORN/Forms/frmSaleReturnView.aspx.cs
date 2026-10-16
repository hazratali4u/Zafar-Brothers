using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmSaleReturnView : System.Web.UI.Page
{
    readonly OrderEntryController _purCtl = new OrderEntryController();

 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            Session.Remove("srId");

            GetSalesReturn(int.Parse(Session["Distributor_Id"].ToString()),Constants.LongNullValue, 5);

            FillRepeater();
        }
    }

    private void FillRepeater()
    {
        rptSaleReturn.DataSource = null;
        rptSaleReturn.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        var pagedResults = new PagedDataSource { AllowPaging = true, PageSize = 14 };

        var dt = (DataTable)Session["dtOrder"];

        DataView view = new DataView(dt);
        DataTable dt2 = view.ToTable(true, "SALE_INVOICE_ID", "customer_name", "NetReturn", "DOCUMENT_DATE", "LAST_UPDATE", "SALES_RETURN_ID");

        pagedResults.DataSource = dt2.DefaultView;
        if (pagedResults.Count > 0)
        {
            pagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !pagedResults.IsLastPage;
            linkbtnprev.Enabled = !pagedResults.IsFirstPage;
            rptSaleReturn.DataSource = pagedResults;
            rptSaleReturn.DataBind();

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

    public DataTable GetSalesReturn(int pDistributorId, long pId, int pTypeId)
    {
        DataTable dtOrder = _purCtl.GetDocumentDetail(int.Parse(Session["Distributor_Id"].ToString()),Constants.LongNullValue, 5);
        Session.Add("dtOrder", dtOrder);
        return dtOrder;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEnquiryView.aspx?TopMenu=" + Request.QueryString["TopMenu"] + "&LevelID=" + Request.QueryString["LevelID"]);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        DataTable dt = _purCtl.GetDocumentDetail(int.Parse(Session["Distributor_Id"].ToString()), Constants.LongNullValue, 5);

        DataTable dtFilter = dt.Clone();
        DataRow[] foundRows = dt.Select("customer_name LIKE '%" + txtProduct.Text + "%'");
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
    protected void rptSaleReturn_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
         
        if (e.CommandName == "view")
        {
            long srId = Convert.ToInt64(e.CommandArgument);
            Session.Add("srId", srId);

            Response.Redirect("frmSaleReturnDoc.aspx?LevelType=3&LevelID=156");
        }
        else
        {
            bool isValid = _purCtl.Delete_SaleReturn(Convert.ToInt64(e.CommandArgument));
            if (!isValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred!');", true);
            }
            else
            {
                GetSalesReturn(int.Parse(Session["Distributor_Id"].ToString()), Constants.LongNullValue, 5);

                FillRepeater();
            }
        }
    }
}