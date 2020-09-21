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
            foreach (var item in getEmployees())
            {
                if(item.DepartmentId == employee.DepartmentId)
                {
                    var empnotification = userManager.FindByEmailAsync(item.Email).Result;
                    hubContext.Clients.User(empnotification.Id).SendAsync("addEmployee", employee.Name + " " + employee.Surname + " employee Added");
                }
            }
            _context.SaveChanges();
            hubContext.Clients.All.SendAsync("RefreshEmployee");
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            _context.Update(employee);
            hubContext.Clients.Users("334cd12d-3af6-437f-b32f-1a231dbea8df", "4f6e7d79-14d6-4041-9244-cf6012f35cc1").SendAsync("employeeUpdate", employee.Name+" " +employee.Surname + " changed profile");
            _context.SaveChanges();
            hubContext.Clients.All.SendAsync("RefreshEmployee");
        }
    }
}
