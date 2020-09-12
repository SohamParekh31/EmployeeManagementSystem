using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockDepartmentList : IDepartment
    {
        
        private readonly AppDbContext _context;
        public MockDepartmentList(AppDbContext context)
        {
            _context = context;
        }

        public List<Department> getDepartments()
        {
            return _context.departments.ToList();
        }

        public void InsertDepartment(Department department)
        {
            _context.departments.Add(department);
            _context.SaveChanges();
        }
        public void UpdateDepartment(int id,Department department)
        {
            _context.Update(department);
            _context.SaveChanges();
        }
        public void DeleteDepartment(int id)
        {
            var department = _context.departments.Find(id);
            _context.departments.Remove(department);
            var employees = _context.employees.FirstOrDefault(e => e.DepartmentId == id);
            _context.employees.Remove(employees);
            _context.SaveChanges();
        }

        public Department getDepartmentById(int id)
        {
            return _context.departments.FirstOrDefault(d => d.DepartmentId == id); 
        }
    }
}
 