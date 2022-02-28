using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public class ReportParmPersister
    {
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public double OpenBal { set; get; }
        public string CustId { set; get; }
        public int CustomerId { set; get; }
        public string CustName { set; get; }
        public string Address { set; get; }
        public string BranchName { set; get; }
        public DateTime AsOnDate { set; get; }
        public string TsoName { set; get; }
        public int TsoId { set; get; }
        public int SampleNo { set; get; }
        public string ChalanNo { set; get; }
        public DateTime SampleDate { set; get; }
        public int IFGSNo { set; get; }
        public DateTime IFGSDate { set; get; }
        public string SampleType { set; get; }
        public string PostedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string ShiftFrom { set; get; }
        public string ShiftTo { set; get; }
        public int SalesReturnNo { get; internal set; }
        public DateTime SalesReturnDate { get; internal set; }
        public int InvoiceNo { get; internal set; }
        public string AccountName { set; get; }
        public int BatchID { get; set; }
        public string BatchNo { get; set; }
        public string ReceiveAccount { set; get; }
        public string ReceiverName { set; get; }
        public string CostGroup { set; get; }
        public string RefNo { set; get; }
        public int RVNo { set; get; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string StoreName { get; set; }
        public string RSMName { get; set; }
        public int GRNNo { get; set; }
        public DateTime GRNDate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int  LCNo {get; set;}
        public DateTime LCDate { get; set; }
        public string Description { get; set; }
        public int ILCId { get; set; }
        public string ILCNo { get; set; }
        public string IALCNo { get; set; }
        public string LCType { get; set; }
        public string Month { get; set; }
        public string CompanyName { get; set; }
        public int Year { get; set; }
        public int Months { get; set; }
        public int SpvNo { get; set; }
        public DateTime SpDate { get; set; }
        public string DebitAccountNameSP { get; set; }
        public int ID { get; set; }
        public string EmpName { get; set; }
        public string EmpDesignation { get; set; }
        public int LCPCNo { get; set; }
        public string ImageLink { get; set; }
        public int StoreID { get; set; }
        public string RootAccountType { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemGroup { get; set; }
        public string DepotName { get; set; }
        public string SupplierGroup { get; set; }
        public int DID { get; set; }
        public int DepotID { get; set; }
        public int CGroup { set; get; }
        public int SupplierGroupID { set; get; }
        public int CostCenterID { set; get; }
        public string CostCenterName { set; get; }
        public int LedgerAccountID { set; get; }




        public string CompName {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return  company.CompanyName;
            }
            private set
            {

            }
        }
        public string CompAddress {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return company.CompanyAddress;
            }
            private set
            {

            }
        }
        public string CompContact
        {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return company.ConNo;
            }
            private set
            {

            }
        }
        public string Fax
        {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return company.Fax;
            }
            private set
            {

            }
        }
        public string FactAddress
        {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return company.FactoryAddress;
            }
            private set
            {

            }
        }
        public string FactContact
        {
            get
            {
                CompanyInformation company = new CompanyInformation();
                company.InitializeCompanyInformation();
                return company.FactoryContact;
            }
            private set
            {

            }
        }
    }
}