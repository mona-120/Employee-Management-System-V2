using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Models
{
    internal class Manager : Employee
    {
        List<string> TeamMembers = new List<string>();

        public Manager(int id, string name, int departmentId, decimal salary, List<string> skills ,List<string> members)
            : base(id, name, departmentId, salary , skills)
        {
            TeamMembers = members;
        }
    }
}
