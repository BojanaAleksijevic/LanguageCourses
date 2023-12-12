using LanguageCourses.API.Enums;
using LanguageCourses.API.Models;
using Microsoft.EntityFrameworkCore;
using LanguageCourses.API.Extensions;
using System.Security.Cryptography;
using Org.BouncyCastle.Security;

namespace LanguageCourses.API.Data;

public class LanguageCoursesDbContext : DbContext
{
    private readonly HMACSHA512 hmac;

    private string FilipPwd = "course123";

    public LanguageCoursesDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        hmac = new HMACSHA512();
    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseUser> Enrollments { get; set; }

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

            builder
                .HasMany(e => e.Courses)
                .WithMany(e => e.Users)
                .UsingEntity<CourseUser>();

            builder.HasData(
                new User
                {
                    Id = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    FirstName = "Filip",
                    LastName = "Jovanović",
                    Phone = "061 755 8995",
                    Email = "fjovanovic284@gmail.com",
                    PasswordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(FilipPwd)),
                    PasswordSalt = hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.ADMIN
                }
            );
        });
    }
}