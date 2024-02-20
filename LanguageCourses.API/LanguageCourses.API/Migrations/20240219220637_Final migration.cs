using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LanguageCourses.API.Migrations
{
    /// <inheritdoc />
    public partial class Finalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Courses_CourseId1",
                        column: x => x.CourseId1,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Available", "Description", "Duration", "Language", "Level", "Name", "Picture", "Price", "ProfessorId", "Type" },
                values: new object[,]
                {
                    { new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), true, "Ovladajte španskim jezikom kroz sveobuhvatne lekcije koje obuhvataju gramatiku, vokabular i konverzaciju. Ovaj kurs je pogodan kako za početnike tako i za one koji žele unaprediti svoje već postojeće znanje španskog jezika.", 8, "Španski", "Početni", "Majstorstvo Španskog Jezika", "spanija.png", 29.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 0 },
                    { new Guid("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"), true, "Ovaj kurs nemačkog jezika fokusiran je na književnost i kulturu nemačkog govornog područja. Kroz analizu klasičnih i savremenih dela nemačke književnosti, proširićete svoje jezičke veštine uz dublje razumevanje nemačke kulture.", 12, "Nemački", "Napredni", "Nemački Jezik Kroz Književnost", "nemacka.png", 54.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("1836d3ee-f532-448e-bb5e-cce7d7f541e1"), true, "Napredni kurs italijanskog jezika namenjen je polaznicima koji žele da unaprede svoje veštine u govoru, čitanju i pisanju na italijanskom jeziku. Kroz raznolike teme, vežbe i interaktivne aktivnosti, ovaj kurs omogućava dublje razumevanje i praktičnu primenu italijanskog jezika.", 14, "Italijanski", "Napredni", "Napredni Kurs Italijanskog Jezika", "italija.png", 44.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"), true, "Ovaj kurs srpskog jezika je namenjen onima koji žele da unaprede svoje veštine u govoru, čitanju i pisanju na srpskom jeziku. Kroz dublje analize i praktične vežbe, naučićete kako da se izražavate na srpskom jeziku sa većom preciznošću i tečnošću.", 14, "Srpski", "Napredni", "Napredni Kurs Srpskog Jezika", "srbija.png", 44.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("4c4c2aec-8006-43ac-a094-17ede6b36cd5"), true, "Ukoliko želite da unapredite svoje veštine u konverzaciji na ruskom jeziku, ovaj kurs je pravi izbor za vas. Kroz različite teme i simulirane situacije, naučićete kako da se izražavate tečno i samouvereno na ruskom jeziku.", 12, "Ruski", "Srednji", "Konverzacijski Kurs Ruskog Jezika", "rusija.png", 49.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("5615f4d6-c9c9-4306-9307-a583a931477f"), true, "Ovaj kurs je namenjen naprednim polaznicima koji žele da prodube svoje znanje ruske gramatike. Kroz analizu složenih gramatičkih konstrukcija i vežbe, unapredićete svoje jezičke veštine na ruskom jeziku.", 10, "Ruski", "Napredni", "Napredni Kurs Ruskih Gramatičkih Konstrukcija", "rusija.png", 49.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 1 },
                    { new Guid("78a18af3-b252-45cb-a29e-bdfef8dabd05"), true, "Ovaj kurs portugalskog jezika je namenjen onima koji žele da nauče osnove portugalskog jezika kako bi se lakše snalazili na putovanjima. Kroz praktične lekcije i situacije iz svakodnevnog života, naučićete osnove komunikacije na portugalskom jeziku.", 6, "Portugalski", "Osnovni", "Portugalski Jezik Za Putovanja", "portugalija.png", 34.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 0 },
                    { new Guid("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"), true, "Uživajte u učenju francuskog jezika kroz filmsku umetnost! Ovaj kurs omogućava vam da istražite francuski jezik kroz klasične i savremene francuske filmove. Kroz analize, diskusije i vežbe, unapredićete svoje jezičke veštine uz zabavu i kreativnost.", 12, "Francuski", "Napredni", "Francuski Jezik Kroz Filmsku Umjetnost", "francuska.png", 49.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), true, "Istražite napredne teme u engleskoj književnosti uključujući književnu analizu i kritičko razmišljanje. Ovaj kurs pruža dublje razumevanje klasičnih dela engleske književnosti kao i savremenih pristupa interpretaciji književnih tekstova.", 12, "Engleski", "Napredni", "Napredna Engleska Književnost", "engleska.png", 39.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("842a4a74-208d-408d-b8e8-952fded15531"), true, "Ovaj kurs španskog jezika je namenjen onima koji žele da nauče osnove španskog jezika kako bi se lakše snalazili na putovanjima u špansko govornom području. Kroz praktične lekcije i situacije iz svakodnevnog života, naučićete osnove komunikacije na španskom jeziku.", 6, "Španski", "Osnovni", "Španski Jezik Za Putovanja", "spanija.png", 34.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 0 },
                    { new Guid("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"), true, "Ovaj kurs kineskog jezika je fokusiran na poslovnu komunikaciju na kineskom jeziku. Kroz praktične primere, vežbe i uloge, naučićete kako da efikasno komunicirate sa poslovnim partnerima na kineskom jeziku.", 10, "Kineski", "Napredni", "Kineski Jezik Za Poslovnu Komunikaciju", "kina.png", 59.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 0 },
                    { new Guid("af53ef1b-4b52-4cc4-b72e-a6afd499de07"), true, "Napredni kurs engleskog jezika usredsređen na poslovnu komunikaciju. Kroz različite teme kao što su pregovaranje, prezentacije i poslovna korespondencija, usavršićete svoje veštine za poslovno okruženje.", 10, "Engleski", "Napredni", "Napredni Kurs Engleskog Jezika za Poslovanje", "engleska.png", 49.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 1 },
                    { new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), true, "Intenzivni kurs dizajniran da vam pomogne da postignete veštinu u nemačkom jeziku. Ovaj kurs je pogodan za one koji već imaju određeno znanje nemačkog jezika i žele da unaprede svoje sposobnosti u govoru, čitanju i pisanju.", 10, "Nemački", "Napredni", "Intenzivni Kurs Nemačkog Jezika", "nemacka.png", 59.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 0 },
                    { new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), true, "Unapredite svoje veštine u razgovoru na francuskom jeziku kroz aktivnosti i razgovore na srednjem nivou. Ovaj kurs je idealan za one koji već imaju osnovno znanje francuskog jezika i žele unaprediti svoju komunikaciju.", 16, "Francuski", "Intermedijski", "Intermedijski Francuski Razgovori", "francuska.png", 49.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 },
                    { new Guid("dd64f615-a47b-45dd-8305-dbf5f9f684a1"), true, "Učite italijanski kroz kulinarske veštine! Ovaj kurs kombinuje učenje italijanskog jezika sa pripremom autentičnih italijanskih jela. Upoznajte se sa italijanskim kulinarskim terminima i tehnikama kroz praktične demonstracije i vežbe.", 10, "Italijanski", "Srednji", "Škola Italijanskog Kulinarskog Jezika", "italija.png", 44.99m, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), 1 },
                    { new Guid("f6085f08-170e-43b7-96cc-413449ffd9be"), true, "Ovaj kurs je namenjen onima koji žele da unaprede svoje veštine u pisanju na japanskom jeziku. Kroz intenzivne lekcije, vežbe i analize tekstova, naučićete kako da pišete jasno i gramatički ispravno na japanskom jeziku.", 14, "Japanski", "Napredni", "Napredni Kurs Japanskog Pisanja", "japan.png", 54.99m, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "Phone", "Picture", "ResetTokenExpires", "Role", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc"), "boka0404002.ba@gmail.com", "Aleksandra", "Đorđević", new byte[] { 77, 213, 220, 192, 185, 62, 95, 73, 167, 145, 147, 181, 156, 137, 58, 98, 219, 73, 234, 26, 99, 70, 248, 31, 30, 185, 227, 198, 135, 251, 224, 68, 133, 138, 187, 19, 161, 236, 212, 194, 103, 207, 4, 250, 72, 98, 80, 38, 228, 104, 90, 150, 136, 207, 55, 93, 10, 161, 112, 211, 191, 200, 72, 246 }, null, new byte[] { 229, 215, 34, 222, 2, 255, 123, 225, 249, 147, 61, 42, 196, 39, 254, 180, 149, 121, 183, 218, 11, 254, 138, 178, 81, 255, 93, 117, 247, 36, 39, 251, 158, 15, 48, 190, 68, 170, 39, 204, 57, 137, 160, 175, 145, 146, 102, 30, 7, 117, 61, 12, 84, 143, 121, 169, 58, 85, 247, 162, 21, 121, 88, 201, 49, 158, 161, 219, 109, 173, 124, 67, 126, 143, 167, 10, 164, 82, 167, 254, 111, 202, 198, 171, 163, 64, 169, 241, 98, 91, 211, 234, 215, 49, 41, 160, 212, 149, 11, 62, 245, 219, 14, 87, 203, 154, 101, 56, 216, 130, 9, 17, 129, 63, 223, 244, 246, 180, 53, 241, 53, 83, 173, 171, 113, 68, 100, 190 }, "062 600 5468", "unknown.png", null, 0, "954A40626F6712D50D7D49826D707E0220AA04CE401CB760A665A0E3B397C81B48222A72B340C90B23D4772342183A2228F3C665A0B0CDF1A67479FA0CF46210", new DateTime(2024, 2, 19, 23, 6, 37, 618, DateTimeKind.Local).AddTicks(886) },
                    { new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), "lule19@gmail.com", "Luka", "Petrović", new byte[] { 77, 213, 220, 192, 185, 62, 95, 73, 167, 145, 147, 181, 156, 137, 58, 98, 219, 73, 234, 26, 99, 70, 248, 31, 30, 185, 227, 198, 135, 251, 224, 68, 133, 138, 187, 19, 161, 236, 212, 194, 103, 207, 4, 250, 72, 98, 80, 38, 228, 104, 90, 150, 136, 207, 55, 93, 10, 161, 112, 211, 191, 200, 72, 246 }, null, new byte[] { 229, 215, 34, 222, 2, 255, 123, 225, 249, 147, 61, 42, 196, 39, 254, 180, 149, 121, 183, 218, 11, 254, 138, 178, 81, 255, 93, 117, 247, 36, 39, 251, 158, 15, 48, 190, 68, 170, 39, 204, 57, 137, 160, 175, 145, 146, 102, 30, 7, 117, 61, 12, 84, 143, 121, 169, 58, 85, 247, 162, 21, 121, 88, 201, 49, 158, 161, 219, 109, 173, 124, 67, 126, 143, 167, 10, 164, 82, 167, 254, 111, 202, 198, 171, 163, 64, 169, 241, 98, 91, 211, 234, 215, 49, 41, 160, 212, 149, 11, 62, 245, 219, 14, 87, 203, 154, 101, 56, 216, 130, 9, 17, 129, 63, 223, 244, 246, 180, 53, 241, 53, 83, 173, 171, 113, 68, 100, 190 }, "064 765 9876", "viber_image_2024-02-04_17-38-12-871.jpg", null, 2, "F9028AD6D24F5C36458C16846D4FFC89641941C91D169F800FB6175C6A2F9A4A79E7490A243B7514274A0EDF895BF787E7A5430CD8091278DFD553CC04F44954", new DateTime(2024, 2, 19, 23, 6, 37, 618, DateTimeKind.Local).AddTicks(522) },
                    { new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), "boka0404002.ba@gmail.com", "Bojana", "Aleksijević", new byte[] { 77, 213, 220, 192, 185, 62, 95, 73, 167, 145, 147, 181, 156, 137, 58, 98, 219, 73, 234, 26, 99, 70, 248, 31, 30, 185, 227, 198, 135, 251, 224, 68, 133, 138, 187, 19, 161, 236, 212, 194, 103, 207, 4, 250, 72, 98, 80, 38, 228, 104, 90, 150, 136, 207, 55, 93, 10, 161, 112, 211, 191, 200, 72, 246 }, null, new byte[] { 229, 215, 34, 222, 2, 255, 123, 225, 249, 147, 61, 42, 196, 39, 254, 180, 149, 121, 183, 218, 11, 254, 138, 178, 81, 255, 93, 117, 247, 36, 39, 251, 158, 15, 48, 190, 68, 170, 39, 204, 57, 137, 160, 175, 145, 146, 102, 30, 7, 117, 61, 12, 84, 143, 121, 169, 58, 85, 247, 162, 21, 121, 88, 201, 49, 158, 161, 219, 109, 173, 124, 67, 126, 143, 167, 10, 164, 82, 167, 254, 111, 202, 198, 171, 163, 64, 169, 241, 98, 91, 211, 234, 215, 49, 41, 160, 212, 149, 11, 62, 245, 219, 14, 87, 203, 154, 101, 56, 216, 130, 9, 17, 129, 63, 223, 244, 246, 180, 53, 241, 53, 83, 173, 171, 113, 68, 100, 190 }, "064 784 5668", "unknown.png", null, 1, "28D4CB81F13935AE29A274185D34FDE88BC30E5C459AEDB64CACC20091F44CD29A78EE5C07D45DDF2E603FC9E212A37F25F8626CCBCC1D6FC56E93578CB22ACD", new DateTime(2024, 2, 19, 23, 6, 37, 618, DateTimeKind.Local).AddTicks(690) },
                    { new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), "fjovanovic284@gmail.com", "Filip", "Jovanović", new byte[] { 77, 213, 220, 192, 185, 62, 95, 73, 167, 145, 147, 181, 156, 137, 58, 98, 219, 73, 234, 26, 99, 70, 248, 31, 30, 185, 227, 198, 135, 251, 224, 68, 133, 138, 187, 19, 161, 236, 212, 194, 103, 207, 4, 250, 72, 98, 80, 38, 228, 104, 90, 150, 136, 207, 55, 93, 10, 161, 112, 211, 191, 200, 72, 246 }, null, new byte[] { 229, 215, 34, 222, 2, 255, 123, 225, 249, 147, 61, 42, 196, 39, 254, 180, 149, 121, 183, 218, 11, 254, 138, 178, 81, 255, 93, 117, 247, 36, 39, 251, 158, 15, 48, 190, 68, 170, 39, 204, 57, 137, 160, 175, 145, 146, 102, 30, 7, 117, 61, 12, 84, 143, 121, 169, 58, 85, 247, 162, 21, 121, 88, 201, 49, 158, 161, 219, 109, 173, 124, 67, 126, 143, 167, 10, 164, 82, 167, 254, 111, 202, 198, 171, 163, 64, 169, 241, 98, 91, 211, 234, 215, 49, 41, 160, 212, 149, 11, 62, 245, 219, 14, 87, 203, 154, 101, 56, 216, 130, 9, 17, 129, 63, 223, 244, 246, 180, 53, 241, 53, 83, 173, 171, 113, 68, 100, 190 }, "061 755 8995", "9150584F-EB77-4A84-A13F-698A581985D8.jpg", null, 2, "3C557BC29B4589AE1DFDDA541C08C284F423B1D92F439754CBE04AEA1A356C9E39BAFF49B6E6387A2DFF3B056001249682631C0C31BE7A839160C6F5566EE4B2", new DateTime(2024, 2, 19, 23, 6, 37, 618, DateTimeKind.Local).AddTicks(426) },
                    { new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4"), "andrijanaa.mihailovic@gmail.com", "Andrijana", "Mihailović", new byte[] { 77, 213, 220, 192, 185, 62, 95, 73, 167, 145, 147, 181, 156, 137, 58, 98, 219, 73, 234, 26, 99, 70, 248, 31, 30, 185, 227, 198, 135, 251, 224, 68, 133, 138, 187, 19, 161, 236, 212, 194, 103, 207, 4, 250, 72, 98, 80, 38, 228, 104, 90, 150, 136, 207, 55, 93, 10, 161, 112, 211, 191, 200, 72, 246 }, null, new byte[] { 229, 215, 34, 222, 2, 255, 123, 225, 249, 147, 61, 42, 196, 39, 254, 180, 149, 121, 183, 218, 11, 254, 138, 178, 81, 255, 93, 117, 247, 36, 39, 251, 158, 15, 48, 190, 68, 170, 39, 204, 57, 137, 160, 175, 145, 146, 102, 30, 7, 117, 61, 12, 84, 143, 121, 169, 58, 85, 247, 162, 21, 121, 88, 201, 49, 158, 161, 219, 109, 173, 124, 67, 126, 143, 167, 10, 164, 82, 167, 254, 111, 202, 198, 171, 163, 64, 169, 241, 98, 91, 211, 234, 215, 49, 41, 160, 212, 149, 11, 62, 245, 219, 14, 87, 203, 154, 101, 56, 216, 130, 9, 17, 129, 63, 223, 244, 246, 180, 53, 241, 53, 83, 173, 171, 113, 68, 100, 190 }, "061 356 8872", "unknown.png", null, 1, "56CD4BBCE8917A28A18B4A026B464819DA22EAE7C414E382541E51424C9051BA9D5CFFF141DF928C32288611410CCECB46685C53CB3D5CD2A3E8424669561FC3", new DateTime(2024, 2, 19, 23, 6, 37, 618, DateTimeKind.Local).AddTicks(829) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CourseId", "CourseId1", "PostDate", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("135e5ed7-a978-47f7-a17d-4962c58775fb"), "Ovaj kurs je pravo otkriće za mene! Naučio sam mnogo korisnih stvari koje mogu odmah primeniti u svakodnevnom životu. Definitivno vredi uloženog vremena i truda.", new Guid("dd64f615-a47b-45dd-8305-dbf5f9f684a1"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1201), 5, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("14a913c8-2631-4630-9c2a-dbb81f8f3ebe"), "Kroz ovaj kurs sam upoznao mnogo novih ljudi i stekao jezičke veštine koje će mi koristiti celog života. Toplo preporučujem svima koji žele da nauče jezik na kvalitetan način.", new Guid("f6085f08-170e-43b7-96cc-413449ffd9be"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1192), 4, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("190a5f5a-6f8f-4e5d-ad14-cb0691931c80"), "Profesor je veoma stručan i vešto prilagođava nastavu potrebama svakog učenika. Veoma sam zadovoljan što sam odabrao ovaj kurs.", new Guid("1836d3ee-f532-448e-bb5e-cce7d7f541e1"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1152), 5, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("1db4abb3-df4a-45a8-b05d-c4361dfbd1ee"), "Profesor je bio veoma strpljiv i podržavajući tokom celog kursa. Njegova stručnost i ljubaznost su me motivisali da učim svakog dana sve više.", new Guid("5615f4d6-c9c9-4306-9307-a583a931477f"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1250), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("31786f1a-9e48-4cfa-a0fe-02ec1470d8b2"), "Ovaj kurs je bio pravo otkriće za mene! Nisam samo naučio jezik, već sam otkrio potpuno novi svet kroz njega. Veoma sam zahvalan na ovom iskustvu.", new Guid("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1241), 5, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("3e520956-7350-4c78-a3f4-da70dca4aa91"), "Kurs je veoma koristan! Naučio sam mnogo novih stvari i osećam se mnogo samouverenije u svom jezičkom izražavanju.", new Guid("1836d3ee-f532-448e-bb5e-cce7d7f541e1"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1148), 4, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("42fa4f91-8edf-40a3-9ae8-2b9b605d7400"), "Ovaj kurs je zaista promenio moj pristup učenju jezika. Sada uživam u učenju i osećam se motivisano da nastavim napredovanje.", new Guid("4c4c2aec-8006-43ac-a094-17ede6b36cd5"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1170), 5, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("44a0fcd8-50ff-41d8-b9af-9072c7344537"), "Kurs je pružio sve što sam očekivao i više od toga! Profesor je stručan i veoma posvećen, a atmosfera na časovima je veoma prijateljska i podsticajna.", new Guid("dd64f615-a47b-45dd-8305-dbf5f9f684a1"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1197), 4, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("46715118-1fdb-4277-928f-8e63d78a7729"), "Ovaj kurs je bio prava investicija u moje lično i profesionalno usavršavanje. Preporučujem ga svima koji žele da napreduju i rastu kao osoba.", new Guid("5615f4d6-c9c9-4306-9307-a583a931477f"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1254), 3, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4abf8e23-8fbd-46a6-80dc-fedd31814e24"), "Kurs je zaista inspirativan! Naučio sam mnogo kroz zanimljive aktivnosti i diskusije. Preporučujem svima koji žele da nauče jezik na zabavan način.", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1115), 4, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4bc9fece-54dc-4cca-a31b-de8a0557f8da"), "Kurs je bio izuzetan! Profesor je veoma strpljiv i jasno objašnjava složene koncepte. Veoma sam zahvalan na prilici da naučim jezik na ovako efikasan način.", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1139), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4cc0b4b7-4e8c-42c0-b53a-bfb4add8f785"), "Veoma sam impresioniran organizacijom kursa. Sve informacije su jasno predstavljene, a atmosfera na časovima je veoma podsticajna.", new Guid("4c4c2aec-8006-43ac-a094-17ede6b36cd5"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1165), 4, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("50930ad2-9b3d-4fbc-b3cf-686c6963e1ff"), "Veoma sam zadovoljan načinom na koji su organizovane lekcije. Materijali su strukturirani i lako razumljivi, a progres u učenju je primetan već nakon nekoliko časova.", new Guid("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1206), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("50b01a07-9f1a-4cdf-854f-fb9deafa35a7"), "Ovaj kurs je bio pravo osveženje u mom učenju jezika. Osećam se mnogo sigurnije u svojim jezičkim veštinama nakon završetka kursa.", new Guid("f6085f08-170e-43b7-96cc-413449ffd9be"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1185), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("5be446ee-aae0-4a7d-a477-cbe5f3b56a36"), "Materijali za kurs su odlični i veoma raznoliki. Svaka lekcija je interesantna i podstiče aktivno učenje.", new Guid("78a18af3-b252-45cb-a29e-bdfef8dabd05"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1157), 4, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"), "Profesor je fantastičan! Veoma je strpljiv i pažljivo objašnjava gramatičke koncepte. Osećam se mnogo samouverenije nakon ovog kursa.", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1120), 3, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("5f632e40-9f96-4c75-9afa-59dc6460d8e0"), "Kurs je odličan! Profesor je veoma posvećen i pruža individualni pristup svakom učeniku. Osećam se veoma motivisano da nastavim učenje nakon ovog kursa.", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1130), 5, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("637829c8-5d56-4ffa-8aea-34bb84a81a71"), "Ovaj kurs je bio izuzetno koristan za moje profesionalne ciljeve. Naučio sam mnogo korisnih poslovnih izraza i veština komunikacije koje ću koristiti u svojoj karijeri.", new Guid("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1223), 5, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("71625778-c722-41ec-a029-47a4cbf7866b"), "Profesorica je veoma strpljiva i ljubazna, uvek spremna da odgovori na sva pitanja i pruži dodatna objašnjenja. Svojim entuzijazmom uspela je da mi probudi interesovanje za jezik.", new Guid("7f76f35a-e6dc-4bf1-89f8-0bdb3339bc19"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1210), 4, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("7d527f7b-0d11-45dc-88c1-ec6ed2b6dfc1"), "Ovaj kurs je bio pravo osveženje za moje učenje jezika. Veoma sam zahvalan na svim korisnim savetima i tehnika koje sam naučio tokom trajanja kursa.", new Guid("842a4a74-208d-408d-b8e8-952fded15531"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1215), 3, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"), "Kurs je sjajan! Veoma sam impresioniran načinom na koji su organizovane lekcije i kako se naglašava važnost praktične primene jezika. Definitivno vredi svake pare.", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1125), 4, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("87d4f0d8-14e9-43f1-bc57-1b4b3ae0d16a"), "Kroz ovaj kurs sam stekao ne samo jezičke veštine, već i dublje razumevanje kulture povezane sa jezikom. Fantastično iskustvo!", new Guid("78a18af3-b252-45cb-a29e-bdfef8dabd05"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1161), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("8d9c748d-4c6d-483f-a3a5-3e131dab676d"), "Kroz ovaj kurs sam stekao ne samo jezičke veštine, već i nove prijatelje sa istim interesovanjima. Sve pohvale za organizaciju i sadržaj kursa!", new Guid("af53ef1b-4b52-4cc4-b72e-a6afd499de07"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1233), 4, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("a07a313f-59a4-4172-bb6a-2aca6084e8b1"), "Interaktivne vežbe i grupne diskusije su mi pomogle da savladam jezik na potpuno novi način. Osećam se spremno da se upustim u bilo koju jezičku situaciju.", new Guid("2861ea0f-cdb6-4cdf-9fc5-9ecdddb2048c"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1229), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("a4b12efb-c878-4a43-9197-1efdf1d33e4a"), "Kurs jezika je neverovatan! Profesor je stručan i veoma angažovan, a materijali su odlično pripremljeni. Veoma sam zadovoljan svojim napretkom.", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1095), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("caeda2a8-c404-4db6-85cc-bea175ea098c"), "Profesorica je veoma inspirativna i pruža puno dodatnih resursa za dodatno učenje. Veoma sam zahvalan na njenom angažmanu.", new Guid("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1175), 3, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("d1e9f410-4fb1-4f78-8ed7-9a8dd266f4a3"), "Nakon ovog kursa, osećam se mnogo samouverenije u svom jezičkom izražavanju. Veoma sam impresioniran nivoom podrške i motivacije koji sam dobio od profesora.", new Guid("842a4a74-208d-408d-b8e8-952fded15531"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1219), 5, new Guid("bb9efa33-b752-4c9a-8b0b-5f6ba6983ec4") },
                    { new Guid("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"), "Kurs je bio fantastičan! Materijali su veoma informativni, a profesor je veoma stručan i ljubazan. Osećam se kao da sam stekao čvrstu osnovu.", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1135), 4, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("df63645d-2e9e-454a-b8fb-1d90a91bc9ba"), "Ovaj kurs je bio odličan izbor za moje učenje jezika. Sada se osećam spremno da se izrazim na jeziku i komuniciram sa ljudima širom sveta.", new Guid("af53ef1b-4b52-4cc4-b72e-a6afd499de07"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1237), 4, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("e70877e9-a1a4-498b-958a-4d0a16e447ea"), "Nakon završetka ovog kursa, osećam se kao da sam postao potpuno novi čovek. Naučio sam mnogo više od samog jezika - naučio sam kako da se bolje razumem s drugima.", new Guid("10a87bb7-dc64-46f3-a357-d4ad9d4a1484"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1246), 4, new Guid("1c1a51fa-67c3-4d44-a906-f6b00ddd4fcc") },
                    { new Guid("e8a67c2e-3943-44ed-9d6c-7a56565302e9"), "Kurs je bio nezaboravan! Naučio sam mnogo više od samog jezika - upoznao sam i bogatu kulturu i istoriju. Toplo preporučujem ovaj kurs svima koji žele da istraže jezik i kulturu.", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1144), 4, new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6") },
                    { new Guid("fbad1f49-63fc-4e14-aedc-db1d572a6393"), "Interaktivne vežbe i praktične aktivnosti su me naučile kako da koristim jezik u stvarnim situacijama. Osećam se spremno da koristim jezik u svakodnevnom životu.", new Guid("9d8d6878-a7db-4cc2-98bb-8e4070db83d7"), null, new DateTime(2024, 2, 19, 23, 6, 37, 619, DateTimeKind.Local).AddTicks(1180), 4, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseId",
                table: "Reviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseId1",
                table: "Reviews",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
