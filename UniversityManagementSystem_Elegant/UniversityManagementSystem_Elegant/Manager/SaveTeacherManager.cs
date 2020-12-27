using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class SaveTeacherManager
    {
        public List<Department> GetDepartments()
        {
            SaveTeacherGateway saveTeacherGateway=new SaveTeacherGateway();
            return saveTeacherGateway.GetDepartments();
        }
        public List<Designation> GetDesignation()
        {
            SaveTeacherGateway saveTeacherGateway = new SaveTeacherGateway();
            return saveTeacherGateway.GetDesignation();
        }
        public string SaveTeacher(Teacher teacher)
        {
            SaveTeacherGateway saveTeacherGateway = new SaveTeacherGateway();
            teacher.Name = teacher.Name.Trim();
            teacher.Address = teacher.Address.Trim();
            teacher.Email = teacher.Email.Trim();
            string message;
            int result = saveTeacherGateway.SaveTeacher(teacher);
            if (result == -1)
            {
                message = "Teacher Already Exists with this Email or Contact";
            }
            else if (result > 0)
            {
                message = "Teacher Insert Successfull";
            }
            else
            {
                message = "Teacher Insert Failed";

            }
            return message;

        }
    }
}