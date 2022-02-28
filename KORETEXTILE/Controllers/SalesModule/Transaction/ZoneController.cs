using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule
{
    [ShowNotification]
    public class ZoneController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Zone
        public ActionResult Zone()
        {
            ViewBag.Zones = context.TblZones.ToList();
            ViewBag.ZoneId = generate.GenerateZoneId(context);
            return View();
        }
        [HttpPost]
        public ActionResult SaveZone(TblZone tblZone)
        {
            if(ModelState.IsValid)
            {
                if (tblZone.ZoneId==0||tblZone==null)
                {
                    return RedirectToAction("Zone");
                }
                else
                {
                    var prevZone = context.TblZones.AsNoTracking().FirstOrDefault(m => m.ZoneId == tblZone.ZoneId);
                    if (prevZone == null)
                    {
                        context.TblZones.Add(tblZone);
                        context.SaveChanges();
                        return RedirectToAction("Zone");
                    }
                    else
                    {
                        context.Entry<TblZone>(tblZone).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Zone");
                    }
                }
                
                
            }
            else
            {
                ViewBag.ZoneId = generate.GenerateZoneId(context);
                ViewBag.Zones = context.TblZones.ToList();
                return View("Zone");
            }
           
        }

        [HttpPost]
        public ActionResult UpdateZone(TblZone tblZone)
        {
            if (ModelState.IsValid)
            {
                context.Entry<TblZone>(tblZone).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Zone");
            }
            else
            {
                return View(tblZone);
            }
        }
        public ActionResult GetAllZone()
        {         
            return Json(context.TblZones.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZone(int zonId)
        {
            if (zonId == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var zone = context.TblZones.FirstOrDefault(m => m.ZoneId == zonId);
                return Json(new { zone = zone }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}