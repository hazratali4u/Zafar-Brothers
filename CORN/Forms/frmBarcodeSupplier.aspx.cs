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

public partial class Forms_frmBarcodeSupplier : Page
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
        _dt.Columns.Add("Company_Id", typeof(int));
        _dt.Columns.Add("Company_Name", typeof(string));
        _dt.Columns.Add("SIZE", typeof(string));
        _dt.Columns.Add("Quantity", typeof(decimal));
        _dt.Columns.Add("Code", typeof(string));
     //   _dt.Columns.Add("BARCODE_IMAGE", typeof(byte[]));     
        Session.Add("_dt", _dt);
    }
    private void loadDocument()
    {
        drpSelectDocument.Items.Clear();
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 10, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        drpSelectDocument.Items.Add(new ListItem("New", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpSelectDocument, m_dt, 0, 1);
    }
    private void LoadSkuDetail()
    {
        drpSupplier.Items.Clear();
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 11, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
     clsWebFormUtil.FillDropDownList(drpSupplier, m_dt, "Company_Id", "Company_Name");
        Session.Add("Dtsku_Price", m_dt);
    }
    private bool CheckDublicateSku()
    {
        _dt = (DataTable)Session["_dt"];
        DataRow[] foundRows = _dt.Select("SIZE  = '" + txtSize.Text+ "'");
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
        DataRow[] foundRows = dtskuPrice.Select("Company_Id  = '" + drpSupplier.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Item")
            {
                if (CheckDublicateSku())
                {
                    drpSupplier.Enabled = false;
                    //txtcode.Enabled = false;
                    DataRow dr = _dt.NewRow();
                    dr["Company_Id"] = foundRows[0]["Company_Id"];
                    dr["Company_Name"] = foundRows[0]["Company_Name"];
                    dr["SIZE"] = (_dc.chkNull_0(txtSize.Text)); 
                    dr["Code"] = (_dc.chkNull_0(txtcode.Text));
                    dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtquantity.Text));
                    _dt.Rows.Add(dr);
                  //  txt_code.Text = foundRows[0]["BAR_CODE"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" +txtSize.Text+ " Already Exists ');", true);
                }
            }
            else if (btnSave.Text == "Update Item")
            {
                DataRow dr = _dt.Rows[RowNo];
                dr["Company_Id"] = foundRows[0]["Company_Id"];
                dr["Company_Name"] = foundRows[0]["Company_Name"];
                dr["SIZE"] = (_dc.chkNull_0(txtSize.Text));
                dr["Code"] = (_dc.chkNull_0(txtcode.Text));
                dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtquantity.Text));
            }
            Session.Add("_dt", _dt);
            LoadGird();          
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(drpSupplier);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Supplier please check in list');", true);
        }
        ClearAll();
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
        txtquantity.Text = "1";
        btnSave.Text = "Add Item";
        txtquantity.Enabled = true;
        txtSize.Text = "";
      //  txtcode.Text = "";
   
    }
    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        drpSupplier.Enabled = true;
        btnSave.Enabled = true;
        RowNo = e.NewEditIndex;
        if (drpSelectDocument.SelectedValue == Constants.IntNullValue.ToString())
        {
            drpSupplier.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        }
        else
        {
            drpSelectDocument.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        }
     
        txtquantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtSize.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtcode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        drpSupplier.Enabled = false;
        drpSelectDocument.Enabled = false;
        txtquantity.Focus();
        btnSave.Text = "Update Item";
        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[5].Enabled = false;
            GrdPurchase.Rows[i].Cells[6].Enabled = false;

        }

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
        drpSupplier.Enabled = true;
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
            loadDocument();
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
        var sk = new SkuController();
        if (drpSelectDocument.SelectedValue == Constants.IntNullValue.ToString())
        {
            _cn.InsertBarcodeSupplierMaster(int.Parse(drpSupplier.SelectedValue), drpSupplier.SelectedItem.Text, _dt,0);
            _dt = sk.SelectSkuBarcodeSupplier(int.Parse(drpSupplier.SelectedValue));
        }
        else
        {
            string sname = drpSelectDocument.SelectedItem.Text;
            string sss = sname.Substring(0,sname.IndexOf("<"));
            _cn.InsertBarcodeSupplierMaster(int.Parse(drpSelectDocument.SelectedValue), sss, _dt,1);
            _dt = sk.SelectSkuBarcodeSupplier(int.Parse(drpSelectDocument.SelectedValue));
        }
        var crpReport = new CrpBarcodeSupplier();
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
        loadDocument();
        LoadSkuDetail();
    }
    private void clear()
    {
        ClearAll();
        txtcode.Enabled = true;
        txt_code.Text = "";
        txtcode.Text = "";
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        CreateTable();
        drpSupplier.Enabled = true;
        drpSelectDocument.Enabled = true;
        drpSupplier.Visible = true;
        btnSave.Enabled = true;
       
          
    }
    private Bitmap Generatecode(string Bar_Code)
    {
        Linear barcode = new Linear();
        barcode.Type = BarcodeType.CODE128C;
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
        var sk = new SkuController();
        if (drpSelectDocument.SelectedValue == Constants.IntNullValue.ToString())
        {
            _cn.InsertBarcodeSupplierMaster(int.Parse(drpSupplier.SelectedValue), drpSupplier.SelectedItem.Text, _dt,0);
            _dt = sk.SelectSkuBarcodeSupplier(int.Parse(drpSupplier.SelectedValue));
        }
        else
        {
            string sname = drpSelectDocument.SelectedItem.Text;
            string sss = sname.Substring(0, sname.IndexOf("<"));
            _cn.InsertBarcodeSupplierMaster(int.Parse(drpSelectDocument.SelectedValue),sss, _dt,1);
            _dt = sk.SelectSkuBarcodeSupplier(int.Parse(drpSelectDocument.SelectedValue));
        }
        var crpReport = new CrpBarcodSupplierSingle();
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
        loadDocument();
        LoadSkuDetail();
    }

    protected void btn_void_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void drpSelectDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (drpSelectDocument.SelectedValue == Constants.IntNullValue.ToString())
        {
            CreateTable();
            Session.Add("_dt", _dt);
            LoadGird();
            drpSupplier.Enabled = true;
            drpSupplier.Visible = true;
            txtcode.Enabled = true;
        }
        else
        {
            drpSupplier.Enabled = false;
            drpSupplier.Visible = false;
            btnSave.Enabled = false;
            SKUPriceDetailController PController = new SKUPriceDetailController();
            _dt = PController.SelectSupplierDetailBarcode(int.Parse(drpSelectDocument.SelectedValue.ToString()));
            Session.Add("_dt", _dt);
            LoadGird();
           // txtcode.Enabled = false;
           
        }
    }
}