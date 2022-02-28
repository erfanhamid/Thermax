using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EnterWorkerInfoController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EnterWorkerInfo
        public ActionResult EnterWorkerInfo()
        {
            ViewBag.Designation = LoadComboBox.LoadWorkerDesignation();
            ViewBag.Type = LoadComboBox.LoadWorkerType();
            ViewBag.Gender = LoadComboBox.LoadAllGender();
            ViewBag.Religion = LoadComboBox.LoadAllReligion();
            ViewBag.WorkStation = LoadComboBox.LoadBranchInfo();
            ViewBag.Section = LoadComboBox.LoadSection();
            return View();
        }

        public ActionResult SaveEWI(EnterWorkerInformation ewi)
        {
            ewi.DateOfBirth = ewi.DateOfBirth.Add(DateTime.Now.TimeOfDay);
            ewi.JoiningDate = ewi.JoiningDate.Add(DateTime.Now.TimeOfDay);
            try
            {
                context.EnterWorkerInformations.Add(ewi);
                UserLog.SaveUserLog(ref context, new UserLog(ewi.WorkerID.ToString(), DateTime.Now, "EnterWorkerInfo", ScreenAction.Save));
                context.SaveChanges();
                return Json(ewi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetEWIById(int workerid)
        {
            var ewi = context.EnterWorkerInformations.FirstOrDefault(x => x.WorkerID == workerid);
            {
                if (ewi != null)
                {
                    return Json(ewi, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateEWI(EnterWorkerInformation ewi)
        {
            var ewiExist = context.EnterWorkerInformations.AsNoTracking().FirstOrDefault(x => x.WorkerID == ewi.WorkerID);
            if (ewiExist != null)
            {
                context.Entry<EnterWorkerInformation>(ewi).State = EntityState.Modified;
                UserLog.SaveUserLog(ref context, new UserLog(ewi.WorkerID.ToString(), DateTime.Now, "EnterWorkerInfo", ScreenAction.Update));
                context.SaveChanges();
                return Json(ewi, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteEWIById(int wid)
        {
            var ewiExist = context.EnterWorkerInformations.FirstOrDefault(x => x.WorkerID == wid);
            if (ewiExist != null)
            {
                context.EnterWorkerInformations.Remove(ewiExist);
                UserLog.SaveUserLog(ref context, new UserLog(wid.ToString(), DateTime.Now, "EnterWorkerInfo", ScreenAction.Delete));
                context.SaveChanges();
                return Json(wid, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}