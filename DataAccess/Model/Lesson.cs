using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Lesson:BaseEntity
    {
        private int lecturer;
        private int student;
        private int grade;

        public int Lecturer { get => lecturer; set => lecturer = value; }
        public int Student { get => student; set => student = value; }
        public int Grade { get => grade; set => grade = value; }
    }
}
