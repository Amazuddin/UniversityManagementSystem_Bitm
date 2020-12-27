using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class CourseManager
    {
        CourseGateway courseGateway=new CourseGateway();

        public CourseManager()
        {
            courseGateway=new CourseGateway();
        }
        public List<Semester> GetAllSemester()
        {
            return courseGateway.GetAllSemester();
        }

        public string SaveCourse(Course course)
        {
           string message;
            course.CourseCode = course.CourseCode.Trim();
            course.Name = course.Name.Trim();
            if(course.Description!=null)
            course.Description = course.Description.Trim();
           
           int result =  courseGateway.SaveCourse(course);
           if (result == -1)
           {
               message = "Course Already Exists with this Code or Name";
           }
           else if (result > 0)
           {
               message = "Course Insert Successfull";
           }
           else
           {
               message = "Course Insert Failed";

           }
           return message;
        }
    }
}