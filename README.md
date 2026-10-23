# Student Management API 🎓

A simple, fully asynchronous RESTful Web API built with **C#, ASP.NET Core**, and **Entity Framework Core (EF Core)**. The application handles student records, course management, and enrollments using modern enterprise software patterns, asynchronous non-blocking operations, and defensive validation.

## 🚀 Features

The system allows to:
- **Add & Update Students:** Applied with strict input validation to guarantee data accuracy, preventing duplicate emails via database constraints and API validation (returning `409 Conflict`).
- **Search Students by Name:** Supports partial name matching rather than strict exact matching via efficient LINQ string operations.
- **Manage Courses:** Validates course properties prior to persisting changes and ensures a course contains no active enrolled students before allowing deletion.
- **Manage Enrollments:** Registers students to courses after ensuring both entities exist, actively preventing duplicate enrollments.
- **Relational Reporting & Aggregations:** Computes the average course grade via direct SQL aggregation (`AverageAsync`), lists all courses for a given student, and retrieves all students within a specific course.

## 🛠 Key Architectural Patterns & Technologies

**Service-Oriented Architecture & DTOs**
- Decouples the HTTP presentation layer (Controllers) from business logic (Services). 
- Utilizes **Data Transfer Objects (DTOs)** to prevent over-posting attacks, and shape the data required by the user.

**High-Performance Query Projections**
- Uses LINQ `.Select()` projections and `.AsNoTracking()` to map database records directly to DTOs in a single optimized SQL query.

**Defensive Validation & Error Handling**
- Combines targeted `try-catch` blocks for business logic with `UseExceptionHandler()` and `AddProblemDetails()` to return standardized JSON `500` errors with **no stack trace**.
- Utilizes strict input validation and **DTOs** for all operations, ensuring users never interact directly with internal database entities.

**Custom Middleware Pipeline**
- Integrates a custom `RequestLoggingMiddleware` using `Stopwatch` to intercept all incoming HTTP traffic.
- Automatically logs the HTTP method, request path, status code, and exact execution time (in milliseconds) for performance monitoring.

**Database Configurations via Fluent API**
- Decouples data annotations from domain models by centralizing database rules, composite keys (for enrollments), and relationships using Fluent API (`IEntityTypeConfiguration<T>`).

## 📂 Project Structure

StudentManagementAPI/
│
├── Controllers/
│   ├── CoursesController.cs          # HTTP endpoints for course operations
│   ├── EnrollmentsController.cs      # HTTP endpoints for student & Courses registrations
│   └── StudentsController.cs         # HTTP endpoints for student operations
│
├── Data/
│   ├── Configurations/
│   │   ├── CourseConfiguration.cs    # Fluent API config for Course constraints
│   │   ├── EnrollmentConfiguration.cs# Fluent API config (Composite keys & relations)
│   │   └── StudentConfiguration.cs   # Fluent API config for Student constraints
│   └── AppDbContext.cs               # EF Core DbContext & connection pipeline
│
├── Dtos/                             # Data Transfer Objects (Request/Response models)
│   ├── CourseDto.cs                  
│   ├── CreateCourseRequest.cs        
│   ├── CreateEnrollmentRequest.cs    
│   ├── StudentDto.cs                 
│   ├── StudentRequest.cs             
│   └── StudentResponse.cs   
│
├── Entities/                         
│   ├── Course.cs                     
│   ├── Enrollment.cs                 # Explicit join entity linking Students and Courses
│   └── Student.cs                    
│
├── Middleware/
│   └── RequestLoggingMiddleware.cs   # Intercepts requests to log Method, Path, and latency
│
├── Migrations/                       # Database schema 
│
├── Services/
│   ├── CourseService.cs              # Course business logic implementation
│   ├── EnrollmentService.cs          # Enrollment logic implementation
│   ├── ICourseService.cs             # Course service contract
│   ├── IEnrollmentService.cs         # Enrollment service contract
│   ├── IStudentService.cs            # Student service contract
│   └── StudentService.cs             # Student business logic implementation
│
├── Students.http                     # Integrated HTTP testing file for all API endpoints
├── appsettings.json                  # Application configurations (Connection Strings)
└── Program.cs                        # DI Container, Services registration, and Middleware pipeline
