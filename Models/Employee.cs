using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EmpWebSystem.Models
{
    public class DatabaseList
{
        public Database DataBase { get; set; }
    }

    public class Database
    {
        public Department[] Departments { get; set; }
    }

    public class Department
    {
        public string Name { get; set; }
        public Employee[] Employees { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
    }

}