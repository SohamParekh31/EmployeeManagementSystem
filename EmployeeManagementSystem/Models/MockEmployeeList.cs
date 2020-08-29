using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeList : IEmployee
    {
        SqlConnection con;
        private List<Employee> emp = new List<Employee>();
        private Employee employees;
        public MockEmployeeList()
        {
            string cs = "data source=.; database = EmployeeManagementSystem; integrated security=SSPI";
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Employees inner join  Departments on Employees.DepartmentId = Departments.DepartmentId", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees = new Employee();
                employees.department = new Department();
                employees.Id= Convert.ToInt32(reader[0]);
                employees.Name = reader[1].ToString();
                employees.Surname = reader[2].ToString();
                employees.Address = reader[3].ToString();
                employees.Qualification = reader[4].ToString();
                employees.Contact_Number = Convert.ToInt32(reader[5]);
                employees.DepartmentId = Convert.ToInt32(reader[6]);
                employees.department.DepartmentId = Convert.ToInt32(reader[7]);
                employees.department.Name = reader[8].ToString();
                emp.Add(employees);
            }
            con.Close();
        }
        public void DeleteEmployee(int id)
        {
            con.Open();
            string query = "DELETE FROM Employees WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Employee employee = employees.Find(x => x.Id == id);
            //employees.Remove(employee);
        }

        public Employee getEmployeeById(int id)
        {
            return emp.Find(m => m.Id == id);
        }

        public List<Employee> getEmployees()
        {
            return emp;
        }

        public void InsertEmployee(Employee employee)
        {
            con.Open();
            // Insert query  
            string query = "INSERT INTO Employees(Id,Name,Surname,Address,Qualification,Contact_number,DepartmentId) VALUES(@Id,@Name,@Surname,@Address,@Qualification,@Contact_number,@DepartmentId)";
            SqlCommand cmd = new SqlCommand(query, con);

            employees = new Employee();
            // Passing parameter values  
            employee.Id = emp.Max(x => x.Id) + 1;

            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@Qualification", employee.Qualification);
            cmd.Parameters.AddWithValue("@Contact_number", employee.Contact_Number);
            cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            cmd.ExecuteNonQuery();
            //dept.Add(department);
            con.Close();
            //employees.Add(employee);
            //return employees;
        }

        public void UpdateEmployee(int id,Employee employee)
        {
            con.Open();
            string query = "UPDATE Employees SET Name = '" + employee.Name + "',Surname = '"+employee.Surname+"',Address = '"+employee.Address+ "',Qualification = '"+employee.Qualification+ "',Contact_number = "+employee.Contact_Number+ ",DepartmentId = "+employee.DepartmentId+" WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Employee updateEmployee = emp.Find(x => x.Id == employee.Id);
            //updateEmployee.Id = employee.Id;
            //updateEmployee.Name = employee.Name;
            //updateEmployee.Surname = employee.Surname;
            //updateEmployee.Address = employee.Address;
            //updateEmployee.Qualification = employee.Qualification;
            //updateEmployee.Contact_Number = employee.Contact_Number;
            //updateEmployee.department = employee.department;
        }
    }
}
