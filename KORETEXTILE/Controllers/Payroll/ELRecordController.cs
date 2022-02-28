using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    public class ELRecordController : Controller
    {
        BEEContext context = new BEEContext();
        [ShowNotification]
        [Permission]
        //[Authorize(Roles = "RMAdmin,RMOperator,RMViewer,RMApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
        // GET: ELRecord
        public ActionResult ELREntry()
        {
            ViewBag.type = LoadComboBox.LoadLeaveType();
            return View();
        }
        public ActionResult GetLeaveBalance(int id, int empId, string fDate, bool IsUpdate)
        {

            if (id != 0 && empId != 0)
            {
                var data = 0;
                DateTime date = Convert.ToDateTime(fDate).Date;
                var prevLeave = 0;
                var LOB = 0;
                var msg = "";
       
                var checkAvailibility = (from el in context.EmployeeLeaveRecordLine
                                         join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                         where e.EmployeeID == empId && el.FromDate > date
                                         select el).ToList();

                if(checkAvailibility.Count > 0 && IsUpdate == false) {
                    msg += "You are not allow to take leave on the given date, Please Contact With authority";
                } else
                {
                    try
                    {
                        var empData = context.Employees.FirstOrDefault(x => x.Id == empId).ConfirmationDate;

                        if (empData != null)
                        {
                            try
                            {
                                var val = context.OpeningLeaveBalances.FirstOrDefault(x => x.EmployeeId == empId && x.ValidateUntil >= date );
                                LOB = val.CasualLeave + val.SickLeave + val.EarnLeave;
                            }
                            catch
                            {

                            }
                            if (LOB > 0)
                            {
                                var LeaveTypeName = context.LeaveType.FirstOrDefault(x => x.slno == id).Leavename;
                                if (LeaveTypeName == "Casual Leave")
                                {
                                    data = context.OpeningLeaveBalances.FirstOrDefault(x => x.EmployeeId == empId).CasualLeave;
                                }
                                else if (LeaveTypeName == "Sick Leave")
                                {
                                    data = context.OpeningLeaveBalances.FirstOrDefault(x => x.EmployeeId == empId).SickLeave;
                                }
                                else
                                {
                                    data = context.OpeningLeaveBalances.FirstOrDefault(x => x.EmployeeId == empId).EarnLeave;
                                }
                                if (IsUpdate == true)
                                {
                                    var prev = (from el in context.EmployeeLeaveRecordLine
                                                join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate < date
                                                select el);
                                    prevLeave = prev.Sum(x => x.LeaveDays);
                                }
                                else
                                {
                                    var prev = (from el in context.EmployeeLeaveRecordLine
                                                join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate <= date
                                                select el);
                                    prevLeave = prev.Sum(x => x.LeaveDays);
                                }

                            }
                            else
                            {
                                data = context.LeaveType.FirstOrDefault(x => x.slno == id).NumberOfDays;
                                var yof = date.Year;
                                var yon = DateTime.Now.Year;
                                if (yof > yon)
                                {
                                    prevLeave = 0;
                                }
                                else
                                {
                                    if (IsUpdate == true)
                                    {
                                        var prev = (from el in context.EmployeeLeaveRecordLine
                                                    join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                    where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate < date
                                                    select el);
                                        prevLeave = prev.Sum(x => x.LeaveDays);
                                    }
                                    else
                                    {
                                        var prev = (from el in context.EmployeeLeaveRecordLine
                                                    join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                    where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate <= date
                                                    select el);
                                        prevLeave = prev.Sum(x => x.LeaveDays);
                                    }
                                }

                            }


                        }
                        else
                        {
                            if (id == 1)
                            {
                                data = 5;
                                if (IsUpdate == true)
                                {
                                    var prev = (from el in context.EmployeeLeaveRecordLine
                                                join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate < date
                                                select el);
                                    prevLeave = prev.Sum(x => x.LeaveDays);
                                }
                                else
                                {
                                    var prev = (from el in context.EmployeeLeaveRecordLine
                                                join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                                where e.EmployeeID == empId && el.LeaveTypeId == id && el.FromDate <= date
                                                select el);
                                    prevLeave = prev.Sum(x => x.LeaveDays);
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                }

                
                var LeaveBalance = data - prevLeave;
                if(msg != "" && IsUpdate == false)
                {
                    return Json(new {Message=0, msg = msg }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {Message=1, LeaveBalance = LeaveBalance }, JsonRequestBehavior.AllowGet);
                }
                
            }



            return Json(0, JsonRequestBehavior.AllowGet);
        }

        //public int[] LateEmp(int id, int empId, DateTime fromDate, bool IsUpdate, DateTime date)
        //{

        //    int[] LateEmpData = new int[2];
        //    if (id == 1)
        //    {
        //        var lateEntryData = context.AttandanceLogs.Where(e => (e.CheckIn < fromDate && e.CheckIn.Hour >= 9 && e.CheckIn.Minute > 5 && e.EmployeeId == empId) || (e.CheckIn < fromDate && e.CheckIn.Hour > 9 && e.CheckIn.Minute >= 0 && e.EmployeeId == empId)).ToList().Count() / 3;
        //        LateEmpData[0] = lateEntryData;
        //    }
        //    if (IsUpdate == true)
        //    {
        //        var a = context.EmployeeLeaveRecords.Where(x => x.EmployeeID == empId && x.LeaveTypeID == id && x.FromDate < date).ToList();
        //        if (a.Count() > 0)
        //        {
        //            LateEmpData[1] = context.EmployeeLeaveRecords.Where(x => x.EmployeeID == empId && x.LeaveTypeID == id && x.FromDate < date).Sum(x => x.LeaveDays);
        //        }
        //    }
        //    else
        //    {
        //        var a = context.EmployeeLeaveRecords.Where(x => x.EmployeeID == empId && x.LeaveTypeID == id && x.FromDate <= date).ToList();
        //        if (a.Count() > 0)
        //        {
        //            LateEmpData[1] = context.EmployeeLeaveRecords.Where(x => x.EmployeeID == empId && x.LeaveTypeID == id && x.FromDate <= date).Sum(x => x.LeaveDays);
        //        }
        //    }
        //    return LateEmpData;
        //}

        public ActionResult CheckHoliday(string FromDate, string ToDate)
        {
            //var CheckHolidayRule = context.CompanySetup.Where(c => c.HolidayRules == true).ToList();
            //if(CheckHolidayRule.Count == 0)
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
                int countF = 0;
                int counth = 0;
                var x = "";
                DateTime fdate = Convert.ToDateTime(FromDate).Date;
                DateTime tdate = Convert.ToDateTime(ToDate).Date;
                foreach (var date in EachDay(fdate, tdate))
                {
                    x = date.DayOfWeek.ToString();
                    if (x == "Friday")
                    {
                        countF++;
                    }
                }
                var data = context.Holidays.Where(y => y.HolidayDate >= fdate && y.HolidayDate <= tdate).ToList();
                data.ForEach(d =>
                {
                    var f = d.HolidayDate.DayOfWeek.ToString();
                    if(f == "Friday")
                    {
                        counth++;
                    }
                });
                var holiday = data.Count - counth;
                int total = countF + holiday;
                return Json(total, JsonRequestBehavior.AllowGet);
            //}
            
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public ActionResult SaveElRecord(EmployeeLeaveRecords ELR, List<EmployeeLeaveRecordLine> addedItems)
        {
            var message = "";
            if(ELR != null)
            {
                int count = context.EmployeeLeaveRecords.Count();
                var countAddedItem = 0;
                if (count == 0)
                {
                    string s1 = ELR.ELRDate.ToString("yy");
                    string date = s1 + "00000";

                    ELR.ELRNo = Convert.ToInt32(date) + 1;
                }
               
                else
                {
                    ELR.ELRNo = context.EmployeeLeaveRecords.ToList().Max(x => x.ELRNo) + 1;
                }
                if(addedItems.Count > 0)
                {
                    addedItems.ForEach(x =>
                    {
                        DateTime Fdate = Convert.ToDateTime(x.FromDate);
                        //var IsExist = context.EmployeeLeaveRecordLine.Where(y => y.EmployeeID == ELR.EmployeeID && x.FromDate <= Fdate.Date && x.ToDate >= Fdate.Date).ToList();
                        var IsExist = (from el in context.EmployeeLeaveRecordLine
                                       join e in context.EmployeeLeaveRecords on el.ELRNo equals e.ELRNo
                                       where e.EmployeeID == ELR.EmployeeID && el.FromDate <= Fdate.Date && el.ToDate >= Fdate.Date
                                       select el).ToList();
                        if (IsExist.Count() == 0)
                        {
                            try
                            {
                                x.ELRNo = ELR.ELRNo;
                                context.EmployeeLeaveRecordLine.Add(x);
                                context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            countAddedItem++;
                             message += "Given date " + IsExist.FirstOrDefault().FromDate.ToString("DD-MM-YYYY") + " To " + IsExist.FirstOrDefault().ToDate.ToString("DD-MM-YYYY") + " Already In Leave List \n";
                        }

                    });
                    if(addedItems.Count == countAddedItem)
                    {
                        return Json(new { Message = 3, message = message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        context.EmployeeLeaveRecords.Add(ELR);
                        context.SaveChanges();
                    }
                    
                }

                UserLog.SaveUserLog(ref context, new UserLog(ELR.ELRNo.ToString(), DateTime.Now, "ELR", ScreenAction.Save));
                return Json(new { Message = 1, Id = ELR.ELRNo, message = message }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           

        }

        public ActionResult UpdateELR(EmployeeLeaveRecords ELR, List<EmployeeLeaveRecordLine> addedItems)
        {
            using (context)
            {
                var ELRExist = context.EmployeeLeaveRecords.FirstOrDefault(x => x.ELRNo == ELR.ELRNo);
                if(ELRExist != null)
                {
                    context.EmployeeLeaveRecords.Remove(ELRExist);
                    var line = context.EmployeeLeaveRecordLine.Where(x => x.ELRNo == ELR.ELRNo).ToList();
                    if(line.Count > 0)
                    {
                        line.ForEach(x =>
                        {
                            context.EmployeeLeaveRecordLine.Remove(x);
                        });
                    }

                    ELR.ELRDate = ELR.ELRDate.Add(DateTime.Now.TimeOfDay);

                    context.EmployeeLeaveRecords.Add(ELR);
                    context.SaveChanges();
                    addedItems.ForEach(x =>
                    {
                        x.ELRNo = ELR.ELRNo;
                        context.EmployeeLeaveRecordLine.Add(x);
                    });
                    context.SaveChanges();
                    return Json(ELR, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult GetDataByELRNo(int id)
        {
            var data = context.EmployeeLeaveRecords.ToList().FirstOrDefault(x => x.ELRNo == id);
            var Line = from el in context.EmployeeLeaveRecordLine
                       join l in context.LeaveType on el.LeaveTypeId equals l.slno
                       where el.ELRNo == id
                       select new { el.ELRNo, el.LeaveDays, el.LeaveTypeId, el.FromDate, el.ToDate, LeaveTypeName = l.Leavename};
            

            if(data != null)
            {
                return Json(new { data = data, LineItem = Line }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDataByEmployeeID(int id)
        {
            //try
            //{
                var data = context.Employees.FirstOrDefault(x => x.Id == id);
                if (data == null)
                {

                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (data.Designation != 0)
                    {
                        data.DesignationName = context.Designation.FirstOrDefault(x => x.Id == data.Designation).Name;
                    }
                    else
                    {
                        data.DesignationName = "";
                    }
                    if (data.BranchID != 0)
                    {
                        data.WorkStation = context.BranchInformation.FirstOrDefault(x => x.Slno == data.BranchID).BrnachName;
                    }
                    else
                    {
                        data.WorkStation = "";
                    }
                    if (data.DepatmentID != 0)
                    {
                        data.Department = context.Department.FirstOrDefault(x => x.DeptmentID == data.DepatmentID).DeprtmntName;
                    }
                    else
                    {
                        data.Department = "";
                    }
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            //}
            //catch(Exception e)
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
          
            
           
        }


        public ActionResult DeleteELEntryByid(int id)
        {
            var str = context.EmployeeLeaveRecords.ToList().Find(x => x.ELRNo == id);

            context.EmployeeLeaveRecords.Remove(str);
            //var strline = context.EmployeeLeaveRecordLine.ToList().Find(x => x.ELRNo == id);
            context.EmployeeLeaveRecordLine.Where(x => x.ELRNo == id).ToList().ForEach(x =>
            {
                context.EmployeeLeaveRecordLine.Remove(x);
            });
            UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "ELR", ScreenAction.Delete));
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

    }
}