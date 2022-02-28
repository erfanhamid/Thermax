using BEEERP.CommercialModule.Settings;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule.Settings;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.CommercialModule.Settings
{
    [ShowNotification]
    [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class ILCOBController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ILCOB
        public ActionResult ILCOB()
        {
            ViewBag.asOn = context.CompanySetup.FirstOrDefault(m => m.ID == 1).StartDate;
            return View();
        }

        public ActionResult GetLCByLCId(int lcId)
        {
            var lcDetails = context.ImportLC.FirstOrDefault(x => x.ILCId == lcId);

            if(lcDetails != null)
            {
                string supplierName = context.Supplier.FirstOrDefault(x => x.Id == lcDetails.SupplierId).SupplierName;
                return Json(new { LCNo = lcDetails.ILCNO, SupplierName = supplierName}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveILCOB(ILCOB lcob, List<ILCOBLineItem> addedItems)
        {
            using (context)
            {

                lcob.ILCOBNO = GetILCObNo();
                lcob.Date = lcob.Date.Add(DateTime.Now.TimeOfDay);
                addedItems.ForEach(x =>
                {
                    x.ILCOBNO = lcob.ILCOBNO;
                    context.ILCOBLineItem.Add(x);
                });

                context.ILCOB.Add(lcob);
                LcBalanceCalculationBridge.InsertFromLCOpening(ref context, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(lcob.ILCOBNO.ToString(), DateTime.Now, "ILCOB", ScreenAction.Save));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

                context.SaveChanges();
                return Json(new { lcob }, JsonRequestBehavior.AllowGet);    
            }
        }

        public ActionResult GetILCOBById(int id)
        {
            var lcobItem = context.ILCOB.FirstOrDefault(x => x.ILCOBNO == id);
            if(lcobItem != null)
            {
                var lcobLineItem = context.ILCOBLineItem.Where(x => x.ILCOBNO == id).ToList();

                return Json(new { lcobItem, lcobLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateILCOB(ILCOB lcob, List<ILCOBLineItem> addedItems)
        {
            using (context)
            {
                var lcobExist = context.ILCOB.FirstOrDefault(x => x.ILCOBNO == lcob.ILCOBNO);
                if (lcobExist != null)
                {
                    context.ILCOB.Remove(lcobExist);

                    context.ILCOBLineItem.Where(x => x.ILCOBNO == lcob.ILCOBNO).ToList().ForEach(x => {
                        context.ILCOBLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.ILCOBNO = lcob.ILCOBNO;
                        context.ILCOBLineItem.Add(x);
                    });

                    lcob.Date = lcob.Date.Add(DateTime.Now.TimeOfDay);
                    context.ILCOB.Add(lcob);
                    UserLog.SaveUserLog(ref context, new UserLog(lcob.ILCOBNO.ToString(), DateTime.Now, "ILCOB", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                    context.SaveChanges();
                    return Json(new { lcob }, JsonRequestBehavior.AllowGet);    
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteLCOBByid(int id)
        {
            var lcobExist = context.ILCOB.FirstOrDefault(x => x.ILCOBNO == id);
            if (lcobExist != null)
            {
                context.ILCOB.Remove(lcobExist);
                var lcobLineItem = context.ILCOBLineItem.Where(x => x.ILCOBNO == id).ToList();
                context.ILCOBLineItem.Where(x => x.ILCOBNO == id).ToList().ForEach(x =>
                {
                    context.ILCOBLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "ILCOB", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsiaExist, new List<DSIALineItem>());
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetILCObNo()
        {
            var ilcob = context.ILCOB.ToList().OrderBy(x => x.ILCOBNO).LastOrDefault();

            if(ilcob != null)
            {
                return ilcob.ILCOBNO + 1;
            }
            else
            {
                return 1;
            }
        }

    }
}