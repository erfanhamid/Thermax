using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Dashboard;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers
{
    [ShowNotification]
    public class HomeController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult Index()
        {
            ViewBag.thisYearTotalSales = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(SalesValue),0)) from SalesEntryNewLineItem sl inner join SalesEntryNew s on sl.InvoiceNo=s.InvoiceNo where year(s.SalesDate) =  year(GETDATE())").FirstOrDefault();
            ViewBag.thisMonthTotalSales = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(SalesValue),0)) from SalesEntryNewLineItem sl inner join SalesEntryNew s on sl.InvoiceNo=s.InvoiceNo where month(s.SalesDate) =  month(GETDATE()) and  year(s.SalesDate) =  year(GETDATE())").FirstOrDefault();
            ViewBag.FgInventory = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(Qty*TRType),0)) from InventoryTransaction i inner join chartofinv c on i.ciid = c.id where c.rootAccountType = 'FG' and type = 'l'").FirstOrDefault();
            ViewBag.RmInventory = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(Qty*TRType),0)) from InventoryTransaction i inner join chartofinv c on i.ciid = c.id where c.rootAccountType = 'RM' and type = 'l'").FirstOrDefault();
            ViewBag.Cash = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull((sum(trType*Amount)*-1),0)) from transection t inner join ChartOfAccount c on t.CAID = c.id inner join ChartOfAccount c1 on c.parentId = c1.id where c1.name = 'Cash'").FirstOrDefault();
            ViewBag.Bank = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull((sum(trType*Amount)*-1),0)) from transection t inner join ChartOfAccount c on t.CAID = c.id inner join ChartOfAccount c1 on c.parentId = c1.id where c1.name = 'Bank'").FirstOrDefault();
            ViewBag.TotalCollection = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(ReceiveAmt),0)) from ReceivePaymentInfo where year(tdate) = year(getdate())").FirstOrDefault();
            ViewBag.ThisMonthCollection = context.Database.SqlQuery<decimal>("select convert(decimal(18,2),isnull(sum(ReceiveAmt),0)) from ReceivePaymentInfo where month(tdate) = month(getdate()) and year(tdate) = year(getdate())").FirstOrDefault();


            return View();
        }
        //[Authorize(Roles = "AccAdmin,AccOperator")]
        public ActionResult GetChartData()
        {
            int blue = 40;
            int red = 80;
            int green = 150;
            var areaDetails = context.Database.SqlQuery<AreaDetails>("exec AreaDetails").ToList();
            List<decimal> totalSales = new List<decimal>();
            List<string> branchName = new List<string>();
            List<string> color = new List<string>();
            foreach (var item in areaDetails)
            {
                totalSales.Add(item.Sales);
                branchName.Add(item.BranchName);
                color.Add("rgb(" + red + ", " + green + ", " + blue + ")");
                red += 20;
                green +=60 ;
                blue -= 100;
                if (red > 255)
                {
                    red = 0;
                }
                if (blue > 255)
                {
                    blue = 0;
                }
                if (green > 255)
                {
                    green = 0;
                }
            }
            return Json(new
            {
                totalSales = totalSales,
                branchName = branchName,
                color = color

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepotWiseExpense()
        {
            int blue = 40;
            int red = 80;
            int green = 150;
            var areaDetails = context.Database.SqlQuery<ApvsAr>("exec DepotWiseExpense").ToList();
            List<decimal> totalSales = new List<decimal>();
            List<string> branchName = new List<string>();
            List<string> color = new List<string>();
            foreach (var item in areaDetails)
            {
                totalSales.Add(item.Amount);
                branchName.Add(item.Name);
                color.Add("rgb(" + red + ", " + green + ", " + blue + ")");
                red += 20;
                green += 60;
                blue -= 100;
                if (red > 255)
                {
                    red = 0;
                }
                if (blue > 255)
                {
                    blue = 0;
                }
                if (green > 255)
                {
                    green = 0;
                }
            }
            return Json(new
            {
                totalSales = totalSales,
                branchName = branchName,
                color = color

            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAcData()
        {

            List<decimal> amount = new List<decimal>();
            List<string> Name = new List<string>();
            List<string> Color = new List<string>();
            var AC = context.Database.SqlQuery<ApvsAr>("exec ApvsAr").ToList();
            foreach (var x in AC)
            {
                amount.Add(x.Amount);
                Name.Add(x.Name);
                var name = x.Name.Substring(4, (x.Name.LastIndexOf('e') - 2));
                if (name.Trim() == "Receiveable")
                {
                    Color.Add("rgb(89, 116, 119)");
                }
                else
                {
                    Color.Add("rgb(61, 228, 247)");
                }
                
            }
           
            return Json(new
            {
                amount = amount,
                Name = Name,
                Color = Color

            }, JsonRequestBehavior.AllowGet);
        }
       
        //public ActionResult GetMonthWiseSales()
        //{
        //    var monthsSales = context.Database.SqlQuery<MonthWiseSales>("exec MonthWiseSalesComparision").ToList();
        //    List<decimal> totalSales = new List<decimal>();
        //    List<string> monthName = new List<string>();
        //    List<string> color = new List<string>();
        //    List<string> Bcolor = new List<string>();
        //    List<string> Label = new List<string>();
        //    foreach (var item in monthsSales)
        //    {
        //        totalSales.Add(item.TotalSales);
        //        monthName.Add(item.MName);
        //        if(item.SYear == DateTime.Now.Year)
        //        {
        //            color.Add("rgb(153, 214, 255)");
        //            Bcolor.Add("rgb(153, 214, 200)");

        //        }
        //        else
        //        {
        //            color.Add("rgb(100, 100, 100)");
        //            Bcolor.Add("rgb(100, 100, 100)");
        //        }

        //    }
        //    return Json(new
        //    {
        //        totalSales = totalSales,
        //        monthName = monthName,
        //        color = color,
        //        bcolor = Bcolor

        //    }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetMonthWiseSales()
        {
            var PrevYearmonthsSales = context.Database.SqlQuery<MonthWiseSales>("exec MonthWiseSalesComparision").ToList();
            var ThisYearmonthsSales = context.Database.SqlQuery<MonthWiseSales>("exec MonthWiseSalesThisyear").ToList();
            List<decimal> PtotalSales = new List<decimal>();
            List<string> PmonthName = new List<string>();
            List<decimal> totalSales = new List<decimal>();
            List<string> monthName = new List<string>();
            var plabel = "Sales - "+PrevYearmonthsSales.FirstOrDefault().SYear.ToString();
            string label;
            if (ThisYearmonthsSales.Count==0)
            {
                 label= "Sales - "+DateTime.Now.Year.ToString() ;
            }
            else
            {
                 label = "Sales - " + ThisYearmonthsSales.FirstOrDefault().SYear.ToString();
            }
            
            foreach (var item in PrevYearmonthsSales)
            {
                PtotalSales.Add(item.TotalSales);
                PmonthName.Add(item.MName);

            }

            foreach (var item in ThisYearmonthsSales)
            {
                totalSales.Add(item.TotalSales);
                monthName.Add(item.MName);

            }
            var mName = PmonthName.Union(monthName);
            return Json(new
            {
                PtotalSales = PtotalSales,
                monthName = mName,
                totalSales= totalSales,
                plabel = plabel,
                label = label

            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMonthWiseSalesAndColletion()
        {
            var monthsSales = context.Database.SqlQuery<MonthWiseSales>("exec MonthWiseSales").ToList();
            var monthsCollection = context.Database.SqlQuery<MonthWiseCollection>("exec MonthWiseCollection").ToList();
            List<decimal> totalSales = new List<decimal>();
            List<decimal> totalCollection = new List<decimal>();
            List<string> SalesMonth = new List<string>();
            List<string> CMonth = new List<string>();
      

            foreach (var item in monthsSales)
            {
                totalSales.Add(item.TotalSales);
                SalesMonth.Add(item.MName);
               
         
            }
            foreach (var item in monthsCollection)
            {
                totalCollection.Add(item.TotalCollection);
                
                CMonth.Add(item.CName);
        

            }
           var Month = SalesMonth.Union(CMonth);
            return Json(new
            {
                totalSales = totalSales,
                SalesMonth = Month,
                totalCollection = totalCollection
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "AccAdmin,AccOperator")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application ascription page.";

            return View();
        }
        [Authorize(Roles = "AccAdmin,AccOperator")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetSalesSumamry()
        {
            List<string> x1 = new List<string>();
            List<decimal> y1 = new List<decimal>();

            var lastTlvMonthSale = context.Database.SqlQuery<MonthWiseSale>("select top(12) CONVERT(varchar(10), convert(char(3),DATENAME(MONTH, salesdate))) as SalesMonth,MONTH(SalesDate) as MonthNumb, YEAR(SalesDate) - 2000 SalesYear, sum(InvoiceAmount)/100000 as SalesAmount from SalesEntryNew group by CONVERT(varchar(10), convert(char(3), DATENAME(MONTH, salesdate))), MONTH(SalesDate),YEAR(SalesDate) order by YEAR(SalesDate) asc, MONTH(SalesDate) asc").ToList();
            foreach (var item in lastTlvMonthSale)
            {
                x1.Add(item.SalesMonth.ToString() + "-" + item.SalesYear);
                y1.Add(item.SalesAmount);
            }
            List<string> x2 = new List<string>();
            List<decimal> y2 = new List<decimal>();
            var lastThirtyDaySale = context.Database.SqlQuery<LastThirtyDaySale>("select top(20) CONVERT(varchar(10), CONVERT(char(3),DATENAME(MONTH, salesdate))) as SalesMonth,DAY(SalesDate) as SalesDay,MONTH(SalesDate) as MonthNumb, YEAR(SalesDate) - 2000 SalesYear, (sum(InvoiceAmount))/100000 as SalesAmount into #t from SalesEntryNew group by CONVERT(varchar(10), CONVERT(char(3), DATENAME(MONTH, salesdate))), MONTH(SalesDate), DAY(SalesDate), YEAR(SalesDate)order by YEAR(SalesDate) desc, MONTH(SalesDate) desc, DAY(SalesDate) desc select * from #t order by MonthNumb,SalesDay drop table #t").ToList();
            foreach (var item in lastThirtyDaySale)
            {
                x2.Add(item.SalesDay + "-" + item.SalesMonth.ToString() + "-" + item.SalesYear);
                y2.Add(item.SalesAmount);
            }

            List<string> x3 = new List<string>();
            List<decimal> y3 = new List<decimal>();
            var lastTlvMonthCollection = context.Database.SqlQuery<MonthWiseSale>("select top(12) CONVERT(varchar(10), convert(char(3),DATENAME(MONTH,TDate ))) as SalesMonth,MONTH(TDate) as MonthNumb,YEAR(TDate) - 2000 SalesYear, sum(netamount) / 100000 as SalesAmount from ReceivePaymentInfo group by CONVERT(varchar(10),convert(char(3), DATENAME(MONTH, TDate))), MONTH(TDate), YEAR(TDate) order by YEAR(TDate) asc, MONTH(TDate) asc").ToList();
            foreach (var item in lastTlvMonthCollection)
            {
                x3.Add(item.SalesMonth.ToString() + "-" + item.SalesYear);
                y3.Add(item.SalesAmount);
            }
            List<string> x4 = new List<string>();
            List<decimal> y4 = new List<decimal>();
            var lastThirtyDayCollection = context.Database.SqlQuery<LastThirtyDaySale>("select top(20) CONVERT(varchar(10), convert(char(3),DATENAME(MONTH, salesdate))) as SalesMonth,DAY(SalesDate) as SalesDay,MONTH(SalesDate) as MonthNumb, YEAR(SalesDate) - 2000 SalesYear, sum(InvoiceAmount) / 100000 as SalesAmount into #t from SalesEntryNew group by CONVERT(varchar(10), convert(char(3), DATENAME(MONTH, salesdate))), MONTH(SalesDate), DAY(SalesDate),YEAR(SalesDate) order by YEAR(SalesDate) desc, MONTH(SalesDate) desc, DAY(SalesDate) desc select * from #t order by MonthNumb,SalesDay,SalesYear drop table #t").ToList();
            foreach (var item in lastThirtyDayCollection)
            {
                x4.Add(item.SalesDay + "-" + item.SalesMonth.ToString() + "-" + item.SalesYear);
                y4.Add(item.SalesAmount);
            }
            return Json(new { x1 = x1, x2 = x2, y1 = y1, y2 = y2, x3 = x3, x4 = x4, y3 = y3, y4 = y4 }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[Authorize(Roles = "AccAdmin,AccOperator")]
        //public ActionResult GetLineCurveDataByDates(FromDateToDate dates)
        //{
        //    var salesMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec salesMonthWise'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
        //    return Json(new { salesMonthYearAmount = salesMonthYearAmount }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetLineCurveDataByDates(FromDateToDate dates)
        {
            var salesMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec salesMonthWise'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
            return Json(new { salesMonthYearAmount = salesMonthYearAmount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "AccAdmin,AccOperator")]
        public ActionResult GetBarChartDataByDates(FromDateToDate dates)
        {
            var targetsalesMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec TargetsalesMonthWiseYTDdyna'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
            var salesMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec salesMonthWise'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
            return Json(new { targetsalesMonthYearAmount= targetsalesMonthYearAmount, salesMonthYearAmount = salesMonthYearAmount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "AccAdmin,AccOperator")]
        public ActionResult GetHorizontalBarChartDataByDates(FromDateToDate dates)
        {
            var targetCollectionMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec TargetcollectionMonthWiseYTDdyna'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
            var CollectionMonthYearAmount = context.Database.SqlQuery<MonthYearAmount>("exec collectionMonthWiseYTDdyna'" + DateTimeFormat.ConvertToDDMMYYYY(dates.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(dates.ToDate) + "'").ToList();
            return Json(new { targetCollectionMonthYearAmount = targetCollectionMonthYearAmount, CollectionMonthYearAmount = CollectionMonthYearAmount }, JsonRequestBehavior.AllowGet);
        }
    }
}