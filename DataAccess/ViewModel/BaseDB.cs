using DataAccess.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using Mysqlx.Crud;

namespace DataAccess.ViewModel
{

    public class BaseDB
    {
        private readonly string connectionString;

        static public CityList citylist;

        public BaseDB(string connectionString)
        {
            this.connectionString = connectionString;

            var connection = new MySqlConnection(connectionString);

            connection.Open();
            if (citylist == null)
            {
                var cities = connection.Query<City>("SELECT * FROM city;").ToList();
                citylist = new CityList(cities);
            }
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
            City? city = citylist.Find(item => item.Id == id);
            return city;
        }

        //Get all Lessons
        public LessonListB GetLessons()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var lessons = connection.Query<LessonB>("select t2.FirstName,t2.LastName, t1.Grade, subject, t4.FirstName as lectName, t4.LastName as lectLName from lessons as t1 inner join people as t2 on t1.Student=t2.ID inner join course as t3 on t1.Course=t3.ID inner join people as t4 on t1.Lecturer=t4.ID;").ToList();
                return new LessonListB(lessons);
            }

        }

        //Get all lesson for student or lecturer
        public LessonList GetLessonsById(int? studentId, int? lecturerId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (studentId != null)
                {
                    var lessons = connection.Query<Lesson>("SELECT * FROM lessons WHERE Student=" + studentId + ";").ToList();
                    return new LessonList(lessons);
                }
                else
                {
                    var lessons = connection.Query<Lesson>("SELECT * FROM lessons WHERE Lecturer=" + lecturerId + ";").ToList();
                    return new LessonList(lessons);
                }
            }

        }

        //Insert student
        public int insertPeople(People people)
        {
            int succeed = 0;
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO people (FirstName, LastName, City, Phone, Email, Password) 
                        VALUES (@FirstName, @LastName, @City, @Phone, @Email, @Password);
                        SELECT LAST_INSERT_ID();";

                    // Execute the query and retrieve the auto-generated ID
                    int newId = connection.ExecuteScalar<int>(sql, new
                    {
                        FirstName = people.FirstName,
                        LastName = people.LastName,
                        City = people.City,
                        Phone = people.Phone,
                        Email = people.Email,
                        Password = randomPassword() // Assuming randomPassword() generates a password
                    });

                    if (newId > 0)
                    {
                        succeed = insertSudent(newId); // Assuming insertStudent is a method to handle further actions
                        if(succeed > 0)
                        {
                            return succeed;
                        }
                        else
                        {
                            string sqlDelete = @"delete from people where ID ="+ newId+";";
                            connection.Query<People>(sqlDelete);
                            return 0;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle MySQL specific exceptions here
                Console.WriteLine($"MySQL Error: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                // Handle general exceptions here
                Console.WriteLine($"General Error: {ex.Message}");
                
            }
            return succeed;
        }

        public int insertSudent(int id)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string insertSql = "INSERT INTO student (ID) VALUES (@ID);";
                    connection.Execute(insertSql, new { ID = id });

                    // Query the student with the specific ID
                    string selectSql = "SELECT * FROM student WHERE ID = @ID;";
                    var student = connection.QueryFirstOrDefault(selectSql, new { ID = id });
                    if (student != null)
                    {
                        rowsAffected = 1;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle MySQL specific exceptions here
                Console.WriteLine($"MySQL Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                // Handle general exceptions here
                Console.WriteLine($"General Error: {ex.Message}");

            }
            return rowsAffected;
        }
        public string randomPassword()
        {
            Random rand = new Random();

            // Choosing the size of string 
            // Using Next() string 
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number. 
                randValue = rand.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }
    }
}

