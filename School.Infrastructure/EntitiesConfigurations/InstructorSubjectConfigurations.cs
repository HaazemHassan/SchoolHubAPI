using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.Infrastructure.EntitiesConfigurations
{
    public class InstructorSubjectConfigurations : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.InsID });
        }
    }
}
