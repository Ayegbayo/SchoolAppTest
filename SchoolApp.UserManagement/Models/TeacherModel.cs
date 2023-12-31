﻿using MediatR;
using SchoolApp.Data.Enums;
using System;
using System.Collections.Generic;

namespace SchoolApp.UserManagement.Models
{
    public class GetTeachersQuery : IRequest<APIResponse>
    {

    }
    
    public class GetTeachersModel
    {
        public string TeacherNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalId { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public int Id { get; set; }
    }
}
