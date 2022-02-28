using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreRM;
using BEEERP.Models.StoreRM.RMSalesEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class RMCustomerCalculationBridge
    {
        
        public static bool InsertFromCreateCustomer(ref BEEContext context, CreateCustomer customer)
        {
            var item = context.RMCustomerBalanceCalculation.FirstOrDefault(x => x.RMCustomerID == customer.clmCustomerID && x.DocType == "RMCOB");
            if (item != null)
            {
                context.RMCustomerBalanceCalculation.Remove(item);
            }
            customer.clmAccOBDate.Add(DateTime.Now.TimeOfDay);
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
            RMCustomerBalanceCalculation rmbc = new RMCustomerBalanceCalculation();
            rmbc.DocNo = customer.clmCustomerID;
            rmbc.RMCustomerID = customer.clmCustomerID;
            rmbc.SalesDate = customer.clmAccOBDate;
            rmbc.DocType = "RMCOB";
            rmbc.SalesAmount = (decimal)customer.clmAccOB;
            rmbc.RefNo = "";
            rmbc.Description = "";
            rmbc.AccountID = accId;
            context.RMCustomerBalanceCalculation.Add(rmbc);
            return true;
        }

        public static bool InsertUpdateFromRMSales(ref BEEContext context, RMSales rms)
        {
            int salesId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Sales").RefValue;
            var isexist = context.RMCustomerBalanceCalculation.Where(x => x.DocType == "RMS" && x.RMCustomerID == rms.clmCustomerID && x.DocNo == rms.clmRMSalesNo).ToList();
            if (isexist.Count > 0)
            {
                context.RMCustomerBalanceCalculation.RemoveRange(isexist);
            }

            RMCustomerBalanceCalculation rmcbc = new RMCustomerBalanceCalculation();
            rmcbc.DocNo = rms.clmRMSalesNo;
            rmcbc.RMCustomerID = rms.clmCustomerID;
            rmcbc.SalesDate = rms.clmDate;
            rmcbc.DocType = "RMS";
            rmcbc.SalesAmount = (decimal)rms.clmRMSTotal;
            if(string.IsNullOrEmpty(rms.clmRefNo))
            {
                rms.clmRefNo = "";
            }

            rmcbc.RefNo = rms.clmRefNo;
            rmcbc.Description = rms.clmDescription==null?"":rms.clmDescription;
            rmcbc.AccountID = salesId;
            context.RMCustomerBalanceCalculation.Add(rmcbc);

            return true;
        }
        internal static bool DeleteFromRMSales(ref BEEContext context, int id)
        {
            var isexist = context.RMCustomerBalanceCalculation.Where(x => x.DocType == "RMS" && x.DocNo == id).ToList();
            if (isexist.Count > 0)
            {
                context.RMCustomerBalanceCalculation.RemoveRange(isexist);
            }
            

            return true;
        }
    }

    
}