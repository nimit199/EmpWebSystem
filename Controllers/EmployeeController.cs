using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using EmpWebSystem.Models;
using EmpWebSystem.ViewModel;
using Newtonsoft.Json;

namespace EmpWebSystem.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Route("Employee/{dept}/{id}")]
        [Route("Employee/Index")]
        [Route("Employee")]
        [Route("Home/Employee/{dept}/{id}")]
        [Route("Home/Employee")]
        public ActionResult Index(string Did = null, string Eid = null)
        {
            ViewBag.Edit = new bool();
            var Employees = new RootModel();
            var jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json")); //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);
            jsonFile.Close();


            //EMPLOYEE VIEWMODEL
            var curEmp = new Employee();
            if (Did != null || Eid != null)
            {
                foreach (var department in Employees.Database.Departments)
                {
//                    if (department.Employees == null)
//                    {
//                        continue;
//                    }

                    if (department.Did == Did)
                        foreach (var employee in department.Employees)
                            if (employee.Eid == Eid)
                            {
                                curEmp = employee;
                                curEmp.Did = Did;
                            }
                }
            }
            else
            {
                curEmp = null;
            }


            var viewModel = new EmployeeViewModel
            {
                Employee = curEmp
            };
            if (curEmp == null)
                viewModel.IsEdit = false;
            else
                viewModel.IsEdit = true;

            //DEPARTMENT LIST VIEWBAG
            var departmentList = new Dictionary<string, string>();
            foreach (var department in Employees.Database.Departments)
                departmentList.Add(department.Did, department.Name);


            ViewBag.DepartmentList = departmentList;
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            var Employees = new RootModel();
            var jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json")); //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);
            jsonFile.Close();
            //var newEmp = new Employee();
            //newEmp = viewModel.Employee;


            var flag = 0;

            if (viewModel.IsEdit == true)
            {
                foreach (var department in Employees.Database.Departments.ToArray())
                {
//                    if (department.Employees == null)
//                    {
//                        continue;
//                    }

                    foreach (var employee in department.Employees.ToArray())
                    {
                        if (employee.Eid == viewModel.Employee.Eid && department.Did == viewModel.Employee.Did)
                        {
                            employee.Name = viewModel.Employee.Name;
                            employee.Salary = viewModel.Employee.Salary;
                            jsonString = JsonConvert.SerializeObject(Employees);

                            System.IO.File.WriteAllText(Server.MapPath("~/Models/DataBase.json"), jsonString);
                            return RedirectToAction("Index", "Home");
                        }

                        if (employee.Eid == viewModel.Employee.Eid && employee.Did != viewModel.Employee.Did)
                        {
                            department.Employees.Remove(employee);
                            flag = -1;
                            break;
                        }
                    }
                }

                TempData["shortMessage"] = null;
            }


            if (flag != 1)
            {
                foreach (var department in Employees.Database.Departments.ToArray())
                {
//                    if (department.Employees == null)
//                    {
//                        continue;
//                    }


                    foreach (var employee in department.Employees.ToArray())
                    {
                        if (employee.Eid == viewModel.Employee.Eid)
                        {
                            TempData["shortMessage"] = "Employee ID Already Exist";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["shortMessage"] = null;
                        }
                    }
                }


                Employees.Database.Departments[int.Parse(viewModel.Employee.Did) - 1].Employees.Add(viewModel.Employee);
            }


            jsonString = JsonConvert.SerializeObject(Employees);
            System.IO.File.WriteAllText(Server.MapPath("~/Models/DataBase.json"), jsonString);
            return RedirectToAction("Index", "Home");
        }
    }
}