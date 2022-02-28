using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.HRModule;
using BEEERP.Models.Database;
using BEEERP.Models.Bridge;
using BEEERP.Models.Payroll;
using Microsoft.AspNet.Identity;
using BEEERP.Models.Log;
using System.IO;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class EmployeeController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Employee
        public ActionResult Employee()
        {
            //var lastemployee = context.Employees.Max(x=>x.Id);

            int EmployeeID = 0;
            var CustId = context.Employees.Count();
            if (CustId == 0)
            {
                EmployeeID = 1001;
            }
            else
            {
                var maxempid = context.Employees.Max(x => x.Id);

                EmployeeID = maxempid + 1;
            }




            ViewBag.EmployeeId = EmployeeID;//lastemployee.Max(x=>x.Id) + 1;
            ViewBag.Gender = LoadComboBox.LoadAllGender();
            ViewBag.Religion = LoadComboBox.LoadAllReligion();
            ViewBag.MaritualStatus = LoadComboBox.LoadAllMaritalStatus();
            ViewBag.Branch = LoadComboBox.LoadBranchInfo();
            ViewBag.Designation = LoadComboBox.LoadAllDesignation();
            ViewBag.Department = LoadComboBox.LoadAllDepartment();
            ViewBag.FuncDesignation = LoadComboBox.LoadAllFuncDesignation();
            ViewBag.Section = LoadComboBox.LoadAllSection();
            ViewBag.StaffType = LoadComboBox.LoadAllStaffType();
            ViewBag.Education = LoadComboBox.LoadAllEducation();
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
            ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();
            ViewBag.CompanyInfo = LoadComboBox.LoadCompanyInformation();
            ViewBag.GradeList = LoadComboBox.LoadGradeList();
            ViewBag.EmpCheck = LoadComboBox.LoadEmpType();
            return View();
        }
        
        public ActionResult SaveEmployee(Employee emp)  
        {


            var empExist = context.Employees.FirstOrDefault(x => x.Id == emp.Id);

            if (empExist == null)
            {
                emp.LogInUserName = string.Empty;
                emp.Id = emp.Id;
                context.Employees.Add(emp);

                //EmployeeBalance employeeBalance = new EmployeeBalance();
                //employeeBalance.EmployeeID = emp.Id;
                //employeeBalance.TDate = emp.JoiningDate;
                //employeeBalance.DocumentType = "EOB";
                //employeeBalance.AccountID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "OBE").RefValue;
                //employeeBalance.TRDescription = "OB";
                //employeeBalance.RefNo = "OB";
                //employeeBalance.Amount = Convert.ToDecimal(emp.OpBalance);
                //employeeBalance.DocNO = emp.Id;
                //context.EmployeeBalances.Add(employeeBalance);
                //context.SaveChanges();
                    
                //AccountModuleBridge.InsertFromEmployee(ref context, emp);
                //EmployeeCalculationBridge.InsertFromEmployee(ref context, emp);
                UserLog.SaveUserLog(ref context, new UserLog(emp.Id.ToString(), DateTime.Now, "Employee", ScreenAction.Save));
                context.SaveChanges();

                return Json(new { id = emp.Id }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Gender = LoadComboBox.LoadAllGender();
                ViewBag.Religion = LoadComboBox.LoadAllReligion();
                ViewBag.MaritualStatus = LoadComboBox.LoadAllMaritalStatus();
                ViewBag.Branch = LoadComboBox.LoadBranchInfo();
                ViewBag.Designation = LoadComboBox.LoadAllDesignation();
                ViewBag.Department = LoadComboBox.LoadAllDepartment();
                ViewBag.FuncDesignation = LoadComboBox.LoadAllFuncDesignation();
                ViewBag.Section = LoadComboBox.LoadAllSection();
                ViewBag.StaffType = LoadComboBox.LoadAllStaffType();
                ViewBag.Education = LoadComboBox.LoadAllEducation();
                ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
                ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();

                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SearchEmployeeById(int empId)
        {
            var UserId = User.Identity.GetUserId();
            var empExist = context.Employees.FirstOrDefault(x => x.Id == empId);

            if (empExist == null)
            {
                var lastemployee = context.Employees.Max(x => x.Id);
                ViewBag.EmployeeId = lastemployee + 1;
                ViewBag.Gender = LoadComboBox.LoadAllGender();
                ViewBag.Religion = LoadComboBox.LoadAllReligion();
                ViewBag.MaritualStatus = LoadComboBox.LoadAllMaritalStatus();
                ViewBag.Branch = LoadComboBox.LoadBranchInfo();
                ViewBag.Designation = LoadComboBox.LoadAllDesignation();
                ViewBag.Department = LoadComboBox.LoadAllDepartment();
                ViewBag.FuncDesignation = LoadComboBox.LoadAllFuncDesignation();
                ViewBag.Section = LoadComboBox.LoadAllSection();
                ViewBag.StaffType = LoadComboBox.LoadAllStaffType();
                ViewBag.Education = LoadComboBox.LoadAllEducation();
                ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
                ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();
                ViewBag.CompanyInfo = LoadComboBox.LoadCompanyInformation();
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var IsUpdate = 1;
                //var Permission = context.UserActionPermission.Where(x => x.UserId == UserId && x.ScreenName == "Employee" && x.Type == "EOIU").ToList();
                //if (Permission.Count() > 0)
                //{
                //    IsUpdate = 1;
                //}
                //else
                //{
                //    IsUpdate = 0;
                //}
                var mployeeImageAndAttachment = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                if (mployeeImageAndAttachment != null)
                {
                    string pathFPro = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.EmployeeImageName + ".png");
                    var imagePathe = "";
                    FileInfo previmage = new FileInfo(pathFPro);
                    if (previmage.Exists)
                    {
                        imagePathe = "/Image/Employees/" + mployeeImageAndAttachment.EmployeeImageName + ".png";
                    }
                    else
                    {
                        imagePathe = "/Image/Employees/profile-default.png";
                    }

                    string pathFA1 = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.Attachment1 + ".png");
                    var attaPath1 = "";
                    FileInfo preva1 = new FileInfo(pathFA1);
                    if (preva1.Exists)
                    {
                        attaPath1 = "/Image/Employees/" + mployeeImageAndAttachment.Attachment1 + ".png";
                    }
                    else
                    {
                        attaPath1 = "/Image/Employees/NotAttasted.jpg";
                    }
                    string pathFA2 = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.Attachment2 + ".png");
                    var attaPath2 = "";
                    FileInfo preva2 = new FileInfo(pathFA2);
                    if (preva2.Exists)
                    {
                        attaPath2 = "/Image/Employees/" + mployeeImageAndAttachment.Attachment2 + ".png";
                    }
                    else
                    {
                        attaPath2 = "/Image/Employees/NotAttasted.jpg";
                    }
                    string pathFA3 = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.Attachment3 + ".png");
                    var attaPath3 = "";
                    FileInfo preva3 = new FileInfo(pathFA3);
                    if (preva3.Exists)
                    {
                        attaPath3 = "/Image/Employees/" + mployeeImageAndAttachment.Attachment3 + ".png";
                    }
                    else
                    {
                        attaPath3 = "/Image/Employees/NotAttasted.jpg";
                    }
                    string pathFA4 = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.Attachment4 + ".png");
                    var attaPath4 = "";
                    FileInfo preva4 = new FileInfo(pathFA4);
                    if (preva4.Exists)
                    {
                        attaPath4 = "/Image/Employees/" + mployeeImageAndAttachment.Attachment4 + ".png";
                    }
                    else
                    {
                        attaPath4 = "/Image/Employees/NotAttasted.jpg";
                    }
                    string pathFA5 = System.IO.Path.Combine(Server.MapPath("~/Image/Employees/"), mployeeImageAndAttachment.Attachment5 + ".png");
                    var attaPath5 = "";
                    FileInfo preva5 = new FileInfo(pathFA5);
                    if (preva5.Exists)
                    {
                        attaPath5 = "/Image/Employees/" + mployeeImageAndAttachment.Attachment5 + ".png";
                    }
                    else
                    {
                        attaPath5 = "/Image/Employees/NotAttasted.jpg";
                    }
                    var emp = new { attaPath1 = attaPath1, attaPath2 = attaPath2, attaPath3 = attaPath3, attaPath4 = attaPath4, attaPath5 = attaPath5, imagePathe = imagePathe, Id = empExist.Id, Name = empExist.Name,  FatherName = empExist.FatherName, MotherName = empExist.MotherName, PresentAddress = empExist.PresentAddress, PermanentAddress = empExist.PermanentAddress, Mobile = empExist.Mobile, Email = empExist.Email,  DateOfBirth = empExist.DateOfBirth.ToString("dd-MM-yyyy"), JoiningDate = empExist.JoiningDate.ToString("dd-MM-yyyy"), ConfirmationDate = empExist.ConfirmationDate, Designation = empExist.Designation, HighestEducation = empExist.HighestEducation,  DepatmentID = empExist.DepatmentID,  AlternativeContct = empExist.AlternativeContct, PersonalEmail = empExist.PersonalEmail,   Section = empExist.Section, BranchID = empExist.BranchID,  CompanyId = empExist.CompanyId, IsUpdate = IsUpdate,Status=empExist.Status };
                    return Json(emp, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var imagePathe = "/Image/Employees/profile-default.png";
                    var attaPath1 = "/Image/Employees/NotAttasted.jpg";
                    var attaPath2 = "/Image/Employees/NotAttasted.jpg";
                    var attaPath3 = "/Image/Employees/NotAttasted.jpg";
                    var attaPath4 = "/Image/Employees/NotAttasted.jpg";
                    var attaPath5 = "/Image/Employees/NotAttasted.jpg";
                    var emp = new { attaPath1 = attaPath1, attaPath2 = attaPath2, attaPath3 = attaPath3, attaPath4 = attaPath4, attaPath5 = attaPath5, imagePathe = imagePathe, Id = empExist.Id, Name = empExist.Name,   FatherName = empExist.FatherName, MotherName = empExist.MotherName,  PresentAddress = empExist.PresentAddress, PermanentAddress = empExist.PermanentAddress, Mobile = empExist.Mobile, Email = empExist.Email,  DateOfBirth = empExist.DateOfBirth.ToString("dd-MM-yyyy"), JoiningDate = empExist.JoiningDate.ToString("dd-MM-yyyy"), ConfirmationDate = empExist.ConfirmationDate, Designation = empExist.Designation, HighestEducation = empExist.HighestEducation,  DepatmentID = empExist.DepatmentID, AlternativeContct = empExist.AlternativeContct, PersonalEmail = empExist.PersonalEmail,  Section = empExist.Section, BranchID = empExist.BranchID,  CompanyId = empExist.CompanyId, IsUpdate = IsUpdate,  Status = empExist.Status };
                    return Json(emp, JsonRequestBehavior.AllowGet);
                }
            }
        }
        
        public ActionResult DeleteEmpById(int id)
        {
            var emp = context.Employees.Find(id);

            if (emp != null)
            {
                context.Employees.Remove(emp);
                UserLog.SaveUserLog(ref context, new UserLog(emp.Id.ToString(), DateTime.Now, "Employee", ScreenAction.Delete));
                context.SaveChanges();
                return Json(new { Id = emp.Id}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lastemployee = context.Employees.Max(x => x.Id);
                ViewBag.EmployeeId = lastemployee + 1;
                ViewBag.Gender = LoadComboBox.LoadAllGender();
                ViewBag.Religion = LoadComboBox.LoadAllReligion();
                ViewBag.MaritualStatus = LoadComboBox.LoadAllMaritalStatus();
                ViewBag.Branch = LoadComboBox.LoadBranchInfo();
                ViewBag.Designation = LoadComboBox.LoadAllDesignation();
                ViewBag.Department = LoadComboBox.LoadAllDepartment();
                ViewBag.FuncDesignation = LoadComboBox.LoadAllFuncDesignation();
                ViewBag.Section = LoadComboBox.LoadAllSection();
                ViewBag.StaffType = LoadComboBox.LoadAllStaffType();
                ViewBag.Education = LoadComboBox.LoadAllEducation();
                ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
                ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        
        public ActionResult UpdateEmployee(Employee emp)    
        {
            Employee empExists = context.Employees.Find(emp.Id);

            if (empExists != null)
            {
                //context.Employees.Remove(empExists);
                emp.LogInUserName = empExists.LogInUserName;
                //context.Employees.Add(emp);
                empExists.Name = emp.Name;
                empExists.FatherName = emp.FatherName;
                empExists.MotherName = emp.MotherName;
                empExists.PresentAddress = emp.PresentAddress;
                empExists.PermanentAddress = emp.PermanentAddress;
                empExists.Mobile = emp.Mobile;
                empExists.Email = emp.Email;
                empExists.DateOfBirth = emp.DateOfBirth;
                empExists.JoiningDate = emp.JoiningDate;
                empExists.ConfirmationDate = emp.ConfirmationDate;
                empExists.Designation = emp.Designation;
                empExists.HighestEducation = emp.HighestEducation;
                empExists.DepatmentID = emp.DepatmentID;
                empExists.AlternativeContct = emp.AlternativeContct;
                empExists.PersonalEmail = emp.PersonalEmail;
                empExists.Section = emp.Section;
                empExists.BranchID = emp.BranchID;
                empExists.CompanyId = emp.CompanyId;
                UserLog.SaveUserLog(ref context, new UserLog(emp.Id.ToString(), DateTime.Now, "Employee", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { id = emp.Id }, JsonRequestBehavior.AllowGet);
            }
            else {
                var lastemployee = context.Employees.Max(x => x.Id);
                ViewBag.EmployeeId = lastemployee + 1;
                ViewBag.Gender = LoadComboBox.LoadAllGender();
                ViewBag.Religion = LoadComboBox.LoadAllReligion();
                ViewBag.MaritualStatus = LoadComboBox.LoadAllMaritalStatus();
                ViewBag.Branch = LoadComboBox.LoadBranchInfo();
                ViewBag.Designation = LoadComboBox.LoadAllDesignation();
                ViewBag.Department = LoadComboBox.LoadAllDepartment();
                ViewBag.FuncDesignation = LoadComboBox.LoadAllFuncDesignation();
                ViewBag.Section = LoadComboBox.LoadAllSection();
                ViewBag.StaffType = LoadComboBox.LoadAllStaffType();
                ViewBag.Education = LoadComboBox.LoadAllEducation();

                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult Rsm()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.Rsm = LoadComboBox.Rsm(depot);
            ViewBag.TSO = LoadComboBox.LoadAllTSO();
            //var items=from p in context.TsoRsm join c in context.Employees on p.equals
            var items = new List<TsoRsm>();
            var tso = context.TsoRsm.ToList();
            var newItems = new List<TsoRsm>();
            (from p in tso from c in context.Employees where(p.TsoId==c.Id)
             select new {TsoId=c.Id,TsoName=c.Name,RsmId=p.RsmId,RsmName="" }).ToList().ForEach(x=> { items.Add(new TsoRsm() { TsoId = x.TsoId, TsoName = x.TsoName, RsmId = x.RsmId,RsmName="" }); });
            (from p in items from c in context.Employees where (p.RsmId == c.Id) select new { TsoId = p.TsoId, TsoName = p.TsoName, RsmId = c.Id, RsmName = c.Name }).ToList().ForEach(x=> { newItems.Add(new TsoRsm() { TsoId = x.TsoId, TsoName = x.TsoName, RsmId = x.RsmId, RsmName = x.RsmName}); });

            ViewBag.Items = newItems;
            return View();
        }
        public ActionResult SaveRsm(TsoRsm tsoRsm)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var isExist = context.TsoRsm.FirstOrDefault(x=>x.TsoId==tsoRsm.TsoId&&x.RsmId==tsoRsm.RsmId);
                    if(isExist==null)
                    {
                        context.TsoRsm.Add(tsoRsm);
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateRsm(TsoRsm tsoRsm,List<int> prevData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int pTsoId = prevData[0];
                    int pRsmId = prevData[1];
                    var delete = context.TsoRsm.FirstOrDefault(x=>x.TsoId== pTsoId && x.RsmId== pRsmId);
                    
                    if(delete!=null)
                    {
                        context.TsoRsm.Remove(delete);
                    }
                    var isExist = context.TsoRsm.FirstOrDefault(x => x.TsoId == tsoRsm.TsoId && x.RsmId == tsoRsm.RsmId);
                    if (isExist == null)
                    {
                        context.TsoRsm.Add(tsoRsm);
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(3, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteRsm(TsoRsm tsoRsm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var isExist = context.TsoRsm.FirstOrDefault(x => x.TsoId == tsoRsm.TsoId && x.RsmId == tsoRsm.RsmId);
                    if (isExist != null)
                    {
                        context.TsoRsm.Remove(isExist);
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(3, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SubmitImage(EmployeeImageAndAttachment data)
        {

            HttpPostedFileBase userImage = data.employeeImage;
            int empId = data.EmployeeId;
            string error = "";
            if (userImage != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpImage");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userImage.FileName).ToUpper();
                data.EmployeeImageName = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userImage.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userImage.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId && m.EmployeeImageName == pic);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = data.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = prevEmpImage.Attachment1;
                            employeeImageAndAttachment.Attachment2 = prevEmpImage.Attachment2;
                            employeeImageAndAttachment.Attachment3 = prevEmpImage.Attachment3;
                            employeeImageAndAttachment.Attachment4 = prevEmpImage.Attachment4;
                            employeeImageAndAttachment.Attachment5 = prevEmpImage.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }

                        var ImagePathe = "/Image/Employees/" + pic + ".png";
                        return Json(new { ImagePathe = ImagePathe, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SubmitAttachement1(EmployeeImageAndAttachment data)
        {
            HttpPostedFileBase userAttach1 = data.AttachmentImage1;
            int empId = data.EmployeeId;
            string error = "";
            if (userAttach1 != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpAttachment1");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userAttach1.FileName).ToUpper();
                data.Attachment1 = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userAttach1.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userAttach1.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = prevEmpImage.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = data.Attachment1;
                            employeeImageAndAttachment.Attachment2 = prevEmpImage.Attachment2;
                            employeeImageAndAttachment.Attachment3 = prevEmpImage.Attachment3;
                            employeeImageAndAttachment.Attachment4 = prevEmpImage.Attachment4;
                            employeeImageAndAttachment.Attachment5 = prevEmpImage.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }
                        var attachpath1 = "/Image/Employees/" + pic + ".png";
                        return Json(new { attachpath1 = attachpath1, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SubmitAttachement2(EmployeeImageAndAttachment data)
        {
            HttpPostedFileBase userAttach2 = data.AttachmentImage2;
            int empId = data.EmployeeId;
            string error = "";
            if (userAttach2 != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpAttachment2");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userAttach2.FileName).ToUpper();
                data.Attachment2 = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userAttach2.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userAttach2.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = prevEmpImage.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = prevEmpImage.Attachment1;
                            employeeImageAndAttachment.Attachment2 = data.Attachment2;
                            employeeImageAndAttachment.Attachment3 = prevEmpImage.Attachment3;
                            employeeImageAndAttachment.Attachment4 = prevEmpImage.Attachment4;
                            employeeImageAndAttachment.Attachment5 = prevEmpImage.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }
                        var attachpath2 = "/Image/Employees/" + pic + ".png";
                        return Json(new { attachpath2 = attachpath2, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SubmitAttachement3(EmployeeImageAndAttachment data)
        {
            HttpPostedFileBase userAttach3 = data.AttachmentImage3;
            int empId = data.EmployeeId;
            string error = "";
            if (userAttach3 != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpAttachment3");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userAttach3.FileName).ToUpper();
                data.Attachment3 = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userAttach3.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userAttach3.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = prevEmpImage.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = prevEmpImage.Attachment1;
                            employeeImageAndAttachment.Attachment2 = prevEmpImage.Attachment2;
                            employeeImageAndAttachment.Attachment3 = data.Attachment3;
                            employeeImageAndAttachment.Attachment4 = prevEmpImage.Attachment4;
                            employeeImageAndAttachment.Attachment5 = prevEmpImage.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }
                        var attachpath3 = "/Image/Employees/" + pic + ".png";
                        return Json(new { attachpath3 = attachpath3, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SubmitAttachement4(EmployeeImageAndAttachment data)
        {
            HttpPostedFileBase userAttach4 = data.AttachmentImage4;
            int empId = data.EmployeeId;
            string error = "";
            if (userAttach4 != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpAttachment4");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userAttach4.FileName).ToUpper();
                data.Attachment4 = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userAttach4.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userAttach4.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = prevEmpImage.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = prevEmpImage.Attachment1;
                            employeeImageAndAttachment.Attachment2 = prevEmpImage.Attachment2;
                            employeeImageAndAttachment.Attachment3 = prevEmpImage.Attachment3;
                            employeeImageAndAttachment.Attachment4 = data.Attachment4;
                            employeeImageAndAttachment.Attachment5 = prevEmpImage.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }
                        var attachpath4 = "/Image/Employees/" + pic + ".png";
                        return Json(new { attachpath4 = attachpath4, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SubmitAttachement5(EmployeeImageAndAttachment data)
        {
            HttpPostedFileBase userAttach5 = data.AttachmentImage5;
            int empId = data.EmployeeId;
            string error = "";
            if (userAttach5 != null)
            {
                string pic = System.IO.Path.GetFileName(empId.ToString() + "EmpAttachment5");
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Image/Employees/"), pic + ".png");
                string extension = Path.GetExtension(userAttach5.FileName).ToUpper();
                data.Attachment5 = pic;
                if (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
                {
                    if (userAttach5.ContentLength <= 500000)
                    {
                        FileInfo previmage = new FileInfo(path);

                        if (previmage.Exists)
                        {
                            previmage.Delete();
                        }
                        userAttach5.SaveAs(path);
                        var prevEmpImage = context.EmployeeImageAndAttachment.FirstOrDefault(m => m.EmployeeId == empId);
                        if (prevEmpImage == null)
                        {
                            context.EmployeeImageAndAttachment.Add(data);
                            context.SaveChanges();
                        }
                        else
                        {
                            EmployeeImageAndAttachment employeeImageAndAttachment = new EmployeeImageAndAttachment();
                            employeeImageAndAttachment.EmployeeId = prevEmpImage.EmployeeId;
                            employeeImageAndAttachment.EmployeeImageName = prevEmpImage.EmployeeImageName;
                            employeeImageAndAttachment.Attachment1 = prevEmpImage.Attachment1;
                            employeeImageAndAttachment.Attachment2 = prevEmpImage.Attachment2;
                            employeeImageAndAttachment.Attachment3 = prevEmpImage.Attachment3;
                            employeeImageAndAttachment.Attachment4 = prevEmpImage.Attachment4;
                            employeeImageAndAttachment.Attachment5 = data.Attachment5;
                            context.EmployeeImageAndAttachment.Remove(prevEmpImage);
                            context.EmployeeImageAndAttachment.Add(employeeImageAndAttachment);
                            context.SaveChanges();
                        }
                        var attachpath5 = "/Image/Employees/" + pic + ".png";
                        return Json(new { attachpath5 = attachpath5, error = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        error = "Only 500KB size is allowed to upload";
                        return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    error = "Only .jpg/.jpeg/.png file is allowed to upload";
                    return Json(new { error = error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                error = "Please, select a image first ";
                return Json(new { error = error }, JsonRequestBehavior.AllowGet);
            }

        }

    }

}