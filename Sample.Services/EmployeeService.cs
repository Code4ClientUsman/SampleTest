using Sample.Domains;
using Sample.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Save(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            _employeeRepository.Insert(employee);
        }

        public IEnumerable<Employee> GetAll(
            string firstName = null,
            string lastName = null,
            string gender = null)
        {
            IEnumerable<Employee> query = _employeeRepository.GetAll();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(e => e.FirstName.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(e => e.LastName.Contains(lastName));

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(e => e.Gender == gender);

            return query.ToList();
        }
    }
}
