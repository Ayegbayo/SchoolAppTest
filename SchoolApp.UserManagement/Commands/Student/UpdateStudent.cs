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
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, APIResponse>
    {

        private readonly IDataAccess<Student> _dataAccess;
        public UpdateStudentHandler(IDataAccess<Student> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var existingStudent = await _dataAccess.GetById(command.Id);
            if(existingStudent == null)
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No student matches this Id");
                return result;
            }

            var anotherStudentExistsWithStudentNumber = _dataAccess.GetAll().Any(x => x.StudentNumber == command.StudentNumber && x.Id != command.Id);
            if (anotherStudentExistsWithStudentNumber)
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.Conflict, "A student exists with this student number already");
                return result;
            }

            existingStudent.DateOfBirth = command.DateofBirth;
            existingStudent.Name = command.Name;
            existingStudent.NationalId = command.NationalId;
            existingStudent.SurName = command.Surname;
            existingStudent.StudentNumber = command.StudentNumber;
            existingStudent.DateModified = DateTime.Now;

            await _dataAccess.SaveChangesAsync();
            result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Student record has been updated successfully", existingStudent);
            return result;
        }
    }
}
