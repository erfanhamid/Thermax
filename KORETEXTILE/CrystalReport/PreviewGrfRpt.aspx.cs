using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.StoreDepot;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class PreviewGrfRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                GrfPreviewR report = (GrfPreviewR)Session["PreviewRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            GrfPreviewR grfPreviewR = new GrfPreviewR();

            grfPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\GrfPreviewR.rpt");
            grfPreviewR.Load(APPPATH);    

            ReportParmPersister persister = HttpContext.Current.Session["GrfPreview"] as ReportParmPersister;
            List<GRFSampleReport> data = HttpContext.Current.Session["PreviewGrfReportData"] as List<GRFSampleReport>;
            grfPreviewR.SetDataSource(data);

            grfPreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            grfPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            grfPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            grfPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            grfPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            grfPreviewR.SetParameterValue("postedBy", persister.PostedBy);

            CrystalReportViewer1.ReportSource = grfPreviewR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["PreviewRDoc"] = grfPreviewR;
        }
    }
}