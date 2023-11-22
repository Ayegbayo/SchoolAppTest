using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.UserManagement.Models;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApp.UserManagement.Queries
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, APIResponse>
    {
        private readonly IDataAccess<Teacher> _dataAccess;
        public GetTeacherByIdHandler(IDataAccess<Teacher> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var teacher = await _dataAccess.GetAll().AsNoTracking().Select(x => new GetTeachersModel
            {                
                TeacherNumber = x.TeacherNumber,
                Name = x.Name,
                Surname = x.SurName,
                DateofBirth = x.DateOfBirth,
                NationalId = x.NationalId,
                Salary = x.Salary,
                Title = x.Title.ToString(),
                Id = x.Id
            }).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (teacher != null)
            {
                result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, $"Found teacher with Id #{teacher.Id}", teacher);
            }
            else
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No teacher matches this id");
            }

            return result;
        }
    }
}
