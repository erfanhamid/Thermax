using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.EmployeeAccountOB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class EMCBalanceCalculationBridge
    {
        public EMCBalanceCalculationBridge()
        {
        }

        public static bool InsertUpdateEMCOB(ref BEEContext context, EMCOB mcob)
        {
            var emcbal = context.EmcBalanceCalculation.FirstOrDefault(x => x.DocID == mcob.EmployeeId && x.DocType == "EMCOB" && x.EmpID == mcob.EmployeeId);

            if (emcbal != null)
            {
                context.EmcBalanceCalculation.Remove(emcbal);
            }

            EmcBalanceCalculation emcbc = new EmcBalanceCalculation();
            emcbc.DocID = mcob.EmployeeId;
            emcbc.EmpID = mcob.EmployeeId;
            emcbc.Date = mcob.Tdate;
            emcbc.DocType = "EMCOB";
            emcbc.TAmount = mcob.TAmount;
            emcbc.TReference = mcob.TRefNo;
            emcbc.Description = mcob.TDescription;
            emcbc.AccountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").Id;
            context.EmcBalanceCalculation.Add(emcbc);
            return true;
        }
        public static bool DeleteEMCOB(ref BEEContext context, EMCOB mcob)
        {
            var emcbal = context.EmcBalanceCalculation.FirstOrDefault(x => x.DocID == mcob.EmployeeId && x.DocType == "EMCOB" && x.EmpID == mcob.EmployeeId);
            if (emcbal != null)
            {
                context.EmcBalanceCalculation.Remove(emcbal);
            }
            return true;
        }
    }
}