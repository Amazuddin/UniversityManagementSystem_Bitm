using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class RegisterStudentManager
    {
        public string Register(RegisterStudent registerstudent)
        {
            RegisterStudentGateway registerstudentgateway=new RegisterStudentGateway();
            registerstudent.StudentName = registerstudent.StudentName.Trim();
            registerstudent.StudentAddress = registerstudent.StudentAddress.Trim();
            bool isemailExists = registerstudentgateway.IsEmailExists(registerstudent.StudentEmail);
            bool iscontactExists = registerstudentgateway.IsContactExists(registerstudent.StudentContactNo);
            if (isemailExists||iscontactExists)
            {
                return "Unsuccessful";
            }
            else
            {
                int rowEffected = registerstudentgateway.Register(registerstudent);
                if (rowEffected > 0)
                {
                    return "Registered Successfully";
                }
                else
                {
                    return "Unsuccessful";
                }
            }
            
        }
        public List<Department> GetDepartments()
        {
            CourseAssignTeacherGateway courseAssignTeacherGateway = new CourseAssignTeacherGateway();
            return courseAssignTeacherGateway.GetDepartments();
        }
        public List<RegisterStudent> GetAllStudentId()
        {
            RegisterStudentGateway registerstudentgateway = new RegisterStudentGateway();
            return registerstudentgateway.GetAllStudentsId();
        }

        public RegisterStudent GetAllStudentsIdPdf(int id)
        {
            RegisterStudentGateway registerstudentgateway = new RegisterStudentGateway();
            return registerstudentgateway.GetAllStudentsIdPdf(id);
        }
    }
}