using BEEERP.Models.AccountModule;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Bridge
{
    public class LTRBalCalulationBridge
    {
        public static bool InsertUpdateFromLTR(ref BEEContext context, LTR LTR)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == LTR.LTRID && x.DocType == "LATR");
            if (LTRBal == null)
            {
                LTRBalCalculations LtrBal = new LTRBalCalculations();
                LtrBal.LTRID = LTR.LTRID;
                LtrBal.Date = LTR.Date;
                LtrBal.ACCID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
                LtrBal.Amount = LTR.Amount;
                LtrBal.Description = LTR.Description;
                LtrBal.RefNo = LTR.RefNo;
                LtrBal.DocType = "LATR";
                LtrBal.DocNo = LTR.LTRID;
                context.LTRBalCalculations.Add(LtrBal);
            }
            else
            {
                LTRBal.LTRID = LTR.LTRID;
                LTRBal.Date = LTR.Date;
                LTRBal.ACCID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Import LC Account").RefValue;
                LTRBal.Amount = LTR.Amount;
                LTRBal.Description = LTR.Description;
                LTRBal.RefNo = LTR.RefNo;
                LTRBal.DocType = "LATR";
                LTRBal.DocNo = LTR.LTRID;
                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool InsertUpdateFromLTROB(ref BEEContext context, LTR LTR)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == LTR.LTRID && x.DocType == "LATROB");
            if (LTRBal == null)
            {
                LTRBalCalculations LtrBal = new LTRBalCalculations();
                LtrBal.LTRID = LTR.LTRID;
                LtrBal.Date = LTR.Date;
                //LtrBal.ACCID = LTR.ACCID;
                LtrBal.ACCID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
                LtrBal.Amount = LTR.Amount;
                LtrBal.Description = LTR.Description;
                LtrBal.RefNo = LTR.RefNo;
                LtrBal.DocType = "LATROB";
                LtrBal.DocNo = LTR.LTRID;
                context.LTRBalCalculations.Add(LtrBal);
            }
            else
            {
                LTRBal.LTRID = LTR.LTRID;
                LTRBal.Date = LTR.Date;
                LTRBal.ACCID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
                LTRBal.Amount = LTR.Amount;
                LTRBal.Description = LTR.Description;
                LTRBal.RefNo = LTR.RefNo;
                LTRBal.DocType = "LATROB";
                LTRBal.DocNo = LTR.LTRID;
                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool DeleteFromLTRBal(ref BEEContext context, LTR LTR)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == LTR.LTRID && x.DocType=="LATR");
            if (LTRBal != null)
            {
                context.LTRBalCalculations.Remove(LTRBal);
                return true;
            }
            return false;
        }
        public static bool DeleteFromLTROBBal(ref BEEContext context, LTR LTR)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == LTR.LTRID && x.DocType == "LATROB");
            if (LTRBal != null)
            {
                context.LTRBalCalculations.Remove(LTRBal);
                return true;
            }
            return false;
        }
        //public static bool InsertFromLTRA(ref BEEContext context,LTRA ltra,string previoustype="")
        //{
        //    if (previoustype == "" || previoustype == null)
        //    {
        //        if (ltra.Type.Equals("Increase", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == ltra.LTRID && x.DocType == "LTRAPayable");
        //            if (LTRBal == null)
        //            {
        //                LTRBalCalculations LtrBal = new LTRBalCalculations();
        //                LtrBal.LTRID = ltra.LTRID;
        //                LtrBal.Date = ltra.Date;
        //                LtrBal.ACCID = ltra.AccountID;
        //                LtrBal.Amount = ltra.Amount;
        //                LtrBal.Description = ltra.Description;
        //                LtrBal.RefNo = "";
        //                LtrBal.DocType = "LTRAPayable";
        //                LtrBal.DocNo = ltra.LTRANo;
        //                context.LTRBalCalculations.Add(LtrBal);
        //            }
        //            else
        //            {
        //                LTRBal.LTRID = ltra.LTRID;
        //                LTRBal.Date = ltra.Date;
        //                LTRBal.ACCID = ltra.AccountID;
        //                LTRBal.Amount = ltra.Amount;
        //                LTRBal.Description = ltra.Description;
        //                LTRBal.RefNo = "";
        //                LTRBal.DocType = "LTRAPayable";
        //                LTRBal.DocNo = ltra.LTRANo;
        //                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //            }
        //            return true;
        //        }
        //        if (ltra.Type.Equals("Decrease", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == ltra.LTRID && x.DocType == "LTRAPayment");
        //            if (LTRBal == null)
        //            {
        //                LTRBalCalculations LtrBal = new LTRBalCalculations();
        //                LtrBal.LTRID = ltra.LTRID;
        //                LtrBal.Date = ltra.Date;
        //                LtrBal.ACCID = ltra.AccountID;
        //                LtrBal.Amount = -1 * ltra.Amount;
        //                LtrBal.Description = ltra.Description;
        //                LtrBal.RefNo = "";
        //                LtrBal.DocType = "LTRAPayment";
        //                LtrBal.DocNo = ltra.LTRANo;
        //                context.LTRBalCalculations.Add(LtrBal);
        //            }
        //            else
        //            {
        //                LTRBal.LTRID = ltra.LTRID;
        //                LTRBal.Date = ltra.Date;
        //                LTRBal.ACCID = ltra.AccountID;
        //                LTRBal.Amount = ltra.Amount;
        //                LTRBal.Description = ltra.Description;
        //                LTRBal.RefNo = "";
        //                LTRBal.DocType = "LTRAPayment";
        //                LTRBal.DocNo = ltra.LTRANo;
        //                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //            }
        //            return true;
        //        }
        //    }
        //    if (previoustype != "" && previoustype != null)
        //    {
        //        if (previoustype.Equals("Increase", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == ltra.LTRID && x.DocType == "LTRAPayable");
        //            if (LTRBal != null)
        //            {
        //                if (ltra.Type.Equals("Increase", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    LTRBal.LTRID = ltra.LTRID;
        //                    LTRBal.Date = ltra.Date;
        //                    LTRBal.ACCID = ltra.AccountID;
        //                    LTRBal.Amount = ltra.Amount;
        //                    LTRBal.Description = ltra.Description;
        //                    LTRBal.RefNo = "";
        //                    LTRBal.DocType = "LTRAPayable";
        //                    LTRBal.DocNo = ltra.LTRANo;
        //                    context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //                }
        //                if (ltra.Type.Equals("Decrease", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    LTRBal.LTRID = ltra.LTRID;
        //                    LTRBal.Date = ltra.Date;
        //                    LTRBal.ACCID = ltra.AccountID;
        //                    LTRBal.Amount = -1 * ltra.Amount;
        //                    LTRBal.Description = ltra.Description;
        //                    LTRBal.RefNo = "";
        //                    LTRBal.DocType = "LTRAPayment";
        //                    LTRBal.DocNo = ltra.LTRANo;
        //                    context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //                }
        //            }
        //        }
        //        if (previoustype.Equals("Decrease", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == ltra.LTRID && x.DocType == "LTRAPayment");
        //            if (LTRBal != null)
        //            {
        //                if (ltra.Type.Equals("Increase", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    LTRBal.LTRID = ltra.LTRID;
        //                    LTRBal.Date = ltra.Date;
        //                    LTRBal.ACCID = ltra.AccountID;
        //                    LTRBal.Amount = -1 * ltra.Amount;
        //                    LTRBal.Description = ltra.Description;
        //                    LTRBal.RefNo = "";
        //                    LTRBal.DocType = "LTRAPayable";
        //                    LTRBal.DocNo = ltra.LTRANo;
        //                    context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //                }
        //                if (ltra.Type.Equals("Decrease", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    LTRBal.LTRID = ltra.LTRID;
        //                    LTRBal.Date = ltra.Date;
        //                    LTRBal.ACCID = ltra.AccountID;
        //                    LTRBal.Amount = ltra.Amount;
        //                    LTRBal.Description = ltra.Description;
        //                    LTRBal.RefNo = "";
        //                    LTRBal.DocType = "LTRAPayment";
        //                    LTRBal.DocNo = ltra.LTRANo;
        //                    context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
        //                }
        //            }
        //        }

        //    }
        //    return false;
        //}
        public static bool InsertFromLTRA(ref BEEContext context, LTRA ltra)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.DocNo == ltra.LTRANo && x.LTRID == ltra.LTRID && x.DocType == "LATRA");
            if (LTRBal == null)
            {
                LTRBalCalculations LtrBal = new LTRBalCalculations();
                LtrBal.LTRID = ltra.LTRID;
                LtrBal.Date = ltra.Date;
                LtrBal.ACCID = ltra.AccountID;
                LtrBal.Amount = ltra.Amount;
                LtrBal.Description = ltra.Description;
                LtrBal.RefNo = "";
                LtrBal.DocType = "LATRA";
                LtrBal.DocNo = ltra.LTRANo;
                context.LTRBalCalculations.Add(LtrBal);
            }
            else
            {
                LTRBal.LTRID = ltra.LTRID;
                LTRBal.Date = ltra.Date;
                LTRBal.ACCID = ltra.AccountID;
                LTRBal.Amount = ltra.Amount;
                LTRBal.Description = ltra.Description;
                LTRBal.RefNo = "";
                LTRBal.DocType = "LATRA";
                LTRBal.DocNo = ltra.LTRANo;
                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool InsertFromLTRP(ref BEEContext context, LATRP ltra)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.DocNo == ltra.LTRPNo && x.LTRID == ltra.LTRID && x.DocType == "LATRP");
            if (LTRBal == null)
            {
                LTRBalCalculations LtrBal = new LTRBalCalculations();
                LtrBal.LTRID = ltra.LTRID;
                LtrBal.Date = ltra.Date;
                LtrBal.ACCID = ltra.AccountID;
                LtrBal.Amount = -ltra.Amount;
                LtrBal.Description = ltra.Description;
                LtrBal.RefNo = "";
                LtrBal.DocType = "LATRP";
                LtrBal.DocNo = ltra.LTRPNo;
                context.LTRBalCalculations.Add(LtrBal);
            }
            else
            {
                LTRBal.LTRID = ltra.LTRID;
                LTRBal.Date = ltra.Date;
                LTRBal.ACCID = ltra.AccountID;
                LTRBal.Amount = -ltra.Amount;
                LTRBal.Description = ltra.Description;
                LTRBal.RefNo = "";
                LTRBal.DocType = "LATRP";
                LTRBal.DocNo = ltra.LTRPNo;
                context.Entry<LTRBalCalculations>(LTRBal).State = EntityState.Modified;
            }
            return true;
        }
        public static bool DeleteFromLTRA(ref BEEContext context, LTRA ltra)
        {
                var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.DocNo == ltra.LTRANo && x.LTRID == ltra.LTRID && x.DocType == "LATRA");
                if (LTRBal != null)
                {
                    context.LTRBalCalculations.Remove(LTRBal);
                    return true;
                }
            
            //if (ltra.Type.Equals("Decrease", StringComparison.OrdinalIgnoreCase))
            //{
            //    var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.LTRID == ltra.LTRID && x.DocType == "LTRAPayment");
            //    if (LTRBal != null)
            //    {
            //        context.LTRBalCalculations.Remove(LTRBal);
            //        return true;
            //    }
            //}
            return false;
        }
        public static bool DeleteFromLTRP(ref BEEContext context, LATRP ltra)
        {
            var LTRBal = context.LTRBalCalculations.FirstOrDefault(x => x.DocNo == ltra.LTRPNo && x.LTRID == ltra.LTRID && x.DocType == "LATRP");
            if (LTRBal != null)
            {
                context.LTRBalCalculations.Remove(LTRBal);
                return true;
            }

   
            return false;
        }
    }
}