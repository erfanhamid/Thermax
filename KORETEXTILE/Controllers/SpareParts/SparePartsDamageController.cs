using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.SpareParts;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class SparePartsDamageController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SparePartsDamage
        public ActionResult SparePartsDamage()
        {
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.GroupId = LoadComboBox.LoadAllSPGroup();
            ViewBag.ItemId = LoadComboBox.LoadAllItemByID(null);
            ViewBag.BoxID = LoadComboBox.LoadAllSPBox(null);
            ViewBag.TypeId = LoadComboBox.LoadAllTypeId();
            ViewBag.MachineID = LoadComboBox.LoadAllMachineID();

            return View();
        }

        public ActionResult GetItemByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllItemByID(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBoxByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllSPBox(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSPDamage(SparePartsDamage spd, List<SparePartsDamageLineItem> spdLineItems)
        {

            string spdNo = "";
            string yearLastTwoPart = spd.SPDDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.SparePartsDamage.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.SparePartsDamage.ToList().LastOrDefault(x => x.SPDNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);

                if (lastInvoice == null)
                {
                    spdNo = yearLastTwoPart + "0001";
                }
                else
                {
                    spdNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SPDNo.ToString().Substring(2, 4)) + 1).ToString().PadLeft(4, '0');
                }
            }
            else
            {
                spdNo = yearLastTwoPart + "0001";
            }

            spd.SPDNo = Convert.ToInt32(spdNo);

            //ViewBag.Id = Convert.ToInt32(gsmiNo);
            spd.SPDNo = Convert.ToInt32(spdNo);

            var Id = 0;

            Id = spd.SPDNo;

            context.SparePartsDamage.Add(spd);
            spdLineItems.ForEach(x =>
            {
                x.SPDNo = spd.SPDNo;
            });
            context.SparePartsDamageLineItem.AddRange(spdLineItems);

            SPInventoryTransactionBridge.InsertFromSparePartsDamage(ref context, spd, spdLineItems);

            context.SaveChanges();
            return Json(new { Id = spd.SPDNo }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUomByItemId(int id, int storeId, int boxId, int companyId, int typeId, DateTime issueDate)
        {

            var item = FindObjectById.GetChartOfInventoryById(id);
            DateTime ispdate = Convert.ToDateTime(issueDate);
            var cost = LoadComboBox.GetISPRate(id, ispdate.ToString("yyyy-MM-dd"));
            string sql = ("exec spGetISPBalQtyRate " + id + ", '" + DateTimeFormat.ConvertToDDMMYYYY(ispdate) + "'," + storeId + "," + typeId + "," + companyId + ", " + boxId + "");
            List<ISPItemQty> Data = context.Database.SqlQuery<ISPItemQty>(sql).ToList();
            var RemQty = Data[0];
            var qty = (decimal)RemQty.BalanceQty;
            var avgRate = (decimal)RemQty.AverageRate;
            //return Json(new { RemQty = qty, Item = item, Rate = cost }, JsonRequestBehavior.AllowGet);


            var uomId = context.ChartOfInventory.FirstOrDefault(x => x.Id == id).UoMID;
            var uomName = context.UOM.FirstOrDefault(x => x.Id == uomId).Name;

            return Json(new { uname = uomName, balQty = qty, Rate = avgRate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSPDamageById(int id)
        {

            var spdDetails = context.SparePartsDamage.FirstOrDefault(x => x.SPDNo == id);
            if (spdDetails != null)
            {
                var spdLineItems = context.SparePartsDamageLineItem.Where(s => s.SPDNo == spdDetails.SPDNo).ToList();
                spdLineItems.ForEach(s =>
                {

                    var store = context.Store.Where(x => x.Id == s.StoreId).SingleOrDefault();
                    s.StoreId = store.Id;
                    s.StoreName = store.Name;

                    var box = context.SPBox.Where(x => x.BoxID == s.BoxID).SingleOrDefault();
                    s.BoxID = box.BoxID;
                    s.BoxName = box.BoxNo;

                    var item = context.ChartOfInventory.Where(x => x.Id == s.SPItemID).SingleOrDefault();
                    s.SPItemID = item.Id;
                    s.ItemTypeName = item.Name;


                    var group = context.ChartOfInventory.Where(x => x.Id == item.parentId).SingleOrDefault();
                    s.GroupId = group.Id;
                    s.GroupName = group.Name;
                });
                return Json(new { spdDetails, spdLineItems }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateSPDamage(SparePartsDamage spd, List<SparePartsDamageLineItem> spdLineItems)
        {
            var spdExist = context.SparePartsDamage.Where(x => x.SPDNo == spd.SPDNo).SingleOrDefault();
            var spdLineExist = context.SparePartsDamageLineItem.Where(x => x.SPDNo == spd.SPDNo).ToList();
            context.SparePartsDamage.Remove(spdExist);
            context.SparePartsDamageLineItem.RemoveRange(spdLineExist);
            context.SparePartsDamage.Add(spd);
            spdLineItems.ForEach(x =>
            {
                x.SPDNo = spd.SPDNo;
            });
            context.SparePartsDamageLineItem.AddRange(spdLineItems);

            SPInventoryTransactionBridge.UpdateFromSparePartsDamage(ref context, spd, spdLineItems);

            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSPDamageByid(int id)
        {
            var spdExist = context.SparePartsDamage.Where(x => x.SPDNo == id).SingleOrDefault();
            var spdLineExist = context.SparePartsDamageLineItem.Where(x => x.SPDNo == id).ToList();
            context.SparePartsDamage.Remove(spdExist);
            context.SparePartsDamageLineItem.RemoveRange(spdLineExist);

            SPInventoryTransactionBridge.DeleteFromSparePartsDamage(ref context, id);

            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}