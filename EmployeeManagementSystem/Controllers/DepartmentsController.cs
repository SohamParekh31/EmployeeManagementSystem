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
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
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
        [HttpGet]
        public IActionResult Index()
        {
            var deptList = _dept.getDepartments();
            return Ok(deptList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var departments = await _context.departments.FindAsync(id);

            if (departments == null)
            {
                return NotFound();
            }

            return departments;
        }
        

        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Departments/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            _context.departments.Add(department);
            _context.SaveChanges();
            return Ok(department);
        }

        //GET: Departments/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    Department department = _dept.getDepartmentById(id);
        //    return View(department);
        //}

        ////POST: Departments/Edit/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Department department)
        {
            _dept.UpdateDepartment(id,department);
            return Ok(department);
        }

        // GET: Departments/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Department department = _dept.getDepartmentById(id);
        //    return View(department);
        //}

        ////// POST: Departments/Delete/5
        [HttpDelete("{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dept.DeleteDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
