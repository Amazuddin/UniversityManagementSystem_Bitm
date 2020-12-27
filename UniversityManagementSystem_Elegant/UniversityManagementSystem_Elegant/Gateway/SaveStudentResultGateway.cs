using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class SaveStudentResultGateway:BaseGateway
    {
        public RegisterStudent GetStudentAllinfoById(int id)
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
                student.DepartmentId = (int)reader["department_id"];
                student.DepartmentName = reader["department_name"].ToString();
            }
            connection.Close();
            return student;
        }
        public List<Grade> GetGrades()
        {
            string query = "SELECT * FROM Grade";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<Grade> grades = new List<Grade>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Grade grade = new Grade();
                grade.Id = (int) reader["grade_id"];
                grade.Name = reader["grade_name"].ToString();
                grades.Add(grade);
            }
            connection.Close();
            return grades;
        }
        public List<Course> GetAllCourse(int id)
        {

            string query = "SELECT c.course_id, c.course_name FROM  CourseAssignStudent as cas JOIN Course as c ON c.course_id = " +
                           "cas.course_id WHERE cas.student_id=" + id;
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
        public int Save(SaveStudentResult saveStudentResult)
        {
            if (IsGradeExists(saveStudentResult) == false)
            {
                string query = "INSERT INTO StudentResult(student_id ,course_id,grade_id) VALUES ('" +
                               saveStudentResult.StudentId + "','"
                               + saveStudentResult.CourseId + "','" + saveStudentResult.GradeId + "')";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowAffected;
            }
            else if (IsGradeExists(saveStudentResult) == true)
            {
                string query = "UPDATE StudentResult SET grade_id='" + saveStudentResult.GradeId +
                               "' WHERE student_id='" + saveStudentResult.StudentId + "' AND course_id='" +
                               saveStudentResult.CourseId + "'  ";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowAffected;
            }
            else
            {
                return -1;
            }
        }
        public bool IsGradeExists(SaveStudentResult saveStudentResult)
        {

            string query = "SELECT student_id  FROM StudentResult WHERE student_id='" + saveStudentResult.StudentId + "' AND course_id='" 
                + saveStudentResult.CourseId+ "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool result = reader.HasRows;
            connection.Close();
            return result;
        }
    }
}