using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommercialModule;
using System.Data.Entity;

namespace BEEERP.Controllers
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class MoPController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MoP
        public ActionResult MoP()
        {
            var savedMops = context.MOPs.Count();
            if (savedMops == 0)
            {
                ViewBag.numberofmops = 1;
            }
            else
            {
                ViewBag.numberofmops = context.MOPs.ToList().Max(m => m.MoPId) + 1;
            }
            ViewBag.moplist = context.MOPs.ToList();
            return View();
        }
        public ActionResult SaveMop(MOP mop)
        {

            var mopExits = context.MOPs.FirstOrDefault(x => x.MoPId == mop.MoPId);
            if (mopExits == null)
            {
                try
                {
                    var savedMop = context.MOPs.Count();
                    if (savedMop == 0)
                    {
                        mop.MoPId = 1;
                    }
                    else
                    {
                        mop.MoPId = context.MOPs.ToList().Max(m => m.MoPId) + 1;
                    }
                    context.MOPs.Add(mop);
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
        public ActionResult GetMopByMopID(int mopid)
        {
            var mop = context.MOPs.FirstOrDefault(m => m.MoPId == mopid);
            return Json(new { mop = mop, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateMoP(MOP mop)
        {
            try
            {
                var exmop = context.MOPs.AsNoTracking().FirstOrDefault(m => m.MoPId == mop.MoPId);
                if (exmop == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<MOP>(mop).State = EntityState.Modified;
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