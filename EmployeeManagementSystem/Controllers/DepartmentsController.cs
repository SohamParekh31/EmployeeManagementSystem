using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartment _dept;
       
        public DepartmentsController(IDepartment dept)
        {
            _dept = dept;
        }

        // GET: Departments
        public IActionResult Index()
        {
            return View(_dept.getDepartments());
        }


        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        public IActionResult Create(Department department)
        {
            _dept.InsertDepartment(department);
            return RedirectToAction("Index");
        }

        //GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            Department department = _dept.getDepartmentById(id);
            return View(department);
        }

        ////POST: Departments/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            _dept.UpdateDepartment(id,department);
            return RedirectToAction("Index");
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            Department department = _dept.getDepartmentById(id);
            return View(department);
        }

        ////// POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dept.DeleteDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
