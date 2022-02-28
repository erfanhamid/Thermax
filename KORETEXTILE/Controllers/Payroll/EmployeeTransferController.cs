using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.Security.Principal;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Payroll;
using BEEERP.Models.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EmployeeTransferController : Controller
    {

        // GET: EmployeeTransfer
        BEEContext context = new BEEContext();
        public ActionResult EmployeeTransfer()
        {
            //string sql = string.Format("exec GetEmployeeDetailsforTransfer");
            //ViewBag.list = context.Database.SqlQuery<EmployeeTransferVModel>(sql).ToList();
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            return View();
        }

        public ActionResult GetEmployeeDetails(int empId)
        {
            var employee = context.Employees.FirstOrDefault(x => x.Id == empId);

            if (employee != null)
            {
                var desigation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                var isExist = context.EmployeeTransfer.FirstOrDefault(x => x.EmployeeID == empId);
                if (isExist != null)
                {
                    isExist.PresentBranch = context.BranchInformation.FirstOrDefault(x => x.Slno == isExist.PresentBranchID).BrnachName;
                    isExist.PreviousBranch = context.BranchInformation.FirstOrDefault(x => x.Slno == isExist.PreviousBranchID).BrnachName;
                    return Json(new { message=1,isExist, desigation }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    return Json(new { message=0, desigation }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(new {message= 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveEmployeeTransfer(EmployeeTransfer Transfer)
        {
            using (context)
            {
                string transferID = "";
                string yearLastTwoPart = Transfer.EffectiveDate.Year.ToString().Substring(2, 2).ToString();
                var noOfTransfer = context.EmployeeTransfer.Count();

                if (noOfTransfer > 0)
                {
                    EmployeeTransfer lastTransfer = context.EmployeeTransfer.ToList().LastOrDefault(x => x.TransferID.ToString().Substring(0, 2) == yearLastTwoPart);
                    if (lastTransfer == null)
                    {
                        transferID = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        transferID = yearLastTwoPart + (Convert.ToInt32(lastTransfer.TransferID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    transferID = yearLastTwoPart + "00001";
                }
                Transfer.TransferID = Convert.ToInt32(transferID);
                var isexists = context.EmployeeTransfer.FirstOrDefault(x => x.EmployeeID == Transfer.EmployeeID);
                if (Transfer != null)
                {
                    context.EmployeeTransfer.Add(Transfer);
                    context.SaveChanges();
                    return Json(new { id = Transfer.EmployeeID, tid = Transfer.TransferID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.EmployeeTransfer.Add(Transfer);
                    context.SaveChanges();
                    return Json(new { id = Transfer.EmployeeID, tid = Transfer.TransferID }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        public ActionResult UpdateEmployeeTransfer(EmployeeTransfer Transfer)
        {
            var isExist = context.EmployeeTransfer.FirstOrDefault(x => x.EmployeeID == Transfer.EmployeeID);
            if (isExist != null)
            {
                context.EmployeeTransfer.Remove(isExist);
                //Transfer.Tdate = itob.Tdate.Add(DateTime.Now.TimeOfDay);
                context.EmployeeTransfer.Add(Transfer);
                context.SaveChanges();
                return Json(Transfer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteEmployeeTransfer(EmployeeTransfer Transfer)
        {
            var isExist = context.EmployeeTransfer.FirstOrDefault(x => x.EmployeeID == Transfer.EmployeeID);
            if (isExist != null)
            {
                context.EmployeeTransfer.Remove(isExist);
                context.SaveChanges();
                return Json(Transfer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}