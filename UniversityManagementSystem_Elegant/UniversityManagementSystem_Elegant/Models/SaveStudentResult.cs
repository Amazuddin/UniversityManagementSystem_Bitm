using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class SaveStudentResult
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentDepartment { get; set; }
        public string StudentReg{ get; set; }
        public string StudentEmail{ get; set; }
        public string GradeLetter { get; set; }
        public int GradeId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}