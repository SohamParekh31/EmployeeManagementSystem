using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartment _dept;
        private readonly AppDbContext _context;

        public DepartmentsController(IDepartment dept,AppDbContext context)
        {
            _dept = dept;
            _context = context;
        }
        // GET: Departments
        [Authorize(Roles = "Admin,HR")]

        public IActionResult Index()
        {
            var deptList = _dept.getDepartments();
            return View(deptList);
        }
        [Authorize(Roles = "Admin,HR")]
        public IActionResult GetDepart()
        {
            var deptList = _dept.getDepartments();
            return Ok(deptList);
        }
        // GET: Departments/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,Name")] Department department)
        {
            _dept.InsertDepartment(department);
            return RedirectToAction("Index");
        }

        //GET: Departments/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Department department = _dept.getDepartmentById(id);
            return View(department);
        }

        ////POST: Departments/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, Department department)
        {
            _dept.UpdateDepartment(id,department);
            return RedirectToAction("Index");
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Department department = _dept.getDepartmentById(id);
            return View(department);
        }

        ////// POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dept.DeleteDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
