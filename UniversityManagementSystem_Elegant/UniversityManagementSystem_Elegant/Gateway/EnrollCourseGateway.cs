using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem_Elegant.Models;


namespace UniversityManagementSystem_Elegant.Gateway
{
    public class EnrollCourseGateway:BaseGateway
    {
       

        public RegisterStudent GetAllStudentById(int id)
        {
            string query =
                "SELECT s.student_name, s.student_email, s.department_id , d.department_name FROM Student as s" +
                " JOIN Department as d ON s.department_id=d.department_id  WHERE s.student_id=" +
                id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            RegisterStudent student = new RegisterStudent();
            if (reader.Read())
            {
                student.StudentName = reader["student_name"].ToString();
                student.StudentEmail = reader["student_email"].ToString();
                student.DepartmentId = (int) reader["department_id"];
                student.DepartmentName = reader["department_name"].ToString();
            }
            connection.Close();
            return student;
        }

        public List<Course> GetAllCourse(int deptId,int studentId)
        {
           string query="SELECT c.course_name,c.course_id " +
                        "FROM Course as c JOIN Department as d ON c.department_id=d.department_id " +
                        "WHERE c.course_id NOT IN ( " +
                        "SELECT course_id " +
                        "FROM CourseAssignStudent " +
                        "WHERE student_id='"+studentId+"' ) " +
                        "AND d.department_id="+deptId;
           // string query = "SELECT c.course_id, c.course_name FROM  Course as c JOIN Department d ON c.department_id = d.department_id WHERE c.department_id =" + id ;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();       
                course.CourseId = Convert.ToInt32(reader["course_id"]);
                course.Name = reader["course_name"].ToString();

                courseList.Add(course);
            }
            connection.Close();
            return courseList;
        }

        public int Save(EnrollCourse enrollCourse)
        {

            string query = "INSERT INTO CourseAssignStudent(student_id,course_id,courseassignstudentdate) VALUES ('" + enrollCourse.StudentId + "','" + enrollCourse.CourseId + "','" + enrollCourse.RegTime + "')";            
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
        public bool IsCourseExist(int CourseId,int StudentId)
        {
            
            string query = "SELECT student_id,course_id FROM CourseAssignStudent WHERE course_id=" + CourseId + " AND student_id=" + StudentId;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isNumberExist = reader.HasRows;
            connection.Close();
            return isNumberExist;
        }
        public int UnassignCourseStudent()
        {
            string query = "DELETE FROM CourseAssignStudent";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
    }
}