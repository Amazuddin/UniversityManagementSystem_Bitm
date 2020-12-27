using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class ViewClassScheduleGateway:BaseGateway
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
        public List<ViewClassSchedule> GetClassSchedules(int deptId)
        {
             string query = "SELECT * FROM ShowRoomAllocation WHERE department_id="+deptId;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<ViewClassSchedule> schedules = new List<ViewClassSchedule>();
            Dictionary<string, ViewClassSchedule> D=new Dictionary<string, ViewClassSchedule>();
            SqlDataReader reader = command.ExecuteReader();
            ViewClassSchedule viewClass;
            while (reader.Read())
            {
                
                string courseCode = reader["course_code"].ToString();
                string courseName = reader["course_name"].ToString();
                string roomNo = reader["room_no"].ToString();
                string day = reader["day_shortform"].ToString();
                string timeFrom = reader["from_time"].ToString();
                string toTime = reader["to_time"].ToString();

                string result = "R. No :" + roomNo + ", " + day + ", " + timeFrom + " - " + toTime+"</br>";
                if (roomNo == "")
                {
                    result = "Not assigned Yet";
                }

                if (!D.ContainsKey(courseCode))
                {
                    viewClass = new ViewClassSchedule();
                    viewClass.CourseCode = courseCode;
                    viewClass.CourseName = courseName;
                    viewClass.Schedule = result;
                    D[courseCode] = viewClass;
                    schedules.Add(viewClass);
                }
                else
                {
                    viewClass = D[courseCode];
                    viewClass.Schedule = viewClass.Schedule + result;
                }
                
            }
            connection.Close();
            return schedules;
        }
    }
}