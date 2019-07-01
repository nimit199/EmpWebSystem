using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpWebSystem.Models;

namespace EmpWebSystem.ViewModel
{
    public class DatabaseViewModel
    {
        // public List<Movie> Movie { get; set; }
        // public List<Customer> Customers { get; set; }
        public DatabaseList DatabaseList{ get; set; }
        public int id { get; set; }
        public int dept { get; set; }


        public List<SelectListItem> DepartmentList { get; set; }

    }

   

}