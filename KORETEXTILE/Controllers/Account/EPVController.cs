using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Bridge;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class EPVController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EPV
        public ActionResult EPV()
        {
            ViewBag.accounts = LoadComboBox.LoadReceiveAcc();
            return View();
        }
        public ActionResult GetEmployeeById(int emId)
        {
            try
            {
                var employee = context.Employees.FirstOrDefault(m => m.Id == emId);
                if (employee!=null)
                {
                    var name = employee.Name;
                    var workstation = context.BranchInformation.FirstOrDefault(m => m.Slno == employee.BranchID).BrnachName;
                    var organicDesignation = context.Designation.FirstOrDefault(m => m.Id == employee.Designation).Name;
                   // var functionalDesignation = context.FunctionalDesignation.FirstOrDefault(m => m.ID == employee.FunctDesignation).FuncDesignation;
                    return Json(new { name = name, workstation = workstation, organicDesignation = organicDesignation }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch(Exception ex)
            {
                return Json(new { Message=0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveEPV(EPVEntry epv,List<EPVLineItem>epvLineItems) {

            try
            {
                string id = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfEmployeePayVoucher = context.EPV.Count();
                if (noOfEmployeePayVoucher > 0)
                {
                    var lastEPV = context.EPV.ToList().LastOrDefault(x => x.EPVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    id = yearLastTwoPart + (Convert.ToInt32(lastEPV.EPVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }
                epv.EPVNo = Convert.ToInt32(id);
                epv.Date = epv.Date.Add(DateTime.Now.TimeOfDay);
                
                context.EPV.Add(epv);
                AccountModuleBridge.InsertUpdateFromEmployeePaymentVoucher(ref context, epv, epvLineItems);
                epvLineItems.ForEach(m=> { m.EPVNo = epv.EPVNo;context.EPVLineItems.Add(m); });
                UserLog.SaveUserLog(ref context, new UserLog(epv.EPVNo.ToString(), DateTime.Now, "EPVEntry", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetEPVBy(EPVEntry epv, List<EPVLineItem> epvLineItems)
        {

            try
            {
                string id = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfEmployeePayVoucher = context.EPV.Count();
                if (noOfEmployeePayVoucher > 0)
                {
                    var lastEPV = context.EPV.ToList().LastOrDefault(x => x.EPVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    id = yearLastTwoPart + (Convert.ToInt32(lastEPV.EPVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }
                epv.EPVNo = Convert.ToInt32(id);
                epv.Date = epv.Date.Add(DateTime.Now.TimeOfDay);
                context.EPV.Add(epv);
                epvLineItems.ForEach(m => { m.EPVNo = epv.EPVNo; context.EPVLineItems.Add(m); });
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetEPV(int epvNo)
        {
            try
            {
                var epv = context.EPV.FirstOrDefault(m => m.EPVNo == epvNo);
                if (epv == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var epvLineItems = context.EPVLineItems.Where(m => m.EPVNo == epvNo).ToList();
                    epvLineItems.ForEach(d =>
                    {
                        d.Name = context.Employees.FirstOrDefault(p => p.Id == d.EmployeeId).Name;
                    });
                    return Json(new { Message = 1,epv=epv,epvLineItems=epvLineItems }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            

            
        }
        public ActionResult UpdateEPV(EPVEntry epv, List<EPVLineItem> epvLineItems)
        {

            try
            {
                epv.Date = epv.Date.Add(DateTime.Now.TimeOfDay);
                var prevEpv = context.EPV.FirstOrDefault(m => m.EPVNo == epv.EPVNo);
                context.EPV.Remove(prevEpv);
                var PrevlineItems = context.EPVLineItems.Where(d => d.EPVNo == epv.EPVNo).ToList();
                PrevlineItems.ForEach(m=> { context.EPVLineItems.Remove(m); });
                context.SaveChanges();
                context.EPV.Add(epv);
                epvLineItems.ForEach(m => { m.EPVNo = epv.EPVNo; context.EPVLineItems.Add(m); });
                AccountModuleBridge.InsertUpdateFromEmployeePaymentVoucher(ref context, epv, epvLineItems);

                UserLog.SaveUserLog(ref context, new UserLog(epv.EPVNo.ToString(), DateTime.Now, "EPVEntry", ScreenAction.Update));

                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteEPV(EPVEntry epv, List<EPVLineItem> epvLineItems)
        {

            try
            {
                epv.Date = epv.Date.Add(DateTime.Now.TimeOfDay);
                var prevEpv = context.EPV.FirstOrDefault(m => m.EPVNo == epv.EPVNo);
                context.EPV.Remove(prevEpv);
                var PrevlineItems = context.EPVLineItems.Where(d => d.EPVNo == epv.EPVNo).ToList();
                PrevlineItems.ForEach(m => { context.EPVLineItems.Remove(m); });

                AccountModuleBridge.DeleteFromAccEmployeePaymentVoucher(ref context,epv);

                UserLog.SaveUserLog(ref context, new UserLog(epv.EPVNo.ToString(), DateTime.Now, "EPVEntry", ScreenAction.Delete));

                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}