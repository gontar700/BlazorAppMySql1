using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Lecturer : People
    {
        private LessonList lessons;

        public LessonList Lessons { get => lessons; set => lessons = value; }
    }
}
