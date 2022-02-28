using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class AttendanceLogController : Controller
    {
        // GET: AttendanceLog
        BEEContext beeContext = new BEEContext();
        List<AttendanceLogResultVModel> record = new List<AttendanceLogResultVModel>();
        public ActionResult SearchAttandance()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetSearchResult(DateTime fromdate,DateTime todate,string starthour= "09:00:59",string endhour="17:00:59")
        {
            if(fromdate!=null&&todate!=null)
            {
                //var recordList = (from ar in beeContext.AttandanceLogs
                //                  join a in beeContext.Employees on ar.EmployeeId equals a.Id
                //                  where ar.CheckIn >= fromdate && ar.CheckIn <= todate
                //                  select new { Id = ar.EmployeeId, Name = a.Name, CheckIn = ar.CheckIn.ToString(), CheckOut = ar.CheckOut.ToString(),StartDate=ar.CheckIn,EndDate=ar.CheckOut }).ToList();
                
                string sql = string.Format("exec AttendanceReport '"+ starthour + "','"+ endhour + "', '"+ DateTimeFormat.ConvertToDDMMYYYY(fromdate) + "', '"+ DateTimeFormat.ConvertToDDMMYYYY(todate) + "'");
                var result = beeContext.Database.SqlQuery<AttandanceLogVModel>(sql).ToList();
                foreach (var item in result)
                {
                    AttendanceLogResultVModel attendanceLogResultVModel = new AttendanceLogResultVModel();
                    attendanceLogResultVModel.EmployeeId = item.EmployeeId;
                    attendanceLogResultVModel.EmployeeName = item.EmployeeName;
                    attendanceLogResultVModel.CheckIn = item.CheckIn.ToString();
                    attendanceLogResultVModel.CheckOut = item.CheckOut.ToString();
                    attendanceLogResultVModel.Late = item.Late.ToString();
                    attendanceLogResultVModel.OverTime = item.OverTime.ToString();
                    attendanceLogResultVModel.EarlyLeave = item.EarlyLeave.ToString();
                    record.Add(attendanceLogResultVModel); ;
                }
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false);
            }
            
        }
    }
}