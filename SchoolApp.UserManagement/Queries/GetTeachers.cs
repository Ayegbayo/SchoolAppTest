using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Queries
{
    public class GetTeachersHandler : IRequestHandler<IRequest<List<Teacher>>, List<Teacher>>
    {
        private readonly IDataAccess<Teacher> _dataAccess;
        public GetTeachersHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<List<Teacher>> Handle(IRequest<List<Teacher>> request, CancellationToken cancellationToken)
        {
            var teachers = _dataAccess.GetAll().Select(x => ).AsNoTracking().ToListAsync(cancellationToken);
            return teachers;
        }
    }
}
