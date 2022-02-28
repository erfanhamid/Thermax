using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Data_Admin
{
    [ShowNotification]
    public class ScreenEntryLockController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ScreenEntryLock
        public ActionResult ScreenEntryLock()
        {
            ViewBag.Module = LoadComboBox.LoadAllModules();
            ViewBag.Screen = LoadComboBox.LoadScreenByModule(null);
            return View();
        }
        public JsonResult GetScreenByModule(int id)
        {
            return Json(LoadComboBox.LoadScreenByModule(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLockDateByScreen(int screenid)
        {
            var date = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == screenid).SELDate;
            return Json(date, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveScreenLock(ScreenEntryLock ScreenData)
        {
            if(ScreenData != null)
            {
                ScreenEntryLock isExist = context.ScreenEntryLocks.AsNoTracking().FirstOrDefault(x => x.ScreenID == ScreenData.ScreenID);
                if(isExist == null)
                {
                    context.ScreenEntryLocks.Add(ScreenData);
                }
                else
                {
                    ScreenData.ID = isExist.ID;
                    context.Entry<ScreenEntryLock>(ScreenData).State = EntityState.Modified;
                }
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
   
}