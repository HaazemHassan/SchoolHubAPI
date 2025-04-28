using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.Infrastructure.EntitiesConfigurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.DID });
        }
    }
}
