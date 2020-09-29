using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IDepartment _department;

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;
        private readonly IHubContext<NotificationHub> hubContext;

        public EmployeesController(IEmployee employee,IDepartment department, UserManager<IdentityUser> userManager
                                    ,RoleManager<IdentityRole> roleManager,AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _employee = employee;
            _department = department;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.hubContext = hubContext;
        }

        // GET: Employees  
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_employee.getEmployees());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employees = _employee.getEmployeeById(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }


        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.RoleExistsAsync("Employee");
                var userName = employee.Name;
                var email = employee.Name + "@gmail.com";
                var password = employee.Name.ToUpper() + employee.Surname + "@2020";
                
                var doesUserExist = await userManager.FindByEmailAsync(email);
                if(doesUserExist != null)
                {
                    var emailChange = employee.Name + employee.Surname + "@gmail.com";
                    var user = new IdentityUser { UserName = emailChange, Email = emailChange };
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        employee.Email = emailChange;
                        await userManager.AddToRoleAsync(user, "Employee");
                        _employee.InsertEmployee(employee);
                        return Ok();
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    var user = new IdentityUser { UserName = userName, Email = email };
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        employee.Email = email;
                        await userManager.AddToRoleAsync(user, "Employee");
                        _employee.InsertEmployee(employee);
                        return Ok();
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }
            return BadRequest(employee);
        }


        // POST: Employees/Edit/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Employee employee)
        {
            try
            {
                _employee.UpdateEmployee(id,employee);
                return Ok();
            }
            catch
            {
                return View();
            }
        }


        // POST: Employees/Delete/5
        [HttpDelete("{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = context.employees.Find(id);
            var userEmp = await userManager.FindByEmailAsync(emp.Email);
            await userManager.DeleteAsync(userEmp);
            context.employees.Remove(emp);
            context.SaveChanges();
            await hubContext.Clients.All.SendAsync("RefreshEmployee");
            return Ok();
        }
    }
}
