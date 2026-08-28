using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Interfaces
{
    internal interface IHasId<T>
    {
        public int Id { get; }
        public bool IsExist(IEnumerable<T> collection , int id);
    }
}
