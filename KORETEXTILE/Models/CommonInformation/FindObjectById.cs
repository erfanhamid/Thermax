using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Notification;
using BEEERP.Models.HRModule;
using Microsoft.AspNet.Identity;
using BEEERP.Models.Store_FG;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.Procurement.WorkOrder1;
using BEEERP.Models.Procurement;
using BEEERP.Models.AccountModule;
using BEEERP.Models.TaxCalculator;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.StoreRM;

namespace BEEERP.Models.CommonInformation
{
    public static class FindObjectById
    {
        public static RetailerInformation RetailerSearch(int id)
        {
            context = new BEEContext();
            return context.RetailerInformation.FirstOrDefault(x => x.Id == id);
        }
        static BEEContext context;
        public static CreateCustomer RMCustomerSearch(int id)
        {
            context = new BEEContext();
            return context.CreateCustomer.FirstOrDefault(x => x.clmCustomerID == id);
        }
        public static Customer CustomerSearch(int id)
        {
            context = new BEEContext();
            return context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public static TblZone ZoneSearch(int id)
        {
            context = new BEEContext();
            return context.TblZones.FirstOrDefault(x => x.ZoneId == id);
        }

        public static BranchInformation BranchSearch(int id)
        {
            context = new BEEContext();
            return context.BranchInformation.FirstOrDefault(x => x.Slno == id);
        }

        public static ChartOfAccount ChartOfAccountSearch(int id)
        {
            context = new BEEContext();
            return context.ChartOfAccount.FirstOrDefault(x => x.Id == id);
        }
     
        public static UserNotification UserNotificationSearch(int id)
        {
            context = new BEEContext();
            return context.Notification.FirstOrDefault(x => x.Id == id);
        }
        
        public static Customer GetCustByStoreAndCustId(int depotId,int custId)
        {
            context = new BEEContext();
            return context.Customers.FirstOrDefault(x => x.Id == custId&&x.DepotId==depotId);
        }
        public static string GetCustNameByStoreAndCustId(int depotId, int custId)
        {
            context = new BEEContext();
            return context.Customers.FirstOrDefault(x => x.Id == custId && x.DepotId == depotId).CustomerName;
        }

        internal static Employee GetEmpByDepotANdEmpId(int depotId, int empId)
        {
            context = new BEEContext();
            return context.Employees.FirstOrDefault(x => x.Id == empId && x.BranchID == depotId);
        }
        internal static Designations SearchDesignation(int desigId)
        {
            context = new BEEContext();
            return context.Designation.FirstOrDefault(x => x.Id == desigId);
        }
        internal static decimal PrevBalance(int empId)
        {
            context = new BEEContext();
            
            decimal amt = 0;
            decimal amount = (from mr in context.MoneyRequisitionBalanceCalculations where mr.EmpID == empId select mr.Amount).ToList().Sum();
            //decimal amount = context.MoneyRequisitionBalanceCalculations.Where(x => x.EmpID == empId).ToList().Sum(x => x.Amount);
            //List<MoneyRequisitionBalanceCalculation> money = context.MoneyRequisitionBalanceCalculations.Where(x => x.EmpID == empId).ToList();
            if (amount != null)
            {
                amt = amount;
            }
            else
            {
                amt = 0;
            }
            return amt;
        }
        internal static Item_Info GetChartOfInventoryById(int id)
        {
            context = new BEEContext();

            return LoadComboBox.GetGSItemInfo().FirstOrDefault(x => x.Id == id);
        }
        internal static ItemInfoSP GetFreezerDescriptionModelById(int id)
        {
            context = new BEEContext();

            return LoadComboBox.GetFreezerItemInfo().FirstOrDefault(x => x.ID == id);
        }
        internal static Retailer_Info GetRetailerInfoById(int id)
        {
            context = new BEEContext();

            return LoadComboBox.GetRetailerInfo().FirstOrDefault(x => x.Id == id);
        }
        internal static Customer_Info GetCustomerInfoById(int id)
        {
            context = new BEEContext();

            return LoadComboBox.GetCustInfo().FirstOrDefault(x => x.Id == id);
        }
        public static string GetLoggedUserId()
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            return userId;
        }

        public static DateTime GetCompanySetup()
        {
            context = new BEEContext();
            var companySetup = context.CompanySetup.FirstOrDefault(x => x.ID == 1);
            DateTime date = companySetup.StartDate;
            return date;
        }

        internal static Employee GetEmployeeById(int id)
        {
            context = new BEEContext();
            var emp = context.Employees.ToList().Find(x => x.Id == id);
            return emp;
        }

        internal static UOM GetUomById(int id)  
        {
            context = new BEEContext();
            return context.UOM.FirstOrDefault(x => x.Id == id);
        }
        internal static ChartOfInventory GetGroupNameById(int id)
        {
            context = new BEEContext();
            return context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
        }

        //internal static SIFD GetSifdById(int id)
        //{
        //    context = new BEEContext();
        //    return context.SIFD.FirstOrDefault(x => x.ChallanNo == id);
        //}

        internal static GoodsReceiveNote GetGRNById(int id) 
        {
            context = new BEEContext();
            return context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == id);
        }

        internal static FreezerWorkOrder GetFWOById(int id)
        {
            context = new BEEContext();
            return context.FreezerWorkOrder.FirstOrDefault(x => x.WONo == id);
        }

        internal static WorkOrder GetWOById(int id)     
        {
            context = new BEEContext();
            return context.WorkOrder.FirstOrDefault(x => x.WONo == id);
        }
        public static Store GetStoreById(int id)
        {
            context = new BEEContext();
            return context.Store.FirstOrDefault(x => x.Id == id);
        }
        public static bool IsCustomerInvoiceDayLimiteExceed(int customerId)
        {
            return context.Database.SqlQuery<bool>("exec GetMaximumInvoiceDaysCount "+customerId+"").FirstOrDefault();
        }
        internal static List<ERMTransection> GetERMTransection()    
        {
            context = new BEEContext();
            var data = context.Database.SqlQuery<ERMTransection>("SELECT [xpaydate],[xemp],[xprorg],[xprloc],[xdesig],[xconfirmationDate],[xbasic],[xpaycode] ,[xamount]FROM[dbo].[ERM_TRANSECTION]").ToList();
            return data;
        }
        internal static decimal SupplierDueAmount(int supId)
        {
            context = new BEEContext();
            return context.SupplierBalanceCalculation.Where(x => x.SupplierID == supId).ToList().Select(x => x.Amount).DefaultIfEmpty(0).Sum();
        }
        public static ImportLC GetILCNoById(int id)
        {
            context = new BEEContext();
            return context.ImportLC.FirstOrDefault(x => x.ILCId == id);
        }
        
    }
}