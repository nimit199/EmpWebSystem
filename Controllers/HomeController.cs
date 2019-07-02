using System.IO;
using System.Web.Mvc;
using EmpWebSystem.Models;
using EmpWebSystem.ViewModel;
using Newtonsoft.Json;


namespace EmpWebSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Employees = new RootModel();
            StreamReader jsonFile = new StreamReader(Server.MapPath("~/Models/DataBase.json"));  //SAVE DataBase.json in ISS
            var jsonString = jsonFile.ReadToEnd();
            Employees = JsonConvert.DeserializeObject<RootModel>(jsonString);
            jsonFile.Close();




            var viewModel = new DatabaseViewModel
            {
                RootModel = Employees,
            };

            return View(viewModel);
        }
    }
}