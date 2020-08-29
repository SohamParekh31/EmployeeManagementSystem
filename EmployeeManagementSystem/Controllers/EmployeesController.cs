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
            ViewBag.DeptName = _department.getDepartments();
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public IActionResult Create( Employee employee)
        {
            _employee.InsertEmployee(employee);
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    var Department = (_department.getDepartments()).Find(x => x.Name == employee.department.Name);
            //    employee.Id = ((_employee.getEmployees()).Count + 1);
            //    employee.department = Department;
            //    var result = _employee.InsertEmployee(employee);
            //    return View("Index", result);
            //}

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
