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
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {

            var viewModel = new DepartmentViewModel
            {
                Department = null
            };



            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(DepartmentViewModel viewModel)
        {

            var Employees = new RootModel();
            var jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json")); //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);
            jsonFile.Close();

            foreach (var department in Employees.Database.Departments)
            {
                if (department.Name == viewModel.Department.Name)
                {
                    TempData["shortMessage"] = "Department Name Already Exist";
                    return RedirectToAction("Index", "Home");
                }
                if (department.Did == viewModel.Department.Did)
                {
                    TempData["shortMessage"] = "Department ID Already Exist";
                    return RedirectToAction("Index", "Home");
                }
            }

            viewModel.Department.Employees = new List<Employee>();


            Employees.Database.Departments.Add(viewModel.Department);


            jsonString = JsonConvert.SerializeObject(Employees);
            System.IO.File.WriteAllText(Server.MapPath("~/Models/DataBase.json"), jsonString);
            return RedirectToAction("Index", "Home");
        }
    }
}