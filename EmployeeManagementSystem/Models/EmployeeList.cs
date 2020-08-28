using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeList : IEmployee
    {
        static List<Employee> employees = null;
        static EmployeeList()
        {
            employees = new List<Employee>()
            {
               // new Employee(){Id = 1,Name = "Soham",Surname = "Parekh",Address = "Shree Vallabh App,Jamnagar",Qualification="CE",Contact_Number=9879020500,DepartmentId = 2}
            };
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = employees.Find(x => x.Id == id);
            employees.Remove(employee);
        }

        public Employee getEmployeeById(int id)
        {
            return employees.Find(m => m.Id == id);
        }

        public List<Employee> getEmployees()
        {
            return employees;
        }

        public List<Employee> InsertEmployee(Employee employee)
        {
            
            employees.Add(employee);
            return employees;
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee updateEmployee = employees.Find(x => x.Id == employee.Id);
            updateEmployee.Id = employee.Id;
            updateEmployee.Name = employee.Name;
            updateEmployee.Surname = employee.Surname;
            updateEmployee.Address = employee.Address;
            updateEmployee.Qualification = employee.Qualification;
            updateEmployee.Contact_Number = employee.Contact_Number;
            updateEmployee.department = employee.department;
        }
    }
}
