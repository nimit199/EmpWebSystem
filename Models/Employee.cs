using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EmpWebSystem.Models
{
    public class Employee
    {
        public Database[] DataBase { get; set; }
    }

    public class Database
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }


    }
}