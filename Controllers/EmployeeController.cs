using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            var Employees = new RootModel();
            StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);
            jsonFile.Close();


            //EMPLOYEE VIEWMODEL
            Employee curEmp = new Employee();
            if (Did != null)
            {
                foreach (var department in Employees.Database.Departments)
                {
                    if (department.Did==Did)
                    {
                        foreach (var employee in department.Employees)
                        {
                            if (employee.Eid==Eid)
                            {
                                curEmp = employee;
                                curEmp.Did = Did;
                            }
                        }
                    }
                    
                }
            }
            else
            {
                curEmp = null;
            }
            var viewModel = new EmployeeViewModel()
            {
                Employee = curEmp,
            };

            //DEPARTMENT LIST VIEWBAG
            var departmentList = new Dictionary<string, string>();
            foreach (var department in Employees.Database.Departments)
            {
                departmentList.Add(department.Did,department.Name);
            }


            ViewBag.DepartmentList = departmentList;
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            var Employees = new RootModel();
                StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
                var jsonString = jsonFile.ReadToEnd();
                Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);

                //var newEmp = new Employee();
                //newEmp = viewModel.Employee;
                jsonFile.Close();

                Employees.Database.Departments[(Int32.Parse(viewModel.Employee.Did)-1)].Employees.Add(viewModel.Employee);

                jsonString = JsonConvert.SerializeObject(Employees);
                System.IO.File.WriteAllText(Server.MapPath("~/Models/DataBase.json"), jsonString);

                return RedirectToAction("Index", "Home");
        }


    }
}
