using Employee_Management_System_V2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Delegates
{
    delegate bool employeeFilter(Employee emp);  // Delegate declaration (take employee and check condition)
    internal class EmployeeFilter
    {
        public List<Employee> FilterEmployees(List<Employee> emps,employeeFilter condition)  // method that filter employee according to a condition take it using a method or lambda expression
        {
            List<Employee> employees = new List<Employee>();
            foreach(Employee emp in emps)
            {
                if (condition(emp))
                {
                    employees.Add(emp);
                }
            }
            return employees;
        }

    }
}
