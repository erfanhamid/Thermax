using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
//using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.Database;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Controllers.StoreDepot;

namespace BEEERP.Controllers.SalesModule.Report
{
    [ShowNotification]
    //[Authorize(Roles = "SalAdmin,SalOperator,SalViewer,SalApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[SaleModule]
    public class SalesReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SalesReport
        public ActionResult SalesReportView()
        {
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            ViewBag.Group = LoadItemGroup();
            ViewBag.Item = LoadItem(null);

            return View();
        }

        public ActionResult GetCustomerById(int custId, int depotId)
        {
            var customer = FindObjectById.GetCustByStoreAndCustId(depotId, custId);
            if (customer != null)
            {
                return Json(new { Name = customer.CustomerName}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }


        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'g' and rootAccountType = 'FG' ";
            List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
            Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'l' and parentId = " + group + "";
                List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
                //context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name + " - " + x.PacSize); });
                return new SelectList(items, "Key", "Value");
            }

        }

        public ActionResult DepotWiseSalesSummary(ReportVModel model)
        {
            Session["ReportName"] = "DepotWiseSalesSummary";
            var items = context.Database.SqlQuery<DepotWiseSalesReport>(string.Format("exec DepotWiseSalesSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
          

            Session["DepotWiseSalesData"] = items;
            Session["DepotWiseSalesParam"] = param;

            return Redirect("/CrystalReport/DepotWiseSalesRpt.aspx");
        }

        // Dealer Wise Damage Summary

        public ActionResult DealerWiseDamageSummary(ReportVModel model)
        {
            Session["ReportName"] = "DealerWiseDamageReport";
            var items = context.Database.SqlQuery<DepotWiseSalesReport>(string.Format("exec DealerWiseDamageSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;


            Session["DepotWiseSalesData"] = items;
            Session["DepotWiseSalesParam"] = param;

            return Redirect("/CrystalReport/DepotWiseSalesRpt.aspx");
        }


        public ActionResult ShowDepotWiseDealerIncentiveSummary(ReportVModel model)    
        {
            Session["ReportName"] = "Depot And Dealer Wise Incentive Summary";
            var items = context.Database.SqlQuery<CrystalReport.ReportFormat.DepotAndDealerWiseIncentiveSummaryReport>(string.Format("exec DepotAndDealerWiseIncentiveSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'")).ToList();
            ReportParmPersister param = new ReportParmPersister();
            
            param.AsOnDate = model.AsOnDate;

            Session["IncData"] = items;
            Session["IncParam"] = param;

            return Redirect("/CrystalReport/DepotAndDealerWiseIncentiveSummaryRpt.aspx");
        }


        public ActionResult ShowDealerIncentiveLedger(ReportVModel model)
        {
            var dealername = LoadComboBox.GetCustInfo().FirstOrDefault(x => x.Id == model.CustomerID).CustomerName;
            Session["ReportName"] = "Incentive Ledger report Of Dealer - " + dealername;
            var items = context.Database.SqlQuery<DealerIncentiveLedgerReport>(string.Format("exec DealerIncentiveLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'"+"," + model.CustomerID)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.DepotId).BrnachName;
            Session["IncData"] = items;
            Session["IncParam"] = param;

            return Redirect("/CrystalReport/DealerIncentiveLedgerRpt.aspx");
        }
        public ActionResult DepotWiseDue(ReportVModel model)
        {
            
            var items = context.Database.SqlQuery<DepotWiseDueReport>(string.Format("exec DepotWiseDueReport '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["DueData"] = items;
            Session["DueParam"] = param;

            return Redirect("/CrystalReport/DepotWiseDueRpt.aspx");
        }
        public ActionResult DealerWiseSalesDetails(ReportVModel model)
        {

            var items = context.Database.SqlQuery<DealerWiseSalesDetailsReport>(string.Format("exec DealerWiseSalesDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "',"+ model.DepotId)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SalesDetailsData"] = items;
            Session["SalesDetailsParam"] = param;

            return Redirect("/CrystalReport/DealerWiseSalesDetails.aspx");
        }
        public ActionResult DealerWiseSalesSummary(ReportVModel model)
        {

            var items = context.Database.SqlQuery<DealerWiseSalesSummaryReport>(string.Format("exec DealerWiseSalesSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.DepotId)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SalesSummaryData"] = items;
            Session["SalesSummaryParam"] = param;

            return Redirect("/CrystalReport/DealerWiseSalesSummaryRpt.aspx");
        }
        public ActionResult DealerWiseDue(ReportVModel model)
        {

            var items = context.Database.SqlQuery<DealerWiseDueReport>(string.Format("exec [DealerWiseDueReport] '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'" )).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["DueData"] = items;
            Session["DueParam"] = param;

            return Redirect("/CrystalReport/DealerWiseDueRpt.aspx");
        }
        public ActionResult SingleDepotDealerWiseDue(ReportVModel model)
        {

            var items = context.Database.SqlQuery<DealerWiseDueReport>(string.Format("exec [SingleDepotDealerWiseDueReport] '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "',"+model.DepotId)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SingleDueData"] = items;
            Session["SingleDueParam"] = param;

            return Redirect("/CrystalReport/SingleDepotDealerDueRpt.aspx");
        }
        public ActionResult GetItemWiseSales(ReportVModel model)
        {

            var items = context.Database.SqlQuery<ItemWiseAllDepotSalesSummaryReport>(string.Format("exec ItemWiseAllDepotSalesSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemData"] = items;
            Session["ItemParam"] = param;

            return Redirect("/CrystalReport/ItemWiseAllDepotSalesSummary.aspx");
        }
        public ActionResult GetItemWiseSingleDepotSale(ReportVModel model)
        {

            var items = context.Database.SqlQuery<ItemWiseSingleDepotSaleReport>(string.Format("exec [ItemWiseSingleDepotSale] '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.DepotId)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemDataSingleDepot"] = items;
            Session["ItemParamSingleDepot"] = param;

            return Redirect("/CrystalReport/ItemWiseSingleDepotSaleRpt.aspx");
        }

        public ActionResult GetSingleItemDetails(ReportVModel model)
        {

            var items = context.Database.SqlQuery<SingleItemSalesDetailsReport>(string.Format("exec [SingleItemSalesDetails] '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.DepotId+","+model.Item)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            var parentId = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.Item);
            var group = "";
            if(parentId != null)
            {
                group = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == parentId.ParentId).Name;
            }
            param.ItemGroup = group;
            Session["SingleItemData"] = items;
            Session["SingleItemParam"] = param;

            return Redirect("/CrystalReport/SingleItemSalesDetailsRpt.aspx");
        }
        public ActionResult GetSingleItemSummary(ReportVModel model)
        {

            var items = context.Database.SqlQuery<SingleItemSalesSummaryReport>(string.Format("exec [SingleItemSalesSummaryOne] '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.Item)).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.Item).Name;
            var parentId = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.Item);
            var group = "";
            if (parentId != null)
            {
                group = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == parentId.ParentId).Name;
            }
            param.ItemGroup = group;
            Session["SingleItemSummaryData"] = items;
            Session["SingleItemSummaryParam"] = param;

            return Redirect("/CrystalReport/SingleItemSalesSummaryRpt.aspx");
        }



    }
}