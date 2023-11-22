using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Models
{
    public class APIResponse
    {
        public string Message { get; set; }
        public Object Data { get; set; }
        public bool IsError { get; set; }
        public int StatusCode { get; set; }

        public APIResponse()
        {

        }

        public APIResponse(int statusCode, bool isError = false, string message = "An error occurred while processing this request", object data = null)
        {
            Message = message;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public static APIResponse GenerateResponse(bool isError, int statusCode, string message = "An error occurred while processing this request",  object data = null)
        {
            return new APIResponse
            {
                Data = data,
                IsError = isError,
                Message = message,
                StatusCode = statusCode
            };            
        }
    }
}
