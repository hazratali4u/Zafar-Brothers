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


public partial class Forms_RptGatePassFinance : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument crpFinance = (ReportDocument)Session["crpFinance"];
        MemoryStream oStream = new MemoryStream();

        try
        {
            if (int.Parse(Session["ReportTypeFinance"].ToString()) == 0)
            {

                
                oStream = (MemoryStream)crpFinance.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
            else
            {
                string path = CORNCommon.Classes.Configuration.GetAppInstallationPath() + "\\Exported.xls";
                crpFinance.SetDatabaseLogon("sa", "123");
                crpFinance.ExportToDisk(ExportFormatType.Excel, path);
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
            crpFinance.Close();
            crpFinance.Dispose();
            oStream.Close();
            oStream.Dispose();
            Session.Remove("crpFinance");

            GC.Collect();
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {
            crpFinance.Close();
            crpFinance.Dispose();
            oStream.Close();
            oStream.Dispose();
            Session.Remove("crpFinance");
            
            GC.Collect();

           
        }
    }
}