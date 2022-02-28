using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class SubLedgerConfigurationController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SubLedgerConfiguration
        public ActionResult SubLedgerConfiguration()
        {
            ViewBag.GL = LoadComboBox.LoadAllExpenseAccount();
            ViewBag.SubLedger = LoadComboBox.LoadSubLedger();
            return View();
        }
        public ActionResult SaveSubLedger(List<Subledgers> addedItems)
        {
            if(addedItems.Count > 0)
            {
                foreach(var x in addedItems)
                {
                    context.Subledger.Add(x);
                  
                }
                UserLog.SaveUserLog(ref context, new UserLog(addedItems.FirstOrDefault().GLID.ToString(), DateTime.Now, "SubLedgerConf", ScreenAction.Save));
                context.SaveChanges();
      
                return Json(new { GLID = addedItems.FirstOrDefault().GLID }, JsonRequestBehavior.AllowGet);
                // UserLog.SaveUserLog(ref context, new UserLog(LTR.LTRID.ToString(), DateTime.Now, "LATR", ScreenAction.Save));
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SubLedgerValidity(int GLID, int SubLedgerID, bool isUpdate)
        {
            if(isUpdate == false)
            {
                var IsExist = context.Subledger.Where(x => x.GLID == GLID).ToList();
                var sub = IsExist.FirstOrDefault(x => x.SubLedgerID == SubLedgerID);
                if (sub != null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult GetSubLedgerById(int id)
        {
            if (id.ToString() != "" && id.ToString() != null && id != 0)
            {
                var sub = context.Subledger.Where(x => x.GLID == id).ToList().Select(x =>
                {
                    x.GLName = context.ChartOfAccount.FirstOrDefault(c => c.Id == x.GLID).Name;
                    return x;
                }).ToList();
                if(sub.Count > 0)
                {
                    var GL = sub.FirstOrDefault().GLID;
                    return Json(new { sub, GL }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("2", JsonRequestBehavior.AllowGet);
                }

            
            }
            
                return Json("0", JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult UpdateSubLedger(List<Subledgers> addedItems, int GLSearch)
        {
            if (GLSearch.ToString() != "" && GLSearch.ToString() != null && GLSearch != 0)
            {
                var sub = context.Subledger.Where(x => x.GLID == GLSearch).ToList();
                if (sub.Count > 0)
                {
                    foreach (var x in sub)
                    {
                        context.Subledger.Remove(x);

                    }
                }


                if (addedItems.Count > 0)
                {
                    foreach (var x in addedItems)
                    {
                        context.Subledger.Add(x);

                    }
                    UserLog.SaveUserLog(ref context, new UserLog(GLSearch.ToString(), DateTime.Now, "SubLedgerConf", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { GLID = addedItems.FirstOrDefault().GLID }, JsonRequestBehavior.AllowGet);
                  
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteById(int id)
        {
          
            if (id.ToString() != null && id.ToString() != "" && id != 0)
            {
                
             context.Subledger.Where(x => x.GLID == id).ToList().ForEach(x =>
             {
                 context.Subledger.Remove(x);
             });
             UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SubLedgerConf", ScreenAction.Delete));
             context.SaveChanges();
             return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}