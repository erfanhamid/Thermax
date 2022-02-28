using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Data_Admin.Report
{
    [ShowNotification]
    public class UserLogReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: UserLogReport
        public ActionResult UserLogReport()
        {
            ViewBag.Employee = LoadComboBox.LoadActiveEmployees();
            return View();
        }

        public ActionResult GetUserWiseLogReport(ReportVModel model)
        {
           // Session["ReportName"] = "MRLedger";
            ReportParmPersister param = new ReportParmPersister();

            var employee = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID);

                param.EmployeeID = employee.Id;
                param.EmployeeName = employee.Name;
                //param.Address = customer.BillTo;
               // param.EmpDesignation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                param.FromDate = model.From;
                param.ToDate = model.To;
                Session["UserLogparam"] = param;

                var items = context.Database.SqlQuery<UserWiseLogReport>(string.Format("exec UserLogReport '" + model.EmployeeID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
                if (items.Count == 0)
                {
                UserWiseLogReport report = new UserWiseLogReport();
                    items.Add(report);
                }
                Session["UserLogData"] = items;
                return Redirect("/CrystalReport/UserWiseLogRpt.aspx");
            
        }
        public ActionResult ShowUserModuelWiseRoleList(ReportVModel model)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            List<UserModuleWiseRoleListReport> list = new List<UserModuleWiseRoleListReport>();

            ReportParmPersister param = new ReportParmPersister();

            List<ApplicationUser> users;
            if (model.EmployeeID != 0)
            {
                var Employee = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID);
                if (Employee != null)
                {
                    users = dbContext.Users.Where(x => x.UserName == Employee.LogInUserName).ToList();
                }
                else
                {
                    users = new List<ApplicationUser>();
                }

            }
            else
            {
                users = dbContext.Users.ToList();
            }

            users.ForEach(x => {

                var modules = context.UserWiseModule.Where(m => m.UserName == x.UserName).ToList();
                var userRoles = x.Roles.ToList();

                modules.ForEach(m => {

                    // For Accounts Module
                    if (m.ModuleName.Contains(nameof(Module.Accounts)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Acc")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For DataAdmin Module
                    if (m.ModuleName.Contains(nameof(Module.DataAdmin)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Data")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For HRAdmin Module
                    if (m.ModuleName.Contains(nameof(Module.HRAdmin)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Hra")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For SystemAdmin Module
                    if (m.ModuleName.Contains(nameof(Module.SystemAdmin)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Sys")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For Sales Module
                    if (m.ModuleName.Contains(nameof(Module.Sales)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Sal")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For Store Module
                    if (m.ModuleName.Contains(nameof(Module.Store)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Inv")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For Production Module
                    if (m.ModuleName.Contains(nameof(Module.Production)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Production")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    // For Procurement Module
                    if (m.ModuleName.Contains(nameof(Module.Procurement)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Procurement")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }

                    ///// This Modules Are Not Included In The Assign Role


                    //// For CostManagement Module
                    //if (m.ModuleName.Contains(nameof(Module.CostManagement)))
                    //{
                    //    var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Cos")).ToList();
                    //    roles.ForEach(r => {

                    //        if (userRoles.Any(ur => ur.RoleId == r.Id))
                    //        {
                    //            list.Add(new UserModuleWiseRoleListReport
                    //            {
                    //                UserName = x.UserName,
                    //                ModuleName = m.ModuleName,
                    //                RoleId = r.Id,
                    //                RoleName = r.Name,
                    //            });
                    //        }

                    //    });
                    //}

                    //// For LC Module
                    //if (m.ModuleName.Contains(nameof(Module.LC)))
                    //{
                    //    var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Lc")).ToList();
                    //    roles.ForEach(r => {

                    //        if (userRoles.Any(ur => ur.RoleId == r.Id))
                    //        {
                    //            list.Add(new UserModuleWiseRoleListReport
                    //            {
                    //                UserName = x.UserName,
                    //                ModuleName = m.ModuleName,
                    //                RoleId = r.Id,
                    //                RoleName = r.Name,
                    //            });
                    //        }

                    //    });
                    //}

                    //// For Purchase Module
                    //if (m.ModuleName.Contains(nameof(Module.Purchase)))
                    //{
                    //    var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Pur")).ToList();
                    //    roles.ForEach(r => {

                    //        if (userRoles.Any(ur => ur.RoleId == r.Id))
                    //        {
                    //            list.Add(new UserModuleWiseRoleListReport
                    //            {
                    //                UserName = x.UserName,
                    //                ModuleName = m.ModuleName,
                    //                RoleId = r.Id,
                    //                RoleName = r.Name,
                    //            });
                    //        }

                    //    });
                    //}

                    //// For Tso Module
                    //if (m.ModuleName.Contains(nameof(Module.Tso)))
                    //{
                    //    var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Tso")).ToList();
                    //    roles.ForEach(r => {

                    //        if (userRoles.Any(ur => ur.RoleId == r.Id))
                    //        {
                    //            list.Add(new UserModuleWiseRoleListReport
                    //            {
                    //                UserName = x.UserName,
                    //                ModuleName = m.ModuleName,
                    //                RoleId = r.Id,
                    //                RoleName = r.Name,
                    //            });
                    //        }

                    //    });
                    //}

                    // For Commercial Module
                    if (m.ModuleName.Contains(nameof(Module.Commercial)))
                    {
                        var roles = dbContext.Roles.Where(i => i.Name.StartsWith("Commercial")).ToList();
                        roles.ForEach(r => {

                            if (userRoles.Any(ur => ur.RoleId == r.Id))
                            {
                                list.Add(new UserModuleWiseRoleListReport
                                {
                                    UserName = x.UserName,
                                    ModuleName = m.ModuleName,
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                });
                            }

                        });
                    }


                });


            });

            Session["UserModuelWiseRoleListData"] = list;
            Session["UserModuelWiseRoleListParam"] = param;

            return Redirect("/CrystalReport/UserModuelWiseRoleListRpt.aspx");
        }
    }
}