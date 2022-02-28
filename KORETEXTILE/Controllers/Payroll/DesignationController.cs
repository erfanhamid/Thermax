using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.HRModule;

namespace BEEERP.Controllers
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover,")]
    public class DesignationController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Designation
        public ActionResult Designation()   
        {
            ViewBag.Designation = context.Designation.ToList();
            ViewBag.Id = generate.GenerateDesignationId(context);
            return View();
        }
        [HttpPost]
        public ActionResult SaveDesignation(Designations designation)
        {
            //if (ModelState.IsValid)
            //{
            //    var isDesignationExits = context.Designation.FirstOrDefault(x => x.Name.ToLower() == designation.Name.ToLower());

            //    if(isDesignationExits == null)
            //    {
            //        context.Designation.Add(designation);
            //        context.SaveChanges();
            //        return RedirectToAction("Designation");
            //    }
            //    else
            //    {
            //        ViewBag.messege = "This Designation is Already Exits";
            //        ViewBag.Designation = context.Designation.ToList();
            //        ViewBag.Id = generate.GenerateDesignationId(context);
            //        return View("Designation", designation);

            //    }
                
            //}

            //else
            //{
            //    ViewBag.Designation = context.Designation.ToList();
            //    ViewBag.Id = generate.GenerateDesignationId(context);
            //    return View("Designation", designation);
            //}

            if (ModelState.IsValid)
            {
                if (designation.Id == 0 || designation == null)
                {
                    return RedirectToAction("Designation");
                }
                else
                {
                    var prevZone = context.Designation.AsNoTracking().FirstOrDefault(m => m.Id == designation.Id);
                    if (prevZone == null)
                    {
                        context.Designation.Add(designation);
                        context.SaveChanges();
                        return RedirectToAction("Designation");
                    }
                    else
                    {
                        context.Entry<Designations>(designation).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Designation");
                    }
                }


            }
            else
            {
                ViewBag.Designation = context.Designation.ToList();
                ViewBag.Id = generate.GenerateDesignationId(context);
                //    return View("Designation", designation);
                return View("Designation");
            }
        }

        public ActionResult GetDesignationbyID(int designationID)
        {
            if (designationID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var designation = context.Designation.FirstOrDefault(m => m.Id == designationID);
                return Json(new { designation = designation }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteDesignationID(int id)
        {
            var str = context.Designation.ToList().Find(x => x.Id == id);

            context.Designation.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}