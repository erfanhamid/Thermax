using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEEERP.Models.DataAdmin.SPSettings;
using BEEERP.Models.Database;
using BEEERP.Models.SpareParts;

namespace BEEERP.Models.Bridge
{
    public static class SPInventoryTransactionBridge
    {

        public static bool InsertFromMRR(ref BEEContext context, MRRGoodsReceiveNote lcrn, List<MRRGoodsReceiveNoteLineItem> addedItems1)
        {
            //var items = context.SPInventoryTransaction.Where(x => x.DocType == "ISP" && x.DocID == isp.ISPNo).ToList();
            //foreach (var item in items)
            //{
            //    context.SPInventoryTransaction.Remove(item);

            //}
            foreach (var item in addedItems1)
            {
                context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
                {
                    CIID = item.ItemID,
                    Qty = Convert.ToDecimal(item.QcQty),
                    Rate = Convert.ToDecimal(item.CostRt),
                    Amount = Convert.ToDecimal(item.CostVal),
                    TRType = 1,
                    TDate = lcrn.Date,
                    StoreID = item.StoreId,
                    CompanyID = lcrn.CompanyID,
                    TypeId = lcrn.TypeId,
                    BoxID = item.BoxID,
                    DocType = "MRR",
                    DocID = lcrn.LCRNNo
                });
            }
            return true;


        }


