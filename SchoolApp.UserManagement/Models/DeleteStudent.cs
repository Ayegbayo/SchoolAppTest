﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Models
{
    public class DeleteStudentCommand : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }
}
