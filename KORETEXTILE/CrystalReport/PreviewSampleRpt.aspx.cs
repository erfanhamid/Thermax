using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class PreviewSampleR : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SamplePreviewR report = (SamplePreviewR)Session["SamplePreviewRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SamplePreviewR samplePreviewR = new SamplePreviewR();

            samplePreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SamplePreviewR.rpt");
            samplePreviewR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SamplePreview"] as ReportParmPersister;
            List<PreviewSampleReport> data = HttpContext.Current.Session["PreviewSampleReportData"] as List<PreviewSampleReport>;
            samplePreviewR.SetDataSource(data);


            samplePreviewR.SetParameterValue("tsoId", persister.TsoId);
            samplePreviewR.SetParameterValue("tsoName", persister.TsoName);
            samplePreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            samplePreviewR.SetParameterValue("sampleType", persister.SampleType);
            samplePreviewR.SetParameterValue("sampleDate", persister.SampleDate);
            samplePreviewR.SetParameterValue("depotName", persister.BranchName);
            samplePreviewR.SetParameterValue("postedBy", persister.PostedBy);

            CrystalReportViewer1.ReportSource = samplePreviewR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SamplePreviewRDoc"] = samplePreviewR;
        }
    }
}