using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[ProcurementModule]
    public class SupplierGroupController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SupplierGroup
        public ActionResult SupplierGroup()
        {
            var savedGroups = context.SupplierGroups.Count();
            if(savedGroups==0)
            {
                ViewBag.numberofGroups = 1;
            }
            else
            {
                ViewBag.numberofGroups = context.SupplierGroups.ToList().Max(m => m.SgroupId) + 1;
            }
            ViewBag.sgrouplist = context.SupplierGroups.ToList();
            return View();
           
        }
        
        public ActionResult SaveSupplierGroup(SupplierGroup supplierGroup)
        {
           
            var sgExits = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == supplierGroup.SgroupId);
            if (sgExits == null)
            {
                try
                {
                    var savedGroup = context.SupplierGroups.Count();
                    if (savedGroup == 0)
                    {
                        supplierGroup.SgroupId = 1;
                    }
                    else
                    {
                        supplierGroup.SgroupId = context.SupplierGroups.ToList().Max(m => m.SgroupId) + 1;
                    }
                    context.SupplierGroups.Add(supplierGroup);
                    UserLog.SaveUserLog(ref context, new UserLog(supplierGroup.SgroupId.ToString(), DateTime.Now, "SupplierGroup", ScreenAction.Save));
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
        public ActionResult GetSupplierGroupByGroupNo(int sgid)
        {
            var sg = context.SupplierGroups.FirstOrDefault(m => m.SgroupId == sgid);
            return Json(new { sgroup=sg, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateSupplierGroup(SupplierGroup supplierGroup)
        {
            try
            {
                var exsg = context.SupplierGroups.AsNoTracking().FirstOrDefault(m => m.SgroupId == supplierGroup.SgroupId);
                if (exsg == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<SupplierGroup>(supplierGroup).State = EntityState.Modified;
                    UserLog.SaveUserLog(ref context, new UserLog(supplierGroup.SgroupId.ToString(), DateTime.Now, "SupplierGroup", ScreenAction.Update));
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }


    }
}