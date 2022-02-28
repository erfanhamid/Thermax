using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.SalesModule;

namespace BEEERP.Controllers.Data_Admin
{
    [ShowNotification]
    //[Authorize(Roles = "DataAdmin,DataOperator,DataViewer,DataApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class StoreController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: Store
        public ActionResult Store()
        {
            ViewBag.StoreId = generate.GenerateStoreId(context);
            //ViewBag.Store = context.Store.ToList();
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.StoreType = LoadComboBox.LoadStoreType();
            //ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = LoadComboBox.LoadAllDepot();

            ViewBag.FGStore = LoadComboBox.GetFGStoreByName(null);

            var items = new List<StoreVModel>();
            context.Store.ToList().ForEach(x =>
            {
                items.Add(new StoreVModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    //DepotID = int.Parse(GetDepotName(x.DepotID)),
                    DepotName = GetDepotName(x.DepotID),
                    Type = x.Type,
                    //FGStoreID = int.Parse(GetFGStoreName(x.FGStoreID))
                    //FGStoreName = GetFGStoreName(x.FGStoreID)
                    //FGStore = (from s in context.Store
                    //          join b in context.BranchInformation on s.DepotID equals b.Slno
                    //          where ((s.Type == "FG" || s.Type == "FA") && b.Slno==x.Id)
                    //          select s.Name;)
                    //FGStore=context.Store.FirstOrDefault(m=> Convert.ToInt32(m.FGStoreID)==x.Id)
                });
            });

            ViewBag.Store = items;
            return View();
        }

        public string GetFGstoreNamebyId(int Id)
        {
            BEEContext context = new BEEContext();

            //var FGStore = context.Store.AsEnumerable().FirstOrDefault(m => m.Id == Id && (m.Type == "FG" || m.Type == "FA")).Name;
            var FGStore = context.Store.ToList().FirstOrDefault(m => m.Id == Id && (m.Type == "FG" || m.Type == "FA")).Name;
            return FGStore;

            //try
            //{
            //    //var id = Convert.ToInt32(Id);
            //    var FGStore = context.Store.AsEnumerable().FirstOrDefault(m => m.Id == Id && (m.Type == "FG" || m.Type == "FA")).Name;
            //    return FGStore;
            //}
            //catch (Exception)
            //{

            //    return id;
            //}

        }

        public ActionResult SaveStore(BEEERP.Models.SalesModule.Store store)
        {

            var storeExits = context.Store.FirstOrDefault(x => x.Name.ToLower() == store.Name.ToLower().Trim());

            if (storeExits == null)
            {
                context.Store.Add(store);
                context.SaveChanges();
                return Json(new { store.Id, Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.messege = "This Store  already exists";
                ViewBag.StoreId = generate.GenerateStoreId(context);
                ViewBag.Store = context.Store.ToList();
                ViewBag.Depot = LoadComboBox.LoadBranchInfo();
                ViewBag.StoreType = LoadComboBox.LoadStoreType();
                return Json(new { storeId = 0, Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetStoreByid(int id)     
        {
            var store = context.Store.FirstOrDefault(x => x.Id == id);
            if (store == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dpt = context.BranchInformation.FirstOrDefault(x => x.Slno == store.DepotID);
                store.Depot = dpt.BrnachName;
                var DID = Convert.ToString(store.DepotID);
                

                return Json(new { item = store }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetFGstoreName(string depotID)
        //{
        //    var FGStore = LoadComboBox.GetFGStoreByName(depotID);
        //    return Json(new { FGStore }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetFGstoreName(int depotID)
        {
            return Json(LoadComboBox.GetFGStoreByName(depotID), JsonRequestBehavior.AllowGet);
        }



        public ActionResult UpdateStore(BEEERP.Models.SalesModule.Store store)
        {
            var str = context.Store.ToList().Find(x => x.Id == store.Id);

            context.Store.Remove(str);

            context.Store.Add(store);
            context.SaveChanges();
            return Json(store.Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteStoreByid(int id)
        {
            var str = context.Store.ToList().Find(x => x.Id == id);

            context.Store.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
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

        public string GetFGStoreName(int id)
        {
            var item = context.Store.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }
    }

    public class StoreVModel    
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string DepotName { set; get; }
        public string Type { set; get; }
        public string FGStoreName { set; get; }
        //public string FGStoreName { set; get; }

    }
}