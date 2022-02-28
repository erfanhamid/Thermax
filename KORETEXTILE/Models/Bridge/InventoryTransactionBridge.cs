using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Store_FG;
using BEEERP.Models.StoreDepot;
using BEEERP.Models.StoreRM;
using BEEERP.Models.SpareParts;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.StoreRM.IRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEEERP.Models.CostingModule;
using BEEERP.Models.InventoryModule;
using BEEERP.Models.GeneralStore;

namespace BEEERP.Models.Bridge
{
    public static class InventoryTransactionBridge
    {
        public static bool InsertFromFGProduction(ref BEEContext context,List<FGProductionLineItem> fGProductionLine,FGProduction fGProduction)
        {
            var items=context.InventoryTransaction.Where(x => x.TRType == 1 && x.DocType == "FGP" && x.DocID == fGProduction.FGPNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);
                
            }
            foreach(var item in fGProductionLine)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction() {
                    CIID =item.ItemID,Qty=Convert.ToDecimal(item.Qty),Rate=item.CogsRate,Amount=item.CogsTotal,TRType=1,TDate= fGProduction.FGPDate,StoreID=fGProduction.StoreID,
                    DocType="FGP",DocID=fGProduction.FGPNo
                });
            }
            
            return true;

        }
        public static bool InsertFromRMSalesEntry(ref BEEContext context, RMSales rms, List<TblRMSalesLineItem> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RMS" && x.DocID == rms.clmRMSalesNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.clmItemID,
                    Qty = Convert.ToDecimal(item.clmItemQty),
                    Rate = item.clmCOGSRate,
                    Amount = item.clmCOGSTotal,
                    TRType = -1,
                    TDate = rms.clmDate,
                    StoreID = rms.clmStoreID,
                    DocType = "RMS",
                    DocID = rms.clmRMSalesNo
                });
            }

            return true;

        }
        //public static bool InsertFromSPR(ref BEEContext context, StorePurchaseRequistion spr, List<StorePurchaseRequisitionLineItem> addedItem)
        //{
        //    var items = context.InventoryTransaction.Where(x => x.DocType == "RMS" && x.DocID == rms.clmRMSalesNo).ToList();
        //    foreach (var item in items)
        //    {
        //        context.InventoryTransaction.Remove(item);

        //    }
        //    foreach (var item in addedItems)
        //    {
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.clmItemID,
        //            Qty = Convert.ToDecimal(item.clmItemQty),
        //            Rate = item.clmCOGSRate,
        //            Amount = item.clmCOGSTotal,
        //            TRType = -1,
        //            TDate = rms.clmDate,
        //            StoreID = rms.clmStoreID,
        //            DocType = "RMS",
        //            DocID = rms.clmRMSalesNo
        //        });
        //    }

        //    return true;

        //}
        internal static bool DeleteRMSalesEntry(ref BEEContext context,int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RMS" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            

            return true;

        }
        //public static bool InsertFromFGRPEntry(ref BEEContext context, List<FGRPLineItem> fGRPLineItem, FGRP fGRP,int fromStore,int toStore)
        //{
        //    var items = context.InventoryTransaction.Where(x => x.DocType == "IFGS" && x.DocID == fGRP.FGRPNo).ToList();
        //    foreach (var item in items)
        //    {
        //        context.InventoryTransaction.Remove(item);

        //    }
        //    foreach (var item in fGRPLineItem)
        //    {
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.ItemID,
        //            Qty = Convert.ToDecimal(item.FGRPQty),
        //            Rate = item.CostRt,
        //            Amount = item.CostVal,
        //            TRType = -1,
        //            TDate = fGRP.FGRPDate,
        //            StoreID = fromStore,
        //            DocType = "FGRP",
        //            DocID = fGRP.FGRPNo
        //        });
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.ItemID,
        //            Qty = Convert.ToDecimal(item.FGRPQty),
        //            Rate = item.CostRt,
        //            Amount = item.CostVal,
        //            TRType = 1,
        //            TDate = fGRP.FGRPDate,
        //            StoreID = toStore,
        //            DocType = "FGRP",
        //            DocID = fGRP.FGRPNo
        //        });
        //    }

        //    return true;

        //}
        //public static bool InsertFromIFGSEntry(ref BEEContext context, IssueFGStore issuFGStore, List<IssueFGStoreLineItem> addedItems)
        //{
        //    var items = context.InventoryTransaction.Where(x => x.DocType == "IFGS" && x.DocID == issuFGStore.clmIFGSID).ToList();
        //    foreach (var item in items)
        //    {
        //        context.InventoryTransaction.Remove(item);

        //    }
        //    foreach (var item in addedItems)
        //    {
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.clmItemID,
        //            Qty = Convert.ToDecimal(item.clmQty),
        //            Rate = item.clmCostRt,
        //            Amount = item.clmCostVal,
        //            TRType = -1,
        //            TDate = issuFGStore.clmIFGSDate,
        //            StoreID = issuFGStore.IssueFrom,
        //            DocType = "IFGS",
        //            DocID = issuFGStore.clmIFGSID
        //        });
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.clmItemID,
        //            Qty = Convert.ToDecimal(item.clmQty),
        //            Rate = item.clmCostRt,
        //            Amount = item.clmCostVal,
        //            TRType = 1,
        //            TDate = issuFGStore.clmIFGSDate,
        //            StoreID = issuFGStore.IssueTo,
        //            DocType = "IFGS",
        //            DocID = issuFGStore.clmIFGSID
        //        });
        //    }

        //    return true;

        //}

        public static bool InsertFromFGRPEntry(ref BEEContext context, List<FGRPLineItem> fGRPLineItem, FGRP fGRP, int fromStore, int toStore)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "IFGS" && x.DocID == fGRP.FGRPNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in fGRPLineItem)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.FGRPQty),
                    Rate = item.CostRt,
                    Amount = item.CostVal,
                    TRType = -1,
                    TDate = fGRP.FGRPDate,
                    StoreID = fromStore,
                    DocType = "IFGS",
                    DocID = fGRP.FGRPNo
                });
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.FGRPQty),
                    Rate = item.CostRt,
                    Amount = item.CostVal,
                    TRType = 1,
                    TDate = fGRP.FGRPDate,
                    StoreID = toStore,
                    DocType = "IFGS",
                    DocID = fGRP.FGRPNo
                });
            }

            return true;

        }

        public static bool InsertFromIFGSEntry(ref BEEContext context, IssueFGStore issuFGStore, List<IssueFGStoreLineItem> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "IFGS" && x.DocID == issuFGStore.clmIFGSID).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in addedItems)
            //{
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.clmItemID, Convert.ToDecimal(item.clmQty), item.clmCostRt, item.clmCostVal, -1, issuFGStore.clmIFGSDate, issuFGStore.IssueFrom, "IFGS", issuFGStore.clmIFGSID));
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.clmItemID, Convert.ToDecimal(item.clmQty), item.clmCostRt, item.clmCostVal, 1, issuFGStore.clmIFGSDate, issuFGStore.IssueTo, "IFGS", issuFGStore.clmIFGSID));
            //}

            return true;

        }
        public static bool DeleteFromIFGSEntry(ref BEEContext context, IssueFGStore issuFGStore)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "IFGS" && x.DocID == issuFGStore.clmIFGSID).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;
        }
        public static bool InsertFromIRMEntry(ref BEEContext context, List<IRMLineItem> irmLineItem, IRM irm)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "IRMP" && x.DocID == irm.IRMID).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in irmLineItem)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.Qty),
                    Rate = item.Rate,
                    Amount = item.Value,
                    TRType = -1,
                    TDate = irm.IRMDate,
                    StoreID = irm.IssueFrom,
                    DocType = "IRMP",
                    DocID = irm.IRMID
                });
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.Qty),
                    Rate = item.Rate,
                    Amount = item.Value,
                    TRType = 1,
                    TDate = irm.IRMDate,
                    StoreID = irm.IssueTo,
                    DocType = "IRMP",
                    DocID = irm.IRMID
                });
            }

            return true;

        }

        public static bool InsertFromRMCEntry(ref BEEContext context, List<RMCLineItem> rmcLineItem, RMCEntry rmc)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RMC" && x.DocID == rmc.RMCNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in rmcLineItem)
            {
                
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = item.UnitStanCost,
                    Amount = item.TotalStanCost,
                    TRType = -1,
                    TDate = rmc.RMCDate,
                    StoreID = rmc.StoreId,
                    DocType = "RMC",
                    DocID = rmc.RMCNo
                });
            }

            return true;

        }
        public static bool InsertUpdateFromRMCEntry(ref BEEContext context, List<RMCLineItem> rmcLineItem, RMCEntry rmc)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RMC" && x.DocID == rmc.RMCNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in rmcLineItem)
            {

                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = item.UnitStanCost,
                    Amount = item.TotalStanCost,
                    TRType = -1,
                    TDate = rmc.RMCDate,
                    StoreID = rmc.StoreId,
                    DocType = "RMC",
                    DocID = rmc.RMCNo
                });
            }

            return true;

        }
        public static bool DeleteFromRMCEntry(ref BEEContext context, int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RMC" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in receiveNoteLineItems)
            //{
            //    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            //    {
            //        CIID = item.ItemID,
            //        Qty = Convert.ToDecimal(item.Qty),
            //        Rate = item.CostRt,
            //        Amount = (decimal)item.CostVal,
            //        TRType = 1,
            //        TDate = goodsReceiveNotes.GRNDate,
            //        StoreID = goodsReceiveNotes.StoreID,
            //        DocType = "GRN",
            //        DocID = goodsReceiveNotes.GRNNo
            //    });

            //}

            return true;

        }

        public static bool InsertFromARMCEntry(ref BEEContext context, List<RMCApplyLineItem> rmcLineItem, RMCApplyEntry rmc)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "ARMC" && x.DocID == rmc.RMCNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in rmcLineItem)
            {

                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = item.RmcRate,
                    Amount = item.RmcValue,
                    TRType = -1,
                    TDate = rmc.RMCDate,
                    StoreID = rmc.StoreId,
                    DocType = "ARMC",
                    DocID = rmc.RMCNo
                });
            }

            return true;

        }
        public static bool DeleteFromARMCEntry(ref BEEContext context, int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "ARMC" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in receiveNoteLineItems)
            //{
            //    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            //    {
            //        CIID = item.ItemID,
            //        Qty = Convert.ToDecimal(item.Qty),
            //        Rate = item.CostRt,
            //        Amount = (decimal)item.CostVal,
            //        TRType = 1,
            //        TDate = goodsReceiveNotes.GRNDate,
            //        StoreID = goodsReceiveNotes.StoreID,
            //        DocType = "GRN",
            //        DocID = goodsReceiveNotes.GRNNo
            //    });

            //}

            return true;

        }

        internal static void InsertFromSalesRetrunEntry(ref BEEContext context, SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "SalesReturn" && x.DocID == salesReturn.SRNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.ReturnQty + item.OfferQty),
                    Rate = item.Price,
                    Amount = item.Value,
                    TRType = 1,
                    TDate = salesReturn.SRDate,
                    StoreID = salesReturn.Store,
                    DocType = "SalesReturn",
                    DocID = salesReturn.SRNo
                });
            }

            
        }


        public static bool InsertUpdateFromGSInventoryIssue(ref BEEContext context, GSInventoryIssue gii, List<GSInventoryIssueLineItem> gsiiLineItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "GSMI" && x.DocID == gii.GSIIssueNo).ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    context.InventoryTransaction.Remove(item);

                }
            }


            foreach (var item in gsiiLineItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = Convert.ToDecimal(item.ItemRate),
                    //item.ItemRate,
                    Amount = Convert.ToDecimal(item.ItemValue),
                    TRType = -1,
                    TDate = gii.GSIssueDate,
                    StoreID = gii.StoreID,
                    CompanyID = gii.CompanyID,
                    DocType = "GSMI",
                    DocID = gii.GSIIssueNo
                });
            }

            return true;

        }
        public static bool DeleteFromGSInventoryIssue(ref BEEContext context, int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "GSMI" && x.DocID == id).ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    context.InventoryTransaction.Remove(item);

                }
            }


    

            return true;


        }

        //internal static bool InsertFromFGRMEntry(ref BEEContext context, GRF grn, List<GRFLineItem> gRFLineItems)
        //{
        //    var items = context.InventoryTransaction.Where(x => x.DocType == "GRF" && x.DocID == grn.GRFNo).ToList();
        //    foreach (var item in items)
        //    {
        //        context.InventoryTransaction.Remove(item);

        //    }
        //    foreach (var item in gRFLineItems)
        //    {
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.ItemID,
        //            Qty = Convert.ToDecimal(item.Qty),
        //            Rate = item.Rate,
        //            Amount = item.Cost,
        //            TRType = -1,
        //            TDate = grn.ReceiveDate,
        //            StoreID = grn.IssueFrom,
        //            DocType = "GRF",
        //            DocID = grn.GRFNo
        //        });
        //        context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
        //        {
        //            CIID = item.ItemID,
        //            Qty = Convert.ToDecimal(item.Qty),
        //            Rate = item.Rate,
        //            Amount = item.Cost,
        //            TRType = 1,
        //            TDate = grn.ReceiveDate,
        //            StoreID = grn.IssueTo,
        //            DocType = "GRF",
        //            DocID = grn.GRFNo
        //        });
        //    }
        //    return true;
        //}

        public static bool InsertFromFRNEntry(ref BEEContext context, GoodsReceiveNote goodsReceiveNotes,List<GoodsReceiveNoteLineItem> receiveNoteLineItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "FRN" && x.DocID == goodsReceiveNotes.GRNNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in receiveNoteLineItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.Qty),
                    Rate = item.CostRt,
                    Amount = (decimal)item.CostVal,
                    TRType = 1,
                    TDate = goodsReceiveNotes.GRNDate,
                    CompanyID = goodsReceiveNotes.CompanyID,
                    DocType = "FRN",
                    DocID = goodsReceiveNotes.GRNNo
                });
               
            }

            return true;

        }
        public static bool DeleteFromFRNEntry(ref BEEContext context, int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "FRN" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in receiveNoteLineItems)
            //{
            //    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            //    {
            //        CIID = item.ItemID,
            //        Qty = Convert.ToDecimal(item.Qty),
            //        Rate = item.CostRt,
            //        Amount = (decimal)item.CostVal,
            //        TRType = 1,
            //        TDate = goodsReceiveNotes.GRNDate,
            //        StoreID = goodsReceiveNotes.StoreID,
            //        DocType = "GRN",
            //        DocID = goodsReceiveNotes.GRNNo
            //    });

            //}

            return true;

        }

        public static bool InsertFromIOB(ref BEEContext context, InventoryOpeningBalance ob)
        {

            context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            {
                CIID = ob.ItemId,
                Qty = ob.ItemQty,
                Rate = ob.ItemRate,
                Amount = ob.ItemValue,
                TRType = 1,
                TDate = ob.IOBDate,
                StoreID = ob.StoreId,
                CompanyID = ob.CompanyID,
                DocType = "IOB",
                DocID = ob.ItemId
            });

            return true;


        }

        public static bool UpdateFromIOB(ref BEEContext context, InventoryOpeningBalance ob)
        {

            var iobinfo = context.InventoryTransaction.FirstOrDefault(x => x.DocType == "IOB" && x.DocID == ob.ItemId);


            if (iobinfo == null)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = ob.ItemId,
                    Qty = ob.ItemQty,
                    Rate = ob.ItemRate,
                    Amount = ob.ItemValue,
                    TRType = 1,
                    TDate = ob.IOBDate,
                    StoreID = ob.StoreId,
                    CompanyID = ob.CompanyID,
                    DocType = "IOB",
                    DocID = ob.ItemId
                });
            }

            context.InventoryTransaction.Remove(iobinfo);

            context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            {
                CIID = ob.ItemId,
                Qty = ob.ItemQty,
                Rate = ob.ItemRate,
                Amount = ob.ItemValue,
                TRType = 1,
                TDate = ob.IOBDate,
                StoreID = ob.StoreId,
                CompanyID = ob.CompanyID,
                DocType = "IOB",
                DocID = ob.ItemId
            });

            return true;


        }

        public static bool DeleteFromIOB(ref BEEContext context, int id)
        {

            var iobinfo = context.InventoryTransaction.FirstOrDefault(x => x.DocType == "IOB" && x.DocID == id);


            if (iobinfo != null)
            {
                context.InventoryTransaction.Remove(iobinfo);
            }

            
         
            


            return true;


        }

        public static bool InsertFromGRNEntry(ref BEEContext context, GoodsReceiveNote goodsReceiveNotes,List<GoodsReceiveNoteLineItem> receiveNoteLineItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "GRN" && x.DocID == goodsReceiveNotes.GRNNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in receiveNoteLineItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.Qty),
                    //Qty = Convert.ToDecimal(item.Qty),
                    //Qty = (decimal)item.Qty,
                    Rate = item.CostRt,
                    Amount = (decimal)item.CostVal,
                    TRType = 1,
                    TDate = goodsReceiveNotes.GRNDate,
                    CompanyID = goodsReceiveNotes.CompanyID,
                    DocType = "GRN",
                    DocID = goodsReceiveNotes.GRNNo
                });
               
            }

            return true;

        }
        public static bool DeleteFromGRNEntry(ref BEEContext context, int id)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "GRN" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in receiveNoteLineItems)
            //{
            //    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            //    {
            //        CIID = item.ItemID,
            //        Qty = Convert.ToDecimal(item.Qty),
            //        Rate = item.CostRt,
            //        Amount = (decimal)item.CostVal,
            //        TRType = 1,
            //        TDate = goodsReceiveNotes.GRNDate,
            //        StoreID = goodsReceiveNotes.StoreID,
            //        DocType = "GRN",
            //        DocID = goodsReceiveNotes.GRNNo
            //    });

            //}

            return true;

        }
        public static bool InsertFromSampleEntry(ref BEEContext context, DSIA dSIA, List<DSIALineItem> dsiaLineItem)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == dSIA.Type.Trim() && x.DocID == dSIA.DSIANo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in dsiaLineItem)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.AdjQTY),
                    Rate = (decimal)item.Rate,
                    Amount = (decimal)item.COGSValue,
                    TRType = -1,
                    TDate = dSIA.DSIADate,
                    StoreID = dSIA.StoreID,
                    DocType = dSIA.Type.Trim(),
                    DocID = dSIA.DSIANo
                });

            }

            return true;

        }
        public static bool DeleteSampleEntry(ref BEEContext context, DSIA dSIA)
        {
            var isExist = context.InventoryTransaction.Where(x => x.DocID == dSIA.DSIANo && x.DocType == dSIA.Type).ToList();
            if(isExist.Count > 0)
            {
                foreach(var x in isExist)
                {
                    context.InventoryTransaction.Remove(x);
                }
            }
            return true;

        }
        public static bool InsertUpdateFromSales(ref BEEContext context, SalesEntryNew sales, List<SalesEntryNewLineItem> line)
        {
            var isExist = context.InventoryTransaction.Where(x => x.DocType == "Sales" && x.DocID == sales.InvoiceNo).ToList();
            if(isExist.Count> 0)
            {
                foreach(var s in isExist)
                {
                    context.InventoryTransaction.Remove(s);
                }
            }
            foreach(var x in line)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = x.ItemID,
                    Qty = Convert.ToDecimal(x.Qty),
                    Rate = x.clmCOGSRate,
                    Amount = (decimal)x.clmCOGSValue,
                    TRType = -1,
                    TDate = sales.SalesDate,
                    StoreID = x.StoreID,
                    DocType = "Sales",
                    DocID = x.InvoiceNo
                });
            }
            return true;
        }
        public static bool DeleteFromSales(ref BEEContext context, SalesEntryNew sales)
        {
            var isExist = context.InventoryTransaction.Where(x => x.DocID == sales.InvoiceNo && x.DocType == "Sales").ToList();
            if (isExist.Count > 0)
            {
                foreach (var x in isExist)
                {
                    context.InventoryTransaction.Remove(x);
                }
            }
            return true;

        }
        public static bool DeleteFromLCRNEntry(ref BEEContext context,GoodsReceiveNote lcrn)
        {
            var isExist = context.InventoryTransaction.Where(x => x.DocID == lcrn.GRNNo && x.DocType == "LCRN").ToList();
            if (isExist.Count > 0)
            {
                foreach (var x in isExist)
                {
                    context.InventoryTransaction.Remove(x);
                }
            }
            return true;

        }

        public static bool InsertFromLCRNEntry(ref BEEContext context, GoodsReceiveNote goodsReceiveNotes, List<GoodsReceiveNoteLineItem> receiveNoteLineItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "LCRN" && x.DocID == goodsReceiveNotes.GRNNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in receiveNoteLineItems)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.Qty),
                    Rate = item.CostRt,
                    Amount = (decimal)item.CostVal,
                    TRType = 1,
                    TDate = goodsReceiveNotes.GRNDate,
                    CompanyID = goodsReceiveNotes.CompanyID,
                    DocType = "LCRN",
                    DocID = goodsReceiveNotes.GRNNo
                });

            }

            return true;

        }

        public static bool InsertFromIssueRmToFg(ref BEEContext context, IssueRMToFG irf, List<IssueRMToFGLineItem> irfline)    
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RmToFg" && x.DocID == irf.IRTFNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in irfline)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.RMItemId,
                    Qty = Convert.ToDecimal(item.RMQty),
                    Rate = (decimal)item.RMStanderdCost,
                    Amount = (decimal)item.RMTotal,
                    TRType = -1,
                    TDate = irf.Date,
                    StoreID = irf.IssueFrom,
                    DocType = "RmToFg",
                    DocID = irf.IRTFNo
                });

                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.FGItemId,
                    Qty = Convert.ToDecimal(item.FGQty),
                    Rate = (decimal)item.FGStanderdCost,
                    Amount = (decimal)item.FGTotal,
                    TRType = 1,
                    TDate = irf.Date,
                    StoreID = irf.IssueTo,
                    DocType = "RmToFg",
                    DocID = irf.IRTFNo
                });

            }

            return true;

        }

        public static bool DeleteFromIssueRmToFg(ref BEEContext context, IssueRMToFG irf)    
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "RmToFg" && x.DocID == irf.IRTFNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertFromFGItemRepack(ref BEEContext context, FGItemRepack fgir, List<FGItemRepackLineItem> fgirLine)     
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "FGIR" && x.DocID == fgir.FGIRNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            foreach (var item in fgirLine)
            {
                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemIdIn,
                    Qty = Convert.ToDecimal(item.QtyIn),
                    Rate = (decimal)item.StanderdCostIn,
                    Amount = (decimal)item.StanderdCostIn * item.QtyIn,
                    TRType = -1,
                    TDate = fgir.Date,
                    StoreID = Convert.ToInt32(fgir.InStore),
                    DocType = "FGIR",
                    DocID = fgir.FGIRNo
                });

                context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                {
                    CIID = item.ItemIdOut,
                    Qty = Convert.ToDecimal(item.QtyOut),
                    Rate = (decimal)item.StanderdCostOut,
                    Amount = (decimal)item.StanderdCostOut * item.QtyOut,
                    TRType = 1,
                    TDate = fgir.Date,
                    StoreID = Convert.ToInt32(fgir.OutStore),
                    DocType = "FGIR",
                    DocID = fgir.FGIRNo
                });

            }
            return true;

        }

        public static bool DeleteFromFGItemRepack(ref BEEContext context, FGItemRepack fgir)  
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "FGIR" && x.DocID == fgir.FGIRNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertFromStockAdjustment(ref BEEContext context, StockAdjustment sa, List<StockAdjustmentLineItem> saline)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "FGSA") && x.DocID == sa.SANo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }

            if (sa.TransectionType == "Decrease")
            {
                foreach (var item in saline)
                {
                    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                    {
                        CIID = item.ItemId,
                        Qty = Convert.ToDecimal(item.Qty),
                        Rate = item.UnitCost,
                        Amount = item.Value,
                        TRType = -1,
                        TDate = sa.Date,
                        StoreID = sa.Store,
                        DocType = "FGSA",
                        DocID = sa.SANo
                    });
                }
            }
            else
            {
                foreach (var item in saline)
                {
                    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                    {
                        CIID = item.ItemId,
                        Qty = Convert.ToDecimal(item.Qty),
                        Rate = item.UnitCost,
                        Amount = item.Value,
                        TRType = 1,
                        TDate = sa.Date,
                        StoreID = sa.Store,
                        DocType = "FGSA",
                        DocID = sa.SANo
                    });
                }
            }

            return true;
        }
        public static bool DeleteFromStockAdjustment(ref BEEContext context, StockAdjustment sa)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "FGSA") && x.DocID == sa.SANo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertFromStockAdjustmentRM(ref BEEContext context, StockAdjustmentRM sa, List<StockAdjustmentRMLineItem> saline)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "RMSA") && x.DocID == sa.SANo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }

            if (sa.TransectionType == "Decrease")
            {
                foreach (var item in saline)
                {
                    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                    {
                        CIID = item.ItemId,
                        Qty = Convert.ToDecimal(item.Qty),
                        Rate = item.UnitCost,
                        Amount = item.Value,
                        TRType = -1,
                        TDate = sa.Date,
                        StoreID = sa.Store,
                        DocType = "RMSA",
                        DocID = sa.SANo
                    });
                }
            }
            else
            {
                foreach (var item in saline)
                {
                    context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
                    {
                        CIID = item.ItemId,
                        Qty = Convert.ToDecimal(item.Qty),
                        Rate = item.UnitCost,
                        Amount = item.Value,
                        TRType = 1,
                        TDate = sa.Date,
                        StoreID = sa.Store,
                        DocType = "RMSA",
                        DocID = sa.SANo
                    });
                }
            }

            return true;
        }
        public static bool DeleteFromStockAdjustmentRM(ref BEEContext context, StockAdjustmentRM sa)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "RMSA") && x.DocID == sa.SANo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertFromSITV(ref BEEContext context, SITV SITV, List<SITVLine> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "SITV" && x.DocID == SITV.SITVNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in addedItems)
            //{
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.ShiftQty), item.CostRt, item.CostVal, -1, SITV.SDate, SITV.StoreID, "SITV", SITV.SITVNo));
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.ShiftQty), item.CostRt, item.CostVal, 1, SITV.SDate, SITV.VehicleID, "SITV", SITV.SITVNo));
            //}

            return true;

        }
        public static bool DeleteFromSITV(ref BEEContext context, SITV SITV)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "SITV") && x.DocID == SITV.SITVNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertFromSIID(ref BEEContext context, SIFD SIFD, List<SIFDLineItem> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "SIFD" && x.DocID == SIFD.SIFDNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in addedItems)
            //{
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.ShiftQty), item.CostRt, item.CostVal, -1, SIFD.Date, SIFD.CurrentStoreID, "SIFD", SIFD.SIFDNo));
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.ShiftQty), item.CostRt, item.CostVal, 1, SIFD.Date, SIFD.NewStoreID, "SIFD", SIFD.SIFDNo));
            //}

            return true;

        }
        public static bool DeleteFromSIID(ref BEEContext context, SIFD SIFD)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "SIFD") && x.DocID == SIFD.SIFDNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }

        public static bool InsertUpdateFromFGRN(ref BEEContext context, GRF GRF, List<GRFLineItem> addedItems)
        {
            var items = context.InventoryTransaction.Where(x => x.DocType == "GRF" && x.DocID == GRF.GRFNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            //foreach (var item in addedItems)
            //{
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.Qty), item.Rate, item.Cost, -1, GRF.ReceiveDate, GRF.OldStoreID, "GRF", GRF.GRFNo));
            //    context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.Qty), item.Rate, item.Cost, 1, GRF.ReceiveDate, GRF.ReceiveStoreID, "GRF", GRF.GRFNo));
            //    //context.InventoryTransaction.Add(new InventoryTransaction(item.ItemID, Convert.ToDecimal(item.ShiftQty), item.CostRt, item.CostVal, 1, SIFD.Date, SIFD.NewStoreID, "SIFD", SIFD.SIFDNo));
            //}

            return true;

        }
        public static bool DeleteFromFGRN(ref BEEContext context, GRF grf)
        {
            var items = context.InventoryTransaction.Where(x => (x.DocType == "GRF") && x.DocID == grf.GRFNo).ToList();
            foreach (var item in items)
            {
                context.InventoryTransaction.Remove(item);

            }
            return true;

        }
        public static bool InsertFromNewInventoryItem(ref BEEContext context, ChartOfInventory inventoryData)
        {
            //var items = context.InventoryTransaction.Where(x => x.TRType == 1 && x.DocType == "IOB" && x.DocID == inventoryData.Id).ToList();
            //foreach (var item in items)
            //{
            //    context.InventoryTransaction.Remove(item);

            //}

            //context.InventoryTransaction.Add(new CommonInformation.InventoryTransaction()
            //{
            //    CIID = inventoryData.Id,
            //    Qty = Convert.ToDecimal(inventoryData.OBQty),
            //    Rate = inventoryData.OBRate,
            //    Amount = inventoryData.OBValue,
            //    TRType = 1,
            //    TDate = inventoryData.OBDate,
            //    StoreID = inventoryData.StoreID,
            //    DocType = "IOB",
            //    DocID = inventoryData.Id
            //});


            return true;

        }
    }
}