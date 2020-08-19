using System;
using System.Collections.Generic;
using System.Text;

namespace HC.CarsDealer.Application.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
