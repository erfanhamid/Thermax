using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class SalaryProcessController : Controller
    {
        static bool found = false;
        BEEContext beeContext = new BEEContext();

        int salaryProcessNo;
        [HttpGet]
        public ActionResult SalaryProcess()
        {
            ViewBag.CompanyInfo = LoadComboBox.LoadCompanyInformation();
            return View();
        }
        [HttpPost]
        public ActionResult GetProcessData(SalaryProcessVModel salaryprocess, int update)
        {
            if (salaryprocess.FromDate.ToString() != "" && salaryprocess.ToDate.ToString() != "" && salaryprocess.ProcessDate.ToString() != "")
            {
                if (salaryprocess.CompanyId != 0)
                {
                    DateTime fromdate = new DateTime();
                    DateTime todate = new DateTime();
                    int MSE = 0;
                    bool flag = false;
                    if (update == 0)
                    {
                        var check = (from SIL in beeContext.SalaryInformationLine
                                     join SI in beeContext.SalaryInformation on SIL.SalaryInfoNo equals SI.SalaryInfoNo
                                     join E in beeContext.Employees on SIL.EmployeeId equals E.Id
                                     orderby SI.SalaryInfoNo descending
                                     select new { SalaryInfoNo = SI.SalaryInfoNo, FromDate = SI.FromDate, ToDate = SI.ToDate, ProcessDate = SI.ProcessDate }).Take(20).ToList();
                        foreach (var salary in check)
                        {
                            fromdate = salary.FromDate;
                            todate = salary.ToDate;
                            MSE = salary.SalaryInfoNo;
                            if (salary.FromDate.Date >= salaryprocess.FromDate.Date && salary.FromDate.Date <= salaryprocess.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salary.ToDate.Date >= salaryprocess.FromDate.Date && salary.ToDate.Date <= salaryprocess.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salaryprocess.FromDate.Date >= salary.FromDate.Date && salaryprocess.FromDate.Date <= salary.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salaryprocess.ToDate.Date >= salary.FromDate.Date && salaryprocess.ToDate.Date <= salary.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (check.Count > 0 && update == 0 && flag)
                        {
                            return Json(new { id = 0, message = "This company's salary From " + fromdate.ToString("dd-MM-yyyy") + " To " + todate.ToString("dd-MM-yyyy") + " is already processed (SP No : " + MSE + ").\nYour given from-date (" + salaryprocess.FromDate.ToString("dd-MM-yyyy") + ") or to-date (" + salaryprocess.ToDate.ToString("dd-MM-yyyy") + ") is conflicted between " + fromdate.ToString("dd-MM-yyyy") + " to " + todate.ToString("dd-MM-yyyy") + " \nPlease provide a valid date or update previous one" }, JsonRequestBehavior.AllowGet);
                        }
                        else if (update == 0 && !flag)
                        {
                            string yearLastTwoPart = salaryprocess.ProcessDate.Year.ToString().Substring(2, 2).ToString();
                            var noOfInvoice = beeContext.SalaryInformation.Count();
                            if (noOfInvoice > 0)
                            {
                                var lasProcess = beeContext.SalaryInformation.Where(x => x.SalaryInfoNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart).ToList();
                                if (lasProcess.Count > 0)
                                {
                                    var no = yearLastTwoPart + (Convert.ToInt32(lasProcess.LastOrDefault().SalaryInfoNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                                    salaryProcessNo = Convert.ToInt32(no);
                                }
                                else
                                {
                                    salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                                }
                            }
                            else
                            {
                                salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                            }
                            string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 0 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                            var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (update == 1)
                    {
                        var check = (from SIL in beeContext.SalaryInformationLine
                                     join SI in beeContext.SalaryInformation on SIL.SalaryInfoNo equals SI.SalaryInfoNo
                                     join E in beeContext.Employees on SIL.EmployeeId equals E.Id
                                     where SI.SalaryInfoNo != salaryprocess.SalaryInfoNo
                                     orderby SI.SalaryInfoNo descending
                                     select new { SalaryInfoNo = SI.SalaryInfoNo, FromDate = SI.FromDate, ToDate = SI.ToDate, ProcessDate = SI.ProcessDate }).Take(20).ToList();
                        foreach (var salary in check)
                        {
                            fromdate = salary.FromDate;
                            todate = salary.ToDate;
                            MSE = salary.SalaryInfoNo;
                            if (salary.FromDate.Date >= salaryprocess.FromDate.Date && salary.FromDate.Date <= salaryprocess.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salary.ToDate.Date >= salaryprocess.FromDate.Date && salary.ToDate.Date <= salaryprocess.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salaryprocess.FromDate.Date >= salary.FromDate.Date && salaryprocess.FromDate.Date <= salary.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                            else if (salaryprocess.ToDate.Date >= salary.FromDate.Date && salaryprocess.ToDate.Date <= salary.ToDate.Date)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (check.Count > 0 && update == 1 && flag)
                        {
                            return Json(new { id = 0, message = "This company's salary From " + fromdate.ToString("dd-MM-yyyy") + " To " + todate.ToString("dd-MM-yyyy") + " is already processed (SP No : " + MSE + ").\nYour given from-date (" + salaryprocess.FromDate.ToString("dd-MM-yyyy") + ") or to-date (" + salaryprocess.ToDate.ToString("dd-MM-yyyy") + ") is conflicted between " + fromdate.ToString("dd-MM-yyyy") + " to " + todate.ToString("dd-MM-yyyy") + " \nPlease provide a valid date or update previous one" }, JsonRequestBehavior.AllowGet);
                        }
                        else if (update == 1 && !flag)
                        {
                            salaryProcessNo = salaryprocess.SalaryInfoNo;
                            string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 0 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                            var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(false);
                        }
                    }
                    return Json(false);
                }
                else if (salaryprocess.CompanyId == 0)
                {
                    if (update == 0)
                    {
                        var companies = beeContext.CompanyInfo.ToList();
                        foreach (var item in companies)
                        {
                            var check = from SIL in beeContext.SalaryInformationLine
                                        join SI in beeContext.SalaryInformation on SIL.SalaryInfoNo equals SI.SalaryInfoNo
                                        join EMP in beeContext.Employees on SIL.EmployeeId equals EMP.Id
                                        join CMP in beeContext.CompanyInfo on EMP.CompanyId equals CMP.Id
                                        where (EMP.CompanyId == item.Id && SI.Month == salaryprocess.Month && SI.Year == salaryprocess.Year)
                                        select new { CompanyId = EMP.CompanyId, Month = SI.Month, Year = SI.Year, SalaryInfoNo = SI.SalaryInfoNo };
                            if (check != null)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found && update == 0)
                    {
                        string fullMonthName = new DateTime(salaryprocess.Year, salaryprocess.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                        return Json(new { id = 0, message = " Some companies salary already Processed for the month of " + fullMonthName + "," + salaryprocess.Year + "\nDo You Want to update ?" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (update == 1 && found)
                    {
                        string yearLastTwoPart = salaryprocess.ProcessDate.Year.ToString().Substring(2, 2).ToString();
                        var noOfInvoice = beeContext.SalaryInformation.Count();
                        if (noOfInvoice > 0)
                        {
                            var lasProcess = beeContext.SalaryInformation.Where(x => x.SalaryInfoNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart).ToList();
                            if (lasProcess.Count > 0)
                            {
                                var no = yearLastTwoPart + (Convert.ToInt32(lasProcess.LastOrDefault().SalaryInfoNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                                salaryProcessNo = Convert.ToInt32(no);
                            }
                            else
                            {
                                salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                            }
                        }
                        else
                        {
                            salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                        }
                        string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 0 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                        var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                        found = false;
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else if (update == 0 && !found)
                    {
                        string yearLastTwoPart = salaryprocess.ProcessDate.Year.ToString().Substring(2, 2).ToString();
                        var noOfInvoice = beeContext.SalaryInformation.Count();
                        if (noOfInvoice > 0)
                        {
                            var lasProcess = beeContext.SalaryInformation.Where(x => x.SalaryInfoNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart).ToList();
                            if (lasProcess.Count > 0)
                            {
                                var no = yearLastTwoPart + (Convert.ToInt32(lasProcess.LastOrDefault().SalaryInfoNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                                salaryProcessNo = Convert.ToInt32(no);
                            }
                            else
                            {
                                salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                            }
                        }
                        else
                        {
                            salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                        }
                        string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 0 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                        var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult SaveProcessSalaryData(SalaryProcessVModel salaryprocess)
        {
            if (salaryprocess.FromDate.ToString() != "" && salaryprocess.ToDate.ToString() != "" && salaryprocess.ProcessDate.ToString() != "")
            {
                if (salaryprocess.CompanyId != 0)
                {
                    string yearLastTwoPart = salaryprocess.ProcessDate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = beeContext.SalaryInformation.Count();
                    if (noOfInvoice > 0)
                    {
                        var lasProcess = beeContext.SalaryInformation.Where(x => x.SalaryInfoNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart).ToList();
                        if (lasProcess.Count > 0)
                        {
                            var no = yearLastTwoPart + (Convert.ToInt32(lasProcess.LastOrDefault().SalaryInfoNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            salaryProcessNo = Convert.ToInt32(no);
                        }
                        else
                        {
                            salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                        }
                    }
                    else
                    {
                        salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                    }
                    if (salaryprocess.SalaryInfoNo > 0)
                    {
                        salaryProcessNo = salaryprocess.SalaryInfoNo;
                        string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 1 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                        var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                        EmployeeCalculationBridge.InsertFormSalaryProcess(ref beeContext, result, salaryProcessNo, salaryprocess.ProcessDate, salaryprocess.FromDate, salaryprocess.ToDate);
                        AccountModuleBridge.IsnertFromSalaryProcess(ref beeContext, result, salaryprocess.ProcessDate, salaryProcessNo, ("Salary from " + salaryprocess.FromDate.ToString("dd-MM-yyyy") + " to " + salaryprocess.ToDate.ToString("dd-MM-yyyy")));
                        beeContext.SaveChanges();
                        return Json(new { id = salaryProcessNo, comid = salaryprocess.CompanyId, FromDate = salaryprocess.FromDate, ToDate = salaryprocess.ToDate, update = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 1 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                        var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                        EmployeeCalculationBridge.InsertFormSalaryProcess(ref beeContext, result, salaryProcessNo, salaryprocess.ProcessDate, salaryprocess.FromDate, salaryprocess.ToDate);
                        AccountModuleBridge.IsnertFromSalaryProcess(ref beeContext, result, salaryprocess.ProcessDate, salaryProcessNo, ("Salary from " + salaryprocess.FromDate.ToString("dd-MM-yyyy") + " to " + salaryprocess.ToDate.ToString("dd-MM-yyyy")));
                        beeContext.SaveChanges();

                        if (result != null)
                        {
                            return Json(new { id = salaryProcessNo, comid = salaryprocess.CompanyId, FromDate = salaryprocess.FromDate, ToDate = salaryprocess.ToDate, update = 0 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (salaryprocess.CompanyId == 0)
                {
                    string yearLastTwoPart = salaryprocess.ProcessDate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = beeContext.SalaryInformation.Count();
                    if (noOfInvoice > 0)
                    {
                        var lasProcess = beeContext.SalaryInformation.Where(x => x.SalaryInfoNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart).ToList();
                        if (lasProcess.Count > 0)
                        {
                            var no = yearLastTwoPart + (Convert.ToInt32(lasProcess.LastOrDefault().SalaryInfoNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            salaryProcessNo = Convert.ToInt32(no);
                        }
                        else
                        {
                            salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                        }
                    }
                    else
                    {
                        salaryProcessNo = Convert.ToInt32(yearLastTwoPart + "00001");
                    }
                    string sql = string.Format("exec salaryprocess '" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ToDate) + "', '" + salaryprocess.CompanyId + "','" + 1 + "'," + salaryProcessNo + ",'" + DateTimeFormat.ConvertToDDMMYYYY(salaryprocess.ProcessDate) + "'");
                    var result = beeContext.Database.SqlQuery<SalaryProcessViewModel>(sql).ToList();
                    if (result != null)
                    {
                        return Json(new { id = salaryProcessNo, update = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PrintVoucher(SalaryProcessVModel model)
        {
            var isExist = beeContext.SalaryInformation.FirstOrDefault(x => x.SalaryInfoNo == model.SalaryInfoNo);
            if (isExist != null)
            {
                model.CompanyId = beeContext.Employees.FirstOrDefault(x => x.Id == beeContext.SalaryInformationLine.FirstOrDefault(y => y.SalaryInfoNo == model.SalaryInfoNo).EmployeeId).CompanyId;
            }
            int serial = 1;
            List<SalaryProcessVoucherReport> salaryProcessVoucherReports = new List<SalaryProcessVoucherReport>();
            ReportParmPersister param = new ReportParmPersister();
            param.Month = "From " + model.FromDate.ToString("dd-MM-yyyy") + " To " + model.ToDate.ToString("dd-MM-yyyy");
            var company = beeContext.CompanyInfo.FirstOrDefault(x => x.Id == model.CompanyId).CompanyName;
            param.CompanyName = "Asian Petroleum Limited";
            param.BranchName = company;
            Session["SalaryProcessVoucherParam"] = param;
            string sql = string.Format("exec PreviewSalaryProcessVoucher " + model.SalaryInfoNo + "");
            var items = beeContext.Database.SqlQuery<SalaryProcessVoucherReport>(sql).ToList();
            if (items.Count == 0)
            {
                SalaryProcessVoucherReport report = new SalaryProcessVoucherReport();
                items.Add(report);
            }
            foreach (var item in items)
            {
                item.Serial = serial++;
                salaryProcessVoucherReports.Add(item);
            }
            Session["SalaryProcessVoucherData"] = salaryProcessVoucherReports;
            return Redirect("/CrystalReport/SalaryProcessVoucherReportRpt.aspx");
        }
        [HttpPost]
        public ActionResult GetSalaryInformation(int salaryinfono)
        {
            var datas = (from sil in beeContext.SalaryInformationLine
                         join si in beeContext.SalaryInformation on sil.SalaryInfoNo equals si.SalaryInfoNo
                         join e in beeContext.Employees on sil.EmployeeId equals e.Id
                         where si.SalaryInfoNo == salaryinfono
                         select new { SalaryInfoNo = si.SalaryInfoNo, FromDate = si.FromDate, ToDate = si.ToDate, ProcessDate = si.ProcessDate, EmployeeId = sil.EmployeeId, CompanyId = e.CompanyId, EmployeeName = e.Name, PayableSalary = sil.TotalPayableSalary }).ToList(); ;
            if (datas.Count > 0)
            {
                return Json(new { id = 1, datas = datas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}