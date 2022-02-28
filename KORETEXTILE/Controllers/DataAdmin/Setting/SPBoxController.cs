using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.DataAdmin.SPSettings;

namespace BEEERP.Controllers.DataAdmin.Setting
{
    [ShowNotification]
    public class SPBoxController : Controller
    {
        BEEContext Context = new BEEContext();
        // GET: SPBox
        public ActionResult SPBox()
        {
            ViewBag.StoreID = LoadComboBox.LoadAllStore();
            ViewBag.BoxNo = Context.SPBox.ToList();

            var box = Context.SPBox.ToList();
            var maxId = 0;
            if (box.Count > 0)
            {
                var mid = Context.SPBox.Max(x => x.BoxID);
                maxId = mid + 1;
            }
            else
            {
                maxId = 1;
            }
            ViewBag.Id = maxId;

            var items = new List<BoxVModel>();
            Context.SPBox.ToList().ForEach(x =>
            {
                items.Add(new BoxVModel
                {
                    BoxID = x.BoxID,
                    BoxNo = x.BoxNo,
                    StoreName = GetStoreName(x.StoreID)

                });

            });

            ViewBag.BoxNo = items;

            return View();
        }

        public string GetStoreName(int id)
        {
            var item = Context.Store.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }
        public ActionResult SaveBoxInfo(SPBox box)
        {
            var boxCheck = Context.SPBox.FirstOrDefault(x => x.BoxNo == box.BoxNo && x.StoreID == box.StoreID);
            if(boxCheck == null)
            {
                Context.SPBox.Add(box);
                Context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBoxById(int id)
        {

            var boxDetails = Context.SPBox.ToList().FirstOrDefault(x => x.BoxID == id);

            if (boxDetails == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { item = boxDetails }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateBoxInfo(SPBox box)
        {
            var boxExit = Context.SPBox.FirstOrDefault(x => x.BoxID == box.BoxID);

            if (boxExit != null)
            {
                Context.SPBox.Remove(boxExit);
                Context.SPBox.Add(box);
                Context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteBoxByid(int id)
        {
            var boxExit = Context.SPBox.FirstOrDefault(x => x.BoxID == id);

            if (boxExit != null)
            {
                Context.SPBox.Remove(boxExit);
                Context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

    }

    public class BoxVModel
    {
        public int BoxID { get; set; }
        public string BoxNo { get; set; }
        public string StoreName { get; set; }
    }
}