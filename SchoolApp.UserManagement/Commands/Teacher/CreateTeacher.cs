using MediatR;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.UserManagement.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Commands
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, APIResponse>
    {
        private readonly IDataAccess<Teacher> _dataAccess;
        public CreateTeacherHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<APIResponse> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
        {
            var teacherExists = _dataAccess.Any(x => x.TeacherNumber == command.TeacherNumber);
            if (!teacherExists)
            {
                var newTeacher = await _dataAccess.Add(new Teacher
                {
                    Name = command.Name,
                    DateCreated = DateTime.Now,
                    DateOfBirth = command.DateofBirth,
                    NationalId = command.NationalId,
                    Salary = command.Salary,
                    TeacherNumber = command.TeacherNumber,
                    Title = command.Title,
                    SurName = command.Surname,
                });

                await _dataAccess.SaveChangesAsync();
                return APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Teacher created successfully", newTeacher);
            }

            return APIResponse.GenerateResponse(true, (int)HttpStatusCode.Conflict, "A teacher exists with this teacher number");            
        }

        public bool TeacherExists(string teacherNumber)
        {
            return _dataAccess.GetAll().Any(x => x.TeacherNumber == teacherNumber);
        }
    }
}
