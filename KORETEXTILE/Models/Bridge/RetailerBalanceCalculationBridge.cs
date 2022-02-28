using BEEERP.Models.Database;
using BEEERP.Models.FreezerRent;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class RetailerBalanceCalculationBridge
    {
        public static bool InsertToCustomerBalanceCalculation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var item = context.CustomerBalanceCalculations.FirstOrDefault(x => x.CustomerID == retailer.Id && x.DocumentType == retailer.DocType);
            if (item != null)
            {
                context.CustomerBalanceCalculations.Remove(item);
            }

            var date = context.CompanySetup.FirstOrDefault(x => x.ID == 1).StartDate;
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            context.CustomerBalanceCalculations.Add(new CustomerBalanceCalculation(retailer.Id, date, retailer.DocType, accId, "OB", retailer.RetailerName, Convert.ToDecimal(retailer.OB), retailer.Id));
            return true;
        }

        internal static bool DeleteFromCustomerBalanceCalculation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var items = context.CustomerBalanceCalculations.Where(x => x.DocumentType == retailer.DocType && x.DocNo == retailer.Id).ToList();
            foreach (var item in items)
            {
                context.CustomerBalanceCalculations.Remove(item);
            }
            return true;
        }

        public static bool InsertToInstallmentBalanceCalculation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var item = context.InstallmentBalanceCalculation.FirstOrDefault(x => x.RetailerID == retailer.Id && x.DocumentType == retailer.DocType);
            if (item != null)
            {
                context.InstallmentBalanceCalculation.Remove(item);
            }
            
            var date = context.CompanySetup.FirstOrDefault(x => x.ID == 1).StartDate;
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            context.InstallmentBalanceCalculation.Add(new InstallmentBalanceCalculation(retailer.Id, date, retailer.DocType, accId, "OB", retailer.RetailerName, Convert.ToDecimal(retailer.OB), retailer.Id));
            return true;
        }

        public static bool InsertUpdateFromFRI(ref BEEContext context, FRI fri, List<FRILineItem> addedItems)
        {
            var items = context.InstallmentBalanceCalculation.Where(x => (x.DocumentType == "RFRI" || x.DocumentType == "DFRI") && x.DocNo == fri.clmFRINo).ToList();
            foreach (var item in items)
            {
                context.InstallmentBalanceCalculation.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.InstallmentBalanceCalculation.Add(new InstallmentBalanceCalculation()
                {
                    RetailerID = item.clmRetailerID,
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

        public static bool InsertUpdateFromFARR(ref BEEContext context, FARR farr, List<FARRLineItem> addedItems)
        {
            var items = context.InstallmentBalanceCalculation.Where(x => (x.DocumentType == "RFRR" || x.DocumentType == "DFRR") && x.DocNo == farr.clmFARRNo).ToList();
            foreach (var item in items)
            {
                context.InstallmentBalanceCalculation.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.InstallmentBalanceCalculation.Add(new InstallmentBalanceCalculation()
                {
                    RetailerID = item.clmRetailerID,
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

        public static bool InsertUpdateFromFRR(ref BEEContext context, TblFreezerRent frr, List<TblFreezerRentLineItem> addedItems)
        {
            var items = context.InstallmentBalanceCalculation.Where(x => (x.DocumentType =="Rent" || x.DocumentType == "Dealer Rent") && x.DocNo == frr.clmFRRNo).ToList();
            foreach (var item in items)
            {
                context.InstallmentBalanceCalculation.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.InstallmentBalanceCalculation.Add(new InstallmentBalanceCalculation()
                {
                    RetailerID = item.clmRetailerID,
                    TDate = frr.clmRPDate,
                    DocumentType = item.clmDealerYN,
                    AccountID = frr.clmAccountID,
                    TrDesc = item.clmDescription,
                    RefNo = item.clmRef,
                    Amount = Convert.ToDecimal(item.clmAmount)*-1,
                    DocNo = frr.clmFRRNo,
                });
            }
            return true;
        }

        internal static bool DeleteFromInstallmentBalanceCalculation(ref BEEContext context, RetailerInformation retailer)
        {
            if (retailer.DealerYesNo == "Yes")
            {
                retailer.DocType = "Dealer Rent OB";
            }
            else
            {
                retailer.DocType = "Rent OB";
            }
            var items = context.InstallmentBalanceCalculation.Where(x => x.DocumentType == retailer.DocType && x.DocNo == retailer.Id).ToList();
            foreach (var item in items)
            {
                context.InstallmentBalanceCalculation.Remove(item);
            }
            return true;
        }
        internal static bool DeleteFromFRR(ref BEEContext context, TblFreezerRent frr)
        {
            var isExist = context.InstallmentBalanceCalculation.Where(x => x.DocNo == frr.clmFRRNo && x.DocumentType == "Rent" || x.DocumentType == "Dealer Rent").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.InstallmentBalanceCalculation.Remove(item);
                }
            }
            return true;
        }
        internal static bool DeleteFromFRI(ref BEEContext context, FRI fri)
        {
            var isExist = context.InstallmentBalanceCalculation.Where(x => x.DocNo == fri.clmFRINo && x.DocumentType == "DFRI" || x.DocumentType == "RFRI").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.InstallmentBalanceCalculation.Remove(item);
                }
            }
            return true;
        }
        internal static bool DeleteFromFARR(ref BEEContext context, FARR farr)
        {
            var isExist = context.InstallmentBalanceCalculation.Where(x => x.DocNo == farr.clmFARRNo && x.DocumentType == "DFRR" || x.DocumentType == "RFRR").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.InstallmentBalanceCalculation.Remove(item);
                }
            }
            return true;
        }

    }
}