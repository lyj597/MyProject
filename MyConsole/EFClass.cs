using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyConsole
{
    public class Person {
       [Key] //主键 
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Course
    {
        [Key] //主键 
        public Guid Id { get; set; }
        [ForeignKey("Person")] //外键
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }

        public virtual ICollection<TestCourse> TestCourses { get; set; }
    }

    public class TestCourse
    {
        [Key] //主键 
        public Guid Id { get; set; }

        [ForeignKey("Course")] //外键
        public Guid? CourseId { get; set; }
        public string Name { get; set; }
    }

    public class EFClass { }

    
}
