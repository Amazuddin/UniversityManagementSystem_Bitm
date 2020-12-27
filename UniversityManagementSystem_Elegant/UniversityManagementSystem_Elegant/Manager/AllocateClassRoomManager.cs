using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem_Elegant.Gateway;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Manager
{
    public class AllocateClassRoomManager
    {
        private DepartmentGateway departmentGateway;
        private CourseAssignTeacherGateway courseAssignTeacher;
        private AllocateClassRoomGateway allocateClassRoom;
        public AllocateClassRoomManager()
        {
            departmentGateway=new DepartmentGateway();
            courseAssignTeacher=new CourseAssignTeacherGateway();
            allocateClassRoom = new AllocateClassRoomGateway();
        }

        public List<Department> GetAllDepartment()
        {
            return departmentGateway.GetAllDepartment();
        }

        public List<Course> GetCourseByDeptId(int deptId)
        {
            return courseAssignTeacher.GetCourseByDeptId(deptId);
        }
        public List<Day> GetAllDay()
        {
            return allocateClassRoom.GetAllDay();
        }
        public List<Room> GetAllRoom()
        {
            return allocateClassRoom.GetAllRoom();
        }

        public string SaveClassRoom(AllocateClassroom allocateClassroom)
        {
    
            int result= allocateClassRoom.SaveClassRoom(allocateClassroom);
            if (result == -1)
                return "Room is not available at these time";
            else if (result > 0)
                return "Class room assigned successfully";
            else
            {
                return "Class room assigned failed";
            }
        }

        public string UnallocateClassRoom()
        {
            int res = allocateClassRoom.UnallocateClassRoom();
            if (res > 0)
                return "Class room unallocated";
            else
            {
                return "Class room unallocation failed";
            }
        }

    }
}