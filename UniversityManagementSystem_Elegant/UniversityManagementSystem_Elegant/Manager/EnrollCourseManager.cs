using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;


namespace UniversityManagementSystem_Elegant.Manager
{
    public class EnrollCourseManager
    {
        EnrollCourseGateway enrollCourseGateway = new EnrollCourseGateway();
        
        public RegisterStudent GetAllStudentById(int id)
        {
            return enrollCourseGateway.GetAllStudentById(id);
        }

        public List<Course> GetAllCourse(int id,int sId)
        {
            return enrollCourseGateway.GetAllCourse(id,sId);
        }

        public string Save(EnrollCourse enrollCourse)
        {
            bool isCourseExist = enrollCourseGateway.IsCourseExist(enrollCourse.CourseId, enrollCourse.StudentId);
            if (isCourseExist)
            {
                return "Sorry,Course already selected";
            }
            else
            {
                int rowAffected = enrollCourseGateway.Save(enrollCourse);
                if (rowAffected > 0)
                {
                    return "Data Saved";
                }
                return "Data is not Saved";
            }    
        }
        public string UnallocateStudent()
        {
            int res = enrollCourseGateway.UnassignCourseStudent();
            if (res > 0)
                return "Course Unassign from student successful";
            else
            {
                return "Course Unassign from student failed";
            }
        }

    }
}