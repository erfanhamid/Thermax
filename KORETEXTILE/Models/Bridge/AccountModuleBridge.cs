using BEEERP.Models.AccountModule;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Database;
using BEEERP.Models.HRModule;
using BEEERP.Models.PaymentReceiveInfo;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.Procurement;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using BEEERP.Models.StoreDepot;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.EmployeeAccountOB;
using BEEERP.Models.StoreRM;
using BEEERP.Models.StoreRM.RMSalesEntry;
using BEEERP.Models.FreezerRent;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.CostingModule;

namespace BEEERP.Models.Bridge
{
    public static class AccountModuleBridge
    {
        internal static void InsertUpdateFromLCRN(ref BEEContext context, GoodsReceiveNote lcrn, List<GoodsReceiveNoteLineItem> addedItems)
        {
            int InventoryGLID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int ProvisionID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Provision for Supplier").RefValue;
            var items = context.Transection.Where(x => x.DocType == "LCRN" && x.DocID == lcrn.GRNNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);


           // context.Transection.Add(new Transection(InventoryGLID, -1, (Decimal)lcrn.TotalCost, trid,
                  //  lcrn.GRNDate, lcrn.RefNo, "", lcrn.Descriptions, "LCRN", lcrn.GRNNo, 1, 1));


           // context.Transection.Add(new Transection(ProvisionID, 1, (decimal)lcrn.TotalCost, trid,
              //   lcrn.GRNDate, lcrn.RefNo, "", lcrn.Descriptions, "LCRN", lcrn.GRNNo, 1, 1));
        }
        internal static bool DeleteFromLCRNEntry(ref BEEContext context, GoodsReceiveNote lcrn)
        {
            
            var items = context.Transection.Where(x => x.DocType == "LCRN" && x.DocID == lcrn.GRNNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }
        internal static void InsertUpdateFromGRN(ref BEEContext context, GoodsReceiveNote grn, List<GoodsReceiveNoteLineItem> addedItems1)
        {
            int InventoryGLID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int ProvisionID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Provision for Supplier").RefValue;
            var items = context.Transection.Where(x => x.DocType == "GRN" && x.DocID == grn.GRNNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);


            //context.Transection.Add(new Transection(InventoryGLID, -1, (Decimal)grn.TotalCost, trid,
            //        grn.GRNDate, grn.RefNo, "", grn.Descriptions, "GRN", grn.GRNNo, 1, 1));


            //context.Transection.Add(new Transection(ProvisionID, 1, (decimal)grn.TotalCost, trid,
            //     grn.GRNDate, grn.RefNo, "", grn.Descriptions, "GRN", grn.GRNNo, 1, 1));
        }
        internal static void InsertUpdateFromFARR(ref BEEContext context, FARR farr)
        {
            int AdvanceGLID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance Rental Income- Freeze").RefValue;
            var items = context.Transection.Where(x => x.DocType == "FARR" && x.DocID == farr.clmFARRNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);


            context.Transection.Add(new Transection(AdvanceGLID, -1, (Decimal)farr.clmTotalAmount, trid,
                    farr.clmDate, "", "", "", "FARR", farr.clmFARRNo, 1, 1));


            context.Transection.Add(new Transection(farr.clmAccountID, 1, (decimal)farr.clmTotalAmount, trid,
                farr.clmDate, "", "", "", "FARR", farr.clmFARRNo, 1, 1));
        }

        internal static void InsertUpdateFromFRI(ref BEEContext context, FRI fri)
        {
            int AdvanceGLID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance Rental Income- Freeze").RefValue;
            int IncomeAccID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Freeze Rent Income").RefValue;
            var items = context.Transection.Where(x => x.DocType == "FRI" && x.DocID == fri.clmFRINo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }

            int trid = GenerateTrid(ref context);

          
            context.Transection.Add(new Transection(AdvanceGLID, -1, (Decimal)fri.clmTotal, trid,
                    fri.clmDate, "", "", "", "FRI", fri.clmFRINo, 1, 1));

     
            context.Transection.Add(new Transection(IncomeAccID, 1, (decimal)fri.clmTotal, trid,
                fri.clmDate, "", "", "", "FRI", fri.clmFRINo, 1, 1));
        }

        internal static void InsertUpdateFromFRR(ref BEEContext context, TblFreezerRent frr)
        {
            int RetailerID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance Rental Income- Freeze").RefValue;
            int BankchargeID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank Charges").RefValue;
            var ReceivedAmount = (frr.FrrTotal) - (frr.clmBankCharge);
            var items = context.Transection.Where(x => x.DocType == "FRR" && x.DocID == frr.clmFRRNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);

            // for Cash bank Amount   95
            context.Transection.Add(new Transection(frr.clmAccountID, -1, (Decimal)ReceivedAmount, trid,
                    frr.clmRPDate, "","", "", "FRR", frr.clmFRRNo, 1, 1));

            // for bank charges   5
            context.Transection.Add(new Transection(BankchargeID, -1, (Decimal)frr.clmBankCharge, trid,
         frr.clmRPDate, "", "", "", "FRR", frr.clmFRRNo, 1, 1));

            // for total amount  100

            context.Transection.Add(new Transection(RetailerID, 1, (decimal)frr.FrrTotal, trid,
                frr.clmRPDate, "", "", "", "FRR", frr.clmFRRNo, 1, 1));
        }
        public static bool InsertUpdateFromRMSales(ref BEEContext context, RMSales rmSales, List<TblRMSalesLineItem> addedItems)
        {
            long trid = GenerateTrid(ref context);
            int salesId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Sales").RefValue;
            int rmglId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int receivabeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All RM Customer Balance").RefValue;
            int rmcogsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM COGS").RefValue;
            var isExist = context.Transection.Where(x => (x.DocType == "RMS" || x.DocType == "RMCOGS") && x.DocID == rmSales.clmRMSalesNo).ToList();
            var customer = context.CreateCustomer.FirstOrDefault(x => x.clmCustomerID == rmSales.clmCustomerID).clmCustomerName;
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(receivabeId, -1, rmSales.clmRMSTotal, trid, rmSales.clmDate.Date, rmSales.clmRefNo, customer, rmSales.clmDescription, "RMS", rmSales.clmRMSalesNo, null, null));
            context.Transection.Add(new Transection(salesId, 1, rmSales.clmRMSTotal, trid, rmSales.clmDate.Date, rmSales.clmRefNo, customer, rmSales.clmDescription, "RMS", rmSales.clmRMSalesNo, null, null));
            //for cogs 
            context.Transection.Add(new Transection(rmcogsId, -1, rmSales.clmCOGSTotal, trid, rmSales.clmDate.Date, rmSales.clmRefNo, customer, rmSales.clmDescription, "RMCOGS", rmSales.clmRMSalesNo, null, null));
            context.Transection.Add(new Transection(rmglId, 1, rmSales.clmCOGSTotal, trid, rmSales.clmDate.Date, rmSales.clmRefNo, customer, rmSales.clmDescription, "RMCOGS", rmSales.clmRMSalesNo, null, null));
            return true;
        }
        internal static bool DeleteFromRMSales(ref BEEContext context, int id)
        {
            var isExist = context.Transection.Where(x => (x.DocType == "RMS" ||x.DocType== "RMCOGS") && x.DocID == id).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }
        //public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbillentry, List<GeneralBillsLineItem> generalbillentryline)
        //{
        //    decimal totalamount = 0;
        //    long trid = GenerateTrid(ref context);
        //    //int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Vat").RefValue;
        //    int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
        //    int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
        //    var allSupplierid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
        //    var isExist = context.Transection.Where(x => x.DocType == "GBE" && x.DocID == generalbillentry.GBENo).ToList();
        //    var supplier = context.Supplier.FirstOrDefault(x => x.Id == generalbillentry.SupplierId).SupplierName;
        //    if (isExist.Count > 0)
        //    {
        //        trid = isExist.FirstOrDefault().TransectionID;
        //        context.Transection.RemoveRange(isExist);
        //    }
        //    foreach (var bill in generalbillentryline)
        //    {
        //        totalamount += bill.Amount;
        //    }
        //    context.Transection.Add(new Transection(allSupplierid, 1, totalamount + generalbillentry.VATAmount + generalbillentry.AITAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GBE", generalbillentry.GBENo, null, null));
        //    context.Transection.Add(new Transection(vatId, -1, generalbillentry.VATAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GBE", generalbillentry.GBENo, null, null));
        //    context.Transection.Add(new Transection(aitId, -1, generalbillentry.AITAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GBE", generalbillentry.GBENo, null, null));

        //    foreach (var billentry in generalbillentryline)
        //    {
        //        context.Transection.Add(new Transection(billentry.DebitAccId, -1, billentry.Amount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, billentry.Descriptions, "GBE", generalbillentry.GBENo, null, null));
        //    }
        //    return true;
        //}
        //public static bool DeleteFromGeneralBillEntry(ref BEEContext context, int generalbillno)
        //{
        //    var isExist = context.Transection.Where(x => x.DocType == "GBE" && x.DocID == generalbillno).ToList();
        //    if (isExist.Count > 0)
        //    {
        //        context.Transection.RemoveRange(isExist);
        //    }
        //    return true;
        //}

        //public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbillentry, List<GeneralBillsLineItem> generalbillentryline)
        //{
        //    decimal totalamount = 0;
        //    long trid = GenerateTrid(ref context);
        //    int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Vat").RefValue;
        //    int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
        //    var allSupplierid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
        //    var isExist = context.Transection.Where(x => x.DocType == "GSB" && x.DocID == generalbillentry.GBENo).ToList();
        //    var supplier = context.Supplier.FirstOrDefault(x => x.Id == generalbillentry.SupplierId).SupplierName;
        //    if (isExist.Count > 0)
        //    {
        //        trid = isExist.FirstOrDefault().TransectionID;
        //        context.Transection.RemoveRange(isExist);
        //    }
        //    foreach (var bill in generalbillentryline)
        //    {
        //        totalamount += bill.Amount;
        //    }
        //    context.Transection.Add(new Transection(allSupplierid, 1, totalamount + generalbillentry.VATAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GSB", generalbillentry.GBENo, generalbillentry.DepotId, null));
        //    if (generalbillentry.VATAmount > 0)
        //        context.Transection.Add(new Transection(vatId, -1, generalbillentry.VATAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GSB", generalbillentry.GBENo, generalbillentry.DepotId, null));

        //    foreach (var billentry in generalbillentryline)
        //    {
        //        context.Transection.Add(new Transection(billentry.DebitAccId, -1, billentry.Amount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, billentry.Descriptions, "GSB", generalbillentry.GBENo, generalbillentry.DepotId, billentry.CGId));
        //    }

        //    trid += 1;
        //    if (generalbillentry.AITAmount > 0)
        //    {
        //        context.Transection.Add(new Transection(aitId, 1, generalbillentry.AITAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GSB", generalbillentry.GBENo, generalbillentry.DepotId, null));
        //        context.Transection.Add(new Transection(allSupplierid, -1, generalbillentry.AITAmount, trid, generalbillentry.GBEDate.Date, generalbillentry.RefNo, supplier, "General Bill", "GSB", generalbillentry.GBENo, generalbillentry.DepotId, null));
        //    }
        //    var sq = context.Sqquencers.FirstOrDefault();
        //    sq.Refvalue = (int)trid;
        //    context.Entry<Sqquencer>(sq).State = System.Data.Entity.EntityState.Modified;

        //    return true;
        //}



        //public static bool DeleteFromGeneralBillEntry(ref BEEContext context, int generalbillno)
        //{
        //    var isExist = context.Transection.Where(x => x.DocType == "GSB" && x.DocID == generalbillno).ToList();
        //    if (isExist.Count > 0)
        //    {
        //        context.Transection.RemoveRange(isExist);
        //    }
        //    return true;
        //}

        public static bool InsertFromRMCustomer(ref BEEContext context, CreateCustomer customer)
        {
            string payeeName = customer.clmCustomerName;
            int obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All RM Customer Balance").RefValue;
            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;

            var items = context.Transection.Where(x => x.DocType == "RMCOB" && x.DocID == customer.clmCustomerID).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            context.Transection.Add(new Transection(accId, -1, (Decimal)customer.clmAccOB, trid,
                    customer.clmAccOBDate, "", "OBE", "OBE", "RMCOB", customer.clmCustomerID, null, null));

            context.Transection.Add(new Transection(obeId, 1, (decimal)customer.clmAccOB, trid,
                customer.clmAccOBDate, "", "OBE", "OBE", "RMCOB", customer.clmCustomerID, null, null));

            return true;
        }

        internal static void InsertFromRetailerInformation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var items = context.Transection.Where(x => x.DocType == retailer.DocType && x.DocID == retailer.Id).ToList();
            int RetailerID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance Rental Income- Freeze").RefValue;
            int OBEID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            var date = context.CompanySetup.FirstOrDefault(x => x.ID == 1).StartDate;

            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }

            int trid = GenerateTrid(ref context);
            context.Transection.Add(new Transection(OBEID, -1, (Decimal)retailer.OB, trid,
                    date, "", retailer.RetailerName, "", retailer.DocType, retailer.Id, 1, 1));

