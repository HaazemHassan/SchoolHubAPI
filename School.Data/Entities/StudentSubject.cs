using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudID { get; set; }

        [Key]
        public int SubID { get; set; }

        public decimal? Degree { get; set; }


        #region Relations
        [ForeignKey(nameof(StudID))]
        public virtual Student? Student { get; set; }

        [ForeignKey(nameof(SubID))]
        public virtual Subject? Subject { get; set; }
        #endregion
    }
}
