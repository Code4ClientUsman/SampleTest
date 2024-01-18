using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Domains;
using Sample.Repositories;

namespace Tests.Unit.Sample.Repositories
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        [TestMethod]
        public void DeleteEmployee_WithValidId_ShouldDeleteEmployee()
        {
            // Arrange
            int employeeIdToDelete = 1; // Replace with a valid employee ID
            var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;
            using (var context = new DBContext(options))
            {
                context.Employees.Add(new Employee
                {
                    Id = 2
                });
                context.Employees.Add(new Employee
                {
                    Id = 1
                });
                context.SaveChanges();
                var employeeRepository = new EmployeeRepository(context);
                // Act
                var saveChanges = employeeRepository.Delete(employeeIdToDelete);
                // Assert
                Assert.AreEqual(1, saveChanges);
            }
        }

        [TestMethod]
        public void DeleteEmployee_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            int invalidEmployeeId = -1; // Replace with an invalid employee ID
            var options = new DbContextOptionsBuilder<DBContext>()
           .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
           .Options;
            using (var context = new DBContext(options))
            {
                context.Employees.Add(new Employee
                {
                    Id = 2
                });
                context.Employees.Add(new Employee
                {
                    Id = 1
                });
                context.SaveChanges();
                var employeeRepository = new EmployeeRepository(context);
                var saveChanges = employeeRepository.Delete(invalidEmployeeId);
                Assert.AreEqual(0, saveChanges);
            }
        }
    }
}
