using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public interface IEmployee
    {
        public List<Employee> getEmployees();
        Employee getEmployeeById(int id);
        Employee InsertEmployee(Employee employee);
        void UpdateEmployee(int id,Employee employee);
        void DeleteEmployee(int id);
    }
}
