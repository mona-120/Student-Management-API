using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Student_Management_API.Entities;

namespace Student_Management_API.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });
            builder.Property(e => e.EnrollmentDate).HasDefaultValueSql("SYSDATETIME()");

            builder.HasOne(e=> e.Course)
                .WithMany(c=> c.Enrollments)
                .HasForeignKey(e=> e.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedData());
        }

        private static List<Enrollment> SeedData()
        {
            return new List<Enrollment>
            {
                new Enrollment{StudentId = 1 , CourseId = 1,Grade = 90},
                new Enrollment{StudentId = 2 , CourseId = 1,Grade = 85},
                new Enrollment{StudentId = 3 , CourseId = 2,Grade = null},
                new Enrollment{StudentId = 1 , CourseId = 4,Grade = 80},
                new Enrollment{StudentId = 5 , CourseId = 3,Grade = null},
            };
        }
    }
}
