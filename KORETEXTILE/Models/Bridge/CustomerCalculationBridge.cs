using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.PaymentReceiveInfo;
using BEEERP.Models.SalesModule;
using BEEERP.Models.FreezerRent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public static class CustomerCalculationBridge
    {
        public static bool InsertAndUpdateFromFARR(ref BEEContext context, FARR farr, List<FARRLineItem> addedItems)
        {
            var items = context.CustomerBalanceCalculations.Where(x => x.TDate == farr.clmDate && x.AccountID == farr.clmAccountID).ToList();
            foreach (var item in items)
            {
                context.CustomerBalanceCalculations.Remove(item);
            }
            foreach (var item in addedItems)
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation()
                {
                    CustomerID = farr.clmDealerID,
                    TDate = farr.clmDate,
                    DocumentType = item.clmDealerYN,
                    AccountID = farr.clmAccountID,
                    TrDesc = item.clmDescriptions,
                    RefNo = item.clmREfno,
                    Amount = Convert.ToDecimal(farr.clmTotalAmount),
                    DocNo = farr.clmFARRNo,
                });
            }
            return true;
        }
        public static bool InsertAndUpdateFromFRI(ref BEEContext context, FRI fri, List<FRILineItem> addedItems)
        {
            var items = context.CustomerBalanceCalculations.Where(x => x.TDate == fri.clmDate && x.AccountID == fri.clmAccountID).ToList();
            foreach (var item in items)
            {
                context.CustomerBalanceCalculations.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation()
                {
                    CustomerID = fri.clmDealerID,
                    TDate = fri.clmDate,
                    DocumentType = item.clmDealerYN,
                    AccountID = fri.clmAccountID,
                    TrDesc = item.clmDescriptions,
                    RefNo = item.clmRefNo,
                    Amount = Convert.ToDecimal(fri.clmTotal),
                    DocNo = fri.clmFRINo,
                });
            }
            return true;
        }
        public static bool InsertAndUpdateFromFRR(ref BEEContext context, TblFreezerRent frr, List<TblFreezerRentLineItem> addedItems)
        {
            var items = context.CustomerBalanceCalculations.Where(x => x.TDate == frr.clmRPDate && x.AccountID == frr.clmAccountID).ToList();
            foreach (var item in items)
            {
                context.CustomerBalanceCalculations.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation()
                {
                    CustomerID = frr.clmDealerID,
                    TDate = frr.clmRPDate,
                    DocumentType = item.clmDealerYN,
                    AccountID = frr.clmAccountID,
                    TrDesc = item.clmDescription,
                    RefNo = item.clmRef,
                    Amount = Convert.ToDecimal(frr.FrrTotal),
                    DocNo = frr.clmFRRNo,
                });
            }
            return true;
        }
        public static bool InsertFromCustomerBalanceCalculation(ref BEEContext context, Customer customer)
        {
            var item = context.CustomerBalanceCalculations.FirstOrDefault(x => x.CustomerID == customer.Id&&x.DocumentType=="COB");
            if(item!=null)
            {
                context.CustomerBalanceCalculations.Remove(item);
            }
            customer.AsDate.Add(DateTime.Now.TimeOfDay);
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(customer.Id,customer.AsDate,"COB",accId,"OB",customer.CustomerName,Convert.ToDecimal(customer.OB),customer.Id));
            return true;
        }
        public static bool InsertFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dealer, List<DSIAdjustmentLineItem> Line)
        {
            var item = context.CustomerBalanceCalculations.Where(x => x.DocNo == dealer.DSIANo && x.DocumentType == "DSIA").ToList();
            if (item.Count >0 )
            {
                foreach(var x in item)
                {
                    context.CustomerBalanceCalculations.Remove(x);
                }
                
            }
          
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Payable").RefValue;
            foreach(var d in Line)
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(d.DealerID, dealer.DSIADate, "DSIA", accId,dealer.Description, dealer.RefNo, -d.Amount, d.DSIANo));
            }
   
            return true;
        }
        public static bool DeleteFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dealer)
        {
            var isExist = context.CustomerBalanceCalculations.Where(x => x.DocNo == dealer.DSIANo && x.DocumentType == "DSIA").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertFromSales(ref BEEContext context,SalesEntryNew salesEntryNew,List<SalesEntryNewLineItem> salesEntryNewLineItems )
        {
            salesEntryNew.SalesDate.Add(DateTime.Now.TimeOfDay);
            var customer = context.Customers.FirstOrDefault(x => x.Id == salesEntryNew.CustomerID);
            //var existingSaleCustBal = context.CustomerBalanceCalculations.Where(x => x.CustomerID == salesEntryNew.CustomerID && x.DocNo == salesEntryNew.InvoiceNo).ToList();
            var existingSaleCustBal = context.CustomerBalanceCalculations.Where(x => (x.DocumentType == "Sales" ||x.DocumentType== "DTD" || x.DocumentType == "DTC") && x.DocNo == salesEntryNew.InvoiceNo).ToList();
            if (existingSaleCustBal.Count>0)
            {
                foreach (var item in existingSaleCustBal)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
           
            if(salesEntryNew.InvoiceAmount>0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Sales").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesEntryNew.CustomerID, salesEntryNew.SalesDate, "Sales", accId, customer.CustomerName, "Sales", salesEntryNew.InvoiceAmount, salesEntryNew.InvoiceNo));
            }
            if(salesEntryNew.DiscountAmt>0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Discount").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesEntryNew.CustomerID, salesEntryNew.SalesDate, "DTD", accId, customer.CustomerName, "Sales", salesEntryNew.DiscountAmt*-1, salesEntryNew.InvoiceNo));
            }
            if (salesEntryNew.CommissionAmt > 0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Commission").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesEntryNew.CustomerID, salesEntryNew.SalesDate, "DTC", accId, customer.CustomerName, "Sales", salesEntryNew.CommissionAmt * -1, salesEntryNew.InvoiceNo));
            }
            return true;
        }

        public static bool DeleteFromSales(ref BEEContext context, SalesEntryNew salesEntryNew, List<SalesEntryNewLineItem> salesEntryNewLineItems)
        {
            //var existingSaleCustBal = context.CustomerBalanceCalculations.Where(x => x.CustomerID == salesEntryNew.CustomerID && x.DocNo == salesEntryNew.InvoiceNo).ToList();
            var existingSaleCustBal = context.CustomerBalanceCalculations.Where(x => (x.DocumentType == "Sales" || x.DocumentType == "DTD" || x.DocumentType == "DTC") && x.DocNo == salesEntryNew.InvoiceNo).ToList();
            if (existingSaleCustBal.Count > 0)
            {
                foreach (var item in existingSaleCustBal)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
            return true;
        }

        internal static void InsertFromSalesReturnEntry(ref BEEContext context, SalesReturnEntry salesReturn)
        {
            //salesReturn.SalesDate.Add(DateTime.Now.TimeOfDay);
            var customer = context.Customers.FirstOrDefault(x => x.Id == salesReturn.CustomerId);
            var existingSaleCustBal = context.CustomerBalanceCalculations.Where(x => x.CustomerID == salesReturn.CustomerId && x.DocNo == salesReturn.SRNo).ToList();
            if (existingSaleCustBal.Count > 0)
            {
                foreach (var item in existingSaleCustBal)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }

            if (salesReturn.TotalRtnAmount > 0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Sales").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesReturn.CustomerId, salesReturn.SRDate, "Sales Return", accId, customer.CustomerName, "Sales Return", salesReturn.TotalRtnAmount*-1, salesReturn.SRNo));
            }
            if (salesReturn.TotalDiscountAmount > 0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Discount").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesReturn.CustomerId, salesReturn.SRDate, "Sales Return", accId, customer.CustomerName, "Sales Return", salesReturn.TotalDiscountAmount, salesReturn.SRNo));
            }
            if (salesReturn.TotalVatAmount > 0)
            {
                int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Trade Vat").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(salesReturn.CustomerId, salesReturn.SRDate, "Sales Return", accId, customer.CustomerName, "Sales Return", salesReturn.TotalVatAmount*-1, salesReturn.SRNo));
            }
        }
        public static bool InsertUpdateFromDPR(ref BEEContext context, ReceivePaymentInfo dpr)
        {
            var existDPR = context.CustomerBalanceCalculations.Where(x => x.DocNo == dpr.RPID && x.DocumentType == "DPR").ToList();
            if (existDPR.Count > 0)
            {
                foreach (var item in existDPR)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
            var customer = context.Customers.FirstOrDefault(x => x.Id == dpr.CustomerID);
            if (dpr.BankCharges > 0)
            {
                int bankCharge = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank Charge").RefValue;
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(dpr.CustomerID, dpr.TDate, "DPR", dpr.AccountID, dpr.Description, "DPR", -(dpr.NetAmount), dpr.RPID));
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(dpr.CustomerID, dpr.TDate, "DPR", bankCharge, dpr.Description, "DPR", -(dpr.BankCharges), dpr.RPID));
            }
            else
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(dpr.CustomerID, dpr.TDate, "DPR", dpr.AccountID, dpr.Description, "DPR", -dpr.ReceiveAmt, dpr.RPID));
            }
           
            return true;
        }
        public static bool DeleteFromDPR(ref BEEContext context, ReceivePaymentInfo dpr)
        {
            var existDPR = context.CustomerBalanceCalculations.Where(x => x.DocNo == dpr.RPID && x.DocumentType == "DPR").ToList();
            if (existDPR.Count > 0)
            {
                foreach (var item in existDPR)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
            return true;
        }

        public static bool InsertFromDAA(ref BEEContext context, DealerAccAdjustment dealer, List<DealerAccAdjustmentLineItem> Line)
        {
            var item = context.CustomerBalanceCalculations.Where(x => x.DocNo == dealer.DAANo && x.DocumentType == "DAA").ToList();
            if (item.Count > 0)
            {
                foreach (var x in item)
                {
                    context.CustomerBalanceCalculations.Remove(x);
                }

            }

            foreach (var d in Line)
            {
                context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(d.DealerID, dealer.DAADate, "DAA", dealer.DAAAccountID, dealer.Description, "", -d.Amount, d.DAANo));
            }

            return true;
        }
        public static bool DeleteFromDAA(ref BEEContext context, DealerAccAdjustment dealer)
        {
            var item = context.CustomerBalanceCalculations.Where(x => x.DocNo == dealer.DAANo && x.DocumentType == "DAA").ToList();
            if (item.Count > 0)
            {
                foreach (var x in item)
                {
                    context.CustomerBalanceCalculations.Remove(x);
                }

            }
            return true;
        }
        public static bool DeleteFromFRR(ref BEEContext context, TblFreezerRent frr)
        {
            var isExist = context.CustomerBalanceCalculations.Where(x => x.DocNo == frr.clmFRRNo && x.DocumentType == "Rent" || x.DocumentType == "Dealer Rent").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.CustomerBalanceCalculations.Remove(item);
                }
            }
            return true;
        }

    }
}