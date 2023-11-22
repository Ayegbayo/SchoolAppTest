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
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand, APIResponse>
    {

        private readonly IDataAccess<Teacher> _dataAccess;
        public UpdateTeacherHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var existingTeacher = await _dataAccess.GetById(command.Id);
            if(existingTeacher == null)
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No teacher matches this Id");
                return result;
            }

            var anotherTeacherExistsWithTeacherNumber = _dataAccess.GetAll().Any(x => x.TeacherNumber == command.TeacherNumber && x.Id != command.Id);
            if (anotherTeacherExistsWithTeacherNumber)
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.Conflict, "A teacher exists with this teacher number already");
                return result;
            }

            existingTeacher.DateOfBirth = command.DateofBirth;
            existingTeacher.Name = command.Name;
            existingTeacher.NationalId = command.NationalId;
            existingTeacher.Salary = command.Salary;
            existingTeacher.SurName = command.Surname;
            existingTeacher.TeacherNumber = command.TeacherNumber;
            existingTeacher.DateModified = DateTime.Now;
            existingTeacher.Title = command.Title;

            await _dataAccess.SaveChangesAsync();
            result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Teacher record has been updated successfully", existingTeacher);
            return result;
        }
    }
}
