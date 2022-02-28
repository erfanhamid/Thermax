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
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover,")]
    public class DepartmentController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Department
        public ActionResult Department()
        {
            ViewBag.Department = context.Department.ToList();
            ViewBag.DepartmentId = generate.GenerateDepartmentId(context);
            return View();
        }

        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {

            if (ModelState.IsValid)
            {
                if (department.DeptmentID == 0 || department == null)
                {
                    return RedirectToAction("Department");
                }
                else
                {
                    var prevZone = context.Department.AsNoTracking().FirstOrDefault(m => m.DeptmentID == department.DeptmentID);
                    if (prevZone == null)
                    {
                        context.Department.Add(department);
                        context.SaveChanges();
                        return RedirectToAction("Department");
                    }
                    else
                    {
                        context.Entry<Department>(department).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Department");
                    }
                }


            }
            else
            {
                ViewBag.Department = context.Department.ToList();
                ViewBag.DepartmentId = generate.GenerateDepartmentId(context);
                return View("Department");
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
        public ActionResult GetDepartment(int DepartmentID)
        {
            if (DepartmentID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var department = context.Department.FirstOrDefault(m => m.DeptmentID == DepartmentID);
                return Json(new { department = department }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteDepartmentByid(int id)
        {
            var str = context.Department.ToList().Find(x => x.DeptmentID == id);

            context.Department.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}