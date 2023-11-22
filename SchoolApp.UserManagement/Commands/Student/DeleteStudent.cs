using MediatR;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Commands
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, APIResponse>
    {
        private readonly IDataAccess<Student> _dataAccess;
        public DeleteStudentHandler(IDataAccess<Student> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var existingStudent = await _dataAccess.GetById(command.Id);
            if(existingStudent == null)
            {
                return APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No student matches this id");
            }

            _dataAccess.Delete(existingStudent);
            await _dataAccess.SaveChangesAsync();
            return APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Student has been deleted successfully");
        }

    }
}
