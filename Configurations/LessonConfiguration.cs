using Exam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(x => x.LessonCode)
            .HasColumnType("char(3)")
            .IsRequired();

        builder.Property(x => x.LessonName)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(x => x.ClassLevel)
            .IsRequired();

        builder.Property(x => x.TeacherName)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(x => x.TeacherSurname)
            .HasColumnType("varchar(20)")
            .IsRequired();
    }
}
