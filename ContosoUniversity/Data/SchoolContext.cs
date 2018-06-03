using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{

    /*
     * 数据库上下文类是为给定数据模型协调 EF Core 功能的主类。
     * 此代码会为每个实体集创建一个 DbSet 属性。 在 EF Core 术语中：
       实体集通常对应一个数据库表。
       实体对应表中的行。
    */
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
        /*
         * 创建数据库时，EF Core 会创建名称与 DbSet 属性名相同的表。 
         * 集合的属性名通常采用复数形式
         */
        //可省略
        public DbSet<Course> Courses { get; set; }
        //可省略
        public DbSet<Enrollment> Enrollments { get; set; }
        //EF Core 隐式包含了它们，因为 Student 实体引用 Enrollment 实体，而 Enrollment 实体引用 Course 实体
        public DbSet<Student> Students { get; set; }

        //指定单数形式的表名称
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }

    }
}
