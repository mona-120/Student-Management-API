using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Student_Management_API.Entities;

namespace Student_Management_API.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(c => c.Title)
                .IsUnique();
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.HasData(SeedData());
        }

        private static List<Course> SeedData()
        {
            return new List<Course>
            {
                new Course{ CourseId = 1, Title = "C#",Credits = 40, Description = "Fundamentals of C# and Object-Oriented Programming (OOP)."},
                new Course{ CourseId = 2, Title = "Linq",Credits = 15, Description = "Data querying, filtering, and manipulation techniques."},
                new Course{ CourseId = 3,Title = "EFCore",Credits = 25, Description = "Database management, migrations, and relationship mapping."},
                new Course{ CourseId = 4, Title = "MVC",Credits = 30, Description = "Building structured web applications using Model-View-Controller."},
                new Course{ CourseId = 5,Title = "API",Credits = 35, Description = "Building secure RESTful web services with ASP.NET Core."}
            };
        }
    }
}
