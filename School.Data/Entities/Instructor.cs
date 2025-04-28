using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class Instructor
    {

        public Instructor()
        {
            InsturctorSubjects = new HashSet<InstructorSubject>();
            Instructors = new HashSet<Instructor>();

        }


        [Key]
        public int InsID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal Salary { get; set; }
        public int? DID { get; set; }


        public virtual ICollection<InstructorSubject> InsturctorSubjects { get; set; }

        [ForeignKey(nameof(DID))]
        [InverseProperty(nameof(Department.Instructors))]
        public virtual Department? InstructorDepartment { get; set; }  //The department of the normal instructor

        [InverseProperty(nameof(Department.Supervisor))]
        public virtual Department? SupervisorDepartment { get; set; }   //The department that the supervisor manage


        //self relation
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructors))]
        public virtual Instructor? Supervisor { get; set; }

        [InverseProperty(nameof(Supervisor))]
        public virtual ICollection<Instructor> Instructors { get; set; }   //instructors which the supvervisor manage

    }
}
