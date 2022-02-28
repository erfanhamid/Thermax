using BEEERP.Models.Authentication;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.HRModule;
using BEEERP.Models.Notification;
using BEEERP.Models.SalesModule;
using LOgTest.Models.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BEEERP.Models.PaymentReceiveInfo;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.OrderBooking;
using BEEERP.Models.AccountModule;
using BEEERP.Models.SystemSetting;
using BEEERP.Models.Payroll;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Bridge;
using BEEERP.Models.Log;
using BEEERP.Models.Store_FG;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.Procurement.WorkOrder1; 
using BEEERP.Models.StoreRM.IRM;
using BEEERP.Models.StoreDepot;
using BEEERP.Models.Procurement;
using BEEERP.Models.SMS;
using BEEERP.Models.CommercialModule.Settings;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.CommercialModule.Settings;
using LocationApi.Model;
using LocationApi.Models;
using BEEERP.Models.Location;
using BEEERP.Models.FreezerRent;
using BEEERP.Models.TaxCalculator;
using BEEERP.Models.EmployeeAccountOB;
using BEEERP.Models.StoreRM;
using BEEERP.Models.StoreRM.RMSalesEntry;
using BEEERP.Models.CostingModule;
using BEEERP.Models.SpareParts;
using BEEERP.Models.InventoryModule;
using BEEERP.Models.DataAdmin.SPSettings;
using BEEERP.Models.GeneralStore;

