using DataAccess.Model;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataAccess.ViewModel
{
    
    public class BaseDB
    {
        private readonly string connectionString;

        static private CityList ?citylist = null;

        public BaseDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Get all Students
        public List<Student> GetStudents()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var students = connection.Query<Student>("SELECT * FROM people as T1 inner join student as T2 on T1.ID = T2.ID;").ToList();
                return students;
            }
        }
        //Get city by id
        public City? selectCityById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (citylist == null)
                {
                    var cities = connection.Query<City>("SELECT * FROM city;").ToList();
                    citylist = new CityList(cities);
                }

                City ?city = citylist.Find(item => item.Id == id);
                return city;
            }
        }

        //Get all Lessons
        public LessonListB GetLessons()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var lessons = connection.Query<LessonB>("select t2.FirstName,t2.LastName, subject, t1.Grade, t4.FirstName as lectName, " +
                    "t4.LastName as lectLName from lessons as t1 inner join people as t2 on t1.Student=t2.ID inner join course as t3 on " +
                    "t1.Course=t3.ID inner join people as t4 on t1.Lecturer=t4.ID ORDER BY FirstName ASC;").ToList();
                return new LessonListB(lessons);
            }

        }

        //Get all lesson for student or lecturer
        public LessonList GetLessonsById(int ?studentId,int ?lecturerId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (studentId != null)
                {
                    var lessons = connection.Query<Lesson>("SELECT * FROM lessons WHERE Student="+ studentId+";").ToList();
                    return new LessonList(lessons);
                }
                else
                {
                    var lessons = connection.Query<Lesson>("SELECT * FROM lessons WHERE Lecturer=" + lecturerId + ";").ToList();
                    return new LessonList(lessons);
                }
            }
            
        }
    }
}
//QuerySingleOrDefault<T>()