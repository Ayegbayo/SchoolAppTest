using Microsoft.AspNetCore.Identity;
using SchoolApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.UserManagement
{
    public class Teacher : Person
    {
        public Title Title { get; set; }
        public string TeacherNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