namespace BEEERP.Models.Database
{
    public class BEEContext : DbContext
    {
        public BEEContext() : base("connection")
        {

        }
        public static BEEContext Create()
        {
            return new BEEContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseLineItems>().Property(x => x.Qty).HasPrecision(18, 4);
        }
        public DbSet<RMCApplyEntry> RMCApplyEntry { get; set; }
        public DbSet<RMCApplyLineItem> RMCApplyLineItem { get; set; }
        public DbSet <tblLCCosting> tblLCCosting { get; set; }
        public DbSet<tblLCCostingLine> tblLCCostingLine { get; set; }
        public DbSet<tblLCCostingQtBalance> tblLCCostingQtBalance { get; set; }
        public DbSet<GlConfiguration> GlConfiguration { get; set; }
        public DbSet<FARR> FARR { get; set; }
        public DbSet<FARRLineItem> FARRLineItem { get; set; }
        public DbSet<FRILineItem> FRILineItem { get; set; }
        public DbSet<FRI> FRI { get; set; }
        public DbSet<TblFreezerRentLineItem> TblFreezerRentLineItem { get; set; }
        public DbSet<TblFreezerRent> TblFreezerRent { get; set; }
        public DbSet<RMSales> RMSales { get; set; }
        public DbSet<TblRMSalesLineItem> TblRMSalesLineItem { get; set; }
        public DbSet<RMCustomerBalanceCalculation> RMCustomerBalanceCalculation { get; set; }
        public DbSet<CreateCustomer> CreateCustomer { get; set; }
        public DbSet<InstallmentBalanceCalculation> InstallmentBalanceCalculation { get; set; }
        public DbSet<RetailerInformation> RetailerInformation { get; set; }
        public DbSet<EmployeeIncrement> EmployeeIncrement { get; set; }
        public DbSet<EmployeePromotion> EmployeePromotion { get; set; }
        public DbSet<EmployeeTransfer> EmployeeTransfer { get; set; }
        public DbSet<EmployeeArrearEntry> EmployeeArrearEntry { get; set; }
        public DbSet<EmployeeCashAllowances> EmployeeCashAllowances { get; set; }
        public DbSet<LeftEmployee> LeftEmployee { get; set; }
        public DbSet<EadBalanceCalculation> EadBalanceCalculation { get; set; }
        public DbSet<EADOB> EADOB { get; set; }
        public DbSet<EmcBalanceCalculation> EmcBalanceCalculation { get; set; }
        public DbSet<EMCOB> EMCOB { get; set; }
        public DbSet<EpfBalanceCalculation> EpfBalanceCalculation { get; set; }
        public DbSet<EPFOB> EPFOB { get; set; }
        public DbSet<EitBalanceCalculation> EitBalanceCalculation { get; set; }
        public DbSet<EITOB> EITOB { get; set; }
        public DbSet<EmployeeSalaryDeductions> EmployeeSalaryDeductions { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendance { get; set; }
        public DbSet<FWOCapacity> FWOCapacities { get; set; }
        public DbSet<FWOModel> FWOModels { get; set; }
        public DbSet<FWOBrand> FWOBrands { get; set; }
        public DbSet<FWOType> FWOTypes { get; set; }
        public DbSet<FPB> FPB { get; set; }
        public DbSet<FPBLine> FPBLine { get; set; }
        public DbSet<TblZone> TblZones { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<BranchInformation> BranchInformation { set; get; }
        public DbSet<ChartOfAccount> ChartOfAccount { set; get; }
        public DbSet<UserActionPermission> UserActionPermission { set; get; }
        public DbSet<UserWiseApproved> UserWiseApproved { set; get; }
        public DbSet<UserNotification> Notification { set; get; }
        public DbSet<ChartOfInventory> ChartOfInventory { set; get; }
        public DbSet<Store> Store { set; get; }
        public DbSet<Designations> Designation { set; get; }
        public DbSet<Employee> Employees { set; get; }
        public DbSet<InventoryTransaction> InventoryTransaction { set; get; }
        public DbSet<SalesEntryNew> SalesEntryNew { set; get; }
        public DbSet<SalesEntryNewLineItem> SalesEntryNewLineItem { set; get; }
        public DbSet<NotificationUser> NotificationUsers { set; get; }
        public DbSet<ApprovalLog> ApprovalLog { set; get; }
        public DbSet<ReceivePaymentInfo> ReceivePaymentInfos { get; set; }
        public DbSet<TblCashAccountConfiguration> TblCashAccountConfigurations { get; set; }
        public DbSet<CustomerBalanceCalculation> CustomerBalanceCalculations { get; set; }
        public DbSet<AccountConfiguration> AccountConfiguration { set; get; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<PayVouLineItem> PayVouLineItem { get; set; }
        public DbSet<Transection> Transection { get; set; }
        public DbSet<OrderBookingLineItem> OrderBookingLineItem { get; set; }
        public DbSet<OrderBookings> OrderBookings { get; set; }
        public DbSet<Sqquencer> Sqquencers { set; get; }
        public DbSet<SysSet> SysSet { set; get; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<FunctionalDesignation> FunctionalDesignation { get; set; }
        public DbSet<EmployeeSection> EmployeeSection { get; set; }
        public DbSet<HighestEducation> HighestEducation { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<ReasonOfLeaving> ReasonOfLeaving { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<MarriageStatus> MarriageStatuse { get; set; }
        public DbSet<Religion> Religion { get; set; }
        public DbSet<CompanySetup> CompanySetup { get; set; }
        public DbSet<UOM> UOM { get; set; }
        public DbSet<ChartOfInvOb> ChartOfInvOb { set; get; }
        public DbSet<ChartOfInvObLineItem> ChartOfInvObLineItem { set; get; }
        public DbSet<ChartOfInvFGOB> ChartOfInvFGOB { set; get; }
        public DbSet<ChartOfInvFGOBLineItem> ChartOfInvFGOBLineItem { set; get; }
        public DbSet<UserWiseModule> UserWiseModule { set; get; }
        public DbSet<EmployeeBalance> EmployeeBalances { set; get; }
        public DbSet<UserLog> UserLog { set; get; }
        public DbSet<TsoRsm> TsoRsm { set; get; }
        public DbSet<ShiftInventory> ShiftInventorie { get; set; }
        public DbSet<ShiftInventoryLineItem> ShiftInventoryLineItem { get; set; }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<IssueFGStore> IssueFGStore { get; set; }
        public DbSet<IssueFGStoreLineItem> IssueFGStoreLineItem { get; set; }
        public DbSet<FGProductionLineItem> FGProductionLineItem { set; get; }
        public DbSet<FGProduction> FGProduction { set; get; }
        public DbSet<BatchLineItem> BatchLineItem { set; get; }
        public DbSet<SIFD> SIFD { get; set; }
        public DbSet<SIFDLineItem> SIFDLineItem { get; set; }
        public DbSet<FGRP> FGRP { set; get; }
        public DbSet<FGRPLineItem> FGRPLineItem { set; get; }
        public DbSet<ChartOfInvRMOB> ChartOfInvRMOB { set; get; }
        public DbSet<ChartOfInvRMOBLineItem> ChartOfInvRMOBLineItem { set; get; }
        public DbSet<GoodsReceiveNoteLineItem> GoodsReceiveNoteLineItem { get; set; }
        public DbSet<GoodsReceiveNote> GoodsReceiveNote { get; set; }
        public DbSet<RMCEntry> RMCEntry { get; set; }
        public DbSet<RMCLineItem> RMCLineItem { get; set; }
        public DbSet<IRM> IRM { get; set; }
        public DbSet<IRMLineItem> IRMLine { get; set; }
        public DbSet<GRF> GRF { get; set; }
        public DbSet<GRFLineItem> GRFLineItem { get; set; }
        public DbSet<CompanyInformation> CompanyInformation { set; get; }
        public DbSet<SupplierGroup> SupplierGroups { get; set; }
        public DbSet<PurchaseLineGRN> PurchaseLineGRN { set; get; }
        public DbSet<PayBillInfo> PayBillInfo { get; set; }
        public DbSet<PaymentVoucher> PaymentVoucher { get; set; }
        public DbSet<PurchaseEntry> PurchaseEntry { get; set; }
        public DbSet<PurchaseLineItems> PurchaseLineItem { get; set; }
        public DbSet<SupplierBalanceCalculation> SupplierBalanceCalculation { get; set; }
        public DbSet<FreezerWorkOrder> FreezerWorkOrder { get; set; }
        public DbSet<FreezerWorkOrderLineItem> FreezerWorkOrderLineItem { get; set; }
        public DbSet<FreezerModelDescription> FreezerModelDescription { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<WorkOrderAIT> WorkOrderAIT { get; set; }
        public DbSet<WorkOrderLineItem> WorkOrderLineItem { get; set; }
        public DbSet<WorkOrderVat> WorkOrderVat { get; set; }
        public DbSet<GeneralBill> GeneralBills { set; get; }
        public DbSet<GeneralBillsLineItem> GeneralBillsLineItems { set; get; }
        public DbSet<SMSSuccess> SMSSuccess { set; get; }
        public DbSet<SMSErrorLog> SMSErrorLog { set; get; }
        public DbSet<AdvanceAmountInfo> AdvanceAmountInfo { get; set; }
        public DbSet<AdvanceAmountLineItem> AdvanceAmountLine { get; set; }
        public DbSet<ImportCostItem> ImportCostItems { get; set; }
        public DbSet<DSIA> DSIA { get; set; }
        public DbSet<DSIALineItem> DSIALineItem { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Currency> Currencys { get; set; }
        public DbSet<MOP> MOPs { get; set; }
        public DbSet<Incoterm> Incoterms { get; set; }
        public DbSet<SalesReturnEntry> SalesReturnEntrie { get; set; }
        public DbSet<SalesReturnLineItem> SalesReturnLineItem { get; set; }
        public DbSet<BeneficiaryBank> BeneficiaryBanks { get; set; }
        public DbSet<ImportProformaInvoice> ImportProformaInvoices { get; set; }
        public DbSet<ImportProformaInvoiceLineItems> ImportProformaInvoiceLineItems { get; set; }
        public DbSet<ImportLC> ImportLC { get; set; }
        public DbSet<ImportLCLineItem> ImportLCLineItem { get; set; }
        public DbSet<ILCOB> ILCOB { get; set; }
        public DbSet<ILCOBLineItem> ILCOBLineItem { get; set; }
        public DbSet<ReportPermission> ReportPermission { get; set; }
        public DbSet<LocationTrack> Locationtrack { set; get; }
        public DbSet<DealerTravelLocation> DealerTravelLocation { set; get; }
        public DbSet<FundTransferVoucher> FundTransferVouchers { set; get; }
        public DbSet<ReceiveVoucher> ReceiveVouchers { set; get; }
        public DbSet<JournalVoucherOne> JournalVoucherOne { set; get; }
        public DbSet<JournalVoucherOneLineItem> JournalVoucherOneLineItem { set; get; }
        public DbSet<Subledgers> Subledger { set; get; }
        public DbSet<JVSubLedgerLine> JVSubLedgerLine { get; set; }
        public DbSet<MoneyRequisitionBalanceCalculation> MoneyRequisitionBalanceCalculations { get; set; }
        public DbSet<RequisitionMoneyVoucher> RequisitionMoneyVoucher { get; set; }
        public DbSet<RequisitionMoneyVoucherLineItem> RequisitionMoneyVoucherLineItem { get; set; }
        public DbSet<MoneyRequisitionVoucher> MoneyRequisitionVouchers { get; set; }
        public DbSet<MRVAReference> MRVAReferences { get; set; }
        public DbSet<MoneyRequisitionVoucherAdjust> MoneyRequisitionVoucherAdjusts { get; set; }
        public DbSet<MoneyRequisitionVoucherAdjustLineItem> MoneyRequisitionVoucherAdjustLineItems { get; set; }
        public DbSet<MRPA> MRPA { get; set; }
        public DbSet<PRS> PRs { get; set; }
        public DbSet<ExemtionList> ExemtionList { get; set; }
        public DbSet<SLabList> SLabList { get; set; }
        public DbSet<RebatePercntgList> RebatePercntgList { get; set; }
        public DbSet<ExemtionOnTtl> ExemtionOnTtl { get; set; }
        public DbSet<PaymentVoucherOne> PaymentVoucherOne { get; set; }
        public DbSet<PaymentVoucherOneLineItem> PaymentVoucherOneLineItem { get; set; }
        public DbSet<PaymentVoucherOneSubLdgrLine> PaymentVoucherOneSubLdgrLine { get; set; }
        public DbSet<ILCPayment> ILCPayment { get; set; }
        public DbSet<ILCPaymentLineItem> ILCPaymentLineItem { get; set; }
        public DbSet<ILCPSubLdgrLine> ILCPSubLdgrLine { get; set; }
        public DbSet<LCAccBalCalculation> LCAccBalCalculation { get; set; }
        public DbSet<TrusteeBoard> TrusteeBoard { get; set; }
        public DbSet<RebateCondition> RebateCondition { get; set; }
        public DbSet<RebateConditionLine> RebateConditionLine { get; set; }
        public DbSet<LTRA> LTRA { get; set; }
        public DbSet<LTRBalCalculation> LTRBalCalculation { get; set; }
        public DbSet<LTR> LTR { get; set; }
        public DbSet<AITForecastLine> AITForecastLine { get; set; }
        public DbSet<AITForecast> AITForecast { get; set; }
        public DbSet<ChallanPercentageList> ChallanPercentageList { get; set; }
        public DbSet<MonthlyAITDeduction> MonthlyAITDeduction { get; set; }
        public DbSet<MonthlyAITDeductionLine> MonthlyAITDeductionLine { get; set; }
        public DbSet<EmployeeBalanceCalculationTAX> EmployeeBalanceCalculationTAX { get; set; }
        public DbSet<PayBillLine> PayBillLines { get; set; }
        public DbSet<PayBillAdvance> PayBillAdvances { get; set; }
        public DbSet<EPVEntry> EPV { get; set; }
        public DbSet<EPVLineItem> EPVLineItems { get; set; }
        public DbSet<AITChallan> AITChallan { get; set; }
        public DbSet<AITChallanLine> AITChallanLine { get; set; }
        public DbSet<TAXBalanceCalculation> TAXBalanceCalculation { get; set; }
        public DbSet<TaxCalculation> TaxCalculation { get; set; }
        public DbSet<TaxCalculationLine> TaxCalculationLine { get; set; }
        public DbSet<SupplierAcAdjustment> SupplierAcAdjustments { get; set; }
        public DbSet<FGItemRepack> FGItemRepack { get; set; }
        public DbSet<FGItemRepackLineItem> FGItemRepackLineItem { get; set; }
        public DbSet<CustomerBalanceAdjustment> CustomerBalanceAdjustment { get; set; }
        public DbSet<CustomerBalanceAdjustmentLine> CustomerBalanceAdjustmentLine { get; set; }
        public DbSet<SPAdvanceAdjustment> SPAdvanceAdjustments { get; set; }
        public DbSet<SPAdvanceAmountLineItem> SPAdvanceAmountLineItems { get; set; }
        public DbSet<LCCosting> LCCostings { get; set; }
        public DbSet<LCCostingLine> LCCostingLines { get; set; }
        public DbSet<LCCostingQtyBalance> LCCostingQtyBalances { get; set; }
        public DbSet<EmployeeLeaveRecords> EmployeeLeaveRecords { get; set; }
        public DbSet<ILCB> ILCB { get; set; }
        public DbSet<ILCBLine> ILCBLine { get; set; }
        public DbSet<ILCBSubLdgrLine> ILCBSubLdgrLine { get; set; }
        public DbSet<IssueRMToFG> IssueRMToFG { get; set; }
        public DbSet<IssueRMToFGLineItem> IssueRMToFGLineItem { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItem { get; set; }
        public DbSet<ImportItemList> ImportItemList { get; set; }
        public DbSet<WorkerSection> WorkSection { get; set; }
        public DbSet<WorkPlace> WorkPlace { get; set; }
        public DbSet<WorkerDesignation> WorkerDesignation { get; set; }
        public DbSet<WorkerType> WorkerType { get; set; }
        public DbSet<TargetSalesAndCollection> TargetSalesAndCollection { get; set; }
        public DbSet<TargetSalesAndCollectionLineItem> TargetSalesAndCollectionLineItem { get; set; }
        public DbSet<TsoCustomerTravelCheckpoint> TsoCustomerTravelCheckpoints { get; set; }
        public DbSet<CustomerLocation> CustomerLocations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferLineItem> OfferLineItems { get; set; }
        public DbSet<ILCBalCalculations> ILCBalCalculation { get; set; }
        public DbSet<LTRBalCalculations> LTRBalCalculations { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { set; get; }
        public DbSet<AttandanceLog> AttandanceLogs { set; get; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<EmployeeSalaryAddition> EmployeeSalaryAdditions { set; get; }
        public DbSet<EmployeeSalaryAdditionHistory> EmployeeSalaryAdditionHistories { set; get; }
        public DbSet<SalaryStructure> SalaryStructure { get; set; }
        public DbSet<EnterWorkerInformation> EnterWorkerInformations { get; set; }
        public DbSet<JobQuittingInformation> JobQuittingInformations { get; set; }
        public DbSet<SalaryDeduction> SalaryDeductions { get; set; }
        public DbSet<SalaryDeductionHistory> SalaryDeductionHistories { set; get; }
        public DbSet<SalaryInformationLine> SalaryInformationLine { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { set; get; }
        public DbSet<SalaryPaymentLineItem> SalaryPaymentLineItem { get; set; }
        public DbSet<SalaryInformation> SalaryInformation { get; set; }
        public DbSet<CheckInAttandanceLog> CheckInAttandanceLogs { get; set; }
        public DbSet<SalaryIncrement> SalaryIncrement { get; set; }
        public DbSet<EmployeeImageAndAttachment> EmployeeImageAndAttachment { get; set; }
        public DbSet<RemainingStock> RemainingStock { get; set; }
        public DbSet<MoneyRequisitionOB> MoneyRequisitionOB { get; set; }
        public DbSet<ImportLCPayment> ImportLCPaymentD { get; set; }
        public DbSet<EmployeesPromotionHistory> EmployeesPromotionHistory { get; set; }
        public DbSet<MoneyRequisitionRefund> MoneyRequisitionRefund { get; set; }
        public DbSet<EmployeeLeaveRecordLine> EmployeeLeaveRecordLine { get; set; }
        public DbSet<OpeningLeaveBalance> OpeningLeaveBalances { get; set; }
        public DbSet<ImportCostGroup> ImportCostGroups { get; set; }
        public DbSet<LATRP> LATRP { get; set; }
        public DbSet<ILCP> ILCP { get; set; }
        public DbSet<ILCPLine> ILCPLine { get; set; }
        public DbSet<StockAdjustment> StockAdjustment { get; set; }
        public DbSet<StockAdjustmentLineItem> StockAdjustmentLineItems { get; set; }
        public DbSet<StockAdjustmentRM> StockAdjustmentRM { get; set; }
        public DbSet<StockAdjustmentRMLineItem> StockAdjustmentRMLineItem { get; set; }
        public DbSet<BatchBalanceCalculation> BatchBalanceCalculations { get; set; }
        public DbSet<MDSI> MDSI { get; set; }
        public DbSet<MDSILineItem> MDSILineItem { get; set; }
        public DbSet<DealerIncentiveBalanceCalculation> DealerIncentiveBalanceCalculation { get; set; }
        public DbSet<DealerSalesIncentiveAdjustment> DealerSalesIncentiveAdjustment { get; set; }
        public DbSet<DSIAdjustmentLineItem> DSIAdjustmentLineItem { get; set; }
        public DbSet<Commission> Commission { get; set; }
        //public DbSet<IssueFGtoFactoryFG> IssueFGtoFactoryFG { get; set; }
        //public DbSet<IssueFGtoFactoryFGLineItem> IssueFGtoFactoryFGLineItem { get; set; }
        // public DbSet<CashAccountConfig> CashAccountConfig { get; set; }
        public DbSet<SITV> SITV { get; set; }
        public DbSet<SITVLine> SITVLine { get; set; }
        public DbSet<DealerAccAdjustment> DealerAccAdjustment { get; set; }
        public DbSet<DealerAccAdjustmentLineItem> DealerAccAdjustmentLineItem { get; set; }
        public DbSet<BEEERP.Models.Data_Admin.Module> Modules { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenEntryLock> ScreenEntryLocks { get; set; }
        public DbSet<FPBLineFRN> FPBLineFRN { get; set; }
        public DbSet<EmployeePayScale> EmployeePayScale { get; set; }
        public DbSet<CreateSubLedger> CreateSubLedger { get; set; }
        public DbSet<LCPVSubLedgerLine> LCPVSubLedgerLine { get; set; }
        //public DbSet<SparePartsDepartment> SparePartsDepartment { get; set; }
        public DbSet<StorePurchaseRequistion> StorePurchaseRequistion { get; set; }
        public DbSet<StorePurchaseRequisitionLineItem> StorePurchaseRequisitionLineItem { get; set; }
        public DbSet<InventoryOpeningBalance> InventoryOpeningBalance { get; set; }
        public DbSet<InventoryAdditionalInfo> InventoryAdditionalInfo { get; set; }
        public DbSet<CompanyList> CompanyList { get; set; }
        public DbSet<MachineDetails> MachineDetails { get; set; }
        public DbSet<SoftwareList> SoftwareList { get; set; }
        public DbSet<SPDepartment> SPDepartment { get; set; }
        public DbSet<ProductionSections> ProductionSections { get; set; }
        public DbSet<SparePartsItemList> SparePartsItemList { get; set; }
        public DbSet<YearList> YearList { get; set; }
        public DbSet<MachineType> MachineType { get; set; }
        public DbSet<MachineSection> MachineSection { get; set; }
        public DbSet<InventoryOpeningBalanceOld> InventoryOpeningBalanceOld { get; set; }
        public DbSet<SpOpeningBalance> SpOpeningBalance { get; set; }
        public DbSet<GSInventoryIssue> GSInventoryIssue { get; set; }
        public DbSet<GSInventoryIssueLineItem> GSInventoryIssueLineItem { get; set; }
        public DbSet<SPItemType> SPItemType { get; set; }
        public DbSet<SparePartsDepartment> SparePartsDepartment { get; set; }
        public DbSet<SPAdditionalInfo> SPAdditionalInfo { get; set; }
        public DbSet<SPInventoryTransaction> SPInventoryTransaction { get; set; }
        public DbSet<SPBox> SPBox { get; set; }
        public DbSet<IssueSpareParts> IssueSpareParts { get; set; }
        public DbSet<IssueSparePartsLineItem> IssueSparePartsLineItem { get; set; }
        public DbSet<SPIndent> SPIndent { get; set; }
        public DbSet<SPIndentLineItem> SPIndentLineItem { get; set; }
        public DbSet<SPBrand> SPBrand { get; set; }
        public DbSet<SPType> SPType { get; set; }
        public DbSet<WOType> WOTypes { get; set; }
        public DbSet<SparePartsDamage> SparePartsDamage { get; set; }
        public DbSet<SparePartsDamageLineItem> SparePartsDamageLineItem { get; set; }
        public DbSet<MRRGoodsReceiveNote> MRRGoodsReceiveNote { get; set; }
        public DbSet<MRRGoodsReceiveNoteLineItem> MRRGoodsReceiveNoteLineItem { get; set; }
        public DbSet<QualityControl> QualityControl { get; set; }
        public DbSet<QualityControlLineItem> QualityControlLineItem { get; set; }
        public DbSet<LMRGoodsReceiveNote> LMRGoodsReceiveNote { get; set; }
        public DbSet<LMRGoodsReceiveNoteLineItem> LMRGoodsReceiveNoteLineItem { get; set; }
        public DbSet<InventoryTransfer> InventoryTransfer { get; set; }
        public DbSet<InventoryTransferLineItem> InventoryTransferLineItem { get; set; }



    }
}