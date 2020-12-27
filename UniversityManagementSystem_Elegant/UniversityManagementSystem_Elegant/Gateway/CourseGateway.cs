using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class CourseGateway:BaseGateway
    {
        public List<Semester> GetAllSemester()
        {
            string query = "SELECT * FROM Semester";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Semester> semesters = new List<Semester>();
            while (reader.Read())
            {
                Semester semester = new Semester();
                semester.SemesterId = (int)reader["semester_id"];
                semester.SemesterName = reader["semester_name"].ToString();
                semesters.Add(semester);
            }
            connection.Close();
            return semesters;
        }
        public int SaveCourse(Course course)
        {
            if (IsCourseExists(course) == false)
            {
                string query = "INSERT INTO Course(course_code,course_name,course_credit,course_description,department_id,semester_id) " +
                               "VALUES('"+course.CourseCode+"','"+course.Name+"',"+course.Credit+",'"+course.Description+"',"+course.DepartmentId+","+course.SemesterId+")";
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
        public bool IsCourseExists(Course course)
        {
            string query = "SELECT course_id FROM Course WHERE course_code='" + course.CourseCode + "' OR course_name='" + course.Name + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool result = reader.HasRows;
            connection.Close();
            return result;
        }

    }
}