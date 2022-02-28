using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Data_Admin;

namespace BEEERP.Controllers.Data_Admin 
{
    [ShowNotification]
    public class MROBController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MROB
        public ActionResult MROB()
        {
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
            return View();
        }

        public ActionResult GetEmployeeDetails(int empId)
        {
            var employee = context.Employees.FirstOrDefault(x => x.Id == empId);

            if(employee != null)
            {
                string Name = employee.Name;
                //string FunctDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == employee.FunctDesignation).FuncDesignation;
                string Designation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                string WorkStation = context.BranchInformation.FirstOrDefault(x => x.Slno == employee.BranchID).BrnachName;
                string JoinDate = employee.JoiningDate.ToString("dd-MM-yyyy");
                string Department = context.Department.FirstOrDefault(x => x.DeptmentID == employee.DepatmentID).DeprtmntName;

                var mrObExist = context.MoneyRequisitionOB.FirstOrDefault(x => x.EmployeeId == empId);
                if(mrObExist != null)
                {
                    decimal Amount = mrObExist.Amount;
                    string RefNo = mrObExist.RefNo;
                    string Description = mrObExist.Description;
                    //string MROBDate = mrObExist.MROBDate.ToString("dd-MM-yyyy");

                    return Json(new { Name, Designation, WorkStation, JoinDate, Department, Amount, RefNo, Description }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string Amount = "";
                    string RefNo = "";
                    string Description = "";
                    //string MROBDate = DateTime.Now.ToString("dd-MM-yyyy");

                    return Json(new { Name,  Designation, WorkStation, JoinDate, Department, Amount, RefNo, Description }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        //public ActionResult SaveMROB(MoneyRequisitionOB mrob)
        //{
        //    if(mrob != null)
        //    {
        //        mrob.MROBDate = mrob.MROBDate.Add(DateTime.Now.TimeOfDay);
        //        context.MoneyRequisitionOB.Add(mrob);
        //        AccountModuleBridge.InsertUpdateFromMoneyRequisitionOB(ref context, mrob);

        //        context.SaveChanges();
        //        return Json(mrob, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //public ActionResult UpdateMROB(MoneyRequisitionOB mrob)
        //{
        //    var mrobExist = context.MoneyRequisitionOB.FirstOrDefault(x => x.EmployeeId == mrob.EmployeeId);
        //    if(mrobExist != null)
        //    {
        //        context.MoneyRequisitionOB.Remove(mrobExist);
        //        mrob.MROBDate = mrob.MROBDate.Add(DateTime.Now.TimeOfDay);
        //        context.MoneyRequisitionOB.Add(mrob);
        //        AccountModuleBridge.InsertUpdateFromMoneyRequisitionOB(ref context, mrob);

        //        context.SaveChanges();
        //        return Json(mrob, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult DeleteMROB(int id)
        {
            var mrobExist = context.MoneyRequisitionOB.FirstOrDefault(x => x.EmployeeId == id);
            if(mrobExist != null)
            {
                context.MoneyRequisitionOB.Remove(mrobExist);
                AccountModuleBridge.DeleteFromMoneyRequisitionOB(ref context, mrobExist);

                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }

}