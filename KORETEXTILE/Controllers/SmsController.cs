using BEEERP.Models.CustomAttribute;
using BEEERP.Models.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.SMS;


namespace BEEERP.Controllers
{
    [ShowNotification]
    public class SmsController : Controller
    {
        // GET: Sms
        public async Task<ActionResult> Index()
        {
            SMSService service = new SMSService();
            await service.SendSms("+8801867253953", "");
            return View();
        }
    }
}