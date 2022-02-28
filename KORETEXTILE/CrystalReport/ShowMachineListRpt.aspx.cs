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
    public partial class ShowMachineListRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ShowMachineListR report = (ShowMachineListR)Session["MachineListDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ShowMachineListR machinelist = new ShowMachineListR();

            machinelist.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ShowMachineListR.rpt");
            machinelist.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ShowMachineListparam"] as ReportParmPersister;
            List<MachineListReport> data = HttpContext.Current.Session["ShowMachineListData"] as List<MachineListReport>;
            machinelist.SetDataSource(data);

            machinelist.SetParameterValue("compAddress", persister.CompAddress);
            machinelist.SetParameterValue("compName", persister.CompName);
            //dwspinvsumm.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //dwspinvsumm.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            //machinelist.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = machinelist;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["MachineListDoc"] = machinelist;
        }
    }
}