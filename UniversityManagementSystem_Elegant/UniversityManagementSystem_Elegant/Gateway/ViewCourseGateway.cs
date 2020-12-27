using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class ViewCourseGateway:BaseGateway
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
        public List<ViewCourses> GetAllInfoCoursesesById(int id)
        {
            string query = "SELECT * FROM CourseStaticsView WHERE department_id=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<ViewCourses> viewcourseses = new List<ViewCourses>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ViewCourses viewcourse = new ViewCourses();
                viewcourse.CourseCode = reader["course_code"].ToString();
                viewcourse.CourseName = reader["course_name"].ToString();
                viewcourse.CourseSemester = reader["semester_name"].ToString();
                viewcourse.CourseTeacher = reader["teacher_name"].ToString();
                if (viewcourse.CourseTeacher == "")
                    viewcourse.CourseTeacher = "Teacher Not Assigned Yet";
                viewcourseses.Add(viewcourse);
            }
            connection.Close();
            return viewcourseses;
        }

    }
}