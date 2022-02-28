using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class DealerIncentiveBalCalculationBridge
    {

        public static bool InsertUpdateFromMDSI(ref BEEContext context, MDSI mdsi, List<MDSILineItem> line)
        {
            var item = context.DealerIncentiveBalanceCalculation.Where(x => x.DocNo == mdsi.MDSINO && x.DocType == "MDSI").ToList();
            if (item.Count >  0)
            {
              
                foreach (var x in item)
                {
                    context.DealerIncentiveBalanceCalculation.Remove(x);
                }
                 
            }
           
            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Dealer Incentive Exp").RefValue;
            foreach(var d in line)
            {
                context.DealerIncentiveBalanceCalculation.Add(new DealerIncentiveBalanceCalculation(d.DealerID,mdsi.MDSIDate, "MDSI", accId, mdsi.Description, mdsi.RefNo, d.Amount, mdsi.MDSINO));
            }
            
            return true;
        }

        public static bool DeleteFromMDSI(ref BEEContext context, MDSI mdsi)
        {
            var item = context.DealerIncentiveBalanceCalculation.Where(x => x.DocNo == mdsi.MDSINO && x.DocType == "MDSI").ToList();
            if (item.Count > 0)
            {

                foreach (var x in item)
                {
                    context.DealerIncentiveBalanceCalculation.Remove(x);
                }

            }
            return true;
        }

        public static bool InsertUpdateFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dsia, List<DSIAdjustmentLineItem> line)
        {
            var item = context.DealerIncentiveBalanceCalculation.Where(x => x.DocNo == dsia.DSIANo && x.DocType == "DSIA").ToList();
            if (item.Count > 0)
            {

                foreach (var x in item)
                {
                    context.DealerIncentiveBalanceCalculation.Remove(x);
                }

            }

            int accId = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Customer Balance").RefValue;
            foreach (var d in line)
            {
                context.DealerIncentiveBalanceCalculation.Add(new DealerIncentiveBalanceCalculation(d.DealerID, dsia.DSIADate, "DSIA", accId, dsia.Description, dsia.RefNo, -d.Amount, dsia.DSIANo));
            }

            return true;
        }
        public static bool DeleteFromDSIAdj(ref BEEContext context, DealerSalesIncentiveAdjustment dsia)
        {
            var item = context.DealerIncentiveBalanceCalculation.Where(x => x.DocNo == dsia.DSIANo && x.DocType == "DSIA").ToList();
            if (item.Count > 0)
            {

                foreach (var x in item)
                {
                    context.DealerIncentiveBalanceCalculation.Remove(x);
                }

            }
            return true;
        }

    }
}