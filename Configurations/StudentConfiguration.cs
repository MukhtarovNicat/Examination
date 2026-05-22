using Exam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(x => x.Class)
            .IsRequired();
    }
}
