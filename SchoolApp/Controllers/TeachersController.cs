using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.UserManagement.Commands;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    public class TeachersController : BaseAPIController
    {
        private readonly IMediator _mediator;
        
        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FetchTeachers() 
        {
            var teachers = await _mediator.Send(new GetTeachersQuery());
            return PrepareResponse(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FetchTeacher(int id)
        {
            var teacher = await _mediator.Send(new GetTeacherByIdQuery { Id = id });
            return PrepareResponse(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherCommand teacher) 
        {
            var response = await _mediator.Send(teacher);
            return PrepareResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherCommand teacher)
        {
            var response = await _mediator.Send(teacher);
            return PrepareResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var deleteTeacherCommand = new DeleteTeacherCommand { Id = id };
            var response = await _mediator.Send(deleteTeacherCommand);
            return PrepareResponse(response);
        }

    }
}
