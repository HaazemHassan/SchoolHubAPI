using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using System.Reflection;

namespace School.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<DepartmentSubject> DepartmetSubjects { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<InstructorSubject> InstructorSubjects { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
