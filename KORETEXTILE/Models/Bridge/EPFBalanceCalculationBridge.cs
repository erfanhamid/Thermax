using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.EmployeeAccountOB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class EPFBalanceCalculationBridge
    {

        public static bool InsertUpdateEPFOB(ref BEEContext context, EPFOB pfob)
        {
            var epfbal = context.EpfBalanceCalculation.FirstOrDefault(x => x.DocID == pfob.EmployeeId && x.DocType == "EPFOB" && x.EmpID == pfob.EmployeeId);

            if (epfbal != null)
            {
                context.EpfBalanceCalculation.Remove(epfbal);
            }

            EpfBalanceCalculation epfbc = new EpfBalanceCalculation();
            epfbc.DocID = pfob.EmployeeId;
            epfbc.EmpID = pfob.EmployeeId;
            epfbc.Date = pfob.Tdate;
            epfbc.DocType = "EPFOB";
            epfbc.TAmount = pfob.TAmount;
            epfbc.TReference = pfob.TRefNo;
            epfbc.Description = pfob.TDescription;
            epfbc.AccountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").Id;
            context.EpfBalanceCalculation.Add(epfbc);
            return true;
        }
        public static bool DeleteEPFOB(ref BEEContext context, EPFOB pfob)
        {
            var epfbal = context.EpfBalanceCalculation.FirstOrDefault(x => x.DocID == pfob.EmployeeId && x.DocType == "EPFOB" && x.EmpID == pfob.EmployeeId);
            if (epfbal != null)
            {
                context.EpfBalanceCalculation.Remove(epfbal);
            }
            return true;
        }
    }
}