using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public class GenerateId
    {
        public int GenerateZoneId(BEEContext context)
        {
            var zone= context.TblZones.ToList().OrderBy(x => x.ZoneId).LastOrDefault();
            if(zone!=null)
            {
                return zone.ZoneId + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateWDID(BEEContext context)
        {
            var Work = context.WorkerDesignation.ToList().OrderBy(x => x.WDesignationNo).LastOrDefault();
            if (Work != null)
            {
                return Work.WDesignationNo + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateWorkSectionId(BEEContext context)
        {
            var Work = context.WorkSection.ToList().OrderBy(x => x.ProdSectID).LastOrDefault();
            if (Work != null)
            {
                return Work.ProdSectID + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GetWTID(BEEContext context)
        {
            var Work = context.WorkerType.ToList().OrderBy(x => x.WTNo).LastOrDefault();
            if (Work != null)
            {
                return Work.WTNo + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GetWPID(BEEContext context)
        {
            var workPlace = context.WorkPlace.ToList().OrderBy(x => x.WorkPlaceNo).LastOrDefault();

            if (workPlace != null)
            {
                return workPlace.WorkPlaceNo + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateDepartmentId(BEEContext context)
        {
            var department = context.Department.ToList().OrderBy(x => x.DeptmentID).LastOrDefault();
            if (department != null)
            {
                return department.DeptmentID + 1; 
            }
            else
            {
                return 1;
            }
        }
        public int GenerateSPDepartmentId(BEEContext context)
        {
            var department = context.SPDepartment.ToList().OrderBy(x => x.SPDID).LastOrDefault();
            if (department != null)
            {
                return department.SPDID + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateMachineSectionId(BEEContext context)
        {
            var section = context.MachineSection.ToList().OrderBy(x => x.MSID).LastOrDefault();
            if (section != null)
            {
                return section.MSID + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GeneratSoftwareId(BEEContext context)
        {
            var section = context.SoftwareList.ToList().OrderBy(x => x.SoftwareID).LastOrDefault();
            if (section != null)
            {
                return section.SoftwareID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateMachTypeId(BEEContext context)
        {
            var section = context.MachineType.ToList().OrderBy(x => x.MTID).LastOrDefault();
            if (section != null)
            {
                return section.MTID + 1;
            }
            else
            {
                return 1;
            }
        }


        public int GenerateGradeId(BEEContext context)
        {
            var grade = context.Grade.ToList().OrderBy(x => x.GradeID).LastOrDefault();
            if (grade != null)
            {
                return grade.GradeID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateDesignationId(BEEContext context)
        {
            var designation = context.Designation.ToList().OrderBy(x => x.Id).LastOrDefault();

            if (designation != null)
            {
                return designation.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateFunctionalDesignationId(BEEContext context)  
        {
            var funcdesignation = context.FunctionalDesignation.ToList().OrderBy(x => x.ID).LastOrDefault();

            if (funcdesignation != null)
            {
                return funcdesignation.ID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateEmployeeSectionId(BEEContext context)    
        {
            var employeeSection = context.EmployeeSection.ToList().OrderBy(x => x.ID).LastOrDefault();

            if (employeeSection != null)
            {
                return employeeSection.ID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GeneratedHightEduId(BEEContext context)
        {
            var highEdu = context.HighestEducation.ToList().OrderBy(x => x.ID).LastOrDefault();

            if (highEdu != null)
            {
                return highEdu.ID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GeneratedLeaveTypeId(BEEContext context) 
        {
            var levTyp = context.LeaveType.ToList().OrderBy(x => x.slno).LastOrDefault();

            if (levTyp != null)
            {
                return levTyp.slno + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GeneratedReasonOfLevId(BEEContext context)   
        {
            var resonOfLev = context.ReasonOfLeaving.ToList().OrderBy(x => x.RoLID).LastOrDefault();

            if (resonOfLev != null)
            {
                return resonOfLev.RoLID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateInventoryId(BEEContext context)  
        {
            var inv = context.ChartOfInventory.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (inv != null)
            {
                return inv.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateStoreId(BEEContext context)  
        {
            var store = context.Store.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (store != null)
            {
                return store.Id + 1;
            }
            else
            {
                return 1;
            }
        }
        // to generate Sub Ledger ID
        public int GenerateSubLedgerID(BEEContext context)
        {
            var slid = context.CreateSubLedger.ToList().OrderBy(x => x.SubLedgerID).LastOrDefault();
            if (slid != null)
            {
                return slid.SubLedgerID + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GenerateDepotId(BEEContext context)  
        {
            var depot = context.BranchInformation.ToList().OrderBy(x => x.Slno).LastOrDefault();  
            if (depot != null)
            {
                return depot.Slno + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateUoMId(BEEContext context)
        {
            var uom = context.UOM.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (uom != null)
            {
                return uom.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        //public int GenerateChallamNo(BEEContext context)    
        //{
        //    var challan = context.SIFD.ToList().OrderBy(x => x.ChallanNo).LastOrDefault();
        //    if (challan != null)
        //    {
        //        return challan.ChallanNo + 1;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        public int GenerateHolidayID(BEEContext context)
        {
            var id = context.Holidays.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (id != null)
            {
                return id.ID + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GenerateSalaryStructureID(BEEContext context)
        {
            var id = context.SalaryStructure.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (id != null)
            {
                return id.ID + 1;
            }
            else
            {
                return 1;
            }
        }

    }
}