using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.CommercialModule.Settings
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class CurrencyController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Currency
        public ActionResult Currency()
        {
            var savedCurrencys = context.Currencys.Count();
            if (savedCurrencys == 0)
            {
                ViewBag.numbersOfCorrencys = 1;
            }
            else
            {
                ViewBag.numbersOfCorrencys = context.Currencys.ToList().Max(m => m.CurrencyId) + 1;
            }
            ViewBag.currenyList = context.Currencys.ToList();
            return View();
        }
        public ActionResult SaveCurrency(Currency currency)
        {

            var currencyExits = context.Currencys.FirstOrDefault(x => x.CurrencyId == currency.CurrencyId);
            if (currencyExits == null)
            {
                try
                {
                    var savedCurrencys = context.Currencys.Count();
                    if (savedCurrencys == 0)
                    {
                        currency.CurrencyId = 1;
                    }
                    else
                    {
                        currency.CurrencyId = context.Currencys.ToList().Max(m => m.CurrencyId) + 1;
                    }
                    context.Currencys.Add(currency);
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
        public ActionResult GetCurrencyByCurrencyId(int currencyId)
        {
            var currency = context.Currencys.FirstOrDefault(m => m.CurrencyId == currencyId);
            return Json(new { currency = currency, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdateCurrency(Currency currency)
        {
            try
            {
                var excurrency = context.Currencys.AsNoTracking().FirstOrDefault(m => m.CurrencyId == currency.CurrencyId);
                if (excurrency == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<Currency>(currency).State = EntityState.Modified;
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