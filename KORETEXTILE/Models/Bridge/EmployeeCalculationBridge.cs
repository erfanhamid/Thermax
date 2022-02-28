using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.HRModule;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class EmployeeCalculationBridge
    {
        //public static bool InsertFromEmployee(ref BEEContext context, Employee employee)
        //{
        //    int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
        //    var empob = context.EmployeeBalances.FirstOrDefault(x => x.EmployeeID == employee.Id && x.DocumentType == "OB" && x.AccountID == accId);
        //    if(empob==null)
        //    {
        //        context.EmployeeBalances.Add(new EmployeeBalance(employee.Id, employee.OBDate, "EOB", accId, "OB", "OB", Convert.ToDecimal(employee.OpBalance), employee.Id));
        //    }
        //    else
        //    {
        //        context.EmployeeBalances.Remove(empob);
        //        context.EmployeeBalances.Add(new EmployeeBalance(employee.Id, employee.OBDate, "EOB", accId, "OB", "OB", Convert.ToDecimal(employee.OpBalance), employee.Id));
        //    }
            
        //    return true;

        //}

        public static bool InsertFormSalaryProcess(ref BEEContext context, List<SalaryProcessViewModel> salaryProcesses, int salaryInfoNo, DateTime processDate, DateTime from, DateTime to)
        {
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Salary Payable").RefValue;
            var isExist = context.EmployeeBalances.Where(x => x.DocNO == salaryInfoNo && x.DocumentType == "MSE").ToList();
            if (isExist.Count > 0)
            {
                foreach (var item in isExist)
                {
                    context.EmployeeBalances.Remove(item);
                }
                foreach (var item in salaryProcesses)
                {
                    context.EmployeeBalances.Add(new EmployeeBalance() { EmployeeID = item.EmployeeId, TDate = processDate, DocumentType = "MSE", AccountID = accId, TRDescription = ("Salary from " +DateTimeFormat.ConvertToDDMMYYYY(from)+" to "+ DateTimeFormat.ConvertToDDMMYYYY(to)), RefNo = "", Amount = item.PayableSalary, DocNO = salaryInfoNo });
                };
            }
            else
            {
                foreach (var item in salaryProcesses)
                {
                    context.EmployeeBalances.Add(new EmployeeBalance() { EmployeeID = item.EmployeeId, TDate = processDate, DocumentType = "MSE", AccountID = accId, TRDescription = ("Salary from " + DateTimeFormat.ConvertToDDMMYYYY(from) + " to " + DateTimeFormat.ConvertToDDMMYYYY(to)), RefNo = "", Amount = item.PayableSalary, DocNO = salaryInfoNo });
                };
            }
            return true;
        }
    }
}