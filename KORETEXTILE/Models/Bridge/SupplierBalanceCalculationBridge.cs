using BEEERP.Models.AccountModule;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class SupplierBalanceCalculationBridge
    {
        //public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill, List<GeneralBillsLineItem> generalBillsLineItems)
        //{
        //    int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
        //    int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
        //    var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "GBE" && x.SupplierID == generalbill.SupplierId && x.DocNo == generalbill.GBENo).ToList();
        //    if (isexist.Count > 0)
        //    {
        //        context.SupplierBalanceCalculation.RemoveRange(isexist);
        //    }
        //    foreach (var item in generalBillsLineItems)
        //    {
        //        SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
        //        sbc.DocNo = generalbill.GBENo;
        //        sbc.SupplierID = generalbill.SupplierId;
        //        sbc.TDate = generalbill.GBEDate;
        //        sbc.DocumentType = "GBE";
        //        sbc.Amount = (-1) * item.Amount;
        //        sbc.RefNo = generalbill.RefNo;
        //        sbc.TrDesription = "General Bill";
        //        sbc.AccountID = item.DebitAccId;
        //        context.SupplierBalanceCalculation.Add(sbc);
        //    }
        //    SupplierBalanceCalculation sbc1 = new SupplierBalanceCalculation();
        //    sbc1.DocNo = generalbill.GBENo;
        //    sbc1.SupplierID = generalbill.SupplierId;
        //    sbc1.TDate = generalbill.GBEDate;
        //    sbc1.DocumentType = "GBE";
        //    sbc1.Amount = (-1) * generalbill.VATAmount;
        //    sbc1.RefNo = generalbill.RefNo;
        //    sbc1.TrDesription = "General Bill";
        //    sbc1.AccountID = vatId;
        //    context.SupplierBalanceCalculation.Add(sbc1);

        //    SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
        //    sbc2.DocNo = generalbill.GBENo;
        //    sbc2.SupplierID = generalbill.SupplierId;
        //    sbc2.TDate = generalbill.GBEDate;
        //    sbc2.DocumentType = "GBE";
        //    sbc2.Amount = (-1) * generalbill.AITAmount;
        //    sbc2.RefNo = generalbill.RefNo;
        //    sbc2.TrDesription = "General Bill";
        //    sbc2.AccountID = aitId;
        //    context.SupplierBalanceCalculation.Add(sbc2);
        //    return true;
        //}

        //public static bool DeleteFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill)
        //{
        //    var isExist = context.SupplierBalanceCalculation.Where(x => x.DocNo == generalbill.GBENo && x.DocumentType == "GBE").ToList();
        //    foreach(var x in isExist)
        //    {
        //        context.SupplierBalanceCalculation.Remove(x);
        //    }
        //    return true;
        //}

        //public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill, List<GeneralBillsLineItem> generalBillsLineItems)
        //{
        //    int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Vat").RefValue;
        //    int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
        //    var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "GSB" && x.SupplierID == generalbill.SupplierId && x.DocNo == generalbill.GBENo).ToList();
        //    if (isexist.Count > 0)
        //    {
        //        context.SupplierBalanceCalculation.RemoveRange(isexist);
        //    }
        //    foreach (var item in generalBillsLineItems)
        //    {
        //        SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
        //        sbc.DocNo = generalbill.GBENo;
        //        sbc.SupplierID = generalbill.SupplierId;
        //        sbc.TDate = generalbill.GBEDate;
        //        sbc.DocumentType = "GSB";
        //        sbc.Amount = item.Amount;
        //        sbc.RefNo = generalbill.RefNo;
        //        sbc.TrDesription = "General Bill";
        //        sbc.AccountID = item.DebitAccId;
        //        context.SupplierBalanceCalculation.Add(sbc);
        //    }
        //    SupplierBalanceCalculation sbc1 = new SupplierBalanceCalculation();
        //    sbc1.DocNo = generalbill.GBENo;
        //    sbc1.SupplierID = generalbill.SupplierId;
        //    sbc1.TDate = generalbill.GBEDate;
        //    sbc1.DocumentType = "GSB";
        //    sbc1.Amount = (-1) * generalbill.VATAmount;
        //    sbc1.RefNo = generalbill.RefNo;
        //    sbc1.TrDesription = "General Bill";
        //    sbc1.AccountID = vatId;
        //    context.SupplierBalanceCalculation.Add(sbc1);

        //    SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
        //    sbc2.DocNo = generalbill.GBENo;
        //    sbc2.SupplierID = generalbill.SupplierId;
        //    sbc2.TDate = generalbill.GBEDate;
        //    sbc2.DocumentType = "GSB";
        //    sbc2.Amount = (-1) * generalbill.AITAmount;
        //    sbc2.RefNo = generalbill.RefNo;
        //    sbc2.TrDesription = "General Bill";
        //    sbc2.AccountID = aitId;
        //    context.SupplierBalanceCalculation.Add(sbc2);
        //    return true;
        //}

        //public static bool DeleteFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill, List<GeneralBillsLineItem> generalBillsLineItems)
        //{
        //    if (generalbill != null)
        //    {
        //        var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "GSB" && x.SupplierID == generalbill.SupplierId && x.DocNo == generalbill.GBENo).ToList();
        //        if (isexist.Count > 0)
        //        {
        //            context.SupplierBalanceCalculation.RemoveRange(isexist);
        //        }
        //    }
        //    return true;
        //}
        public static bool DeleteFromILCB(BEEContext context, ILCB iLCB)
        {


            var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "ILCB" && x.DocNo == iLCB.ILCBNo).ToList();

            if (supplierBal != null)
            {
                supplierBal.ForEach(x =>
                {
                    context.SupplierBalanceCalculation.Remove(x);
                });

            }


            return true;
        }
        public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalBill, List<GeneralBillsLineItem> generalBillsLineItems)
        {
            int psId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue;
            var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "GSB" && x.SupplierID == generalBill.SupplierId && x.DocNo == generalBill.GBENo).ToList();
            if (isexist.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(isexist);
            }
            foreach (var generalBillsLine in generalBillsLineItems)
            {
                SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
                sbc.DocNo = generalBill.GBENo;
                sbc.SupplierID = generalBill.SupplierId;
                sbc.TDate = generalBill.GBEDate;
                sbc.DocumentType = "GSB";
                sbc.Amount = (decimal)generalBillsLine.Amount;
                sbc.RefNo = generalBill.RefNo;
                sbc.TrDesription = generalBillsLine.Descriptions;
                sbc.AccountID = generalBillsLine.DebitAccId;
                context.SupplierBalanceCalculation.Add(sbc);
            }  
            if (generalBill.VATAmount > 0)
            {
                SupplierBalanceCalculation sbc1 = new SupplierBalanceCalculation();
                sbc1.DocNo = generalBill.GBENo;
                sbc1.SupplierID = generalBill.SupplierId;
                sbc1.TDate = generalBill.GBEDate;
                sbc1.DocumentType = "GSB";
                sbc1.Amount = (decimal)generalBill.VATAmount;
                sbc1.RefNo = generalBill.RefNo;
                sbc1.TrDesription = "VAT";
                sbc1.AccountID = vatId;
                context.SupplierBalanceCalculation.Add(sbc1);
            }
            if (generalBill.AITAmount > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = generalBill.GBENo;
                sbc2.SupplierID = generalBill.SupplierId;
                sbc2.TDate = generalBill.GBEDate;
                sbc2.DocumentType = "GSB";
                sbc2.Amount = (-1) * (decimal)generalBill.AITAmount;
                sbc2.RefNo = generalBill.RefNo;
                sbc2.TrDesription = "AIT";
                sbc2.AccountID = aitId;
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (generalBill.Discountamt > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = generalBill.GBENo;
                sbc2.SupplierID = generalBill.SupplierId;
                sbc2.TDate = generalBill.GBEDate;
                sbc2.DocumentType = "GSB";
                sbc2.Amount = (-1) * (decimal)generalBill.Discountamt;
                sbc2.RefNo = generalBill.RefNo;
                sbc2.TrDesription = "Discount";
                sbc2.AccountID = discount;
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (generalBill.VDSAmount > 0)
            {
                SupplierBalanceCalculation sbc3 = new SupplierBalanceCalculation();
                sbc3.DocNo = generalBill.GBENo;
                sbc3.SupplierID = generalBill.SupplierId;
                sbc3.TDate = generalBill.GBEDate;
                sbc3.DocumentType = "GSB";
                sbc3.Amount = (-1) * (decimal)generalBill.VDSAmount;
                sbc3.RefNo = generalBill.RefNo;
                sbc3.TrDesription ="VDS";
                sbc3.AccountID = vdsId;
                context.SupplierBalanceCalculation.Add(sbc3);
            }
            return true;
        }


        public static bool InsertUpdateFromILCB(BEEContext context, ILCB iLCB, List<ILCBLine> iLCBLines)
        {

            var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "ILCB" && x.DocNo == iLCB.ILCBNo).ToList();

            if (supplierBal != null)
            {
                supplierBal.ForEach(x =>
                {
                    context.SupplierBalanceCalculation.Remove(x);
                });

                iLCBLines.ForEach(x =>
                {
                    SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
                    sb.SupplierID = iLCB.SupplierID;
                    sb.TDate = iLCB.Date;
                    sb.DocumentType = "ILCB";
                    sb.AccountID = x.DebitAccID;
                    sb.TrDesription = x.Description;
                    sb.RefNo = "ILCB";
                    sb.Amount = x.Amount;
                    sb.DocNo = x.ILCBNo;

                    context.SupplierBalanceCalculation.Add(sb);
                });

            }


            return true;
        }
        public static bool DeleteFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill)
        {
            var supbal = context.SupplierBalanceCalculation.Where(x => x.DocNo == generalbill.GBENo && x.DocumentType == "GSB").ToList(); 
            if (supbal.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(supbal);
            }
            return true;
        }

        public static bool InsertUpdateFromPaymentRefundFromSupplier(ref BEEContext context, PRS prs)
        {
            
            //var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "PRFS" && x.DocNo == prs.PRSNo).ToList();
            //if (isexist.Count > 0)
            //{
            //    context.SupplierBalanceCalculation.RemoveRange(isexist);
            //}
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == prs.PRSNo && x.DocumentType == "PRFS" && x.SupplierID == prs.SupplierID);
            if (supbal==null)
            {
                SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
                sbc.DocNo = prs.PRSNo;
                sbc.SupplierID = prs.SupplierID;
                sbc.TDate = prs.Date;
                sbc.DocumentType = "PRFS";
                sbc.Amount = prs.ReturnAmount;
                sbc.RefNo = prs.RefNo;
                sbc.TrDesription = prs.Description;
                sbc.AccountID = prs.ReceiveAccountID;
                context.SupplierBalanceCalculation.Add(sbc);
            }
            else
            {
                supbal.SupplierID = prs.SupplierID;
                supbal.TDate = prs.Date;
                supbal.DocumentType = "PRFS";
                supbal.Amount = prs.ReturnAmount;
                supbal.RefNo = prs.RefNo;
                supbal.TrDesription = prs.Description;
                //supbal.AccountID = prs.ReceiveAccountID;
                context.Entry<SupplierBalanceCalculation>(supbal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool DeleteFromPaymentRefundFromSupplier(ref BEEContext context, PRS prs)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == prs.PRSNo && x.DocumentType == "PRFS" && x.SupplierID == prs.SupplierID);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
                return true;
            }
            return false;
        }
        public static bool InsertUpdateFromGoodPurchaseBill(ref BEEContext context, PurchaseEntry purchasebill)
        {
            int psId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue; 
            var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "GPB" && x.SupplierID == purchasebill.SupplierId && x.DocNo == purchasebill.BillNo).ToList();
            if (isexist.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(isexist);
            }

            //List<SupplierBalanceCalculation> sbl = new List<SupplierBalanceCalculation>();
            SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
            sbc.DocNo = purchasebill.BillNo;
            sbc.SupplierID = purchasebill.SupplierId;
            sbc.TDate = purchasebill.PDate;
            sbc.DocumentType = "GPB";
            sbc.Amount =  (decimal)purchasebill.BillTotalValue;
            sbc.RefNo = purchasebill.PurRef;
            sbc.TrDesription = purchasebill.PurDescription;
            sbc.AccountID = psId;
            //sbl.Add(sbc);
            context.SupplierBalanceCalculation.Add(sbc);
            if(purchasebill.VatAmount  > 0)
            {
                //For Vat Amount
                SupplierBalanceCalculation sbc1 = new SupplierBalanceCalculation();
                sbc1.DocNo = purchasebill.BillNo;
                sbc1.SupplierID = purchasebill.SupplierId;
                sbc1.TDate = purchasebill.PDate;
                sbc1.DocumentType = "GPB";
                sbc1.Amount = (decimal)purchasebill.VatAmount;
                sbc1.RefNo = purchasebill.PurRef;
                sbc1.TrDesription = purchasebill.PurDescription;
                sbc1.AccountID = vatId;
                //sbl.Add(sbc1);
                context.SupplierBalanceCalculation.Add(sbc1);
            }           

            if (purchasebill.AitAmount > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = purchasebill.BillNo;
                sbc2.SupplierID = purchasebill.SupplierId;
                sbc2.TDate = purchasebill.PDate;
                sbc2.DocumentType = "GPB";
                sbc2.Amount = (-1) * (decimal)purchasebill.AitAmount;
                sbc2.RefNo = purchasebill.PurRef;
                sbc2.TrDesription = purchasebill.PurDescription;
                sbc2.AccountID = aitId;
                //sbl.Add(sbc2);
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (purchasebill.DiscountAmt > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = purchasebill.BillNo;
                sbc2.SupplierID = purchasebill.SupplierId;
                sbc2.TDate = purchasebill.PDate;
                sbc2.DocumentType = "GPB";
                sbc2.Amount = (-1) * (decimal)purchasebill.DiscountAmt;
                sbc2.RefNo = purchasebill.PurRef;
                sbc2.TrDesription = purchasebill.PurDescription;
                sbc2.AccountID = discount;
                //sbl.Add(sbc2);
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (purchasebill.VDAAmnt > 0)
            {
                //For Vat Amount
                SupplierBalanceCalculation sbc3 = new SupplierBalanceCalculation();
                sbc3.DocNo = purchasebill.BillNo;
                sbc3.SupplierID = purchasebill.SupplierId;
                sbc3.TDate = purchasebill.PDate;
                sbc3.DocumentType = "GPB";
                sbc3.Amount = (-1)*(decimal)purchasebill.VDAAmnt;
                sbc3.RefNo = purchasebill.PurRef;
                sbc3.TrDesription = purchasebill.PurDescription;
                sbc3.AccountID = vdsId;
                //sbl.Add(sbc1);
                context.SupplierBalanceCalculation.Add(sbc3);
            }


            return true;
        }

        public static bool InsertUpdateFromFreezerPurchaseBill(ref BEEContext context, FPB fpb)
        {
            int caId = fpb.DebtAccID;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue;
            var isexist = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "FPB" && x.SupplierID == fpb.SupplierId && x.DocNo == fpb.BillNo).ToList();
            if (isexist.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(isexist);
            }

            //List<SupplierBalanceCalculation> sbl = new List<SupplierBalanceCalculation>();
            SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
            sbc.DocNo = fpb.BillNo;
            sbc.SupplierID = fpb.SupplierId;
            sbc.TDate = fpb.PDate;
            sbc.DocumentType = "FPB";
            sbc.Amount = (decimal)fpb.BillTotalValue;
            sbc.RefNo = fpb.PurRef;
            sbc.TrDesription = fpb.PurDescription;
            sbc.AccountID = caId;
            //sbl.Add(sbc);
            context.SupplierBalanceCalculation.Add(sbc);
            if (fpb.VatAmount > 0)
            {
                //For Vat Amount
                SupplierBalanceCalculation sbc1 = new SupplierBalanceCalculation();
                sbc1.DocNo = fpb.BillNo;
                sbc1.SupplierID = fpb.SupplierId;
                sbc1.TDate = fpb.PDate;
                sbc1.DocumentType = "FPB";
                sbc1.Amount = (decimal)fpb.VatAmount;
                sbc1.RefNo = fpb.PurRef;
                sbc1.TrDesription = fpb.PurDescription;
                sbc1.AccountID = vatId;
                //sbl.Add(sbc1);
                context.SupplierBalanceCalculation.Add(sbc1);
            }

            if (fpb.AitAmount > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = fpb.BillNo;
                sbc2.SupplierID = fpb.SupplierId;
                sbc2.TDate = fpb.PDate;
                sbc2.DocumentType = "FPB";
                sbc2.Amount = (-1) * (decimal)fpb.AitAmount;
                sbc2.RefNo = fpb.PurRef;
                sbc2.TrDesription = fpb.PurDescription;
                sbc2.AccountID = aitId;
                //sbl.Add(sbc2);
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (fpb.DiscountAmt > 0)
            {
                SupplierBalanceCalculation sbc2 = new SupplierBalanceCalculation();
                sbc2.DocNo = fpb.BillNo;
                sbc2.SupplierID = fpb.SupplierId;
                sbc2.TDate = fpb.PDate;
                sbc2.DocumentType = "FPB";
                sbc2.Amount = (-1) * (decimal)fpb.DiscountAmt;
                sbc2.RefNo = fpb.PurRef;
                sbc2.TrDesription = fpb.PurDescription;
                sbc2.AccountID = discount;
                //sbl.Add(sbc2);
                context.SupplierBalanceCalculation.Add(sbc2);
            }
            if (fpb.VDAAmnt > 0)
            {
                //For Vat Amount
                SupplierBalanceCalculation sbc3 = new SupplierBalanceCalculation();
                sbc3.DocNo = fpb.BillNo;
                sbc3.SupplierID = fpb.SupplierId;
                sbc3.TDate = fpb.PDate;
                sbc3.DocumentType = "FPB";
                sbc3.Amount = (-1) * (decimal)fpb.VDAAmnt;
                sbc3.RefNo = fpb.PurRef;
                sbc3.TrDesription = fpb.PurDescription;
                sbc3.AccountID = vdsId;
                //sbl.Add(sbc1);
                context.SupplierBalanceCalculation.Add(sbc3);
            }


            return true;
        }

        public static bool InsertUpdatefromILCB(BEEContext context,  ILCB iLCB, List<ILCBLine> iLCBLines)
        {
            var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "LCSP" && x.DocNo == iLCB.ILCBNo).ToList();

            if (supplierBal != null)
            {
                supplierBal.ForEach(x =>
                {
                    context.SupplierBalanceCalculation.Remove(x);
                });

                iLCBLines.ForEach(x =>
                {
                    SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
                    sb.SupplierID = iLCB.SupplierID;
                    sb.TDate = iLCB.Date;
                    sb.DocumentType = "LCSP";
                    sb.AccountID = x.DebitAccID;
                    sb.TrDesription = x.Description;
                    sb.RefNo = "LCSP";
                    sb.Amount = x.Amount;
                    sb.DocNo = x.ILCBNo;

                    context.SupplierBalanceCalculation.Add(sb);
                });

            }





            return true;
        }
        public static bool InsertUpdateFromSAAD(ref BEEContext context, SupplierAcAdjustment supplierAcAdjustment)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == supplierAcAdjustment.SAADNo && x.DocumentType == "SAAD" && x.SupplierID == supplierAcAdjustment.SupplierId);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
            }

            SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
            sbc.DocNo = supplierAcAdjustment.SAADNo;
            sbc.SupplierID = supplierAcAdjustment.SupplierId;
            sbc.TDate = supplierAcAdjustment.Date;
            sbc.DocumentType = "SAAD";
            sbc.Amount = supplierAcAdjustment.Amount;
            sbc.RefNo = supplierAcAdjustment.RefNo;
            sbc.TrDesription = supplierAcAdjustment.Description;
            sbc.AccountID = supplierAcAdjustment.LedgerAcId;
            context.SupplierBalanceCalculation.Add(sbc);
            return true;
        }
        public static bool DeleteFromSupplierAccountAdjustment(ref BEEContext context, SupplierAcAdjustment supplierAcAdjustment)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == supplierAcAdjustment.SAADNo && x.DocumentType == "SAAD" && x.SupplierID == supplierAcAdjustment.SupplierId);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
            }
            return true;
        }

        public static bool InsertUpdateFromSupplierPaymentVoucher(ref BEEContext context,PayBillInfo payBill)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == payBill.PBID && x.DocumentType == "ILCB" && x.SupplierID == payBill.SupplierId);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
            }
          
            SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
            sbc.DocNo = payBill.PBID;
            sbc.SupplierID = payBill.SupplierId;
            sbc.TDate = payBill.TDate;
            sbc.DocumentType = "ILCB";
            sbc.Amount = payBill.Amount;
            sbc.RefNo = payBill.RefNo;
            sbc.TrDesription = payBill.Description;
            sbc.AccountID = payBill.PaymentAccId;
            context.SupplierBalanceCalculation.Add(sbc);
            return true;
        }
        public static bool DeleteFromSupplierPaymentVoucher(ref BEEContext context,PayBillInfo spvoucher)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == spvoucher.PBID && x.DocumentType == "SPV" && x.SupplierID == spvoucher.SupplierId);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
            }
            return true;
        }


        public static bool InsertUpdateFromSupplierPaymentVoucher(ref BEEContext context, PayBillInfo spvoucher, List<PayBillLine> addedItems)
        {
            var supbal = context.SupplierBalanceCalculation.FirstOrDefault(x => x.DocNo == spvoucher.PBID && x.DocumentType == "SPV" && x.SupplierID == spvoucher.SupplierId);
            if (supbal != null)
            {
                context.SupplierBalanceCalculation.Remove(supbal);
            }
            string billType = "";
            if (addedItems.Count() == 0)
            {
                billType = "Advance";
            }
            else
            {
                billType = addedItems.FirstOrDefault().BillType;
            }


            SupplierBalanceCalculation sbc = new SupplierBalanceCalculation();
            sbc.DocNo = spvoucher.PBID;
            sbc.SupplierID = spvoucher.SupplierId;
            sbc.TDate = spvoucher.TDate;
            sbc.DocumentType = "SPV";
            sbc.Amount = spvoucher.PaidAmt * -1;
            sbc.RefNo = spvoucher.RefNo;
            sbc.TrDesription = spvoucher.Description;
            sbc.AccountID = spvoucher.PaymentAccId;
            context.SupplierBalanceCalculation.Add(sbc);
            return true;
        }

       

        public static bool DeleteFromGPB(ref BEEContext context, PurchaseEntry purchaseEntry)
        {
            var supbal = context.SupplierBalanceCalculation.Where(x => x.DocNo == purchaseEntry.BillNo && x.DocumentType == "GPB").ToList();
            //var isExist = context.Transection.Where(x => (x.DocType == "GPB" || x.DocType == "GPBT" || x.DocType == "GPBD" || x.DocType == "GPBV") && x.DocID == billNo).ToList();
            if (supbal.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(supbal);
            }
            return true;
        }
        public static bool DeleteFromFPB(ref BEEContext context, FPB fpb)
        {
            var supbal = context.SupplierBalanceCalculation.Where(x => x.DocNo == fpb.BillNo && x.DocumentType == "FPB").ToList();
            //var isExist = context.Transection.Where(x => (x.DocType == "GPB" || x.DocType == "GPBT" || x.DocType == "GPBD" || x.DocType == "GPBV") && x.DocID == billNo).ToList();
            if (supbal.Count > 0)
            {
                context.SupplierBalanceCalculation.RemoveRange(supbal);
            }
            return true;
        }
        
    }
}