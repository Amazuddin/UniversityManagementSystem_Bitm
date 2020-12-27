using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class SaveStudentResultManager
    {
        SaveStudentResultGateway saveStudentResultGateway=new SaveStudentResultGateway();
        public RegisterStudent GetStudentAllinfoById(int id)
        {
            return saveStudentResultGateway.GetStudentAllinfoById(id);
        }

        public List<Grade> GetGrades()
        {
            return saveStudentResultGateway.GetGrades();
        }
        public List<Course> GetAllCourse(int id)
        {

            return saveStudentResultGateway.GetAllCourse(id);
        }

        public string Save(SaveStudentResult saveStudentResult)
        {
            int roweffect = saveStudentResultGateway.Save(saveStudentResult);
            if (roweffect>0)
            {
                return "Student Result Saved Successfully";
            }
            else
            {
                return "Student Result Not Saved";
            }
        }
    }
}