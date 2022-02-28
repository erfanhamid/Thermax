using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class ReasonOfLeavingController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: ReasonOfLeaving
        public ActionResult ReasonOfLeaving()
        {
            ViewBag.ReasonOfLev = context.ReasonOfLeaving.ToList();
            ViewBag.Id = generate.GeneratedReasonOfLevId(context);
            return View();
        }

        [HttpPost]
        public ActionResult SaveReasonOfLeaving(ReasonOfLeaving rolev)
        {
            //if (ModelState.IsValid)
            //{
            //    var isResonOfLevExist = context.ReasonOfLeaving.FirstOrDefault(x => x.RoL.ToLower() == rolev.RoL.ToLower());

            //    if(isResonOfLevExist == null)
            //    {
            //        context.ReasonOfLeaving.Add(rolev);
            //        context.SaveChanges();
            //        return RedirectToAction("ReasonOfLeaving");
            //    }

            //    else
            //    {
            //        ViewBag.messege = "This Reason Of Leaving Is Already Exist";
            //        ViewBag.ReasonOfLev = context.ReasonOfLeaving.ToList();
            //        ViewBag.Id = generate.GeneratedReasonOfLevId(context);
            //        return View("ReasonOfLeaving", rolev);
            //    }

            //}

            //else
            //{
            //    ViewBag.ReasonOfLev = context.ReasonOfLeaving.ToList();
            //    ViewBag.Id = generate.GeneratedReasonOfLevId(context);
            //    return View("ReasonOfLeaving", rolev);
            //}

            if (ModelState.IsValid)
            {
                if (rolev.RoLID == 0 || rolev == null)
                {
                    return RedirectToAction("ReasonOfLeaving");
                }
                else
                {
                    var prevZone = context.ReasonOfLeaving.AsNoTracking().FirstOrDefault(m => m.RoLID == rolev.RoLID);
                    if (prevZone == null)
                    {
                        context.ReasonOfLeaving.Add(rolev);
                        context.SaveChanges();
                        return RedirectToAction("ReasonOfLeaving");
                    }
                    else
                    {
                        context.Entry<ReasonOfLeaving>(rolev).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("ReasonOfLeaving");
                    }
                }


            }
            else
            {
                ViewBag.ReasonOfLev = context.ReasonOfLeaving.ToList();
                ViewBag.Id = generate.GeneratedReasonOfLevId(context);
                return View("ReasonOfLeaving");
            }
        }

        public ActionResult DeleteResonOfLeaveByid(int id)
        {
            var str = context.ReasonOfLeaving.ToList().Find(x => x.RoLID == id);

            context.ReasonOfLeaving.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}