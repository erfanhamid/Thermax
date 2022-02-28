using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.DataAdmin.SPSettings;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class MachineDetailsController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MachineDetails
        public ActionResult MachineDetails()
        {
            ViewBag.MFGYear = LoadComboBox.LoadAllMFGYear();
            ViewBag.CompanyID = LoadComboBox.LoadAllCompanyID();
            ViewBag.CountryID = LoadComboBox.LoadAllCountry();
            ViewBag.BrandID = LoadComboBox.LoadAllSPBrand();
            ViewBag.MachineSection = LoadComboBox.LoadAllMachineSection();
            ViewBag.MachineDetails = context.MachineDetails.ToList();



            var machineinfo = context.MachineDetails.ToList();
            var maxId = 0;
            if (machineinfo.Count > 0)
            {
                var machined = context.MachineDetails.Max(x => x.MachineID);
                maxId = machined + 1;

            }
            else
            {
                maxId = 1;

            }
            ViewBag.mid = maxId;

            var items = new List<MachineDetailVModel>();
            context.MachineDetails.ToList().ForEach(x =>
            {
                items.Add(new MachineDetailVModel
                {
                    MachineID = x.MachineID,
                    MachineSerial = x.MachineSerial,
                    UnitName = GetUnitName(x.CompanyID),
                    CountryName = GetCountryName(x.CountryID),
                    BrandName = GetBrandName(x.BrandID),
                    ModelNo = x.ModelNo,
                    YearName = GetYearName(x.MFGYear),
                    MSection = GetMSection(x.MSection)
                });
            });

            ViewBag.MachineDet = items;

            return View();
        }

        public string GetYearName(int id)
        {
            var item = context.YearList.FirstOrDefault(x => x.YearID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.YearName;
            }
        }

        public string GetUnitName(int id)
        {
            var item = context.CompanyList.FirstOrDefault(x => x.CompanyID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.CompanyName;
            }
        }
        public string GetCountryName(int id)
        {
            var item = context.Countries.FirstOrDefault(x => x.ID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }

        public string GetBrandName(int id)
        {
            var item = context.SPBrand.FirstOrDefault(x => x.BrandID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.BrandName;
            }
        }
        public string GetMSection(int id)
        {
            var item = context.MachineSection.FirstOrDefault(x => x.MSID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.MSName;
            }
        }

        public ActionResult GetMachineDetailsByid(int id)
        {
            var machine = context.MachineDetails.ToList().FirstOrDefault(x => x.MachineID == id);
            if (machine == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = machine }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMachineDetails(MachineDetails md)
        {
            var machineExist = context.MachineDetails.FirstOrDefault(x => x.MachineSerial == md.MachineSerial && x.CompanyID == md.CompanyID);
            if (machineExist == null)
            {
                context.MachineDetails.Add(md);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateMachineDetails(MachineDetails md)
        {
            var machineExit = context.MachineDetails.FirstOrDefault(x => x.MachineID == md.MachineID);

            if (machineExit != null)
            {
                context.MachineDetails.Remove(machineExit);
                context.MachineDetails.Add(md);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteMachineDetails(int id)
        {

            //var hasTransaction = context.SPInventoryTransaction.FirstOrDefault(x => x.MachineID == id);
            //if (hasTransaction == null)
            //{
                var machineid = context.MachineDetails.FirstOrDefault(x => x.MachineID == id);

                context.MachineDetails.Remove(machineid);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
        }
        public ActionResult ShowMachineList()
        {
            Session["ReportName"] = "ShowMachineListR";

            ReportParmPersister param = new ReportParmPersister();
            //param.AsOnDate = model.AsOnDate;
            ////param.FromDate = model.FromDate;
            ////param.ToDate = model.ToDate;
            ////param.ItemID = model.ItemID;
            ////param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            ////param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            ////param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["ShowMachineListparam"] = param;

            string sql = string.Format("exec spMachineList");
            var items = context.Database.SqlQuery<MachineListReport>(sql).ToList();
            if (items.Count == 0)
            {
                MachineListReport report = new MachineListReport();
                items.Add(report);
            }
            Session["ShowMachineListData"] = items;
            return Redirect("/CrystalReport/ShowMachineListRpt.aspx");
        }

    }

    public class MachineDetailVModel
    {
        public int MachineID { get; set; }
        public string MachineSerial { set; get; }
        public string ModelNo { set; get; }
        public string YearName { set; get; }
        public string MSection { set; get; }
        public string UnitName { set; get; }
        public string CountryName { set; get; }
        public string BrandName { set; get; }

    }
}