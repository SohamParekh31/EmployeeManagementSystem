﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentList : IDepartment
    {
        static List<Department> dept = null;
        static DepartmentList()
        {
             dept = new List<Department>()
            {
                new Department() { DepartmentId = 1,Name = "HR"},
                new Department() { DepartmentId = 2,Name = "Engineer"},
            };
        }
        
        public List<Department> getDepartments()
        {
            return dept;
        }

        public void InsertDepartment(Department department)
        {
            department.DepartmentId = dept.Max(m => m.DepartmentId);
            department.DepartmentId += 1;
            dept.Add(department);
        }

        public void UpdateDepartment(Department department)
        {
            Department updateDepartment = dept.Find(x => x.DepartmentId == department.DepartmentId);
            updateDepartment.DepartmentId = department.DepartmentId;
            updateDepartment.Name = department.Name;
        }
        public void DeleteDepartment(int id)
        {
            Department department = dept.Find(x => x.DepartmentId == id);
            dept.Remove(department);
        }

        public Department getDepartmentById(int id)
        {
            return dept.Find(m => m.DepartmentId == id); 
        }
    }
}
