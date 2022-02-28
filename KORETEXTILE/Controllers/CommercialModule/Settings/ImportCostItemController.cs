using BEEERP.Models.CommercialModule.Settings;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using System.Data.Entity;

namespace BEEERP.Controllers.CommercialModule.Settings
{
    [ShowNotification]
    public class ImportCostItemController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ImportCostItem
        public ActionResult ImportCostItemLoad()
        {
            var savedImportCostItems = context.ImportCostItems.Count();
            if (savedImportCostItems == 0)
            {
                ViewBag.numberofImportCostItems = 1;
            }
            else
            {
                ViewBag.numberofImportCostItems = context.ImportCostItems.ToList().Max(m => m.SlnNo) + 1;
            }
            var ImportCostItemList= context.ImportCostItems.ToList();
            ImportCostItemList.ForEach(m => { m.AccountHeadName = context.ChartOfAccount.FirstOrDefault(c=>c.Id==m.CostGroupId).Name; });
            ViewBag.ImportCostItemList = ImportCostItemList;
            ViewBag.AccountHead = LoadComboBox.LoadAccountHeads();

            return View();
        }
        public ActionResult SaveImportCostItem(ImportCostItem importCostItem)
        {
            var savedImportCostItems = context.ImportCostItems.Count();
            if (savedImportCostItems == 0)
            {
                importCostItem.SlnNo = 1;
            }
            else
            {
                importCostItem.SlnNo = context.ImportCostItems.ToList().Max(m => m.SlnNo) + 1;
            }
            try
            {
                context.ImportCostItems.Add(importCostItem);
                context.SaveChanges();
                return Json(new { Message = 1, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult GetImpotCostItemById(int SLno)
        {
            var ICI = context.ImportCostItems.FirstOrDefault(m => m.SlnNo == SLno);
            return Json(new { ImportCostItem = ICI, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateImportCostItem(ImportCostItem importCostItem)
        {
            
            try
            {
                var icitem = context.ImportCostItems.AsNoTracking().FirstOrDefault(m => m.SlnNo == importCostItem.SlnNo);
                if(icitem==null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<ImportCostItem>(importCostItem).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return Json(new { Message = 1, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
        }
    }
}