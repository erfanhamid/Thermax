using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
    // [Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class PayrollReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PayrollReport
        public ActionResult PayrollReport()
        {
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            //ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            ViewBag.Employee = LoadComboBox.LoadActiveEmployees();
            return View();
        }
        // Single Branch Employee Pay Scale 
        public ActionResult SingleBranchEmployeePayScale(ReportVModel model)
        {
            Session["ReportName"] = "SingleBranchEmployeePayScaleR";

            ReportParmPersister param = new ReportParmPersister();
            //param.AsOnDate = model.AsOnDate;
            // param.ToDate = model.To;
            //param.ItemID = model.StoreId;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;
            param.StoreName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.DepotId).BrnachName;

            Session["SingleBranchEmployeePayScaleparam"] = param;

            string sql = string.Format("exec spSingleBranchEPS " + model.DepotId + " ");
            var items = context.Database.SqlQuery<SingleBranchEPSReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleBranchEPSReport report = new SingleBranchEPSReport();
                items.Add(report);
            }
            Session["SingleBranchEmployeePayScaleData"] = items;
            return Redirect("/CrystalReport/SingleBranchEmployeePayScaleRpt.aspx");
        }
    }
}