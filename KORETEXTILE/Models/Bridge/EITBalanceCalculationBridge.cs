using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.EmployeeAccountOB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class EITBalanceCalculationBridge
    {
        
        public static bool InsertUpdateEITOB(ref BEEContext context, EITOB itob)
        {
            var eitbal = context.EitBalanceCalculation.FirstOrDefault(x => x.DocID == itob.EmployeeId && x.DocType == "EITOB" && x.EmpID == itob.EmployeeId);
            
            if (eitbal != null)
            {
                context.EitBalanceCalculation.Remove(eitbal);
            }

            EitBalanceCalculation eitbc = new EitBalanceCalculation();
            eitbc.DocID = itob.EmployeeId;
            eitbc.EmpID = itob.EmployeeId;
            eitbc.Date = itob.Tdate;
            eitbc.DocType = "EITOB";
            eitbc.TAmount = itob.TAmount;
            eitbc.TReference = itob.TRefNo;
            eitbc.Description = itob.TDescription;
            eitbc.AccountId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").Id;
            context.EitBalanceCalculation.Add(eitbc);
            return true;
        }
        public static bool DeleteEITOB(ref BEEContext context, EITOB itob)
        {
            var eitbal = context.EitBalanceCalculation.FirstOrDefault(x => x.DocID == itob.EmployeeId && x.DocType == "EITOB" && x.EmpID == itob.EmployeeId);
            if (eitbal != null)
            {
                context.EitBalanceCalculation.Remove(eitbal);
            }
            return true;
        }
    }
}