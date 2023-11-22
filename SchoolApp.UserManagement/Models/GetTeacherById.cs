using MediatR;
using SchoolApp.Data.Enums;
using System;

namespace SchoolApp.UserManagement.Models
{
    public class GetTeacherByIdQuery : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }

    public class GetTeacherByIdModel
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
