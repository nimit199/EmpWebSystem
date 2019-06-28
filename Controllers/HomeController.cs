using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EmpWebSystem.Models;
using EmpWebSystem.ViewModel;
using Newtonsoft.Json;


namespace EmpWebSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Employees = new Employee();

            StreamReader jsonFile = new StreamReader("DataBase.json");
            var jsonString = jsonFile.ReadToEnd();

            Employees = JsonConvert.DeserializeObject<Employee>(jsonString);

            var viewModel = new DatabaseViewModel
            {
                Employees = Employees,
            };

            return View(viewModel);
        }

        public ActionResult Employee()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Department()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}