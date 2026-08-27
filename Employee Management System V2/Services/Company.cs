using Employee_Management_System_V2.Common;
using Employee_Management_System_V2.Events;
using Employee_Management_System_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Employee_Management_System_V2.Services
{
    internal class Company
    {
        public event EventHandler<EmployeeEventArgs> EmployeeActiviated;
        public event EventHandler<EmployeeEventArgs> EmployeePromoted;

       private List<Employee> ActiveEmployees = new List<Employee>();
       private Dictionary<int, string> Departments = new Dictionary<int, string>();
       private Queue<Employee> OnBoarding = new Queue<Employee>();
       private Stack<string> ActionsHistory = new Stack<string>();
       private HashSet<string> UniqueSkills = new HashSet<string>();


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
            if(emp.Salary <= 0)
            {
                return new Result<Employee>(false, "Salary must be > 0", null);
            }
            emp.Id = EmpId;
            EmpId++;
            OnBoarding.Enqueue(emp);
            ActionsHistory.Push($"Employee {emp.Name} added to Onboarding Queue");
            return new Result<Employee>(true, $"Add employee {emp.Name} Successfully", emp);
        }


        // Add new Department
        public Result<Department> AddDepartment(Department department)
        {
            if(string.IsNullOrWhiteSpace(department.Name))
            {
                return new Result<Department>(false, "Invalid Department name, please try again", null);
            }
            department.Id = DeptId;
            Departments.Add(department.Id,department.Name);
            ActionsHistory.Push($"Department {DeptId} added to Our system");
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
            ActionsHistory.Push($"Employee {emp.Name} added to Active Employees List");
            EmployeeActiviated.Invoke(this,new EmployeeEventArgs(emp.Name));
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
                ActionsHistory.Push($"New skill {skill} added");
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
            Console.WriteLine($"Employee {id} doesn't Exist");
            return null;
        }


        // find Employee by name
        public Employee FindemployeeByName(string name)
        {
            foreach (var emp in ActiveEmployees)
            {
                if (emp.Name == name)
                {
                    return emp;
                }
            }
            Console.WriteLine($"Employee {name} doesn't Exist");
            return null;
        }


        // Promote employee
        public Result<Manager> PromoteEmployee(int EmpId)
        {
            Employee? emp = FindemployeeByID(EmpId);
            if(emp == null)
            {
                return new Result<Manager>(false, $"Employee {emp.Id} isn't exist", null);
            }
            Manager manager = new Manager(emp.Name,emp.DepartmentId,emp.Salary);
            manager.Id = EmpId;
            manager.HireDate = DateTime.Now;
            ActionsHistory.Push($"Employee {emp.Name} Promoted to be a manager");
            EmployeePromoted.Invoke(this, new EmployeeEventArgs(manager.Name));
            return new Result<Manager>(true, $"Employee {manager.Name} Promoted to be manager of department {manager.DepartmentId}", manager);
        }


        // Display Employee of specific Department
        public List<Employee> DepartmentEmployees(int deptId)
        {
            if (!Departments.ContainsKey(deptId))
            {
                Console.WriteLine($"Department {deptId} doesn't exist");
                return new List<Employee>();
            }
            List<Employee> employees = new List<Employee>();
            foreach (var emp in ActiveEmployees)
            {
                if (emp.DepartmentId == deptId)
                {
                    employees.Add(emp);
                }
            }
            return employees;
        }


        // Calculate avarage salary
        public decimal AverageSalary()
        {
            decimal TotalSalary = 0;
            int employeeCount = 0;
            foreach(var emp in ActiveEmployees)
            {
                TotalSalary += emp.Salary;
                employeeCount++;
            }
            if (employeeCount == 0) return 0;
            return TotalSalary/employeeCount;
        }


        // Employee Count per department
        public Dictionary<int,int> EmployeesPerDepartment()
        {
            Dictionary<int,int> result = new Dictionary<int, int>();
            foreach(var key in Departments.Keys)
            {
                int employeeCount = 0;
                foreach (var emp in ActiveEmployees)
                {
                    if(emp.DepartmentId == key)
                    {
                        employeeCount++;
                    }
                }
                result.Add(key, employeeCount);
            }
            return result;
        }


        // store Action History
        public void ActionHistory(string action)
        {
            ActionsHistory.Push(action);
        } 

        // Display action History
        public void DisplayActionHistory()
        {
            if (ActionsHistory.Count == 0)
            {
                Console.WriteLine("No Actions to display");
                return;
            }
            foreach (var action in ActionsHistory)
            {
                Console.WriteLine(action);
            }
        }

        // Display unique skills
        public void DisplayUniqueSkills()
        {
            if (UniqueSkills.Count == 0)
            {
                Console.WriteLine("No Skills to display");
            }
            foreach (var skill in UniqueSkills)
            {
                Console.WriteLine(skill);
            }
        }

    }
}
