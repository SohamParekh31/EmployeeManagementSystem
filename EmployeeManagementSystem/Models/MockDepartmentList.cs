using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockDepartmentList : IDepartment
    {
        static List<Department> dept = new List<Department>();
        private IEmployee _employee;
        public MockDepartmentList(IEmployee  employee)
        {
            _employee = employee;
        }
        static MockDepartmentList()
        {
            dept = new List<Department>()
            {
                new Department() { DepartmentId = 1,Name = "HR"},
                new Department() { DepartmentId = 2,Name = "Engineer"},
            };
        }

        public List<Department> getDepartments()
        {
            
            return dept;
        }

        public void InsertDepartment(Department department)
        {
            dept.Add(department);
        }
        public void UpdateDepartment(int id,Department department)
        {
            Department updateDepartment = dept.Find(x => x.DepartmentId == department.DepartmentId);
            updateDepartment.DepartmentId = department.DepartmentId;
            updateDepartment.Name = department.Name;
            foreach (var item in _employee.getEmployees().ToList())
            {
                if (item.department.DepartmentId == department.DepartmentId)
                    item.department.Name = department.Name;
            }
        }
        public void DeleteDepartment(int id)
        {
            Department department = dept.Find(x => x.DepartmentId == id);
            dept.Remove(department);
            foreach (var item in _employee.getEmployees().ToList())
            {
                if (item.department.DepartmentId == id)
                    _employee.getEmployees().Remove(item);
            }
        }

        public Department getDepartmentById(int id)
        {
            return dept.Find(m => m.DepartmentId == id); 
        }
    }
}
 