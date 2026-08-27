using Employee_Management_System_V2.Common;
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


        // Id Generated automatically
        int EmpId = 1;
        int DeptId = 1;


        // Add new employee
        public Result<Employee> AddEmployee(Employee emp)
        {
            if (string.IsNullOrWhiteSpace(emp.Name))
            {
                return new Result<Employee>(false, "Invalid Employee name ,please try again", null);
            }
            if(! Departments.ContainsKey(emp.DepartmentId))
            {
                return new Result<Employee>(false, "Our system doesn't this Department ID", null);
            }
            if(emp.Salary < 0)
            {
                return new Result<Employee>(false, "Salary must be > 0", null);
            }
            emp.Id = EmpId;
            EmpId++;
            OnBoarding.Enqueue(emp);
            return new Result<Employee>(true, $"Add employee {emp.Name} Successfully", emp);
        }


        // Add new Department
        public Result<Department> AddDepartment(Department department)
        {
            if(string.IsNullOrWhiteSpace(department.Name))
            {
                return new Result<Department>(false, "Invalid Department name, please try again", null);
            }
            Departments.Add(DeptId,department.Name);
            DeptId++;
            return new Result<Department>(true, $"Department {department.Name} Added Successfully", department);
        }


        // Process Onboarding queue
        public Result<Employee> ProcessNextEmployee()
        {
            if(OnBoarding.Count == 0)
            {
                return new Result<Employee>(false, "Onboarding Queue is empty", null);
            }
            Employee emp = OnBoarding.Dequeue();
            ActiveEmployees.Add(emp);
            return new Result<Employee>(true,$"Employee {emp.Name} Added successfully",emp);
        }


        // Adding skills
        public Result<Employee> AddSkills(int empId,List<string> skills)
        {
            Employee? emp = FindemployeeByID(empId);
            if (emp == null)
            {
                return new Result<Employee>(false, "Can't add a skill for un existed employee", null);
            }
            foreach(var skill in skills)
            {
                UniqueSkills.Add(skill);
                emp.Skills.Add(skill);
            }
            return new Result<Employee>(true, $"Added skills for employee {emp.Name} successfully", emp);
        }


        // Find employee by ID
        public Employee FindemployeeByID(int id)
        {
            foreach(var emp in ActiveEmployees)
            {
                if(emp.Id == id)
                    return emp;
            }
            return null;
        }




    }
}
