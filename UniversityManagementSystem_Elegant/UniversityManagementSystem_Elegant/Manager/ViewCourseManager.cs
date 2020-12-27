using System;
using System.Collections.Generic;
using System.Linq;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class ViewCourseManager
    {
        public List<Department> GetDepartments()
        {
            CourseAssignTeacherGateway courseAssignTeacherGateway = new CourseAssignTeacherGateway();
            return courseAssignTeacherGateway.GetDepartments();
        }
        public List<ViewCourses> GetAllInfoCoursesesById(int id)
        {
            ViewCourseGateway viewcoursegateway=new ViewCourseGateway();
            return viewcoursegateway.GetAllInfoCoursesesById(id);
        }
    }
}