using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class DepartmentGateway:BaseGateway
    {
        public int SaveDepartment(Department department)
        {
            if (IsDepartmentExists(department)==false)
            {
                string query = "INSERT INTO Department(department_code,department_name) VALUES('"+department.DepartmentCode+"','"+department.DepartmentName+"')";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                return result;
            }
            else
            {
                return -1;
            }
        }
        public bool IsDepartmentExists(Department department)
        {
            string query = "SELECT department_id FROM Department WHERE department_code='"+department.DepartmentCode+"' OR department_name='"+department.DepartmentName+"'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool result = reader.HasRows;
            connection.Close();
            return result;
        }

        public List<Department> GetAllDepartment()
        {
            string query = "SELECT * FROM Department";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Department> departments = new List<Department>();
            while (reader.Read())
            {
                Department department=new Department();
                department.Id = (int) reader["department_id"];
                department.DepartmentCode = reader["department_code"].ToString();
                department.DepartmentName = reader["department_name"].ToString();
                departments.Add(department);
            }
            connection.Close();
            return departments;
        }
    }
}