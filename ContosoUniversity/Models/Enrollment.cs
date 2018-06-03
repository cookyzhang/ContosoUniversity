using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A,B,C,D,E,F 
    }
    public class Enrollment
    {
        //默认情况下，EF Core 将名为 ID 或 classnameID 的属性视为主键
        public int EnrollmentID { get; set; }
        //外键属性命名为 <primary key property name>
        public int CourseID { get; set; }
        //<navigation property name><primary key property name>，EF Core 会将其视为外键
        public int StudentID { get; set; }
        //Grade 声明类型后的?表示 Grade 属性可以为 null。
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
