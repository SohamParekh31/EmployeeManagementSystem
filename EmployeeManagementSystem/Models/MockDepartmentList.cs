using EmployeeManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<NotificationHub> hubContext;

        public MockDepartmentList(AppDbContext context,IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            this.hubContext = hubContext;
        }

        public List<Department> getDepartments()
        {
            return _context.departments.ToList();
        }

        public void InsertDepartment(Department department)
        {
            _context.departments.Add(department);
            hubContext.Clients.All.SendAsync("departmentAdded", department.Name + " Department Added");
            _context.SaveChanges();
            hubContext.Clients.All.SendAsync("Refresh");

        }
        public void UpdateDepartment(int id,Department department)
        {
            _context.Update(department);
            _context.SaveChanges();
            hubContext.Clients.All.SendAsync("Refresh");
        }
        public void DeleteDepartment(int id)
        {
            var department = _context.departments.Find(id);
            _context.departments.Remove(department);
            hubContext.Clients.Users("334cd12d-3af6-437f-b32f-1a231dbea8df").SendAsync("departmentDelete", department.Name + " Department deleted");
            //var employees = _context.employees.FirstOrDefault(e => e.DepartmentId == id);
            //_context.employees.Remove(employees);
            _context.SaveChanges();
            hubContext.Clients.All.SendAsync("Refresh");

        }

        public Department getDepartmentById(int id)
        {
            return _context.departments.FirstOrDefault(d => d.DepartmentId == id); 
        }
    }
}
 