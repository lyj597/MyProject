using System;
using System.Collections.Generic;
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
}
