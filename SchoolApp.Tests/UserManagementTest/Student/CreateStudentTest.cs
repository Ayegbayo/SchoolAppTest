using MediatR;
using Moq;
using NUnit.Framework;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.UserManagement.Commands;
using SchoolApp.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Tests.UserManagementTest.Student
{
    public class CreateStudentCommandHandleTest 
    {
        private  Mock<IDataAccess<Data.UserManagement.Student>> _dataAccess;        
        
        [SetUp]
        public async Task Setup()
        {
            _dataAccess = new();            
        }

        [Test]
        public async Task CreateStudent_With_Existing_Student_Number()
        {            
            var createStudentCommand = new Mock<CreateStudent>();
            createStudentCommand.Object.StudentNumber = "Student-001";
            Mock<CreateStudentHandler> command = new Mock<CreateStudentHandler>(_dataAccess.Object);

            _dataAccess.Setup(x => x.Any(x => x.StudentNumber == createStudentCommand.Object.StudentNumber)).Returns(true);
            var createdStudentresponse = await command.Object.Handle(createStudentCommand.Object, default);

            Assert.That(createdStudentresponse.IsError);
            Assert.That(createdStudentresponse.StatusCode == (int)HttpStatusCode.Conflict);
        }

        [Test]
        public async Task CreateStudent_Create_New_Student()
        {
            var createStudentCommand = new Mock<CreateStudent>();
            createStudentCommand.Object.StudentNumber = "Student-001";
            var command = new Mock<CreateStudentHandler>(_dataAccess.Object);

            var createdStudentresponse = await command.Object.Handle(createStudentCommand.Object, default);

            Assert.That(!createdStudentresponse.IsError);
            Assert.That(createdStudentresponse.StatusCode == (int)HttpStatusCode.OK);
        }

    }
}
