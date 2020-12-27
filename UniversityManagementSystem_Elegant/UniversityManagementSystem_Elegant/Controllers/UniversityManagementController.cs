using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.DataHandler;
using Rotativa;
using UniversityManagementSystem_Elegant.Manager;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Controllers
{
    public class UniversityManagementController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private CourseAssignTeacherManager courseAssignTeacherManager;
        private ViewCourseManager viewCourseManager; 
        private RegisterStudentManager registerStudentManager;
        private AllocateClassRoomManager allocateClassRoom;
        private SaveTeacherManager saveTeacherManager;
        private ViewClassScheduleManage viewClassScheduleManager;
        private EnrollCourseManager enrollCourseManager;
        private SaveStudentResultManager saveStudentResultManager;
        public UniversityManagementController()
        {
            departmentManager=new DepartmentManager();
            courseManager=new CourseManager();
            courseAssignTeacherManager = new CourseAssignTeacherManager();
            viewCourseManager = new ViewCourseManager();
            registerStudentManager = new RegisterStudentManager();
            allocateClassRoom=new AllocateClassRoomManager();
            saveTeacherManager = new SaveTeacherManager();
            viewClassScheduleManager = new ViewClassScheduleManage();
            enrollCourseManager = new EnrollCourseManager();
            saveStudentResultManager=new SaveStudentResultManager();
        }
        //
        // GET: /UniversityManagement/
        public ActionResult SaveDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            string message = departmentManager.SaveDepartment(department);
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ViewDepartment()
        {
            List<Department> departments = departmentManager.GetAllDepartment();
            return View(departments);
        }

        public ActionResult SaveCourse()
        {
            List<Semester> semesters = courseManager.GetAllSemester();
            List<Department> departments = departmentManager.GetAllDepartment();
            ViewBag.Semester = semesters;
            ViewBag.Department = departments;
            return View();
        }
        [HttpPost]
        public ActionResult SaveCourse(Course course)
        {
            string message = courseManager.SaveCourse(course);
            ViewBag.Message = message;
            List<Semester> semesters = courseManager.GetAllSemester();
            List<Department> departments = departmentManager.GetAllDepartment();
            ViewBag.Semester = semesters;
            ViewBag.Department = departments;
            return View();
        }
        

        //....................Amaz....................
        [HttpGet]
        public ActionResult CourseAssignTeacher()
        {
            ViewBag.Message = null;
            ViewBag.Department = courseAssignTeacherManager.GetDepartments();
            return View();
        }
        [HttpPost]
        public JsonResult GetTeacherById(int id)
        {
           
            List<Teacher> teacher = courseAssignTeacherManager.GetTeachersById(id);
            return Json(teacher);
        }
        [HttpPost]
        public JsonResult GetCourseById(int id)
        {
            List<Course> course = courseAssignTeacherManager.GetCourseById(id);
            return Json(course);
        }

        [HttpPost]
        public JsonResult GetCreditTakenById(int id)
        {
            Teacher teacher = new Teacher();
            teacher = courseAssignTeacherManager.GetCreditTakenById(id);
            return Json(teacher);
        }
        public JsonResult GetCourseNameCreditById(int id)
        {
            Course course = new Course();
            course = courseAssignTeacherManager.GetCourseNameCreditById(id);
            return Json(course);
        }
        [HttpPost]
        public ActionResult CourseAssignTeacher(CourseAssignTeacher courseassignteacher, int Remainingcredit, int Coursecredit)
        {
            ViewBag.Department = courseAssignTeacherManager.GetDepartments();
            ViewBag.Message = courseAssignTeacherManager.Assign(courseassignteacher, Remainingcredit, Coursecredit);
            return View();
        }
        [HttpGet]
        public ActionResult ShowCourse()
        {
            ViewBag.Department = viewCourseManager.GetDepartments();
            return View();
        }


        public JsonResult GetAllInfoCoursesesById(int id)
        {
            List<ViewCourses> viewcourseses = viewCourseManager.GetAllInfoCoursesesById(id);
            return Json(viewcourseses, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RegisterStudent()
        {
            ViewBag.Message = null;
            ViewBag.Department = registerStudentManager.GetDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterStudent(RegisterStudent registerstudent)
        {
            ViewBag.Department = registerStudentManager.GetDepartments();
            ViewBag.Message = registerStudentManager.Register(registerstudent);
            return View();
        }

        public ActionResult AllocateClassRoom()
        {
            
            ViewBag.Department=allocateClassRoom.GetAllDepartment();
            ViewBag.Room = allocateClassRoom.GetAllRoom();
            ViewBag.Day = allocateClassRoom.GetAllDay();

            return View();
        }

        [HttpPost]
        public ActionResult AllocateClassRoom(AllocateClassroom allocateClassroom)
        {

            string message = allocateClassRoom.SaveClassRoom(allocateClassroom);
            ViewBag.K = message;
            ViewBag.Department = allocateClassRoom.GetAllDepartment();
            ViewBag.Room = allocateClassRoom.GetAllRoom();
            ViewBag.Day = allocateClassRoom.GetAllDay();

            return View();
        }

        public JsonResult GetCourseByDeptId(int id)
        {
            List<Course> courses = allocateClassRoom.GetCourseByDeptId(id);
            return Json(courses);
        }

        //4
        public ActionResult SaveTeacher()
        {
            ViewBag.Department = saveTeacherManager.GetDepartments();
            ViewBag.Designation = saveTeacherManager.GetDesignation();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher teacher)
        {
            ViewBag.Department = saveTeacherManager.GetDepartments();
            ViewBag.Designation = saveTeacherManager.GetDesignation();
            ViewBag.Message = saveTeacherManager.SaveTeacher(teacher);
            return View();
        }
        [HttpGet]
        public ActionResult ViewClassSchedule()
        {
            ViewBag.Department = viewClassScheduleManager.GetDepartments();
            return View();
        }


        public JsonResult GetAllInfoClassScheduleById(int id)
        {
            List<ViewClassSchedule> scheduls = viewClassScheduleManager.GetClassSchedules(id);
            return Json(scheduls, JsonRequestBehavior.AllowGet);
        }
        //10
        public ActionResult EnrollCourse()
        {
            ViewBag.RegNo = registerStudentManager.GetAllStudentId();
            return View();
        }
        [HttpPost]
        public ActionResult EnrollCourse(EnrollCourse enrollCourse)
        {
            string message = enrollCourseManager.Save(enrollCourse);
            ViewBag.Message = message;
            ViewBag.RegNo = registerStudentManager.GetAllStudentId();

            return View();
        }
        public JsonResult GetStudentDetailsbyRegNo(int id)
        {
            RegisterStudent student = enrollCourseManager.GetAllStudentById(id);


            return Json(student);
        }

        public JsonResult GetCoursebyRegNo(int id)
        {
            RegisterStudent student = enrollCourseManager.GetAllStudentById(id);
            List<Course> courses = enrollCourseManager.GetAllCourse(student.DepartmentId,id);
            return Json(courses);
        }
        
        //11
        public ActionResult SaveStudentResult()
        {
            ViewBag.RegNo = registerStudentManager.GetAllStudentId();
            ViewBag.Grades = saveStudentResultManager.GetGrades();
            return View();
        }

        [HttpPost]
        public ActionResult SaveStudentResult(SaveStudentResult saveStudentResult)
        {

            ViewBag.Message = saveStudentResultManager.Save(saveStudentResult);
            ViewBag.RegNo = registerStudentManager.GetAllStudentId();
            ViewBag.Grades = saveStudentResultManager.GetGrades();
            return View();
        }
        public JsonResult GetStudentAllinfoById(int id)
        {
            RegisterStudent student=new RegisterStudent();
            student = saveStudentResultManager.GetStudentAllinfoById(id);
            return Json(student);
        }
        public JsonResult GetCoursebyStudentRegNo(int id)
        {
            List<Course> courses = saveStudentResultManager.GetAllCourse(id);
            return Json(courses);
        }


        public ActionResult ViewResult()
        {
            ViewBag.Student = registerStudentManager.GetAllStudentId();
            return View();
        }

        public JsonResult GetResult(int id)
        {
            ViewResultManager viewResult=new ViewResultManager();
            List<ViewCourses> result = viewResult.GetResultById(id);
            return Json(result);
        }

        public ActionResult MakePdf(int StudentId)
        {
            SpecialModel model=new SpecialModel();
            model.Student = registerStudentManager.GetAllStudentsIdPdf(StudentId);
            
           
            ViewResultManager viewResult = new ViewResultManager();
            List<ViewCourses> result = viewResult.GetResultById(StudentId);
            model.Result = result;
            ViewBag.M = model;
           
            return View();

        }
        public ActionResult ConvertToPDF(int studentId)
        {
            var printpdf = new ActionAsPdf("MakePdf", new{StudentId=studentId}){FileName = "ResultSheet"};
            return printpdf;
        }



        public ActionResult UnassignCourses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourses(string a)
        {
            ViewBag.Message = courseAssignTeacherManager.UnallocateTeacher();
                ViewBag.Message2=enrollCourseManager.UnallocateStudent();
            return View();
        }
        //14
        public ActionResult UnallocateAllClassrooms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnallocateAllClassrooms(string data)
        {
            ViewBag.Message=allocateClassRoom.UnallocateClassRoom();
            return View();
        }
        
	}
}