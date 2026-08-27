using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Models
{
    internal class Manager : Employee
    {
        List<string> TeamMembers = new List<string>();

        public Manager(string name, int departmentId, decimal salary)
            : base(name, departmentId, salary) { }
        
    }
}
