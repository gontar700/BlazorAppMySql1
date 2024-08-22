using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class LessonListB : List<LessonB>
    {
        public LessonListB() { }

        public LessonListB(IEnumerable<LessonB> list) : base(list) { }

        public LessonListB(IEnumerable<BaseEntity> list) : base(list.Cast<LessonB>().ToList()) { }

    }
}
