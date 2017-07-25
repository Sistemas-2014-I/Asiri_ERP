using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Common.Helper
{
    public class CrystalReportPdfResult : ActionResult
    {
        private readonly byte[] _contentBytes;
        TextObject textObject;

        public CrystalReportPdfResult(string reportPath, object dataSet, string pSection = null, string pTarget = null, string pValue = null)
        {
            try
            {
                var reportDocument = new ReportDocument();
                reportDocument.Load(reportPath);
                if (pTarget != null && pTarget != null && pValue != null)
                {
                    textObject = (TextObject)reportDocument.ReportDefinition.Sections[pSection].ReportObjects[pTarget];
                    textObject.Text = pValue;
                }
                reportDocument.SetDataSource(dataSet);
                _contentBytes = StreamToBytes(reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
            }
            catch (Exception e)
            {
                var log = LogManager.GetLogger("fileLogger");
                log.Error(e, "Excepción en: El constructor de  CrystalReportPdfResult.");
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {

            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(_contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
