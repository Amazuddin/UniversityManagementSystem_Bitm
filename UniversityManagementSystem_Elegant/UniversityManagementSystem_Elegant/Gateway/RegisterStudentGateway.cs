using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class RegisterStudentGateway:BaseGateway
    {
        public int Register(RegisterStudent registerstudent)
        {
            
            string reg = GetMaxId(registerstudent.DepartmentId);
            string code = GetCode(registerstudent.DepartmentId);
            string year = DateTime.Now.Year.ToString();
            string regno = code + "-" + year + "-" + reg;
            string query = "INSERT INTO Student(student_regno,student_name,student_email,student_contact,student_joindate,student_address,department_id)" +
                           " VALUES('" + regno + "','" + registerstudent.StudentName + "','" + registerstudent.StudentEmail + "','" + registerstudent.StudentContactNo + "','" + registerstudent.Date + "','" + registerstudent.StudentAddress + "','" + registerstudent.DepartmentId+ "')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }

        public bool IsEmailExists(string email)
        {
            string query = "SELECT * FROM Student WHERE student_email= '" + email + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isemailExists = reader.HasRows;
            connection.Close();
            return isemailExists;
        }
        public bool IsContactExists(string contact)
        {
            string query = "SELECT * FROM Student WHERE student_contact='" + contact + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool iscontactExists = reader.HasRows;
            connection.Close();
            return iscontactExists;
        }

        public string GetMaxId(int id)
        {
            string query = "SELECT TOP 1  student_regno FROM Student WHERE department_id='" + id + "' ORDER BY student_id  DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            string data;
            if (reader.Read())
            {
                data = reader["student_regno"].ToString();
                int len = data.Length;
                string a = data[len-3].ToString()+ data[len-2].ToString() + data[len-1].ToString();
                int t = Convert.ToInt32(a)+1;
                data = t.ToString("D3");
            }
            else
            {
                int t = 1;
                data = t.ToString("D3");
            }
            
            connection.Close();
            return data;
        }

        public string GetCode(int id)
        {
            string query = "SELECT department_code FROM Department WHERE department_id="+id;
            SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            string code="";
            if (reader.Read())
            {
                code = reader["department_code"].ToString();
            }
            connection.Close();
            return code;
        }
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
        public List<RegisterStudent> GetAllStudentsId()
        {
            string query = "SELECT student_id,student_regno FROM Student";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<RegisterStudent> students = new List<RegisterStudent>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RegisterStudent student = new RegisterStudent();
                student.Id = (int)reader["student_id"];
                student.RegNo = reader["student_regno"].ToString();
                students.Add(student);
            }
            connection.Close();
            return students;
        }

        public RegisterStudent GetAllStudentsIdPdf(int id)
        {
            string query = "SELECT * FROM Student JOIN Department on Student.department_id=Department.department_id WHERE Student.student_id="+id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            RegisterStudent student = new RegisterStudent();
            while (reader.Read())
            {

                student.StudentId = (int) reader["student_id"];
                student.RegNo = reader["student_regno"].ToString();
                student.StudentName = reader["student_name"].ToString();
                student.DepartmentName = reader["department_name"].ToString();
                student.StudentAddress = reader["student_address"].ToString();
                student.StudentEmail = reader["student_email"].ToString();
                
            }
            connection.Close();
            return student;
        }
    }
}