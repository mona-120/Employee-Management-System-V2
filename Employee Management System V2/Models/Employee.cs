using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime HireDate { get; set; } = DateTime.Now;
        public int DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; } = new List<string>();

        public Employee(int id, string name , int deptId, decimal salary , List<string> skills)
        {
            Id = id;
            Name = name;
            DepartmentId = deptId;
            Salary = salary;
            Skills = skills;
        }
    }
}
