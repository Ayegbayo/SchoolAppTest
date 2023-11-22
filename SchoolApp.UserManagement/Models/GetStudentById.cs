﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Models
{
    public class GetStudentByIdQuery : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }

    public class GetStudentByIdModel
    {
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalId { get; set; }
        public DateTime DateofBirth { get; set; }
        public int Id { get; set; }
    }    
}
