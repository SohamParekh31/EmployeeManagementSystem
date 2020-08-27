using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IDepartment _department;
        

        public EmployeesController(IEmployee employee,IDepartment department)
        {
            _employee = employee;
            _department = department;
        }

        // GET: Employees
        public IActionResult Index()
        {
            ViewData["DeptName"] = new SelectList(_department.getDepartments(), "DepartmentId", "Name");
            return View(_employee.getEmployees());
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DeptName"] = new SelectList(_department.getDepartments(), "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Employee employee)
        {
            _employee.InsertEmployee(employee);
            return RedirectToAction("Index");
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            ViewData["DeptName"] = new SelectList(_department.getDepartments(), "DepartmentId", "Name");
            Employee employee = _employee.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            try
            {
                _employee.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int id)
        {
            Employee employee = _employee.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
