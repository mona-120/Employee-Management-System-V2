using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Events
{
    internal class EmployeeEventArgs : EventArgs
    {
        public string EmployeeName { get; set; }
        public EmployeeEventArgs(string empName)
        {
            EmployeeName = empName;
        }
    }
}
