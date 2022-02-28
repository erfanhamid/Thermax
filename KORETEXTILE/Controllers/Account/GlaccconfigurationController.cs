using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class GlaccconfigurationController : Controller
    {
        // GET: Glaccconfiguration
        BEEContext context = new BEEContext();
        public ActionResult Glaccconfiguration()
        {
            var type = context.GlConfiguration.ToList();
            type.ForEach(x => x.AccName = context.ChartOfAccount.FirstOrDefault(p => p.Id == x.GlAccount).Name);
            ViewBag.AllGlConfiguration = type;
            ViewBag.AllExAccount = LoadComboBox.LoadAllExAccount();
            ViewBag.SubLedgerType = LoadComboBox.LoadAllSubLedgerType();
            return View();
        }
        public ActionResult AllGlAc()
        {
            var items = context.GlConfiguration.ToList();
            return Json( items ,JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveGL(GlConfiguration item)
        {
            
            var lastItem = context.GlConfiguration.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (lastItem!=null)
            {
                item.Id = lastItem.Id + 1;
                
            }
            else
            {
                item.Id = 1;
            }
            context.GlConfiguration.Add(item);
            UserLog.SaveUserLog(ref context, new UserLog(item.Id.ToString(), DateTime.Now, "GlAccconfiguration", ScreenAction.Save));
            context.SaveChanges();
            return Json(new { Id=item.Id}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGL(GlConfiguration item)
        {

            var exitem = context.GlConfiguration.FirstOrDefault(x => x.Id == item.Id);
            item.Id =exitem.Id;
            context.GlConfiguration.Remove(exitem);
            context.GlConfiguration.Add(item);
            UserLog.SaveUserLog(ref context, new UserLog(item.Id.ToString(), DateTime.Now, "GlAccconfiguration", ScreenAction.Update));
            context.SaveChanges();
            return Json(new { Id=item.Id}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteById(int id)
        {

            var lastItem = context.GlConfiguration.FirstOrDefault(x => x.Id == id);
            
            context.GlConfiguration.Remove(lastItem);
            UserLog.SaveUserLog(ref context, new UserLog(lastItem.Id.ToString(), DateTime.Now, "GlAccconfiguration", ScreenAction.Delete));
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult check(GlConfiguration item)
        {

            var lastItem = context.GlConfiguration.FirstOrDefault(x => x.VehicleType == item.VehicleType&&x.GlAccount==item.GlAccount);
            if (lastItem!=null)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}