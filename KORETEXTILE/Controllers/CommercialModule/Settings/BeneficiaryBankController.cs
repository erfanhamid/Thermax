using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.CommercialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BEEERP.Controllers.CommercialModule.Settings
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class BeneficiaryBankController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: BeneficiaryBank
        public ActionResult BeneficiaryBank()
        {
            var savedBeneficiaryBanks = context.BeneficiaryBanks.Count();
            if (savedBeneficiaryBanks == 0)
            {
                ViewBag.numbersBeneficiaryBanks = 1;
            }
            else
            {
                ViewBag.numbersBeneficiaryBanks = context.BeneficiaryBanks.ToList().Max(m => m.BankId) + 1;
            }
            ViewBag.beneficiarybank = context.BeneficiaryBanks.ToList();
            return View();
        }
        public ActionResult SaveBeneficiaryBank(BeneficiaryBank beneficiaryBank)
        {

            var beneficiaryBankExits = context.Currencys.FirstOrDefault(x => x.CurrencyId == beneficiaryBank.BankId);
            if (beneficiaryBankExits == null)
            {
                try
                {
                    var savedBeneficiaryBanks = context.BeneficiaryBanks.Count();
                    if (savedBeneficiaryBanks == 0)
                    {
                        beneficiaryBank.BankId = 1;
                    }
                    else
                    {
                        beneficiaryBank.BankId = context.BeneficiaryBanks.ToList().Max(m => m.BankId) + 1;
                    }
                    context.BeneficiaryBanks.Add(beneficiaryBank);
                    context.SaveChanges();

                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }

            }
            else
            {
                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult GetBeneficiaryBankByBankId(int bankId)
        {
            var beneficiaryBank = context.BeneficiaryBanks.FirstOrDefault(m => m.BankId == bankId);
            return Json(new { beneficiaryBank = beneficiaryBank, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateBeneficiaryBank(BeneficiaryBank beneficiaryBank)
        {
            try
            {
                var exBeneficiaryBank = context.BeneficiaryBanks.AsNoTracking().FirstOrDefault(m => m.BankId == beneficiaryBank.BankId);
                if (exBeneficiaryBank == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<BeneficiaryBank>(beneficiaryBank).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}