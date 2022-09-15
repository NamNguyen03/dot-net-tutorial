using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Weather.Models
{
    public class MyResponseListModels<T>
    {
        public IEnumerable<T> Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}