using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
    public class HolidayReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: HolidayReport
        public ActionResult HolydayDetails()
        {
            ViewBag.Month = LoadComboBox.LoadMonths();
            return View();
        }

        public ActionResult ShowHolidayDetails(ReportVModel model)
        {

            Session["ReportName"] = "HolidayDetails";

            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec ShowHolidayDetails'" + model.Month + "', '" + model.Year + "' ");
            var items = context.Database.SqlQuery<HolidayDetailsR>(sql).ToList();
            

            param.Year = model.Year;
            if(model.Month > 0)
            {
                var m = GetAbbreviatedMonthName(model.Month);

                param.Month = m;
            }
            else
            {
                param.Month = "Not Applicable";
            }
            
            if (items.Count == 0)
            {
                HolidayDetailsR report = new HolidayDetailsR();
                items.Add(report);
            }
            Session["HolidayDetailsData"] = items;
            Session["HolidayDetailsParam"] = param;
            //return  Json(new { result = "Redirect", url = Url.Action("Redirct", "HolidayReport") });
            return Redirect("/CrystalReport/HolidaydayReport.aspx");
        }
   
        private static string GetAbbreviatedMonthName(int month)
        {

            DateTime date = new DateTime(2010, month, 1);
            return date.ToString("MMMM");

        }
    }
}