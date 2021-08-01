using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyConsole
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }

    public class Department
    {
        public Department()
        {
            this.Courses = new HashSet<Course>();
        }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }


    [Table("Blogs")]
    public class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }
        public int BlogId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        [NotMapped]
        public DateTime StartDate { get; set; }

        [ForeignKey("TestAA")]
        public int? TestId { get; set; }
        public TestAA TestAA { get; set; }

        public virtual ICollection<Post> Posts { get;  set; }
    }

    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public string PostDisplay
        {
            get
            {
                return Title + ":" + Content;
            }
        }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get;  set; }

        public virtual ICollection<Comment> Comments { get; private set; }
    }

    [Table("TestAA")]
    public class TestAA {
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int TestId { get; set; }

       public string Name { get; set; }

    }

    [Table("TaskTest1")]
    public class TaskTest1 { 
         public Guid Id { get; set; }
        public string Name { get; set; }
    }

    [Table("TaskTest2")]
    public class TaskTest2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; private set; }
    }

    public class Passport
    {
        [Key]
        [Column(Order = 1)]
        public int PassportNumber { get; set; }
        [Key]
        [Column(Order = 2)]
        public string IssuingCountry { get; set; }
        public DateTime Issued { get; set; }

        [MaxLength(10, ErrorMessage = "输入的Expires值不能超过10个字节"), MinLength(5)]
        public DateTime Expires { get; set; }
    }

    /// <summary>
    /// 添加架构
    /// </summary>
    //[Table("Test1",Schema ="dbo")]
    public class Test1 { 
        public int Test1Id { get; set; }
        public string Name { get; set; }   

    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Student : Person
    {
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }

}
