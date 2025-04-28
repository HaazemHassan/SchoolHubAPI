using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.Infrastructure.EntitiesConfigurations
{
    public class StudentSubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.StudID });

        }
    }
}
