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
                    Picture = "viber_image_2024-02-04_17-38-12-871.jpg",
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
                    Picture = "unknown.png",
                    PasswordHash = _hmac
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:BojanaPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.PROFESSOR
                },
                new User
                {
                    Id = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    FirstName = "Andrijana",
                    LastName = "Mihailović",
                    Phone = "061 356 8872",
                    Email = "andrijanaa.mihailovic@gmail.com",
                    Picture = "unknown.png",
                    PasswordHash = _hmac
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:AndrijanaPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.PROFESSOR
                },
                new User
                {
                    Id = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    FirstName = "Aleksandra",
                    LastName = "Đorđević",
                    Phone = "062 600 5468",
                    Email = "boka0404002.ba@gmail.com",
                    Picture = "unknown.png",
                    PasswordHash = _hmac
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Passwords:AleksandarPwd")!)),
                    PasswordSalt = _hmac.Key,
                    VerificationToken = UserConversions.CreateRandomToken(),
                    VerifiedAt = DateTime.Now,
                    Role = Role.STUDENT
                }
            ); ;
        });

        modelBuilder.Entity<Course>(builder =>
        {
            builder.ToTable("Courses");

            builder.HasData
            (
                new Course
                {
                    Id = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Napredna Engleska Književnost",
                    Description = "Istražite napredne teme u engleskoj književnosti uključujući književnu analizu i kritičko razmišljanje. Ovaj kurs pruža dublje razumevanje klasičnih dela engleske književnosti kao i savremenih pristupa interpretaciji književnih tekstova.",
                    Language = "Engleski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 39.99M,
                    Duration = 12,
                    Available = true,
                    Picture = "engleska.png"
                },
                new Course
                {
                    Id = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Majstorstvo Španskog Jezika",
                    Description = "Ovladajte španskim jezikom kroz sveobuhvatne lekcije koje obuhvataju gramatiku, vokabular i konverzaciju. Ovaj kurs je pogodan kako za početnike tako i za one koji žele unaprediti svoje već postojeće znanje španskog jezika.",
                    Language = "Španski",
                    Level = "Početni",
                    Type = CourseType.INDIVIDUAL,
                    Price = 29.99M,
                    Duration = 8,
                    Available = true,
                    Picture = "spanija.png"
                },
                new Course
                {
                    Id = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Intermedijski Francuski Razgovori",
                    Description = "Unapredite svoje veštine u razgovoru na francuskom jeziku kroz aktivnosti i razgovore na srednjem nivou. Ovaj kurs je idealan za one koji već imaju osnovno znanje francuskog jezika i žele unaprediti svoju komunikaciju.",
                    Language = "Francuski",
                    Level = "Intermedijski",
                    Type = CourseType.GROUP,
                    Price = 49.99M,
                    Duration = 16,
                    Available = true,
                    Picture = "francuska.png"
                },
                new Course
                {
                    Id = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Intenzivni Kurs Nemačkog Jezika",
                    Description = "Intenzivni kurs dizajniran da vam pomogne da postignete veštinu u nemačkom jeziku. Ovaj kurs je pogodan za one koji već imaju određeno znanje nemačkog jezika i žele da unaprede svoje sposobnosti u govoru, čitanju i pisanju.",
                    Language = "Nemački",
                    Level = "Napredni",
                    Type = CourseType.INDIVIDUAL,
                    Price = 59.99M,
                    Duration = 10,
                    Available = true,
                    Picture = "nemacka.png"
                },
                new Course
                {
                    Id = Guid.Parse("1836d3ee-f532-448e-bb5e-cce7d7f541e1"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Napredni Kurs Italijanskog Jezika",
                    Description = "Napredni kurs italijanskog jezika namenjen je polaznicima koji žele da unaprede svoje veštine u govoru, čitanju i pisanju na italijanskom jeziku. Kroz raznolike teme, vežbe i interaktivne aktivnosti, ovaj kurs omogućava dublje razumevanje i praktičnu primenu italijanskog jezika.",
                    Language = "Italijanski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 44.99M,
                    Duration = 14,
                    Available = true,
                    Picture = "italija.png"
                },
                new Course
                {
                    Id = Guid.Parse("78a18af3-b252-45cb-a29e-bdfef8dabd05"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Portugalski Jezik Za Putovanja",
                    Description = "Ovaj kurs portugalskog jezika je namenjen onima koji žele da nauče osnove portugalskog jezika kako bi se lakše snalazili na putovanjima. Kroz praktične lekcije i situacije iz svakodnevnog života, naučićete osnove komunikacije na portugalskom jeziku.",
                    Language = "Portugalski",
                    Level = "Osnovni",
                    Type = CourseType.INDIVIDUAL,
                    Price = 34.99M,
                    Duration = 6,
                    Available = true,
                    Picture = "portugalija.png"
                },
                new Course
                {
                    Id = Guid.Parse("4c4c2aec-8006-43ac-a094-17ede6b36cd5"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Konverzacijski Kurs Ruskog Jezika",
                    Description = "Ukoliko želite da unapredite svoje veštine u konverzaciji na ruskom jeziku, ovaj kurs je pravi izbor za vas. Kroz različite teme i simulirane situacije, naučićete kako da se izražavate tečno i samouvereno na ruskom jeziku.",
                    Language = "Ruski",
                    Level = "Srednji",
                    Type = CourseType.GROUP,
                    Price = 49.99M,
                    Duration = 12,
                    Available = true,
                    Picture = "rusija.png"
                },
                new Course
                {
                    Id = Guid.Parse("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Kineski Jezik Za Poslovnu Komunikaciju",
                    Description = "Ovaj kurs kineskog jezika je fokusiran na poslovnu komunikaciju na kineskom jeziku. Kroz praktične primere, vežbe i uloge, naučićete kako da efikasno komunicirate sa poslovnim partnerima na kineskom jeziku.",
                    Language = "Kineski",
                    Level = "Napredni",
                    Type = CourseType.INDIVIDUAL,
                    Price = 59.99M,
                    Duration = 10,
                    Available = true,
                    Picture = "kina.png"
                },
                new Course
                {
                    Id = Guid.Parse("f6085f08-170e-43b7-96cc-413449ffd9be"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Napredni Kurs Japanskog Pisanja",
                    Description = "Ovaj kurs je namenjen onima koji žele da unaprede svoje veštine u pisanju na japanskom jeziku. Kroz intenzivne lekcije, vežbe i analize tekstova, naučićete kako da pišete jasno i gramatički ispravno na japanskom jeziku.",
                    Language = "Japanski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 54.99M,
                    Duration = 14,
                    Available = true,
                    Picture = "japan.png"
                },
                new Course
                {
                    Id = Guid.Parse("dd64f615-a47b-45dd-8305-dbf5f9f684a1"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Škola Italijanskog Kulinarskog Jezika",
                    Description = "Učite italijanski kroz kulinarske veštine! Ovaj kurs kombinuje učenje italijanskog jezika sa pripremom autentičnih italijanskih jela. Upoznajte se sa italijanskim kulinarskim terminima i tehnikama kroz praktične demonstracije i vežbe.",
                    Language = "Italijanski",
                    Level = "Srednji",
                    Type = CourseType.GROUP,
                    Price = 44.99M,
                    Duration = 10,
                    Available = true,
                    Picture = "italija.png"
                },
                new Course
                {
                    Id = Guid.Parse("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Francuski Jezik Kroz Filmsku Umjetnost",
                    Description = "Uživajte u učenju francuskog jezika kroz filmsku umetnost! Ovaj kurs omogućava vam da istražite francuski jezik kroz klasične i savremene francuske filmove. Kroz analize, diskusije i vežbe, unapredićete svoje jezičke veštine uz zabavu i kreativnost.",
                    Language = "Francuski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 49.99M,
                    Duration = 12,
                    Available = true,
                    Picture = "francuska.png"
                },
                new Course
                {
                    Id = Guid.Parse("842a4a74-208d-408d-b8e8-952fded15531"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Španski Jezik Za Putovanja",
                    Description = "Ovaj kurs španskog jezika je namenjen onima koji žele da nauče osnove španskog jezika kako bi se lakše snalazili na putovanjima u špansko govornom području. Kroz praktične lekcije i situacije iz svakodnevnog života, naučićete osnove komunikacije na španskom jeziku.",
                    Language = "Španski",
                    Level = "Osnovni",
                    Type = CourseType.INDIVIDUAL,
                    Price = 34.99M,
                    Duration = 6,
                    Available = true,
                    Picture = "spanija.png"
                },
                new Course
                {
                    Id = Guid.Parse("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Napredni Kurs Srpskog Jezika",
                    Description = "Ovaj kurs srpskog jezika je namenjen onima koji žele da unaprede svoje veštine u govoru, čitanju i pisanju na srpskom jeziku. Kroz dublje analize i praktične vežbe, naučićete kako da se izražavate na srpskom jeziku sa većom preciznošću i tečnošću.",
                    Language = "Srpski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 44.99M,
                    Duration = 14,
                    Available = true,
                    Picture = "srbija.png"
                },
                new Course
                {
                    Id = Guid.Parse("af53ef1b-4b52-4cc4-b72e-a6afd499de07"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Napredni Kurs Engleskog Jezika za Poslovanje",
                    Description = "Napredni kurs engleskog jezika usredsređen na poslovnu komunikaciju. Kroz različite teme kao što su pregovaranje, prezentacije i poslovna korespondencija, usavršićete svoje veštine za poslovno okruženje.",
                    Language = "Engleski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 49.99M,
                    Duration = 10,
                    Available = true,
                    Picture = "engleska.png"
                },
                new Course
                {
                    Id = Guid.Parse("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"),
                    ProfessorId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Name = "Nemački Jezik Kroz Književnost",
                    Description = "Ovaj kurs nemačkog jezika fokusiran je na književnost i kulturu nemačkog govornog područja. Kroz analizu klasičnih i savremenih dela nemačke književnosti, proširićete svoje jezičke veštine uz dublje razumevanje nemačke kulture.",
                    Language = "Nemački",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 54.99M,
                    Duration = 12,
                    Available = true,
                    Picture = "nemacka.png"
                },
                new Course
                {
                    Id = Guid.Parse("5615f4d6-c9c9-4306-9307-a583a931477f"),
                    ProfessorId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Name = "Napredni Kurs Ruskih Gramatičkih Konstrukcija",
                    Description = "Ovaj kurs je namenjen naprednim polaznicima koji žele da prodube svoje znanje ruske gramatike. Kroz analizu složenih gramatičkih konstrukcija i vežbe, unapredićete svoje jezičke veštine na ruskom jeziku.",
                    Language = "Ruski",
                    Level = "Napredni",
                    Type = CourseType.GROUP,
                    Price = 49.99M,
                    Duration = 10,
                    Available = true,
                    Picture = "rusija.png"
                }
            );
        });

        modelBuilder.Entity<Review>(builder =>
        {
            builder.ToTable("Reviews");

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
                    Content = "Kurs jezika je neverovatan! Profesor je stručan i veoma angažovan, a materijali su odlično pripremljeni. Veoma sam zadovoljan svojim napretkom.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("4abf8e23-8fbd-46a6-80dc-fedd31814e24"),
                    CourseId = Guid.Parse("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 4,
                    Content = "Kurs je zaista inspirativan! Naučio sam mnogo kroz zanimljive aktivnosti i diskusije. Preporučujem svima koji žele da nauče jezik na zabavan način.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 3,
                    Content = "Profesor je fantastičan! Veoma je strpljiv i pažljivo objašnjava gramatičke koncepte. Osećam se mnogo samouverenije nakon ovog kursa.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"),
                    CourseId = Guid.Parse("053504c1-bfad-4ec1-9932-1e7b5e536ce8"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 4,
                    Content = "Kurs je sjajan! Veoma sam impresioniran načinom na koji su organizovane lekcije i kako se naglašava važnost praktične primene jezika. Definitivno vredi svake pare.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("5f632e40-9f96-4c75-9afa-59dc6460d8e0"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 5,
                    Content = "Kurs je odličan! Profesor je veoma posvećen i pruža individualni pristup svakom učeniku. Osećam se veoma motivisano da nastavim učenje nakon ovog kursa.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"),
                    CourseId = Guid.Parse("d1b4704a-5a5a-4b51-ab72-68b5db496d96"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 4,
                    Content = "Kurs je bio fantastičan! Materijali su veoma informativni, a profesor je veoma stručan i ljubazan. Osećam se kao da sam stekao čvrstu osnovu.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("4bc9fece-54dc-4cca-a31b-de8a0557f8da"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Kurs je bio izuzetan! Profesor je veoma strpljiv i jasno objašnjava složene koncepte. Veoma sam zahvalan na prilici da naučim jezik na ovako efikasan način.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("e8a67c2e-3943-44ed-9d6c-7a56565302e9"),
                    CourseId = Guid.Parse("c8b98b9e-a370-4c71-b899-ad558f4124b8"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 4,
                    Content = "Kurs je bio nezaboravan! Naučio sam mnogo više od samog jezika - upoznao sam i bogatu kulturu i istoriju. Toplo preporučujem ovaj kurs svima koji žele da istraže jezik i kulturu.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("3e520956-7350-4c78-a3f4-da70dca4aa91"),
                    CourseId = Guid.Parse("1836d3ee-f532-448e-bb5e-cce7d7f541e1"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 4,
                    Content = "Kurs je veoma koristan! Naučio sam mnogo novih stvari i osećam se mnogo samouverenije u svom jezičkom izražavanju.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("190a5f5a-6f8f-4e5d-ad14-cb0691931c80"),
                    CourseId = Guid.Parse("1836d3ee-f532-448e-bb5e-cce7d7f541e1"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 5,
                    Content = "Profesor je veoma stručan i vešto prilagođava nastavu potrebama svakog učenika. Veoma sam zadovoljan što sam odabrao ovaj kurs.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("5be446ee-aae0-4a7d-a477-cbe5f3b56a36"),
                    CourseId = Guid.Parse("78a18af3-b252-45cb-a29e-bdfef8dabd05"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 4,
                    Content = "Materijali za kurs su odlični i veoma raznoliki. Svaka lekcija je interesantna i podstiče aktivno učenje.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("87d4f0d8-14e9-43f1-bc57-1b4b3ae0d16a"),
                    CourseId = Guid.Parse("78a18af3-b252-45cb-a29e-bdfef8dabd05"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Kroz ovaj kurs sam stekao ne samo jezičke veštine, već i dublje razumevanje kulture povezane sa jezikom. Fantastično iskustvo!",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("4cc0b4b7-4e8c-42c0-b53a-bfb4add8f785"),
                    CourseId = Guid.Parse("4c4c2aec-8006-43ac-a094-17ede6b36cd5"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 4,
                    Content = "Veoma sam impresioniran organizacijom kursa. Sve informacije su jasno predstavljene, a atmosfera na časovima je veoma podsticajna.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("42fa4f91-8edf-40a3-9ae8-2b9b605d7400"),
                    CourseId = Guid.Parse("4c4c2aec-8006-43ac-a094-17ede6b36cd5"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 5,
                    Content = "Ovaj kurs je zaista promenio moj pristup učenju jezika. Sada uživam u učenju i osećam se motivisano da nastavim napredovanje.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("caeda2a8-c404-4db6-85cc-bea175ea098c"),
                    CourseId = Guid.Parse("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 3,
                    Content = "Profesorica je veoma inspirativna i pruža puno dodatnih resursa za dodatno učenje. Veoma sam zahvalan na njenom angažmanu.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("fbad1f49-63fc-4e14-aedc-db1d572a6393"),
                    CourseId = Guid.Parse("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 4,
                    Content = "Interaktivne vežbe i praktične aktivnosti su me naučile kako da koristim jezik u stvarnim situacijama. Osećam se spremno da koristim jezik u svakodnevnom životu.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("50b01a07-9f1a-4cdf-854f-fb9deafa35a7"),
                    CourseId = Guid.Parse("f6085f08-170e-43b7-96cc-413449ffd9be"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 5,
                    Content = "Ovaj kurs je bio pravo osveženje u mom učenju jezika. Osećam se mnogo sigurnije u svojim jezičkim veštinama nakon završetka kursa.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("14a913c8-2631-4630-9c2a-dbb81f8f3ebe"),
                    CourseId = Guid.Parse("f6085f08-170e-43b7-96cc-413449ffd9be"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 4,
                    Content = "Kroz ovaj kurs sam upoznao mnogo novih ljudi i stekao jezičke veštine koje će mi koristiti celog života. Toplo preporučujem svima koji žele da nauče jezik na kvalitetan način.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("44a0fcd8-50ff-41d8-b9af-9072c7344537"),
                    CourseId = Guid.Parse("dd64f615-a47b-45dd-8305-dbf5f9f684a1"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 4,
                    Content = "Kurs je pružio sve što sam očekivao i više od toga! Profesor je stručan i veoma posvećen, a atmosfera na časovima je veoma prijateljska i podsticajna.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("135e5ed7-a978-47f7-a17d-4962c58775fb"),
                    CourseId = Guid.Parse("dd64f615-a47b-45dd-8305-dbf5f9f684a1"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 5,
                    Content = "Ovaj kurs je pravo otkriće za mene! Naučio sam mnogo korisnih stvari koje mogu odmah primeniti u svakodnevnom životu. Definitivno vredi uloženog vremena i truda.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("50930ad2-9b3d-4fbc-b3cf-686c6963e1ff"),
                    CourseId = Guid.Parse("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Veoma sam zadovoljan načinom na koji su organizovane lekcije. Materijali su strukturirani i lako razumljivi, a progres u učenju je primetan već nakon nekoliko časova.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("71625778-c722-41ec-a029-47a4cbf7866b"),
                    CourseId = Guid.Parse("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 4,
                    Content = "Profesorica je veoma strpljiva i ljubazna, uvek spremna da odgovori na sva pitanja i pruži dodatna objašnjenja. Svojim entuzijazmom uspela je da mi probudi interesovanje za jezik.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("7d527f7b-0d11-45dc-88c1-ec6ed2b6dfc1"),
                    CourseId = Guid.Parse("842a4a74-208d-408d-b8e8-952fded15531"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 3,
                    Content = "Ovaj kurs je bio pravo osveženje za moje učenje jezika. Veoma sam zahvalan na svim korisnim savetima i tehnika koje sam naučio tokom trajanja kursa.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("d1e9f410-4fb1-4f78-8ed7-9a8dd266f4a3"),
                    CourseId = Guid.Parse("842a4a74-208d-408d-b8e8-952fded15531"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 5,
                    Content = "Nakon ovog kursa, osećam se mnogo samouverenije u svom jezičkom izražavanju. Veoma sam impresioniran nivoom podrške i motivacije koji sam dobio od profesora.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("637829c8-5d56-4ffa-8aea-34bb84a81a71"),
                    CourseId = Guid.Parse("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 5,
                    Content = "Ovaj kurs je bio izuzetno koristan za moje profesionalne ciljeve. Naučio sam mnogo korisnih poslovnih izraza i veština komunikacije koje ću koristiti u svojoj karijeri.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("a07a313f-59a4-4172-bb6a-2aca6084e8b1"),
                    CourseId = Guid.Parse("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Interaktivne vežbe i grupne diskusije su mi pomogle da savladam jezik na potpuno novi način. Osećam se spremno da se upustim u bilo koju jezičku situaciju.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("8d9c748d-4c6d-483f-a3a5-3e131dab676d"),
                    CourseId = Guid.Parse("af53ef1b-4b52-4cc4-b72e-a6afd499de07"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 4,
                    Content = "Kroz ovaj kurs sam stekao ne samo jezičke veštine, već i nove prijatelje sa istim interesovanjima. Sve pohvale za organizaciju i sadržaj kursa!",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("df63645d-2e9e-454a-b8fb-1d90a91bc9ba"),
                    CourseId = Guid.Parse("af53ef1b-4b52-4cc4-b72e-a6afd499de07"),
                    UserId = Guid.Parse("73fb19cd-7b56-459e-8b84-be0d05dd67b6"),
                    Rating = 4,
                    Content = "Ovaj kurs je bio odličan izbor za moje učenje jezika. Sada se osećam spremno da se izrazim na jeziku i komuniciram sa ljudima širom sveta.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("31786f1a-9e48-4cfa-a0fe-02ec1470d8b2"),
                    CourseId = Guid.Parse("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"),
                    UserId = Guid.Parse("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"),
                    Rating = 5,
                    Content = "Ovaj kurs je bio pravo otkriće za mene! Nisam samo naučio jezik, već sam otkrio potpuno novi svet kroz njega. Veoma sam zahvalan na ovom iskustvu.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("e70877e9-a1a4-498b-958a-4d0a16e447ea"),
                    CourseId = Guid.Parse("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"),
                    UserId = Guid.Parse("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"),
                    Rating = 4,
                    Content = "Nakon završetka ovog kursa, osećam se kao da sam postao potpuno novi čovek. Naučio sam mnogo više od samog jezika - naučio sam kako da se bolje razumem s drugima.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("1db4abb3-df4a-45a8-b05d-c4361dfbd1ee"),
                    CourseId = Guid.Parse("5615f4d6-c9c9-4306-9307-a583a931477f"),
                    UserId = Guid.Parse("9150584F-EB77-4A84-A13F-698A581985D8"),
                    Rating = 5,
                    Content = "Profesor je bio veoma strpljiv i podržavajući tokom celog kursa. Njegova stručnost i ljubaznost su me motivisali da učim svakog dana sve više.",
                    PostDate = DateTime.Now
                },
                new Review
                {
                    Id = Guid.Parse("46715118-1fdb-4277-928f-8e63d78a7729"),
                    CourseId = Guid.Parse("5615f4d6-c9c9-4306-9307-a583a931477f"),
                    UserId = Guid.Parse("4f96f59a-a880-4f17-955a-c7d94f36f6ed"),
                    Rating = 3,
                    Content = "Ovaj kurs je bio prava investicija u moje lično i profesionalno usavršavanje. Preporučujem ga svima koji žele da napreduju i rastu kao osoba.",
                    PostDate = DateTime.Now
                }
            );
        });
    }
}