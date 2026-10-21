using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Student_Management_API.Entities;

namespace Student_Management_API.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);
            builder.Property(s=> s.FullName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(s=> s.Email)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.HasIndex(s => s.Email)
                   .IsUnique();
            builder.Property(s => s.EnrollmentDate).HasDefaultValueSql("SYSDATETIME()");

            builder.HasData(SeedData());
        }

        private static List<Student> SeedData()
        {
            return new List<Student>
            {
                new Student{ StudentId = 1,FullName ="Youssef Karim",Email = "youssef.karim@example.com",DateOfBirth = null},
                new Student{ StudentId = 2,FullName ="Mariam Adel",Email = "mariam.adel@example.com",DateOfBirth = DateOnly.Parse("2003-03-15")},
                new Student{ StudentId = 3,FullName ="Liam Carter",Email = "liam.carter@example.com",DateOfBirth = DateOnly.Parse("2005-07-20")},
                new Student{ StudentId = 4,FullName ="Nour El-Din",Email = "nour.eldin@example.com",DateOfBirth = DateOnly.Parse("2006-01-12")},
                new Student{ StudentId = 5,FullName ="Sofia Martinez",Email = "sofia.martinez@example.com",DateOfBirth = null},
            };
        }
    }
}
