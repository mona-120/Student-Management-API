using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Middlewares;
using Student_Management_API.Services;

namespace Student_Management_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(op =>
            {
                // Prevent cycles when return more than one table
                op.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("StudentDb")));

            // Register Sevices
            // StudentService
            builder.Services.AddScoped<IStudentService, StudentService>();
            // Course Service
            builder.Services.AddScoped<ICourseService, CourseService>();
            //EnrollmentService
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            builder.Services.AddProblemDetails();

            var app = builder.Build();
            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
