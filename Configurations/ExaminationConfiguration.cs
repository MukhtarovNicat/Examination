using Exam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Configurations;

public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
{
    public void Configure(EntityTypeBuilder<Examination> builder)
    {
        //builder.Property(x => x.LessonCode)
        //    .HasColumnType("char(3)")
        //    .IsRequired();

        //builder.Property(x => x.StudentNumber)
        //    .IsRequired();

        builder.Property(x => x.ExamDate)
            .IsRequired();

        builder.Property(x => x.Grade)
            .IsRequired();

        builder.HasOne(x => x.Lesson)
            .WithMany(x => x.Examinations)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.Examinations)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
