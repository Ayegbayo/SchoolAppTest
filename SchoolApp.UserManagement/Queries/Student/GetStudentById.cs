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
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, APIResponse>
    {
        private readonly IDataAccess<Student> _dataAccess;
        public GetStudentByIdHandler(IDataAccess<Student> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<APIResponse> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new APIResponse();
            var student = await _dataAccess.GetAll().AsNoTracking().Select(x => new GetStudentsModel
            {                
                StudentNumber = x.StudentNumber,
                Name = x.Name,
                Surname = x.SurName,
                DateofBirth = x.DateOfBirth,
                Id = x.Id,                
                NationalId = x.NationalId
            }).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (student != null)
            {
                result = APIResponse.GenerateResponse(false, (int)HttpStatusCode.OK, $"Found student with Id #{student.Id}", student);
            }
            else
            {
                result = APIResponse.GenerateResponse(true, (int)HttpStatusCode.NotFound, "No student matches this id");
            }

            return result;
        }
    }
}
