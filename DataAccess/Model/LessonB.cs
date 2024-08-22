using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class LessonB : BaseEntity
    {
        private string firstName;
        private string lastName;
        private int grade;
        private string subject;
        private string lectName;
        private string lectLName;


        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Grade { get => grade; set => grade = value; }
        public string Subject { get => subject; set => subject = value; }
        public string LectName { get => lectName; set => lectName = value; }
        public string LectLName { get => lectLName; set => lectLName = value; }

    }
}
