using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Payroll;


namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class GradeController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Grade
        public ActionResult Grade()
        {
            ViewBag.Grade = context.Grade.ToList();
            ViewBag.GradeId = generate.GenerateGradeId(context);
            return View();
        }

        [HttpPost]
        public ActionResult SaveGrade(Grade grade)
        {

            if (ModelState.IsValid)
            {
                if (grade.GradeID == 0 || grade == null)
                {
                    return RedirectToAction("Grade");
                }
                else
                {
                    var prevZone = context.Grade.AsNoTracking().FirstOrDefault(m => m.GradeID == grade.GradeID);
                    if (prevZone == null)
                    {
                        context.Grade.Add(grade);
                        context.SaveChanges();
                        return RedirectToAction("Grade");
                    }
                    else
                    {
                        context.Entry<Grade>(grade).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Grade");
                    }
                }


            }
            else
            {
                ViewBag.Grade = context.Grade.ToList();
                ViewBag.GradeID = generate.GenerateGradeId(context);
                return View("Grade");
            }
            //if (ModelState.IsValid)
            //{
            //    var deptExits = context.Department.FirstOrDefault(x => x.DeprtmntName.ToLower() == department.DeprtmntName.ToLower());

            //    if(deptExits == null)
            //    {
            //        context.Department.Add(department);
            //        context.SaveChanges();
            //        return RedirectToAction("Department");
            //    }
            //    else
            //    {
            //        ViewBag.messege = "This Department is Already Exist";
            //        ViewBag.Department = context.Department.ToList();
            //        ViewBag.DepartmentId = generate.GenerateDepartmentId(context);
            //        return View("Department", department);
            //    }

            //}

            //else
            //{
            //    ViewBag.Department = context.Department.ToList();
            //    ViewBag.DepartmentId = generate.GenerateDepartmentId(context);
            //    return View("Department", department);
            //}
            
        }
        public ActionResult GetGrade(int GradeID)
        {
            if (GradeID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var grade = context.Grade.FirstOrDefault(m => m.GradeID == GradeID);
                return Json(new { garde = grade }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteGradeByid(int id)
        {
            var str = context.Grade.ToList().Find(x => x.GradeID == id);

            context.Grade.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

  
    }
}