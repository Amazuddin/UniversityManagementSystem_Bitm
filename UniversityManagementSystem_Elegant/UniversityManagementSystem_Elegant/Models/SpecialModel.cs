using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class SpecialModel
    {
        public RegisterStudent Student { get; set; }
        public List<ViewCourses> Result { get; set; }
    }
}