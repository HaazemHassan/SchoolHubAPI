using System.ComponentModel.DataAnnotations;

namespace School.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmetsSubjects = new HashSet<DepartmentSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        public int SubID { get; set; }

        [StringLength(500)]
        public string SubjectName { get; set; }
        public int? Period { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmetsSubjects { get; set; }
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

    }
}
