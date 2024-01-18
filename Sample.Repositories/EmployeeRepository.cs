using Sample.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Repositories
{
    public interface IEmployeeRepository
    {
        void Insert(Employee employee);
        void Update(Employee employee);
        int Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        Employee GetById(int employeeId);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext _dbContext;

        public EmployeeRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var existingEmployee = _dbContext.Employees.Find(employee.Id);

            if (existingEmployee != null)
            {
                // Update properties based on your requirements
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Age = employee.Age;
                existingEmployee.Gender = employee.Gender;

                _dbContext.SaveChanges();
            }
        }

        public int Delete(int employeeId)
        {
            try
            {
                var employee = _dbContext.Employees.Find(employeeId);
                if (employee != null)
                {
                    _dbContext.Employees.Remove(employee);
                    return _dbContext.SaveChanges();
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
                   }
        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetById(int employeeId)
        {
            return _dbContext.Employees.Find(employeeId);
        }
    }
}
