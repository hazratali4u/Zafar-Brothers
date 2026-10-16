using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using OnBarcode.Barcode;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmBarcode : Page
{
    
    DataTable _dt = new DataTable();
     private static int RowNo;
    public static string CompanyName;
    readonly SkuController _cn = new SkuController();
    readonly DocumentPrintController mController = new DocumentPrintController();
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly DataControl _dc=new DataControl();
    private void CreateTable()
    {
        _dt = new DataTable();       
        _dt.Columns.Add("SKU_ID", typeof(int));
        _dt.Columns.Add("SKU_Code", typeof(string));
        _dt.Columns.Add("BAR_Code", typeof(string));
        _dt.Columns.Add("COLOR", typeof(string));
        _dt.Columns.Add("SKU_Name", typeof(string));
        _dt.Columns.Add("Quantity", typeof(decimal));
        _dt.Columns.Add("PRICE", typeof(decimal));
        _dt.Columns.Add("Code", typeof(byte[]));       
        Session.Add("_dt", _dt);
    }
    private void LoadSkuDetail()
    {
        ddlSKuCde.Items.Clear();
        DataTable dtskuPrice = PController.SelectDataPrice2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 18, true);
        Session.Add("Dtsku_Price", dtskuPrice);
    }
    private bool CheckDublicateSku()
    {
        _dt = (DataTable)Session["_dt"];
        DataRow[] foundRows = _dt.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        _dt = (DataTable)Session["_dt"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Item")
            {
                if (CheckDublicateSku())
                {
                    DataRow dr = _dt.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["BAR_Code"] = foundRows[0]["BAR_CODE"];
                    dr["COLOR"] = foundRows[0]["COLOR"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                    dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                    _dt.Rows.Add(dr);
                    txt_code.Text = foundRows[0]["BAR_CODE"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                }
            }
            else if (btnSave.Text == "Update Item")
            {
                DataRow dr = _dt.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["BAR_Code"] = foundRows[0]["BAR_CODE"];
                dr["COLOR"] = foundRows[0]["COLOR"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                dr["Quantity"] = decimal.Parse(txtQuantity.Text);
                txt_code.Text = foundRows[0]["BAR_CODE"].ToString();
            }
            Session.Add("_dt", _dt);
            LoadGird();          
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item please check in list');", true);
        }
    }       
    private void LoadGird()
    {
        _dt = (DataTable)Session["_dt"];
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = _dt;
        GrdPurchase.DataBind();
    }
    private void ClearAll()
    {
        txtQuantity.Text = "1";
        btnSave.Text = "Add Item";
        ddlSKuCde.Enabled = true;
    }
    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ddlSKuCde.Enabled = true;
        btnSave.Enabled = true;
        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[4].Enabled = false;
            GrdPurchase.Rows[i].Cells[5].Enabled = false;

        }
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update Item";
       

    }

    /// <summary>
    /// Deletes Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _dt = (DataTable)Session["_dt"];
        if (_dt.Rows.Count > 0)
        {
            _dt.Rows.RemoveAt(e.RowIndex);
            Session.Add("_dt", _dt);
            LoadGird();
        }
        ddlSKuCde.Enabled = true;
        btnSave.Enabled = true;
    }        
    private void LoadloginDetail()
    {
        try
        {

            DataTable dt = mController.SelectReportTitle(int.Parse(Session["Distributor_Id"].ToString()));

            if (dt.Rows.Count > 0)
            {
                CompanyName = dt.Rows[0]["COMPANY_NAME"].ToString();
            }
            else
            {
                CompanyName = "";
            }

           
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreateTable();
            LoadSkuDetail();
            LoadloginDetail();
            //aceProduct.ContextKey = Session["Distributor_Id"].ToString();
        }
       
        btn_generate.Attributes.Add("onclick", "return ValidateForm();");
    }
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        _dt = (DataTable)Session["_dt"];
        DataTable _insertDt = _dt.Clone();
        foreach (DataRow dr in _dt.Rows)
        {
            var oBitmap = Generatecode(dr["bar_code"].ToString());
            var ms = new MemoryStream();
            oBitmap.Save(ms, ImageFormat.Png);
            var bytearray = ms.ToArray();
            _insertDt.ImportRow(dr);
            
            if (this.Session["Distributor_Id"].ToString() == "5" || this.Session["Distributor_Id"].ToString() == "6" || this.Session["Distributor_Id"].ToString() == "7")
            {
                _cn.InsertBarcode("SALMAS", bytearray, _insertDt);
            }
            else
            {
                _cn.InsertBarcode("ZBG", bytearray, _insertDt);
            }
            _insertDt.Rows.Clear();
        }
        var sk = new SkuController();
        _dt = sk.SelectSkuBarcode();
        var crpReport = new CrpBarcode();
        crpReport.SetDataSource(_dt);
        crpReport.Refresh();
        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        clear();
    }
    private void clear()
    {
       // txt_productName.Text = "";
        txt_code.Text = "";
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        CreateTable();
            // txt_price.Text = "";

       // txt_productName.Focus();
       // txt_row.Text = "1";
    }
    private Bitmap Generatecode(string Bar_Code)
    {
        Linear barcode = new Linear();
        barcode.Type = BarcodeType.EAN13;
        barcode.Data = Bar_Code;
        barcode.LeftMargin = 0;
        barcode.RightMargin = 0;
        barcode.TopMargin = 0;
        barcode.BottomMargin = 0;
        barcode.Resolution = 72;
        barcode.UOM = UnitOfMeasure.PIXEL;
        byte[] barcodeInBytes = barcode.drawBarcodeAsBytes();
        Bitmap barcodeInBitmap = barcode.drawBarcode();
        return barcodeInBitmap;
    }
    protected void btn_generateSingle_Click(object sender, EventArgs e)
    {
        _dt = (DataTable)Session["_dt"];
        DataTable _insertDt = _dt.Clone();
        foreach (DataRow dr in _dt.Rows)
        {
            var oBitmap = Generatecode(dr["bar_code"].ToString());
            var ms = new MemoryStream();
            oBitmap.Save(ms, ImageFormat.Png);
            var bytearray = ms.ToArray();
            _insertDt.ImportRow(dr);
            if (this.Session["Distributor_Id"].ToString() == "5" || this.Session["Distributor_Id"].ToString() == "6" || this.Session["Distributor_Id"].ToString() == "7")
            {
                _cn.InsertBarcode("SALMAS", bytearray, _insertDt);
            }
            else
            {
                _cn.InsertBarcode("ZBG", bytearray, _insertDt);
            }
            _insertDt.Rows.Clear();
        }        
        var sk = new SkuController();
        _dt = sk.SelectSkuBarcode();        
        var crpReport = new CrpBarcodeSingle();
        crpReport.SetDataSource(_dt);
        crpReport.Refresh();
        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        var cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);        
        clear();
    }
}