using Employee_Management_System_V2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Services
{
    internal class Company
    {
        List<Employee> ActiveEmployees = new List<Employee>();
        Dictionary<int, string> Departments = new Dictionary<int, string>();
        Queue<Employee> OnBoarding = new Queue<Employee>();
        Stack<string> ActionsHistory = new Stack<string>();
        HashSet<string> UniqueSkills = new HashSet<string>();
    }
}
