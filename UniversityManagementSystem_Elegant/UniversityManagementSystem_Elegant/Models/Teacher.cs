using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem_Elegant.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public int Department { get; set; }
        public int Totalcredit{ get; set; }
        public int Takencredit{ get; set; }
      
     
    }
}