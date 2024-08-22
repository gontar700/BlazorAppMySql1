using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class City:BaseEntity
    {
        private string cityName;
        public string CityName { get => cityName; set => cityName = value; }


    }
}
