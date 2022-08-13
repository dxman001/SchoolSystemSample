namespace School.Test.NUnit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using School.Controllers;
using School.Dtos;
using School.Enums;
using School.Persistance;
using School.Repositories;
using School.Services;
using System;

public class StudentServiceTest
{
    private Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
    private SchoolDbContext context;
    private IStudentService studentService;
    private IStudentRepository studentRepo;
    private StudentController studentController;
    private List<StudentDto> studentDtos;

    [SetUp]
    public void Setup()
    {
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString());
        context = new SchoolDbContext(dbOptions.Options);
        studentRepo = new StudentRepository(context);
        mockUnitOfWork.Setup(p => p.Students).Returns(studentRepo);
        studentService = new StudentService(mockUnitOfWork.Object);
        studentController = new StudentController(studentService);
        studentDtos = new List<StudentDto>()
        {
            new StudentDto()
            {
                Id= 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                MiddleName = "MiddleName1",
                BirthDate = DateTime.Parse("1990-01-01"),
                Gender = Gender.Male
            },
            new StudentDto()
            {
                Id = 2,
                FirstName = "FirstName2",
                LastName = "LastName2",
                MiddleName = "MiddleName2",
                BirthDate = DateTime.Parse("1995-01-01"),
                Gender = Gender.Female
             }
        };
    }

    [Test]
    public async Task GetAllStudents_Test()
    {
        // Arrange
        AddStudents();

        //Act
        var result = await studentService.GetAll();

        // Assert
        Assert.True(result.Count==2);
    }

    [Test]
    public async Task GetStudentById_Test()
    {
        //Arrange
        AddStudents();

        //Act
        var response = await studentController.GetById(1);
        var result = (response.Result as ObjectResult).Value as StudentDto;

        //Assert
        Assert.True(result.FirstName ==  studentDtos.FirstOrDefault().FirstName);
        Assert.True(result.MiddleName == studentDtos.FirstOrDefault().MiddleName);
    }

    [Test]
    public async Task CreateStudent_Test()
    {
        var response =  await studentController.Create(studentDtos.FirstOrDefault());
        mockUnitOfWork.Object.Students.Persist();
        Assert.IsTrue(int.TryParse((response.Result as ObjectResult).Value.ToString(),out int result));
        Assert.IsTrue(result == 1);
    }

    [Test]
    public async Task UpdateStudent_Test()
    {
        //Arrange
        AddStudents();
        studentDtos.LastOrDefault().FirstName = "Updated First Name2";
        studentDtos.LastOrDefault().Gender = Gender.Male;

        //Act
        await studentController.Update(studentDtos.LastOrDefault());
        mockUnitOfWork.Object.Students.Persist();
        var response = await studentController.GetById(2);
        var updatedStudent = (response.Result as ObjectResult).Value as StudentDto;

        //Assert
        Assert.IsTrue(updatedStudent.FirstName.Equals("Updated First Name2"));
        Assert.IsTrue(updatedStudent.Gender==Gender.Male);
    }

    [Test]
    public async Task DeleteStudent_Test()
    {
        //Arrange
        AddStudents();
        
        //Act
        await studentController.Delete(studentDtos.FirstOrDefault());
        mockUnitOfWork.Object.Students.Persist();
        var response = await studentController.GetById(1);
        var deletedStudent = (response.Result as ObjectResult).Value as StudentDto;

        //Assert
        Assert.IsTrue((response.Result as ObjectResult).StatusCode == 200);
        Assert.IsTrue(deletedStudent == null);
       
    }
    private void AddStudents()
    {
        studentDtos.ForEach(async x => await studentController.Create(x));
        mockUnitOfWork.Object.Students.Persist();
    }
   
}