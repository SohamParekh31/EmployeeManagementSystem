using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public interface IDepartment
    {
        public List<Department> getDepartments();
        Department getDepartmentById(int id);
        void InsertDepartment(Department department);
        void UpdateDepartment(int id,Department department);
        void DeleteDepartment(int id);
    }
}
