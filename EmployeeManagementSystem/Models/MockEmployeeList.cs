using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeList : IEmployee
    {
        SqlConnection con;
        private List<Employee> emp = new List<Employee>();
        private Employee employees;
        public MockEmployeeList()
        {
            string cs = "data source=SOHAM; database = EmployeeManagementSystem; integrated security=SSPI";
            con = new SqlConnection(cs);
            con.Open();
        }
        public void DeleteEmployee(int id)
        {
            string query = "DELETE FROM Employees WHERE Id = " + id;
            con.Execute(query);
            con.Close();
        }

        public Employee getEmployeeById(int id)
        {
            List<Employee> getMaxIdofEmployee = con.Query<Employee>("select * from Employees").ToList();
            return getMaxIdofEmployee.Find(m => m.Id == id);
        }

        public List<Employee> getEmployees()
        {
            string query = "select * from Employees inner join Departments on Employees.DepartmentId = Departments.DepartmentId";
            return con.Query<Employee>(query).ToList();
        }

        public void InsertEmployee(Employee employee)
        {
            List<Employee> getMaxIdofEmployee = con.Query<Employee>("select * from Employees").ToList();
            employee.Id = getMaxIdofEmployee.Max(e => e.Id) + 1;
            string query = "INSERT INTO Employees(Id,Name,Surname,Address,Qualification,Contact_number,DepartmentId) VALUES(@Id,@Name,@Surname,@Address,@Qualification,@Contact_number,@DepartmentId)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", employee.Id);
            parameters.Add("@Name", employee.Name);
            parameters.Add("@Surname", employee.Surname);
            parameters.Add("@Address", employee.Address);
            parameters.Add("@Qualification", employee.Qualification);
            parameters.Add("@Contact_number", employee.Contact_Number);
            parameters.Add("@DepartmentId", employee.DepartmentId);
            con.Execute(query, parameters);
            con.Close();
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            string query = "UPDATE Employees SET Name = '" + employee.Name + "',Surname = '"+employee.Surname+"',Address = '"+employee.Address+ "',Qualification = '"+employee.Qualification+ "',Contact_number = "+employee.Contact_Number+ ",DepartmentId = "+employee.DepartmentId+" WHERE Id = " + id;
            con.Execute(query);
            con.Close();
        }
    }
}
