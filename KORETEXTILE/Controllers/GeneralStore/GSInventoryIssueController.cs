using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.GeneralStore;

namespace BEEERP.Controllers.GeneralStore
{
    [ShowNotification]
    public class GSInventoryIssueController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GSInventoryIssue
        public ActionResult GSInventoryIssueView()
        {
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.DepartmentID = LoadComboBox.LoadAllDepartment();
            ViewBag.Group = LoadComboBox.LoadGroupGS();
            ViewBag.Item = LoadComboBox.LoadAllItemByID(null);
            ViewBag.Uom = LoadComboBox.LoadAllUomByID(null);

            return View();
        }
        public ActionResult SaveGSInventory(GSInventoryIssue gii, List<GSInventoryIssueLineItem> gsiiLineItems)
        {


            string GMIusseNo = "";
            string yearLastTwoPart = gii.GSIssueDate.Year.ToString().Substring(2, 2).ToString();
            if (context.GSInventoryIssue.Count() > 0)
            {
                var lastGSI = context.GSInventoryIssue.ToList().LastOrDefault(x => x.GSIIssueNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);

                
                if (lastGSI == null)
                {
                    GMIusseNo = yearLastTwoPart + "00001";
                }
                else
                {
                    GMIusseNo = yearLastTwoPart + (Convert.ToInt32(lastGSI.GSIIssueNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                GMIusseNo = yearLastTwoPart + "00001";
            }
            gii.GSIIssueNo = Convert.ToInt32(GMIusseNo);

            //ViewBag.Id = Convert.ToInt32(gsmiNo);
            gii.GSIIssueNo = Convert.ToInt32(GMIusseNo);

            var Id = 0;

            Id = gii.GSIIssueNo;

            context.GSInventoryIssue.Add(gii);

            gsiiLineItems.ForEach(x =>
            {
                x.GSIIssueNo = gii.GSIIssueNo;
            });
            context.GSInventoryIssueLineItem.AddRange(gsiiLineItems);

            InventoryTransactionBridge.InsertUpdateFromGSInventoryIssue(ref context, gii, gsiiLineItems);


            context.SaveChanges();
            return Json(new { Id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateGSInventory(GSInventoryIssue gii, List<GSInventoryIssueLineItem> gsiiLineItems)
        {
            var gs = context.GSInventoryIssue.Where(s => s.GSIIssueNo == gii.GSIIssueNo).SingleOrDefault();
            var gsline = context.GSInventoryIssueLineItem.Where(s => s.GSIIssueNo == gii.GSIIssueNo).ToList();
            context.GSInventoryIssue.Remove(gs);
            context.GSInventoryIssueLineItem.RemoveRange(gsline);
            context.GSInventoryIssue.Add(gii);

            gsiiLineItems.ForEach(x =>
            {
                x.GSIIssueNo = gii.GSIIssueNo;
            });
            context.GSInventoryIssueLineItem.AddRange(gsiiLineItems);

            InventoryTransactionBridge.InsertUpdateFromGSInventoryIssue(ref context, gii, gsiiLineItems);

            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllItemByID(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemRemainQty(int itemId, int storeId, int companyId, DateTime issueDate)
        {
            DateTime GSissueDate = DateTime.Now;
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            DateTime rmcdate = Convert.ToDateTime(GSissueDate);
            var cost = LoadComboBox.GSIssueRate(itemId, rmcdate.ToString("yyyy-MM-dd"), storeId);

            // for remaining item qty
            string sql = ("exec spGetGSItemQty " + itemId + ", '" + DateTimeFormat.ConvertToDDMMYYYY(rmcdate) + "'," + storeId + "," + companyId + "");
            List<GSCItemQty> Data = context.Database.SqlQuery<GSCItemQty>(sql).ToList();
            var RemQty = Data[0];
            var qty = (decimal)RemQty.BalanceQty;
            var rt = (decimal)RemQty.AvgRate;


            // Unit of measurement 
            var umid = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId).UoMID;
            var um = context.UOM.FirstOrDefault(x => x.Id == umid).Name;



            return Json(new { RemQty = qty, Rate = rt, uom = um }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGSInventoryById(int id)
        {

            var gsDetails = context.GSInventoryIssue.FirstOrDefault(x => x.GSIIssueNo == id);

            if (gsDetails != null)
            {
                var gsiiLineItems = context.GSInventoryIssueLineItem.Where(s => s.GSIIssueNo == gsDetails.GSIIssueNo).ToList();
                gsiiLineItems.ForEach(s =>
                {
                    s.GroupName = context.ChartOfInventory.Where(x => x.Id == s.GroupID).SingleOrDefault().Name;
                    s.ItemName = context.ChartOfInventory.Where(x => x.Id == s.ItemID).SingleOrDefault().Name;
                });
                return Json(new { gsDetails, gsiiLineItems }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteGSInventoryByid(int id)
        {
            var gs = context.GSInventoryIssue.Where(s => s.GSIIssueNo == id).SingleOrDefault();
            var gsline = context.GSInventoryIssueLineItem.Where(s => s.GSIIssueNo == id).ToList();
            context.GSInventoryIssue.Remove(gs);
            context.GSInventoryIssueLineItem.RemoveRange(gsline);

            InventoryTransactionBridge.DeleteFromGSInventoryIssue(ref context, id); 

            context.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);

        }

    }
}