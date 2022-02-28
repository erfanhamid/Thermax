using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.EmployeeAccountOB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class EADBalanceCalculationBridge
    {

        public static bool InsertUpdateEADOB(ref BEEContext context, EADOB adob)
        {
            var eadbal = context.EadBalanceCalculation.FirstOrDefault(x => x.DocID == adob.EmployeeId && x.DocType == "EADOB" && x.EmpID == adob.EmployeeId);

            if (eadbal != null)
            {
                context.EadBalanceCalculation.Remove(eadbal);
            }

            EadBalanceCalculation eadbc = new EadBalanceCalculation();
            eadbc.DocID = adob.EmployeeId;
            eadbc.EmpID = adob.EmployeeId;
            eadbc.Date = adob.Tdate;
            eadbc.DocType = "EADOB";
            eadbc.TAmount = adob.TAmount;
            eadbc.TReference = adob.TRefNo;
            eadbc.Description = adob.TDescription;
            eadbc.AccountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").Id;
            context.EadBalanceCalculation.Add(eadbc);
            return true;
        }
        public static bool DeleteEADOB(ref BEEContext context, EADOB adob)
        {
            var eadbal = context.EadBalanceCalculation.FirstOrDefault(x => x.DocID == adob.EmployeeId && x.DocType == "EADOB" && x.EmpID == adob.EmployeeId);
            if (eadbal != null)
            {
                context.EadBalanceCalculation.Remove(eadbal);
            }
            return true;
        }
    }
}