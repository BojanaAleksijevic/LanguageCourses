using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.API.Data;

public class LanguageCoursesDbContext : DbContext
{
    public LanguageCoursesDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<Forum> Forums { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");

            /* builder
                .HasMany(user => user.Courses)
                .WithMany(course => course.Users)
                .UsingEntity<Enrollment>(); */

            builder.HasData(
                new User
                {
                    Id = Guid.Parse("cfeb0e5e-5d09-464a-82dd-fe5d39e45ad9"),
                    FirstName = "Filip",
                    LastName = "Jovanovic",
                    Email = "filipj@gmail.com",
                    Phone = "062 3856 142",
                    Password = "password1",
                    Role = Role.ADMIN
                },
                new User
                {
                    Id = Guid.Parse("af19ba81-1376-4a55-b2f3-a0cb6782f491"),
                    FirstName = "Luka",
                    LastName = "Petrovic",
                    Email = "lukap@gmail.com",
                    Phone = "065 9934 376",
                    Password = "password2",
                    Role = Role.ADMIN
                },
                new User
                {
                    Id = Guid.Parse("998902da-c58d-4963-ae0d-39079971e5cd"),
                    FirstName = "Bojana",
                    LastName = "Aleksijevic",
                    Email = "bojanaa@gmail.com",
                    Phone = "060 9603 672",
                    Password = "password3",
                    Role = Role.PROFESSOR
                },
                new User 
                {
                    Id = Guid.Parse("b0f6f6f3-9077-4186-a22e-8f9df4198d71"),
                    FirstName = "Andrijana",
                    LastName = "Mihailovic",
                    Email = "andrijanam@gmail.com",
                    Phone = "060 9603 672",
                    Password = "password4",
                    Role = Role.PROFESSOR
                },
                new User
                {
                    Id = Guid.Parse("c8d54e95-6d30-4667-b07e-cd30c4160d59"),
                    FirstName = "Aleksandar",
                    LastName = "Djordjevic",
                    Email = "aleksandardj@gmail.com",
                    Phone = "060 4622 672",
                    Password = "password5",
                    Role = Role.PROFESSOR
                }
            );
        });
    }
}