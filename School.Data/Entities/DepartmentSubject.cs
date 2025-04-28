using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class DepartmentSubject
    {
        [Key]
        public int DID { get; set; }

        [Key]
        public int SubID { get; set; }

        [ForeignKey(nameof(DID))]
        public virtual Department? Department { get; set; }

        [ForeignKey(nameof(SubID))]
        public virtual Subject? Subject { get; set; }
    }
}
