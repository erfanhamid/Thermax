using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.PaymentReceiveInfo;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using System.Data.Entity;
using BEEERP.Models.ViewModel.Sales.Transaction;
using BEEERP.Models.SystemSetting;
using BEEERP.Models.Bridge;
using BEEERP.Models.SMS;
using System.Threading.Tasks;
using System.IO;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.CrystalReport.ReportFormat;
using System.Net;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    [Permission]
    [SaleModule]
    public class PaymentVoucharController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PaymentVouchar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerVouchar()
        {
            ViewBag.DepotId = LoadComboBox.LoadBranchInfo();
            ViewBag.Account = LoadComboBox.LoadAccount(ScreenSessionData.GetEmployeeDepot());
            ViewBag.AC = null;
            var setting = context.SysSet.FirstOrDefault();
            Setting.IsPayVouAdjManually = setting.IsPayVouAdjManually;
            return View();
        }
        public  ActionResult SavePayMentRVouchar(ReceivePaymentInfo rcvPayInfo, List<Invoice> selectedInvoice)
        {
            try
            {
                using (context)
                {
                    //rcvPayInfo.TDate = DateTime.Now;
                    string RPID = "";
                    string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                    var noOfRPID = context.ReceivePaymentInfos.Count();
                    if (noOfRPID > 0)
                    {
                        var lastnoOfRPID = context.ReceivePaymentInfos.ToList().LastOrDefault(x => x.RPID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        RPID = yearLastTwoPart + (Convert.ToInt32(lastnoOfRPID.RPID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        RPID = yearLastTwoPart + "00001";
                    }

                    rcvPayInfo.RPID = Convert.ToInt32(RPID);
                    rcvPayInfo.Purpose = "CRV";
                    context.ReceivePaymentInfos.Add(rcvPayInfo);
                    var customer = FindObjectById.CustomerSearch(rcvPayInfo.CustomerID);
                    if (rcvPayInfo.PayMode == "sales")
                    {

                        
                        decimal netAmount = rcvPayInfo.ReceiveAmt;

                        foreach (var item in selectedInvoice)
                        {
                            if(item.DocType=="OB")
                            {
                                
                                decimal paidAmount = (from r in context.ReceivePaymentInfos.AsEnumerable()
                                                      join pv in context.PayVouLineItem.AsEnumerable() on r.RPID equals pv.RPID
                                                      where pv.DocType == "OB" &&r.CustomerID==rcvPayInfo.CustomerID
                                                      select pv.AdjustedAmount).ToList().Sum(x => x);
                                var invoice = context.CustomerBalanceCalculations.FirstOrDefault(x => x.DocumentType == "cob" && x.CustomerID == rcvPayInfo.CustomerID);
                                decimal saleAmount = invoice.Amount;
                                PayVouLineItem aPayVou = new PayVouLineItem();
                                aPayVou.RPID = rcvPayInfo.RPID;
                                aPayVou.InvoiceNo = item.InvoiceNo;
                                if (netAmount > 0)
                                {
                                    aPayVou.DocType = "OB";
                                    if ((saleAmount - paidAmount) >= netAmount)
                                    {
                                        aPayVou.AdjustedAmount = netAmount;
                                        netAmount -= netAmount;

                                        context.PayVouLineItem.Add(aPayVou);

                                    }
                                    else if ((saleAmount - paidAmount) < netAmount)
                                    {
                                        aPayVou.AdjustedAmount = saleAmount - paidAmount;
                                        netAmount -= saleAmount - paidAmount;
                                        context.PayVouLineItem.Add(aPayVou);
                                    }
                                }
                            }
                            else{
                                decimal paidAmount = context.PayVouLineItem.Where(x => x.InvoiceNo == item.InvoiceNo).ToList().Sum(x => x.AdjustedAmount);
                                var invoice = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == item.InvoiceNo);
                                decimal saleAmount = invoice.InvoiceAmount - invoice.DiscountAmt + invoice.VatAmount;
                                PayVouLineItem aPayVou = new PayVouLineItem();
                                aPayVou.RPID = rcvPayInfo.RPID;
                                aPayVou.InvoiceNo = item.InvoiceNo;
                                if (netAmount > 0)
                                {
                                    aPayVou.DocType = "Sales";
                                    if ((saleAmount - paidAmount) >= netAmount)
                                    {
                                        aPayVou.AdjustedAmount = netAmount;
                                        netAmount -= netAmount;

                                        context.PayVouLineItem.Add(aPayVou);

                                    }
                                    else if ((saleAmount - paidAmount) < netAmount)
                                    {
                                        aPayVou.AdjustedAmount = saleAmount - paidAmount;
                                        netAmount -= saleAmount - paidAmount;
                                        context.PayVouLineItem.Add(aPayVou);
                                    }
                                }
                            }
                            

                        }
                    }
                    else if (rcvPayInfo.PayMode == "advance")
                    {
                        PayVouLineItem aPayVou = new PayVouLineItem();
                        aPayVou.RPID = rcvPayInfo.RPID;
                        aPayVou.InvoiceNo = 0;
                        aPayVou.DocType = "Advance";
                        aPayVou.AdjustedAmount = 0;
                        context.PayVouLineItem.Add(aPayVou);
                    }

                    CustomerBalanceCalculation customerBalance = new CustomerBalanceCalculation();
                    customerBalance.CustomerID = rcvPayInfo.CustomerID;
                    customerBalance.TDate = rcvPayInfo.TDate;
                    customerBalance.DocumentType = "CRV";
                    customerBalance.DocNo = rcvPayInfo.RPID;
                    customerBalance.AccountID = rcvPayInfo.AccountID;
                    customerBalance.TrDesc = rcvPayInfo.Description;
                    customerBalance.RefNo = rcvPayInfo.RefNo;
                    customerBalance.Amount = rcvPayInfo.NetAmount * -1;
                    context.CustomerBalanceCalculations.Add(customerBalance);
                    if (rcvPayInfo.BankCharges > 0)
                    {
                        CustomerBalanceCalculation custBalBankCharge = new CustomerBalanceCalculation();
                        custBalBankCharge.CustomerID = rcvPayInfo.CustomerID;
                        custBalBankCharge.TDate = rcvPayInfo.TDate;
                        custBalBankCharge.DocumentType = "CRV";
                        custBalBankCharge.DocNo = rcvPayInfo.RPID;
                        custBalBankCharge.AccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Bank Charges").RefValue;
                        custBalBankCharge.TrDesc = rcvPayInfo.Description;
                        custBalBankCharge.RefNo = rcvPayInfo.RefNo;
                        custBalBankCharge.Amount = rcvPayInfo.BankCharges * -1;
                        context.CustomerBalanceCalculations.Add(custBalBankCharge);
                    }
                    AccountModuleBridge.InsertFromPaymentVoucher(ref context, rcvPayInfo);
                    context.SaveChanges();
                    //string message = "Dear Customer, We received your payment "+ rcvPayInfo.ReceiveAmt+"tk. "+"Receipt No : "+rcvPayInfo.RPID.ToString()+"Your outstanding balance is "+ SaleCommonInfo.GetDueByCustomerId(customer.Id) + "tk. Thank you";
                    //HttpWebRequest request = WebRequest.Create("http://sms.iglweb.com/smsapi.php?api_key=4451534223571511534223571&type=text&contacts="+customer.MobileNo+"&senderid=8804445604445&msg="+message+"") as HttpWebRequest;
                   
                    //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //Stream stream = response.GetResponseStream();

                    //SMSService sms = new SMSService();
                    //await sms.SendSms(customer.MobileNo.ToString(), message);
                    return Json(rcvPayInfo.RPID, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(rcvPayInfo.TDate.ToString("dd-MM-yyyy"),JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeletePayMentRVouchar(ReceivePaymentInfo rcvPayInfo)
        {
            try
            {
                using (context)
                {
                    
                    var advanceAmountCount = context.AdvanceAmountLine.Where(m => m.RPID == rcvPayInfo.RPID).ToList().Count();
                    if (advanceAmountCount == 0)
                    {
                        var receivePaymentInfoTobeDeleted = context.ReceivePaymentInfos.FirstOrDefault(m=>m.RPID==rcvPayInfo.RPID);
                        var payvouLineItemListTobeDeleted = context.PayVouLineItem.Where(m => m.RPID == rcvPayInfo.RPID).ToList();
                        payvouLineItemListTobeDeleted.ForEach(m => { context.PayVouLineItem.Remove(m); });
                        context.ReceivePaymentInfos.Remove(receivePaymentInfoTobeDeleted);
                        context.CustomerBalanceCalculations.Remove(context.CustomerBalanceCalculations.FirstOrDefault(x => x.CustomerID == rcvPayInfo.CustomerID && x.DocNo == rcvPayInfo.RPID && x.DocumentType == "CRV"));
                        var transection = context.Transection.Where(x => x.DocID == rcvPayInfo.RPID && x.DocType == "CRV").ToList();
                        foreach(var item in transection)
                        {
                            context.Transection.Remove(item);
                        }
                        context.SaveChanges();

                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult UpdatePayMentRVouchar(ReceivePaymentInfo rcvPayInfo)    
        //{
        //    try
        //    {
        //        using (context)
        //        {
        //            context.ReceivePaymentInfos.Where(x => x.RPID == rcvPayInfo.RPID).ToList().ForEach(x => context.ReceivePaymentInfos.Remove(x));
        //            context.Entry<ReceivePaymentInfo>(rcvPayInfo).State = EntityState.Modified;
        //            context.CustomerBalanceCalculations.Where(x => x.DocNo == rcvPayInfo.RPID && x.DocumentType == "DPR").ToList().ForEach(y => context.CustomerBalanceCalculations.Remove(y));

        //            rcvPayInfo.RPID = rcvPayInfo.RPID;
        //            rcvPayInfo.Purpose = "DPR";
        //            context.ReceivePaymentInfos.Add(rcvPayInfo);

        //            CustomerBalanceCalculation cbc = new CustomerBalanceCalculation();
        //            cbc.AccountID = rcvPayInfo.AccountID;
        //            cbc.CustomerID = rcvPayInfo.CustomerID;
        //            cbc.DocNo = rcvPayInfo.RPID;
        //            cbc.TDate = rcvPayInfo.TDate;
        //            cbc.DocumentType = rcvPayInfo.Purpose;
        //            var customer = FindObjectById.CustomerSearch(rcvPayInfo.CustomerID);
        //            cbc.TrDesc = customer.CustomerName;
        //            cbc.RefNo = rcvPayInfo.RefNo;
        //            double amount = rcvPayInfo.NetAmount;
        //            cbc.Amount = -amount;
        //            context.CustomerBalanceCalculations.Add(cbc);


        //            context.SaveChanges();
        //            return Json(rcvPayInfo.RPID, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetVoucharByRPID(int rpid)
        {
            var vouchar = context.ReceivePaymentInfos.FirstOrDefault(x => x.RPID == rpid);
            if (vouchar == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RevPaymentVModel rpvm = new RevPaymentVModel();
                var customer = context.Customers.FirstOrDefault(x => x.Id == vouchar.CustomerID);
                rpvm.DepotId = customer.DepotId;
                rpvm.CustomerName = customer.CustomerName;
                rpvm.Area = customer.AreaName;
                var zoneName = context.TblZones.FirstOrDefault(x => x.ZoneId == customer.ZoneId);
                rpvm.ZoneName = zoneName.ZoneName;
                rpvm.CustomerID = vouchar.CustomerID;
                rpvm.TDate = vouchar.TDate;
                decimal Due = SaleCommonInfo.GetDueByCustomerId(vouchar.CustomerID);
                rpvm.Due = Due;
                var accName = FindObjectById.ChartOfAccountSearch(vouchar.AccountID);
                rpvm.AccName = accName.Id.ToString();
                rpvm.RefNo = vouchar.RefNo;
                rpvm.Description = vouchar.Description;
                rpvm.ReceiveAmt = vouchar.ReceiveAmt;
                rpvm.NetAmount = vouchar.NetAmount;
                rpvm.BankCharges = vouchar.BankCharges;
                rpvm.PayMode = vouchar.PayMode;
                var lineItem = context.PayVouLineItem.Where(x => x.RPID == rpid).ToList();
                if (vouchar.PayMode == "advance")
                {
                    
                    lineItem = new List<PayVouLineItem>();
                }
                return Json(new { item = rpvm, LineItem = lineItem }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAccountByDepotId(int depot)
        {
            return Json(LoadComboBox.LoadAccount(depot), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDueByCustomerId(int custId)
        {
            var due = SaleCommonInfo.GetDueByCustomerId(custId);
            return Json(new { Due = due }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoiceInfoByCustomerId(int custId)
        {
            dynamic items = new List<dynamic>();
            var saleMain = context.SalesEntryNew.Where(x => x.CustomerID == custId).ToList();
            var cob = context.CustomerBalanceCalculations.FirstOrDefault(x => x.DocumentType == "cob" && x.CustomerID == custId);
            if(cob!=null)
            {
                var paidAmount = (from r in context.ReceivePaymentInfos.AsEnumerable()
                                 join pv in context.PayVouLineItem.AsEnumerable() on r.RPID equals pv.RPID where pv.DocType == "OB"&&r.CustomerID==custId select new { AdjustedAmount= pv.AdjustedAmount, InvoiceNo = "OB" }).ToList().Sum(x=>x.AdjustedAmount);
                var aItem = new { InvoiceNo="OB", InvoiceAmount = cob.Amount-(paidAmount) };
                if(aItem.InvoiceAmount>0)
                {
                    items.Add(aItem);
                }
               
            }
            foreach (var item in saleMain)
            {
                decimal paidAmount = context.PayVouLineItem.Where(x => x.InvoiceNo == item.InvoiceNo).ToList().Sum(x => x.AdjustedAmount);
                var invoice = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == item.InvoiceNo);
                decimal saleAmount = invoice.InvoiceAmount - invoice.DiscountAmt + invoice.VatAmount - paidAmount;
                if (saleAmount > 0)
                {
                    decimal adjustableAmount = item.InvoiceAmount - item.DiscountAmt + item.VatAmount - paidAmount;
                    if (adjustableAmount > 0)
                    {
                        var aItem = new { item.InvoiceNo, InvoiceAmount = adjustableAmount };
                        items.Add(aItem);
                    }

                }

            }
            return Json(new { item = items }, JsonRequestBehavior.AllowGet);

        }
        public sealed class Invoice
        {
            public int InvoiceNo { set; get; }
            public decimal InvoiceAmount { set; get; }
            public decimal AdjustedAmount { set; get; }
            public string DocType { set; get; }
        }

        public FileResult GetCustomerRVPreview(int RPID)
        {
            Session["ReportName"] = "CustomerRVPreviewR";
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec CustomerRVPreview '" + RPID + "'");
            var items = context.Database.SqlQuery<CustomerRVPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                CustomerRVPreviewReport report = new CustomerRVPreviewReport();
                items.Add(report);
            }
            CustomerRVPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public CustomerRVPreviewR ShowReport(ReportParmPersister persister, List<CustomerRVPreviewReport> data)
        {
            CustomerRVPreviewR customerRVPreviewR = new CustomerRVPreviewR();
            customerRVPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CustomerRVPreviewR.rpt");
            customerRVPreviewR.Load(APPPATH);
            customerRVPreviewR.SetDataSource(data);

            customerRVPreviewR.SetParameterValue("compName", persister.CompName);
            customerRVPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            customerRVPreviewR.SetParameterValue("compContact", persister.CompContact);
            customerRVPreviewR.SetParameterValue("compFax", persister.Fax);
            customerRVPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            customerRVPreviewR.SetParameterValue("factContact", persister.FactContact);
            return customerRVPreviewR;
        }      
    }

}