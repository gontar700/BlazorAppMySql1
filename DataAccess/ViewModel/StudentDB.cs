using Dapper;
using MySql.Data.MySqlClient;
using DataAccess.Model;

namespace DataAccess.ViewModel
{
    public class StudentDB 
    {
        private readonly string connectionString;

        public StudentDB(string connectionString)
        {
            this.connectionString = connectionString;   
        }

        public List<Student> GetStudents()
        {
            using(var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var students = connection.Query<Student>("SELECT * FROM people as T1 inner join student as T2 on T1.ID = T2.ID;").ToList();
                return students;
            }
        }
    }
}
