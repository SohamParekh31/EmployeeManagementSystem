using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeList : IEmployee
    {
        static List<Employee> emp = new List<Employee>();
       // private Employee employees;
        public MockEmployeeList()
        {
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = emp.Find(x => x.Id == id);
            emp.Remove(employee);
        }

        public Employee getEmployeeById(int id)
        {
            return emp.Find(m => m.Id == id);
        }

        public List<Employee> getEmployees()
        {
            return emp;
        }

        public Employee InsertEmployee(Employee employee)
        {
            emp.Add(employee);
            return employee;
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            Employee updateEmployee = emp.Find(x => x.Id == employee.Id);
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
