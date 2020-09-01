using System;
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
            string query = "INSERT INTO Departments(DeptName) VALUES(@Name)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", department.DeptName);
            con.Execute(query, parameters);
            con.Close();
        }
        public void UpdateDepartment(int id,Department department)
        {
            string query = "UPDATE Departments SET DeptName = '" + department.DeptName + "' WHERE DepartmentId = " + id;
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
            List<Department> getMaxIdofDepartment = con.Query<Department>("select * from Departments").ToList();
            return getMaxIdofDepartment.Find(m => m.DepartmentId == id); 
        }
    }
}
 