        public static bool InsertFromSparePartsDamage(ref BEEContext context, SparePartsDamage spd, List<SparePartsDamageLineItem> spdLineItems)
        {
            //var items = context.SPInventoryTransaction.Where(x => x.DocType == "ISP" && x.DocID == isp.ISPNo).ToList();
            //foreach (var item in items)
            //{
            //    context.SPInventoryTransaction.Remove(item);

            //}
            foreach (var item in spdLineItems)
            {
                context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
                {
                    CIID = item.SPItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = Convert.ToDecimal(item.ItemRate),
                    Amount = Convert.ToDecimal(item.ItemValue),
                    TRType = -1,
                    TDate = spd.SPDDate,
                    StoreID = item.StoreId,
                    CompanyID = spd.CompanyID,
                    TypeId = spd.TypeId,
                    BoxID = item.BoxID,
                    DocType = "SPD",
                    DocID = spd.SPDNo
                });
            }
            return true;


        }


        public static bool UpdateFromSparePartsDamage(ref BEEContext context, SparePartsDamage spd, List<SparePartsDamageLineItem> spdLineItems)
        {
            var items = context.SPInventoryTransaction.Where(x => x.DocType == "SPD" && x.DocID == spd.SPDNo).ToList();

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    context.SPInventoryTransaction.Remove(item);

                }
            }


            foreach (var item in spdLineItems)
            {
                context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
                {
                    CIID = item.SPItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = Convert.ToDecimal(item.ItemRate),
                    Amount = Convert.ToDecimal(item.ItemValue),
                    TRType = -1,
                    TDate = spd.SPDDate,
                    StoreID = item.StoreId,
                    CompanyID = spd.CompanyID,
                    TypeId = spd.TypeId,
                    BoxID = item.BoxID,
                    DocType = "SPD",
                    DocID = spd.SPDNo
                });
            }
            return true;


        }

        public static bool DeleteFromSparePartsDamage(ref BEEContext context, int id)
        {
            var items = context.SPInventoryTransaction.Where(x => x.DocType == "SPD" && x.DocID == id).ToList();

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    context.SPInventoryTransaction.Remove(item);

                }
            }



            return true;


        }
        public static bool InsertFromIssueSpareParts(ref BEEContext context, IssueSpareParts isp, List<IssueSparePartsLineItem> ispLineItems)
        {
            //var items = context.SPInventoryTransaction.Where(x => x.DocType == "ISP" && x.DocID == isp.ISPNo).ToList();
            //foreach (var item in items)
            //{
            //    context.SPInventoryTransaction.Remove(item);

            //}
            foreach (var item in ispLineItems)
            {
                context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
                {
                    CIID = item.SPItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = Convert.ToDecimal(item.ItemRate),
                    Amount = Convert.ToDecimal(item.ItemValue),
                    TRType = -1,
                    TDate = isp.ISPDate,
                    StoreID = item.StoreId,
                    CompanyID = isp.CompanyID,
                    TypeId = isp.TypeId,
                    BoxID = item.BoxID,
                    DocType = "ISP",
                    DocID = isp.ISPNo
                });
            }
            return true;


        }

        public static bool UpdateFromIssueSpareParts(ref BEEContext context, IssueSpareParts isp, List<IssueSparePartsLineItem> ispLineItems)
        {
            var items = context.SPInventoryTransaction.Where(x => x.DocType == "ISP" && x.DocID == isp.ISPNo).ToList();
            foreach (var item in items)
            {
                context.SPInventoryTransaction.Remove(item);

            }
            foreach (var item in ispLineItems)
            {
                context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
                {
                    CIID = item.SPItemID,
                    Qty = Convert.ToDecimal(item.ItemQty),
                    Rate = Convert.ToDecimal(item.ItemRate),
                    Amount = Convert.ToDecimal(item.ItemValue),
                    TRType = -1,
                    TDate = isp.ISPDate,
                    StoreID = item.StoreId,
                    CompanyID = isp.CompanyID,
                    TypeId = isp.TypeId,
                    BoxID = item.BoxID,
                    DocType = "ISP",
                    DocID = isp.ISPNo
                });
            }

            return true;




        }
        public static bool DeleteFromIssueSpareParts(ref BEEContext context, int id)
        {
            var items = context.SPInventoryTransaction.Where(x => x.DocType == "ISP" && x.DocID == id).ToList();
            foreach (var item in items)
            {
                context.SPInventoryTransaction.Remove(item);

            }
            //foreach (var item in ispLineItems)
            //{
            //    context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
            //    {
            //        CIID = item.SPItemID,
            //        Qty = Convert.ToDecimal(item.ItemQty),
            //        Rate = Convert.ToDecimal(item.ItemRate),
            //        Amount = Convert.ToDecimal(item.ItemValue),
            //        TRType = -1,
            //        TDate = isp.ISPDate,
            //        StoreID = item.StoreId,
            //        CompanyID = isp.CompanyID,
            //        TypeId = isp.TypeId,
            //        BoxID = item.BoxID,
            //        DocType = "ISP",
            //        DocID = isp.ISPNo
            //    });
            //}
            return true;

        }
        public static bool InsertFromSPOpeningBalance(ref BEEContext context, SpOpeningBalance spob)
        {
            //var obdata = context.SPInventoryTransaction.FirstOrDefault(x => x.DocType == "SPOB" && x.DocID == spob.ItemId);


            //context.SPInventoryTransaction.Remove(obdata);
            ////foreach (var item in items)
            //{


            //}
            //foreach (var item in addedItems)
            //{
            context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
            {
                CIID = spob.ItemId,
                Qty = Convert.ToDecimal(spob.Quantity),
                Rate = spob.Rate,
                Amount = spob.Value,
                TRType = 1,
                TDate = spob.SpDate,
                StoreID = spob.StoreId,
                CompanyID = spob.CompanyID,
                TypeId = spob.TypeId,
                BoxID = spob.BoxID,
                DocType = "SPOB",
                DocID = spob.ItemId
            });
            //}

            return true;

        }
        public static bool UpdateFromSPOpeningBalance(ref BEEContext context, SpOpeningBalance spob)
        {
            var obdata = context.SPInventoryTransaction.FirstOrDefault(x => x.DocType == "SPOB" && x.DocID == spob.ItemId);


            context.SPInventoryTransaction.Remove(obdata);
            ////foreach (var item in items)
            //{


            //}
            //foreach (var item in addedItems)
            //{
            context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
            {
                CIID = spob.ItemId,
                Qty = Convert.ToDecimal(spob.Quantity),
                Rate = spob.Rate,
                Amount = spob.Value,
                TRType = 1,
                TDate = spob.SpDate,
                StoreID = spob.StoreId,
                CompanyID = spob.CompanyID,
                TypeId = spob.TypeId,
                BoxID = spob.BoxID,
                DocType = "SPOB",
                DocID = spob.ItemId
            });
            //}

            return true;

        }
        public static bool DeleteFromSPOpeningBalance(ref BEEContext context, int id)
        {
            var obdata = context.SPInventoryTransaction.FirstOrDefault(x => x.DocType == "SPOB" && x.DocID == id);


            context.SPInventoryTransaction.Remove(obdata);
            ////foreach (var item in items)
            //{


            //}
            //foreach (var item in addedItems)
            //{
            //context.SPInventoryTransaction.Add(new CommonInformation.SPInventoryTransaction()
            //{
            //    CIID = spob.ItemId,
            //    Qty = Convert.ToDecimal(spob.Quantity),
            //    Rate = spob.Rate,
            //    Amount = spob.Value,
            //    TRType = 1,
            //    TDate = spob.SpDate,
            //    StoreID = spob.StoreId,
            //    CompanyID = spob.CompanyID,
            //    TypeId = spob.TypeId,
            //    BoxID = spob.BoxID,
            //    DocType = "SPOB",
            //    DocID = spob.ItemId
            //});
            //}

            return true;

        }
    }
}