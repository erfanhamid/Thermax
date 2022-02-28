using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using BEEERP.Models.Log;
using BEEERP.Models.ViewModel.Payroll;
using Rotativa;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class SalaryPaymentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SalaryPayment
        public ActionResult SalaryPayment()
        {
            ViewBag.CompanyInfo = LoadComboBox.LoadCompanyInformation();
            ViewBag.Paymode = LoadComboBox.LoadReceiveAcc();
            return View();
        }

        public ActionResult GetEmployeeSalaryListByCompanyId(int companyId)
        {
            var employeeSalaryInfo = (from s in context.SalaryInformationLine join 
                                      e in context.Employees on s.EmployeeId equals e.Id join
                                      c in context.CompanyInfo on e.CompanyId equals c.Id
                                      where (e.CompanyId == companyId)
                                      select s).ToList();

            if(employeeSalaryInfo.Count() > 0)
            {
                List<EmployeeSalaryList> empSalaryList = new List<EmployeeSalaryList>();
                employeeSalaryInfo.ForEach(x =>
                {
                    var empAlreadyExist = empSalaryList.FirstOrDefault(y => y.EmployeeId == x.EmployeeId);
                    if (empAlreadyExist == null)
                    {
                        double salaryAmount = employeeSalaryInfo.Where(y => y.EmployeeId == x.EmployeeId).ToList().Select(z => z.TotalPayableSalary).DefaultIfEmpty(0).Sum();
                        decimal paidAmount = context.SalaryPaymentLineItem.Where(y => y.EmployeeId == x.EmployeeId).ToList().Select(z => z.SalaryAmount).DefaultIfEmpty(0).Sum();
                        decimal dueAmount = (decimal)salaryAmount - paidAmount;

                        if (dueAmount > 0)
                        {
                            EmployeeSalaryList employee = new EmployeeSalaryList();
                            employee.EmployeeId = x.EmployeeId;
                            employee.EmployeeName = context.Employees.FirstOrDefault(y => y.Id == x.EmployeeId).Name;
                            employee.SalaryAmount = dueAmount;
                            empSalaryList.Add(employee);
                        }
                    }
                });

                if(empSalaryList.Count > 0)
                {
                    return Json(new { empSalaryList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            } 
        }

        public ActionResult SaveSalaryPayment(SalaryPayment salaryPayment, List<SalaryPaymentLineItem> selectedEmployee)
        {
            using (context)
            {
                string spvNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.SalaryPayment.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.SalaryPayment.ToList().LastOrDefault(x => x.SPVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    spvNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SPVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    spvNo = yearLastTwoPart + "00001";
                }

                salaryPayment.SPVNo = Convert.ToInt32(spvNo);

                salaryPayment.Date = salaryPayment.Date.Add(DateTime.Now.TimeOfDay);
                selectedEmployee.ForEach(x =>
                {
                    if(x.SalaryAmount != 0)
                    {
                        x.SPVNo = salaryPayment.SPVNo;
                        context.SalaryPaymentLineItem.Add(x);

                        EmployeeBalance eb = new EmployeeBalance();
                        eb.EmployeeID = x.EmployeeId;
                        eb.AccountID = Convert.ToInt32(salaryPayment.Paymode);
                        eb.DocNO = salaryPayment.SPVNo;
                        eb.TDate = salaryPayment.Date;
                        eb.DocumentType = "ESP";
                        eb.TRDescription = "Salary Payment";
                        eb.RefNo = "";
                        eb.Amount = -x.SalaryAmount;
                        context.EmployeeBalances.Add(eb);
                    }
                });

                context.SalaryPayment.Add(salaryPayment);
                
                UserLog.SaveUserLog(ref context, new UserLog(salaryPayment.SPVNo.ToString(), DateTime.Now, "ESP", ScreenAction.Save));
                AccountModuleBridge.InsertFromSalaryPaymentAdjustment(ref context, salaryPayment);

                context.SaveChanges();
                return Json(new { salaryPayment }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult GetSalaryPaymentById(int id)
        {
            var salaryPaymentItem = context.SalaryPayment.FirstOrDefault(x => x.SPVNo == id);

            if (salaryPaymentItem != null)
            {
                var salaryPaymentLineItem = context.SalaryPaymentLineItem.Where(x => x.SPVNo == id).ToList().Select(x =>
                {
                    x.EmployeeName = context.Employees.FirstOrDefault(y => y.Id == x.EmployeeId).Name;
                    return x;
                }).ToList();

                return Json(new { salaryPaymentItem, salaryPaymentLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSalaryPaymentByid(int id)
        {
            var salaryPaymentExist = context.SalaryPayment.FirstOrDefault(x => x.SPVNo == id);
            if (salaryPaymentExist != null)
            {
                context.SalaryPayment.Remove(salaryPaymentExist);
                context.SalaryPaymentLineItem.Where(x => x.SPVNo == id).ToList().ForEach(x =>  
                {
                    context.SalaryPaymentLineItem.Remove(x);
                });

                context.EmployeeBalances.Where(x => x.DocNO == id && x.DocumentType == "ESP").ToList().ForEach(x =>
                {
                    context.EmployeeBalances.Remove(x);
                });

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "ESP", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromSalaryPayment(context, salaryPaymentExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalaryPaySlipPrintPreview(SalaryPaymentVModel model)
        {
            var spItem = context.SalaryPayment.FirstOrDefault(x => x.SPVNo == model.SPVNo);
            if(spItem != null)
            {
                List<SalaryPaySlipVModel> modelList = new List<SalaryPaySlipVModel>();
                var spLineItem = context.SalaryPaymentLineItem.Where(x => x.SPVNo == model.SPVNo).ToList();
                spLineItem.ForEach(x =>
                {
                    SalaryPaySlipVModel sv = new SalaryPaySlipVModel();

                    var companyDetails = context.CompanyInformation.FirstOrDefault();
                    sv.CompanyName = companyDetails.CompanyName;
                    sv.CompanyAddress = companyDetails.CompanyAddress;
                    sv.EmployeeCompany = context.CompanyInfo.FirstOrDefault(y => y.Id == spItem.CompanyId).CompanyName;
                    int payMode = Convert.ToInt32(spItem.Paymode);
                    sv.PaymentAccount = context.ChartOfAccount.FirstOrDefault(y => y.Id == payMode).Name;
                    var employee = context.Employees.FirstOrDefault(y => y.Id == x.EmployeeId);
                    sv.ReceivedBy = employee.Name;
                    sv.PVNo = x.SPVNo;
                    sv.Date = spItem.Date.ToString("dd-MM-yyyy");
                    sv.SlNo = 1;
                    sv.DebitAccountHead = "Salary Account.";
                    sv.Descriptions = "Salary Payment";
                    sv.Amount = x.SalaryAmount;
                    sv.TotalAmount = x.SalaryAmount;
                    sv.InWords = NumberToWord.NumberToWords((int)x.SalaryAmount);
                    sv.EmployeeId = x.EmployeeId;
                    sv.Designation = context.Designation.FirstOrDefault(y => y.Id == employee.Designation).Name;

                    modelList.Add(sv);
                });

                SalaryPaySilpListVModel vModel = new SalaryPaySilpListVModel();
                vModel.PaySlip = modelList;

                return new ViewAsPdf(vModel);
            }
            else
            {
                return new ViewAsPdf(new List<SalaryPaySlipVModel>());
            } 
        }

        public ActionResult GetPaymentVoucherPreview(int SPVNo)
        {
            Session["ReportName"] = "SalaryPaymentVoucherPreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var sp = context.SalaryPayment.FirstOrDefault(x => x.SPVNo == SPVNo);
            if (sp != null)
            {
                param.SpvNo = sp.SPVNo;
                var companyDetails = context.CompanyInformation.FirstOrDefault();
                
                if (companyDetails != null)
                {
                    param.CompanyName = context.CompanyInfo.FirstOrDefault(y => y.Id == sp.CompanyId).CompanyName;
                    param.Address = companyDetails.CompanyAddress;
                }

                int paymode = Convert.ToInt32(sp.Paymode);
                param.AccountName = context.ChartOfAccount.FirstOrDefault(x => x.Id == paymode).Name;
                param.DebitAccountNameSP = context.AccountConfiguration.FirstOrDefault(x => x.Name == "All Staff Salary").Name;
                param.SpDate = sp.Date;
                param.PostedBy = (from e in context.Employees.ToList()
                                  join
                                  u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
                                  where u.ScreenName == "ESP" && u.Action == "Save" && u.TrnasId == sp.SPVNo.ToString()
                                  select e.Name).FirstOrDefault();
            }
            else
            {
                param.SpvNo = 0;
                param.CompanyName = "";
                param.Address = "";
                param.SpDate = new DateTime();
                param.PostedBy = "";
            }

            Session["SalaryPaymentVoucherPreviewParam"] = param;

            string sql = string.Format("exec salaryPaymentVoucherPreviewsp '" + SPVNo + "'");
            var items = context.Database.SqlQuery<SalaryPayment>(sql).ToList();
            if (items.Count == 0)
            {
                SalaryPayment report = new SalaryPayment();
                items.Add(report);
            }
            SalaryPaymentVoucherPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public SalaryPaymentVoucherPreviewR ShowReport(ReportParmPersister persister, List<SalaryPayment> data)
        {
            SalaryPaymentVoucherPreviewR pcpR = new SalaryPaymentVoucherPreviewR();

            pcpR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SalaryPaymentVoucherPreviewR.rpt");
            pcpR.Load(APPPATH);
            pcpR.SetDataSource(data);

            pcpR.SetParameterValue("spvNo", persister.SpvNo);
            pcpR.SetParameterValue("companyName", persister.CompanyName);
            //pcpR.SetParameterValue("address", persister.Address);
            //pcpR.SetParameterValue("date", persister.SpDate);
            pcpR.SetParameterValue("accountName", persister.AccountName);
            pcpR.SetParameterValue("debitAccountNameSP", persister.DebitAccountNameSP);
            pcpR.SetParameterValue("postedBy", persister.PostedBy);
            pcpR.SetParameterValue("compName", persister.CompName);
            pcpR.SetParameterValue("compAddress", persister.CompAddress);
            pcpR.SetParameterValue("compContact", persister.CompContact);
            pcpR.SetParameterValue("compFax", persister.Fax);
            pcpR.SetParameterValue("factAddress", persister.FactAddress);
            pcpR.SetParameterValue("factContact", persister.FactContact);

            return pcpR;
        }
    }

    internal class EmployeeSalaryList
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal SalaryAmount { get; set; }
    }
}