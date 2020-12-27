using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class EnrollCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegTime { get; set; }
    }
}