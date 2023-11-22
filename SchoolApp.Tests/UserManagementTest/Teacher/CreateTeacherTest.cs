using Moq;
using NUnit.Framework;
using SchoolApp.DataAccess.Abstractions;
using SchoolApp.DataAccess.Implementations;
using SchoolApp.UserManagement.Commands;
using SchoolApp.UserManagement.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SchoolApp.Tests.UserManagementTest.Student
{
    public class CreateTeacherCommandHandleTest
    {
        private Mock<IDataAccess<Data.UserManagement.Teacher>> _dataAccess;

        [SetUp]
        public async Task Setup()
        {
            _dataAccess = new();
        }

        [Test]
        public async Task CreateTeacher_With_Existing_Teacher_Number()
        {
            var createStudentCommand = new Mock<CreateTeacherCommand>();
            createStudentCommand.Object.TeacherNumber = "Student-001";
            var command = new Mock<CreateTeacherHandler>(_dataAccess.Object);

            command.Setup(x => x.TeacherExists(createStudentCommand.Object.TeacherNumber)).Returns(true);
            var createdStudentresponse = await command.Object.Handle(createStudentCommand.Object, default);

            Assert.That(createdStudentresponse.IsError);
            Assert.That(createdStudentresponse.StatusCode == (int)HttpStatusCode.Conflict);
        }

        [Test]
        public async Task CreateTeacher_Create_New_Student()
        {
            var createTeacherCommand = new Mock<CreateTeacherCommand>();
            createTeacherCommand.Object.TeacherNumber = "Teacher-001";
            var command = new Mock<CreateTeacherHandler>(_dataAccess.Object);

            var createdStudentresponse = await command.Object.Handle(createTeacherCommand.Object, default);

            Assert.That(!createdStudentresponse.IsError);
            Assert.That(createdStudentresponse.StatusCode == (int)HttpStatusCode.OK);
        }

    }
}