            context.Transection.Add(new Transection(RetailerID, 1, (decimal)retailer.OB, trid,
                date, "", retailer.RetailerName, "", retailer.DocType, retailer.Id, 1, 1));
        }
        internal static bool DeleteFromRetailerInformation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var items = context.Transection.Where(x => x.DocType == retailer.DocType && x.DocID == retailer.Id).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }
        public static bool InsertUpdateFromGeneralBillEntry(ref BEEContext context, GeneralBill generalbill, List<GeneralBillsLineItem> generalbillentryline)
        {
            long trid = GenerateTrid(ref context);
            int psId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            var allSupplierid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue;
            var isExist = context.Transection.Where(x => (x.DocType == "GSB" || x.DocType == "GSBD" || x.DocType == "GBVAT" || x.DocType == "GBVDS" || x.DocType == "GBAIT") && x.DocID == generalbill.GBENo).ToList();
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == generalbill.SupplierId).SupplierName;
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(allSupplierid, 1, generalbill.GBETotal, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "GSB", "GSB", generalbill.GBENo, generalbill.DepotId , null));
            foreach (var generalBillsLine in generalbillentryline)
            {
                context.Transection.Add(new Transection(generalBillsLine.DebitAccId, -1, generalBillsLine.Amount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "", "GSB", generalbill.GBENo, generalbill.DepotId, generalBillsLine.CGId));
            }  
            if (generalbill.Discountamt > 0)
            {
                context.Transection.Add(new Transection(discount, 1, generalbill.Discountamt, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "Discount", "GSBD", generalbill.GBENo, generalbill.DepotId, null));
                context.Transection.Add(new Transection(allSupplierid, -1, generalbill.Discountamt, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "Discount", "GSBD", generalbill.GBENo, generalbill.DepotId, null));
            }
            if (generalbill.VATAmount > 0)
            {
                context.Transection.Add(new Transection(vatId, -1, generalbill.VATAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "VAT", "GBVAT", generalbill.GBENo, generalbill.DepotId, null));
                context.Transection.Add(new Transection(allSupplierid, 1, generalbill.VATAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "VAT", "GBVAT", generalbill.GBENo, generalbill.DepotId, null));
            }
            if (generalbill.AITAmount > 0)
            {
                context.Transection.Add(new Transection(aitId, 1, generalbill.AITAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "AIT", "GBAIT", generalbill.GBENo, generalbill.DepotId, null));
                context.Transection.Add(new Transection(allSupplierid, -1, generalbill.AITAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "AIT", "GBAIT", generalbill.GBENo, generalbill.DepotId, null));
            }
            if (generalbill.VDSAmount > 0)
            {
                context.Transection.Add(new Transection(vdsId, 1, generalbill.VDSAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "VDS", "GBVDS", generalbill.GBENo, generalbill.DepotId, null));
                context.Transection.Add(new Transection(allSupplierid, -1, generalbill.VDSAmount, trid, generalbill.GBEDate.Date, generalbill.RefNo, supplier, "VDS", "GBVDS", generalbill.GBENo, generalbill.DepotId, null));
            }
            return true;
        }
        public static bool DeleteFromGeneralBillEntry(ref BEEContext context, int billNo)
        {
            var isExist = context.Transection.Where(x => (x.DocType == "GSB" || x.DocType == "GSBD" || x.DocType == "GBVAT" || x.DocType == "GBVDS" || x.DocType == "GBAIT") && x.DocID == billNo).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }
        public static bool InsertFromPaymentVoucher(ref BEEContext context, ReceivePaymentInfo paymentInfo)
        {
            string payeeName = context.Customers.FirstOrDefault(x => x.Id == paymentInfo.CustomerID).CustomerName;
            int bankCharge = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank Charges").RefValue;
            int allCustomer = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;
            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            var items = context.Transection.Where(x => x.DocType == "CRV" && x.DocID == paymentInfo.RPID).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            if (paymentInfo.BankCharges > 0)
            {
                context.Transection.Add(new Transection(paymentInfo.AccountID, -1, paymentInfo.NetAmount, trid,
                    paymentInfo.TDate, paymentInfo.RefNo, payeeName, paymentInfo.Description, "CRV", paymentInfo.RPID, null, null));

                context.Transection.Add(new Transection(bankCharge, -1, paymentInfo.BankCharges, trid,
                    paymentInfo.TDate, paymentInfo.RefNo, payeeName, paymentInfo.Description, "CRV", paymentInfo.RPID, null, null));

                context.Transection.Add(new Transection(allCustomer, 1, paymentInfo.ReceiveAmt, trid,
                    paymentInfo.TDate, paymentInfo.RefNo, payeeName, paymentInfo.Description, "CRV", paymentInfo.RPID, null, null));

            }
            else
            {
                context.Transection.Add(new Transection(paymentInfo.AccountID, -1, paymentInfo.ReceiveAmt, trid,
                   paymentInfo.TDate, paymentInfo.RefNo, payeeName, paymentInfo.Description, "CRV", paymentInfo.RPID, null, null));

                context.Transection.Add(new Transection(allCustomer, 1, paymentInfo.ReceiveAmt, trid,
                    paymentInfo.TDate, paymentInfo.RefNo, payeeName, paymentInfo.Description, "CRV", paymentInfo.RPID, null, null));
            }

            return true;
        }
        public static bool InsertUpdateFromRMCBatch(ref BEEContext context, RMCEntry rmc, List<RMCLineItem> rmclineitem)
        {
            decimal totalamount = 0;
            foreach (var item in rmclineitem)
            {
                totalamount += item.TotalStanCost;
            }
            long trid = GenerateTrid(ref context);
            int apcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int wipid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of All Batches").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "RMC" && x.DocID == rmc.RMCNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(apcaid, 1, Convert.ToDecimal(totalamount), trid, rmc.RMCDate, rmc.RefNo, " ", rmc.Descriptions, "RMC", rmc.RMCNo, null, null));
            context.Transection.Add(new Transection(wipid, -1, Convert.ToDecimal(totalamount), trid, rmc.RMCDate, rmc.RefNo, " ", rmc.Descriptions, "RMC", rmc.RMCNo, null, null));
            return true;
        }


        public static bool DeleteFromRMCBatch(ref BEEContext context, int rmcid)
        {
            var isExist = context.Transection.Where(x => x.DocType == "RMC" && x.DocID == rmcid).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }

        public static bool InsertUpdateFromARMCBatch(ref BEEContext context, RMCApplyEntry rmc, List<RMCApplyLineItem> rmclineitem)
        {
            decimal totalamount = 0;
            foreach (var item in rmclineitem)
            {
                totalamount += item.RmcValue;
            }
            long trid = GenerateTrid(ref context);
            int apcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int wipid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of All Batches").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "ARMC" && x.DocID == rmc.RMCNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(apcaid, 1, Convert.ToDecimal(totalamount), trid, rmc.RMCDate, rmc.RefNo, " ", rmc.Descriptions, "ARMC", rmc.RMCNo, null, null));
            context.Transection.Add(new Transection(wipid, -1, Convert.ToDecimal(totalamount), trid, rmc.RMCDate, rmc.RefNo, " ", rmc.Descriptions, "ARMC", rmc.RMCNo, null, null));
            return true;
        }


        public static bool DeleteFromARMCBatch(ref BEEContext context, int rmcid)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ARMC" && x.DocID == rmcid).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }

        public static bool DeleteFromGRN(ref BEEContext context, int id)
        {
            var isExist = context.Transection.Where(x => x.DocType == "GRN" && x.DocID == id).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }

        public static bool InsertUpdateFromFGP(ref BEEContext context, FGProduction FGP, List<FGProductionLineItem> FGPL)
        {
            decimal totalamount = 0;
            foreach (var item in FGPL)
            {
                totalamount += item.CogsTotal;
            }
            long trid = GenerateTrid(ref context);
            int drAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            int crAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All COGS").RefValue;

            var isExist = context.Transection.Where(x => x.DocType == "FGP" && x.DocID == FGP.FGPNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(crAcc, 1, Convert.ToDecimal(totalamount), trid, FGP.FGPDate, FGP.RefNo, " ", FGP.Descriptions, "FGP", FGP.FGPNo, null, null));
            context.Transection.Add(new Transection(drAcc, -1, Convert.ToDecimal(totalamount), trid, FGP.FGPDate, FGP.RefNo, " ", FGP.Descriptions, "FGP", FGP.FGPNo, null, null));
            return true;
        }

        public static bool InsertUpdateFromMDSI(ref BEEContext context, MDSI mdsi, List<MDSILineItem> addedItems)
        {
      
            long trid = GenerateTrid(ref context);
            int drcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Exp").RefValue;
            int crcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Payable").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "MDSI" && x.DocID == mdsi.MDSINO).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(crcaid, 1, mdsi.TotalAmount, trid, mdsi.MDSIDate, mdsi.RefNo, " ", mdsi.Description, "MDSI", mdsi.MDSINO, null, null));
            context.Transection.Add(new Transection(drcaid, -1, mdsi.TotalAmount, trid, mdsi.MDSIDate, mdsi.RefNo, " ", mdsi.Description, "MDSI", mdsi.MDSINO, null, null));
            return true;
        }
        public static bool InsertUpdateFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dsia, List<DSIAdjustmentLineItem> addedItems)
        {

            long trid = GenerateTrid(ref context);
            int crcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            int drcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Payable").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "DSIA" && x.DocID == dsia.DSIANo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(crcaid, 1, dsia.TotalAmount, trid, dsia.DSIADate, dsia.RefNo, " ", dsia.Description, "DSIA", dsia.DSIANo, null, null));
            context.Transection.Add(new Transection(drcaid, -1, dsia.TotalAmount, trid, dsia.DSIADate, dsia.RefNo, " ", dsia.Description, "DSIA", dsia.DSIANo, null, null));
            return true;
        }
        public static bool DeleteFromMDSI(ref BEEContext context, MDSI mdsi)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MDSI" && x.DocID == mdsi.MDSINO).ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertUpdateFromPaymentRefundSupplier(ref BEEContext context, PRS pRS)
        {

            long trid = GenerateTrid(ref context);
            int crcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            //int drcaid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Payable").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "PRFS" && x.DocID == pRS.PRSNo).ToList();
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == pRS.SupplierID);
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(crcaid, 1, pRS.ReturnAmount, trid, pRS.Date, pRS.RefNo, supplier.SupplierName, pRS.Description, "PRFS", pRS.PRSNo, 1, 1));
            context.Transection.Add(new Transection(pRS.ReceiveAccountID, -1, pRS.ReturnAmount, trid, pRS.Date, pRS.RefNo, supplier.SupplierName, pRS.Description, "PRFS", pRS.PRSNo, 1, 1));
            return true;
        }
        public static bool DeleteFromPaymentRefundSupplier(ref BEEContext context, int id)
        {
            var isExist = context.Transection.Where(x => x.DocType == "PRFS" && x.DocID == id).ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool DeleteFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dsia)
        {
            var isExist = context.Transection.Where(x => x.DocType == "DSIA" && x.DocID == dsia.DSIANo).ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertUpdateFromDSIA(ref BEEContext context, DSIA dsia, List<DSIALineItem> Line)
        {
            decimal totalamount = 0;
            foreach (var item in Line)
            {
                totalamount += item.COGSValue;
            }
            long trid = GenerateTrid(ref context);
            int sia = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of Sample Goods").RefValue;
            int dia = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of Damage Goods").RefValue;
            int dsa = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of Dumping Adjustment").RefValue;
            int offer = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Cost of Offer Goods").RefValue;
            int inventory = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == dsia.Type && x.DocID == dsia.DSIANo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            if(dsia.Type == "SIA")
            {
                context.Transection.Add(new Transection(inventory, 1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "SIA", dsia.DSIANo, null, null));
                context.Transection.Add(new Transection(sia, -1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "SIA", dsia.DSIANo, null, null));
            }
            else if(dsia.Type == "DIA")
            {
                context.Transection.Add(new Transection(inventory, 1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "DIA", dsia.DSIANo, null, null));
                context.Transection.Add(new Transection(dia, -1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "DIA", dsia.DSIANo, null, null));
            }
            else if (dsia.Type == "DSA")
            {
                context.Transection.Add(new Transection(inventory, 1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "DSA", dsia.DSIANo, null, null));
                context.Transection.Add(new Transection(dsa, -1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "DSA", dsia.DSIANo, null, null));
            }
            else
            {
                context.Transection.Add(new Transection(inventory, 1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "Offer", dsia.DSIANo, null, null));
                context.Transection.Add(new Transection(offer, -1, Convert.ToDecimal(totalamount), trid, dsia.DSIADate, dsia.Ref, " ", dsia.Description, "Offer", dsia.DSIANo, null, null));
            }
            
            //context.Transection.Add(new Transection(wipid, -1, Convert.ToDecimal(totalamount), trid, FGP.FGPDate, FGP.RefNo, " ", FGP.Descriptions, "FGP", FGP.FGPNo, null, null));
            return true;
        }

        public static bool DeleteFromDSIA(ref BEEContext context, DSIA dsia)
        {
            var isExist = context.Transection.Where(x => x.DocType == dsia.Type && x.DocID == dsia.DSIANo).ToList();
            if(isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }
            public static bool InsertFromCustomer(ref BEEContext context, Customer customer)
        {
            string payeeName = customer.CustomerName;
            int obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            int allCustomer = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;

            var items = context.Transection.Where(x => x.DocType == "COB" && x.DocID == customer.Id).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }

            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            var compSetUp = context.CompanySetup.FirstOrDefault();
            context.Transection.Add(new Transection(allCustomer, -1, (Decimal)customer.OB, trid,
                    compSetUp.StartDate, "", "OBE", "OBE", "COB", customer.Id, null, null));

            context.Transection.Add(new Transection(obeId, 1, (decimal)customer.OB, trid,
                compSetUp.StartDate, "", "OBE", "OBE", "COB", customer.Id, null, null));

            return true;
        }

        internal static void InsertFromSupplierPayment(ref BEEContext context, PayBillInfo spvoucher, List<PayBillLine> addedItems)
        {
            var items = context.Transection.Where(x => x.DocType == "SPV" && x.DocID == spvoucher.PBID).ToList();
            int allSupplier = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == spvoucher.SupplierId);
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);
            context.Transection.Add(new Transection(allSupplier, -1, (Decimal)spvoucher.Amount, trid,
                    spvoucher.TDate, spvoucher.RefNo, supplier.SupplierName, spvoucher.Description, "SPV", spvoucher.PBID, 1, 1));

            context.Transection.Add(new Transection(spvoucher.PaymentAccId, 1, (decimal)spvoucher.Amount, trid,
                spvoucher.TDate, spvoucher.RefNo, supplier.SupplierName, spvoucher.Description, "SPV", spvoucher.PBID, 1, 1));
        }
        internal static bool DeleteFromSupplierPayment(ref BEEContext context, PayBillInfo spvoucher)
        {
            var items = context.Transection.Where(x => x.DocType == "SPV" && x.DocID == spvoucher.PBID).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }

        internal static void InsertUpdateFromSAAD(ref BEEContext context, SupplierAcAdjustment supplierAcAdjustment)
        {
            var items = context.Transection.Where(x => x.DocType == "SAAD" && x.DocID == supplierAcAdjustment.SAADNo).ToList();
            int allSupplier = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == supplierAcAdjustment.SupplierId);
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            int trid = GenerateTrid(ref context);
            context.Transection.Add(new Transection(allSupplier, -1, (Decimal)supplierAcAdjustment.Amount, trid,
                    supplierAcAdjustment.Date, supplierAcAdjustment.RefNo, supplier.SupplierName, supplierAcAdjustment.Description, "SAAD", supplierAcAdjustment.SAADNo, 1, 1));

            context.Transection.Add(new Transection(supplierAcAdjustment.LedgerAcId, 1, (decimal)supplierAcAdjustment.Amount, trid,
                supplierAcAdjustment.Date, supplierAcAdjustment.RefNo, supplier.SupplierName, supplierAcAdjustment.Description, "SAAD", supplierAcAdjustment.SAADNo, 1, 1));
        }
        internal static bool DeleteFromSupplierAccountAdjestment(ref BEEContext context, SupplierAcAdjustment supplierAcAdjustment)
        {
            var items = context.Transection.Where(x => x.DocType == "SAAD" && x.DocID == supplierAcAdjustment.SAADNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }

        public static bool InsertFromFGOB(ref BEEContext context, ChartOfInvFGOB chartOfInvOb, decimal standardCost)
        {
            int obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            int glId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;
            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            var compSetUp = context.CompanySetup.FirstOrDefault();


            var items = context.Transection.Where(x => x.DocType == "FGOB" && x.DocID == chartOfInvOb.FGOBNO).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            context.Transection.Add(new Transection(glId, -1, standardCost, trid,
                    compSetUp.StartDate, "", "OBE", "FGOB", "FGOB", chartOfInvOb.FGOBNO, null, null));

            context.Transection.Add(new Transection(obeId, 1, (decimal)standardCost, trid,
                compSetUp.StartDate, "", "OBE", "FGOB", "FGOB", chartOfInvOb.FGOBNO, null, null));
            return true;
        }

        internal static void InsertFromSalesReturn(ref BEEContext context, SalesReturnEntry salesReturn)
        {
            int supplier = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            int glId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            int cogs = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All COGS").RefValue;
            int sales = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Sales").RefValue;

            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;
            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            var compSetUp = context.CompanySetup.FirstOrDefault();
            decimal standardCost = salesReturn.TotalCOGS;

            var items = context.Transection.Where(x => x.DocType == "SalesReturn" || x.DocType == "COGS" && x.DocID == salesReturn.SRNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            context.Transection.Add(new Transection(sales, -1, salesReturn.TotalRtnAmount, trid,
                    salesReturn.SRDate, "", "", "Sales Return", "SalesReturn", salesReturn.SRNo, null, null));

            context.Transection.Add(new Transection(supplier, 1, salesReturn.TotalRtnAmount, trid,
                salesReturn.SRDate, "", "", "Sales Return", "SalesReturn", salesReturn.SRNo, null, null));

            context.Transection.Add(new Transection(glId, -1, standardCost, trid,
                    salesReturn.SRDate, "", "", "Cost of goods sold", "COGS", salesReturn.SRNo, null, null));

            context.Transection.Add(new Transection(cogs, 1, (decimal)standardCost, trid,
                salesReturn.SRDate, "", "", "Cost of goods sold", "COGS", salesReturn.SRNo, null, null));

        }
        
        public static bool InsertUpdateFromGoodPurchaseBill(ref BEEContext context, PurchaseEntry purchasebill)
        {
            long trid = GenerateTrid(ref context);
            int psId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Provision for Supplier").RefValue;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            var allSupplierid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue;
            var isExist = context.Transection.Where(x => (x.DocType == "GPB" || x.DocType == "GPBT" || x.DocType == "GPBD" || x.DocType == "GPBV") && x.DocID == purchasebill.BillNo).ToList();
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == purchasebill.SupplierId).SupplierName;
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(psId, -1, purchasebill.BillTotalValue, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPB", purchasebill.BillNo, null, null));
            context.Transection.Add(new Transection(allSupplierid, 1, purchasebill.BillTotalValue, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPB", purchasebill.BillNo, null, null));
            if (purchasebill.VatAmount > 0) { 
                context.Transection.Add(new Transection(vatId, -1, purchasebill.VatAmount, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBV", purchasebill.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, 1, purchasebill.VatAmount, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBV", purchasebill.BillNo, null, null));
            }
            if (purchasebill.AitAmount > 0) { 
                context.Transection.Add(new Transection(aitId, 1, purchasebill.AitAmount, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBT", purchasebill.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, purchasebill.AitAmount , trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBT", purchasebill.BillNo, null, null));
            }
            if (purchasebill.DiscountAmt > 0) { 
                context.Transection.Add(new Transection(discount, 1, purchasebill.DiscountAmt, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBD", purchasebill.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, purchasebill.DiscountAmt, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "GPBD", purchasebill.BillNo, null, null));
            }
            if (purchasebill.VDAAmnt > 0)
            {
                context.Transection.Add(new Transection(vdsId, 1, purchasebill.VDAAmnt, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "SVDA", purchasebill.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, purchasebill.VDAAmnt, trid, purchasebill.PDate.Date, purchasebill.PurRef, supplier, purchasebill.PurDescription, "SVDA", purchasebill.BillNo, null, null));
            }

            return true;
        }

        public static bool InsertUpdateFromFreezerPurchaseBill(ref BEEContext context, FPB fpb)
        {
            long trid = GenerateTrid(ref context);
            int caId = fpb.DebtAccID;
            int vatId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Control Account").RefValue;
            int vdsId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Vat Deducted at Source (VDS)").RefValue;
            int aitId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "AIT of Suppliers Payable").RefValue;
            var allSupplierid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            int discount = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Purchase Discount").RefValue;
            var isExist = context.Transection.Where(x => (x.DocType == "FPB" || x.DocType == "FPBVT" || x.DocType == "FPBDIS" || x.DocType == "FPBVDS"||x.DocType== "FPBAIT") && x.DocID == fpb.BillNo).ToList();
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == fpb.SupplierId).SupplierName;
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(caId, -1, fpb.BillTotalValue, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPB", fpb.BillNo, null, null));
            context.Transection.Add(new Transection(allSupplierid, 1, fpb.BillTotalValue, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPB", fpb.BillNo, null, null));
            if (fpb.VatAmount > 0)
            {
                context.Transection.Add(new Transection(vatId, -1, fpb.VatAmount, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBVT", fpb.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, 1, fpb.VatAmount, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBVT", fpb.BillNo, null, null));
            }
            if (fpb.AitAmount > 0)
            {
                context.Transection.Add(new Transection(aitId, 1, fpb.AitAmount, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBAIT", fpb.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, fpb.AitAmount, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBAIT", fpb.BillNo, null, null));
            }
            if (fpb.DiscountAmt > 0)
            {
                context.Transection.Add(new Transection(discount, 1, fpb.DiscountAmt, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBDIS", fpb.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, fpb.DiscountAmt, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBDIS", fpb.BillNo, null, null));
            }
            if (fpb.VDAAmnt > 0)
            {
                context.Transection.Add(new Transection(vdsId, 1, fpb.VDAAmnt, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBVDS", fpb.BillNo, null, null));
                context.Transection.Add(new Transection(allSupplierid, -1, fpb.VDAAmnt, trid, fpb.PDate.Date, fpb.PurRef, supplier, fpb.PurDescription, "FPBVDS", fpb.BillNo, null, null));
            }

            return true;
        }

        //public static bool InsertFromEmployee(ref BEEContext context, Employee employee)
        //{
        //    int obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
        //    int glId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Employee Balance").RefValue;
        //    var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
        //    int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;
        //    context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
        //    var compSetUp = context.CompanySetup.FirstOrDefault();

        //    var items = context.Transection.Where(x => x.DocType == "EmpOB" && x.DocID == employee.Id).ToList();
        //    foreach (var item in items)
        //    {
        //        context.Transection.Remove(item);
        //    }
        //    context.Transection.Add(new Transection(glId, 1, Convert.ToDecimal(employee.OpBalance), trid,
        //            compSetUp.StartDate, "", "OBE", "OBE", "EmpOB", employee.Id, null, null));
        //    context.Transection.Add(new Transection(obeId, -1, Convert.ToDecimal(employee.OpBalance), trid,
        //        compSetUp.StartDate, "", "OBE", "OBE", "EmpOB", employee.Id, null, null));
        //    return true;
        //}
        //public static bool InsertFromSales(ref BEEContext context, SalesEntryNew salesEntry, int? customerId)
        //{
        //    string payeeName = "";
        //    if (customerId == null)
        //    {
        //        payeeName = context.Customers.FirstOrDefault(x => x.Id == salesEntry.CustomerID).CustomerName;
        //    }
        //    else
        //    {
        //        payeeName = context.Customers.FirstOrDefault(x => x.Id == customerId).CustomerName;
        //    }


        //    int saleAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "all Sales").RefValue;
        //    int allCustomer = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
        //    int vatAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Vat").RefValue;
        //    int discountAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Discount").RefValue;
        //    int cogsAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All COGS").RefValue;
        //    int invAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;

        //    var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
        //    int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;

        //    context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;

        //    var items = context.Transection.Where(x =>( x.DocType == "Trade Discount" || x.DocType == "Sales"||x.DocType== "COGS" )&& x.DocID == salesEntry.InvoiceNo).ToList();

        //    foreach (var item in items)
        //    {
        //        context.Transection.Remove(item);
        //    }
        //    if (salesEntry.InvoiceAmount > 0)
        //    {
        //        context.Transection.Add(new Transection(saleAcc, 1, salesEntry.InvoiceAmount, trid,
        //            salesEntry.SalesDate, "", payeeName, "Sales", "Sales", salesEntry.InvoiceNo, null, null));

        //        context.Transection.Add(new Transection(allCustomer, -1, salesEntry.InvoiceAmount, trid,
        //            salesEntry.SalesDate, "", payeeName, "Sales", "Sales", salesEntry.InvoiceNo, null, null));
        //    }
        //    if (salesEntry.DiscountAmt > 0)
        //    {
        //        trid += 1;
        //        context.Transection.Add(new Transection(allCustomer, 1, salesEntry.DiscountAmt, trid,
        //            salesEntry.SalesDate, "", payeeName, "CTD", "Trade Discount", salesEntry.InvoiceNo, null, null));

        //        context.Transection.Add(new Transection(discountAcc, -1, salesEntry.DiscountAmt, trid,
        //            salesEntry.SalesDate, "", payeeName, "CTD", "Trade Discount", salesEntry.InvoiceNo, null, null));
        //    }
        //    //if (salesEntry.VatAmount > 0)
        //    //{
        //    //    trid += 1;
        //    //    context.Transection.Add(new Transection(vatAcc, 1, salesEntry.VatAmount, trid,
        //    //        salesEntry.SalesDate, "", payeeName, "CTV", "Trade Vat", salesEntry.InvoiceNo, null, null));

        //    //    context.Transection.Add(new Transection(allCustomer, -1, salesEntry.VatAmount, trid,
        //    //        salesEntry.SalesDate, "", payeeName, "CTV", "Trade Vat", salesEntry.InvoiceNo, null, null));
        //    //}
        //    decimal cost = salesEntry.clmCOGSTotal;
        //    trid += 1;

        //    context.Transection.Add(new Transection(cogsAcc, -1, cost, trid,
        //        salesEntry.SalesDate, "", payeeName, "Cost of good sold", "COGS", salesEntry.InvoiceNo, null, null));

        //    context.Transection.Add(new Transection(invAcc, 1, cost, trid,
        //        salesEntry.SalesDate, "", payeeName, "Cost of good sold", "COGS", salesEntry.InvoiceNo, null, null));
        //    return true;
        //}
        public static bool InsertFromSales(ref BEEContext context, SalesEntryNew salesEntry)
        {
            string payeeName = "";
          
            payeeName = context.Customers.FirstOrDefault(x => x.Id == salesEntry.CustomerID).CustomerName;

            int saleAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Sales").RefValue;
            int allCustomer = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            int comAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Commission").RefValue;
            int discountAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Discount").RefValue;
            int cogsAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All COGS").RefValue;
            int invAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;

            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid =sqquencer.Refvalue + 1;         

            var items = context.Transection.Where(x =>  (x.DocType == "Sales" || x.DocType == "DTD" || x.DocType == "DTC") && x.DocID == salesEntry.InvoiceNo).ToList();
            var netInvoice = salesEntry.InvoiceAmount;
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            if (salesEntry.InvoiceAmount > 0)
            {
                context.Transection.Add(new Transection(saleAcc, 1, salesEntry.InvoiceAmount, trid,
                    salesEntry.SalesDate, "", payeeName, "Sales", "Sales", salesEntry.InvoiceNo, null, null));
                context.Transection.Add(new Transection(allCustomer, -1, salesEntry.InvoiceAmount, trid,
               salesEntry.SalesDate, "", payeeName, "", "Sales", salesEntry.InvoiceNo, null, null));

                //context.Transection.Add(new Transection(allCustomer, -1, salesEntry.InvoiceAmount, trid,
                //    salesEntry.SalesDate, "", payeeName, "Sales", "Sales", salesEntry.InvoiceNo, null, null));
                
            }
            if (salesEntry.DiscountAmt > 0)
            {
                trid += 1;
                netInvoice = netInvoice - salesEntry.DiscountAmt;
                //context.Transection.Add(new Transection(allCustomer, 1, salesEntry.DiscountAmt, trid,
                //    salesEntry.SalesDate, "", payeeName, "CTD", "Trade Discount", salesEntry.InvoiceNo, null, null));

                context.Transection.Add(new Transection(discountAcc, -1, salesEntry.DiscountAmt, trid,
                    salesEntry.SalesDate, "", payeeName, "CTD", "DTD", salesEntry.InvoiceNo, null, null));
                context.Transection.Add(new Transection(allCustomer, 1, salesEntry.DiscountAmt, trid,
                   salesEntry.SalesDate, "", payeeName, "CTD", "DTD", salesEntry.InvoiceNo, null, null));
            }
            if (salesEntry.CommissionAmt > 0)
            {
                trid += 1;
                netInvoice = netInvoice - salesEntry.CommissionAmt;
                //context.Transection.Add(new Transection(allCustomer, 1, salesEntry.DiscountAmt, trid,
                //    salesEntry.SalesDate, "", payeeName, "CTD", "Trade Discount", salesEntry.InvoiceNo, null, null));

                context.Transection.Add(new Transection(comAcc, -1, salesEntry.CommissionAmt, trid,
                    salesEntry.SalesDate, "", payeeName, "CTD", "DTC", salesEntry.InvoiceNo, null, null));
                context.Transection.Add(new Transection(allCustomer, 1, salesEntry.CommissionAmt, trid,
                 salesEntry.SalesDate, "", payeeName, "CTD", "DTC", salesEntry.InvoiceNo, null, null));
            }

            if(salesEntry.clmCOGSTotal > 0)
            {
                trid += 1;

                context.Transection.Add(new Transection(cogsAcc, -1, salesEntry.clmCOGSTotal, trid,
               salesEntry.SalesDate, "", payeeName, "", "COGS", salesEntry.InvoiceNo, null, null));
                context.Transection.Add(new Transection(invAcc, 1, salesEntry.clmCOGSTotal, trid,
               salesEntry.SalesDate, "", payeeName, "", "COGS", salesEntry.InvoiceNo, null, null));

            }
            sqquencer.Refvalue = trid;
            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            return true;
        }

        public static bool DeleteFromSales(ref BEEContext context, SalesEntryNew salesEntry, int? customerId)
        {
            var items = context.Transection.Where(x => (x.DocType == "Sales" || x.DocType == "DTD" || x.DocType == "DTC") && x.DocID == salesEntry.InvoiceNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }

            return true;

        }

        public static bool InsertFromRMOB(ref BEEContext context, ChartOfInvRMOB chartOfInvOb, decimal standardCost)
        {
            int obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            int glId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            var sqquencer = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int trid = sqquencer.Refvalue = sqquencer.Refvalue + 1;
            context.Entry<Sqquencer>(sqquencer).State = System.Data.Entity.EntityState.Modified;
            var compSetUp = context.CompanySetup.FirstOrDefault();

            var items = context.Transection.Where(x => x.DocType == "RMOB" && x.DocID == chartOfInvOb.RMOBNO).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            context.Transection.Add(new Transection(glId, -1, standardCost, trid,
                    compSetUp.StartDate, "", "OBE", "RMOB", "RMOB", chartOfInvOb.RMOBNO, null, null));

            context.Transection.Add(new Transection(obeId, 1, (decimal)standardCost, trid,
                compSetUp.StartDate, "", "OBE", "RMOB", "RMOB", chartOfInvOb.RMOBNO, null, null));
            return true;
        }

        #region Accounts Module

        public static bool InsertUpdateFromReceiveVoucher(ref BEEContext context, ReceiveVoucher receiveVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "RV" && x.DocID == receiveVoucher.RVNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                context.Transection.Add(new Transection(receiveVoucher.ReceiveAccountID, -1, receiveVoucher.RVAmount, (int)isExist[0].TransectionID, receiveVoucher.RVDate, receiveVoucher.RefNo, receiveVoucher.PayeeName, receiveVoucher.Description, "RV", receiveVoucher.RVNo, 1, 1));
                context.Transection.Add(new Transection(receiveVoucher.CreditAccountID, 1, receiveVoucher.RVAmount, (int)isExist[0].TransectionID, receiveVoucher.RVDate, receiveVoucher.RefNo, receiveVoucher.PayeeName, receiveVoucher.Description, "RV", receiveVoucher.RVNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(receiveVoucher.ReceiveAccountID, -1, receiveVoucher.RVAmount, tridNo, receiveVoucher.RVDate, receiveVoucher.RefNo, receiveVoucher.PayeeName, receiveVoucher.Description, "RV", receiveVoucher.RVNo, 1, 1));
                context.Transection.Add(new Transection(receiveVoucher.CreditAccountID, 1, receiveVoucher.RVAmount, tridNo, receiveVoucher.RVDate, receiveVoucher.RefNo, receiveVoucher.PayeeName, receiveVoucher.Description, "RV", receiveVoucher.RVNo, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromReceiveVoucher(ref BEEContext context, ReceiveVoucher receiveVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "RV" && x.DocID == receiveVoucher.RVNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }

        public static bool InsertUpdateFromJournalVoucher(ref BEEContext context, JournalVoucherOne journalVoucher, List<JournalVoucherOneLineItem> lineItems, List<JVSubLedgerLine> subledger)
        {
            var isExist = context.Transection.Where(x => x.DocType == "JV" && x.DocID == journalVoucher.JVNO).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                //var subLedgers = context.JVSubLedgerLine.Where(x => x.JVNO == journalVoucher.JVNO);
                //foreach(var sub in subLedgers)
                //{
                //    context.JVSubLedgerLine.Remove(sub);
                //}
                foreach (var item in lineItems)
                {
                    if (item.DebOrCre == "Debit")
                    {
                        context.Transection.Add(new Transection(item.AccountHeadID, -1, item.JVAmount, (int)isExist[0].TransectionID, journalVoucher.JVDate, null, "Debit", item.Description, "JV", journalVoucher.JVNO, journalVoucher.BranchId, item.CostGroupId));
                    }
                    else
                    {
                        context.Transection.Add(new Transection(item.AccountHeadID, 1, item.JVAmount, (int)isExist[0].TransectionID, journalVoucher.JVDate, null, "Credit", item.Description, "JV", journalVoucher.JVNO, journalVoucher.BranchId, item.CostGroupId));
                    }
                }
                //if(subledger!=null)
                //{

                //    foreach (var item in subledger)
                //    {
                //        item.JVNO = journalVoucher.JVNO;
                //        context.JVSubLedgerLine.Add(item);
                //    }
                //}

            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                foreach (var item in lineItems)
                {
                    if (item.DebOrCre == "Debit")
                    {
                        context.Transection.Add(new Transection(item.AccountHeadID, -1, item.JVAmount, tridNo, journalVoucher.JVDate, null, "Debit", item.Description, "JV", journalVoucher.JVNO, journalVoucher.BranchId, item.CostGroupId));
                    }
                    else
                    {
                        context.Transection.Add(new Transection(item.AccountHeadID, 1, item.JVAmount, tridNo, journalVoucher.JVDate, null, "Credit", item.Description, "JV", journalVoucher.JVNO, journalVoucher.BranchId, item.CostGroupId));
                    }
                }
                //if (subledger != null)
                //{

                //    foreach (var item in subledger)
                //    {
                //        item.JVNO = journalVoucher.JVNO;
                //        context.JVSubLedgerLine.Add(item);
                //    }
                //}
            }

            return true;
        }
        public static bool DeleteFromJournalVoucher(ref BEEContext context, JournalVoucherOne journalVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "JV" && x.DocID == journalVoucher.JVNO).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                var subLedger = context.JVSubLedgerLine.Where(x => x.JVNO == journalVoucher.JVNO).ToList();
                foreach (var sub in subLedger)
                {
                    context.JVSubLedgerLine.Remove(sub);
                }
                return true;
            }
            return false;
        }

        public static bool InsertUpdateFromFTVVoucher(ref BEEContext context, FundTransferVoucher fundTrancferVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "FTV" && x.DocID == fundTrancferVoucher.TransferId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                context.Transection.Add(new Transection(fundTrancferVoucher.TransFrom, 1, fundTrancferVoucher.TransAmount, (int)isExist[0].TransectionID, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
                context.Transection.Add(new Transection(fundTrancferVoucher.TransTo, -1, fundTrancferVoucher.TransAmount, (int)isExist[0].TransectionID, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(fundTrancferVoucher.TransFrom, 1, fundTrancferVoucher.TransAmount, tridNo, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
                context.Transection.Add(new Transection(fundTrancferVoucher.TransTo, -1, fundTrancferVoucher.TransAmount, tridNo, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromFTVVoucher(ref BEEContext context, FundTransferVoucher fundTrancferVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "FTV" && x.DocID == fundTrancferVoucher.TransferId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }

        public static bool InsertUpdateFromMoneyRequisitionVoucher(ref BEEContext context, MoneyRequisitionVoucher moneyRequisitionVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRV" && x.DocID == moneyRequisitionVoucher.MRVNo).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == moneyRequisitionVoucher.EmpID).Name;
                context.Transection.Add(new Transection(moneyReqId.RefValue, -1, moneyRequisitionVoucher.Amount, (int)isExist[0].TransectionID, moneyRequisitionVoucher.Date, moneyRequisitionVoucher.RefNo, payeeName, moneyRequisitionVoucher.Description, "MRV", moneyRequisitionVoucher.MRVNo, 1, 1));
                context.Transection.Add(new Transection(moneyRequisitionVoucher.AccountId, 1, moneyRequisitionVoucher.Amount, (int)isExist[0].TransectionID, moneyRequisitionVoucher.Date, moneyRequisitionVoucher.RefNo, payeeName, moneyRequisitionVoucher.Description, "MRV", moneyRequisitionVoucher.MRVNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(moneyReqId.RefValue, -1, moneyRequisitionVoucher.Amount, tridNo, moneyRequisitionVoucher.Date, moneyRequisitionVoucher.RefNo, "", moneyRequisitionVoucher.Description, "MRV", moneyRequisitionVoucher.MRVNo, 1, 1));
                context.Transection.Add(new Transection(moneyRequisitionVoucher.AccountId, 1, moneyRequisitionVoucher.Amount, tridNo, moneyRequisitionVoucher.Date, moneyRequisitionVoucher.RefNo, "", moneyRequisitionVoucher.Description, "MRV", moneyRequisitionVoucher.MRVNo, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromMoneyRequisitionVoucher(ref BEEContext context, MoneyRequisitionVoucher moneyRequisitionVoucher)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRV" && x.DocID == moneyRequisitionVoucher.MRVNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }


        public static bool InsertUpdateFromMoneyRequisitionVouAdjust(BEEContext context, RequisitionMoneyVoucher moneyRequisitionVoucherAdjust, List<RequisitionMoneyVoucherLineItem> moneyRequisitionVoucherAdjustLineItems)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRVA" && x.DocID == moneyRequisitionVoucherAdjust.RSLNO).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                //decimal amount = 0;
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == moneyRequisitionVoucherAdjust.EmpID).Name;
                
                moneyRequisitionVoucherAdjustLineItems.ForEach(x =>
                {
                    //amount += x.MRVAAmount;
                    context.Transection.Add(new Transection(x.DebitAC, -1, x.Amount, (int)isExist[0].TransectionID, moneyRequisitionVoucherAdjust.Date, x.RefNo, payeeName, x.Description, "MRVA", moneyRequisitionVoucherAdjust.RSLNO, moneyRequisitionVoucherAdjust.CostCenterID, x.CostGroupID));
                });
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, (decimal)moneyRequisitionVoucherAdjust.TotalAmount, (int)isExist[0].TransectionID, moneyRequisitionVoucherAdjust.Date, "", payeeName, moneyRequisitionVoucherAdjust.Description, "MRVA", moneyRequisitionVoucherAdjust.RSLNO, moneyRequisitionVoucherAdjust.CostCenterID, 1));

            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                //decimal amount = 0;
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == moneyRequisitionVoucherAdjust.EmpID).Name;
               
                moneyRequisitionVoucherAdjustLineItems.ForEach(x =>
                {
                    //amount += x.MRVAAmount;
                    
                    context.Transection.Add(new Transection(x.DebitAC, -1, x.Amount, tridNo, moneyRequisitionVoucherAdjust.Date, x.RefNo, payeeName, x.Description, "MRVA", moneyRequisitionVoucherAdjust.RSLNO, moneyRequisitionVoucherAdjust.CostCenterID, x.CostGroupID));
                });
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, (decimal)moneyRequisitionVoucherAdjust.TotalAmount, tridNo, moneyRequisitionVoucherAdjust.Date, "", payeeName, moneyRequisitionVoucherAdjust.Description, "MRVA", moneyRequisitionVoucherAdjust.RSLNO, moneyRequisitionVoucherAdjust.CostCenterID, 1));


            }

            return true;
        }
        public static bool DeleteFromMoneyRequisitionVouAdjust(BEEContext context, RequisitionMoneyVoucher moneyRequisitionVoucherAdjust, List<RequisitionMoneyVoucherLineItem> moneyRequisitionVoucherAdjustLineItems)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRVA" && x.DocID == moneyRequisitionVoucherAdjust.RSLNO).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }


        public static bool InsertUpdateFromMoneyRequisitionVouRefund(ref BEEContext context, MoneyRequisitionRefund mrr)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRR" && x.DocID == mrr.MRRNo).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mrr.EmpID).Name;
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrr.Amount, (int)isExist[0].TransectionID, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
                context.Transection.Add(new Transection(mrr.AccountId, -1, mrr.Amount, (int)isExist[0].TransectionID, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrr.Amount, tridNo, mrr.Date, mrr.RefNo, "", mrr.Description, "MRR", mrr.MRRNo, 1, 1));
                context.Transection.Add(new Transection(mrr.AccountId, -1, mrr.Amount, tridNo, mrr.Date, mrr.RefNo, "", mrr.Description, "MRR", mrr.MRRNo, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromMoneyRequisitionRefund(ref BEEContext context, MoneyRequisitionRefund mrr)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRR" && x.DocID == mrr.MRRNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }


        public static bool InsertUpdateFromMoneyRequisitionPurchaseAdjustment(ref BEEContext context, MRPA mrpa)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRPA" && x.DocID == mrpa.MRPANo).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            var supGLID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mrpa.EmployeeID).Name;
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrpa.AdjustmentAmnt, (int)isExist[0].TransectionID, mrpa.MRPADate, mrpa.RefNo, payeeName, mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
                context.Transection.Add(new Transection(supGLID.RefValue, -1, mrpa.AdjustmentAmnt, (int)isExist[0].TransectionID, mrpa.MRPADate, mrpa.RefNo, payeeName, mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(supGLID.RefValue, 1, mrpa.AdjustmentAmnt, tridNo, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
                context.Transection.Add(new Transection(supGLID.RefValue, -1, mrpa.AdjustmentAmnt, tridNo, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromMoneyRequisitionPurchaseAdjustment(ref BEEContext context, int mrpa)
        {
            //BEEContext context = new BEEContext();
            var isExist = context.Transection.Where(x => x.DocType == "MRPA" && x.DocID == mrpa).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }


        public static bool InsertUpdateFromMoneyRequisitionPurAdjust(ref BEEContext context, MRPA mrpa)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRPA" && x.DocID == mrpa.MRPANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
                context.Transection.Add(new Transection(mrpa.SupplierID, -1, mrpa.AdjustmentAmnt, (int)isExist[0].TransectionID, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrpa.AdjustmentAmnt, (int)isExist[0].TransectionID, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
                context.Transection.Add(new Transection(mrpa.SupplierID, -1, mrpa.AdjustmentAmnt, tridNo, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrpa.AdjustmentAmnt, tridNo, mrpa.MRPADate, mrpa.RefNo, "", mrpa.Descriptions, "MRPA", mrpa.MRPANo, 1, 1));
            }

            return true;
        }
        public static bool DeleteFromMoneyRequisitionPurAdjust(ref BEEContext context, MRPA mrpa)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRPA" && x.DocID == mrpa.MRPANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                return true;
            }
            return false;
        }

        public static int InsertUpdateFromChartOfAccount(ref BEEContext context, ChartOfAccount chartOfAccount, decimal opAmount)
        {
            var companySetup = context.CompanySetup.FirstOrDefault();
            var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            if (chartOfAccount.InitialBalanceTransectionId > 0)
            {
                var transections = context.Transection.Where(x => x.DocID == chartOfAccount.Id && x.DocType == "AOB").ToList();
                //var transections = context.Transection.Where(x => x.DocID == chartOfAccount.Id && x.DocType == "AOB").ToList();
                foreach (var item in transections)
                {
                    context.Transection.Remove(item);
                }
                var caid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");
                if (chartOfAccount.RootAccountType == "as" || chartOfAccount.RootAccountType == "ex")
                {
                    context.Transection.Add(new Transection(chartOfAccount.Id, -1, opAmount, chartOfAccount.InitialBalanceTransectionId, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                    context.Transection.Add(new Transection(caid.RefValue, 1, opAmount, chartOfAccount.InitialBalanceTransectionId, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                }
                else
                {
                    context.Transection.Add(new Transection(caid.RefValue, -1, opAmount, chartOfAccount.InitialBalanceTransectionId, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                    context.Transection.Add(new Transection(chartOfAccount.Id, 1, opAmount, chartOfAccount.InitialBalanceTransectionId, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));

                }

            }
            else
            {

                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                var caid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");
                if (chartOfAccount.RootAccountType == "as" || chartOfAccount.RootAccountType == "ex")
                {
                    context.Transection.Add(new Transection(chartOfAccount.Id, -1, opAmount, trid.Refvalue, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                    context.Transection.Add(new Transection(caid.RefValue, 1, opAmount, trid.Refvalue, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                }
                else
                {
                    context.Transection.Add(new Transection(caid.RefValue, -1, opAmount, trid.Refvalue, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));
                    context.Transection.Add(new Transection(chartOfAccount.Id, 1, opAmount, trid.Refvalue, companySetup.StartDate, "", "OBE", "OBE", "AOB", chartOfAccount.Id, 1, 1));

                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
            }

            return trid.Refvalue;
        }

        public static void DeleteFromChartOfAccount(ref BEEContext context, ChartOfAccount chartOfAccount)
        {
            var transections = context.Transection.Where(x => x.DocID == chartOfAccount.Id && x.DocType == "AOB").ToList();
            foreach (var item in transections)
            {
                context.Transection.Remove(item);
            }
        }

        public static void InsertUpdateFromAccPaymentVoucher(ref BEEContext context, PaymentVoucherOne paymentVoucherOne, List<PaymentVoucherOneLineItem> voucherOneLineItems)
        {
            var isExist = context.Transection.Where(x => x.DocID == paymentVoucherOne.PVNo && x.DocType == "PV").ToList();
            foreach (var item in isExist)
            {
                context.Transection.Remove(item);
            }
            long trid = 0;
            if (isExist.Count > 0)
            {
                trid = isExist[0].TransectionID;
            }
            else
            {
                trid = GenerateTrid(ref context);
            }
            context.Transection.Add(new Transection(paymentVoucherOne.CashBankID, 1, paymentVoucherOne.PVTotalAmount, trid, paymentVoucherOne.PVDate, paymentVoucherOne.RefNo, paymentVoucherOne.ReceiverName, "", "PV", paymentVoucherOne.PVNo, paymentVoucherOne.SalesCenterID, 1));
            foreach (var item in voucherOneLineItems)
            {
                context.Transection.Add(new Transection(item.DebitAccID, -1, item.RVAmount, trid, paymentVoucherOne.PVDate, paymentVoucherOne.RefNo, paymentVoucherOne.ReceiverName, item.Description, "PV", item.PVNo, paymentVoucherOne.SalesCenterID, item.CostGroupID));
            }
        }
        public static void DeleteFromAccPaymentVoucher(ref BEEContext context, PaymentVoucherOne paymentVoucherOne)
        {
            var isExist = context.Transection.Where(x => x.DocID == paymentVoucherOne.PVNo && x.DocType == "PV").ToList();
            foreach (var item in isExist)
            {
                context.Transection.Remove(item);
            }
        }

       

        public static void InsertUpdateFromEmployeePaymentVoucher(ref BEEContext context, EPVEntry paymentVoucher, List<EPVLineItem> ePVLineItem)
        {
            var isExist = context.Transection.Where(x => x.DocID == paymentVoucher.EPVNo && x.DocType == "EPV").ToList();
            foreach (var item in isExist)
            {
                context.Transection.Remove(item);
            }
            long trid = 0;
            if (isExist.Count > 0)
            {
                trid = isExist[0].TransectionID;
            }
            else
            {
                trid = GenerateTrid(ref context);
            }
            int empBalnceId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Employee Balance").RefValue;
            context.Transection.Add(new Transection(empBalnceId, -1, paymentVoucher.EPVTotal, trid, paymentVoucher.Date, paymentVoucher.RefNo, paymentVoucher.Description, "", "EPV", paymentVoucher.EPVNo, 1, 1));
            foreach (var item in ePVLineItem)
            {
                context.Transection.Add(new Transection(paymentVoucher.PayAccId, 1, item.Amount, trid, paymentVoucher.Date, paymentVoucher.RefNo, paymentVoucher.Description, "", "EPV", paymentVoucher.EPVNo, 1, 1));
            }
        }
        public static void DeleteFromAccEmployeePaymentVoucher(ref BEEContext context, EPVEntry paymentVoucherOne)
        {
            var isExist = context.Transection.Where(x => x.DocID == paymentVoucherOne.EPVNo && x.DocType == "EPV").ToList();
            foreach (var item in isExist)
            {
                context.Transection.Remove(item);
            }
        }
        public static int GenerateTrid(ref BEEContext context)
        {

            var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
            int tridNo = 0;
            if (trid == null)
            {
                tridNo += 1;
            }
            else
            {
                tridNo = trid.Refvalue + 1;
                trid.Refvalue = tridNo;
            }

            context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;

            return tridNo;
        }

        #endregion
        public static bool InsertUpdateFromMRVoucherAdjustment(ref BEEContext context, MoneyRequisitionVoucherAdjust moneyRequisitionVoucherAdjust)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRVA" && x.DocID == moneyRequisitionVoucherAdjust.MRVANo).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                //context.Transection.Add(new Transection(moneyReqId.RefValue, -1, moneyRequisitionVoucherAdjust.Amount, (int)isExist[0].TransectionID, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
                //context.Transection.Add(new Transection(fundTrancferVoucher.TransTo, 1, moneyRequisitionVoucherAdjust.Amount, (int)isExist[0].TransectionID, fundTrancferVoucher.Date, fundTrancferVoucher.RefNo, "None", fundTrancferVoucher.Description, "FTV", fundTrancferVoucher.TransferId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                //context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                //context.Transection.Add(new Transection(moneyReqId.RefValue, -1, moneyRequisitionVoucherAdjust.Amount, tridNo, moneyRequisitionVoucherAdjust.Date, "MRVA", "Krishna", moneyRequisitionVoucherAdjust.Description, "MRVA", moneyRequisitionVoucherAdjust.MRVANo, 1, 1));
                //context.Transection.Add(new Transection(moneyReqId.RefValue, 1, moneyRequisitionVoucherAdjust.Amount, tridNo, moneyRequisitionVoucherAdjust.Date, "MRVA", "Krishna", moneyRequisitionVoucherAdjust.Description, "MRVA", moneyRequisitionVoucherAdjust.MRVANo, 1, 1));
            }

            return true;
        }

        public static bool InsertFromCustomerBalAdustment(ref BEEContext context, CustomerBalanceAdjustment cba)
        {
            var cbId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance");
            var asId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Sales");

            var isExist = context.Transection.Where(x => x.DocType == "SA" && x.DocID == cba.CAANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }

                context.Transection.Add(new Transection(cbId.RefValue, -1, cba.TotalAAmount, (int)isExist[0].TransectionID, cba.Date, "", "", "", "SA", cba.CAANo, 1, 1));
                context.Transection.Add(new Transection(asId.RefValue, 1, cba.TotalAAmount, (int)isExist[0].TransectionID, cba.Date, "", "", "", "SA", cba.CAANo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(cbId.RefValue, -1, cba.TotalAAmount, tridNo, cba.Date, "", "", "", "SA", cba.CAANo, 1, 1));
                context.Transection.Add(new Transection(asId.RefValue, 1, cba.TotalAAmount, tridNo, cba.Date, "", "", "", "SA", cba.CAANo, 1, 1));
            }

            var customerBal = context.CustomerBalanceCalculations.Where(x => x.DocumentType == "SA" && x.DocNo == cba.CAANo).FirstOrDefault();

            if (customerBal != null)
            {
                context.CustomerBalanceCalculations.Remove(customerBal);
            }

            CustomerBalanceCalculation cb = new CustomerBalanceCalculation();
            cb.CustomerID = cba.CustomerId;
            cb.TDate = cba.Date;
            cb.DocumentType = "SA";
            cb.AccountID = cbId.RefValue;
            cb.TrDesc = "";
            cb.RefNo = "";
            cb.Amount = cba.TotalAAmount;
            cb.DocNo = cba.CAANo;

            context.CustomerBalanceCalculations.Add(cb);


            return true;
        }

        public static bool DeleteFromCustomerBalAdjustment(ref BEEContext context, CustomerBalanceAdjustment cba)
        {
            var isExist = context.Transection.Where(x => x.DocType == "SA" && x.DocID == cba.CAANo).ToList();

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }

                var customerBal = context.CustomerBalanceCalculations.Where(x => x.DocumentType == "SA" && x.DocNo == cba.CAANo).FirstOrDefault();
                context.CustomerBalanceCalculations.Remove(customerBal);

                return true;
            }
            return false;
        }

        public static bool InsertUpdateFromILCB(BEEContext context, ILCB iLCB, List<ILCBLine> iLCBLines)
        {
            var allSupplierBal = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance");

            var isExist = context.Transection.Where(x => x.DocType == "ILCB" && x.DocID == iLCB.ILCBNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }

                iLCBLines.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, (int)isExist[0].TransectionID, iLCB.Date, "ILCB", "ILCB", "ILCB", "ILCB", iLCB.ILCBNo, 1, 1));
                });

                context.Transection.Add(new Transection(allSupplierBal.RefValue, 1, iLCB.BillTotalValue, (int)isExist[0].TransectionID, iLCB.Date, "ILCB", "ILCB", "ILCB", "ILCB", iLCB.ILCBNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;

                iLCBLines.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, tridNo, iLCB.Date, "ILCB", "ILCB", "ILCB", "ILCB", iLCB.ILCBNo, 1, 1));
                });

                context.Transection.Add(new Transection(allSupplierBal.RefValue, 1, iLCB.BillTotalValue, tridNo, iLCB.Date, "ILCB", "ILCB", "ILCB", "ILCB", iLCB.ILCBNo, 1, 1));
            }

            //var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "LCSP" && x.DocNo == iLCB.ILCBNo).ToList();

            //if (supplierBal != null)
            //{
            //    supplierBal.ForEach(x =>
            //    {
            //        context.SupplierBalanceCalculation.Remove(x);
            //    });

            //    iLCBLines.ForEach(x =>
            //    {
            //        SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
            //        sb.SupplierID = iLCB.SupplierID;
            //        sb.TDate = iLCB.Date;
            //        sb.DocumentType = "LCSP";
            //        sb.AccountID = x.DebitAccID;
            //        sb.TrDesription = x.Description;
            //        sb.RefNo = "LCSP";
            //        sb.Amount = x.Amount;
            //        sb.DocNo = x.ILCBNo;

            //        context.SupplierBalanceCalculation.Add(sb);
            //    });

            //}





            return true;
        }

        public static bool DeleteFromILCB(BEEContext context, ILCB iLCB)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILCB" && x.DocID == iLCB.ILCBNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            //var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "ILCB" && x.DocNo == iLCB.ILCBNo).ToList();

            //if (supplierBal != null)
            //{
            //    supplierBal.ForEach(x =>
            //    {
            //        context.SupplierBalanceCalculation.Remove(x);
            //    });

            //}

            //var lcAccBalExist = context.LCAccBalCalculation.Where(x => x.DocType == "ILCB" && x.DocNo == iLCB.ILCBNo).FirstOrDefault();

            //if (lcAccBalExist != null)
            //{
            //    context.LCAccBalCalculation.Remove(lcAccBalExist);
            //}

            return true;
        }
        public static bool DeleteFromLCPV(BEEContext context, ILCP iLCP)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LCPV" && x.DocID == iLCP.clmILCPNO).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

  

            return true;
        }
        public static bool InsertUpdateFromLCPV(BEEContext context, ILCP iLCP, List<ILCPLine> iLCPLines)
        {
            var allSupplierBal = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance");

            var isExist = context.Transection.Where(x => x.DocType == "LCPV" && x.DocID == iLCP.clmILCPNO).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }

                iLCPLines.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, (int)isExist[0].TransectionID, iLCP.clmDate, "LCPV", "LCPV", "LCPV", "LCPV", iLCP.clmILCPNO, 1, 1));
                });

                context.Transection.Add(new Transection(iLCP.clmAccountID, 1, iLCP.clmTotalAmount, (int)isExist[0].TransectionID, iLCP.clmDate, "LCPV", "LCPV", "LCPV", "LCPV", iLCP.clmILCPNO, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;

                iLCPLines.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, tridNo, iLCP.clmDate, "LCPV", "LCPV", "LCPV", "LCPV", iLCP.clmILCPNO, 1, 1));
                });

                context.Transection.Add(new Transection(iLCP.clmAccountID, 1, iLCP.clmTotalAmount, tridNo, iLCP.clmDate, "LCPV", "LCPV", "LCPV", "LCPV", iLCP.clmILCPNO, 1, 1));
            }

            //var supplierBal = context.SupplierBalanceCalculation.Where(x => x.DocumentType == "ILCP" && x.DocNo == iLCP.ILCBNo).ToList();

            //if (supplierBal != null)
            //{
            //    supplierBal.ForEach(x =>
            //    {
            //        context.SupplierBalanceCalculation.Remove(x);
            //    });

            //}

            //iLCBLines.ForEach(x =>
            //{
            //    SupplierBalanceCalculation sb = new SupplierBalanceCalculation();
            //    sb.SupplierID = iLCB.SupplierID;
            //    sb.TDate = iLCB.Date;
            //    sb.DocumentType = "ILCB";
            //    sb.AccountID = x.DebitAccID;
            //    sb.TrDesription = x.Description;
            //    sb.RefNo = "ILCB";
            //    sb.Amount = x.Amount;
            //    sb.DocNo = x.ILCBNo;

            //    context.SupplierBalanceCalculation.Add(sb);
            //});

            //var lcAccBalExist = context.LCAccBalCalculation.Where(x => x.DocType == "ILCB" && x.DocNo == iLCB.ILCBNo).FirstOrDefault();

            //if(lcAccBalExist != null)
            //{
            //    context.LCAccBalCalculation.Remove(lcAccBalExist);
            //}

            //LCAccBalCalculation lcbal = new LCAccBalCalculation();
            //lcbal.ILCID = iLCB.ILCID;
            //lcbal.Date = iLCB.Date;
            //lcbal.PaymentAccountID = allSupplierBal.RefValue;
            //lcbal.Amount = iLCB.BillTotalValue;
            //lcbal.Descriptions = "ILCB";
            //lcbal.Refno = "ILCB";
            //lcbal.DocType = "ILCB";
            //lcbal.DocNo = iLCB.ILCBNo;

            //context.LCAccBalCalculation.Add(lcbal);


            return true;
        }

        public static bool InsertFromILCP(BEEContext context, ILCPayment lcp, List<ILCPaymentLineItem> lcpLine)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILCP" && x.DocID == lcp.ILCPNo).ToList();

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }

                lcpLine.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, (int)isExist[0].TransectionID, lcp.Date, "ILCP", "ILCP", "ILCP", "ILCP", lcp.ILCPNo, 1, 1));
                });

                context.Transection.Add(new Transection(lcp.AccountID, 1, lcp.TotalAmount, (int)isExist[0].TransectionID, lcp.Date, "ILCP", "ILCP", "ILCP", "ILCP", lcp.ILCPNo, 1, 1));
            }
            else
            {
                int tridNo = GenerateTrid(ref context);

                lcpLine.ForEach(x =>
                {
                    context.Transection.Add(new Transection(x.DebitAccID, -1, x.Amount, tridNo, lcp.Date, "ILCP", "ILCP", "ILCP", "ILCP", lcp.ILCPNo, 1, 1));
                });

                context.Transection.Add(new Transection(lcp.AccountID, 1, lcp.TotalAmount, tridNo, lcp.Date, "ILCP", "ILCP", "ILCP", "ILCP", lcp.ILCPNo, 1, 1));
            }



            return true;
        }

        public static bool DeleteFromILCP(BEEContext context, ILCPayment lcp)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILCP" && x.DocID == lcp.ILCPNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            var lcAccBalExist = context.LCAccBalCalculation.Where(y => y.DocType == "ILCP" && y.DocNo == lcp.ILCPNo).FirstOrDefault();

            if (lcAccBalExist != null)
            {
                context.LCAccBalCalculation.Remove(lcAccBalExist);
            }

            return true;
        }
        public static bool InsertFromILCPD(ref BEEContext context, ImportLCPayment importLCPayment)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILCP" && x.DocID == importLCPayment.ILCPNo).ToList();

            if (isExist.Count() > 0)
            {
                var existdebit = isExist.FirstOrDefault(x => x.trType == -1);
                var existcredit = isExist.FirstOrDefault(x => x.trType == 1);
                context.Transection.Add(new Transection(importLCPayment.DebitAccID,-1, importLCPayment.Amount,existdebit.TransectionID, importLCPayment.Date, importLCPayment.RefNo,"", "ILCP", "ILCP", importLCPayment.ILCPNo,null,null));
                context.Transection.Add(new Transection(importLCPayment.CreditAccID, 1, importLCPayment.Amount, existcredit.TransectionID, importLCPayment.Date, importLCPayment.RefNo, "", "ILCP", "ILCP", importLCPayment.ILCPNo,null,null));
                context.Transection.Remove(existdebit);
                context.Transection.Remove(existcredit);
            }
            else
            {
                int tridNo = GenerateTrid(ref context);
                context.Transection.Add(new Transection(importLCPayment.DebitAccID, -1, importLCPayment.Amount, tridNo, importLCPayment.Date, importLCPayment.RefNo, "", "ILCP", "ILCP", importLCPayment.ILCPNo,null,null));
                context.Transection.Add(new Transection(importLCPayment.CreditAccID, 1, importLCPayment.Amount, tridNo, importLCPayment.Date, importLCPayment.RefNo, "", "ILCP", "ILCP", importLCPayment.ILCPNo,null,null));

            }
            return true;
        }
        public static bool DeleteFromILCPD(ref BEEContext context, ImportLCPayment importLCPayment)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILCP" && x.DocID == importLCPayment.ILCPNo).ToList();
            if (isExist.Count > 0)
            {
                var existdebit = isExist.FirstOrDefault(x => x.trType == -1);
                context.Transection.Remove(existdebit);
                var existcrebit = isExist.FirstOrDefault(x => x.trType == 1);
                context.Transection.Remove(existcrebit);
            }
            return true;
        }

        public static bool InsertFromLTR(ref BEEContext context, LTR LTR)
        {
            int ID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            int CAID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "LATR" && x.DocID == LTR.LTRID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            context.Transection.Add(new Transection(ID, -1, LTR.Amount, trid, 
                    LTR.Date, LTR.RefNo, "", LTR.Description, "LATR", LTR.LTRID, null, null));

            context.Transection.Add(new Transection(CAID, 1, LTR.Amount, trid,
                LTR.Date, LTR.RefNo, "", LTR.Description, "LATR", LTR.LTRID, null, null));
            return true;
        }
        public static bool InsertFromLTROB(ref BEEContext context, LTR LTR)
        {
            int ID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            int CAID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "LATROB" && x.DocID == LTR.LTRID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            context.Transection.Add(new Transection(ID, -1, LTR.Amount, trid,
                    LTR.Date, LTR.RefNo, "", LTR.Description, "LATROB", LTR.LTRID, null, null));

            context.Transection.Add(new Transection(CAID, 1, LTR.Amount, trid,
                LTR.Date, LTR.RefNo, "", LTR.Description, "LATROB", LTR.LTRID, null, null));
            return true;
        }
        public static bool InsertFromLTRA(ref BEEContext context, LTRA LTR)
        {
            int ID = LTR.AccountID;
            int CAID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "LATRA" && x.DocID == LTR.LTRANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            context.Transection.Add(new Transection(ID, -1, LTR.Amount, trid,
                    LTR.Date, LTR.RefNo, "", LTR.Description, "LATRA", LTR.LTRANo, null, null));

            context.Transection.Add(new Transection(CAID, 1, LTR.Amount, trid,
                LTR.Date, LTR.RefNo, "", LTR.Description, "LATRA", LTR.LTRANo, null, null));
            return true;
        }
        public static bool InsertFromLTRP(ref BEEContext context, LATRP LTR)
        {
            int ID = LTR.AccountID;
            int CAID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All LATR Account Balance GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "LATRP" && x.DocID == LTR.LTRPNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            context.Transection.Add(new Transection(CAID, -1, LTR.Amount, trid,
                    LTR.Date, LTR.RefNo, "", LTR.Description, "LATRP", LTR.LTRPNo, null, null));

            context.Transection.Add(new Transection(ID, 1, LTR.Amount, trid,
                LTR.Date, LTR.RefNo, "", LTR.Description, "LATRP", LTR.LTRPNo, null, null));
            return true;
        }
        public static bool DeleteFromLTR(ref BEEContext context, LTR LTR)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LATR" && x.DocID == LTR.LTRID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }
        public static bool DeleteFromLTROB(ref BEEContext context, LTR LTR)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LATROB" && x.DocID == LTR.LTRID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }
        public static bool DeleteFromLTRA(ref BEEContext context, LTRA LTR)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LATRA" && x.DocID == LTR.LTRANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }
        public static bool DeleteFromLTRP(ref BEEContext context, LATRP LTR)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LATRP" && x.DocID == LTR.LTRPNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }
        public static bool InsertFromLC(ref BEEContext context, ImportLC inportlc)
        {
            int ID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            int allSupplier = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "ILC" && x.DocID == inportlc.ILCId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            context.Transection.Add(new Transection(ID, -1, inportlc.GrandTotal, trid,
                    inportlc.ILCDate, inportlc.ILCNO, "", "", "ILC", inportlc.ILCId, null, null));

            context.Transection.Add(new Transection(allSupplier, 1, inportlc.GrandTotal, trid,
                   inportlc.ILCDate, inportlc.ILCNO, "", "", "ILC", inportlc.ILCId, null, null));
            return true;
        }

        public static bool DeleteFromLC(ref BEEContext context, ImportLC inportlc)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ILC" && x.DocID == inportlc.ILCId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }
        public static bool InsertFromLCPC(ref BEEContext context, LCCosting LCPC, List<LCCostingLine> addedItems, DateTime LCPCDate)
        {
            int LCPCNo = addedItems[0].LCPCNo;
            int trid = GenerateTrid(ref context);
            int FGInvId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            int ImportLcId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "LCPC" && x.DocID == LCPCNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            context.Transection.Add(new Transection(FGInvId, -1, LCPC.CostingTotal, trid, LCPCDate, "", "", "", "LCPC", LCPCNo, null, null));
            context.Transection.Add(new Transection(ImportLcId, 1, LCPC.CostingTotal, trid, LCPCDate, "", "", "", "LCPC", LCPCNo, null, null));
            return true;
        }
        public static bool DeleteFromLCPC(ref BEEContext context, int id)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LCPC" && x.DocID == id).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertFromSalaryPaymentAdjustment(ref BEEContext context, SalaryPayment salary)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ESP" && x.DocID == salary.SPVNo).ToList();
            var aCId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Staff Salary");
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                context.Transection.Add(new Transection(aCId.RefValue, 1, salary.TotalAmount, (int)isExist[0].TransectionID, salary.Date, "", "None", "", "ESP", salary.SPVNo, 1, 1));
                context.Transection.Add(new Transection(Convert.ToInt32(salary.Paymode), -1, salary.TotalAmount, (int)isExist[0].TransectionID, salary.Date, "", "None", "", "ESP", salary.SPVNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(Convert.ToInt32(salary.Paymode), -1, salary.TotalAmount, tridNo, salary.Date, "", "", "", "ESP", salary.SPVNo, 1, 1));
                context.Transection.Add(new Transection(aCId.RefValue, 1, salary.TotalAmount, tridNo, salary.Date, "", "", "", "ESP", salary.SPVNo, 1, 1));
            }

            return true;
        }

        public static bool DeleteFromSalaryPayment(BEEContext context, SalaryPayment salary)
        {
            var isExist = context.Transection.Where(x => x.DocType == "ESP" && x.DocID == salary.SPVNo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            return true;
        }

        public static bool IsnertFromSalaryProcess(ref BEEContext context, List<SalaryProcessViewModel> salaryInformationLine, DateTime processDate, int salaryProcessNo, string description)
        {
            int pfAcc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "PF(OWN)").RefValue;
            int incomeTax = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Income Tax").RefValue;
            int utilityBill = context.AccountConfiguration.FirstOrDefault(x => x.Name == "UtilityBill").RefValue;
            int revenueStamp = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Revenue Stamp").RefValue;
            int advanceAdjust = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance Adjust").RefValue;
            int securtityReceive = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Security Receive").RefValue;
            int otherAdjust = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Other Adjust").RefValue;
            int messBill = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Mess Bill").RefValue;
            int canteenBill = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Canteen Bill").RefValue;
            int tmPhone = context.AccountConfiguration.FirstOrDefault(x => x.Name == "T/M Phone").RefValue;
            int houseRent = context.AccountConfiguration.FirstOrDefault(x => x.Name == "House Rent").RefValue;
            int fSales = context.AccountConfiguration.FirstOrDefault(x => x.Name == "F-Sales").RefValue;
            int salaryExpense = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Staff Salary").RefValue;
            int payableId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Salary Payable").RefValue;
            long trid = 0;
            var isExist = context.Transection.Where(x => x.DocID == salaryProcessNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            else
            {
                trid = GenerateTrid(ref context);
            }


            decimal incomeTaxAmt = (decimal)salaryInformationLine.Sum(x => x.Tax);
            if (incomeTaxAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = incomeTax, trType = 1, Amount = incomeTaxAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal pfAccAmt = (decimal)salaryInformationLine.Sum(x => x.PFOwn);
            if (pfAccAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = pfAcc, trType = 1, Amount = pfAccAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal utilityBillAmt = (decimal)salaryInformationLine.Sum(x => x.UtilityBill);
            if (utilityBillAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = utilityBill, trType = 1, Amount = utilityBillAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal revenueStampAmt = (decimal)salaryInformationLine.Sum(x => x.RevenueStamp);
            if (revenueStampAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = revenueStamp, trType = 1, Amount = revenueStampAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal advanceAdjustAmt = (decimal)salaryInformationLine.Sum(x => x.AdvanceAdjust);
            if (advanceAdjustAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = advanceAdjust, trType = 1, Amount = advanceAdjustAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal securityReceiveAmt = (decimal)salaryInformationLine.Sum(x => x.SecurityReceive);
            if (securityReceiveAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = securtityReceive, trType = 1, Amount = securityReceiveAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal otherAdjustAmt = (decimal)salaryInformationLine.Sum(x => x.OtherAdjust);
            if (otherAdjustAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = otherAdjust, trType = 1, Amount = otherAdjustAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal messBillAmt = (decimal)salaryInformationLine.Sum(x => x.MessBill);
            if (messBillAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = messBill, trType = 1, Amount = messBillAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal canteenBillAmt = (decimal)salaryInformationLine.Sum(x => x.CanteenBill);
            if (canteenBillAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = canteenBill, trType = 1, Amount = canteenBillAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal tmPhoneAmt = (decimal)salaryInformationLine.Sum(x => x.TMPhone);
            if (tmPhoneAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = tmPhone, trType = 1, Amount = tmPhoneAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal houseRentAmt = (decimal)salaryInformationLine.Sum(x => x.HouseRent);
            if (houseRentAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = houseRent, trType = 1, Amount = houseRentAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal fSalesAmt = (decimal)salaryInformationLine.Sum(x => x.FSales);
            if (fSalesAmt > 0)
            {
                context.Transection.Add(new Transection { CAID = fSales, trType = 1, Amount = fSalesAmt, TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            decimal netPayable = salaryInformationLine.Sum(x => x.PayableSalary);
            decimal payable = salaryInformationLine.Sum(x => x.EarnedSalary);
            if (payable > 0)
            {
                context.Transection.Add(new Transection { CAID = payableId, trType = 1, Amount = (decimal)Convert.ToInt32(netPayable), TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            }
            context.Transection.Add(new Transection { CAID = salaryExpense, trType = -1, Amount = (decimal)Convert.ToInt32(payable), TransectionID = trid, trdate = processDate, referenceNo = "", payeeName = "", transectionDesc = description, DocType = "MSG", DocID = salaryProcessNo, CostCntrID = null, CostGrpID = null });
            return true;
        }

        public static bool InsertUpdateFromEITOB(ref BEEContext context, EITOB itob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EITOB" && x.DocID == itob.EmployeeId).ToList();
            var eitOBID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Employee Income Tax Payable");
            var obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");

            //if(itob.Amount < 0)
            //{
            //    itob.Amount = itob.Amount * -1;
            //}

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == itob.EmployeeId).Name;
                context.Transection.Add(new Transection(eitOBID.Id, 1, itob.TAmount, (int)isExist[0].TransectionID, itob.Tdate, itob.TRefNo, payeeName, itob.TDescription, "EITOB", itob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, itob.TAmount, (int)isExist[0].TransectionID, itob.Tdate, itob.TRefNo, payeeName, itob.TDescription, "EITOB", itob.EmployeeId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == itob.EmployeeId).Name;
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(eitOBID.Id, 1, itob.TAmount, tridNo, itob.Tdate, itob.TRefNo, payeeName, itob.TDescription, "EITOB", itob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, itob.TAmount, tridNo, itob.Tdate, itob.TRefNo, payeeName, itob.TDescription, "EITOB", itob.EmployeeId, 1, 1));
            }

            //var itobExist = context.EitBalanceCalculation.FirstOrDefault(x => x.EmpID == itob.EmployeeId && x.DocType == "EITOB");
            //if (itobExist != null)
            //{
            //    context.EitBalanceCalculation.Remove(itobExist);

            //}

            //MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            //mrbc.EmpID = itob.EmployeeId;
            //mrbc.Date = itob.itobDate.Add(DateTime.Now.TimeOfDay);
            //mrbc.DocumentType = "itob";
            //mrbc.AccountId = obeId.RefValue;
            //mrbc.Description = itob.Description;
            //mrbc.RefNo = itob.RefNo;
            //mrbc.Amount = itob.Amount;
            //mrbc.DocID = itob.EmployeeId;

            //context.MoneyRequisitionBalanceCalculations.Add(mrbc);

            return true;
        }

        public static bool DeleteFromMoneyRequisitionOB(ref BEEContext context, MoneyRequisitionOB mrob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "MROB" && x.DocID == mrob.EmployeeId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            var mrbcExist = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.EmpID == mrob.EmployeeId && x.DocumentType == "MROB");
            if (mrbcExist != null)
            {
                context.MoneyRequisitionBalanceCalculations.Remove(mrbcExist);

            }

            return true;
        }


        public static bool InsertUpdateFromEPFOB(ref BEEContext context, EPFOB pfob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EPFOB" && x.DocID == pfob.EmployeeId).ToList();
            var epfOBID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Employee Provident Fund Payable");
            var obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");

            //if(itob.Amount < 0)
            //{
            //    itob.Amount = itob.Amount * -1;
            //}

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == pfob.EmployeeId).Name;
                context.Transection.Add(new Transection(epfOBID.Id, 1, pfob.TAmount, (int)isExist[0].TransectionID, pfob.Tdate, pfob.TRefNo, payeeName, pfob.TDescription, "EPFOB", pfob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, pfob.TAmount, (int)isExist[0].TransectionID, pfob.Tdate, pfob.TRefNo, payeeName, pfob.TDescription, "EPFOB", pfob.EmployeeId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == pfob.EmployeeId).Name;
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(epfOBID.Id, 1, pfob.TAmount, tridNo, pfob.Tdate, pfob.TRefNo, payeeName, pfob.TDescription, "EPFOB", pfob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, pfob.TAmount, tridNo, pfob.Tdate, pfob.TRefNo, payeeName, pfob.TDescription, "EPFOB", pfob.EmployeeId, 1, 1));
            }

            //var itobExist = context.EitBalanceCalculation.FirstOrDefault(x => x.EmpID == itob.EmployeeId && x.DocType == "EITOB");
            //if (itobExist != null)
            //{
            //    context.EitBalanceCalculation.Remove(itobExist);

            //}

            //MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            //mrbc.EmpID = itob.EmployeeId;
            //mrbc.Date = itob.itobDate.Add(DateTime.Now.TimeOfDay);
            //mrbc.DocumentType = "itob";
            //mrbc.AccountId = obeId.RefValue;
            //mrbc.Description = itob.Description;
            //mrbc.RefNo = itob.RefNo;
            //mrbc.Amount = itob.Amount;
            //mrbc.DocID = itob.EmployeeId;

            //context.MoneyRequisitionBalanceCalculations.Add(mrbc);

            return true;
        }

        public static bool DeleteFromEPFOB(ref BEEContext context, EPFOB pfob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EPFOB" && x.DocID == pfob.EmployeeId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            var pfbcExist = context.EpfBalanceCalculation.FirstOrDefault(x => x.EmpID == pfob.EmployeeId && x.DocType == "EPFOB");
            if (pfbcExist != null)
            {
                context.EpfBalanceCalculation.Remove(pfbcExist);

            }

            return true;
        }


        public static bool InsertUpdateFromEMCOB(ref BEEContext context, EMCOB mcob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EMCOB" && x.DocID == mcob.EmployeeId).ToList();
            var emcOBID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Employee Motor Cycle Payable");
            var obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");

            //if(itob.Amount < 0)
            //{
            //    itob.Amount = itob.Amount * -1;
            //}

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mcob.EmployeeId).Name;
                context.Transection.Add(new Transection(emcOBID.Id, 1, mcob.TAmount, (int)isExist[0].TransectionID, mcob.Tdate, mcob.TRefNo, payeeName, mcob.TDescription, "EMCOB", mcob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, mcob.TAmount, (int)isExist[0].TransectionID, mcob.Tdate, mcob.TRefNo, payeeName, mcob.TDescription, "EMCOB", mcob.EmployeeId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mcob.EmployeeId).Name;
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(emcOBID.Id, 1, mcob.TAmount, tridNo, mcob.Tdate, mcob.TRefNo, payeeName, mcob.TDescription, "EMCOB", mcob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, mcob.TAmount, tridNo, mcob.Tdate, mcob.TRefNo, payeeName, mcob.TDescription, "EMCOB", mcob.EmployeeId, 1, 1));
            }

            //var itobExist = context.EitBalanceCalculation.FirstOrDefault(x => x.EmpID == itob.EmployeeId && x.DocType == "EITOB");
            //if (itobExist != null)
            //{
            //    context.EitBalanceCalculation.Remove(itobExist);

            //}

            //MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            //mrbc.EmpID = itob.EmployeeId;
            //mrbc.Date = itob.itobDate.Add(DateTime.Now.TimeOfDay);
            //mrbc.DocumentType = "itob";
            //mrbc.AccountId = obeId.RefValue;
            //mrbc.Description = itob.Description;
            //mrbc.RefNo = itob.RefNo;
            //mrbc.Amount = itob.Amount;
            //mrbc.DocID = itob.EmployeeId;

            //context.MoneyRequisitionBalanceCalculations.Add(mrbc);

            return true;
        }

        public static bool DeleteFromEMCOB(ref BEEContext context, EMCOB mcob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EMCOB" && x.DocID == mcob.EmployeeId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            var mcbcExist = context.EmcBalanceCalculation.FirstOrDefault(x => x.EmpID == mcob.EmployeeId && x.DocType == "EMCOB");
            if (mcbcExist != null)
            {
                context.EmcBalanceCalculation.Remove(mcbcExist);

            }

            return true;
        }


        public static bool InsertUpdateFromEADOB(ref BEEContext context, EADOB adob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EADOB" && x.DocID == adob.EmployeeId).ToList();
            var eadOBID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Advance to Employee GL");
            var obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");

            //if(itob.Amount < 0)
            //{
            //    itob.Amount = itob.Amount * -1;
            //}

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == adob.EmployeeId).Name;
                context.Transection.Add(new Transection(eadOBID.Id, 1, adob.TAmount, (int)isExist[0].TransectionID, adob.Tdate, adob.TRefNo, payeeName, adob.TDescription, "EADOB", adob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, adob.TAmount, (int)isExist[0].TransectionID, adob.Tdate, adob.TRefNo, payeeName, adob.TDescription, "EADOB", adob.EmployeeId, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == adob.EmployeeId).Name;
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(eadOBID.Id, 1, adob.TAmount, tridNo, adob.Tdate, adob.TRefNo, payeeName, adob.TDescription, "EADOB", adob.EmployeeId, 1, 1));
                context.Transection.Add(new Transection(obeId.Id, -1, adob.TAmount, tridNo, adob.Tdate, adob.TRefNo, payeeName, adob.TDescription, "EADOB", adob.EmployeeId, 1, 1));
            }

            //var itobExist = context.EitBalanceCalculation.FirstOrDefault(x => x.EmpID == itob.EmployeeId && x.DocType == "EITOB");
            //if (itobExist != null)
            //{
            //    context.EitBalanceCalculation.Remove(itobExist);

            //}

            //MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            //mrbc.EmpID = itob.EmployeeId;
            //mrbc.Date = itob.itobDate.Add(DateTime.Now.TimeOfDay);
            //mrbc.DocumentType = "itob";
            //mrbc.AccountId = obeId.RefValue;
            //mrbc.Description = itob.Description;
            //mrbc.RefNo = itob.RefNo;
            //mrbc.Amount = itob.Amount;
            //mrbc.DocID = itob.EmployeeId;

            //context.MoneyRequisitionBalanceCalculations.Add(mrbc);

            return true;
        }

        public static bool DeleteFromEADOB(ref BEEContext context, EADOB adob)
        {
            var isExist = context.Transection.Where(x => x.DocType == "EADOB" && x.DocID == adob.EmployeeId).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }

            var adbcExist = context.EadBalanceCalculation.FirstOrDefault(x => x.EmpID == adob.EmployeeId && x.DocType == "EADOB");
            if (adbcExist != null)
            {
                context.EadBalanceCalculation.Remove(adbcExist);

            }

            return true;
        }
        public static bool InsertUpdateFromMoneyRequisitionRefundVoucher(ref BEEContext context, MoneyRequisitionRefund mrr)   
        {
            var isExist = context.Transection.Where(x => x.DocType == "MRR" && x.DocID == mrr.MRRNo).ToList();
            var moneyReqId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Money Requisition Balance");
            //var obeId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE");

            //if(mrob.Amount < 0)
            //{
            //    mrob.Amount = mrob.Amount * -1;
            //}

            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mrr.EmpID).Name;
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrr.Amount, (int)isExist[0].TransectionID, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
                context.Transection.Add(new Transection(mrr.AccountId, -1, mrr.Amount, (int)isExist[0].TransectionID, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
            }
            else
            {
                var trid = context.Sqquencers.FirstOrDefault(x => x.Name == "trid");
                int tridNo = 0;
                if (trid == null)
                {
                    tridNo += 1;
                }
                else
                {
                    tridNo = trid.Refvalue + 1;
                    trid.Refvalue = tridNo;
                }
                string payeeName = context.Employees.FirstOrDefault(x => x.Id == mrr.EmpID).Name;
                context.Entry<Sqquencer>(trid).State = System.Data.Entity.EntityState.Modified;
                context.Transection.Add(new Transection(moneyReqId.RefValue, 1, mrr.Amount, tridNo, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
                context.Transection.Add(new Transection(mrr.AccountId, -1, mrr.Amount, tridNo, mrr.Date, mrr.RefNo, payeeName, mrr.Description, "MRR", mrr.MRRNo, 1, 1));
            }

            var mrbcExist = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.EmpID == mrr.EmpID && x.DocumentType == "MRR");
            if (mrbcExist != null)
            {
                context.MoneyRequisitionBalanceCalculations.Remove(mrbcExist);

            }

            MoneyRequisitionBalanceCalculation mrbc = new MoneyRequisitionBalanceCalculation();
            mrbc.EmpID = mrr.EmpID;
            mrbc.Date = mrr.Date.Add(DateTime.Now.TimeOfDay);
            mrbc.DocumentType = "MRR";
            mrbc.AccountId = mrr.AccountId;
            mrbc.Description = mrr.Description;
            mrbc.RefNo = mrr.RefNo;
            mrbc.Amount = mrr.Amount;
            mrbc.DocID = mrr.MRRNo;

            context.MoneyRequisitionBalanceCalculations.Add(mrbc);

            return true;
        }

        public static bool InsertFromFGSA(ref BEEContext context, StockAdjustment SA)
        {
            int Selected = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory Adjustement A/C").RefValue;
            int baseAc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "FGSA" && x.DocID == SA.SANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            if(SA.TransectionType == "Decrease")
            {
                context.Transection.Add(new Transection(Selected, -1, SA.TotalAmount, trid,
                                   SA.Date, SA.RefNo, "", SA.Description, "FGSA", SA.SANo, null, null));

                context.Transection.Add(new Transection(baseAc, 1, SA.TotalAmount, trid,
                       SA.Date, SA.RefNo, "", SA.Description, "FGSA", SA.SANo, null, null));
            }
            else
            {
                context.Transection.Add(new Transection( baseAc, -1, SA.TotalAmount, trid,
                   SA.Date, SA.RefNo, "", SA.Description, "FGSA", SA.SANo, null, null));

                context.Transection.Add(new Transection(Selected, 1, SA.TotalAmount, trid,
                       SA.Date, SA.RefNo, "", SA.Description, "FGSA", SA.SANo, null, null));
            }
           
            return true;
        }


        public static bool DeleteFromFGSA(ref BEEContext context, StockAdjustment sa)
        {
            var isExist = context.Transection.Where(x => x.DocType == "FGSA" && x.DocID == sa.SANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertFromRMSA(ref BEEContext context, StockAdjustmentRM SA)
        {
            int Selected = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory Adjustment A/C").RefValue;
            int baseAc = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "RMSA" && x.DocID == SA.SANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            if (SA.TransectionType == "Decrease")
            {
                context.Transection.Add(new Transection(Selected, -1, SA.TotalAmount, trid,
                                   SA.Date, SA.RefNo, "", SA.Description, "RMSA", SA.SANo, null, null));

                context.Transection.Add(new Transection(baseAc, 1, SA.TotalAmount, trid,
                       SA.Date, SA.RefNo, "", SA.Description, "RMSA", SA.SANo, null, null));
            }
            else
            {
                context.Transection.Add(new Transection(baseAc, -1, SA.TotalAmount, trid,
                   SA.Date, SA.RefNo, "", SA.Description, "RMSA", SA.SANo, null, null));

                context.Transection.Add(new Transection(Selected, 1, SA.TotalAmount, trid,
                       SA.Date, SA.RefNo, "", SA.Description, "RMSA", SA.SANo, null, null));
            }

            return true;
        }
        public static bool DeleteFromRMSA(ref BEEContext context, StockAdjustmentRM sa)
        {
            var isExist = context.Transection.Where(x => x.DocType == "RMSA" && x.DocID == sa.SANo).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }


        public static bool InsertUpdateFromDPR(ref BEEContext context, ReceivePaymentInfo dpr)
        {
            int bankCharge = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank Charge").RefValue;
            int cr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            int trid = GenerateTrid(ref context);
            var isExist = context.Transection.Where(x => x.DocType == "DPR" && x.DocID == dpr.RPID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            if (dpr.BankCharges > 0)
            {
                context.Transection.Add(new Transection(bankCharge, -1, dpr.BankCharges, trid,
                                   dpr.TDate, dpr.RefNo, "", dpr.Description, "DPR", dpr.RPID, null, null));
                context.Transection.Add(new Transection(dpr.AccountID, -1, dpr.NetAmount, trid,
                                   dpr.TDate, dpr.RefNo, "", dpr.Description, "DPR", dpr.RPID, null, null));
                context.Transection.Add(new Transection(cr, 1, dpr.ReceiveAmt , trid,
                                 dpr.TDate, dpr.RefNo, "", dpr.Description, "DPR", dpr.RPID, null, null));
             
            }
            else
            {
                context.Transection.Add(new Transection(dpr.AccountID, -1, dpr.ReceiveAmt, trid,
                                   dpr.TDate, dpr.RefNo, "", dpr.Description, "DPR", dpr.RPID, null, null));
                context.Transection.Add(new Transection(cr, 1, dpr.ReceiveAmt, trid,
                                 dpr.TDate, dpr.RefNo, "", dpr.Description, "DPR", dpr.RPID, null, null));
            }

            return true;
        }
        public static bool DeleteFromDPR(ref BEEContext context, ReceivePaymentInfo dpr)
        {
            var isExist = context.Transection.Where(x => x.DocType == "DPR" && x.DocID == dpr.RPID).ToList();
            if (isExist.Count() > 0)
            {
                foreach (var item in isExist)
                {
                    context.Transection.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertUpdateFromDAA(ref BEEContext context, DealerAccAdjustment DAA)
        {
         
         
            long trid = GenerateTrid(ref context);
            int cr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
          
            var isExist = context.Transection.Where(x => x.DocType == "DAA" && x.DocID == DAA.DAANo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(cr, 1, DAA.TotalAmount, trid, DAA.DAADate, "", " ", DAA.Description, "DAA", DAA.DAANo, DAA.DepotID, DAA.CostGroup));
            context.Transection.Add(new Transection(DAA.DAAAccountID, -1, DAA.TotalAmount, trid, DAA.DAADate, "", " ", DAA.Description, "DAA", DAA.DAANo, DAA.DepotID, DAA.CostGroup));
            return true;
        }
        public static bool DeleteFromDAA(ref BEEContext context, DealerAccAdjustment DAA)
        {

            var isExist = context.Transection.Where(x => x.DocType == "DAA" && x.DocID == DAA.DAANo).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
           
            return true;
        }
        public static bool DeleteFromGPB(ref BEEContext context, int billNo)
        {
            var isExist = context.Transection.Where(x => (x.DocType == "GPB" || x.DocType == "GPBT" || x.DocType == "GPBD" || x.DocType == "GPBV") && x.DocID==billNo).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }

        public static bool DeleteFromFPB(ref BEEContext context, int billNo)
        {
            var isExist = context.Transection.Where(x => (x.DocType == "FPB" || x.DocType == "FPBVT" || x.DocType == "FPBDIS" || x.DocType == "FPBVDS" || x.DocType== "FPBAIT") && x.DocID==billNo).ToList();
            if (isExist.Count > 0)
            {
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }
        public static bool InsertUpdateFromLCFC(ref BEEContext context, tblLCCosting lcfc)
        {
            long trid = GenerateTrid(ref context);
            int cr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            int dr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Provision for Supplier").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "LCFC" && x.DocID == lcfc.LCPCNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(cr, 1, lcfc.ILCCostingTotal, trid, lcfc.LCPCDate, "", " ", "", "LCFC", lcfc.LCPCNo, 1, 1));
            context.Transection.Add(new Transection(dr, -1, lcfc.ILCCostingTotal, trid, lcfc.LCPCDate, "", " ", "", "LCFC", lcfc.LCPCNo, 1, 1));
            return true;
        }
        public static bool DeleteFromLCFC(ref BEEContext context, tblLCCosting lcfc)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LCFC" && x.DocID == lcfc.LCPCNo).ToList();
            if (isExist.Count > 0)
            {
                //trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }
        public static bool InsertUpdateFromLCPC(ref BEEContext context, tblLCCosting lcfc)
        {
            long trid = GenerateTrid(ref context);
            int cr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
            int dr = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Supplier Balance").RefValue;
            var isExist = context.Transection.Where(x => x.DocType == "LCPC" && x.DocID == lcfc.LCPCNo).ToList();
            if (isExist.Count > 0)
            {
                trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            context.Transection.Add(new Transection(cr, 1, lcfc.ILCCostingTotal, trid, lcfc.LCPCDate, "", " ", "", "LCPC", lcfc.LCPCNo, 1, 1));
            context.Transection.Add(new Transection(dr, -1, lcfc.ILCCostingTotal, trid, lcfc.LCPCDate, "", " ", "", "LCPC", lcfc.LCPCNo, 1, 1));
            return true;
        }
        public static bool DeleteFromLCPC(ref BEEContext context, tblLCCosting lcfc)
        {
            var isExist = context.Transection.Where(x => x.DocType == "LCPC" && x.DocID == lcfc.LCPCNo).ToList();
            if (isExist.Count > 0)
            {
                //trid = isExist.FirstOrDefault().TransectionID;
                context.Transection.RemoveRange(isExist);
            }
            return true;
        }
        internal static bool DeleteFromFRR(ref BEEContext context, TblFreezerRent frr)
        {

            var items = context.Transection.Where(x => x.DocType == "FRR" && x.DocID == frr.clmFRRNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }
        internal static bool DeleteFromFRI(ref BEEContext context, FRI fri)
        {

            var items = context.Transection.Where(x => x.DocType == "FRI" && x.DocID == fri.clmFRINo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }
        internal static bool DeleteFromFARR(ref BEEContext context, FARR farr)
        {

            var items = context.Transection.Where(x => x.DocType == "FARR" && x.DocID == farr.clmFARRNo).ToList();
            foreach (var item in items)
            {
                context.Transection.Remove(item);
            }
            return true;
        }
        public static bool InsertFromNewInventoryItem(ref BEEContext context, ChartOfInventory inventoryData)
        {
            //decimal totalamount = 0;
            //foreach (var item in FGPL)
            //{
            //    totalamount += item.CogsTotal;
            //}
            long trid = GenerateTrid(ref context);
            int invGLId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "FG Inventory GL").RefValue;
            int obEID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;

            //var isExist = context.Transection.Where(x => x.DocType == "FGP" && x.DocID == FGP.FGPNo).ToList();
            //if (isExist.Count > 0)
            //{
            //    trid = isExist.FirstOrDefault().TransectionID;
            //    context.Transection.RemoveRange(isExist);
            //}
            //context.Transection.Add(new Transection(obEID, 1, Convert.ToDecimal(inventoryData.OBValue), trid, inventoryData.OBDate, "", "", "FG OB", "IOB", inventoryData.Id, 1, 1));
            //context.Transection.Add(new Transection(invGLId, -1, Convert.ToDecimal(inventoryData.OBValue), trid, inventoryData.OBDate, "", "", "FG OB", "IOB", inventoryData.Id, 1, 1));
            return true;
        }
    }
}