using Employee_Management_System_V2.Common;
using Employee_Management_System_V2.Delegates;
using Employee_Management_System_V2.Events;
using Employee_Management_System_V2.Models;
using Employee_Management_System_V2.Services;

namespace Employee_Management_System_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();
            company.DataSeeding();
            EmployeeFilter employeeFilter = new EmployeeFilter();

            // subscribe events
            company.EmployeeActiviated += AddingEmployeeHandler;
            company.EmployeePromoted += PromoteEmployeeHandler;

            while (true)
            {
                Console.WriteLine("Welcome in Employee System!");
                Console.WriteLine("=================================");
                Console.WriteLine("Please choose the process you need from the following: ");
                Console.WriteLine("1. Add an employee to Onboarding Queue");
                Console.WriteLine("2. Process employees in Onboarding queue");
                Console.WriteLine("3. Add new Department");
                Console.WriteLine("4. Add Skills for an employee");
                Console.WriteLine("5. Search for an employee using id");
                Console.WriteLine("6. Search for an employee using name");
                Console.WriteLine("7. Promote employee to be a manager");
                Console.WriteLine("8. Display employees of specific Department");
                Console.WriteLine("9. Calaulate avarage salary");
                Console.WriteLine("10. Display number of employees for each Department");
                Console.WriteLine("11. Display Actions History");
                Console.WriteLine("12. Display All Unique skills");
                Console.WriteLine("13. Get managers");
                Console.WriteLine("14. Get Employees with specific salary");
                Console.WriteLine("15. Get Employees that take alary above a specific number");
                Console.WriteLine("16. Exit");

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input , please enter valid input(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16)");
                        break;
                    }
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Employee name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Department Id: ");
                            if (!int.TryParse(Console.ReadLine(), out int Id))
                            {
                                Console.WriteLine("Id must be integer number, please try again");
                                break;
                            }
                            Console.Write("Enter salary: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
                            {
                                Console.WriteLine("Salary must be decimal number, please try again");
                                break;
                            }
                            Employee emp = new Employee(name, Id, salary);
                            Result<Employee> resultemp = company.AddEmployee(emp);
                            Console.WriteLine(resultemp.Message);
                            break;
                        case 2:
                            company.ProcessNextEmployee();
                            break;
                        case 3:
                            Console.Write("Enter Department name: ");
                            string deptName = Console.ReadLine();
                            Department dept = new Department(deptName);
                            Result<Department> resDept = company.AddDepartment(dept);
                            Console.WriteLine(resDept.Message);
                            break;
                        case 4:
                            List<string> skills = new List<string>();
                            Console.Write("Enter employee id: ");
                            if(!int.TryParse(Console.ReadLine(),out int empid))
                            {
                                Console.WriteLine("Id must be integer number, please try again");
                                break;
                            }
                            Console.Write("Enter number of skills: ");
                            int num = int.Parse(Console.ReadLine());
                            for(int i= 0; i < num; i++)
                            {
                                Console.Write("Enter a skill: ");
                                string skill = Console.ReadLine();
                                skills.Add(skill);
                            }
                            Result<Employee> resSkill = company.AddSkills(empid,skills);
                            Console.WriteLine(resSkill.Message);
                            break;
                        case 5:
                            Console.Write("Enter Employee id: ");
                            if(!int.TryParse(Console.ReadLine(),out int id))
                            {
                                Console.WriteLine("Id must be integer number, please try again");
                                break;
                            }
                            Employee employee = company.FindemployeeByID(id);
                            if(employee != null)
                            {
                                Console.WriteLine($"Found Employee {employee.Name}");
                            }
                            else
                            {
                                Console.WriteLine($"Employee with id {id} doesn't Exist");
                            }
                            break;
                        case 6:
                            Console.Write("Enter Employee name: ");
                            string empName = Console.ReadLine();
                            Employee employee_ = company.FindemployeeByName(empName);
                            if (employee_ != null)
                            {
                                Console.WriteLine($"Found Employee with Id: {employee_.Id}");
                            }
                            else
                            {
                                Console.WriteLine($"Employee {empName} doesn't Exist");
                            }
                            break;
                        case 7:
                            Console.Write("Enter Employee id: ");
                            if (!int.TryParse(Console.ReadLine(), out int id7))
                            {
                                Console.WriteLine("Id must be integer number, please try again");
                                break;
                            }
                            company.PromoteEmployee(id7);
                            break;
                        case 8:
                            Console.Write("Enter Department id: ");
                            if (!int.TryParse(Console.ReadLine(), out int id8))
                            {
                                Console.WriteLine("Id must be integer number, please try again");
                                break;
                            }
                            List<Employee> result8 = company.DepartmentEmployees(id8);
                            foreach(var emp8 in result8)
                            {
                                Console.WriteLine(emp8.Name);
                            }
                            break;
                        case 9:
                            var average = company.AverageSalary();
                            Console.WriteLine(average);
                            break;
                        case 10:
                            Dictionary<int, int> result10 = company.EmployeesPerDepartment();
                            foreach(var val in result10)
                            {
                                Console.WriteLine($"Department Id: {val.Key} , Count of employees: {val.Value}");
                            }
                            break;
                        case 11:
                            company.DisplayActionHistory();
                            break;
                        case 12:
                            company.DisplayUniqueSkills();
                            break;
                        case 13:
                            List<Employee> result13 = employeeFilter.FilterEmployees(company.ActiveEmployees, emp => emp is Manager);
                            if (result13.Count > 0)
                            {
                                foreach (var emp13 in result13)
                                {
                                    Console.WriteLine($"Employee {emp13.Name} with Id: {emp13.Id} is a manager");
                                }
                            }
                            else
                            {
                                Console.WriteLine("There is no managers");
                            }
                            break;
                        case 14:
                            Console.Write("Enter salary to use to filter employee: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal sal))
                            {
                                Console.WriteLine("Id must be decimal number, please try again");
                                break;
                            }
                            List<Employee> result14 = employeeFilter.FilterEmployees(company.ActiveEmployees, emp => emp.Salary == sal);
                            if (result14.Count > 0)
                            {
                                foreach (var emp14 in result14)
                                {
                                    Console.WriteLine($"Employee {emp14.Name} with Id: {emp14.Id}");
                                }
                            }
                            else Console.WriteLine($"There is no employee that take salary {sal}");
                            break;
                        case 15:
                            Console.Write("Enter number: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal number))
                            {
                                Console.WriteLine("Id must be decimal number, please try again");
                                break;
                            }
                            List<Employee> result15 = employeeFilter.FilterEmployees(company.ActiveEmployees,emp => emp.Salary > number);
                            if (result15.Count > 0)
                            {
                                foreach (var emp15 in result15)
                                {
                                    Console.WriteLine($"Employee {emp15.Name} with Id: {emp15.Id}");
                                }
                            }
                            else Console.WriteLine($"No Employees take salary above {number}");
                            break;
                        case 16:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Process number , please enter valid input(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16)");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        // methods that handle event
        public static void AddingEmployeeHandler(object? ob,EmployeeEventArgs e)
        {
            Console.WriteLine($"Employee {e.EmployeeName} added to Active Employee List");
        }

        public static void PromoteEmployeeHandler(object? ob,EmployeeEventArgs e)
        {
            Console.WriteLine($"Employee {e.EmployeeName} become manager");
        }
    }
}
