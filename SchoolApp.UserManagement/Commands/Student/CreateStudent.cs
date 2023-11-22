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
    public class CreateStudentHandler : IRequestHandler<CreateStudent, APIResponse>
    {
        private readonly IDataAccess<Student> _dataAccess;
        public CreateStudentHandler(IDataAccess<Student> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<APIResponse> Handle(CreateStudent command, CancellationToken cancellationToken)
        {
            var studentExists = _dataAccess.Any(x => x.StudentNumber == command.StudentNumber);
            if (!studentExists)
            {
                var newstudent = await _dataAccess.Add(new Student
                {
                    Name = command.Name,
                    DateCreated = DateTime.Now,
                    DateOfBirth = command.DateofBirth,
                    NationalId = command.NationalId,                    
                    StudentNumber = command.StudentNumber,                    
                    SurName = command.Surname,                                
                });

                await _dataAccess.SaveChangesAsync();
                return APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Student created successfully", newstudent);
            }

            return APIResponse.GenerateResponse(true, (int)HttpStatusCode.Conflict, "A student exists with this student number");            
        }       
    }
}
