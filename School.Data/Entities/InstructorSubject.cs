using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class InstructorSubject
    {
        [Key]
        public int InsID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey(nameof(InsID))]
        public Instructor instructor { get; set; }

        [ForeignKey(nameof(SubID))]
        public Subject Subject { get; set; }
    }
}
