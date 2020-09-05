using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IDepartment _department;

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public EmployeesController(IEmployee employee,IDepartment department, UserManager<IdentityUser> userManager
                                    ,  RoleManager<IdentityRole> roleManager)
        {
            _employee = employee;
            _department = department;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: Employees
        public IActionResult Index()
        {
            if (User.IsInRole("Employee")) 
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                var emp = _employee.getEmployees().ToList();
                var employee = emp.Find(e => e.Name == user.UserName);
                var employeeList = emp.Where(e => e.DepartmentId == employee.DepartmentId);
                //var res = user.UserName;
                return View(employeeList);
            }
            return View(_employee.getEmployees());
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.DeptName = _department.getDepartments();
            return View();
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
                var user = new IdentityUser { UserName = userName, Email = email };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    _employee.InsertEmployee(employee);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.DeptName = _department.getDepartments();
            Employee employee = _employee.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            try
            {
                _employee.UpdateEmployee(id,employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            Employee employee = _employee.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
