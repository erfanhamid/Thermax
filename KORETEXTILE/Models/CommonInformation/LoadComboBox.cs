using BEEERP.Models.CommercialModule;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.ViewModel.CommercialVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.CommonInformation
{
    public static class LoadComboBox
    {

        public static SelectList LoadLCMR(int? supplierId, int? typeId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select a LC/ ---");
            if (typeId == 1)
            {
                var importSupplier = (from lc in context.ImportLC where lc.SupplierId == supplierId select new { lcId = lc.ILCId, lcno = lc.ILCNO }).ToList();
                //lcs.ForEach(x => { items.Add(x.lcId.ToString(), (x.lcId.ToString() + " - " + x.lcno)); });
                importSupplier.ForEach(x => { items.Add(x.lcId.ToString(), x.lcId.ToString() + "-" + x.lcno); });
            }
            else
            {
                var localSupplier = (from lc in context.WorkOrder where lc.SupplierID == supplierId select new { lcId = lc.WONo, lcno = lc.WONo }).ToList();

                localSupplier.ForEach(x => { items.Add(x.lcId.ToString(), x.lcId.ToString()); });
            }


            return new SelectList(items, "Key", "Value");
        }
        public static SelectList SupplierFromType(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Supplier ---");
            if (id != null)
            {
                context.Supplier.Where(x => x.GroupID == id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + "-" + x.SupplierName); });
            }


            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSPGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");            
            context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "SP".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllTypeId()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Type ---");
            context.SPType.ToList().ForEach(x => { items.Add(x.SPTypeId.ToString(), x.SPTypeId.ToString() + " - " + x.SPTypeName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSPBrand()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Brand ---");
            context.SPBrand.ToList().ForEach(x => { items.Add(x.BrandID.ToString(), x.BrandID.ToString() + " - " + x.BrandName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSPBox(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Box ---");
            if (id != null)
            {
                context.SPBox.Where(x => x.StoreID == id).ToList().ForEach(x => { items.Add(x.BoxID.ToString(), x.BoxID.ToString() + " - " + x.BoxNo); });
            }
            else
            {

            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList loadAllSPItemType(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Type ---");
            if (id != null)
            {
                context.SPItemType.Where(x => x.DepartmentID == id).ToList().ForEach(x => { items.Add(x.ItemTypeID.ToString(), x.ItemTypeID.ToString() + " - " + x.ItemType); });
            }
            else
            {

            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllItemList(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item Part No ---");
            if (id != null)
            {
                context.SparePartsItemList.Where(x => x.SPTypeID == id).ToList().ForEach(x => { items.Add(x.SPItemID.ToString(), x.SPItemID.ToString() + " - " + x.SPItemPartNo); });
            }
            else
            {

            }
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSparePartsDepartment()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Department ---");
            context.SparePartsDepartment.ToList().ForEach(x => { items.Add(x.SPDId.ToString(), x.SPDId.ToString() + " - " + x.SPDName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadGroupGS()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            //context.ChartOfInventory.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "GS".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSPGroupGS()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            //context.ChartOfInventory.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "SP".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static decimal? GetRMCRate(int id, string date)
        {
            //DateTime Rdate = Convert.ToDateTime(date);
            //string sql = "exec RMCRateById " + id + ", '" + date + "'";
            string sql = "exec GetNewRMCRateById " + id + ", '" + date + "'";
            BEEContext context = new BEEContext();

            //try
            //{
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            if (Items.Count > 0)
            {
                var item = Items.FirstOrDefault();
                return Convert.ToDecimal(item.clmStandardCost);
            }
            else
            {
                return 0;
            }
            //  }
            //catch(Exception ex)
            //  {
            //      return 0;
            //  }

        }

        public static decimal? GetISPRate(int id, string date)
        {
            //DateTime Rdate = Convert.ToDateTime(date);
            //string sql = "exec RMCRateById " + id + ", '" + date + "'";
            string sql = "exec GetNewRMCRateById " + id + ", '" + date + "'";
            BEEContext context = new BEEContext();

            //try
            //{
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            if (Items.Count > 0)
            {
                var item = Items.FirstOrDefault();
                return Convert.ToDecimal(item.clmStandardCost);
            }
            else
            {
                return 0;
            }

        }
        public static List<Item_Info> GetItemInfo()
        {
            string sql = "exec ItemDetails";
            BEEContext context = new BEEContext();
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            return Items;
        }
        public static SelectList LoadAllGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            context.ChartOfInventory.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllItemByID(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            if (id != null)
            {
                context.ChartOfInventory.Where(x => x.parentId == id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.Name); });
            }
            else
            {

            }
            return new SelectList(items, "Key", "Value");
        }



        public static SelectList LoadAllUomByID(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Uom ---");
            if (id != null)
            {
                context.ChartOfInventory.Where(x => x.UoMID == id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.Name); });
            }
            else
            {

            }
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllItemList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            context.SparePartsItemList.ToList().ForEach(x => { items.Add(x.SPItemID.ToString(), x.SPItemID.ToString() + " - " + x.SPItemPartNo); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSpDepartment()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Department ---");
            context.SPDepartment.ToList().ForEach(x => { items.Add(x.SPDID.ToString(), x.SPDID.ToString() + " - " + x.SPDName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllMachineSection()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Machine Section ---");
            context.MachineSection.ToList().ForEach(x => { items.Add(x.MSID.ToString(), x.MSID.ToString() + " - " + x.MSName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllMachineType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Machine Type ---");
            context.MachineType.ToList().ForEach(x => { items.Add(x.MTID.ToString(), x.MTID.ToString() + " - " + x.MtName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllMFGYear()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Year ---");
            context.YearList.ToList().ForEach(x => { items.Add(x.YearID.ToString(), x.YearName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllMachineID()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Machine ---");
            context.MachineDetails.ToList().ForEach(x => { items.Add(x.MachineID.ToString(), x.MachineID.ToString() + " - " + x.MachineSerial); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSoftwareID()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Software ---");
            context.SoftwareList.ToList().ForEach(x => { items.Add(x.SoftwareID.ToString(), x.SoftwareID.ToString() + " - " + x.SoftwareName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllCompanyID()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Unit ---");
            context.CompanyList.ToList().ForEach(x => { items.Add(x.CompanyID.ToString(), x.CompanyID.ToString() + " - " + x.CompanyName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadInventoryItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> ChartOfInv = new Dictionary<string, string>();
            ChartOfInv.Add("", "--- Select Item ---");
            string sql = "select id,Name from ChartOfInv where type = 'l'";
            List<ItemList> Data = context.Database.SqlQuery<ItemList>(sql).ToList();
            Data.ForEach(x => { ChartOfInv.Add(x.id.ToString(), x.id.ToString() + " - " + x.Name); });
            return new SelectList(ChartOfInv, "Key", "Value");
        }

        public static SelectList LoadGroupItem()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "");
            items.Add("g", "Group");
            items.Add("l", "Item");
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList GetAllInventory()
        {
            
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "");
            context.ChartOfInventory.ToList().ForEach(s =>
            {
                items.Add(s.Id.ToString(), s.Name);
            });
           
                

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadInventoryType()//demo category ,not from database
        {
            //BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Type ---");
            items.Add("RM", "RM");
            items.Add("WIP", "WIP");
            items.Add("FG", "FG");
            items.Add("BP", "BP");
            items.Add("SP", "SP");
            items.Add("PM", "PM");
            items.Add("GS", "GS");
            return new SelectList(items, "Key", "Value");
        }


        public static List<ILC_Info> GetILCInfo(int id)
        {
            string sql = "EXEC spILCItemDetailById " + id;
            BEEContext context = new BEEContext();
            List<ILC_Info> Items = context.Database.SqlQuery<ILC_Info>(sql).ToList();
            var receivedQTY = (from t1 in context.GoodsReceiveNote
                               join t2 in context.GoodsReceiveNoteLineItem
                               on t1.GRNNo equals t2.GRNNo
                               where t1.Type == "LC" && t1.WONo == id 
                               select new {lcrn=t1.GRNNo, itemID = t2.ItemID, receivedQty = t2.Qty }).ToList();

            if (receivedQTY.Count() > 0)
            {

                Items.ForEach(x =>
                {
                    var checkReceiveItem = receivedQTY.Where(m => m.itemID == x.ItemId).Count();
                    if (checkReceiveItem>0)
                    {
                        //x.LcrnQTY = receivedQTY.FirstOrDefault(m => m.itemID == x.ItemId).receivedQty;
                        x.LcrnQTY = receivedQTY.Where(m => m.itemID == x.ItemId).ToList().Sum(s => s.receivedQty);
                        x.LCRNO = receivedQTY.Where(m => m.itemID == x.ItemId).First().lcrn;
                        //x.LcrnQTY = receivedQTY.Sum(s => s.receivedQty);
                    }
                    else
                    {
                        //Items.Remove(x);
                    }

                });
            }

            return Items;
        }
        public static SelectList LoadALLILCIdAndNo()
        {
            BEEContext context = new BEEContext();
            var results = context.ImportLC.ToList();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select One ---");
            results.ToList().ForEach(x => { items.Add(x.ILCId.ToString(), x.ILCId.ToString() + " - " + x.ILCNO); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadGlConfugaretion()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            var type = context.GlConfiguration.ToList();
            items.Add("", "--- Select Account type ---");
            type.ForEach(x => { items.Add(Convert.ToString( x.Id), (context.ChartOfAccount.FirstOrDefault(p=>p.Id==x.GlAccount).Name)); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllILC()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ILC ---");
            //context.ImportLC.OrderBy(x => x.ILCNO).ToList().ForEach(x => { items.Add(x.ILCId.ToString(), x.ILCNO); });
            context.ImportLC.OrderBy(x => x.ILCId).ToList().ForEach(x => { items.Add(x.ILCId.ToString(), x.ILCId.ToString() + " - " + x.ILCNO); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadEmpType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("true", " Active ");
            items.Add("false", " Not Active ");


            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllExAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var acc = context.ChartOfAccount.Where(x => x.RootAccountType == "ex").Select(x => new { x.Id, x.Name }).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.ChartOfAccount.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSubLedgerType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            var type = context.Subledger.Select(x => x.Type).Distinct().ToList();
            items.Add("", "--- Select Account type ---");
            type.ForEach(x => { items.Add(x, x); });
            //items.Add("", "--- Select Account type ---");
            //items.Add("Delivery Van", " Delivery Van ");
            //items.Add("Motor Cycle", " Motor Cycle ");
            //items.Add("Casual Sales Van", " Casual Sales Van ");
            //items.Add("Management Car", " Management Car ");
            //items.Add("Micro Bus", " Micro Bus ");
            //items.Add("Pick Up", " Pick Up ");
            //items.Add("Sales Van", " Sales Van ");


            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllCustomerBalance()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var acc = context.ChartOfAccount.Where(x => x.Name == "All Customer Balance").Select(x => new { x.Id, x.Name }).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.ChartOfAccount.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAccountType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select type ---");
            items.Add("Dealer Balance", " Dealer Balance ");
            items.Add("Cash or Bank", " Cash or Bank ");
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadRMCustomer()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> customer = new Dictionary<string, string>();
            customer.Add("", "--- Select Customer ---");
            string sql = "select clmCustomerID,clmCustomerName from tblRMCustomer";
            List<RMCustomer> Data = context.Database.SqlQuery<RMCustomer>(sql).ToList();
            Data.ForEach(x => { customer.Add(x.clmCustomerID.ToString(), x.clmCustomerID.ToString() + " - " + x.clmCustomerName); });
            return new SelectList(customer, "Key", "Value");
        }
        public static SelectList LoadAllRetailerByDealer(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Retailer ---");
            GetRetailerInfo().Where(x => x.CustomerID == id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.RetailerName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllSubLedgerBytype(string vtype)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Sub Ledger ---");
            GetSubLedgerInfo().Where(x => x.SubLedgerType == vtype).ToList().ForEach(x => { items.Add(x.SubLedgerID.ToString(), x.SubLedgerID.ToString() + " - " + x.SubLedgerName); });
            return new SelectList(items, "Key", "Value");
        }
        public static List<SubLedger_Info> GetSubLedgerInfo()
        {
            string sql = "exec SubledgerDetails";
            BEEContext context = new BEEContext();
            List<SubLedger_Info> Items = context.Database.SqlQuery<SubLedger_Info>(sql).ToList();
            return Items;
        }


        public static SelectList LoadAllFWOTypeList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.FWOTypes.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllCustomers()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.Customers.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.CustomerName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllFWOBrandList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.FWOBrands.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllFWOModelList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.FWOModels.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllFWOCapacityList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.FWOCapacities.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList GetFGStoreByName(int? depotID)
        {
            if (depotID == 0)
            {
                BEEContext context = new BEEContext();
                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add("", "--- Select FG Store ---");
                //var item = new SelectList(items, "Key", "Value");
                return new SelectList(items, "Key", "Value");
            }
            else
            {

                BEEContext context = new BEEContext();
                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add("", "--- Select FG Store ---");
                int depotid = Convert.ToInt32(depotID);
                var dpt = context.BranchInformation.FirstOrDefault(x => x.Slno == depotid);
                //var depotName = dpt.BrnachName;

                var storeFGName = from s in context.Store
                                  join b in context.BranchInformation on s.DepotID equals b.Slno
                                  where ((s.Type == "FG" || s.Type == "FA") && b.Slno == depotid && s.Id != 24)
                                  select new { s.Id, s.Name };
                storeFGName.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                //var item = new SelectList(items, "Key", "Value");
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadFreezerModelDescription(int? itemid)
        {
            
                BEEContext context = new BEEContext();
                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add("", "--- Select Item ---");
                string sql = string.Format("exec itemFWO ");
                var item = context.Database.SqlQuery<ItemInfoSP>(sql).ToList();
                item.ForEach(x => { items.Add(Convert.ToString(x.ID), x.Item); });
                return new SelectList(items, "Key", "Value");
            
        }

        internal static dynamic LoadAllUserName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select User Name ---");
            dbContext.Users.ToList().ForEach(x => { items.Add(x.UserName, x.UserName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadDirectPurchaseNo(int? suplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            //context.DirectPurchase.Where(x => x.SupplierId == suplierId).ToList().ForEach(x =>
            //{
            //    decimal dueAmount = DPDueAmountByDpNo(x.DPNo);
            //    if (dueAmount > 0)
            //    {
            //        items.Add(x.DPNo.ToString(), x.DPNo.ToString());
            //    }
            //});
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllCashBankAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Cash/Bank Account ---");
            context.ChartOfAccount.Where(x => (x.ParentId == 8 || x.ParentId == 9) && x.Type == "l").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllLedgerGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account Group ---");
            context.ChartOfAccount.Where(x => x.Type == "g").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadChartofAccountGroupOnlyHasLedger()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            var groupId = (from p in context.ChartOfAccount.ToList().Where(x => x.Type == "l" && (x.RootAccountType.Trim() == "li" || x.RootAccountType.Trim() == "as")).ToList()
                           select p.ParentId).ToList().Distinct();
            (from p in groupId
             join c in context.ChartOfAccount.ToList() on p equals c.Id
             select new
             {
                 c.Id,
                 Type = c.RootAccountType == "as" ? "Assets :" : "Liabilities :",
                 c.Name
             }).ToList().ForEach(x => items.Add(x.Id.ToString(), x.Type + " " + x.Name));
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSubLedger()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Sub Ledger ---");
            context.Store.Where(x => x.Type == "Delivery Van" || x.Name.Contains("2434") || x.Name.Contains("0500")).ToList().ForEach(x=> { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        
        public static SelectList LoadAllDepot()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Depot ---");
            context.BranchInformation.OrderBy(x => x.BrnachName).ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllCostCenter()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Cost Center ---");
            context.BranchInformation.OrderBy(x => x.BrnachName).ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadDepotByID(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Depot ---");
            context.BranchInformation.Where(x=> x.Slno == id).ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAcByDepot(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select A/C ---");
            var AcInfo = (from c in context.TblCashAccountConfigurations
                          join
                          a in context.ChartOfAccount
                          on c.CashBankID equals a.Id
                          select new { Id = a.Id, Name = a.Name,Depot = c.OperationUnitID }).Where(x=> x.Depot == id).ToList();
            AcInfo.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.BranchInformation.Where(x => x.Slno == id).ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadDepot()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select A/C ---");
            if (HttpContext.Current.User.IsInRole("Admin"))
            {
                var AcInfo = (from c in context.TblCashAccountConfigurations
                              join
                              a in context.ChartOfAccount
                              on c.CashBankID equals a.Id
                              select new { Id = a.Id, Name = a.Name, Depot = c.OperationUnitID }).ToList();
                AcInfo.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            }
            else
            {
                var employee = context.Employees.FirstOrDefault(x => x.LogInUserName == HttpContext.Current.User.Identity.Name);
                var AcInfo = (from c in context.TblCashAccountConfigurations
                              join
                              a in context.ChartOfAccount
                              on c.CashBankID equals a.Id
                              select new { Id = a.Id, Name = a.Name, Depot = c.OperationUnitID }).Where(x => x.Depot == employee.BranchID).ToList();
                AcInfo.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            }

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllCashBankAccountNew()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Cash/Bank Account ---");
            context.ChartOfAccount.Where(x => (x.ParentId == context.AccountConfiguration.FirstOrDefault(y=> y.Name == "Cash").RefValue) ||( x.ParentId == context.AccountConfiguration.FirstOrDefault(y => y.Name == "Bank").RefValue) && x.Type == "l").OrderBy(z=> z.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString()+" - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadZone()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("","--- Select Area ---");
            context.TblZones.ToList().ForEach(x => { items.Add(x.ZoneId.ToString(), x.ZoneName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadTransectionType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Type ---");
            items.Add("Increase", "Increase");
            items.Add("Decrease", "Decrease");
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadDebitAccount()
        {

            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("","--- Select Type ---");
            context.ChartOfAccount.Where(x => x.ParentId == 4078).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Bank Account ---");
            context.ChartOfAccount.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadExpenseAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Bank Account ---");
            context.ChartOfAccount.Where(a=> a.RootAccountType == "ex" && a.Type== "l").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() +" - "+ x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAccounts()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Bank Account ---");
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAccountHeads()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "---- Select A/C ----");
            context.ChartOfAccount.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadPShipments()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select PShifment ---");
            items.Add("Allowed", "Allowed");
            items.Add("Not Allowed", "Not Allowed");
            //context.Customers.ToList().ForEach(x => { items.Add(x.AreaName, x.AreaName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadCreditAdvance()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            items.Add("Credit", "Credit");
            items.Add("Advance", "Advance");
            //items.Add("AdvAdj", "Advance Adjustment");
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadMOP()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Mode of Payment ---");
            context.MOPs.OrderBy(x => x.MoPName).ToList().ForEach(x => { items.Add(x.MoPId.ToString(), x.MoPName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadIncoterms()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Incoterms ---");
            context.Incoterms.OrderBy(x => x.IncotermName).ToList().ForEach(x => { items.Add(x.IncotermsId.ToString(), x.IncotermName); });
            return new SelectList(items, "Key", "Value");
        }
        
        //public static SelectList LoadAllFGStores()
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    items.Add("", "--- Select Store ---");
        //    //context.Store.ToList().ForEach(x => { items.Add(x.IncotermsId.ToString(), x.IncotermName); });
        //    var store = (from fgStore in context.Store where fgStore.Type == "FG" select new { Name = fgStore.Name, id = fgStore.Id }).ToList();
        //    store.ForEach(m => { items.Add(m.id.ToString(), m.Name.ToString()); });
        //    return new SelectList(items, "Key", "Value");
        //}
        public static SelectList LoadAllFGStores()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            //context.Store.ToList().ForEach(x => { items.Add(x.IncotermsId.ToString(), x.IncotermName); });
            var store = (from fgStore in context.Store where fgStore.Type == "FG" || fgStore.Type == "Delivery Van" select new { Name = fgStore.Name, id = fgStore.Id }).OrderBy(x=> x.Name).ToList();
            store.ForEach(m => { items.Add(m.id.ToString(), m.Name.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList GetIssuFrom()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            //context.Store.ToList().ForEach(x => { items.Add(x.IncotermsId.ToString(), x.IncotermName); });
            var store = (from fgStore in context.Store where fgStore.Name.ToLower() == "floor fg" select new { Name = fgStore.Name, id = fgStore.Id }).OrderBy(x => x.Name).ToList();
            store.ForEach(m => { items.Add(m.id.ToString(), m.Name.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList GetIssuTo()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            //context.Store.ToList().ForEach(x => { items.Add(x.IncotermsId.ToString(), x.IncotermName); });
            var store = (from fgStore in context.Store where fgStore.Name.ToLower() == "store fg" select new { Name = fgStore.Name, id = fgStore.Id }).OrderBy(x => x.Name).ToList();
            store.ForEach(m => { items.Add(m.id.ToString(), m.Name.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        
        public static SelectList LoadLCType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select LCType ---");
            items.Add("Inventory", "Inventory");
            items.Add("Fixed Asset", "Fixed Asset");
            //context.Customers.ToList().ForEach(x => { items.Add(x.AreaName, x.AreaName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadVehicleType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Vehicle Type ---");
            items.Add("Delivery Van", "Delivery Van");
            items.Add("Motor Cycle", "Motor Cycle");
            items.Add("Management Car", "Management Car");
            //context.Customers.ToList().ForEach(x => { items.Add(x.AreaName, x.AreaName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadPort()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Port ---");
            context.Ports.OrderBy(x => x.PortName).ToList().ForEach(x => { items.Add(x.PortID.ToString(), x.PortName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadBeneficiaryBanks()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Beneficiary Bank ---");
            context.BeneficiaryBanks.OrderBy(x => x.BankName).ToList().ForEach(x => { items.Add(x.BankId.ToString(), x.BankName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadIssuingBanks()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select IssuingBank ---");
            var issuingBanke = (from coa in context.ChartOfAccount
                                join a in context.AccountConfiguration
                                on coa.ParentId equals a.RefValue
                                where a.Name == "bank"
                                orderby coa.Name ascending
                                select new { Id = coa.Id, Name = coa.Name }).ToList();
            issuingBanke.ForEach(v => { items.Add(v.Id.ToString(), v.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadCurrency()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select PI ---");
            context.Currencys.OrderBy(x => x.CurrencyName).ToList().ForEach(x => { items.Add(x.CurrencyId.ToString(), x.CurrencyName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSupplierForLC()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Supplier ---");
            var supplierForLC = (from ipi in context.ImportProformaInvoices
                                 join s in context.Supplier
            on ipi.SupplierId equals s.Id
                                 select new { Id = s.Id, Name = s.SupplierName }).Distinct().ToList();
            supplierForLC.ForEach(k => { items.Add(k.Id.ToString(), k.Name.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadPI(int supplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Proforma Invoice ---");
            var IpIs = (from p in context.ImportProformaInvoices.Where(x => x.SupplierId == supplierId).ToList()
                        //join c in context.ImportLC.ToList() on p.IPIId equals c.IPIId into data
                        //from selected in data.DefaultIfEmpty()
                        //where selected == null
                        select new { id = p.IPIId, p.IPINO }).ToList();
            IpIs.ForEach(x => { items.Add(x.id.ToString(), x.IPINO.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllPI()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Proforma Invoice ---");
            var IpIs = context.ImportProformaInvoices.ToList();
            IpIs.ForEach(x => { items.Add(x.IPIId.ToString(), x.IPINO.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSubArea()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Sub Area ---");
            context.Customers.ToList().ForEach(x => { items.Add(x.AreaName, x.AreaName); });
            return new SelectList(items, "Key", "Value");
        }

        internal static dynamic LoadALLCostGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            context.EmployeeSection.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.CGroup); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllSupplierGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Supplier Group ---");
            context.SupplierGroups.Select(x => new { x.SgroupId, x.SgroupName }).OrderBy(x => x.SgroupName).ToList().ForEach(x => { items.Add(x.SgroupId.ToString(), x.SgroupName); });
            return new SelectList(items, "Key", "Value");
        }

        internal static SelectList LoadBatch()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Batch ---");
            context.Batch.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.BatchNo); });
            return new SelectList(items, "Key", "Value");
        }

        //internal static dynamic LoadSingleStore(object factoryFG)
        //{
        //    throw new NotImplementedException();
        //}

        internal static dynamic LoadProductionStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            //var sysSet = context.SysSet.FirstOrDefault();
            var floor = context.Store.ToList().FirstOrDefault(x => x.Name == "Floor FG");
            if (floor != null)
            {
                items.Add(floor.Id.ToString(), floor.Name);
            }
            return new SelectList(items, "Key", "Value");
        }

        internal static dynamic LoadAllRMItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            //GetInfo().Where(x => x.Type.ToLower() == "l" && x.RootAcType == "RM").ToList().ForEach(y => { items.Add(y.Id.ToString(), y.Id + " - " + y.Name); });
            GetItemInfo().Where(x => x.Type.ToLower() == "l" && x.RootAcType == "RM").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        internal static dynamic LoadItemByGroup(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            //GetInfo().Where(x => x.Type.ToLower() == "l" && x.RootAcType == "RM").ToList().ForEach(y => { items.Add(y.Id.ToString(), y.Id + " - " + y.Name); });
            GetItemInfo().Where(x => x.Type.ToLower() == "l" && x.RootAcType == "FG" && x.ParentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        internal static dynamic LoadAllFGGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            //GetInfo().Where(x => x.Type.ToLower() == "l" && x.RootAcType == "RM").ToList().ForEach(y => { items.Add(y.Id.ToString(), y.Id + " - " + y.Name); });
            GetItemInfo().Where(x => x.Type.ToLower() == "g" && x.RootAcType == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        internal static dynamic LoadAllFGItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.rootAccountType == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name ); });
            return new SelectList(items, "Key", "Value");
        }

        internal static dynamic LoadProductionStoreForRM()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            //var sysSet = context.SysSet.FirstOrDefault();
            var floor = context.Store.ToList().FirstOrDefault(x => x.Name == "Floor RM");
            if (floor != null)
            {
                items.Add(floor.Id.ToString(), floor.Name);
            }
            return new SelectList(items, "Key", "Value");
        }

        public static List<string> LoadArea()
        {
            BEEContext context = new BEEContext();
            List<string> items = new List<string>();
            items.Add("--- Select Staff Type ---");
            context.Customers.ToList().Distinct();
            return items;
        }

        public static SelectList LoadCustomerArea()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Area ---");
            var areaname = context.Customers.Select(x => x.AreaName).Distinct().ToList();
            areaname.ForEach(x =>
            {
                if (x != "")
                {
                    items.Add(x, x);
                }
            });
            return new SelectList(items, "Key", "Value");

        }

        internal static dynamic LoadItemGroupByBatchNo(string batchNo)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");

            if (!string.IsNullOrEmpty(batchNo))
            {
                var batch = context.BatchLineItem.Where(x => x.BatchNo == batchNo.Trim()).ToList();
                (from b in batch
                 from i in context.ChartOfInventory.ToList()
                 where b.ItemID == i.Id
                 from g in context.ChartOfInventory.ToList()
                 where g.Id == i.parentId
                 select g).ToList().ForEach(x => { try { items.Add(x.Id.ToString(), x.Name); } catch { } });

            }
            return new SelectList(items, "Key", "Value");
        }

        internal static dynamic LoadItemByBatchNo(string batchNo)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");

            if (!string.IsNullOrEmpty(batchNo))
            {
                var batch = context.BatchLineItem.Where(x => x.BatchNo == batchNo.Trim()).ToList();
                (from b in batch
                 from i in context.ChartOfInventory.ToList()
                 where b.ItemID == i.Id
                 from g in context.ChartOfInventory.ToList()
                 where g.Id == i.parentId
                 //select new { Id = b.ItemID, Name = i.Name } ).ToList().ForEach(x => { try { items.Add(x.Id.ToString(), x.Name); } catch { } });
                 select i).ToList().ForEach(x => { try { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name); } catch { } });

            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadBranchInfo()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Unit ---");
            //if (HttpContext.Current.User.IsInRole("Admin") || HttpContext.Current.User.IsInRole("HraAdmin"))
            //{
                context.BranchInformation.ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
            //}
            //else
            //{
            //    var employee = context.Employees.FirstOrDefault(x => x.LogInUserName == HttpContext.Current.User.Identity.Name);
            //    context.BranchInformation.Where(x => x.Slno == employee.BranchID).ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });

            //}
            var item = from s in items
                       orderby s.Value
                       select s;
            return new SelectList(item, "Key", "Value");
        }

        //public static SelectList LoadBranchInfo()
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    items.Add("", "--- Select Center ---");
        //    context.BranchInformation.ToList().ForEach(x => { items.Add(x.Slno.ToString(), x.BrnachName); });
        //    return new SelectList(items, "Key", "Value");
        //}
        public static SelectList LoadCategoryType()//demo category ,not from database
        {
            //BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Category ---");
            items.Add("corporate", "Corporate");
            items.Add("national", "National");
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            GetItemInfo().Where(x => x.Type.ToLower() == "g" && x.RootAcType.ToUpper() == "FG".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "FG".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadGeneralStoreGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            GetItemInfo().Where(x => x.Type.ToLower() == "g" && x.RootAcType.ToUpper() == "GS".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "FG".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadItemGroupfa()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            GetItemInfo().Where(x => x.Type.ToLower() == "g" && x.RootAcType.ToUpper() == "FA".ToUpper()).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            //context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "FG".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        internal static SelectList LoadItemBatchwise(string batchNo)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            (from p in context.BatchLineItem.ToList()
             join item in context.ChartOfInventory.ToList() on p.ItemID
            equals item.Id
             where p.BatchNo.Trim().ToLower() == batchNo.Trim().ToLower()
             select new { Name = item.Name, Id = item.Id }).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadItemGroupRM()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            //context.ChartOfInventory.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "RM".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadGSItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            //context.ChartOfInventory.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "GS".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSPDepartments()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Department ---");
            //context.ChartOfInventory.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            context.SPDepartment.OrderBy(x => x.SPDName).ToList().ForEach(x => { items.Add(x.SPDID.ToString(), x.SPDName); });
            return new SelectList(items, "Key", "Value");
        }
        //public static List<Item_Info> GetInfo()
        //{
        //    BEEContext context = new BEEContext();
        //    string sql = "exec Item_info";
        //    List<Item_Info> Data = context.Database.SqlQuery<Item_Info>(sql).ToList();
        //    return Data;
        //}
        public static SelectList LoadGroup(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            if (id == 2)
            {
                context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "SP".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            }
            else
            {
                context.ChartOfInventory.Where(x => x.type.ToLower() == "g" && x.rootAccountType.ToUpper() == "GS".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            }
            return new SelectList(items, "Key", "Value");

        }

        public static SelectList LoadItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                GetItemInfo().Where(x => x.Type.ToLower() == "l" && x.ParentId == group).ToList().ForEach(y => { items.Add(y.Id.ToString(), y.Id + " - " + y.Name); });
               // context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name + " - " + x.PacSize); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadRmItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group && x.rootAccountType == "RM").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name); });
                return new SelectList(items, "Key", "Value");
            }


        }
        public static SelectList LoadFGItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group && x.rootAccountType == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }

        public static SelectList LoadStore(int? storeId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (storeId == null || storeId == 0)
            {
                items.Add("", "--- Select Store ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Store ---");
                context.Store.Where(x => x.DepotID == storeId && (x.Type == "Delivery Van" || x.Type == "FG")).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }

        public static SelectList LoadGSStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("", "--- Select Store ---");
            context.Store.Where(x => x.Type == "SP" || x.Type == "GS").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");


        }


        public static SelectList LoadFGStoreByDepot(int? depot)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depot == null || depot == 0)
            {
                items.Add("", "--- Select Store ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Store ---");
                context.Store.Where(x => x.DepotID == depot && x.Type == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadFGStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            
                items.Add("", "--- Select Store ---");
                context.Store.Where(x =>  x.Type == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            

        }
        public static SelectList LoadVehicleByDepot(int? depot)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depot == null || depot == 0)
            {
                items.Add("", "--- Select Vehicle ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Vehicle ---");
                context.Store.Where(x => x.DepotID == depot && x.Type == "Delivery Van").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadVehicle()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
           
                items.Add("", "--- Select Vehicle ---");
                context.Store.Where(x =>x.Type == "Delivery Van").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
        }
        //public static SelectList LoadDriver(/*int? storeId*/)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    //if (storeId == null || storeId == 0)
        //    //{
        //    //    items.Add("", "--- Select Driver ---");
        //    //    return new SelectList(items, "Key", "Value");
        //    //}
        //    //else
        //    //{
        //        items.Add("", "--- Select Driver ---");
        //        var emp = (from e in context.Employees join f in context.FunctionalDesignation on e.FunctDesignation equals f.ID select new { emp = e, func = f }).ToList();
        //        emp.Where(x => /*x.emp.BranchID == storeId &&*/ x.func.FuncDesignation == "Driver").ToList().ForEach(x => { items.Add(x.emp.Id.ToString(), x.emp.Name); });
        //        return new SelectList(items, "Key", "Value");
        //    //}

        //}
        
        public static SelectList LoadAllStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            context.Store.OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllQCNo()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select QC No ---");
            context.QualityControl.OrderBy(x => x.QcNo).ToList().ForEach(x => { items.Add(x.QcNo.ToString(), x.QcNo.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllMRNo()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select MR No ---");
            context.GoodsReceiveNote.OrderBy(x => x.GRNNo).ToList().ForEach(x => { items.Add(x.GRNNo.ToString(), x.GRNNo.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllRMStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            context.Store.Where(x => x.Type == "RM").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllFGStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            context.Store.Where(x => x.Type == "FG").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllRootAcType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account type ---");
            items.Add("RM", " RM ");
            items.Add("FG", " FG ");
            items.Add("WIP", " WIP ");
            items.Add("BP", " BP ");
            

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllFGStoreByDepot(int? depotId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depotId == null || depotId == 0)
            {
                items.Add("", "--- Select Store ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Store ---");
                context.Store.Where(x => x.Type == "FG" && x.DepotID == depotId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadAllRMStoreByDepot(int? depotId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depotId == null || depotId == 0)
            {
                items.Add("", "--- Select Store ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Store ---");
                context.Store.Where(x => x.Type == "RM" && x.DepotID == depotId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }

        //public static SelectList LoadAllRMStore()
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();

        //        items.Add("", "--- Select Store ---");
        //        context.Store.Where(x => x.Type == "RM").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
        //        return new SelectList(items, "Key", "Value");
            

        //}

        public static SelectList LoadAllFGRevStoreByDepot(int? depotId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depotId == null || depotId == 0)
            {
                items.Add("", "--- Select Store ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                var floorFg = context.Store.Where(x => x.Name.ToLower() == "floorFg" && x.Name.ToLower() == "factoryFg").ToList();
                items.Add("", "--- Select Store ---");
                context.Store.Where(x => x.Type == "FG" && x.Name.ToLower() != "floor fg" && x.Name.ToLower() != "factory Fg").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
                return new SelectList(items, "Key", "Value");
            }

        }

        public static SelectList LoadAccount(int? depot)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depot == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                var acc = (from p in context.ChartOfAccount
                           join c in context.TblCashAccountConfigurations
                           on p.Id equals c.CashBankID
                           join d in context.BranchInformation on c.OperationUnitID equals d.Slno
                           where d.Slno == depot && (p.ParentId == 8 || p.ParentId == 9) && p.Type.ToLower() == "l"
                           select new { p.Name, p.Id }).ToList();
                acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });

                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadAllAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            var acc = context.ChartOfAccount.Where(x => x.Type.ToLower() == "l").Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllPaymentAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            var acc = context.ChartOfAccount.Where(x => x.Type.ToLower() == "l" && x.Id != (from c in context.ChartOfAccount
                                                                                            join i in context.ImportCostItems on c.Id equals i.CostGroupId
                                                                                            where x.Id == i.CostGroupId
                                                                                            select i.CostGroupId).Distinct().ToList().FirstOrDefault()).Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();

            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllDebitAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            //var acc = context.ImportCostItems.Where(x => x.AccountHead).Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            var acc = (from c in context.ChartOfAccount
                       join
l in context.ImportCostItems on c.Id equals l.CostGroupId
                       select new { c.Id, c.Name }).Distinct().ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllDebitAccountNew()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            //var acc = context.ImportCostItems.Where(x => x.AccountHead).Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            var acc = (from c in context.ImportCostGroups
                       join
l in context.ImportCostItems on c.CGId equals l.CostGroupId
                       select new { c.CGId, c.CostGroupName }).Distinct().ToList();
            acc.ForEach(x => { items.Add(x.CGId.ToString(), x.CostGroupName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllSubLedgerAccount(int? accid)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (accid == null)
            {
                items.Add("", "--- Select Account ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Account ---");
                var acc = (from c in context.ImportCostItems
                           join
l in context.ChartOfAccount on c.CostGroupId equals l.Id
                           where c.CostGroupId == accid
                           select new { c.SlnNo, c.CostItem }).Distinct().ToList();
                acc.ForEach(x => { items.Add(x.SlnNo.ToString(), x.CostItem); });

                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadAllAccountWithoutBankAndCash()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            var bank = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank");
            var cash = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank");
            var acc = context.ChartOfAccount.Where(x => x.Type.ToLower() == "l" && x.ParentId != bank.RefValue && x.ParentId != cash.RefValue).Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(),x.Id.ToString() +" - "+ x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllExpenseAccount()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");
            var acc = context.ChartOfAccount.Where(x => x.Type.ToLower() == "l" && x.RootAccountType == "ex").Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(),x.Id.ToString() +" - "+ x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadReceiveAcc()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Account ---");

            var cash = context.AccountConfiguration.FirstOrDefault(x => x.Name == "cash").RefValue;
            var bank = context.AccountConfiguration.FirstOrDefault(x => x.Name == "bank").RefValue;
            var acc = (from c in context.ChartOfAccount
                       where (c.ParentId == (cash) || c.ParentId == (bank))
                       select new { c.Id, c.Name }).OrderBy(x => x.Name).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        
        public static SelectList LoadCostGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            var costGroup = context.ChartOfAccount.ToList();
            costGroup.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");

        }
        public static SelectList LoadAllCostGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            var costGroup = context.ChartOfAccount.ToList();
            costGroup.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");

        }
        public static SelectList LoadSingleStore(int? storeId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            var acc = context.Store.Where(x => x.Id == storeId).Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            acc.ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");

        }

        public static SelectList LoadStoreRM()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            context.Store.Where(x => x.Type.ToUpper() == "RM".ToUpper()).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadStoreFAStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            context.Store.Where(x => x.Type.ToUpper() == "FA".ToUpper()).OrderBy(x=>x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadGBPGSBFPBB()
        {
           
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Bill Type---");
            items.Add("GPB", "GPB");
            items.Add("GSB", "GSB");
            items.Add("FPB", "FPB");
            //items.Add("ILCB", "ILCB");
            return new SelectList(items, "Key", "Value");
            
        }

        public static SelectList LoadAdvanceID(int supplierId)
        {
            BEEContext context = new BEEContext();
            var advInfo = context.PayBillInfo.Where(x => x.CreditAdvance == "Advance" && x.SupplierId == supplierId).ToList();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            foreach (var item in advInfo)
            {
                var sumOfAAomunt = context.PayBillLines.Where(x => x.PBID == item.PBID && x.BillType != "ASP").ToList().Select(x => x.PaymentAmount).DefaultIfEmpty(0).Sum();
                // var sumOfAAomunt = context.PayBillAdvances.Where(x => x.PBID == item.PBID).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                if (sumOfAAomunt < item.PaidAmt)
                {
                    var amount = item.PaidAmt - sumOfAAomunt;
                    var advance = new { SPVNo = item.PBID, Amount = amount, Date = item.TDate };
                    items.Add(item.PBID.ToString(), item.PBID.ToString());

                }

            }
            return new SelectList(items, "Key", "Value");
            
        }

        public static SelectList LoadGPBNo(int? suplierId)
        {
            decimal paid, adj,totalPaid;
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var gbpNo = (from g in context.PurchaseEntry where g.SupplierId == suplierId select g.BillNo).ToList();
            //Convert.ToDecimal((from x in context.PayBillLines where x.BillNo == suplierId && x.BillType == "GPB" select x.PaymentAmount).Sum())
            //var gbpNo = (from g in context.PurchaseEntry where g.SupplierId == suplierId && g.TotalPayable<=g.PaidAmount select g.BillNo).ToList();
            gbpNo.ForEach(
                m => {
                    decimal bill = Convert.ToDecimal((from g in context.PurchaseEntry where g.BillNo == m select g.TotalPayable).Single());
                    paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where g.BillNo == m && g.BillType == "GPB" select g.PaymentAmount).Sum());
                    adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == m && g.BillType == "GPB" select g.Amount).Sum());
                    totalPaid = paid + adj;
                    if (bill> totalPaid)
                    {
                        items.Add(m.ToString(), m.ToString());
                    }
                });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadGSBNo(int? suplierId)
        {
            decimal paid=0,adj,totalPaid;
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var gsbNo = (from g in context.GeneralBills where g.SupplierId == suplierId select g.GBENo).ToList();
            //var gbpNo = (from g in context.PurchaseEntry where g.SupplierId == suplierId && g.TotalPayable<=g.PaidAmount select g.BillNo).ToList();
            gsbNo.ForEach(m => {
            decimal bill = Convert.ToDecimal((from g in context.GeneralBills where g.GBENo == m select g.NetAmount).Single());
            paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where g.BillNo == m && g.BillType == "GSB" select g.PaymentAmount).Sum());
            adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == m && g.BillType == "GSB" select g.Amount).Sum());
            totalPaid = paid + adj;
            if (bill > totalPaid)
            {
                items.Add(m.ToString(), m.ToString());
            }
            });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadFPBNo(int? suplierId)
        {
            decimal paid, adj,totalPaid;
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var fpbNo = (from g in context.FPB where g.SupplierId == suplierId select g.BillNo).ToList();
            //var gbpNo = (from g in context.PurchaseEntry where g.SupplierId == suplierId && g.TotalPayable<=g.PaidAmount select g.BillNo).ToList();
            fpbNo.ForEach(m => {
                decimal bill = Convert.ToDecimal((from g in context.FPB where g.BillNo == m select g.TotalPayable).Single());
                    paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where g.BillNo == m && g.BillType == "FPB" select g.PaymentAmount).Sum());
                    adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == m && g.BillType == "FPB" select g.Amount).Sum());
                    totalPaid = paid + adj;
                if (bill > totalPaid)
                {
                    items.Add(m.ToString(), m.ToString());
                }
            });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadILCBNo(int? suplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            var ilcbNo = (from g in context.ILCB where g.SupplierID == suplierId select g.ILCBNo).ToList();
            //var gbpNo = (from g in context.PurchaseEntry where g.SupplierId == suplierId && g.TotalPayable<=g.PaidAmount select g.BillNo).ToList();
            ilcbNo.ForEach(m => { items.Add(m.ToString(), m.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadGBENo(int? suplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            //GetAllGBE().Where(x => x.SupplierId == suplierId).ToList().ForEach(x => { items.Add(x.GBENo.ToString(), x.GBENo.ToString()); });
            context.GeneralBills.Where(x => x.SupplierId == suplierId && x.NetAmount<=x.PaidAmount).ToList().ForEach(x => { items.Add(x.GBENo.ToString(), x.GBENo.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAdjustSPVNo(int? suplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select ---");
            context.PayBillInfo.Where(x => x.SupplierId == suplierId).ToList().ForEach(x => { items.Add(x.PBID.ToString(), x.PBID.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadSupplier(int? groupId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (groupId == null || groupId == 0)
            {
                items.Add("", "--- Select Supplier ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Supplier ---");
                context.Supplier.Where(x => x.GroupID == groupId).Select(x => new { x.Id, x.SupplierName }).OrderBy(x => x.SupplierName).ToList().ForEach(x => { items.Add(x.Id.ToString(),x.Id + " - " + x.SupplierName); });
                //context.Supplier.Where(x => x.GroupID == groupId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.SupplierName); });
                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadAllSupplier()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> supplier = new Dictionary<string, string>();
            supplier.Add("", "--- Select Supplier ---");
            context.Supplier.OrderBy(x => x.SupplierName).ToList().ForEach(x => { supplier.Add(x.Id.ToString(), x.SupplierName); });
            return new SelectList(supplier, "Key", "Value");

        }
        public static SelectList LoadAllImportSupplier()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> supplier = new Dictionary<string, string>();
            supplier.Add("", "--- Select Supplier ---");
            context.Supplier.Where(x => x.GroupID ==1).ToList().ForEach(x => { supplier.Add(x.Id.ToString(), x.SupplierName); });
            return new SelectList(supplier, "Key", "Value");

        }
        public static SelectList LoadAllLocalSupplier()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> supplier = new Dictionary<string, string>();
            supplier.Add("", "--- Select Supplier ---");
            context.Supplier.Where(x => x.GroupID == 2).ToList().ForEach(x => { supplier.Add(x.Id.ToString(), x.SupplierName); });
            return new SelectList(supplier, "Key", "Value");

        }
        public static SelectList LoadAllSupplierByGroup(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> supplier = new Dictionary<string, string>();
            supplier.Add("", "--- Select Supplier ---");
            context.Supplier.Where(x=> x.GroupID == id).ToList().ForEach(x => { supplier.Add(x.Id.ToString(), x.SupplierName); });
            return new SelectList(supplier, "Key", "Value");

        }

        public static SelectList LoadAllGender()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("", "--- Select Gender ---");
            context.Gender.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllReligion()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Religion ---");
            context.Religion.ToList().ForEach(x => { items.Add(x.sln.ToString(), x.ReligionName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllMaritalStatus()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Marriage Status ---");
            context.MarriageStatuse.ToList().ForEach(x => { items.Add(x.slno.ToString(), x.mstatus); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadGradeList()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Grade ---");
            context.Grade.ToList().ForEach(x => { items.Add(x.GradeID.ToString(), x.GradeName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllDesignation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Designaiton ---");
            context.Designation.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllDepartment()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Department ---");
            context.Department.ToList().ForEach(x => { items.Add(x.DeptmentID.ToString(), x.DeprtmntName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadPayeeName()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Name ---");
            context.ReceiveVouchers.ToList().Select(x => x.PayeeName).Distinct().ToList().ForEach(x => { items.Add(x, x); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllFuncDesignation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Functional Designation ---");
            context.FunctionalDesignation.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.FuncDesignation); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllSection()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Cost Group ---");
            context.EmployeeSection.OrderBy(x => x.CGroup).ToList().ForEach(x => { items.Add(x.ID.ToString(), x.CGroup); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadOneUser()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            var user = context.Employees.FirstOrDefault(m => m.LogInUserName == HttpContext.Current.User.Identity.Name);
            items.Add(user.Id.ToString(), user.Name.ToString());
            return new SelectList(items, "Key", "Value");
        }
        internal static SelectList ProbableRsm(int? depotId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (depotId == null)
            {
                items.Add("", "--- Select RSM ---");
            }
            else
            {
                items.Add("", "--- Select RSM ---");
                context.Employees.Where(x => x.Designation == context.Designation.FirstOrDefault(y => y.Name.ToLower() == "rsm" || y.Name.ToLower() == "Area Manager" || y.Name.ToLower() == "Sr. Area Manager" || y.Name.Trim().ToLower() == "sr.rsm").Id && x.BranchID == depotId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            }

            return new SelectList(items, "Key", "Value");
        }
        internal static SelectList Rsm(int? depotId)
        {
            if (depotId == null)
            {
                depotId = 0;
            }
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select RSM ---");
            //context.Employees.Where(x => x.Designation == context.Designation.FirstOrDefault(y => y.Name.ToLower() == "rsm" || y.Name.ToLower() == "Area Manager" || y.Name.ToLower() == "Sr. Area Manager" || y.Name.Trim().ToLower() == "sr.rsm").Id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            (from emp in context.Employees
             join
             des in context.Designation on emp.Designation equals des.Id
             where (des.Name == "rsm" || des.Name == "Area Manager" || des.Name == "Sr.RSM" || des.Name == "Sr. Area Manager" || des.Name == "Operation Director") && emp.BranchID == depotId
             select new { emp.Id, emp.Name }).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        internal static SelectList LoadAllTSO()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select TSO ---");
            //context.Employees.Where(x => x.Designation == context.Designation.FirstOrDefault(y => y.Name.ToLower() == "tso" || y.Name.ToLower() == "ase" || y.Name.ToLower() == " sr.tso").Id).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            (from e in context.Employees
             join
             d in context.Designation on e.Designation equals d.Id
             where (d.Name == "tso" || d.Name == "ase" || d.Name == "sr.tso" || d.Name == "Operation Director")
             select new { e.Id, e.Name, DesignationName = d.Name }).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
//        internal static SelectList LoadTSO(int? branchId)
//        {
//            BEEContext context = new BEEContext();
//            Dictionary<string, string> items = new Dictionary<string, string>();
//            var isAdmin = HttpContext.Current.User.IsInRole("SalAdmin");
//            items.Add("", "--- Select TSO ---");
//            if (isAdmin == true)
//            {
//                (from e in context.Employees
//                 join
//d in context.FunctionalDesignation on e.FunctDesignation equals d.ID
//                 where d.FuncDesignation == "tso" || d.FuncDesignation == "ase" || d.FuncDesignation == "sr.tso"||d.FuncDesignation== "Operation Director"
//                 select new { e.Id, e.Name, DesignationName = d.FuncDesignation }).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); }); ;


//            }
//            else
//            {
//                (from e in context.Employees
//                 join
//d in context.FunctionalDesignation on e.FunctDesignation equals d.ID
//                 where (d.FuncDesignation == "tso" || d.FuncDesignation == "ase" || d.FuncDesignation == "sr.tso" || d.FuncDesignation == "Operation Director") && e.BranchID == branchId
//                 select new { e.Id, e.Name, DesignationName = d.FuncDesignation }).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
//            }


//            return new SelectList(items, "Key", "Value");
//        }
        public static SelectList LoadAllStaffType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("", "--- Select Staff Type ---");
            items.Add("Management", "Management");
            items.Add("Non-Management", "Non-Management");

            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllEducation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Education ---");
            context.HighestEducation.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Education); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadGroupOrItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("", "--- Group or Item ---");
            items.Add("G", "Group");
            items.Add("L", "Item");

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllUOM()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select UOM ---");
            context.UOM.OrderBy(x => x.Name).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllWOT()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select WO Type ---");
            context.WOTypes.OrderBy(x => x.WOTypeName).ToList().ForEach(x => { items.Add(x.WOTID.ToString(), x.WOTypeName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllCountry()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Country ---");
            context.Countries.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadStoreType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store Type ---");
            items.Add("RM", "RM");
            items.Add("FG", "FG");
            //items.Add("FS", "FS");
            items.Add("FA", "FA");
            items.Add("SP", "SP");
            items.Add("GS", "");
            //items.Add("Management Car", "Management Car");
            //items.Add("Motor Cycle", "Motor Cycle");

            return new SelectList(items, "Key", "Value");
        }
        // to generate sub ledger type
        public static SelectList LoadSubLedgerType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Sub Ledger Type ---");
            items.Add("Delivery Van", "Delivery Van");
            items.Add("Distribution Van", "Distribution Van");
            items.Add("Management Car", "Management Car");
            items.Add("Motor Cycle", "Motor Cycle");

            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadAllFreezerWorkOrder(int? suppliierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (suppliierId == null)
            {
                items.Add("", "--- Select Work Order ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                //items.Add("", "--- Select Work Order ---");
                //context.WorkOrder.Where(x => x.SupplierID == suppliierId).ToList().ForEach(x => { items.Add(x.WONo.ToString(), x.WOCode.ToString()); });
                //return new SelectList(items, "Key", "Value");

                items.Add("", "--- Select Work Order ---");
                //var item = (from M in context.FreezerWorkOrder
                //            join L in context.FreezerWorkOrderLineItem on M.WONo equals L.WONo
                //            join S in context.Supplier on M.SupplierID equals S.Id
                //            join I in context.ChartOfInventory on L.ItemID equals I.Id
                //            join G in context.ChartOfInventory on I.parentId equals G.Id
                //            let Q = (from RM in context.GoodsReceiveNote
                //                     join RL in context.GoodsReceiveNoteLineItem on RM.GRNNo equals RL.GRNNo
                //                     join S in context.Supplier on M.SupplierID equals S.Id
                //                     where (RM.SupplierID == S.Id) && (RM.WONo == M.WONo) && (RL.ItemID == I.Id)
                //                     select RL.Qty).DefaultIfEmpty(0).Sum()
                //            let p = (L.ItemQty - Q)
                //            where (p > 0) && (S.Id == suppliierId)
                //            orderby M.WODate descending
                //            select new { M.WOCode, M.WONo }).Distinct().ToList();

                var item = (from M in context.FreezerWorkOrder
                               join L in context.FreezerWorkOrderLineItem on M.WONo equals L.WONo
                               join S in context.Supplier on M.SupplierID equals S.Id
                               join I in context.FreezerModelDescription on L.ItemID equals I.ID
                               let Q = (from RM in context.GoodsReceiveNote
                                        join RL in context.GoodsReceiveNoteLineItem on RM.GRNNo equals RL.GRNNo
                                        join S in context.Supplier on M.SupplierID equals S.Id
                                        where (RM.SupplierID == S.Id) && (RM.WONo == M.WONo) && (RL.ItemID == I.ID)
                                        select RL.Qty).DefaultIfEmpty(0).Sum()
                               let p = (L.ItemQty - Q)
                               where (p > 0) && (S.Id == suppliierId)
                               orderby M.WODate descending
                               select new { M.WOCode, M.WONo }).Distinct().ToList();

                item.ForEach(x => items.Add(x.WONo.ToString(), x.WOCode.ToString()));

                return new SelectList(items, "Key", "Value");
            }

        }

        public static SelectList LoadAllFreezerWorkOrderSearch(int? suppliierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (suppliierId == null)
            {
                items.Add("", "--- Select Work Order ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                //items.Add("", "--- Select Work Order ---");
                //context.WorkOrder.Where(x => x.SupplierID == suppliierId).ToList().ForEach(x => { items.Add(x.WONo.ToString(), x.WOCode.ToString()); });
                //return new SelectList(items, "Key", "Value");

                items.Add("", "--- Select Work Order ---");
                var item = (from M in context.FreezerWorkOrder
                            join L in context.FreezerWorkOrderLineItem on M.WONo equals L.WONo
                            join S in context.Supplier on M.SupplierID equals S.Id
                            join I in context.FreezerModelDescription on L.ItemID equals I.ID
                            let Q = (from RM in context.GoodsReceiveNote
                                     join RL in context.GoodsReceiveNoteLineItem on RM.GRNNo equals RL.GRNNo
                                     join S in context.Supplier on M.SupplierID equals S.Id
                                     where (RM.SupplierID == S.Id) && (RM.WONo == M.WONo) && (RL.ItemID == I.ID)
                                     select RL.Qty).DefaultIfEmpty(0).Sum()
                            where (S.Id == suppliierId)
                            orderby M.WODate descending
                            select new { M.WOCode, M.WONo }).Distinct().ToList();

                item.ForEach(x => items.Add(x.WONo.ToString(), x.WOCode.ToString()));

                return new SelectList(items, "Key", "Value");
            }

        }

        //public static SelectList LoadAllWorkOrder(int? suppliierId)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    if (suppliierId == null)
        //    {
        //        items.Add("", "--- Select Work Order ---");
        //        return new SelectList(items, "Key", "Value");
        //    }
        //    else
        //    {
        //        //items.Add("", "--- Select Work Order ---");
        //        //context.WorkOrder.Where(x => x.SupplierID == suppliierId).ToList().ForEach(x => { items.Add(x.WONo.ToString(), x.WOCode.ToString()); });
        //        //return new SelectList(items, "Key", "Value");

        //        items.Add("", "--- Select Work Order ---");
        //        var item = (from M in context.WorkOrder
        //                    join L in context.WorkOrderLineItem on M.WONo equals L.WONo
        //                    join S in context.Supplier on M.SupplierID equals S.Id
        //                    join I in context.ChartOfInventory on L.ItemID equals I.Id
        //                    join G in context.ChartOfInventory on I.parentId equals G.Id
        //                    let Q = (from RM in context.GoodsReceiveNote
        //                             join RL in context.GoodsReceiveNoteLineItem on RM.GRNNo equals RL.GRNNo
        //                             join S in context.Supplier on M.SupplierID equals S.Id
        //                             where (RM.SupplierID == S.Id) && (RM.WONo == M.WONo) && (RL.ItemID == I.Id)
        //                             select RL.Qty).DefaultIfEmpty(0).Sum()
        //                    let p = (L.ItemQty - Q)
        //                    where (p > 0) && (S.Id == suppliierId)
        //                    orderby M.WODate descending
        //                    select new { M.WOCode, M.WONo }).Distinct().ToList();

        //        item.ForEach(x => items.Add(x.WONo.ToString(), x.WOCode.ToString()));

        //        return new SelectList(items, "Key", "Value");
        //    }

        //}

        //public static SelectList LoadAllWorkOrderSearch(int? suppliierId)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    if (suppliierId == null)
        //    {
        //        items.Add("", "--- Select Work Order ---");
        //        return new SelectList(items, "Key", "Value");
        //    }
        //    else
        //    {
        //        //items.Add("", "--- Select Work Order ---");
        //        //context.WorkOrder.Where(x => x.SupplierID == suppliierId).ToList().ForEach(x => { items.Add(x.WONo.ToString(), x.WOCode.ToString()); });
        //        //return new SelectList(items, "Key", "Value");

        //        items.Add("", "--- Select Work Order ---");
        //        var item = (from M in context.WorkOrder
        //                    join L in context.WorkOrderLineItem on M.WONo equals L.WONo
        //                    join S in context.Supplier on M.SupplierID equals S.Id
        //                    join I in context.ChartOfInventory on L.ItemID equals I.Id
        //                    join G in context.ChartOfInventory on I.parentId equals G.Id
        //                    let Q = (from RM in context.GoodsReceiveNote
        //                             join RL in context.GoodsReceiveNoteLineItem on RM.GRNNo equals RL.GRNNo
        //                             join S in context.Supplier on M.SupplierID equals S.Id
        //                             where (RM.SupplierID == S.Id) && (RM.WONo == M.WONo) && (RL.ItemID == I.Id)
        //                             select RL.Qty).DefaultIfEmpty(0).Sum()
        //                    where (S.Id == suppliierId)
        //                    orderby M.WODate descending
        //                    select new { M.WOCode, M.WONo }).Distinct().ToList();

        //        item.ForEach(x => items.Add(x.WONo.ToString(), x.WOCode.ToString()));

        //        return new SelectList(items, "Key", "Value");
        //    }

        //}

        public static SelectList LoadAllItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Item ---");
            context.ChartOfInventory.Where(x => x.type.ToLower() == "l").ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name); });
            return new SelectList(items, "Key", "Value");

        }
        //public static SelectList LoadWorkOrderDepartment()
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    context.WorkOrder.ToList().Select(x => x.RefNo).ToList().Distinct().ToList().ForEach(x => { items.Add(x, x); });
        //    return new SelectList(items, "Key", "Value");
        //}

        public static SelectList LoadEmployeeByDepot(int? depotId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (depotId == null || depotId == 0)
            {
                items.Add("", "--- Select Employee ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Employee ---");
                context.Employees.Where(x => x.BranchID == depotId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.Name); });
                return new SelectList(items, "Key", "Value");
            }
        }




        public static SelectList LoadCustomerByTSO(int? tsoId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (tsoId == null || tsoId == 0)
            {
                items.Add("", "--- Select Dealer ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Customer ---");
                context.Customers.Where(x => x.TSOId == tsoId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.CustomerName); });
                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadCustomerByTSOInCL(int? tsoId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (tsoId == null || tsoId == 0)
            {
                items.Add("", "--- Select Dealer ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Customer ---");
                //context.Customers.Where(x => x.TSOId == tsoId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.CustomerName); });
                var item = (from c in context.Customers
                            join cl in context.CustomerLocations on c.Id equals cl.CustomerID
                            where cl.CustomerID == c.Id && c.TSOId == tsoId
                            select new { c.Id, c.CustomerName }).ToList();
                item.ForEach(x =>
                { items.Add(x.Id.ToString(), x.CustomerName); });

                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadCustomer()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Customer ---");
            context.Customers.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.CustomerName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllCustomerByDepot(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Dealer ---");
            GetCustInfo().Where(x=>x.DepotId == id).OrderBy(x => x.CustomerName).ToList().ForEach(x => { items.Add(x.Id.ToString(),x.Id.ToString() +" - "+ x.CustomerName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadDebitOrCredit()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select One ---");
            items.Add("Debit", "Debit");
            items.Add("Credit", "Credit");
            return new SelectList(items, "Key", "Value");
        }
        public static decimal GBEDueAmountByGbeNo(int gbe)
        {
            BEEContext context = new BEEContext();
            string sql = "exec GetGBEDueAmount " + gbe + "";
            SupplierDueAmount sda = new SupplierDueAmount();
            sda = context.Database.SqlQuery<SupplierDueAmount>(sql).FirstOrDefault();

            return sda.DueAmount;

        }

        public static decimal GPBDueAmountByGpbNo(int gpb)
        {
            BEEContext context = new BEEContext();
            decimal paid,adj,totalPaid;
            decimal bill = Convert.ToDecimal((from g in context.PurchaseEntry where g.BillNo == gpb select g.TotalPayable).Single());
            paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where g.BillNo == gpb && g.BillType == "GPB" select g.PaymentAmount).Sum());
            adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == gpb && g.BillType == "GPB" select g.Amount).Sum());
            totalPaid = paid + adj;
            decimal DueAmount = bill - totalPaid;

            return DueAmount;
        }

        public static decimal GSBDueAmountByGbeNo(int gbe)
        {
            BEEContext context = new BEEContext();
            decimal paid, adj, totalPaid;
            decimal bill = Convert.ToDecimal((from g in context.GeneralBills where g.GBENo == gbe select g.NetAmount).Single());
            paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where (int?)g.BillNo == gbe && (string)g.BillType == "GSB" select g.PaymentAmount).Sum());
            adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == gbe && g.BillType == "GSB" select g.Amount).Sum());
            totalPaid = paid + adj;
            decimal DueAmount = bill - totalPaid;

            return DueAmount;
        }
        public static decimal FPBDueAmountByGbeNo(int fpb)
        {
            BEEContext context = new BEEContext();
            decimal paid, adj,totalPaid;
            decimal bill =Convert.ToDecimal((from g in context.FPB where g.BillNo== fpb select g.TotalPayable).Single());
            paid = Convert.ToDecimal((from g in context.PayBillLines.AsEnumerable() where g.BillNo == fpb && g.BillType == "FPB" select g.PaymentAmount).Sum());
            adj = Convert.ToDecimal((from g in context.SupplierAcAdjustments.AsEnumerable() where g.BillNo == fpb && g.BillType == "FPB" select g.Amount).Sum());
            totalPaid = paid + adj;
            decimal DueAmount = bill - totalPaid;

            return DueAmount;
        }

        public static SelectList LoadSubLedgerByAccId(int? accId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (accId == null || accId == 0)
            {
                items.Add("", "--- Select One ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select One ---");
                var item = (from s in context.Subledger
                            join l in context.ChartOfAccount on s.GLID equals l.Id
                            where l.Id == accId
                            select new { s.SubLdgerName, s.SubLedgerID }).ToList();
                item.ForEach(x =>
                { items.Add(x.SubLedgerID.ToString(), x.SubLdgerName); });
                return new SelectList(items, "Key", "Value");
            }
        }

        //for Tax App

        public static SelectList LoadAssesmentYear()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Year Range ---");
            int year = DateTime.Now.Year;
            if (DateTime.Now.Month >= 7)
            {
                for (int i = 0; i < 10; i++)
                {
                    items.Add((year + " - " + (year + 1)).ToString(), (year + " - " + (year + 1)).ToString());
                    year--;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    items.Add(((year - 1) + " - " + year).ToString(), ((year - 1) + " - " + year).ToString());
                    year--;
                }
            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadDuringYear()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Year Range ---");
            int year = DateTime.Now.Year - 1;
            if (DateTime.Now.Month >= 7)
            {
                for (int i = 0; i < 10; i++)
                {
                    items.Add((year + " - " + (year + 1)).ToString(), (year + " - " + (year + 1)).ToString());
                    year--;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    items.Add(((year - 1) + " - " + year).ToString(), ((year - 1) + " - " + year).ToString());
                    year--;
                }
            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadCorporation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Corporation ---");
            context.TrusteeBoard.ToList().ForEach(x => { items.Add(x.TrusteeNo.ToString(), x.TrusteeName); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadLocation()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Location ---");
            var data = FindObjectById.GetERMTransection().Select(x => x.xprloc).Distinct().ToList();
            data.ForEach(x => { items.Add(x, x); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadRebateCondition()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "---- Select Condition ----");
            context.RebateCondition.ToList().ForEach(x => { items.Add(x.ID.ToString(), x.Name); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadLTR(int? lcId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (lcId == null)
            {
                items.Add("", "--- Select LATR ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select LATR ---");
                context.LTR.Where(x => x.LCID == lcId ).ToList().ForEach(x => { items.Add(x.LTRNO.ToString(), x.LTRNO); });
                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadALLLATR()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add("", "--- Select One ---");
                context.LTR.ToList().ForEach(x => { items.Add(x.LTRID.ToString(),x.LTRID.ToString() +" - "+ x.LTRNO); });
                return new SelectList(items, "Key", "Value");   
        }
        public static SelectList LoadALLLATRID()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select One ---");
            context.LTR.ToList().ForEach(x => { items.Add(x.LTRID.ToString(), x.LTRID.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadALLLATRNO()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select One ---");
            context.LTR.ToList().ForEach(x => { items.Add(x.LTRNO.ToString(), x.LTRNO.ToString()); });
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadIncreseOrDecrese()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select One ---");
            items.Add("Increase", "Increase");
            items.Add("Decrease", "Decrease");
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadMonth()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Month ---");
            items.Add("January", "January");
            items.Add("February", "February");
            items.Add("March", "March");
            items.Add("April", "April");
            items.Add("May", "May");
            items.Add("June", "June");
            items.Add("July", "July");
            items.Add("August", "August");
            items.Add("September", "September");
            items.Add("October", "October");
            items.Add("November", "November");
            items.Add("December", "December");
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadYear()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Year ---");
            for (int i = DateTime.Now.Year; i > 2000; i--)
            {
                var year =Convert.ToString(i) ;
                items.Add(year, year);
            }
            return new SelectList(items, "Key", "Value");
        }

        public static SelectList LoadLC(int supplierId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select a LC ---");
            var lcs = (from lc in context.ImportLC where lc.SupplierId == supplierId select new { lcId = lc.ILCId, lcno = lc.ILCNO }).ToList();
            //lcs.ForEach(x => { items.Add(x.lcId.ToString(), (x.lcId.ToString() + " - " + x.lcno)); });
            lcs.ForEach(x => { items.Add(x.lcId.ToString(), x.lcno); });
            
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList SupplierFromLC()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select a Supplier ---");
            var lcs = (from lc in context.ImportLC join splr in context.Supplier on lc.SupplierId equals splr.Id select new { suplierId = lc.SupplierId, supplierName = splr.SupplierName }).Distinct().ToList();

            lcs.ForEach(x =>
            {
                items.Add(x.suplierId.ToString(), x.supplierName.ToString());
            });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList SupplierFromLCByGroup(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select a Supplier ---");
            if (id==null)
            {
                return new SelectList(items, "Key", "Value");
            }
            var lcs = (from splr in context.Supplier
                       join lc in context.ImportLC on splr.Id equals lc.SupplierId 
                       where splr.GroupID==id
                       select new { suplierId = lc.SupplierId, supplierName = splr.SupplierName }).Distinct().OrderBy(x=>x.supplierName).ToList();
            lcs.ForEach(x =>
            {
                items.Add(x.suplierId.ToString(), (x.suplierId.ToString() + " - " + x.supplierName));
            });
            return new SelectList(items, "Key", "Value");
        }
        public static List<GetLcItem> LoadLCItems(int lcId)
        {
            BEEContext context = new BEEContext();
            string sql = "exec spGetLCItemAvailableQty " + lcId + " ";
            List<GetLcItem> itm = context.Database.SqlQuery<GetLcItem>(sql).ToList();
            return itm;

        }

        public static List<GetLcItem> LoadWOItems(int lcId)
        {
            BEEContext context = new BEEContext();
            string sql = "exec getWOItemAvailableQty " + lcId + " ";
            List<GetLcItem> itm = context.Database.SqlQuery<GetLcItem>(sql).ToList();
            return itm;

        }

        //public static SelectList LoadLCItems(int? lcId)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    if (lcId==null)
        //    {
        //        items.Add("", "--- Select a LC Item ---");
        //        return new SelectList(items, "Key", "Value");
        //    }
        //    else
        //    {
        //        //items.Add("", "--- Select a LC Item ---");
        //        //var lcItems = (from lcitems in context.ImportLCLineItem
        //        //               join itm in context.ChartOfInventory
        //        //               on lcitems.ItemId equals itm.Id
        //        //               where lcitems.ILCID == lcId
        //        //               select new { itemId = itm.Id, itemName = itm.Name, packSize = itm.PacSize, itemCode = itm.ItemCode }).ToList();
        //        //lcItems.ForEach(x => { items.Add(x.itemId.ToString(), x.itemCode + "-" + x.itemName + "-" + x.packSize); });
        //        //return new SelectList(items, "Key", "Value");

        //        items.Add("", "--- Select a LC Item ---");
        //        var lcItems = (from lcitems in context.ImportLCLineItem
        //                       join itm in context.ChartOfInventory
        //                       on lcitems.ItemId equals itm.Id
        //                       where lcitems.ILCID == lcId
        //                       select new { itemId = itm.Id, itemName = itm.Name }).ToList();
        //        lcItems.ForEach(x => { items.Add(x.itemId.ToString(), x.itemName ); });
        //        return new SelectList(items, "Key", "Value");
        //    }

        //}

        public static SelectList LoadSupplierForLCBill()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Supplier ---");

            var supplier = (from s in context.Supplier
                            join sg in context.SupplierGroups on s.GroupID equals sg.SgroupId
                            where (sg.SgroupName.ToLower() == "c & f agent") || (sg.SgroupName.ToLower() == "insurance"|| sg.SgroupName.ToLower() == "others supplier")
                            select new { SId = s.Id, SName = s.SupplierName }).ToList();
            supplier.ForEach(x => { items.Add(x.SId.ToString(), x.SName); });

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadLeaveType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--Select Type--");
            var type = (from t in context.LeaveType select new { TId = t.slno, TName = t.Leavename }).ToList();
            type.ForEach(x => { items.Add(x.TId.ToString(), x.TName); });
            return new SelectList(items, "key", "Value");
        }
        //public static SelectList LoadPurchaseOrderBySupplier(int sid)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    items.Add("", "--Select Purchase--");
        //    var purchase = context.PurchaseOrder.Where(m => m.SupplierID == sid).ToList();
        //    purchase.ForEach(x => { items.Add(x.PONo.ToString(), x.PONo.ToString()); });
        //    return new SelectList(items, "key", "Value");
        //}
        public static SelectList LoadItemBtType(string type)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--Select Purchase--");
            var itms = context.ImportItemList.Where(m => m.Type == type).ToList();
            itms.ForEach(m => { items.Add(m.ItemId.ToString(), m.ItemName); });
            return new SelectList(items, "key", "Value");
        }

        public static SelectList LoadPeriodName()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Period ---");
            var tsoTarget = context.TargetSalesAndCollection.Select(x => x.PeriodName).Distinct().ToList();
            tsoTarget.ForEach(x => { items.Add(x, x); });
            return new SelectList(items, "key", "Value");
        }
        public static SelectList LoadAllEmployee()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "---- Select Employee ----");
            context.Employees.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + "-" + x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllEmployeeByCompanyId(int? companyid)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (companyid == null)
            {
                items.Add("", "--- Select Employee ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "ALL Employees");
                context.Employees.Where(x => x.CompanyId == companyid).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString() + "-" + x.Name); });
                return new SelectList(items, "Key", "Value");
            }
        }
        public static SelectList LoadMonths()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("0", "--- Select Month ---");
            items.Add("1", "January");
            items.Add("2", "February");
            items.Add("3", "March");
            items.Add("4", "April");
            items.Add("5", "May");
            items.Add("6", "June");
            items.Add("7", "July");
            items.Add("8", "August");
            items.Add("9", "September");
            items.Add("10", "October");
            items.Add("11", "November");
            items.Add("12", "December");

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadAllMonths()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Month ---");
            items.Add("1", "January");
            items.Add("2", "February");
            items.Add("3", "March");
            items.Add("4", "April");
            items.Add("5", "May");
            items.Add("6", "June");
            items.Add("7", "July");
            items.Add("8", "August");
            items.Add("9", "September");
            items.Add("10", "October");
            items.Add("11", "November");
            items.Add("12", "December");

            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadCompanyInformation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Company ---");
            context.CompanyInfo.ToList().ForEach(x => { items.Add(x.Id.ToString(), x.CompanyName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadWorkerDesignation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Designaiton ---");
            context.WorkerDesignation.ToList().ForEach(x => { items.Add(x.WDesignationNo.ToString(), x.WorkersDesignation); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadWorkerType()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Type ---");
            context.WorkerType.ToList().ForEach(x => { items.Add(x.WTNo.ToString(), x.WorksType); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSection()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Section ---");
            context.WorkSection.ToList().ForEach(x => { items.Add(x.ProdSectID.ToString(), x.ProductionSection); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList ReasonOfLeaving()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select the Reason ---");
            context.ReasonOfLeaving.ToList().ForEach(x => { items.Add(x.RoLID.ToString(), x.RoL); });
            return new SelectList(items, "Key", "Value");
        }
        // to load RM Customer RMCustomer drop down list
        public static SelectList LoadAllRMCustomer()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Customer ---");
            context.CreateCustomer.ToList().ForEach(x => { items.Add(x.clmCustomerID.ToString(), x.clmCustomerName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadLCPCNo(int lcId)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select LCPCNo ---");
            context.LCCostings.Where(m => m.ILCID == lcId).ToList().ForEach(x => { items.Add(x.LCPCNo.ToString(), x.LCPCNo.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadLCPCItems(int lcpcno)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select LCPC Items ---");
            var lcpcitems = (from lcc in context.LCCostings
                             join lccline in context.LCCostingLines on lcc.LCPCNo equals lccline.LCPCNo
                             join crtInv in context.ChartOfInventory on lccline.ItemID equals crtInv.Id
                             where (lcc.LCPCNo == lcpcno)
                             select new { itemid = crtInv.Id, itemName = crtInv.Name }).ToList();
            lcpcitems.ForEach(m => { items.Add(m.itemid.ToString(), m.itemName); });
            return new SelectList(items, "Key", "Value");
        }
        public static decimal GetItemUnitCost(int ItemId, string DocType, decimal qty, int DocNo, int StoreId, bool IsUpdate)
        {
            BEEContext context = new BEEContext();
            //var data = context.RemainingStock.Where(x => x.ItemId == ItemId && x.Qty > 0 && x.StoreId == StoreId).ToList();
           
            if (IsUpdate == true)
            {
                var isExist = context.RemainingStock.Where(x => x.DocNo == DocNo && x.ItemId == ItemId && x.StoreId == StoreId).ToList();
                if (isExist.Count > 0)
                {
                    isExist.ForEach(x =>
                    {
                        context.RemainingStock.Remove(x);
                    });
                    
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
                });
                return Math.Round((TotalCost / qty), 2);
            }
            else
            {
                return 0;
            }

        }

        public static decimal GetItemUnitCostReturn(int ItemId, string DocType, decimal qty, int DocNo, int StoreId,bool isUpdate)
        {
            BEEContext context = new BEEContext();

            if(isUpdate == true)
            {
                var isExist = context.RemainingStock.Where(x => x.ItemId == ItemId && x.DocNo == DocNo && x.DocType == "SalesReturn").ToList();
                isExist.ForEach(x => {
                    context.RemainingStock.Remove(x);
                });
                context.SaveChanges();
            }
            decimal TotalCost = 0;
            //var data = context.RemainingStock.Where(x => x.DocType == "Sales" && x.DocNo == DocNo && x.StoreId == StoreId && x.ItemId == ItemId).ToList();
            var item = context.RemainingStock.Where(x => x.ItemId == ItemId && x.DocNo == DocNo && x.StoreId == StoreId).ToList();
            var data = item
            .GroupBy(x => new { x.LCRNNo, x.DocNo,x.StoreId,x.ItemId })
            .Select(cl => new ResultLine
            {
                LCRNNo = cl.FirstOrDefault().LCRNNo,
                ItemId = cl.FirstOrDefault().ItemId,
                StoreId = cl.FirstOrDefault().StoreId,
                Qty = cl.Sum(c => c.Qty),
                UnitCost = cl.FirstOrDefault().UnitCost
            }).ToList();
            if (data.Count > 0)
            {
                var TotalQty = context.RemainingStock.Where(x=> x.DocNo == DocNo && x.StoreId == StoreId && x.ItemId == ItemId).Select(x => x.Qty).DefaultIfEmpty(0).Sum();
                decimal totalQtyU = 0;
                if (TotalQty < 0)
                {
                     totalQtyU = Math.Abs(TotalQty);
                }
                else
                {
                    totalQtyU = 0;
                }
                
                if (totalQtyU >= qty)
                {
                    decimal RemainingQty = 0;
                    
                    int count = 0;
                    data.ForEach(x =>
                    {
                        decimal getQty = 0;
                        if(x.Qty < 0)
                        {
                            getQty = Math.Abs(x.Qty);
                        }
                        else
                        {
                            getQty = 0;
                        }
                         
                        if (count == 0)
                        {
                            if (getQty >= qty)
                            {
                                if (qty > 0)
                                {
                                    RemainingStock RS = new RemainingStock();
                                    RS.ItemId = ItemId;
                                    RS.Qty = qty;
                                    RS.LCRNNo = x.LCRNNo;
                                    RS.UnitCost = x.UnitCost;
                                    RS.DocType = DocType;
                                    RS.DocNo = DocNo;
                                    RS.StoreId = x.StoreId;
                                    TotalCost += qty * x.UnitCost;
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
                                RemainingStock RS = new RemainingStock();
                                RS.ItemId = ItemId;
                                RemainingQty = qty - getQty;
                                RS.Qty = getQty;
                                RS.LCRNNo = x.LCRNNo;
                                RS.UnitCost = x.UnitCost;
                                RS.DocType = DocType;
                                RS.DocNo = DocNo;
                                RS.StoreId = x.StoreId;
                                TotalCost += qty * x.UnitCost;
                                context.RemainingStock.Add(RS);
                                context.SaveChanges();

                            }
                        }

                        if (count > 0)
                        {
                            if(RemainingQty > 0)
                            {
                                decimal qtyR = 0;
                                if (x.Qty < 0)
                                {
                                   qtyR = Math.Abs(x.Qty);
                                }
                                else
                                {
                                    qtyR = 0;
                                }
                               
                                if (RemainingQty >= qtyR)
                                {

                                    TotalCost += (qtyR * x.UnitCost);
                                    RemainingQty = RemainingQty - qtyR;
                                    RemainingStock RS = new RemainingStock();
                                    RS.ItemId = ItemId;
                                    RS.Qty = qtyR;
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

                                    TotalCost += (RemainingQty * x.UnitCost);
                                    RemainingStock RS = new RemainingStock();
                                    RS.ItemId = ItemId;
                                    RS.Qty = RemainingQty;
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

                    });
                }

            }
            else
            {
                return 0;
            }



            return Math.Round((TotalCost / qty), 2);
        }

        public static List<DueILCBBill> GetDueILCBBill(int supplierId)
        {
            BEEContext context = new BEEContext();
            string sql = "exec CalculateLcBillDueAmount " + supplierId + "";
            List<DueILCBBill> bills = context.Database.SqlQuery<DueILCBBill>(sql).ToList();
            
               return bills;
        }

        

        public static SelectList GetDueInvoice(int supplierId)
        {
            BEEContext context = new BEEContext();
             string sql = "exec CalculateLcBillDueAmount " + supplierId + "";
            List<DueILCBBill> bills = context.Database.SqlQuery<DueILCBBill>(sql).ToList();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "-- Select --");
            bills.ForEach(x =>
            {
                items.Add(x.ILCBNo.ToString(), x.ILCBNo.ToString());
            });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadSupplierByLc(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Suppplier ---");
            var supplierLc = (from ilcb in context.ILCB
                              join s in context.Supplier on ilcb.SupplierID equals s.Id
                              where ilcb.ILCID == id
                              select new { itemid = s.Id, itemName = s.SupplierName }).ToList().Distinct().ToList();

            supplierLc.ForEach(x =>
            {
                items.Add(x.itemid.ToString(), x.itemid.ToString() + "-" + x.itemName);
            });

            return new SelectList(items,"Key","Value");
        }


        public static List<DueILCBBill> GetAllDueSubLedgerByBillId(int ilcbNo)
        {
            BEEContext context = new BEEContext();
            string sql = "exec CalculateLcBillDueAmountBySubL " + ilcbNo + "";
            List<DueILCBBill> bills = context.Database.SqlQuery<DueILCBBill>(sql).ToList();
            return bills;
        }

        public static SelectList LoadPartialShipment()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Part Shipment ---");
            items.Add("Allowed", "Allowed");
            items.Add("Prohibited", "Prohibited");
            
            return new SelectList(items, "Key", "Value");
        }
        

        public static SelectList LoadTranShipment()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Tran Shipment ---");
            items.Add("Allowed", "Allowed");
            items.Add("Prohibited", "Prohibited");

            return new SelectList(items, "Key", "Value");
        }
        public static List<Item_Info> GetGSItemInfo()
        {
            string sql = "exec GSItemDetails";
            BEEContext context = new BEEContext();
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            return Items;    
        }
        public static List<ItemInfoSP> GetFreezerItemInfo()
        {
            BEEContext context = new BEEContext();
            string sql = string.Format("exec itemFWO ");
            List<ItemInfoSP> Items = context.Database.SqlQuery<ItemInfoSP>(sql).ToList();
            return Items;    
        }
        public static List<Item_Info> GetItemInfofpb()
        {
            string sql = "exec ItemDetailsFPB";
            BEEContext context = new BEEContext();
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            return Items;
        }
        public static List<Retailer_Info> GetRetailerInfo()
        {
            string sql = "exec RetailerDetails";
            BEEContext context = new BEEContext();
            List<Retailer_Info> Items = context.Database.SqlQuery<Retailer_Info>(sql).ToList();
            return Items;
        }
        public static List<Customer_Info> GetCustInfo()
        {
            string sql = "exec CustDetails";
            BEEContext context = new BEEContext();
            List<Customer_Info> Items = context.Database.SqlQuery<Customer_Info>(sql).ToList();
            return Items;
        }
        public static List<GetGBE> GetAllGBE()
        {
            string sql = "exec GetGBE";
            BEEContext context = new BEEContext();
            List<GetGBE> Items = context.Database.SqlQuery<GetGBE>(sql).ToList();
            return Items;
        }
        public static List<GetGPE> GetAllGPE()
        {
            string sql = "exec GetGPE";
            BEEContext context = new BEEContext();
            List<GetGPE> Items = context.Database.SqlQuery<GetGPE>(sql).ToList();
            return Items;
        }
        public static Item_Info GetMovingAvg(int id,int storeid)
        {
            string sql = "exec getRMCMovingAverageByID " + id+","+ storeid;
            BEEContext context = new BEEContext();
            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            var item = Items.FirstOrDefault();
            return item;
        }
        public static decimal? GSIssueRate(int id, string date, int storeid)
        {
            //DateTime Rdate = Convert.ToDateTime(date);
            //string sql = "exec RMCRateById " + id + ", '" + date + "'";
            string sql = "exec spGetGSIssueRateById " + id + ", '" + date + "'," + id + " ";
            BEEContext context = new BEEContext();

            //try
            //{
            List<New_Item_Info> Items = context.Database.SqlQuery<New_Item_Info>(sql).ToList();
            if (Items.Count > 0)
            {
                var item = Items.FirstOrDefault();
                return Convert.ToDecimal(item.AverageRate);
            }
            else
            {
                return 0;
            }
          //  }
          //catch(Exception ex)
          //  {
          //      return 0;
          //  }

        }

        public static decimal? GetNewRMCRateById(int id, string lastDate)
        {
            //DateTime Rdate = Convert.ToDateTime(date);
            string sql = "exec GetNewRMCRateById " + id + ",'" + lastDate + "'";
            BEEContext context = new BEEContext();

            List<Item_Info> Items = context.Database.SqlQuery<Item_Info>(sql).ToList();
            if (Items.Count > 0)
            {
                var item = Items.FirstOrDefault();
                return Convert.ToDecimal(item.clmStandardCost);
            }
            else
            {
                return 0;
            }

        }

        //public static SelectList LoadDirectPurchaseNo(int? suplierId)
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    items.Add("", "--- Select ---");
        //    context.DirectPurchase.Where(x => x.SupplierId == suplierId).ToList().ForEach(x =>
        //    {
        //        decimal dueAmount = DPDueAmountByDpNo(x.DPNo);
        //        if (dueAmount > 0)
        //        {
        //            items.Add(x.DPNo.ToString(), x.DPNo.ToString());
        //        }
        //    });
        //    return new SelectList(items, "Key", "Value");
        //}

        public static decimal OBDueAmountBySupplier(int supplierid)
        {
            BEEContext context = new BEEContext();
            string sql = "exec GetOBDueAmount " + supplierid + "";
            SupplierDueAmount sda = new SupplierDueAmount();
            sda = context.Database.SqlQuery<SupplierDueAmount>(sql).FirstOrDefault();

            return sda.DueAmount;
        }
        public static decimal DPDueAmountByDpNo(int dpNo)
        {
            BEEContext context = new BEEContext();
            string sql = "exec GetDPDueAmount " + dpNo + "";
            SupplierDueAmount sda = new SupplierDueAmount();
            sda = context.Database.SqlQuery<SupplierDueAmount>(sql).FirstOrDefault();

            return sda.DueAmount;
        }
        public static SelectList LoadAllModules()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> Modules = new Dictionary<string, string>();
            Modules.Add("", "-- Select Module --");
            context.Modules.ToList().ForEach(x => { Modules.Add(x.ModuleID.ToString(), x.Name); });
            return new SelectList(Modules, "Key", "Value");
        }
        public static SelectList LoadScreenByModule(int? id)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> Screen = new Dictionary<string, string>();
            Screen.Add("", "-- Select Screen --");
            if(id != null)
            {
                context.Screens.Where(x => x.ModuleID == id).ToList().ForEach(y => { Screen.Add(y.ScreenID.ToString(), y.Name); });

            }
            return new SelectList(Screen, "Key", "Value");
        }
        public static SelectList LoadActiveEmployees()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Employee ---");

            var employee = (from e in context.Employees
                            where (e.Status == 1) 
                            select new { EID = e.Id, EName = e.Name }).ToList();
            //employee.ForEach(x => { items.Add(x.EID.ToString(), x.EName); });
            employee.ForEach(x => { items.Add(x.EID.ToString(), x.EID.ToString() + " - " + x.EName); });

            return new SelectList(items, "Key", "Value");
        }
    }

}

public class ResultLine
{
    public int LCRNNo { get; set; }
    public decimal UnitCost { get; set; }
    public int StoreId { get; set; }
    public decimal Qty { get; set; }
    public int ItemId { get; set; }
}
public class Item_Info
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Int32 ParentId { get; set; }
    public string Type { get; set; }
    public Int32 UoMID { get; set; }
    public string UOM { get; set; }
    public string RootAcType { get; set; }
    public decimal RetailPrice { get; set; }
    public decimal? Litre { get; set; }
    public int? clmCartonCapacity { get; set; }
    public decimal? clmStandardCost { get; set; }
    public string PacSize { get; set; }

}

public class New_Item_Info
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Int32 ParentId { get; set; }
    public string Type { get; set; }
    public Int32 UoMID { get; set; }
    public string UOM { get; set; }
    public string RootAcType { get; set; }
    public decimal AverageRate { get; set; }

}


public class Customer_Info
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public double Credit { get; set; }
    public double OB { get; set; }
    public DateTime AsDate { get; set; }
    public string ConPerson { get; set; }
    public string MobileNo { get; set; }
    public string TelephoneNo { get; set; }
    public string EmailId { get; set; }
    public string Billto { get; set; }
    public string Shipto { get; set; }
    public int DepotId { get; set; }
    public int ZoneId { get; set; }
    public string AreaName { get; set; }
}
public class SupplierDueAmount
{
    public decimal DueAmount { get; set; }
}

public class GetGBE
{
    public int GBENo { get; set; }
     public DateTime GBEDate { get; set; }
    public int DepotId { get; set; }
    public int SupplierId { get; set; }
    public string RefNo { get; set; }
    public decimal GBETotal { get; set; }
    public decimal VAT { get; set; }
    public decimal VATAmount { get; set; }
    public decimal AIT { get; set; }
    public decimal AITAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal PaidAmount { get; set; }
}

public class Retailer_Info
{
    public int Id { get; set; }
    public string RetailerName { get; set; }
    public int CustomerID { get; set; }
    public string ConPerson { get; set; }
    public string MobileNo { get; set; }
    public string TelephoneNo { get; set; }
    public string EmailId { get; set; }
    public string Billto { get; set; }
    public string Shipto { get; set; }
    public string DealerYesNo { get; set; }
    public decimal OB { get; set; }
}

public class GetGPE
{
    public int BillNo { set; get; }
    public DateTime PDate { set; get; }
    public int SupplierId { set; get; }
    public decimal BillTotalValue { set; get; }
    public string PurRef { set; get; }
    public string PurDescription { set; get; }
    public decimal DiscountAmt { set; get; }
    public decimal TotalDiscount { set; get; }
    public decimal VatAmount { set; get; }
    public decimal AitAmount { set; get; }
    public decimal TotalPayable { set; get; }
    public decimal PaidAmount { set; get; }
}
public class RMCustomer
{
    public int clmCustomerID { get; set; }
    public string clmCustomerName { get; set; }
}
public class RMCItemQty
{
    public decimal BalanceQty { get; set; }
}
public class GSCItemQty
{
    public decimal BalanceQty { get; set; }
    public decimal AvgRate { get; set; }
}
public class ISPItemQty
{
    public decimal BalanceQty { get; set; }
    public decimal AverageRate { get; set; }
}
public class GetLcItem
{
    public string GNM { get; set; }
    public int GID { get; set; }
    public string NM { get; set; }
    public int IID { get; set; }
    public decimal QT { get; set; }
    public decimal RQT { get; set; }
    public decimal HQT { get; set; }
    public string UM { get; set; }
    public int UMId { get; set; }
}

public class SubLedger_Info
{
    public int SubLedgerID { get; set; }
    public string SubLedgerName { get; set; }
    public string SubLedgerType { get; set; }
}

public class ItemList
{
    public int id { get; set; }
    public string Name { get; set; }
}