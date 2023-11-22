using Microsoft.AspNetCore.Mvc;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.API.Controllers
{
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
       
        protected IActionResult PrepareResponse(APIResponse response)
        {
            var obj = StatusCode((int)response.StatusCode, response);
            return obj;
        }
    }
}
