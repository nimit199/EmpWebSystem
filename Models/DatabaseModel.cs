
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EmpWebSystem.Models
{

    public class RootModel
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public string Did { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public string Did { get; set; }
        public string Eid { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }

}