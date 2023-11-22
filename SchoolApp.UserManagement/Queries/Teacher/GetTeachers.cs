using MediatR;
using Microsoft.EntityFrameworkCore;
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

namespace SchoolApp.UserManagement.Queries
{
    public class GetTeachersHandler : IRequestHandler<GetTeachersQuery, APIResponse>
    {
        private readonly IDataAccess<Teacher> _dataAccess;
        public GetTeachersHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var teachers = await _dataAccess.GetAll().AsNoTracking().Select(x => new GetTeachersModel
            {
               TeacherNumber = x.TeacherNumber,
               Name = x.Name,
               Surname = x.SurName,
               DateofBirth = x.DateOfBirth,
               NationalId = x.NationalId,
               Salary = x.Salary,
               Title = x.Title.ToString(),
               Id = x.Id
            }).ToListAsync(cancellationToken);

            if (teachers.Any())
            {
                result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, $"Found {teachers.Count} teachers", teachers);
            }
            else
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NoContent);
            }

            return result;
        }
    }
}
