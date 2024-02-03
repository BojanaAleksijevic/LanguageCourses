using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;
using Microsoft.EntityFrameworkCore;
using LanguageCourses.API.Extensions;
using System.Security.Cryptography;

namespace LanguageCourses.API.Data;

public class LanguageCoursesDbContext : DbContext
{
    private readonly HMACSHA512 _hmac;
    private readonly IConfiguration _configuration;

    public LanguageCoursesDbContext(
        DbContextOptions dbContextOptions,
        IConfiguration configuration) : base(dbContextOptions)
    {
        _hmac = new HMACSHA512();
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseUser> Enrollments { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");

            builder
                .HasMany(user => user.Courses)
                .WithMany(course => course.Users)
                .UsingEntity<CourseUser>();



            builder.HasData(
                new User
                {
                    Id = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    FirstName = "Filip",
                    LastName = "Jovanović",
                    Phone = "061 755 8995",
                    Email = "fjovanovic284@gmail.com",
                    Picture = "9150584F-EB77-4A84-A13F-698A581985D8.jpg",
                    PasswordHash = _hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:FilipPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.ADMIN
                },
                new User
                {
                    Id = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    FirstName = "Luka",
                    LastName = "Petrović",
                    Phone = "064 765 9876",
                    Email = "lule19@gmail.com",
                    PasswordHash = _hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:LukaPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.ADMIN
                },
                new User
                {
                    Id = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    FirstName = "Bojana",
                    LastName = "Aleksijević",
                    Phone = "064 784 5668",
                    Email = "boka0404002.ba@gmail.com",
                    PasswordHash = _hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:BojanaPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.ADMIN
                }
            ); ;
        });

        modelBuilder.Entity<Course>(builder =>
        {
            builder.ToTable("Courses");

            builder
                .HasMany(course => course.Lessons)
                .WithOne(lesson => lesson.Course)
                .HasForeignKey(lesson => lesson.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasData
            (
                new Course
                {
                    Id = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    ProfessorId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Name = "Course 1",
                    Description = "Description for Course 1",
                    Language = "Engleski",
                    Level = "B1",
                    Available = true
                },
                new Course
                {
                    Id = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    ProfessorId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Name = "Course 2",
                    Description = "Description for Course 2",
                    Language = "Engleski",
                    Level = "C1",
                    Available = true
                },
                new Course
                {
                    Id = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    ProfessorId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Name = "Course 3",
                    Description = "Description for Course 3",
                    Language = "Engleski",
                    Level = "B2",
                    Available = true
                },
                new Course
                {
                    Id = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    ProfessorId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Name = "Course 4",
                    Description = "Description for Course 4",
                    Language = "Engleski",
                    Level = "C2",
                    Available = true
                }
            );
        });

        modelBuilder.Entity<Lesson>(builder =>
        {
            builder.ToTable("Lessons");

            builder.HasData
            (
                new Lesson
                {
                    Id = Guid.Parse("961fa56e-71a8-4186-8a8d-488143d6a260"),
                    CourseId = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    Title = "Title 1",
                    Content = "Content 1"
                },
                new Lesson
                {
                    Id = Guid.Parse("e0c98ea1-2644-4ff0-8f2e-e7d30a3f2a6b"),
                    CourseId = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    Title = "Title 2",
                    Content = "Content 2"
                },
                new Lesson
                {
                    Id = Guid.Parse("b2d89a70-2ac5-4d26-94b9-ef99b746a388"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    Title = "Title 3",
                    Content = "Content 3"
                },
                new Lesson
                {
                    Id = Guid.Parse("ed0632a3-1b00-421c-be7f-044253c2d820"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    Title = "Title 4",
                    Content = "Content 4"
                },
                new Lesson
                {
                    Id = Guid.Parse("e19c5e3e-c5aa-411d-b540-0597307daf79"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    Title = "Title 5",
                    Content = "Content 5"
                },
                new Lesson
                {
                    Id = Guid.Parse("1695a6e9-f4a8-45f2-8cc9-5ff0c01dad26"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    Title = "Title 6",
                    Content = "Content 6"
                },
                new Lesson
                {
                    Id = Guid.Parse("75f0edab-103a-46ac-aff0-1f17aca2d279"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    Title = "Title 7",
                    Content = "Content 7"
                },
                new Lesson
                {
                    Id = Guid.Parse("c1c2c043-1385-47d0-9ec8-c8a6085bf0bc"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    Title = "Title 8",
                    Content = "Content 8"
                }
            );
        });

        modelBuilder.Entity<Review>(builder =>
        {
            builder.ToTable("Posts");

            builder
                .HasOne(review => review.User)
                .WithMany()
                .HasForeignKey(review => review.UserId)
                .IsRequired();

            builder
                .HasOne(review => review.Course)
                .WithMany()
                .HasForeignKey(review => review.CourseId)
                .IsRequired();

            builder.HasData
            (
                new Review
                {
                    Id = Guid.Parse("a4b12efb-c878-4a43-9197-1efdf1d33e4a"),
                    CourseId = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Content 1",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("4abf8e23-8fbd-46a6-80dc-fedd31814e24"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Content 2",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Content 3",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Content 4",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("5f632e40-9f96-4c75-9afa-59dc6460d8e0"),
                    CourseId = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Content 5",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Content 6",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("4bc9fece-54dc-4cca-a31b-de8a0557f8da"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Content 7",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("e8a67c2e-3943-44ed-9d6c-7a56565302e9"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Content 8",
                    PostDate = DateTime.Now
                }
            );
        });
    }
}