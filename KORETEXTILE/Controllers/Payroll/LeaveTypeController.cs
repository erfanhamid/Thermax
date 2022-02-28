using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Payroll;
using System.Data.Entity;
using BEEERP.CrystalReport.ReportFormat;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class LeaveTypeController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: LeaveType
        public ActionResult LeaveType()
        {
            ViewBag.LeaveType = context.LeaveType.ToList();
            ViewBag.Id = generate.GeneratedLeaveTypeId(context); 
            return View();
        }

        [HttpPost]
        public ActionResult SaveLeaveType(LeaveType levType)
        {
            if (ModelState.IsValid)
            {
                var isLevTypeExist = context.LeaveType.FirstOrDefault(x => x.Leavename.ToLower() == levType.Leavename.ToLower());

                if(isLevTypeExist == null)
                {
                    context.LeaveType.Add(levType);
                    context.SaveChanges();
                    return RedirectToAction("LeaveType");
                }

                else
                {
                    ViewBag.messege = "This Leave Type is Already Exist";
                    ViewBag.LeaveType = context.LeaveType.ToList();
                    ViewBag.Id = generate.GeneratedLeaveTypeId(context);
                    return View("LeaveType", levType);
                }
                
            }

            else
            {
                ViewBag.LeaveType = context.LeaveType.ToList();
                ViewBag.Id = generate.GeneratedLeaveTypeId(context);
                return View("LeaveType", levType);
            }
        }
        [HttpPost]
        public ActionResult UpdateLeaveType(LeaveType leave)
        {
            var leaveExists = context.LeaveType.First<LeaveType>(a=>a.slno==leave.slno);

            if (leaveExists != null)
            {
                leaveExists.Leavename = leave.Leavename;
                leaveExists.NumberOfDays = leave.NumberOfDays;
                context.SaveChanges();
                return Json(new { id = leave.slno }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteLeaveTypeByid(int id)
        {
            var str = context.LeaveType.ToList().Find(x => x.slno == id);

            context.LeaveType.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintLeaveTypeReport()
        {
            Session["ReportName"] = "Leave Types";

            var items = context.LeaveType.ToList();
            if (items.Count == 0)
            {
                LeaveType report = new LeaveType();
                items.Add(report);
            }
            Session["LeaveType"] = items;
            return Redirect("/CrystalReport/LeaveTypeRpt.aspx");

        }
    }
}