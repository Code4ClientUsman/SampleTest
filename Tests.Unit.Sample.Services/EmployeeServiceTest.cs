using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sample.Domains;
using Sample.Repositories;
using Sample.Services;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Unit.Sample.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        [TestMethod]
        public void Save_NewEmployee_ShouldInsertIntoDatabase()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object);
            var newEmployee = new Employee
            {
                Id = 1,
                FirstName = "Usman",
                LastName = "Saleem",
                Age = 30,
                Gender = "Male"
            };
            // Act
            employeeService.Save(newEmployee);
            // Assert
            employeeRepositoryMock.Verify(repo => repo.Insert(newEmployee), Times.Once);
        }

        [TestMethod]
        public void Save_ExistingEmployee_ShouldUpdateDatabase()
        {
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object);
            var existingEmployee = new Employee
            {
                Id = 1,
                FirstName = "Usman",
                LastName = "Saleem",
                Age = 25,
                Gender = "Male"
            };

            employeeRepositoryMock.Setup(repo => repo.GetById(existingEmployee.Id)).Returns(existingEmployee);

            var updatedEmployee = new Employee
            {
                Id = 1,
                FirstName = "Usman",
                LastName = "Fahad", // Updated last name
                Age = 26,          // Updated age
                Gender = "Male"
            };

            // Act
            employeeService.Save(updatedEmployee);

            // Assert
            employeeRepositoryMock.Verify(repo => repo.Update(updatedEmployee), Times.Never);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllEmployeesFromDatabase()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object);
            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Usman", LastName = "Saleem", Age = 30, Gender = "Male" },
            new Employee { Id = 2, FirstName = "Rana", LastName = "Humna", Age = 25, Gender = "Female" }
        };
            employeeRepositoryMock.Setup(repo => repo.GetAll()).Returns(employees);
            // Act
            var result = employeeService.GetAll();
            // Assert
            CollectionAssert.AreEqual(employees, result.ToList());
        }
    }
}