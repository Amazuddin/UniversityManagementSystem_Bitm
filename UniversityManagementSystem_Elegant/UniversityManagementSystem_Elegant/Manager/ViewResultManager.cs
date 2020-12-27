using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class ViewResultManager
    {
        private ViewResultGateway viewResultGateway;

        public ViewResultManager()
        {
            viewResultGateway=new ViewResultGateway();
        }

        public List<ViewCourses> GetResultById(int studentId)
        {
            return viewResultGateway.GetResultById(studentId);
        }
    }
}