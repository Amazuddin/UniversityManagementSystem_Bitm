using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class SaveTeacherGateway:BaseGateway
    {
        public List<Department> GetDepartments()
        {
            
            string query = "SELECT * FROM Department";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<Department> departments = new List<Department>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Department department = new Department();
                department.Id = (int)reader["department_id"];
                department.DepartmentName = reader["department_name"].ToString();
                departments.Add(department);
            }
            connection.Close();
            return departments;
        }

        public List<Designation> GetDesignation()
        {
            
            string query = "SELECT * FROM Designation";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<Designation> designations = new List<Designation>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = (int)reader["designation_id"];
                designation.Name = reader["designation_name"].ToString();
                designations.Add(designation);
            }
            connection.Close();
            return designations;
        }
        public int SaveTeacher(Teacher teacher)
        {
            if (IsEmailContactExists(teacher) == false)
            {
                
                string query = "INSERT INTO Teacher(teacher_name,teacher_address,teacher_email,teacher_contact,teacher_designation,department_id,teacher_totalcredit,teacher_remaincredit)" +
                               " VALUES('" + teacher.Name + "','" + teacher.Address + "','" + teacher.Email + "','" + teacher.Contact+ "'," +
                               "'" + teacher.DesignationId + "','" + teacher.Department + "','" + teacher.Totalcredit + "','" + teacher.Totalcredit + "')";
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
        public bool IsEmailContactExists(Teacher teacher)
        {
            
            string query = "SELECT teacher_id FROM Teacher WHERE teacher_email='" + teacher.Email + "' OR teacher_contact='" +teacher.Contact+ "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool result = reader.HasRows;
            connection.Close();
            return result;
        }
    }
}