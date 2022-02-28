using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;


namespace BEEERP.Models.SMS
{
    public class SMSService
    {
         
        public  async  Task<int> SendSms(string message,string receiver)
        {
            await Send(message,receiver);
            return 1;
        }
        private async  Task<int> Send(string receiver,string message)
        {
            string destinationUrl = "http://bulksms.bdmitech.com/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);

            byte[] bytes;
            //requestXml
            request.ContentType = "text/xml; encoding='utf-8'";
            //request.ContentLength = bytes.Length;
            request.Method = "POST";
            


            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            StringBuilder sb = new StringBuilder();
            int timeout = 10000000;
            XmlDocument doc = new XmlDocument();
            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                
                doc.Load(HttpContext.Current.Server.MapPath("~/Views/Sms.xml"));
                XmlNode first = doc.FirstChild.NextSibling.LastChild.FirstChild;
                first.ChildNodes[0].InnerText = "Sobuj Saya";
                first.ChildNodes[1].InnerText = receiver;
                first.ChildNodes[2].InnerText = message; //"Dear Customer, We received your payment 1000tk. Receipt No : 1800051. Thank you";
                first.ChildNodes[3].InnerText = "Y";
                first.ChildNodes[4].InnerText = "Y";
                first.ChildNodes[5].InnerText = "18052000000001";
                first.ChildNodes[6].InnerText = "sobujsaya";
                first.ChildNodes[7].InnerText = "12345678";
                doc.FirstChild.NextSibling.LastChild.ReplaceChild(first, doc.FirstChild.NextSibling.LastChild.FirstChild);
                doc.WriteTo(xw);
                doc.Save(HttpContext.Current.Server.MapPath("~/Views/Sms.xml"));
            }
            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/Views/Sms.xml")))
            {
                String line; //= await sr.ReadToEndAsync();
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
                byte[] postBytes = Encoding.UTF8.GetBytes(sb.ToString());

                if (timeout < 0)
                {
                    request.ReadWriteTimeout = timeout;
                    request.Timeout = timeout;
                }

                request.ContentLength = postBytes.Length;

                try
                {
                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var successMessage = reader.ReadToEnd();
                                using (BEEContext context = new BEEContext())
                                {
                                    SMSSuccess success = new SMSSuccess();
                                    success.SendingTime = DateTime.Now;
                                    success.Sender = "Sobuj Saya";
                                    success.Receiver = receiver;
                                    success.Message = successMessage;
                                    context.SMSSuccess.Add(success);
                                    context.SaveChanges();
                                }
                            }
                        }
                        
                       
                    }
                }
                catch (Exception ex)
                {
                    using (BEEContext context = new BEEContext())
                    {
                        SMSErrorLog log = new SMSErrorLog();
                        log.FailedTime = DateTime.Now;
                        log.Sender = "";
                        log.Receiver = receiver;
                        log.Message = message;
                        log.ErrorMessage = ex.Message;
                        context.SMSErrorLog.Add(log);
                        context.SaveChanges();
                    }
                }
            }
           // 

            return 1;
          
        }
    }
}