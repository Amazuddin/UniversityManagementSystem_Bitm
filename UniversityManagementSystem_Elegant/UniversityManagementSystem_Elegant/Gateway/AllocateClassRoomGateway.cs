using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using UniversityManagementSystem_Elegant.Models;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class AllocateClassRoomGateway:BaseGateway
    {
        public List<Room> GetAllRoom()
        {
            string query = "SELECT * FROM Room";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Room> rooms = new List<Room>();
            while (reader.Read())
            {
                Room room = new Room();
                room.RoomId = (int)reader["room_id"];
                room.RoomNo = reader["room_no"].ToString();

                rooms.Add(room);
            }
            connection.Close();
            return rooms;
        }
        public List<Day> GetAllDay()
        {
            string query = "SELECT * FROM Day";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Day> days = new List<Day>();
            while (reader.Read())
            {
                Day day = new Day();
                day.DayId = (int)reader["day_id"];
                day.DayName = reader["day_name"].ToString();
                day.DayShortForm = reader["day_shortform"].ToString();
                days.Add(day);
            }
            connection.Close();
            return days;
        }

        public int SaveClassRoom(AllocateClassroom classroom)
        {

            string f = classroom.TimeFrom.ToString("HH:mm");
            string t = classroom.TimeTo.ToString("HH:mm");

            if (isValid(classroom) == 0)
            {

                string query = "INSERT INTO ClassRoomAssign(department_id,course_id,room_id,day_id,from_time,to_time)" +
                               "VALUES(" + classroom.DepartmentId + "," + classroom.CourseId + "," + classroom.RoomId +
                               "," + classroom.DayId + ",'" + f + "','" + t + "')";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int rowAffect = command.ExecuteNonQuery();
                connection.Close();
                return rowAffect;
            }
            else
            {
                return -1;
            }

        }

        public int isValid(AllocateClassroom classroom)
        {
            string fTime, tTime, ftype, ttype;
            int fHour, fMin, tHour, tMin;

            int room = classroom.RoomId;

            string f = classroom.TimeFrom.ToString("HH:mm");
            int fH=Convert.ToInt32(f[0].ToString()+f[1].ToString());
            int fM=Convert.ToInt32(f[3].ToString()+f[4].ToString());
            int from = fH*60 + fM;


            string t = classroom.TimeTo.ToString("HH:mm");
            int tH=Convert.ToInt32(t[0].ToString()+t[1].ToString());
            int tM=Convert.ToInt32(t[3].ToString()+t[4].ToString());
            int to = tH*60 + tM;

            int Failed = 0;



            string query = "SELECT from_time,to_time FROM  ClassRoomAssign WHERE day_id=" +
                           classroom.DayId + "AND room_id=" + room;

            List<AllocateClassroom> classrooms = new List<AllocateClassroom>();
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                fTime = reader["from_time"].ToString();
                tTime = reader["to_time"].ToString();
                fHour = Convert.ToInt32(fTime[0].ToString() + fTime[1].ToString());
                fMin = Convert.ToInt32(fTime[3].ToString() + fTime[4].ToString());
                int checkFrom = fHour*60 + fMin;

                tHour = Convert.ToInt32(tTime[0].ToString() + tTime[1].ToString());
                tMin = Convert.ToInt32(tTime[3].ToString() + tTime[4].ToString());
                int checkTo = tHour*60 + tMin;

                if ((checkFrom < from && checkTo <= from))
                {
                    continue;
                }
                else if (checkFrom > from && checkFrom >= to)
                {
                    continue;
                }
                else
                {
                    Failed = 1;
                    break;
                }


            }
            reader.Close();
            connection.Close();
            return Failed;

        }

        public int UnallocateClassRoom()
        {
            string query = "DELETE FROM ClassRoomAssign";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        
    }
}

/*

9:30 10:30       
570 630
 
  
*/
