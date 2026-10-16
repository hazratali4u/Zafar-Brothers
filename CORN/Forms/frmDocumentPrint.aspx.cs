using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;


public partial class Forms_frmDocumentPrint : System.Web.UI.Page
{
    readonly RptSaleController RptSaleCtl = new RptSaleController();
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly DataControl dc = new DataControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadSaleForce();
            LoadCustomer();

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");

        }
    }
    private void LoadDistributor()
    {
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
        clsWebFormUtil.FillDropDownList(drpDistributor, dt2, 0, 2, true);
    } 
    private void LoadSaleForce()
    {
        drpSaleForce.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(drpReportType.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        drpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
        clsWebFormUtil.FillDropDownList(drpSaleForce, m_dt, 0, 1);
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3, false);

        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
    }
    protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpReportType.SelectedValue == "1" || drpReportType.SelectedValue == "3")
        //{
        //    chkBranch.Visible = true;
        //    chkFinance.Visible = true;
        //    chkCustomer.Visible = true;
        //}
        //else if (drpReportType.SelectedValue == "2" || drpReportType.SelectedValue == "4")
        //{
        //  //  chkBranch.Visible = false;
        //   // chkFinance.Visible = false;
        //    chkCustomer.Visible = false;
        //}
    }
    private void ShowReport(int p_ReportType)
    {
        #region Print
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            if (drpReportType.SelectedValue == "1")// Order
            {
                #region Order
                ds = RptSaleCtl.SelectDocumentNo2(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpCustomer.SelectedValue)
                    , int.Parse(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
                  var x = chkCustomer.Checked;
                //var x = true;
                if (x)
                {
                    ReportDocument crpCustomer = new ReportDocument();
                    crpCustomer = new CrpPrintCustomerDocument();
                                        crpCustomer.SetDataSource(ds);
                    crpCustomer.Refresh();

                    crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpCustomer.SetParameterValue("ContactNo","Contact # :"+ dt.Rows[0]["CONTACT_NUMBER"].ToString()+ ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());

                    Session.Add("crpCustomer", crpCustomer);
                    Session.Add("ReportTypeCustomer", 0);

                    string url = "'RptGatePassCustomer.aspx'";
                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                }


                 var y = chkOffice.Checked;
                //var y = true;
                if (y)
                {
                    ReportDocument crpCustomer = new ReportDocument();
                    crpCustomer = new CrpPrintCustomerDocumentOffice();

                    crpCustomer.SetDataSource(ds);
                    crpCustomer.Refresh();

                    crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpCustomer.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());

                    //Session.Add("crpCustomer", crpCustomer);
                    //Session.Add("ReportTypeCustomer", 0);
                    Session.Add("ReportType2", 0);
                    Session.Add("CrpReport2", crpCustomer);
                    string url = "'Default2.aspx'";
                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");

                    //Session.Add("ReportType", 0);
                    //Session.Add("CrpReport", crpCustomer);
                    //string url = "'Default.aspx'";
                    //Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                }
                #endregion
            }
            
            else
            if (drpReportType.SelectedValue == "2")// invoice
            {
                #region Invoice
                ds = RptSaleCtl.SelectDocumentNo2(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpCustomer.SelectedValue)
                    , int.Parse(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
                if (chkCustomer.Checked)
                {                    ReportDocument crpCustomer = new ReportDocument();
                    crpCustomer = new CrpPrintCustomerDocumentDO();
                    crpCustomer.SetDataSource(ds);
                    crpCustomer.Refresh();
                    crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpCustomer.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());
                    Session.Add("crpCustomer", crpCustomer);
                    Session.Add("ReportTypeCustomer", 0);
                    const string url = "'RptGatePassCustomer.aspx'";
                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                }
                if (chkOffice.Checked)
                {
                    ReportDocument crpCustomer = new ReportDocument();
                    crpCustomer = new CrpPrintCustomerDocumentDOOffice();
                    crpCustomer.SetDataSource(ds);
                    crpCustomer.Refresh();
                    crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpCustomer.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());
                    Session.Add("ReportType4", 0);
                    Session.Add("CrpReport4", crpCustomer);
                    string url = "'Default4.aspx'";
                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                }
                if (chkDeliveryChallan.Checked)
                {
                    ReportDocument crpCustomer = new ReportDocument();
                    crpCustomer = new CrpPrintCustomerDocumentDODelivery();
                    crpCustomer.SetDataSource(ds);
                    crpCustomer.Refresh();
                    crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                    crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                    crpCustomer.SetParameterValue("ContactNo", "Contact # :" + dt.Rows[0]["CONTACT_NUMBER"].ToString() + ", Email :" + dt.Rows[0]["EMAIL_ADDRESS"].ToString());
                    Session.Add("ReportType2", 0);
                    Session.Add("CrpReport2", crpCustomer);
                    string url = "'Default2.aspx'";
                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");
                }
                #endregion
            }
        }
        #region OLD
        //        //------------if Document NO exist on txtDocumentNo.Text

        //        if (txtDocumentNo.Text != "")
        //        {
        //            DataTable table = RptSaleCtl.SelectDocumentNo(txtDocumentNo.Text + ',', 10);
        //            if (table != null)
        //            {
        //                if (table.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in table.Rows)
        //                    {
        //                        checkUom += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 3

        //                    }
        //                }
        //            }
        //            DataTable table2 = RptSaleCtl.SelectDocumentNo(txtDocumentNo.Text + ',', 11);
        //            if (table2 != null)
        //            {
        //                if (table2.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in table2.Rows)
        //                    {
        //                        checkUom2 += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 1,2

        //                    }
        //                }
        //            }
        //        }
        //            //------------------------------------------------------------------\\

        //        else
        //        {
        //DataTable table = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //    , Convert.ToInt32(drpSaleForce.SelectedValue), 3
        //    , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        //if (table.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        checkUom += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 3

        //    }
        //}

        //DataTable table2 = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //   , Convert.ToInt32(drpSaleForce.SelectedValue), 4
        //    , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        //if (table2.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in table2.Rows)
        //    {
        //        checkUom2 += dr["DocumentNo"].ToString() + ','; //get Invoice Id which unit in case is 1,2

        //    }
        //}
        //}
        //        if (checkUom != "")
        //        {
        //            if (txtDocumentNo.Text != "")
        //            {

        //            }
        //            else
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //              , Convert.ToInt32(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue.ToString())
        //              , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
        //            }



        // DataTable table = ds.Tables["spGetPrintSaleDocument"];
        //if (table.Rows.Count > 0)
        //{

        // }
        //                if (chkBranch.Checked)
        //                {
        //                    CrpPrintSaleDocument crpBranch = new CrpPrintSaleDocument();
        //                    crpBranch.SetDataSource(ds);
        //                    crpBranch.Refresh();

        //                    crpBranch.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                    crpBranch.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                    crpBranch.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                    Session.Add("CrpReport", crpBranch);
        //                    Session.Add("ReportType", 0);

        //                    const string url2 = "'Default.aspx'";
        //                    Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");

        //                }
        //                if (chkFinance.Checked)
        //                {
        //                    CrpPrintFinanceDocument crpFinance = new CrpPrintFinanceDocument();
        //                    crpFinance.SetDataSource(ds);
        //                    crpFinance.Refresh();

        //                    crpFinance.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                    crpFinance.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                    crpFinance.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                    Session.Add("crpFinance", crpFinance);
        //                    Session.Add("ReportTypeFinance", 0);

        //                    const string url3 = "'RptGatePassFinance.aspx'";
        //                    Response.Write("<script>window.open(" + url3 + ",'_blank');</script>");
        //                }
        //   }
        //        }

        //        if (checkUom2 != "")
        //        {
        //            if (txtDocumentNo.Text != "")
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom2, Constants.IntNullValue, Constants.IntNullValue
        //                , Constants.IntNullValue, int.Parse(drpReportType.SelectedValue.ToString())
        //                , Constants.DateNullValue, Constants.DateNullValue);
        //            }
        //            else
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom2, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //                       , Convert.ToInt32(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue.ToString())
        //                       , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
        //            }
        //            DataTable table = ds.Tables["spGetPrintSaleDocument"];
        //            if (table.Rows.Count > 0)
        //            {
        //                if (chkCustomer.Checked)
        //                {
        //                    ReportDocument crpCustomer2 = new ReportDocument();
        //                    crpCustomer2 = new CORNBusinessLayer.Reports.CrpPrintCustomerDocument2();

        //                    crpCustomer2.SetDataSource(ds);
        //                    crpCustomer2.Refresh();

        //                    crpCustomer2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                    crpCustomer2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                    crpCustomer2.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                    Session.Add("crpCustomer2", crpCustomer2);
        //                    Session.Add("ReportTypeCustomer2", 0);

        //                    const string url = "'RptGatePassCustomer2.aspx'";
        //                    Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        //                }
        //                if (chkBranch.Checked)
        //                {
        //                    CrpPrintSaleDocument2 crpBranch2 = new CrpPrintSaleDocument2();
        //                    crpBranch2.SetDataSource(ds);
        //                    crpBranch2.Refresh();

        //                    crpBranch2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                    crpBranch2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                    crpBranch2.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                    Session.Add("CrpReport2", crpBranch2);
        //                    Session.Add("ReportType2", 0);

        //                    const string url2 = "'Default2.aspx'";
        //                    Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");

        //                }
        //                if (chkFinance.Checked)
        //                {
        //                    CrpPrintFinanceDocument2 crpFinance2 = new CrpPrintFinanceDocument2();
        //                    crpFinance2.SetDataSource(ds);
        //                    crpFinance2.Refresh();

        //                    crpFinance2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                    crpFinance2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                    crpFinance2.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                    Session.Add("crpFinance2", crpFinance2);
        //                    Session.Add("ReportTypeFinance2", 0);

        //                    const string url3 = "'RptGatePassFinance2.aspx'";
        //                    Response.Write("<script>window.open(" + url3 + ",'_blank');</script>");
        //                }
        //            }
        //        }
        //    }
        //    else if(drpReportType.SelectedValue=="2") //Gate Pass
        //    {
        //        //------------if Document NO exist on txtDocumentNo.Text

        //        if (txtDocumentNo.Text != "")
        //        {
        //            DataTable table = RptSaleCtl.SelectDocumentNo(txtDocumentNo.Text + ',', 12);
        //            if (table != null)
        //            {
        //                if (table.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in table.Rows)
        //                    {
        //                        checkUom += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 1,2

        //                    }
        //                }
        //            }
        //            DataTable table2 = RptSaleCtl.SelectDocumentNo(txtDocumentNo.Text + ',', 13);
        //            if (table2 != null)
        //            {
        //                if (table2.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in table2.Rows)
        //                    {
        //                        checkUom2 += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 1,2

        //                    }
        //                }
        //            }
        //        }
        //        //------------------------------------------------------------------\\
        //        else
        //        {
        //            DataTable table = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //                , Convert.ToInt32(drpSaleForce.SelectedValue), 5
        //                , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        //            if (table.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in table.Rows)
        //                {
        //                    checkUom += dr["DocumentNo"].ToString() + ',';//get Invoice Id which unit in case is 1,2

        //                }
        //            }

        //            DataTable table2 = RptSaleCtl.SelectDocumentNo(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //                    , Convert.ToInt32(drpSaleForce.SelectedValue), 6
        //                    , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        //            if (table2.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in table2.Rows)
        //                {
        //                    checkUom2 += dr["DocumentNo"].ToString() + ','; //get Invoice Id which unit in case is 1,2

        //                }
        //            }
        //        }
        //        if (checkUom != "")
        //        {
        //            if (txtDocumentNo.Text != "")
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom, Constants.IntNullValue, Constants.IntNullValue
        //                , Constants.IntNullValue, int.Parse(drpReportType.SelectedValue.ToString())
        //                , Constants.DateNullValue, Constants.DateNullValue);
        //            }
        //            else
        //            {

        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //                                  , Convert.ToInt32(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue.ToString())
        //                                  , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        //            }
        //            CrpPrintGatePassWareHouse crpGatePass = new CrpPrintGatePassWareHouse();
        //            crpGatePass.SetDataSource(ds);
        //            crpGatePass.Refresh();

        //            crpGatePass.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //            crpGatePass.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //            crpGatePass.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());


        //            Session.Add("CrpReport", crpGatePass);
        //            Session.Add("ReportType", 0);

        //            const string url = "'Default.aspx'";
        //            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        //        }

        //        if (checkUom2 != "") 
        //        {
        //            if (txtDocumentNo.Text != "")
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom2, Constants.IntNullValue, Constants.IntNullValue
        //                , Constants.IntNullValue, int.Parse(drpReportType.SelectedValue.ToString())
        //                , Constants.DateNullValue, Constants.DateNullValue);
        //            }
        //            else
        //            {
        //                ds = RptSaleCtl.SelectDocumentforPrint(checkUom2, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue)
        //                                 , Convert.ToInt32(drpSaleForce.SelectedValue), int.Parse(drpReportType.SelectedValue.ToString())
        //                                 , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));
        //            }


        //            CrpPrintGatePassWareHouse2 crpGatePass2 = new CrpPrintGatePassWareHouse2();
        //            crpGatePass2.SetDataSource(ds);
        //            crpGatePass2.Refresh();

        //            crpGatePass2.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //            crpGatePass2.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //            crpGatePass2.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //            Session.Add("CrpReport2", crpGatePass2);
        //            Session.Add("ReportType2", 0);

        //            const string url2 = "'Default2.aspx'";
        //            Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");
        //        }
        //    }
        //    else if (drpReportType.SelectedValue == "3")
        //    {
        //        RptSaleController rptSaleCtl = new RptSaleController();

        //        if (txtDocumentNo.Text != "")
        //        {
        //            ds = RptSaleCtl.SelectSaleReturnReport(long.Parse(txtDocumentNo.Text), 1, Constants.DateNullValue, Constants.DateNullValue
        //             , Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        //        }
        //        else
        //        {
        //            ds = rptSaleCtl.SelectSaleReturnReport(Constants.LongNullValue, 1, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59")
        //             , int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(drpSaleForce.SelectedValue));
        //        }
        //        DataTable table = ds.Tables["spGetSaleReturnDocument"];

        //        if (table.Rows.Count > 0)
        //        {
        //            if (chkCustomer.Checked)
        //            {

        //                CrpPrintCustomerReturnDocument crpCustomer = new CrpPrintCustomerReturnDocument();
        //                crpCustomer.SetDataSource(ds);
        //                crpCustomer.Refresh();

        //                crpCustomer.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                crpCustomer.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                crpCustomer.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
        //                //---------------------------------------------------------------------------------\\

        //                Session.Add("crpCustomer", crpCustomer);
        //                Session.Add("ReportTypeCustomer", 0);

        //                const string url2 = "'RptGatePassCustomer.aspx'";
        //                Response.Write("<script>window.open(" + url2 + ",'_blank');</script>");

        //            }
        //            if (chkBranch.Checked)
        //            {
        //                CrpPrintSaleReturnDocument crpReport = new CrpPrintSaleReturnDocument();

        //                crpReport.SetDataSource(ds);
        //                crpReport.Refresh();

        //                crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                crpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                crpReport.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());


        //                Session.Add("CrpReport", crpReport);
        //                Session.Add("ReportType", 0);

        //                const string url = "'Default.aspx'";
        //                Response.Write("<script>window.open(" + url + ",'_blank');</script>");


        //            }
        //            if (chkFinance.Checked)
        //            {
        //                CrpPrintSaleReturnFinanceDocument crpFinance = new CrpPrintSaleReturnFinanceDocument();
        //                crpFinance.SetDataSource(ds);
        //                crpFinance.Refresh();

        //                crpFinance.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //                crpFinance.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //                crpFinance.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());

        //                Session.Add("crpFinance", crpFinance);
        //                Session.Add("ReportTypeFinance", 0);

        //                const string url3 = "'RptGatePassFinance.aspx'";
        //                Response.Write("<script>window.open(" + url3 + ",'_blank');</script>");
        //            }
        //        }
        //    }
        //    else if (drpReportType.SelectedValue == "4")
        //    {

        //        RptSaleController rptSaleCtl = new RptSaleController();

        //        if (txtDocumentNo.Text != "")
        //        {
        //            ds = RptSaleCtl.SelectSaleReturnReport(long.Parse(txtDocumentNo.Text), 2, Constants.DateNullValue, Constants.DateNullValue
        //             , Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        //        }
        //        else
        //        {
        //            ds = rptSaleCtl.SelectSaleReturnReport(Constants.LongNullValue, 2, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59")
        //              , int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(drpSaleForce.SelectedValue));
        //        }

        //        DataTable table = ds.Tables["spGetSaleReturnDocument"];
        //        if (table.Rows.Count <= 0) return;

        //        CrpPrintGatePassReturn crpWarehouse = new CrpPrintGatePassReturn();
        //        crpWarehouse.SetDataSource(ds);
        //        crpWarehouse.Refresh();

        //        crpWarehouse.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //        crpWarehouse.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        //        crpWarehouse.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
        //        //---------------------------------------------------------------------------------\\


        //        Session.Add("CrpReport2", crpWarehouse);
        //        Session.Add("ReportType2", 0);

        //        const string url = "'Default2.aspx'";
        //        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        //    }
        //  }
        #endregion
        #endregion

    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}