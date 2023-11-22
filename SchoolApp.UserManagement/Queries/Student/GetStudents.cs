﻿using MediatR;
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
    public class GetStudentsHandler : IRequestHandler<GetStudentsQuery, APIResponse>
    {
        private readonly IDataAccess<Student> _dataAccess;
        public GetStudentsHandler(IDataAccess<Student> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var students = await _dataAccess.GetAll().AsNoTracking().Select(x => new GetStudentsModel
            {
               StudentNumber = x.StudentNumber,
               Name = x.Name,
               Surname = x.SurName,
               DateofBirth = x.DateOfBirth,
               NationalId = x.NationalId,               
               Id = x.Id
            }).ToListAsync(cancellationToken);

            if (students.Any())
            {
                result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, $"Found {students.Count} students", students);
            }
            else
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NoContent);
            }

            return result;
        }
    }
}
