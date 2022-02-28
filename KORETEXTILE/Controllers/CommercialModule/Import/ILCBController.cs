using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.CommercialModule.Import
{
    [Permission]
    [AccountingModule]
    [ShowNotification]
    public class ILCBController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ILCB
        public ActionResult ILCB()  
        {
            //string ilcbNo = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //var noOfInvoice = context.ILCB.Count();
            //if (noOfInvoice > 0)
            //{
            //    var lastInvoice = context.ILCB.ToList().LastOrDefault(x => x.ILCBNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    if (lastInvoice == null)
            //    {
            //        ilcbNo = yearLastTwoPart + "00001";
            //    }
            //    else
            //    {
            //        ilcbNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.ILCBNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //    }

            //}
            //else
            //{
            //    ilcbNo = yearLastTwoPart + "00001";
            //}
            //ViewBag.ILCBNo = ilcbNo;

            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);

            ViewBag.ILC = LoadComboBox.LoadAllILC();

            //ViewBag.Supplier = LoadComboBox.LoadSupplierForLCBill();
            ViewBag.Account = LoadComboBox.LoadAllDebitAccount();
            ViewBag.SubLedger = LoadComboBox.LoadAllSubLedgerAccount(null);

            return View();
        }

        public ActionResult GetSupplierByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadSupplier(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveILCB(ILCB ilcb, List<ILCBLine> addedItems1, List<ILCBSubLdgrLine> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "LC Supplier Payable").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (ilcb.Date > IsExpired.SELDate)
            {
                if (ilcb.ILCBNo == 0)
                {
                    string ilcbNo = "";
                    string yearLastTwoPart = ilcb.Date.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.ILCB.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.ILCB.ToList().LastOrDefault(x => x.ILCBNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastInvoice == null)
                        {
                            ilcbNo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            ilcbNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.ILCBNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }

                    }
                    else
                    {
                        ilcbNo = yearLastTwoPart + "00001";
                    }

                    ilcb.ILCBNo = Convert.ToInt32(ilcbNo);
                    //ilcb.Type = "Create Payable";
                    

                    //else
                    //{
                    //    var ilcbExist = context.ILCB.FirstOrDefault(x => x.ILCBNo == ilcb.ILCBNo);
                    //    context.ILCB.Remove(ilcbExist);

                    //    var ilcbLineExist = context.ILCBLine.Where(x => x.ILCBNo == ilcb.ILCBNo).ToList();
                    //    ilcbLineExist.ForEach(x =>
                    //    {
                    //        context.ILCBLine.Remove(x);
                    //    });

                    //    var ilcbSubLineExist = context.ILCBSubLdgrLine.Where(x => x.ILCBNo == ilcb.ILCBNo).ToList();
                    //    ilcbSubLineExist.ForEach(x =>
                    //    {
                    //    //x.DocNo = x.ILCBNo;
                    //    //x.DocType = "LCSP";
                    //    context.ILCBSubLdgrLine.Remove(x);
                    //    });
                    //    UserLog.SaveUserLog(ref context, new UserLog(ilcb.ILCBNo.ToString(), ilcb.Date, "LCSP", ScreenAction.Update));
                    //}

                    ilcb.Date = ilcb.Date.Add(DateTime.Now.TimeOfDay);

                    addedItems1.ForEach(x =>
                    {
                        x.ILCBNo = ilcb.ILCBNo;
                        context.ILCBLine.Add(x);
                    });

                    //for sub ledger line Item
                    addedItems.ForEach(x =>
                    {
                        x.ILCBNo = ilcb.ILCBNo;
                        //x.DocNo = x.ILCBNo;
                        //x.DocType = "LCSP";
                        context.ILCBSubLdgrLine.Add(x);
                    });

                    context.ILCB.Add(ilcb);
                    AccountModuleBridge.InsertUpdateFromILCB(context, ilcb, addedItems1);
                    SupplierBalanceCalculationBridge.InsertUpdateFromILCB(context, ilcb, addedItems1);
                    LcBalanceCalculationBridge.InsertUpdateFromImportLCBill(ref context, ilcb, addedItems1);
                    UserLog.SaveUserLog(ref context, new UserLog(ilcb.ILCBNo.ToString(), ilcb.Date, "ILCB", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { ilcb }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateILCB(ILCB ilcb, List<ILCBLine> addedItems1, List<ILCBSubLdgrLine> addedItems)
        {
            if(ilcb != null)
            {
                var ilcbExist = context.ILCB.FirstOrDefault(x => x.ILCBNo == ilcb.ILCBNo);
                context.ILCB.Remove(ilcbExist);

                var ilcbLineExist = context.ILCBLine.Where(x => x.ILCBNo == ilcb.ILCBNo).ToList();
                ilcbLineExist.ForEach(x =>
                {
                    context.ILCBLine.Remove(x);
                });

                var ilcbSubLineExist = context.ILCBSubLdgrLine.Where(x => x.ILCBNo == ilcb.ILCBNo).ToList();
                ilcbSubLineExist.ForEach(x =>
                {
                    //x.DocNo = x.ILCBNo;
                    //x.DocType = "LCSP";
                    context.ILCBSubLdgrLine.Remove(x);
                });
                ilcb.Date = ilcb.Date.Add(DateTime.Now.TimeOfDay);

                addedItems1.ForEach(x =>
                {
                    x.ILCBNo = ilcb.ILCBNo;
                    context.ILCBLine.Add(x);
                });

                //for sub ledger line Item
                addedItems.ForEach(x =>
                {
                    x.ILCBNo = ilcb.ILCBNo;
                    //x.DocNo = x.ILCBNo;
                    //x.DocType = "LCSP";
                    context.ILCBSubLdgrLine.Add(x);
                });

                context.ILCB.Add(ilcb);
                AccountModuleBridge.InsertUpdateFromILCB(context, ilcb, addedItems1);
                SupplierBalanceCalculationBridge.InsertUpdateFromILCB(context, ilcb, addedItems1);
                LcBalanceCalculationBridge.InsertUpdateFromImportLCBill(ref context, ilcb, addedItems1);
                UserLog.SaveUserLog(ref context, new UserLog(ilcb.ILCBNo.ToString(), ilcb.Date, "ILCB", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { ilcb }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult GetLcInfo(int liId)
        {
            if (liId != 0)
            {
                var lcDetails = (from l in context.ImportLC
                                 join s in context.Supplier on l.SupplierId equals s.Id
                                 where l.ILCId == liId
                                 select new { ILCID = l.ILCId, ILCNo = l.ILCNO, AltILCNo = l.IALCNO, Supplier = s.SupplierName }).FirstOrDefault();
                var PreviousBill = (from il in context.ILCBLine
                                    join i in context.ILCB on il.ILCBNo equals i.ILCBNo
                                    join c in context.ChartOfAccount on il.DebitAccID equals c.Id into CGroup
                                    from c in CGroup.DefaultIfEmpty()
                                    where i.ILCID == liId
                                    orderby i.ILCBNo descending
                                    select new {DebitAccID = c.Name, Amount = il.Amount, Description = il.Description, ILCBNo = i.ILCBNo}
                                    ).ToList();
                if (lcDetails != null)
                {
                    if(PreviousBill.Count > 0)
                    {
                        return Json(new { lcDetails = lcDetails, PreviousBill = PreviousBill }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { lcDetails = lcDetails, PreviousBill = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    
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

        public ActionResult GetILCBById(int id)
        {
            var ilcbItem = context.ILCB.FirstOrDefault(x => x.ILCBNo == id);
            
            if (ilcbItem != null)
            {
                var ilcbLineItem = context.ILCBLine.Where(x => x.ILCBNo == id).ToList().Select(x =>
                {
                    var item = context.ILCBLine.FirstOrDefault(y => y.ILCBNo == x.ILCBNo && y.DebitAccID == x.DebitAccID);
                    var chartOfAcc = context.ChartOfAccount.FirstOrDefault(z => z.Id == x.DebitAccID);
                    x.DebitAccName = chartOfAcc.Name;
                    return x;
                }).ToList();

                var ilcbSubledgerLineItem = context.ILCBSubLdgrLine.Where(x => x.ILCBNo == id).ToList().Select(x =>
                {
                    var item = context.ILCBSubLdgrLine.FirstOrDefault(y => y.ILCBNo == x.ILCBNo && y.SubLedgerID == x.SubLedgerID);
                    var subLedger = context.ImportCostItems.FirstOrDefault(z => z.SlnNo == x.SubLedgerID);
                    x.SubLedgerName = subLedger.CostItem;
                    return x;
                }).ToList();

                var suppGroupId = context.Supplier.FirstOrDefault(x => x.Id == ilcbItem.SupplierID).GroupID;
                return Json(new { ilcbItem, ilcbLineItem, ilcbSubledgerLineItem, suppGroupId }, JsonRequestBehavior.AllowGet);   
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteILCBByid(int id)
        {
            var ilcbExist = context.ILCB.FirstOrDefault(x => x.ILCBNo == id);
            if (ilcbExist != null)
            {
                context.ILCB.Remove(ilcbExist);
                context.ILCBLine.Where(x => x.ILCBNo == id).ToList().ForEach(x =>
                {
                    context.ILCBLine.Remove(x);
                });

                context.ILCBSubLdgrLine.Where(x => x.ILCBNo == id).ToList().ForEach(x =>
                {
                    context.ILCBSubLdgrLine.Remove(x);
                });
                
                AccountModuleBridge.DeleteFromILCB(context, ilcbExist);
                SupplierBalanceCalculationBridge.DeleteFromILCB(context, ilcbExist);
                LcBalanceCalculationBridge.DeleteFromILCBill(ref context, ilcbExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), ilcbExist.Date, "ILCB", ScreenAction.Delete));
                context.SaveChanges();
                
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult TestCss()
        {
            return View();
        }

        public FileResult GetILCBPreview(int ILCBNo)
        {
            Session["ReportName"] = "ILCBPreview";

            ReportParmPersister param = new ReportParmPersister();
            var loginName = context.UserLog.FirstOrDefault(x => x.TrnasId == ILCBNo.ToString() && x.ScreenName == "ILCB" && x.Action == "Save").LogInName;
            param.PostedBy = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName).Name;
            Session["ILCBPreview"] = param;

            string sql = string.Format("exec PreviewILCB '" + ILCBNo + "'");
            var items = context.Database.SqlQuery<ILCBPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ILCBPreviewReport report = new ILCBPreviewReport();
                items.Add(report);
            }

            ILCBPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public ILCBPreviewR ShowReport(ReportParmPersister persister, List<ILCBPreviewReport> data)
        {
            ILCBPreviewR ILCBPreviewR = new ILCBPreviewR();

            ILCBPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ILCBPreviewR.rpt");
            ILCBPreviewR.Load(APPPATH);
            ILCBPreviewR.SetDataSource(data);
            ILCBPreviewR.SetParameterValue("PostedBy", persister.PostedBy);
            ILCBPreviewR.SetParameterValue("CompName", persister.CompName);
            ILCBPreviewR.SetParameterValue("CompAddress", persister.CompAddress);
            return ILCBPreviewR;
        }
    }
}