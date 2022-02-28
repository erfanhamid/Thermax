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
    public partial class SingleBatchFGPDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleBatchFGPDetailR report = (SingleBatchFGPDetailR)Session["SingleBatchFGPDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleBatchFGPDetailR singleBatchFGPDetailR = new SingleBatchFGPDetailR();

            singleBatchFGPDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseRMIssueSummaryR.rpt");
            singleBatchFGPDetailR.Load(APPPATH);

            //ReportParmPersister persister = HttpContext.Current.Session["SingleBatchRMCDetailparam"] as ReportParmPersister;
            List<SingleBatchFGPDetailReport> data = HttpContext.Current.Session["SingleBatchFGPDetailData"] as List<SingleBatchFGPDetailReport>;
            singleBatchFGPDetailR.SetDataSource(data);

            //singleBatchRMCDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //singleBatchRMCDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleBatchFGPDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleBatchFGPDetailDoc"] = singleBatchFGPDetailR;
        }
    }
}