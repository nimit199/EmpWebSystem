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
using WebGrease.Css.Extensions;


namespace EmpWebSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Employees = new DatabaseList();
            StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<DatabaseList>(jsonString);
            jsonFile.Close();

            var viewModel = new DatabaseViewModel
            {
                DatabaseList = Employees,
            };

            return View(viewModel);
        }





        [Route("Home/EmpPage/{dept}/{id}")]
        [Route("Home/EmpPage")]
        public ActionResult EmpPage(int id = -1, int dept = 0)
        {
            var Employees = new DatabaseList();
            StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<DatabaseList>(jsonString);

            jsonFile.Close();

            List<SelectListItem> departmentList= new List<SelectListItem>();

            foreach (var department in Employees.DataBase.Departments)
                departmentList.Add(new SelectListItem() { Text = department.Name, Value = department.id});
            
            var viewModel = new DatabaseViewModel
            {
                DatabaseList = Employees,
                id=id,
                dept = dept,
                DepartmentList = departmentList,
            };

            ViewBag.Message = "Your application description page.";

            return View(viewModel);
        }





        [HttpPost]
        public ActionResult Create(DatabaseViewModel viewModel)
        {
            var Employees = new DatabaseList();
            StreamWriter jsonFile = new StreamWriter(Server.MapPath("~/Models/DataBase.json"));
            Employees = viewModel.DatabaseList;
            var jsonString = JsonConvert.SerializeObject(Employees);

            

            jsonFile.WriteLine(jsonString);
            jsonFile.Close();

            return RedirectToAction("Index","Home");

        }






        public ActionResult Department()
        {
            var Employees = new DatabaseList();
            StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<DatabaseList>(jsonString);
            jsonFile.Close();
            ViewBag.Message = "Your contact page.";

            var viewModel = new DatabaseViewModel
            {
                DatabaseList = Employees,
            };

            return View(viewModel);
        }
    }
}