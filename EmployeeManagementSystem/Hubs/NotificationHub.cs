using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly AppDbContext _context;
        public NotificationHub(AppDbContext context)
        {
            this._context = context;
        }
        public string GetConnectionId() => Context.ConnectionId;

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Admin"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            }
            else if (Context.User.IsInRole("HR"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "HR");
            }
            else if (this.Context.User.IsInRole("Employee"))
            {
                string dept = _context.employees.Include(e => e.department).Where(e => e.Email == Context.User.Identity.Name).First().department.Name;
                var grpName = "Employee" + dept;
                await this.Groups.AddToGroupAsync(Context.ConnectionId, grpName);

            }
            await base.OnConnectedAsync();

        }
    }
}
