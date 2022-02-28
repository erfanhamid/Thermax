using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.SalesModule;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]  
    public class DepotController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Depot
        public ActionResult Depot() 
        {
            ViewBag.DepotId = generate.GenerateDepotId(context);
            ViewBag.Depot = context.BranchInformation.ToList();

            return View();
        }

        public ActionResult GetDepotByid(int id)
        {
            var depot = context.BranchInformation.ToList().FirstOrDefault(x => x.Slno == id);

            if (depot == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new{ item = depot }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveDepot(BranchInformation depot)
        {
            var dpt = context.BranchInformation.ToList().FirstOrDefault(x => x.BrnachName.ToLower() == depot.BrnachName.ToLower().Trim());

            if(dpt == null)
            {
                context.BranchInformation.Add(depot);
                context.SaveChanges();
                return Json(depot.Slno, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateDepot(BranchInformation depot)
        {
            var dpt = context.BranchInformation.ToList().Find(x => x.Slno == depot.Slno);
            context.BranchInformation.Remove(dpt);

            context.BranchInformation.Add(depot);
            context.SaveChanges();
            return Json(depot.Slno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDepotByid(int id) 
        {
            var dpt = context.BranchInformation.ToList().Find(x => x.Slno == id); 

            context.BranchInformation.Remove(dpt);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);  
        }
    }
}