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
    public partial class IDTSamplePreviewRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                IDTSamplePreviewR report = (IDTSamplePreviewR)Session["PreviewRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            IDTSamplePreviewR idtSamplePreviewR = new IDTSamplePreviewR();

            idtSamplePreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IDTSamplePreviewR.rpt");
            idtSamplePreviewR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IDTSamplePreview"] as ReportParmPersister;
            List<IDTSamplePreviewReport> data = HttpContext.Current.Session["IDTSamplePreviewReportData"] as List<IDTSamplePreviewReport>;
            idtSamplePreviewR.SetDataSource(data);

            idtSamplePreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            idtSamplePreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            idtSamplePreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            idtSamplePreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            idtSamplePreviewR.SetParameterValue("salesCenter", persister.BranchName);
            idtSamplePreviewR.SetParameterValue("postedBy", persister.PostedBy);

            CrystalReportViewer1.ReportSource = idtSamplePreviewR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["PreviewRDoc"] = idtSamplePreviewR;
        }
    }
}