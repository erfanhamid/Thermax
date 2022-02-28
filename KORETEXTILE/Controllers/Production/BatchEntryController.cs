using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Production
{ 
    [ShowNotification]
    public class BatchEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: BatchEntry
        public ActionResult BatchEntry()
        {
            var data = context.Batch.ToList();

            int mid =0;

            if (data.Count > 0)
            {
                var id = context.Batch.Max(x => x.ID);

                mid = id + 1;
            }
            else
            {
                mid = 1;
            }


            
           ViewBag.ID = mid;
            data.ForEach(x =>
            {
                x.Date = x.BatchDate.ToString("dd-MM-yyyy");

            });
            ViewBag.Data = data;
            return View();
        }
        public ActionResult GetBatch(int id)
        {
            var batch = context.Batch.FirstOrDefault(x => x.ID == id);
            return Json(batch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUpdateBatch(Batch batch)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Create Batch").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (batch.BatchDate > IsExpired.SELDate)
            {
                if (batch != null)
                {
                    var isexist = context.Batch.FirstOrDefault(x => x.ID == batch.ID);
                    if (isexist == null)
                    {
                        //var id = context.Batch.Max(x => x.ID);
                        //batch.ID = id + 1;
                        batch.Status = "Active";
                        context.Batch.Add(batch);
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        context.Batch.Remove(isexist);
                        batch.Status = "Active";
                        context.Batch.Add(batch);
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}