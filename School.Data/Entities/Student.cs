using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class Student
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }



        [Key]
        public int StudID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [StringLength(500)]
        public string? Phone { get; set; }
        public int? DID { get; set; }

        [ForeignKey(nameof(DID))]
        public virtual Department? Department { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
