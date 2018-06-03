using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        //默认情况下，EF Core 将名为 ID 或 classnameID 的属性视为主键
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //如果导航属性包含多个实体，则导航属性必须是列表类型，例如 ICollection<T>
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
