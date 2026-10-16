using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CORNCommon.Classes;
using CrystalDecisions.Shared;

public partial class Forms_Default2 : System.Web.UI.Page
{
    System.IO.Stream oStream = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument CrpReport = (ReportDocument)Session["CrpReport2"];
        
        byte[] byteArray = null;
        try
        {
            if (int.Parse(Session["ReportType2"].ToString()) == 0)
            {

                oStream = (System.IO.Stream)CrpReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.End();
            }
            else
            {
                string path = CORNCommon.Classes.Configuration.GetAppInstallationPath() + "\\Exported.xls";
                CrpReport.SetDatabaseLogon("sa", "123");
                CrpReport.ExportToDisk(ExportFormatType.Excel, path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Write("This file does not exist.");
                }
            }
            CrpReport.Close();
            CrpReport.Clone();
            CrpReport.Dispose();
            this.Session.Remove("CrpReport2");
            oStream.Close();
            oStream.Dispose();
            GC.Collect();
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {
            if (CrpReport != null)
            {
                CrpReport.Close();
                CrpReport.Clone();
                CrpReport.Dispose();
            }
            if (this.Session["CrpReport2"] != null)
            {
                this.Session.Remove("CrpReport2");
            }
            if (oStream != null)
            {
                oStream.Flush();
                oStream.Close();
                oStream.Dispose();
            }
            GC.Collect();
        }
    }
}