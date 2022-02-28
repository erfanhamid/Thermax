using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class EmployeeSectionController : Controller
    {

        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: EmployeeSection
        public ActionResult EmployeeSection()   
        {
            ViewBag.EmpSection = context.EmployeeSection.ToList();
            ViewBag.Id = generate.GenerateEmployeeSectionId(context);
            return View();
        }
        [HttpPost]
        public ActionResult SaveEmployeeSection(EmployeeSection empSection)
        {

            if (ModelState.IsValid)
            {
                if (empSection.ID == 0 || empSection == null)
                {
                    return RedirectToAction("EmployeeSection");
                }
                else
                {
                    var prevZone = context.EmployeeSection.AsNoTracking().FirstOrDefault(m => m.ID == empSection.ID);
                    if (prevZone == null)
                    {
                        context.EmployeeSection.Add(empSection);
                        context.SaveChanges();
                        return RedirectToAction("EmployeeSection");
                    }
                    else
                    {
                        context.Entry<EmployeeSection>(empSection).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("EmployeeSection");
                    }
                }


            }
            else
            {
                ViewBag.EmpSection = context.EmployeeSection.ToList();
                ViewBag.Id = generate.GenerateEmployeeSectionId(context);
                return View("EmployeeSection");
            }
        }

        public ActionResult GetSectionDescription(int EmpSecID)
        {
            if (EmpSecID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var sectionDescription = context.EmployeeSection.FirstOrDefault(m => m.ID == EmpSecID);
                return Json(new { sectionDescription = sectionDescription }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteSectionDescriptionByid(int id)
        {
            var str = context.EmployeeSection.ToList().Find(x => x.ID == id);

            context.EmployeeSection.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}