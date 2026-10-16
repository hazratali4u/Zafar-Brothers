using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using CORNCommon.Classes;

public partial class Forms_RptGatePassFinance2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument CrpReport2 = (ReportDocument)Session["crpFinance2"];
        MemoryStream oStream = new MemoryStream();
        try {
            if (int.Parse(Session["ReportTypeFinance2"].ToString()) == 0)
            {

                
                oStream = (MemoryStream)CrpReport2.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
            else
            {
                string path = CORNCommon.Classes.Configuration.GetAppInstallationPath() + "\\Exported.xls";
                CrpReport2.SetDatabaseLogon("sa", "123");
                CrpReport2.ExportToDisk(ExportFormatType.Excel, path);
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

            CrpReport2.Close();
            CrpReport2.Dispose();
            oStream.Close();
            oStream.Dispose();
            Session.Remove("crpFinance2");

            GC.Collect();
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {
            CrpReport2.Close();
            CrpReport2.Dispose();
            oStream.Close();
            oStream.Dispose();
            Session.Remove("crpFinance2");

            GC.Collect();


        }
    }
}