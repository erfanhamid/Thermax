using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers
{
    [ShowNotification]
    public class ChartOfInventoryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ChartOfInventory
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult ChartOfInventory()
        {
            ViewBag.AllInventory = LoadComboBox.GetAllInventory();
            ViewBag.GroupOrItem = LoadComboBox.LoadGroupItem();
            ViewBag.AllUOM = LoadComboBox.LoadAllUOM();
            //ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");

            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //Loop and add the Parent Nodes.
            //foreach (ChartOfInventory type in this.context.ChartOfInventory)
            //{
            //    nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = type.parentId == 0 ? "#" : type.parentId.ToString(), text = type.Name,icon=type.type=="g"? "glyphicon glyphicon-folder-open" : "glyphicon glyphicon-leaf" });
            //}
            return View();
        }

        public ActionResult LoadAllChartOfInventory()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //Loop and add the Parent Nodes.
            foreach (ChartOfInventory type in this.context.ChartOfInventory)
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = type.parentId == 0 ? "#" : type.parentId.ToString(), text = type.Name, icon = type.type == "g"||type.type=="f" ? "glyphicon glyphicon-folder-close" : "glyphicon glyphicon-leaf" });
            }

            return Json(new { jsonvar = nodes }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetGroupItemInfo(int id)
        {
            var dataFormInventoryById = context.ChartOfInventory.Where(s => s.Id == id).SingleOrDefault();

            return Json(new { inventory = dataFormInventoryById }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SaveItemOrGroup(ChartOfInventory inventoryData)
        {

            var namecheck = context.ChartOfInventory.FirstOrDefault(x => x.Name == inventoryData.Name);

            if (namecheck == null)
            {
                var maxID = context.ChartOfInventory.Max(s => s.Id);
                var IsExists = true;
                var data = context.ChartOfInventory.Where(s => s.Id == maxID).SingleOrDefault();
                while (IsExists)
                {
                    if (data != null)
                    {
                        maxID++;
                        data = context.ChartOfInventory.Where(s => s.Id == maxID).SingleOrDefault();
                    }
                    else
                    {
                        IsExists = false;
                    }
                }


                inventoryData.Id = maxID;
                context.ChartOfInventory.Add(inventoryData);


                InventoryTransactionBridge.InsertFromNewInventoryItem(ref context, inventoryData);
                AccountModuleBridge.InsertFromNewInventoryItem(ref context, inventoryData);


                context.SaveChanges();

                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }



        }



        public ActionResult UpdateItemOrGroup(ChartOfInventory inventoryData)
        {
            
            var data = context.ChartOfInventory.Where(s => s.Id == inventoryData.Id).SingleOrDefault();
            context.ChartOfInventory.Remove(data);


            inventoryData.parentId = data.parentId;
            inventoryData.type = data.type;
            inventoryData.rootAccountType = data.rootAccountType;


            context.ChartOfInventory.Add(inventoryData);
            context.SaveChanges();

            return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemOrGroup(int id)
        {
            
            var data = context.ChartOfInventory.Where(s => s.parentId == id).Count();
            if (data>0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var existsData = context.ChartOfInventory.Where(s => s.Id == id).SingleOrDefault();
                context.ChartOfInventory.Remove(existsData);
                context.SaveChanges();

                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            

           
        }
    }
}