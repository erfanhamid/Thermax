using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class CreateSubLedgerController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: CreateSubLedger
        public ActionResult SubLedgerView()
        {
            ViewBag.subledgerid = generate.GenerateSubLedgerID(context);
            ViewBag.DepotId = LoadComboBox.LoadAllDepot();
            ViewBag.SubLedgerType = LoadComboBox.LoadSubLedgerType();


            var items = new List<SubLedgerVModel>();
            context.CreateSubLedger.ToList().ForEach(x =>
            {
                items.Add(new SubLedgerVModel
                {
                    SubLedgerID = x.SubLedgerID,
                    SubLedgerName = x.SubLedgerName,
                    
                    //SubLedgerType = GetDepotName(x.DepotID),
                    SubLedgerType = x.SubLedgerType,
                    //DepotID = int.Parse(GetDepotName(x.DepotID)),
                    DepotName = GetDepotName(x.DepotID),
                    ReferenceNo = x.ReferenceNo,
                    SLDescription = x.SLDescription,
                    //FGStoreID = int.Parse(GetFGStoreName(x.FGStoreID))
                    //FGStoreName = GetFGStoreName(x.FGStoreID)
                    //FGStore = (from s in context.Store
                    //          join b in context.BranchInformation on s.DepotID equals b.Slno
                    //          where ((s.Type == "FG" || s.Type == "FA") && b.Slno==x.Id)
                    //          select s.Name;)
                    //FGStore=context.Store.FirstOrDefault(m=> Convert.ToInt32(m.FGStoreID)==x.Id)
                });
            });

            ViewBag.SubLedger = items;
            return View();
        }
        public string GetDepotName(int id)
        {
            var item = context.BranchInformation.FirstOrDefault(x => x.Slno == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.BrnachName;
            }
        }
        public ActionResult SaveNewSubLedger(CreateSubLedger subledger)
        {

            var ifNameExists = context.CreateSubLedger.FirstOrDefault(x => x.SubLedgerName.ToLower() == subledger.SubLedgerName.ToLower().Trim());

            if (ifNameExists == null)
            {
                context.CreateSubLedger.Add(subledger);
                context.SaveChanges();
                return Json(new { subledger.SubLedgerID, Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ViewBag.messege = "Sub Ledger Name already exists";
                //ViewBag.StoreId = generate.GenerateStoreId(context);
                //ViewBag.Store = context.Store.ToList();
                //ViewBag.Depot = LoadComboBox.LoadBranchInfo();
                //ViewBag.StoreType = LoadComboBox.LoadStoreType();
                return Json(new {  Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSubLedgerByid(int id)
        {
            var subledg = context.CreateSubLedger.FirstOrDefault(x => x.SubLedgerID == id);
            if (subledg == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dpt = context.BranchInformation.FirstOrDefault(x => x.Slno == subledg.DepotID);
                subledg.DepotName = dpt.BrnachName;
                var DID = Convert.ToString(subledg.DepotID);


                return Json(new { item = subledg }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateSubLedger(CreateSubLedger subledger)
        {
            var ifNameExists = context.CreateSubLedger.FirstOrDefault(x => x.SubLedgerID != subledger.SubLedgerID && x.SubLedgerName.ToLower() == subledger.SubLedgerName.ToLower());
            //var ifNameExists = context.CreateSubLedger.FirstOrDefault(x => x.SubLedgerName.ToLower() == subledger.SubLedgerName.ToLower().Trim());

            if (ifNameExists == null)
            {
                var str = context.CreateSubLedger.ToList().Find(x => x.SubLedgerID == subledger.SubLedgerID);

                context.CreateSubLedger.Remove(str);

                context.CreateSubLedger.Add(subledger);
                context.SaveChanges();
                return Json(subledger.SubLedgerID, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteSubLedgerByid(int id)
        {
            var ifTransactionExists = context.PaymentVoucherOneSubLdgrLine.FirstOrDefault(x => x.SubLedgerID == id);

            if (ifTransactionExists == null)
            {
                var str = context.CreateSubLedger.ToList().Find(x => x.SubLedgerID == id);

                context.CreateSubLedger.Remove(str);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public class SubLedgerVModel
        {
            public int SubLedgerID { get; set; }
            public string SubLedgerName { get; set; }
            public string SubLedgerType { get; set; }
            public int DepotID { get; set; }
            public string ReferenceNo { get; set; }
            public string SLDescription { get; set; }
            public string DepotName { get; set; }
        }
    }
}