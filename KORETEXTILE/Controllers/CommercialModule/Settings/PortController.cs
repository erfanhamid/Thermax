using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommercialModule;
using System.Data.Entity;

namespace BEEERP.Controllers.CommercialModule.Settings
{
    [ShowNotification]
   // [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class PortController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Port
        public ActionResult Port()
        {
            ViewBag.ports = context.Ports.ToList();
            var savedPorts = context.Ports.Count();
            if (savedPorts == 0)
            {
                ViewBag.portNumber = 1;
            }
            else
            {
                ViewBag.portNumber = context.Ports.ToList().Max(m => m.PortID) + 1;
            }
            return View();
        }
        public ActionResult SavePort(Port port)
        {

            var portExits = context.Ports.FirstOrDefault(x => x.PortID == port.PortID);
            if (portExits == null)
            {
                try
                {
                    var savedPorts = context.Ports.Count();
                    if (savedPorts == 0)
                    {
                        port.PortID = 1;
                    }
                    else
                    {
                        port.PortID = context.Ports.ToList().Max(m => m.PortID) + 1;
                    }
                    context.Ports.Add(port);
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
        public ActionResult GetPortsByPortID(int portID)
        {
            var port = context.Ports.FirstOrDefault(m => m.PortID == portID);
            return Json(new { port = port, JsonRequestBehavior.AllowGet });
        }
        public ActionResult UpdatePort(Port port)
        {
            try
            {
                var export = context.Ports.AsNoTracking().FirstOrDefault(m => m.PortID == port.PortID);
                if (export == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<Port>(port).State = EntityState.Modified;
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