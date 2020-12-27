using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class AllocateClassroom
    {
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public int DayId { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }

    }
}