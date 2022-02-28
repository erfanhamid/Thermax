﻿using BEEERP.CrystalReport.ReportFormat;
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
    public partial class BatchitemwiseFGPSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BatchitemwiseFGPSummaryR report = (BatchitemwiseFGPSummaryR)Session["BatchitemwiseFGPSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BatchitemwiseFGPSummaryR batchitemwiseFGPSummaryR = new BatchitemwiseFGPSummaryR();

            batchitemwiseFGPSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchitemwiseFGPSummaryR.rpt");
            batchitemwiseFGPSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["BatchitemwiseFGPSummaryParam"] as ReportParmPersister;
            List<BatchitemwiseFGPSummaryReport> data = HttpContext.Current.Session["BatchitemwiseFGPSummaryData"] as List<BatchitemwiseFGPSummaryReport>;
            batchitemwiseFGPSummaryR.SetDataSource(data);

            batchitemwiseFGPSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            batchitemwiseFGPSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = batchitemwiseFGPSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BatchitemwiseFGPSummaryDoc"] = batchitemwiseFGPSummaryR;
        }
    }
}