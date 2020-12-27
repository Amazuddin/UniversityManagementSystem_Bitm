using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class RegisterStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RegNo { get; set; }
        
        public string StudentEmail { get; set; }
        public string StudentContactNo { get; set; }
        public DateTime Date { get; set; }
        public string StudentAddress { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Department Department { get; set; }
    }
}