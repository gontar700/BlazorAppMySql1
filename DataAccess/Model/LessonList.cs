﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class LessonList : List<Lesson>
    {
        public LessonList() { }

        public LessonList(IEnumerable<Lesson> list): base  (list) { }

        public LessonList(IEnumerable<BaseEntity> list): base(list.Cast<Lesson>().ToList()) { } 

    }
}
