﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeManagementSystem.Models
{
    public class MockDepartmentList : IDepartment
    {
        SqlConnection con;
        private List<Department> dept = new List<Department>();
        public MockDepartmentList()
        {
            string cs = "data source=SOHAM; database = EmployeeManagementSystem; integrated security=SSPI";
            con = new SqlConnection(cs);
            con.Open();
            
        }
        
        public List<Department> getDepartments()
        {
            return con.Query<Department>("select * from Departments").ToList();
        }
        public void InsertDepartment(Department department)
        {
            List<Department> getMaxIdofDepartment = con.Query<Department>("select * from Departments").ToList();
            department.DepartmentId= getMaxIdofDepartment.Max(e => e.DepartmentId) + 1;
            string query = "INSERT INTO Departments(DepartmentId,Name) VALUES(@DepartmentId, @Name)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", department.DepartmentId);
            parameters.Add("@Name", department.Name);
            con.Execute(query, parameters);
            con.Close();
        }
        public void UpdateDepartment(int id,Department department)
        {
            //con.Open();
            string query = "UPDATE Departments SET Name = '"+department.Name+ "' WHERE DepartmentId = " + id;
            con.Execute(query);
            con.Close();
        }
        public void DeleteDepartment(int id)
        {
            string query = "DELETE FROm Departments WHERE DepartmentId = " + id;
            con.Execute(query);
            con.Close();
        }

        public Department getDepartmentById(int id)
        {
            return dept.Find(m => m.DepartmentId == id); 
        }
    }
}
 