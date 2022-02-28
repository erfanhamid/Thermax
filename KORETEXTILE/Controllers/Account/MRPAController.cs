using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Procurement;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Account
{
    [Permission]
    [StoreFGModule]
    [ShowNotification]
    public class MRPAController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MRPA
        public ActionResult MRPA()  
        {
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            return View();
        }

        public ActionResult GetEmpDetails(int empId)
        {
            var empDetails = (from e in context.Employees join
                              d in context.Designation on e.Designation equals d.Id
                              where e.Id == empId
                              select new { EmpName = e.Name, EmpDesignation = d.Name}).FirstOrDefault();
            if(empDetails != null)
            {
                decimal PreviousBal = (from e in context.Employees join 
                                       m in context.MoneyRequisitionBalanceCalculations on e.Id equals m.EmpID
                                       where m.EmpID == empId
                                       select m.Amount).ToList().DefaultIfEmpty(0).Sum();

                return Json(new { empDetails , PreviousBal }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMRPA(MRPA mRPA)
        {
            string mRPANo = "";
            string yearLastTwoPart = mRPA.MRPADate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.MRPA.AsEnumerable().Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.MRPA.ToList().LastOrDefault(x => x.MRPANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                //mRPANo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.MRPANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                if (lastInvoice == null)
                {
                    mRPANo = yearLastTwoPart + "00001";
                }
                else
                {
                    mRPANo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.MRPANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                mRPANo = yearLastTwoPart + "00001";
            }

            mRPA.MRPANo = Convert.ToInt32(mRPANo);
            mRPA.MRPADate = mRPA.MRPADate.Add(DateTime.Now.TimeOfDay);

            MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            mrbc.EmpID = mRPA.EmployeeID;
            mrbc.Date = mRPA.MRPADate;
            mrbc.DocumentType = "MRPA";
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            mrbc.AccountId = accId;
            mrbc.Description = mRPA.Descriptions;
            mrbc.RefNo = mRPA.RefNo;
            mrbc.Amount = - mRPA.AdjustmentAmnt;
            mrbc.DocID = mRPA.MRPANo;

            SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
            sb.DocumentType = "MRPA";
            sb.DocNo = mRPA.MRPANo;
            sb.SupplierID = mRPA.SupplierID;
            sb.TDate = mRPA.MRPADate;
            int accountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance").RefValue;
            sb.AccountID = accountId;
            sb.TrDesription = mRPA.Descriptions;
            sb.RefNo = mRPA.RefNo;
            sb.Amount = -mRPA.AdjustmentAmnt;

            context.MRPA.Add(mRPA);
            context.MoneyRequisitionBalanceCalculations.Add(mrbc);
            context.SupplierBalanceCalculation.Add(sb);

            UserLog.SaveUserLog(ref context, new UserLog(mRPA.MRPANo.ToString(), DateTime.Now, "MRPA", ScreenAction.Save));
            AccountModuleBridge.InsertUpdateFromMoneyRequisitionPurchaseAdjustment(ref context, mRPA);
            //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

            context.SaveChanges();
            return Json(new { mRPA }, JsonRequestBehavior.AllowGet);    
        }


        public ActionResult GetMRPAById(int id)
        {
            var mrpaItem = context.MRPA.FirstOrDefault(x => x.MRPANo == id);    

            if (mrpaItem != null)
            {
                decimal PreviousBal = (from e in context.Employees
                                       join
                                        m in context.MoneyRequisitionBalanceCalculations on e.Id equals m.EmpID
                                       where m.EmpID == mrpaItem.EmployeeID
                                       select m.Amount).ToList().DefaultIfEmpty(0).Sum();
                mrpaItem.PreviousBal = PreviousBal;
                return Json(new { mrpaItem}, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateMRPA(MRPA mRPA)
        {
            var mrpaExist = context.MRPA.FirstOrDefault(x => x.MRPANo == mRPA.MRPANo);
            if (mrpaExist != null)
            {
                context.MRPA.Remove(mrpaExist);
                var mrbcExist = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == mRPA.MRPANo && x.DocumentType == "MRPA");
                context.MoneyRequisitionBalanceCalculations.Remove(mrbcExist);
                var sbExist = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == mRPA.MRPANo && x.DocumentType == "MRPA");
                context.SupplierBalanceCalculation.Remove(sbExist);

                mRPA.MRPADate = mRPA.MRPADate.Add(DateTime.Now.TimeOfDay);

                MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
                mrbc.EmpID = mRPA.EmployeeID;
                mrbc.Date = mRPA.MRPADate;
                mrbc.DocumentType = "MRPA";
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
                mrbc.AccountId = accId;
                mrbc.Description = mRPA.Descriptions;
                mrbc.RefNo = mRPA.RefNo;
                mrbc.Amount = -mRPA.AdjustmentAmnt;
                mrbc.DocID = mRPA.MRPANo;

                SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
                sb.DocumentType = "MRPA";
                sb.DocNo = mRPA.MRPANo;
                sb.SupplierID = mRPA.SupplierID;
                sb.TDate = mRPA.MRPADate;
                int accountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance").RefValue;
                sb.AccountID = accountId;
                sb.TrDesription = mRPA.Descriptions;
                sb.RefNo = mRPA.RefNo;
                sb.Amount = -mRPA.AdjustmentAmnt;

                context.MRPA.Add(mRPA);
                context.MoneyRequisitionBalanceCalculations.Add(mrbc);
                context.SupplierBalanceCalculation.Add(sb);

                UserLog.SaveUserLog(ref context, new UserLog(mRPA.MRPANo.ToString(), DateTime.Now, "MRPA", ScreenAction.Update));
                AccountModuleBridge.InsertUpdateFromMoneyRequisitionPurchaseAdjustment(ref context, mRPA);
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                context.SaveChanges();
                return Json(new { mRPA }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteMRPAByid(int id)  
        {
            var mrpaExist = context.MRPA.FirstOrDefault(x => x.MRPANo == id);
            if (mrpaExist != null)
            {
                context.MRPA.Remove(mrpaExist);
                var mrbcExist = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == id && x.DocumentType == "MRPA");
                context.MoneyRequisitionBalanceCalculations.Remove(mrbcExist);
                var sbExist = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == id && x.DocumentType == "MRPA");
                context.SupplierBalanceCalculation.Remove(sbExist);

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "MRPA", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromMoneyRequisitionPurchaseAdjustment(ref context,id);
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsiaExist, new List<DSIALineItem>());
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintReviewMRPA(int mRPANo)
        {
            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "MoneyRequisitionRefund" && p.TrnasId == mRPANo.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spPrintMRPA " + mRPANo + "");
            var items = context.Database.SqlQuery<MRPAreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                MRPAreviewReport report = new MRPAreviewReport();
                items.Add(report);
            }
            MRPAreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public MRPAreviewR ShowReport(ReportParmPersister persister, List<MRPAreviewReport> data)
        {
            MRPAreviewR mRPAreviewR = new MRPAreviewR();

            mRPAreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\MRPAreviewR.rpt");
            mRPAreviewR.Load(APPPATH);
            mRPAreviewR.SetDataSource(data);
            return mRPAreviewR;
        }
    }
}