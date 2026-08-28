using Employee_Management_System_V2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Delegates
{
    delegate bool compareEmployees(Employee a ,Employee b);
    internal class EmployeeComparer
    {
        public List<Employee> CompareEmployees(List<Employee> employees,compareEmployees compare)
        {
            List<Employee> result = employees;
               for(int i = 0; i < result.Count-1; i++)   // Bubble sort
            {
                bool IsSorted = true;
                for (int j = 0; j < result.Count - i - 1; j++)
                {
                    if (compare(result[j], result[j + 1]))
                        (result[j], result[j + 1]) = (result[j+1], result[j]);
                    IsSorted = false;
                }
                if(IsSorted) break;
            }
               return result;
        }

    }
}
