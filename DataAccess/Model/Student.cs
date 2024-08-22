using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Student : People
    {
        private LessonList lessons;

        public LessonList FinishedCourse
        {
            get
            {
                List<Lesson> lst = this.lessons.FindAll(lesson => lesson.Grade > 55).ToList();
                return new LessonList(lst);
            }
        }

        public LessonList Lessons { get => lessons; set => lessons = value; }
    }
}
