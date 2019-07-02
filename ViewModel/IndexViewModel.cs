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
        public RootModel RootModel{ get; set; }

    }

    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
    }


   

}