using DBLayer;
using Evaluation_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Repositories {
    public class StudentRepository {

        public static Student GetStudent(int ID) {
            Student student = null;

            string sql = $"SELECT * FROM Students WHERE Id = {ID}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows) {
                reader.Read();
                student = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();
            return student;
        } //public static Student GetStudent

        public static List<Student> GetStudents() {
            var students = new List<Student>();

            string sql = "SELECT * FROM Students";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            while (reader.Read()) {
                Student student = CreateObject(reader);
                students.Add(student);
            }

            reader.Close();
            DB.CloseConnection();

            return students;
        } //public static List

        private static Student CreateObject(SqlDataReader reader) {
            int ID = int.Parse(reader["Id"].ToString());
            string FirstName = reader["FirstName"].ToString();
            string LastName = reader["LastName"].ToString();
            int Grade = 0;
            try {
                Grade = int.Parse(reader["Grade"].ToString());

            } catch (Exception) {
            }


            var Student =  new Student {
                ID = ID,
                FirstName = FirstName,
                LastName = LastName,
                Grade = Grade
            };

            return Student;
        } //private static Student
    }
}
