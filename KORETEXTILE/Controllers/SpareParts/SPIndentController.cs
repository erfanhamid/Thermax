using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SpareParts;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class SPIndentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SPIndent
        public ActionResult SPIndent()
        {
            ViewBag.TypeID = LoadComboBox.LoadAllTypeId();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.GroupId = LoadComboBox.LoadAllSPGroup();
            ViewBag.ItemId = LoadComboBox.LoadAllItemByID(null);

            return View();
        }

        public ActionResult GetItemByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllItemByID(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSpIndent(SPIndent indent, List<SPIndentLineItem> indentLineItems)
        {
            string spiNo = "";
            string yearLastTwoPart = indent.IdtDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.SPIndent.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.SPIndent.ToList().LastOrDefault(x => x.IndentNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);

                if (lastInvoice == null)
                {
                    spiNo = yearLastTwoPart + "0001";
                }
                else
                {
                    spiNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.IndentNo.ToString().Substring(2, 4)) + 1).ToString().PadLeft(4, '0');
                }
            }
            else
            {
                spiNo = yearLastTwoPart + "0001";
            }

            indent.IndentNo = Convert.ToInt32(spiNo);
            indent.IndentNo = Convert.ToInt32(spiNo);

            var Id = 0;

            Id = indent.IndentNo;
            context.SPIndent.Add(indent);
            indentLineItems.ForEach(x =>
            {
                x.IndentNo = indent.IndentNo;
            });
            context.SPIndentLineItem.AddRange(indentLineItems);
            //InventoryTransactionBridge.InsertFromIssueSpareParts(ref context, isp, ispLineItems);
            context.SaveChanges();
            return Json(new { Id = indent.IndentNo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUomByItemId(int id, int companyId, int typeId, string IdtDate)
        {

            var item = FindObjectById.GetChartOfInventoryById(id);
            DateTime idtdate = Convert.ToDateTime(IdtDate);
            var cost = LoadComboBox.GetISPRate(id, idtdate.ToString("yyyy-MM-dd"));
            string sql = ("exec spGetSPIBalQtyRate " + id + ", '" + DateTimeFormat.ConvertToDDMMYYYY(idtdate) + "'," + typeId + "," + companyId + "");
            List<ISPItemQty> Data = context.Database.SqlQuery<ISPItemQty>(sql).ToList();
            var RemQty = Data[0];
            var qty = (decimal)RemQty.BalanceQty;
            var avgRate = (decimal)RemQty.AverageRate;

            var uomId = context.ChartOfInventory.FirstOrDefault(x => x.Id == id).UoMID;
            var uomName = context.UOM.FirstOrDefault(x => x.Id == uomId).Name;

            return Json(new { uname = uomName, balQty = qty, Rate = avgRate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSPIndentById(int id)
        {

            var indentDetails = context.SPIndent.FirstOrDefault(x => x.IndentNo == id);
            if (indentDetails != null)
            {
                var indentLineItems = context.SPIndentLineItem.Where(s => s.IndentNo == indentDetails.IndentNo).ToList();
                indentLineItems.ForEach(s =>
                {


                    var item = context.ChartOfInventory.Where(x => x.Id == s.SPItemID).SingleOrDefault();
                    s.SPItemID = item.Id;
                    s.ItemName = item.Name;


                    var group = context.ChartOfInventory.Where(x => x.Id == item.parentId).SingleOrDefault();
                    s.GroupId = group.Id;
                    s.GroupName = group.Name;
                });
                return Json(new { indentDetails, indentLineItems }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateSPIndent(SPIndent indent, List<SPIndentLineItem> indentLineItems)
        {
            var indentExist = context.SPIndent.Where(x => x.IndentNo == indent.IndentNo).SingleOrDefault();
            var indentLineExist = context.SPIndentLineItem.Where(x => x.IndentNo == indent.IndentNo).ToList();
            context.SPIndent.Remove(indentExist);
            context.SPIndentLineItem.RemoveRange(indentLineExist);
            context.SPIndent.Add(indent);
            indentLineItems.ForEach(x =>
            {
                x.IndentNo = indent.IndentNo;
            });
            context.SPIndentLineItem.AddRange(indentLineItems);
            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSPIndentByid(int id)
        {
            var indentExist = context.SPIndent.Where(x => x.IndentNo == id).SingleOrDefault();
            var indentLineExist = context.SPIndentLineItem.Where(x => x.IndentNo == id).ToList();
            context.SPIndent.Remove(indentExist);
            context.SPIndentLineItem.RemoveRange(indentLineExist);
            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}