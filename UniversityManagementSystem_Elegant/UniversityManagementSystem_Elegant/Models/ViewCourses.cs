using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class ViewCourses
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double CourseCredit { get; set; }
        public string CourseSemester { get; set; }
        public string CourseTeacher { get; set; }
        public string Grade { get; set; }
        public string GradePoint { get; set; }
    }
}