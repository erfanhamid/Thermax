using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    public class OfferController : Controller
    {
        // GET: Offer
        public ActionResult OfferEntry()
        {
            return View();
        }
    }
}