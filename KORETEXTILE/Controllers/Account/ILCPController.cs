using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    [Permission]
    [AccountingModule]
    public class ILCPController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ILCP
        public ActionResult ILCP()  
        {
            ViewBag.PaymentAC = LoadComboBox.LoadAllPaymentAccount();
            ViewBag.SelectAc = LoadComboBox.LoadAllDebitAccount();
            ViewBag.SubLedger = LoadComboBox.LoadAllSubLedgerAccount(null);
            return View();
        }

        public ActionResult GetLcInfo(int liId)
        {
            if(liId != 0)
            {
                var lcDetails = (from l in context.ImportLC
                                 join s in context.Supplier on l.SupplierId equals s.Id
                                 where l.ILCId == liId
                                 select new { ILCID = l.ILCId, ILCNo = l.ILCNO, AltILCNo = l.IALCNO, Supplier = s.SupplierName }).FirstOrDefault();
                if(lcDetails != null)
                {
                    return Json(lcDetails, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetSubLedgerByLedger(int accId)
        {
            var item = (from c in context.ImportCostItems join 
                       l in context.ChartOfAccount on c.CostGroupId equals l.Id
                       where c.CostGroupId == accId
                        select new { c.SlnNo, c.CostItem}).Distinct().ToList();  

            if (item.Count() > 0)
            {
                var subLed = LoadComboBox.LoadAllSubLedgerAccount(accId);
                return Json(new { subLed, Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var subLed = LoadComboBox.LoadAllSubLedgerAccount(null);
                return Json(new { subLed, Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveILCP(ILCPayment lcp, List<ILCPaymentLineItem> addedItems1, List<ILCPSubLdgrLine> addedItems)
        {
            string jvNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.ILCPayment.ToList().Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.ILCPayment.ToList().LastOrDefault(x => x.ILCPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                jvNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.ILCPNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                jvNo = yearLastTwoPart + "00001";
            }

            lcp.ILCPNo = Convert.ToInt32(jvNo);
            lcp.Date = lcp.Date.Add(DateTime.Now.TimeOfDay);

            addedItems1.ForEach(x =>
            {
                x.ILCPNo = lcp.ILCPNo;
                context.ILCPaymentLineItem.Add(x);
            });

            //for sub ledger line Item
            addedItems.ForEach(x =>
            {
                x.ILCPNo = lcp.ILCPNo;
                context.ILCPSubLdgrLine.Add(x);
            });

            context.ILCPayment.Add(lcp);
            AccountModuleBridge.InsertFromILCP(context, lcp, addedItems1);
            LcBalanceCalculationBridge.InsertFromImportLcPayment(ref context, lcp, addedItems1);
            UserLog.SaveUserLog(ref context, new UserLog(lcp.ILCPNo.ToString(), DateTime.Now, "ILCP", ScreenAction.Save));

            context.SaveChanges();
            return Json(new { lcp }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult GetILCPById(int id) 
        {
            var lcpItem = context.ILCPayment.FirstOrDefault(x => x.ILCPNo == id);

            if (lcpItem != null)
            {
                var lcpLineItem = context.ILCPaymentLineItem.Where(x => x.ILCPNo == id).ToList().Select(x =>   
                {
                    var item = context.ILCPaymentLineItem.FirstOrDefault(y => y.ILCPNo == x.ILCPNo && y.DebitAccID == x.DebitAccID);
                    var chartOfAcc = context.ChartOfAccount.FirstOrDefault(z => z.Id == x.DebitAccID);
                    x.DebitAccName = chartOfAcc.Name;
                    return x;
                }).ToList();

                var lcpSubledgerLineItem = context.ILCPSubLdgrLine.Where(x => x.ILCPNo == id).ToList().Select(x =>    
                {
                    var item = context.ILCPSubLdgrLine.FirstOrDefault(y => y.ILCPNo == x.ILCPNo && y.SubLedgerID == x.SubLedgerID);
                    var subLedger = context.ImportCostItems.FirstOrDefault(z => z.SlnNo == x.SubLedgerID);
                    x.SubLedgerName = subLedger.CostItem;
                    return x;
                }).ToList();
                return Json(new { lcpItem, lcpLineItem, lcpSubledgerLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet); 
            }
        }

        public ActionResult UpdateLCP(ILCPayment lcp, List<ILCPaymentLineItem> addedItems1, List<ILCPSubLdgrLine> addedItems)
        {
            var lcpExist = context.ILCPayment.FirstOrDefault(x => x.ILCPNo == lcp.ILCPNo);
            if (lcpExist != null)
            {
                context.ILCPayment.Remove(lcpExist);

                context.ILCPaymentLineItem.Where(x => x.ILCPNo == lcp.ILCPNo).ToList().ForEach(x => {
                    context.ILCPaymentLineItem.Remove(x);
                });

                //sub ledger remove
                context.ILCPSubLdgrLine.Where(x => x.ILCPNo == lcp.ILCPNo).ToList().ForEach(x => {
                    context.ILCPSubLdgrLine.Remove(x);
                });

                lcp.Date = lcp.Date.Add(DateTime.Now.TimeOfDay);

                //sub ledger add
                addedItems.ForEach(x =>
                {
                    x.ILCPNo = lcp.ILCPNo;
                    context.ILCPSubLdgrLine.Add(x);
                });

                addedItems1.ForEach(x =>
                {
                    x.ILCPNo = lcp.ILCPNo;
                    context.ILCPaymentLineItem.Add(x);
                });

                context.ILCPayment.Add(lcp);

                AccountModuleBridge.InsertFromILCP(context, lcp, addedItems1);
                UserLog.SaveUserLog(ref context, new UserLog(lcp.ILCPNo.ToString(), DateTime.Now, "ILCP", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { lcp }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteLCPByid(int id)   
        {
            var lcpExist = context.ILCPayment.FirstOrDefault(x => x.ILCPNo == id);
            if (lcpExist != null)
            {
                context.ILCPayment.Remove(lcpExist);
                context.ILCPaymentLineItem.Where(x => x.ILCPNo == id).ToList().ForEach(x =>
                {
                    context.ILCPaymentLineItem.Remove(x);
                });

                context.ILCPSubLdgrLine.Where(x => x.ILCPNo == id).ToList().ForEach(x =>
                {
                    context.ILCPSubLdgrLine.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "ILCP", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromILCP(context, lcpExist);
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