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
    public class DeleteTeacherHandler : IRequestHandler<DeleteTeacherCommand, APIResponse>
    {
        private readonly IDataAccess<Teacher> _dataAccess;
        public DeleteTeacherHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(DeleteTeacherCommand command, CancellationToken cancellationToken)
        {
            var existingTeacher = await _dataAccess.GetById(command.Id);
            if(existingTeacher == null)
            {
                return APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No teacher matches this id");
            }

            _dataAccess.Delete(existingTeacher);
            await _dataAccess.SaveChangesAsync();
            return APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, "Teacher has been deleted successfully");
        }

    }
}
