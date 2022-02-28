using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEEERP.Models.CommercialModule;
using System.Web.Mvc;
using System.Data.Entity;

namespace BEEERP.Controllers.CommercialModule.Settings
{
    [ShowNotification]
   // [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class IncotermController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Incoterm
        public ActionResult Incoterm()
        {
            ViewBag.incoterms = context.Incoterms.ToList();
            var savedIncoterms = context.Incoterms.Count();
            if (savedIncoterms == 0)
            {
                ViewBag.incotermNumber = 1;
            }
            else
            {
                ViewBag.incotermNumber = context.Incoterms.ToList().Max(m => m.IncotermsId) + 1;
            }
            return View();
        }
        public ActionResult SaveIncoterm(Incoterm incoterm)
        {

            var incotermExits = context.Incoterms.FirstOrDefault(x => x.IncotermsId == incoterm.IncotermsId);
            if (incotermExits == null)
            {
                try
                {
                    var savedIncoterms = context.Incoterms.Count();
                    if (savedIncoterms == 0)
                    {
                        incoterm.IncotermsId = 1;
                    }
                    else
                    {
                        incoterm.IncotermsId = context.Incoterms.ToList().Max(m => m.IncotermsId) + 1;
                    }
                    context.Incoterms.Add(incoterm);
                    context.SaveChanges();

                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }

            }
            else
            {
                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult GetIncotermsByIncotermsID(int incotermId)
        {
            var incoterm = context.Incoterms.FirstOrDefault(m => m.IncotermsId == incotermId);
            return Json(new { incoterm = incoterm, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateIncoterm(Incoterm incoterm)
        {
            try
            {
                var exincoterm = context.Incoterms.AsNoTracking().FirstOrDefault(m => m.IncotermsId == incoterm.IncotermsId);
                if (exincoterm == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<Incoterm>(incoterm).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}