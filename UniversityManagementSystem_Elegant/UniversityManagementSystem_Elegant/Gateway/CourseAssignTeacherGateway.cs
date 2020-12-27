using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class CourseAssignTeacherGateway:BaseGateway
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

       
        

        public List<Teacher> GetTeachersById(int id )
        {
           string query = "SELECT * FROM Teacher WHERE department_id=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<Teacher> teachers = new List<Teacher>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.Id = (int)reader["teacher_id"];
                teacher.Name = reader["teacher_name"].ToString();
                teachers.Add(teacher);
            }
            connection.Close();
            return teachers;
        }
        public List<Course> GetCourseById(int id)
        {
            string query = "SELECT course_id,course_code,course_name FROM Course WHERE course_id NOT IN  (SELECT course_id FROM CourseAssignTeacher WHERE department_id='" + id + "')AND department_id='" + id + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<Course> courses = new List<Course>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Course course = new Course();
                course.CourseId = (int)reader["course_id"];
                course.CourseCode = reader["course_code"].ToString();
                courses.Add(course);
            }
            connection.Close();
            return courses;
        }
        public Teacher GetCreditTakenById(int id)
        {
            string query = "SELECT * FROM Teacher WHERE teacher_id=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Teacher teacher = new Teacher();
            if (reader.Read())
            {
                teacher.Totalcredit= (int)reader["teacher_totalcredit"];
                teacher.Takencredit =(int) reader["teacher_remaincredit"];
            }
            connection.Close();
            return teacher;
        }

        public Course GetCourseNameCreditById(int id)
        {
            string query = "SELECT * FROM Course WHERE course_id=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Course course = new Course();
            if (reader.Read())
            {
                course.Name = reader["course_name"].ToString();
                course.Credit = Convert.ToDouble(reader["course_credit"]);
            }
            connection.Close();
            return course;
        }

        public int Assign(CourseAssignTeacher courseassignteacher,int value)
        {
          
            string query = "INSERT INTO CourseAssignTeacher(department_id ,teacher_id,course_id) VALUES('" + courseassignteacher.DepartmentId + "','" + courseassignteacher.TeacherId+ "','" + courseassignteacher.CourseId+ "')";
            string que = "UPDATE Teacher SET teacher_remaincredit='" + value + "' WHERE teacher_id=" + courseassignteacher.TeacherId;
            SqlCommand command = new SqlCommand(query, connection); 
            SqlCommand command2 = new SqlCommand(que, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            int rr = command2.ExecuteNonQuery();
            connection.Close();

            return rowAffect*rr;
        }

        public int UnassignCourseTeacher()
        {
            string query = "DELETE FROM CourseAssignTeacher";
            string query2 = "UPDATE Teacher SET teacher_remaincredit=teacher_totalcredit";
            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            int rowAffect2 = command2.ExecuteNonQuery();
            connection.Close();
            return rowAffect * rowAffect2;
        }

        public List<Course> GetCourseByDeptId(int deptId)
        {
            string query = "SELECT * FROM Course WHERE department_id=" + deptId;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while(reader.Read())
            {
                Course course = new Course();
                course.CourseId = (int) reader["course_id"];
                course.Name = reader["course_name"].ToString();
                courses.Add(course);
            }
            connection.Close();
            return courses;
        }

        
    }
}