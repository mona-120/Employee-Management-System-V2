using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Events
{
    internal class SkillRegisterEventArgs : EventArgs
    {
        public string SkillName {  get; set; }
        public SkillRegisterEventArgs(string skillName)
        {
            SkillName = skillName;
        }
    }
}
