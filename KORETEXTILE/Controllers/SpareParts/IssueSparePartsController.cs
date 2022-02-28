using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SpareParts;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class IssueSparePartsController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: IssueSpareParts
        public ActionResult IssueSpareParts()
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

        public ActionResult SaveIssueSpareParts(IssueSpareParts isp, List<IssueSparePartsLineItem> ispLineItems)
        {
            //var parts = context.IssueSpareParts.ToList();
            //var maxId = 1;
            //if (parts.Count >= 1)
            //{
            //    var spare = context.IssueSpareParts.Max(x => x.ISPNo);
            //    maxId = spare + 1;
            //}
            //else
            //{
            //    maxId = 1;
            //}
            //ViewBag.Id = maxId;
            //isp.ISPNo = maxId;

            string ispNo = "";
            string yearLastTwoPart = isp.ISPDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.IssueSpareParts.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.IssueSpareParts.ToList().LastOrDefault(x => x.ISPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);

                if (lastInvoice == null)
                {
                    ispNo = yearLastTwoPart + "0001";
                }
                else
                {
                    ispNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.ISPNo.ToString().Substring(2, 4)) + 1).ToString().PadLeft(4, '0');
                }
            }
            else
            {
                ispNo = yearLastTwoPart + "0001";
            }

            isp.ISPNo = Convert.ToInt32(ispNo);

            //ViewBag.Id = Convert.ToInt32(gsmiNo);
            isp.ISPNo = Convert.ToInt32(ispNo);

            var Id = 0;

            Id = isp.ISPNo;

            context.IssueSpareParts.Add(isp);
            ispLineItems.ForEach(x =>
            {
                x.ISPNo = isp.ISPNo;
            });
            context.IssueSparePartsLineItem.AddRange(ispLineItems);
            SPInventoryTransactionBridge.InsertFromIssueSpareParts(ref context, isp, ispLineItems);
            context.SaveChanges();
            return Json(new { Id = isp.ISPNo }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUomByItemId(int id,int storeId,int boxId,int companyId, int typeId,string issueDate)
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
           // return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIssueSPById(int id)
        {

            var ispDetails = context.IssueSpareParts.FirstOrDefault(x => x.ISPNo == id);
            if (ispDetails != null)
            {
                var ispLineItems = context.IssueSparePartsLineItem.Where(s => s.ISPNo == ispDetails.ISPNo).ToList();
                ispLineItems.ForEach(s =>
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
                return Json(new { ispDetails, ispLineItems }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateIssueSpareParts(IssueSpareParts isp, List<IssueSparePartsLineItem> ispLineItems)
        {
            var ispExist = context.IssueSpareParts.Where(x => x.ISPNo == isp.ISPNo).SingleOrDefault();
            var ispLineExist = context.IssueSparePartsLineItem.Where(x => x.ISPNo == isp.ISPNo).ToList();
            context.IssueSpareParts.Remove(ispExist);
            context.IssueSparePartsLineItem.RemoveRange(ispLineExist);
            context.IssueSpareParts.Add(isp);
            ispLineItems.ForEach(x =>
            {
                x.ISPNo = isp.ISPNo;
            });
            context.IssueSparePartsLineItem.AddRange(ispLineItems);
            SPInventoryTransactionBridge.UpdateFromIssueSpareParts(ref context, isp, ispLineItems);
            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteIssueSparePartsByid(int id)
        {
            var ispExist = context.IssueSpareParts.Where(x => x.ISPNo == id).SingleOrDefault();
            var ispLineExist = context.IssueSparePartsLineItem.Where(x => x.ISPNo == id).ToList();
            context.IssueSpareParts.Remove(ispExist);
            context.IssueSparePartsLineItem.RemoveRange(ispLineExist);
            SPInventoryTransactionBridge.DeleteFromIssueSpareParts(ref context, id);
            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}