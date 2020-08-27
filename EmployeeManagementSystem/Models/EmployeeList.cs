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
                new Employee(){Id = 1,Name = "Soham",Surname = "Parekh",Address = "Shree Vallabh App,Jamnagar",Qualification="CE",Contact_Number=9879020500,DepartmentId = 2}
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

        public void InsertEmployee(Employee employee)
        {
            employee.Id = employees.Max(m => m.Id);
            employee.Id += 1;
            employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee employee1 = employees.Find(x => x.Id == employee.Id);
            employee1.Id = employee.Id;
            employee1.Name = employee.Name;
            employee1.Surname = employee.Surname;
            employee1.Address = employee.Address;
            employee1.Qualification = employee.Qualification;
            employee1.Contact_Number = employee.Contact_Number;
            employee1.DepartmentId = employee.DepartmentId;
        }
    }
}
