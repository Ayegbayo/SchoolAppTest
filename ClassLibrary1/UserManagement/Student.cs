﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.UserManagement
{
    public class Student : Person
    {
        public string StudentNumber { get; set; }
    }
}
