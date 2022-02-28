using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class SetSalaryStructureController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SetSalaryStructure
        public ActionResult SetSalaryStructure()
        {
            //var data = context.SalaryStructure.ToList().Count;
            //if(data > 0)
            //{
                var item = context.SalaryStructure.ToList();
                item.ForEach(x =>
                {
                    x.GradeName = context.Grade.FirstOrDefault(m => m.GradeID == x.GradeID).GradeName;
                });
                ViewBag.Grade = item;
                ViewBag.GradeList = LoadComboBox.LoadGradeList();
                //ViewBag.Basic = context.SalaryStructure.FirstOrDefault().Basic;
                //ViewBag.HR = context.SalaryStructure.FirstOrDefault().HouseRent;
                //ViewBag.MA = context.SalaryStructure.FirstOrDefault().MedicalAllowance;
                //ViewBag.CA = context.SalaryStructure.FirstOrDefault().ConveyanceAllowance;
                //ViewBag.DA = context.SalaryStructure.FirstOrDefault().DearnessAllowance;
                ////ViewBag.Gross = 100;
                //ViewBag.Update = 1;
                //ViewBag.ID = context.SalaryStructure.FirstOrDefault().ID;
                //ViewBag.Scale = context.SalaryStructure.FirstOrDefault().Scale;
            //}
            //else
            //{
            //    ViewBag.Grade = context.Grade.ToList();
            //    ViewBag.GradeList = LoadComboBox.LoadGradeList();
            //    ViewBag.Basic = 0;
            //    ViewBag.HR = 0;
            //    ViewBag.MA = 0;
            //    ViewBag.CA = 0;
            //    ViewBag.DA = 0;
            //    ViewBag.Gross = 0;
            //    ViewBag.Update = 0;
            //    ViewBag.ID = 0;
            //    ViewBag.Scale = 0;
            //}
            

            return View();
        }

        public ActionResult SaveSS(SalaryStructure SS)
        {
            if (SS != null)
            {
                GenerateId generate = new GenerateId();
                SS.ID = generate.GenerateSalaryStructureID(context);
                context.SalaryStructure.Add(SS);
                context.SaveChanges();
                return Json(SS.ID, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult UpdateSS(SalaryStructure SS)
        {
            var hds = context.SalaryStructure.ToList().Find(x => x.ID == SS.ID);
            context.SalaryStructure.Remove(hds);

            context.SalaryStructure.Add(SS);
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteHDByid(int id)
        {
            var hds = context.SalaryStructure.ToList().Find(x => x.ID == id);
            context.SalaryStructure.Remove(hds);
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSetSalaryStructureById(int id)
        {
            var ss = context.SalaryStructure.FirstOrDefault(x => x.ID == id);
            
            return Json(ss,JsonRequestBehavior.AllowGet);
        }

        public ActionResult checkSalaryStructure(int id)
        {
            var hds = context.SalaryStructure.ToList().Find(x => x.GradeID == id);
            if (hds!=null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}