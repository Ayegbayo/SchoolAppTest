using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FetchStudents()
        {
            var students = await _mediator.Send(new GetStudentsQuery());
            return PrepareResponse(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FetchStudent(int id)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery { Id = id });
            return PrepareResponse(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudent student)
        {
            var response = await _mediator.Send(student);
            return PrepareResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentCommand student)
        {
            student.Id = id;
            var response = await _mediator.Send(student);
            return PrepareResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleteStudentCommand = new DeleteStudentCommand { Id = id };
            var response = await _mediator.Send(deleteStudentCommand);
            return PrepareResponse(response);
        }
    }
}
