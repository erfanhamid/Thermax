
using BEEERP.Models.CostingModule;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public static class BatchBalanceCalculationBridge
    {
        //public static bool InsertUpdateFromBatchCostAllocation(ref BEEContext context, BatchAllocation batchallocation, List<BatchAllocationLineItem> batchallocationlineitem)
        //{
        //    var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "BCA" && x.DocNo == batchallocation.AllocationNo).ToList();
        //    if (isExist.Count > 0)
        //    {
        //        context.BatchBalanceCalculations.RemoveRange(isExist);
        //    }
        //    foreach (var item in batchallocationlineitem)
        //    {
        //        context.BatchBalanceCalculations.Add(new BatchBalanceCalculation(batchallocation.AllocationNo, batchallocation.BatchId, item.Account, "BCA", batchallocation.AllocationDate, (-1) * item.AllocateAmount, item.ReferenceNo, item.Description));
        //    }
        //    return true;
        //}
        //public static bool DeleteFromBatchCostAllocation(ref BEEContext context, int allocationno)
        //{
        //    var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "BCA" && x.DocNo == allocationno).ToList();
        //    if (isExist.Count > 0)
        //    {
        //        context.BatchBalanceCalculations.RemoveRange(isExist);
        //    }
        //    return true;
        //}
        public static bool InsertUpdateFromRMCBatch(ref BEEContext context, RMCEntry rmcentry, List<RMCLineItem> rmclineitem)
        {
            decimal totalamount = 0;
            int iGLid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "RMC" && x.DocNo == rmcentry.RMCNo && x.BatchId==rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            foreach (var item in rmclineitem)
            {
                totalamount += item.TotalStanCost;
            }
            context.BatchBalanceCalculations.Add(new BatchBalanceCalculation(rmcentry.RMCNo, rmcentry.BatchID, iGLid, "RMC", rmcentry.RMCDate, totalamount, rmcentry.RefNo, rmcentry.Descriptions));
            return true;
        }
        public static bool DeleteFromRMCBatch(ref BEEContext context, RMCEntry rmcentry)
        {
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "RMC" && x.DocNo == rmcentry.RMCNo && x.BatchId == rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            return true;
        }

        public static bool InsertUpdateFromARMCBatch(ref BEEContext context, RMCApplyEntry rmcentry, List<RMCApplyLineItem> rmclineitem)
        {
            decimal totalamount = 0;
            int iGLid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "RM Inventory GL").RefValue;
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "ARMC" && x.DocNo == rmcentry.RMCNo && x.BatchId == rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            foreach (var item in rmclineitem)
            {
                totalamount += item.RmcRate;
            }
            context.BatchBalanceCalculations.Add(new BatchBalanceCalculation(rmcentry.RMCNo, rmcentry.BatchID, iGLid, "ARMC", rmcentry.RMCDate, totalamount, rmcentry.RefNo, rmcentry.Descriptions));
            return true;
        }
        public static bool DeleteFromARMCBatch(ref BEEContext context, RMCApplyEntry rmcentry)
        {
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "ARMC" && x.DocNo == rmcentry.RMCNo && x.BatchId == rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            return true;
        }

        public static bool InsertUpdateFromRMCJob(ref BEEContext context, RMCEntry rmcentry, List<RMCLineItem> rmclineitem)
        {
            decimal totalamount = 0;
            int iGLid = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Inventory GL").RefValue;
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "RMC" && x.DocNo == rmcentry.RMCNo && x.BatchId == rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            foreach (var item in rmclineitem)
            {
                totalamount += item.TotalStanCost;
            }
            context.BatchBalanceCalculations.Add(new BatchBalanceCalculation(rmcentry.RMCNo, rmcentry.BatchID, iGLid, "RMC", rmcentry.RMCDate, totalamount, rmcentry.RefNo, rmcentry.Descriptions));
            return true;
        }
        public static bool DeleteFromRMCJob(ref BEEContext context, RMCEntry rmcentry, RMCLineItem rmclineitem)
        {
            var isExist = context.BatchBalanceCalculations.Where(x => x.DocType == "RMC" && x.DocNo == rmcentry.RMCNo && x.BatchId == rmcentry.BatchID).ToList();
            if (isExist.Count > 0)
            {
                context.BatchBalanceCalculations.RemoveRange(isExist);
            }
            return true;
        }
    }
}