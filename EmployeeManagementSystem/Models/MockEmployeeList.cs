using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeList : IEmployee
    {
        private readonly AppDbContext _context;
        public MockEmployeeList(AppDbContext context)
        {
            _context = context;
        }
        public void DeleteEmployee(int id)
        {
            _context.employees.Include(e => e.department).ToList();
            var employee = _context.employees.Find(id);
            _context.employees.Remove(employee);
            _context.SaveChanges();
        }

        public Employee getEmployeeById(int id)
        {
            return _context.employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> getEmployees()
        {
            var employee = _context.employees.Include(e => e.department);
            return employee.ToList();
        }

        public void InsertEmployee(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            _context.Update(employee);
            _context.SaveChanges();
        }
    }
}
