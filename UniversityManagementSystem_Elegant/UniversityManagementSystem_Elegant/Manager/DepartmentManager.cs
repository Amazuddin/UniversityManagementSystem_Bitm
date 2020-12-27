using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway departmentGateway;

        public DepartmentManager()
        {
            departmentGateway=new DepartmentGateway();
        }
        public string SaveDepartment(Department department )
        {
            department.DepartmentCode = department.DepartmentCode.Trim();
            department.DepartmentName = department.DepartmentName.Trim();
            string message;
            int result=departmentGateway.SaveDepartment(department);
            if (result == -1)
            {
                message = "Department Already Exists with this Code or Name";   
            }
            else if(result>0)
            {
                message = "Department Insert Successfull";
            }
            else
            {
                message = "Department Insert Failed";
            
            }
            return message;

        }

        public List<Department> GetAllDepartment()
        {
            return departmentGateway.GetAllDepartment();
        }
    }
}