using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNetConsole.EF
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


    public class Blog
    {
        public Blog() {
            Posts = new HashSet<Post>();
        }
        public int BlogId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        public virtual ICollection<Post> Posts { get; private set; }
    }

    public class Post
    {
        public Post() {
            Comments = new HashSet<Comment>();
        }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public string PostDisplay {
            get
            {
                return Title + ":" + Content;
            }
        }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; }
    }

    public class Comment {
        public int CommentId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; private set; }
    }

    public class Passport {
        [Key]
        [Column(Order = 1)]
        public int PassportNumber { get; set; }
        [Key]
        [Column(Order = 2)]
        public string IssuingCountry { get; set; }
        public DateTime Issued { get; set; }

        [MaxLength(10,ErrorMessage = "输入的Expires值不能超过10个字节"),MinLength(5)]
        public DateTime Expires { get; set; }
    }
}
