using EmployeeManagementSystem.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly UserManager<IdentityUser> userManager;

        public MockEmployeeList(AppDbContext context, IHubContext<NotificationHub> hubContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.hubContext = hubContext;
            this.userManager = userManager;
        }
        public void DeleteEmployee(int id)
        {
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
            var dept = _context.departments.Where(d => d.DepartmentId == employee.DepartmentId).First().Name;
            var grpName = "Employee" + dept;
            hubContext.Clients.Group(grpName).SendAsync("addEmployee", employee.Name + " " + employee.Surname + " employee Added");
            _context.SaveChanges();
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            _context.Update(employee);
            hubContext.Clients.Groups("Admin","HR").SendAsync("employeeUpdate", employee.Name+" " +employee.Surname + " changed profile");
            _context.SaveChanges();
        }
    }
}
