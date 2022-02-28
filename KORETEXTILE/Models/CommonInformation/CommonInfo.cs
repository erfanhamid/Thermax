using BEEERP.Models.Database;
using BEEERP.Models.StoreRM.GRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public static class CommonInfo
    {

        public static decimal AllocateItem(ref BEEContext context,int ItemId, string DocType, decimal qty, int DocNo, int StoreId, bool IsUpdate)
        {
           
            //var data = context.RemainingStock.Where(x => x.ItemId == ItemId && x.Qty > 0 && x.StoreId == StoreId).ToList();

            if (IsUpdate == true)
            {
                var isExist = context.RemainingStock.Where(x => x.DocNo == DocNo && x.ItemId == ItemId && x.StoreId == StoreId).ToList();
                if (isExist.Count > 0)
                {
                    foreach(var x in isExist)
                    {
                        context.RemainingStock.Remove(x);
                    }

                }
                context.SaveChanges();
            }
            var item = context.RemainingStock.Where(x => x.ItemId == ItemId && x.StoreId == StoreId).ToList();
            var data = item
            .GroupBy(l => l.LCRNNo)
            .Select(cl => new ResultLine
            {
                LCRNNo = cl.FirstOrDefault().LCRNNo,
                ItemId = cl.FirstOrDefault().ItemId,
                StoreId = cl.FirstOrDefault().StoreId,
                Qty = cl.Sum(c => c.Qty),
                UnitCost = cl.FirstOrDefault().UnitCost
            }).ToList();//.Where(s => s.ItemId == ItemId).ToList();
            var TotalQty = context.RemainingStock.Where(x => x.ItemId == ItemId && x.StoreId == StoreId).Select(x => x.Qty).DefaultIfEmpty(0).Sum();
            if (TotalQty >= qty)
            {
                decimal RemainingQty = 0;
                decimal TotalCost = 0;
                int count = 0;
               foreach(var x in data)
                {
                    if (count == 0)
                    {
                        if (qty >= x.Qty)
                        {
                            if (x.Qty > 0)
                            {
                                var q = -x.Qty;
                                RemainingQty = qty - x.Qty;
                                RemainingStock RS = new RemainingStock();
                                RS.ItemId = ItemId;
                                RS.Qty = q;
                                RS.LCRNNo = x.LCRNNo;
                                RS.UnitCost = x.UnitCost;
                                RS.DocType = DocType;
                                RS.DocNo = DocNo;
                                RS.StoreId = x.StoreId;
                                TotalCost += x.Qty * x.UnitCost;
                                context.RemainingStock.Add(RS);
                                context.SaveChanges();
                            }
                            else
                            {
                                RemainingQty = qty;
                            }

                        }
                        else
                        {

                            var q = -qty;
                            RemainingQty = 0;
                            TotalCost += (qty * x.UnitCost);
                            RemainingStock RS = new RemainingStock();
                            RS.ItemId = ItemId;
                            RS.Qty = q;
                            RS.LCRNNo = x.LCRNNo;
                            RS.UnitCost = x.UnitCost;
                            RS.DocType = DocType;
                            RS.DocNo = DocNo;
                            RS.StoreId = x.StoreId;
                            context.RemainingStock.Add(RS);
                            context.SaveChanges();

                        }
                    }
                    if (count > 0)
                    {
                        if (RemainingQty > 0)
                        {
                            if (RemainingQty >= x.Qty)
                            {
                                var q = -x.Qty;
                                TotalCost += (x.Qty * x.UnitCost);
                                RemainingQty = RemainingQty - x.Qty;
                                RemainingStock RS = new RemainingStock();
                                RS.ItemId = ItemId;
                                RS.Qty = q;
                                RS.LCRNNo = x.LCRNNo;
                                RS.UnitCost = x.UnitCost;
                                RS.DocType = DocType;
                                RS.DocNo = DocNo;
                                RS.StoreId = x.StoreId;
                                context.RemainingStock.Add(RS);
                                context.SaveChanges();
                            }
                            else
                            {
                                var q = -RemainingQty;

                                TotalCost += (RemainingQty * x.UnitCost);
                                RemainingStock RS = new RemainingStock();
                                RS.ItemId = ItemId;
                                RS.Qty = q;
                                RS.LCRNNo = x.LCRNNo;
                                RS.UnitCost = x.UnitCost;
                                RS.DocType = DocType;
                                RS.DocNo = DocNo;
                                RS.StoreId = x.StoreId;
                                RemainingQty = 0;
                                context.RemainingStock.Add(RS);
                                context.SaveChanges();
                            }
                        }
                    }

                    count++;
                };
                return Math.Round((TotalCost / qty), 2);
            }
            else
            {
                return 0;
            }


        }

        public static void TransferItem(ref BEEContext context, int itemId,string docType,decimal qty,int docNo,int issueFrom, int issueTo, bool isUpdate)
        {
           
            //var data = context.RemainingStock.Where(x => x.ItemId == ItemId && x.Qty > 0 && x.StoreId == StoreId).ToList();

            List<RemainingStock> remainingStocks = new List<RemainingStock>();
            if (isUpdate == true)
            {
                var isExist = context.RemainingStock.Where(x => x.DocNo == docNo && x.ItemId == itemId && x.StoreId == issueFrom).ToList();
                if (isExist.Count > 0)
                {
                    foreach (var x in isExist)
                    {
                        context.RemainingStock.Remove(x);
                    }

                }
                
            }
            var item = context.RemainingStock.Where(x => x.ItemId == itemId && x.StoreId == issueFrom).ToList();
            var data = item
            .GroupBy(l => l.LCRNNo)
            .Select(cl => new ResultLine
            {
                LCRNNo = cl.FirstOrDefault().LCRNNo,
                ItemId = cl.FirstOrDefault().ItemId,
                StoreId = cl.FirstOrDefault().StoreId,
                Qty = cl.Sum(c => c.Qty),
                UnitCost = cl.FirstOrDefault().UnitCost
            }).ToList();//.Where(s => s.ItemId == ItemId).ToList();
            var TotalQty = context.RemainingStock.Where(x => x.ItemId == itemId && x.StoreId == issueFrom).Select(x => x.Qty).DefaultIfEmpty(0).Sum();
            decimal RemainingQty = 0;
            decimal TotalCost = 0;
            int count = 0;
            data.ForEach(x =>
            {
                if (count == 0)
                {
                    if (qty >= x.Qty)
                    {
                        if (x.Qty > 0)
                        {
                            var q = -x.Qty;
                            RemainingQty = qty - x.Qty;
                            RemainingStock RS = new RemainingStock();
                            RS.ItemId = itemId;
                            RS.Qty = q;
                            RS.LCRNNo = x.LCRNNo;
                            RS.UnitCost = x.UnitCost;
                            RS.DocType = docType;
                            RS.DocNo = docNo;
                            RS.StoreId = x.StoreId;
                            TotalCost += x.Qty * x.UnitCost;
                            remainingStocks.Add(RS);
                        }
                        else
                        {
                            RemainingQty = qty;
                        }

                    }
                    else
                    {

                        var q = -qty;
                        RemainingQty = 0;
                        TotalCost += (qty * x.UnitCost);
                        RemainingStock RS = new RemainingStock();
                        RS.ItemId = itemId;
                        RS.Qty = q;
                        RS.LCRNNo = x.LCRNNo;
                        RS.UnitCost = x.UnitCost;
                        RS.DocType = docType;
                        RS.DocNo = docNo;
                        RS.StoreId = x.StoreId;
                        remainingStocks.Add(RS);
                    }
                }
                if (count > 0)
                {
                    if (RemainingQty > 0)
                    {
                        if (RemainingQty >= x.Qty)
                        {
                            var q = -x.Qty;
                            TotalCost += (x.Qty * x.UnitCost);
                            RemainingQty = RemainingQty - x.Qty;
                            RemainingStock RS = new RemainingStock();
                            RS.ItemId = itemId;
                            RS.Qty = q;
                            RS.LCRNNo = x.LCRNNo;
                            RS.UnitCost = x.UnitCost;
                            RS.DocType = docType;
                            RS.DocNo = docNo;
                            RS.StoreId = x.StoreId;
                            remainingStocks.Add(RS);
                        }
                        else
                        {
                            var q = -RemainingQty;

                            TotalCost += (RemainingQty * x.UnitCost);
                            RemainingStock RS = new RemainingStock();
                            RS.ItemId = itemId;
                            RS.Qty = q;
                            RS.LCRNNo = x.LCRNNo;
                            RS.UnitCost = x.UnitCost;
                            RS.DocType = docType;
                            RS.DocNo = docNo;
                            RS.StoreId = x.StoreId;
                            RemainingQty = 0;
                            remainingStocks.Add(RS);
                        }
                    }
                }

                count++;
            });
            foreach(var x in remainingStocks) { 
                context.RemainingStock.Add(x);
            };
            var newRemaingStock = new List<RemainingStock>();
            foreach (var x in remainingStocks) {
                RemainingStock stock = new RemainingStock();
                stock.ItemId = x.ItemId;
                stock.LCRNNo = x.LCRNNo;
                stock.Qty = x.Qty * -1;
                stock.StoreId = issueTo;
                stock.UnitCost = x.UnitCost;
                stock.DocNo = x.DocNo;
                stock.DocType = x.DocType;
                newRemaingStock.Add(stock);
            };
            foreach(var x in newRemaingStock)
            {
                context.RemainingStock.Add(x);
            }
        }

        public static void DeleteFromRemainingStock(ref BEEContext context, int DocNo, string DocType)
        {
            var remainingStockExist = context.RemainingStock.Where(m => m.DocNo == DocNo && m.DocType == DocType).ToList();
            if (remainingStockExist.Count>0)
            {
                foreach (var x in remainingStockExist)
                {
                    context.RemainingStock.Remove(x);
                }

            }
        }
        public static decimal GetCostPrice(int id, string jobno, int storeid)
        {
            decimal costPrice = 10;
            return costPrice;
        }
    }
}