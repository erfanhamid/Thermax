using BEEERP.Models.Database;
using BEEERP.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;


namespace BEEERP.Models.CommonInformation
{
    public static class SaleCommonInfo
    {
        private static BEEContext context;
        //public static double GetRemainItemQty(int itemId,int storeId, DateTime? Date)
        //{
        //    context = new BEEContext();
        //    double qty = 0.0;
        //    context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeId && x.TDate <= Date).ToList().ForEach(x=> {
        //        if (x.TRType == 1)
        //        {
        //            qty += x.Qty;
        //        }
        //        else {
        //            qty -= x.Qty;
        //        } });
        //    return Math.Round(qty,3);
        //}
        public static decimal GetRemainItemQty(int itemId, int storeId, DateTime? Date)
        {
            context = new BEEContext();
            decimal qty = 0;
            context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeId).ToList().ForEach(x => {
                if (x.TRType == 1)
                {
                    qty += x.Qty;
                }
                else
                {
                    qty -= x.Qty;
                }
            });
            return qty;
        }
        public static decimal GetAvarageRate(int itemId, int storeId, DateTime? Date)
        {
            context = new BEEContext();
            decimal avgRate = 0;
            //select sum(i.Amount* TrType)/ Sum(I.qty * trtype) from InventoryTransaction i Where CIID = 81 and StoreID = 25
            context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeId).ToList().ForEach(x =>
            {
                avgRate = Convert.ToDecimal(x.Amount * x.TRType) / Convert.ToDecimal(x.Qty * x.TRType);
            });
            return avgRate;
        }
        //public static double GetRemainItemQtyByDate(int itemId, int storeId, DateTime Date)
        //{
        //    context = new BEEContext();
        //    double qty = 0.0;
        //    context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeId && x.TDate <= Date).ToList().ForEach(x => {
        //        if (x.TRType == 1)
        //        {
        //            qty += x.Qty;
        //        }
        //        else
        //        {
        //            qty -= x.Qty;
        //        }
        //    });
        //    return qty;
        //}
        public static decimal GetDueByCustomerId(int id)
        {
            context = new BEEContext();
            decimal amount = context.CustomerBalanceCalculations.Where(x => x.CustomerID == id).ToList().Sum(x => x.Amount);
            return Math.Round(amount, 2);
        }
        public static int ApproveSaleByNotificationNo(int notificationId)   
        {
            try
            {
                string userId = FindObjectById.GetLoggedUserId();
                var isApproveSale = context.ApprovalLog.FirstOrDefault(x => x.UserId == userId && x.NotificationId == notificationId);

                if (isApproveSale == null)
                {
                    return 0;
                }
                else
                {
                    using (context)
                    {
                        ApprovalLog alog = new ApprovalLog();
                        alog.IsApproved = true;
                        alog.IsView = true;

                        context.ApprovalLog.Add(alog);
                        context.SaveChanges();

                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                return 0;
            }

        }
        public static string GetGroupName(int id)
        {
            context = new BEEContext();
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }

        public static string GetUoMName(int id)
        {
            context = new BEEContext();
            var item = context.UOM.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }

        public static decimal GetRemainItemQtyByDate(int itemId, int storeId, DateTime date)
        {
            context = new BEEContext();
            date = date.Add(DateTime.Now.TimeOfDay);
            decimal qty = 0;
            context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeId && x.TDate <= date).ToList().ForEach(x => {
                if (x.TRType == 1)
                {
                    qty += x.Qty;
                }
                else
                {
                    qty -= x.Qty;
                }
            });
            return qty;
        }
        public static decimal GetRemainingQtyForRMC(int itemId)
        {
            context = new BEEContext();
            //var sysset = context.SysSet.ToList().FirstOrDefault();
            var storeid = context.Store.FirstOrDefault(x => x.Name == "Floor RM").Id;
            decimal qty = 0;
            context.InventoryTransaction.Where(x => x.CIID == itemId && x.StoreID == storeid).ToList().ForEach(x => {
                if (x.TRType == 1)
                {
                    qty += x.Qty;
                }
                else
                {
                    qty -= x.Qty;
                }
            });
            return qty;
        }

    }
}