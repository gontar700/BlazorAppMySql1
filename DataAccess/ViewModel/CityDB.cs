using Dapper;
using MySql.Data.MySqlClient;
using DataAccess.Model;

namespace DataAccess.ViewModel
{
    
    public class CityDB
    {
        private readonly string connectionString;

        public CityDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public City selectCityById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                CityList list = (CityList)connection.Query<City>("SELECT * FROM city where ID=" + id).ToList();
                return list[0];
            }
        }
    }
}
