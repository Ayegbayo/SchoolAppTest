using MediatR;
using SchoolApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Models
{
    public class UpdateTeacherCommand : IRequest<APIResponse>
    {
        public string TeacherNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalId { get; set; }
        public DateTime DateofBirth { get; set; }
        public Title Title { get; set; }
        public decimal Salary { get; set; }
        public int Id { get; set; }
    }

}
