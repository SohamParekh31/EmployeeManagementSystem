using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockDepartmentList : IDepartment
    {
        SqlConnection con;
        private List<Department> dept = new List<Department>();
        private Department departments;
        public MockDepartmentList()
        {
            string cs = "data source=SOHAM; database = EmployeeManagementSystem; integrated security=SSPI";
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Departments", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                departments = new Department();
                departments.DepartmentId = Convert.ToInt32(reader[0]);
                departments.Name = reader[1].ToString();
                dept.Add(departments);
            }
            con.Close();
        }
        //static MockDepartmentList()
        //{
        //     dept = new List<Department>()
        //    {
        //        //new Department() { DepartmentId = "1",Name = "HR"},
        //        //new Department() { DepartmentId = "2",Name = "Engineer"},
        //    };
        //}
        
        public List<Department> getDepartments()
        {
            //SqlCommand cmd = new SqlCommand("select * from Departments", con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    departments = new Department();
            //    departments.DepartmentId = Convert.ToInt32(reader[0]);
            //    departments.Name = reader[1].ToString();
            //    dept.Add(departments);
            //}
            return dept;
        }

        public void InsertDepartment(Department department)
        {
            con.Open();
            // Insert query  
            string query = "INSERT INTO Departments(DepartmentId,Name) VALUES(@DepartmentId, @Name)";
            SqlCommand cmd = new SqlCommand(query, con);
            
            departments = new Department();
                // Passing parameter values  
            department.DepartmentId = dept.Max(x => x.DepartmentId)+1;

            cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
            cmd.Parameters.AddWithValue("@Name", department.Name);
            cmd.ExecuteNonQuery();
            //dept.Add(department);
            con.Close();
        }
        public void UpdateDepartment(int id,Department department)
        {
            con.Open();
            string query = "UPDATE Departments SET Name = '"+department.Name+ "' WHERE DepartmentId = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Department updateDepartment = dept.Find(x => x.DepartmentId == department.DepartmentId);
            //updateDepartment.DepartmentId = department.DepartmentId;
            //updateDepartment.Name = department.Name;
            //foreach (var item in _employee.getEmployees().ToList())
            //{
            //    if (item.department.DepartmentId == department.DepartmentId)
            //        item.department.Name = department.Name;
            //}
        }
        public void DeleteDepartment(int id)
        {
            con.Open();
            string query;
            SqlCommand cmd;
            query = "DELETE FROM Departments WHERE DepartmentId = " + id;
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            query = "DELETE FROM Employees WHERE DepartmentId = " + id;
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Department department = dept.Find(x => x.DepartmentId == id);
            //dept.Remove(department);
            //foreach (var item in _employee.getEmployees().ToList())
            //{
            //    if (item.department.DepartmentId == id)
            //        _employee.getEmployees().Remove(item);
            //}
        }

        public Department getDepartmentById(int id)
        {
            return dept.Find(m => m.DepartmentId == id); 
        }
    }
}
 