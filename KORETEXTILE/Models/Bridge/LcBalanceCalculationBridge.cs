using BEEERP.CommercialModule.Settings;
using BEEERP.Models.AccountModule;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.CommercialModule.Settings;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class LcBalanceCalculationBridge
    {
        public static bool DeleteFromLCPC(ref BEEContext context, tblLCCosting lcfc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lcfc.LCPCNo && x.DocType == "LCPC" && x.ILCID == lcfc.ILCID);
            if (importLcBal != null)
            {
                context.ILCBalCalculation.Remove(importLcBal);
                return true;
            }
            return false;
        }
        public static bool InsertUpdateFromLCPC(ref BEEContext context, tblLCCosting lcfc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lcfc.LCPCNo && x.DocType == "LCPC" && x.ILCID == lcfc.ILCID);
            if (importLcBal == null)
            {
                ILCBalCalculations iclBalCal = new ILCBalCalculations();
                iclBalCal.ILCID = lcfc.ILCID;
                iclBalCal.Date = lcfc.LCPCDate;
                iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "All Supplier Balance").RefValue;
                iclBalCal.DocType = "LCPC";
                iclBalCal.Amount = lcfc.ILCCostingTotal;
                iclBalCal.Refno = "";
                iclBalCal.Descriptions = "";
                iclBalCal.DocNo = Convert.ToInt32(lcfc.LCPCNo);
                context.ILCBalCalculation.Add(iclBalCal);
            }
            else
            {
                importLcBal.ILCID = lcfc.ILCID;
                importLcBal.Date = lcfc.LCPCDate;
                importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "All Supplier Balance").RefValue;
                importLcBal.DocType = "LCPC";
                importLcBal.Amount = lcfc.ILCCostingTotal;
                importLcBal.Refno = "";
                importLcBal.Descriptions = "";
                importLcBal.DocNo = Convert.ToInt32(lcfc.LCPCNo);
                context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool DeleteFromLCFC(ref BEEContext context, tblLCCosting lcfc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lcfc.LCPCNo && x.DocType == "LCFC" && x.ILCID == lcfc.ILCID);
            if (importLcBal != null)
            {
                context.ILCBalCalculation.Remove(importLcBal);
                return true;
            }
            return false;
        }
        public static bool InsertUpdateFromLCFC(ref BEEContext context, tblLCCosting lcfc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lcfc.LCPCNo && x.DocType == "LCFC" && x.ILCID == lcfc.ILCID);
            if (importLcBal == null)
            {
                ILCBalCalculations iclBalCal = new ILCBalCalculations();
                iclBalCal.ILCID = lcfc.ILCID;
                iclBalCal.Date = lcfc.LCPCDate;
                iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "All Supplier Balance").RefValue;
                iclBalCal.DocType = "LCFC";
                iclBalCal.Amount = lcfc.ILCCostingTotal*-1;
                iclBalCal.Refno = "";
                iclBalCal.Descriptions = "";
                iclBalCal.DocNo = Convert.ToInt32(lcfc.LCPCNo);
                context.ILCBalCalculation.Add(iclBalCal);
            }
            else
            {
                importLcBal.ILCID = lcfc.ILCID;
                importLcBal.Date = lcfc.LCPCDate;
                importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "All Supplier Balance").RefValue;
                importLcBal.DocType = "LCFC";
                importLcBal.Amount = lcfc.ILCCostingTotal*-1;
                importLcBal.Refno = "";
                importLcBal.Descriptions = "";
                importLcBal.DocNo = Convert.ToInt32(lcfc.LCPCNo);
                context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool InsertUpdateFromLCImport(ref BEEContext context, ImportLC inportlc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo.ToString() == inportlc.ILCNO && x.DocType == "ILC" && x.ILCID == inportlc.ILCId);
            if (importLcBal == null)
            {
                ILCBalCalculations iclBalCal = new ILCBalCalculations();
                iclBalCal.ILCID = inportlc.ILCId;
                iclBalCal.Date = inportlc.ILCDate;
                iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "Import LC Account").RefValue;
                iclBalCal.DocType = "ILC";
                iclBalCal.Amount = inportlc.GrandTotal;
                iclBalCal.Refno = "";
                iclBalCal.Descriptions = "";
                iclBalCal.DocNo = Convert.ToInt32(inportlc.ILCNO);
                context.ILCBalCalculation.Add(iclBalCal);
            }
            else
            {
                importLcBal.ILCID = inportlc.ILCId;
                importLcBal.Date = inportlc.ILCDate;
                importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "Import LC Account").RefValue;
                importLcBal.DocType = "ILC";
                importLcBal.Amount = inportlc.GrandTotal;
                importLcBal.Refno = "";
                importLcBal.Descriptions = "";
                importLcBal.DocNo = Convert.ToInt32(inportlc.ILCNO);
                context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool DeleteFromLCImport(ref BEEContext context, ImportLC inportlc)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo.ToString() == inportlc.ILCNO && x.DocType == "ILC" && x.ILCID == inportlc.ILCId);
            if (importLcBal != null)
            {
                context.ILCBalCalculation.Remove(importLcBal);
                return true;
            }
            return false;
        }

        public static bool InsertFromLTR(ref BEEContext context, LTR lTR)
        {

            //var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "ILC" && x.ILCID == lTR.LCID);
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "LATR" && x.ILCID == lTR.LCID);
            if (importLcBal == null)
            {
                ILCBalCalculations iclBalCal = new ILCBalCalculations();
                iclBalCal.ILCID = lTR.LCID;
                iclBalCal.Date = lTR.Date;
                //iclBalCal.PaymentAccountID = lTR.ACCID;
                iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(x=> x.Name == "All LATR Account Balance GL").RefValue;
                iclBalCal.DocType = "LATR";
                iclBalCal.Amount = lTR.Amount;
                iclBalCal.Refno = lTR.RefNo;
                iclBalCal.Descriptions = lTR.Description;
                iclBalCal.DocNo = Convert.ToInt32(lTR.LTRID);
                context.ILCBalCalculation.Add(iclBalCal);
            }
            else
            {
                importLcBal.ILCID = lTR.LCID;
                importLcBal.Date = lTR.Date;
                importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
                importLcBal.DocType = "LATR";
                importLcBal.Amount = lTR.Amount;
                importLcBal.Refno = lTR.RefNo;
                importLcBal.Descriptions = lTR.Description;
                importLcBal.DocNo = Convert.ToInt32(lTR.LTRID);
                context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool InsertFromLTROB(ref BEEContext context, LTR lTR)
        {

            //var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "ILC" && x.ILCID == lTR.LCID);
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "LATROB" && x.ILCID == lTR.LCID);
            if (importLcBal == null)
            {
                ILCBalCalculations iclBalCal = new ILCBalCalculations();
                iclBalCal.ILCID = lTR.LCID;
                iclBalCal.Date = lTR.Date;
                iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
                iclBalCal.DocType = "LATROB";
                iclBalCal.Amount = lTR.Amount;
                iclBalCal.Refno = lTR.RefNo;
                iclBalCal.Descriptions = lTR.Description;
                iclBalCal.DocNo = Convert.ToInt32(lTR.LTRID);
                context.ILCBalCalculation.Add(iclBalCal);
            }
            else
            {
                importLcBal.ILCID = lTR.LCID;
                importLcBal.Date = lTR.Date;
                importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
                importLcBal.DocType = "LATROB";
                importLcBal.Amount = lTR.Amount;
                importLcBal.Refno = lTR.RefNo;
                importLcBal.Descriptions = lTR.Description;
                importLcBal.DocNo = Convert.ToInt32(lTR.LTRID);
                context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
            }
            return true;
        }

        public static bool DeleteFromLTR(ref BEEContext context, LTR lTR)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "LATR" && x.ILCID == lTR.LCID);
            if (importLcBal != null)
            {
                context.ILCBalCalculation.Remove(importLcBal);
                return true;
            }
            return false;
        }
        public static bool DeleteFromLTROB(ref BEEContext context, LTR lTR)
        {
            var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lTR.LTRID && x.DocType == "LATROB" && x.ILCID == lTR.LCID);
            if (importLcBal != null)
            {
                context.ILCBalCalculation.Remove(importLcBal);
                return true;
            }
            return false;
        }

        public static bool InsertFromLCOpening(ref BEEContext context, List<ILCOBLineItem> addedItems)
        {
            var compSetUp = context.CompanySetup.FirstOrDefault();
            foreach (var item in addedItems)
            {
                 int a = Convert.ToInt32(item.ILCID);
                var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == item.ILCOBNO && x.DocType == "ILCOB" && x.ILCID ==a);
                if (importLcBal == null)
                {
                    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                    iclBalCal.ILCID = Convert.ToInt32(item.ILCID);
                    iclBalCal.Date = compSetUp.StartDate;
                    iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "Import LC Account").RefValue;
                    iclBalCal.DocType = "ILCOB";
                    iclBalCal.Amount = item.ILCOBalance;
                    iclBalCal.Refno = "";
                    iclBalCal.Descriptions = "";
                    iclBalCal.DocNo = Convert.ToInt32(item.ILCOBNO);
                    context.ILCBalCalculation.Add(iclBalCal);
                }
                else
                {
                    importLcBal.ILCID = Convert.ToInt32(item.ILCID);
                    importLcBal.Date =compSetUp.StartDate ;
                    importLcBal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(m => m.Name == "Import LC Account").RefValue;
                    importLcBal.DocType = "ILCOB";
                    importLcBal.Amount = item.ILCOBalance;
                    importLcBal.Refno = "";
                    importLcBal.Descriptions = "";
                    importLcBal.DocNo = Convert.ToInt32(item.ILCOBNO);
                    context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
                }
            }
            return true;
        }

        public static bool InsertUpdateFromImportLCBill(ref BEEContext context, ILCB iLCB, List<ILCBLine> ilcbline)
        {
            var accountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            var itemImportedLCAccount = ilcbline.FirstOrDefault(x => x.DebitAccID == accountID);
            if (itemImportedLCAccount != null)
            {
                var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == iLCB.ILCBNo && x.DocType == "ILCB" && x.ILCID == iLCB.ILCID);

                if (importLcBal == null)
                {
                    var amount = ilcbline.FirstOrDefault(x => x.DebitAccName == "Imported L/C Account").Amount;
                    //var amount = ilcbline.FirstOrDefault(x => x.DebitAccName == "Imported L/C Account").Amount;
                    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                    iclBalCal.ILCID = iLCB.ILCID;
                    iclBalCal.Date = iLCB.Date;
                    iclBalCal.PaymentAccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
                    iclBalCal.DocType = "ILCB";
                    iclBalCal.Amount = amount;
                    //iclBalCal.Amount = iLCB.BillTotalValue;
                    iclBalCal.Refno = "";
                    iclBalCal.Descriptions = ilcbline.FirstOrDefault(x => x.DebitAccName == "Imported L/C Account").Description;
                    iclBalCal.DocNo = Convert.ToInt32(iLCB.ILCBNo);
                    context.ILCBalCalculation.Add(iclBalCal);
                }
                else
                {
                    var amount = ilcbline.FirstOrDefault(x => x.DebitAccName == "Imported L/C Account").Amount;
                    importLcBal.ILCID = iLCB.ILCID;
                    importLcBal.Date = iLCB.Date;
                    importLcBal.PaymentAccountID = importLcBal.PaymentAccountID;
                    importLcBal.DocType = "ILCB";
                    importLcBal.Amount = amount;
                    //importLcBal.Amount = iLCB.BillTotalValue;
                    importLcBal.Refno = "";
                    importLcBal.Descriptions = ilcbline.FirstOrDefault(x => x.DebitAccName == "Imported L/C Account").Description;
                    importLcBal.DocNo = Convert.ToInt32(iLCB.ILCBNo);
                    context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
                }

            }

            return true;
            
        }
        #region OLD INSERT FROM ILCP
        //public static bool InsertFromILCP(ref BEEContext context, ILCP iLCP)
        //{
        //    var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == iLCP.clmILCPNO && x.DocType == "ILCP" && x.ILCID == iLCP.clmILCID);
        //    if (importLcBal == null)
        //    {
        //        ILCBalCalculations iclBalCal = new ILCBalCalculations();
        //        iclBalCal.ILCID = iLCP.clmILCID;
        //        iclBalCal.Date = iLCP.clmDate;
        //        iclBalCal.PaymentAccountID = iLCP.clmAccountID;
        //        iclBalCal.DocType = "ILCP";
        //        iclBalCal.Amount = iLCP.clmTotalAmount;
        //        iclBalCal.Refno = "";
        //        iclBalCal.Descriptions = "";
        //        iclBalCal.DocNo = Convert.ToInt32(iLCP.clmILCPNO);
        //        context.ILCBalCalculation.Add(iclBalCal);
        //    }
        //    else
        //    {
        //        importLcBal.ILCID = iLCP.clmILCID;
        //        importLcBal.Date = iLCP.clmDate;
        //        importLcBal.PaymentAccountID = iLCP.clmAccountID;
        //        importLcBal.DocType = "ILCP";
        //        importLcBal.Amount = iLCP.clmTotalAmount;
        //        importLcBal.Refno = "";
        //        importLcBal.Descriptions = "";
        //        importLcBal.DocNo = Convert.ToInt32(iLCP.clmILCPNO);
        //        context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
        //    }
        //    return true;
        //}
        #endregion

        public static bool InsertUpdateFromLCPV(ref BEEContext context, ILCP iLCP, List<ILCPLine> iLCPLines)
        {
            //select* from AccountConfiguration where Name = 'Import LC Account'
            var accountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            var itemImportedLCAccount = iLCPLines.FirstOrDefault(x => x.DebitAccID == accountID);
            if (itemImportedLCAccount!=null)
            {
                var importLcBal = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == iLCP.clmILCPNO && x.DocType == "LCPV" && x.ILCID == iLCP.clmILCID);
                if (importLcBal == null)
                {
                    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                    iclBalCal.ILCID = iLCP.clmILCID;
                    iclBalCal.Date = iLCP.clmDate;
                    iclBalCal.PaymentAccountID = iLCP.clmAccountID;
                    iclBalCal.DocType = "LCPV";
                    iclBalCal.Amount = itemImportedLCAccount.Amount;
                    iclBalCal.Refno = "";
                    iclBalCal.Descriptions = "";
                    iclBalCal.DocNo = Convert.ToInt32(iLCP.clmILCPNO);
                    context.ILCBalCalculation.Add(iclBalCal);
                }
                else
                {
                    importLcBal.ILCID = iLCP.clmILCID;
                    importLcBal.Date = iLCP.clmDate;
                    importLcBal.PaymentAccountID = iLCP.clmAccountID;
                    importLcBal.DocType = "LCPV";
                    importLcBal.Amount = itemImportedLCAccount.Amount;
                    importLcBal.Refno = "";
                    importLcBal.Descriptions = "";
                    importLcBal.DocNo = Convert.ToInt32(iLCP.clmILCPNO);
                    context.Entry<ILCBalCalculations>(importLcBal).State = EntityState.Modified;
                }
            }
            
            return true;
        }

        public static bool InsertFromImportLcPayment(ref BEEContext context, ILCPayment lcp, List<ILCPaymentLineItem> lcpLine)
        {
            var ilcAccount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account");

            foreach (var x in lcpLine)
            {
                var lcAccBalExist = context.ILCBalCalculation.Where(y => y.DocType == "ILCP" && y.DocNo == lcp.ILCPNo).FirstOrDefault();

                if (lcAccBalExist != null)
                {
                    context.ILCBalCalculation.Remove(lcAccBalExist);
                }

                ILCBalCalculations lcbal = new ILCBalCalculations();
                lcbal.ILCID = lcp.ILCID;
                lcbal.Date = lcp.Date;
                lcbal.PaymentAccountID = lcp.AccountID;
                lcbal.Amount = lcp.TotalAmount;
                lcbal.Descriptions = "ILCP";
                lcbal.Refno = "ILCP";
                lcbal.DocType = "ILCP";
                lcbal.DocNo = lcp.ILCPNo;

                context.ILCBalCalculation.Add(lcbal);
            }
            return true;
        }
        public static bool InsertFromImportLCPaymentD(ref BEEContext context, ImportLCPayment importLCPayment)
        {
            var importLcBal = context.ILCBalCalculation.Where(x => x.DocNo == importLCPayment.ILCPNo && x.DocType == "ILCP" && x.ILCID == importLCPayment.ILCID).ToList();
            if (importLcBal.Count <= 0)
            {
                //if (importLCPayment.DebitAccID > 0)
                //{
                //    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                //    iclBalCal.ILCID = importLCPayment.ILCID;
                //    iclBalCal.Date = importLCPayment.Date;
                //    iclBalCal.PaymentAccountID = importLCPayment.DebitAccID;
                //    iclBalCal.DocType = "ILCPD";
                //    iclBalCal.Amount = importLCPayment.Amount * -1;
                //    iclBalCal.Refno = importLCPayment.RefNo;
                //    iclBalCal.Descriptions = importLCPayment.Description;
                //    iclBalCal.DocNo = Convert.ToInt32(importLCPayment.ILCPNo);
                //    context.ILCBalCalculation.Add(iclBalCal);
                //}
                if (importLCPayment.CreditAccID > 0)
                {
                    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                    iclBalCal.ILCID = importLCPayment.ILCID;
                    iclBalCal.Date = importLCPayment.Date;
                    iclBalCal.PaymentAccountID = importLCPayment.CreditAccID;
                    iclBalCal.DocType = "ILCP";
                    iclBalCal.Amount = importLCPayment.Amount;
                    iclBalCal.Refno = importLCPayment.RefNo;
                    iclBalCal.Descriptions = importLCPayment.Description;
                    iclBalCal.DocNo = Convert.ToInt32(importLCPayment.ILCPNo);
                    context.ILCBalCalculation.Add(iclBalCal);
                }
            }
            else
            {
                //var importLcBalD = importLcBal.FirstOrDefault(x => x.Amount < 0);
                //context.ILCBalCalculation.Remove(importLcBalD);
                var importLcBalC = importLcBal.FirstOrDefault(x => x.Amount >= 0);
                context.ILCBalCalculation.Remove(importLcBalC);
                //if (importLCPayment.DebitAccID > 0)
                //{
                //    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                //    iclBalCal.ILCID = importLCPayment.ILCID;
                //    iclBalCal.Date = importLCPayment.Date;
                //    iclBalCal.PaymentAccountID = importLCPayment.DebitAccID;
                //    iclBalCal.DocType = "ILCPD";
                //    iclBalCal.Amount = importLCPayment.Amount * -1;
                //    iclBalCal.Refno = importLCPayment.RefNo;
                //    iclBalCal.Descriptions = importLCPayment.Description;
                //    iclBalCal.DocNo = Convert.ToInt32(importLCPayment.ILCPNo);
                //    context.ILCBalCalculation.Add(iclBalCal);
                //}
                if (importLCPayment.CreditAccID > 0)
                {
                    ILCBalCalculations iclBalCal = new ILCBalCalculations();
                    iclBalCal.ILCID = importLCPayment.ILCID;
                    iclBalCal.Date = importLCPayment.Date;
                    iclBalCal.PaymentAccountID = importLCPayment.CreditAccID;
                    iclBalCal.DocType = "ILCP";
                    iclBalCal.Amount = importLCPayment.Amount;
                    iclBalCal.Refno = importLCPayment.RefNo;
                    iclBalCal.Descriptions = importLCPayment.Description;
                    iclBalCal.DocNo = Convert.ToInt32(importLCPayment.ILCPNo);
                    context.ILCBalCalculation.Add(iclBalCal);
                }
            }
            return true;
        }
        public static bool DeleteFromImportLCPaymentD(ref BEEContext context, ImportLCPayment importLCPayment)
        {
            var importLcBal = context.ILCBalCalculation.Where(x => x.DocNo == importLCPayment.ILCPNo && x.DocType == "ILCP" && x.ILCID == importLCPayment.ILCID).ToList();
            //var importLcBalD = importLcBal.FirstOrDefault(x => x.Amount < 0);
            //if (importLcBalD != null)
            //{ context.ILCBalCalculation.Remove(importLcBalD); }

            var importLcBalC = importLcBal.FirstOrDefault(x => x.Amount >= 0);
            if (importLcBalC!=null)
            { context.ILCBalCalculation.Remove(importLcBalC); }
            return true;
        }

        public static bool InsertUdpateFromILC(ref BEEContext context, LCCosting lcCosting)
        {
            var importLcCosting = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lcCosting.LCPCNo && x.DocType == "LCPC" && x.ILCID == lcCosting.ILCID);
            if(importLcCosting!=null)
            {
                context.ILCBalCalculation.Remove(importLcCosting);
            }
            ILCBalCalculations iclBalCal = new ILCBalCalculations();
            iclBalCal.ILCID = lcCosting.ILCID;
            iclBalCal.Date = lcCosting.LCPCDate;
            //iclBalCal.PaymentAccountID = lcCosting.;
            iclBalCal.DocType = "LCPC";
            iclBalCal.Amount = lcCosting.CostingTotal*-1;
            iclBalCal.Refno = "";
            iclBalCal.Descriptions = "Import LC Costing";
            iclBalCal.DocNo = Convert.ToInt32(lcCosting.LCPCNo);
            context.ILCBalCalculation.Add(iclBalCal);
            return true;
        }
        public static bool DeleteFromILC(ref BEEContext context,LCCosting lCCosting)
        {
            var importLcCosting = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == lCCosting.LCPCNo && x.DocType == "LCPC" && x.ILCID == lCCosting.ILCID);
            if(importLcCosting!=null)
            {
                context.ILCBalCalculation.Remove(importLcCosting);
            }
            return true;
        }
        public static bool DeleteFromILCBill(ref BEEContext context, ILCB iLCB)
        {
            var ILCBill = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == iLCB.ILCBNo && x.DocType == "ILCB" && x.ILCID == iLCB.ILCID);
            if(ILCBill != null)
            {
                context.ILCBalCalculation.Remove(ILCBill);
            }
            return true;
        }
        public static bool DeleteFromILCPayment(ref BEEContext context, ILCP iLCP)
        {
            var ILCP = context.ILCBalCalculation.FirstOrDefault(x => x.DocNo == iLCP.clmILCPNO && x.DocType == "LCPV" && x.ILCID == iLCP.clmILCID);
            if (ILCP != null)
            {
                context.ILCBalCalculation.Remove(ILCP);
            }
            return true;
        }
        public static bool InsertUpdateFromILCOB()
        {
            return true;
        }
    }
}