using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class ViewClassScheduleManage
    {
        ViewClassScheduleGateway viewClassScheduleGateway = new ViewClassScheduleGateway();

        public List<Department> GetDepartments()
        {
            
            return viewClassScheduleGateway.GetDepartments();
        }

        public List<ViewClassSchedule> GetClassSchedules(int deptId)
        {
            return viewClassScheduleGateway.GetClassSchedules(deptId);
        }
    }
}