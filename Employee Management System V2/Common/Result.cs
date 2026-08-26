using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Management_System_V2.Common
{
    internal class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Result(bool  isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
