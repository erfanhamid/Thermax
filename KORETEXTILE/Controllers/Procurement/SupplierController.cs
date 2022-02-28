using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.StoreRM.GRN;
using System.Data.Entity;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[ProcurementModule]
    public class SupplierController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Supplier
        public ActionResult Supplier()
        {
            ViewBag.dateAsOf = context.CompanySetup.FirstOrDefault().StartDate;
            ViewBag.supplierGroup = LoadComboBox.LoadAllSupplierGroup();
            var supplierlist = context.Supplier.Count();
            if (supplierlist == 0)
            {
                ViewBag.supplierNo = 1;
            }
            else
            {
                ViewBag.supplierNo= context.Supplier.ToList().Max(x => x.Id) + 1;
            }
            return View();
        }
        public ActionResult SaveSuppliers(Supplier supplier)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Create Supplier").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (supplier.AsDate > IsExpired.SELDate)
            {




                var suppNameCheck = context.Supplier.FirstOrDefault(x => x.SupplierName == supplier.SupplierName);


                if (suppNameCheck == null)
                {
                    var supplierlist = context.Supplier.Count();
                    if (supplierlist == 0)
                    {
                        supplier.Id = 1;
                    }
                    else
                    {
                        supplier.Id = context.Supplier.ToList().Max(x => x.Id) + 1;
                    }

                    supplier.AsDate = supplier.AsDate.Add(DateTime.Now.TimeOfDay);
                    try
                    {
                        context.Supplier.Add(supplier);
                        UserLog.SaveUserLog(ref context, new UserLog(supplier.Id.ToString(), DateTime.Now, "Supplier", ScreenAction.Save));
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
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSupplierById(Supplier splr)

        {
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == splr.Id);
            if (supplier == null)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { Message = 1, supplier =supplier, JsonRequestBehavior.AllowGet });
            }
            
        }
        public ActionResult UpdateSuppliers(Supplier supplier)
        {
            try
            {
                var exsg = context.Supplier.AsNoTracking().FirstOrDefault(m => m.Id == supplier.Id);              
                if (exsg == null)
                {
                    if (supplier.AsDate.ToShortDateString() != exsg.AsDate.ToShortDateString())
                    {
                        supplier.AsDate = supplier.AsDate.Add(DateTime.Now.TimeOfDay);
                    }
                    else
                    {
                        supplier.AsDate = exsg.AsDate;
                    }
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<Supplier>(supplier).State = EntityState.Modified;
                    UserLog.SaveUserLog(ref context, new UserLog(supplier.Id.ToString(), DateTime.Now, "Supplier", ScreenAction.Update));
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