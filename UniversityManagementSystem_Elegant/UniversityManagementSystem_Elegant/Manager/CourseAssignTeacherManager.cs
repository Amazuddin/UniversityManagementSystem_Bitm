using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class CourseAssignTeacherManager
    {
        private CourseAssignTeacherGateway courseAssignTeacherGateway;
        public CourseAssignTeacherManager()
        {
            courseAssignTeacherGateway = new CourseAssignTeacherGateway();
        }
        public List<Department> GetDepartments()
        {
           
            return courseAssignTeacherGateway.GetDepartments();
        }

       
        

        public List<Teacher> GetTeachersById(int id )
        {
            
            return courseAssignTeacherGateway.GetTeachersById(id);
        }

        public List<Course> GetCourseById(int id)
        {
            
            return courseAssignTeacherGateway.GetCourseById(id);
        }

        public Teacher GetCreditTakenById(int id)
        {

           
            return courseAssignTeacherGateway.GetCreditTakenById(id);
        }

        public Course GetCourseNameCreditById(int id)
        {
            
            return courseAssignTeacherGateway.GetCourseNameCreditById(id);
        }

        public string Assign(CourseAssignTeacher courseassignteacher, int Remainingcredit, int Coursecredit)
        {
           
            
            int rowAffected = courseAssignTeacherGateway.Assign(courseassignteacher, Remainingcredit - Coursecredit);
            if (rowAffected>0)
            {
                return "Saved";
            }
            else
            {
                return "Not Saved";
            }
        }

        public string UnallocateTeacher()
        {
            int res = courseAssignTeacherGateway.UnassignCourseTeacher();
            if (res > 0)
                return "Course Unassign from teacher successful";
            else
            {
                return "Course Unassign from teacher failed";
            }
        }
    }
}