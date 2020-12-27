using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class ViewResultGateway:BaseGateway
    {
        public List<ViewCourses> GetResultById(int studentId)
        {
            string query = "SELECT c.course_code,c.course_name,c.course_credit,g.grade_name,g.grade_point " +
                           "FROM CourseAssignStudent as cas LEFT JOIN( " +
                           "SELECT sr.course_id,sr.grade_id FROM StudentResult as sr where sr.student_id='"+studentId+"')as sr " +
                           "ON sr.course_id=cas.course_id " +
                           "left join Course as c on c.course_id=cas.course_id " +
                           "Left join Grade as g on g.grade_id=sr.grade_id Where cas.student_id="+studentId;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<ViewCourses> viewcourseses = new List<ViewCourses>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ViewCourses viewcourse = new ViewCourses();
                viewcourse.CourseCode = reader["course_code"].ToString();
                viewcourse.CourseName = reader["course_name"].ToString();
                viewcourse.Grade = reader["grade_name"].ToString();
                viewcourse.GradePoint = reader["grade_point"].ToString();
                viewcourse.CourseCredit = Convert.ToDouble(reader["course_credit"]);
                if (viewcourse.Grade == "")
                {
                    viewcourse.Grade = "Grade Not Assigned Yet";
                    viewcourse.GradePoint = "Grade Not Assigned Yet";
                }
                    

                viewcourseses.Add(viewcourse);
            }
            connection.Close();
            return viewcourseses;
        }
    }
}