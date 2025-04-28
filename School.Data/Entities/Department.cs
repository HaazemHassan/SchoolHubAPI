using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }

        #region Properties
        [Key]
        public int DID { get; set; }
        [StringLength(500)]
        public string DName { get; set; }
        public int? SupervisorId { get; set; }
        #endregion



        #region Relations
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }


        [InverseProperty(nameof(Instructor.InstructorDepartment))]
        public virtual ICollection<Instructor> Instructors { get; set; }


        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructor.SupervisorDepartment))]
        public virtual Instructor? Supervisor { get; set; }

        #endregion

    }
}
