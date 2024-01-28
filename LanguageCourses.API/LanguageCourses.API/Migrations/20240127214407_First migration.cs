using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LanguageCourses.API.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
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
                    Price = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Posts",
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
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Courses_CourseId1",
                        column: x => x.CourseId1,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Available", "Description", "Duration", "Language", "Level", "Name", "Price", "ProfessorId", "Type" },
                values: new object[,]
                {
                    { new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), true, "Description for Course 2", 0, "Engleski", "C1", "Course 2", 0, new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), 0 },
                    { new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), true, "Description for Course 1", 0, "Engleski", "B1", "Course 1", 0, new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), 0 },
                    { new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), true, "Description for Course 4", 0, "Engleski", "C2", "Course 4", 0, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), 0 },
                    { new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), true, "Description for Course 3", 0, "Engleski", "B2", "Course 3", 0, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "Phone", "Picture", "ResetTokenExpires", "Role", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), "lule19@gmail.com", "Luka", "Petrović", new byte[] { 96, 76, 61, 65, 28, 48, 85, 35, 16, 177, 96, 41, 216, 31, 241, 100, 145, 229, 45, 175, 77, 28, 21, 244, 210, 36, 234, 18, 192, 43, 159, 167, 245, 30, 245, 59, 220, 21, 86, 124, 200, 179, 101, 100, 20, 134, 242, 123, 182, 120, 184, 169, 206, 190, 80, 172, 173, 209, 122, 228, 186, 167, 93, 171 }, null, new byte[] { 227, 74, 156, 231, 104, 205, 154, 63, 19, 193, 224, 180, 92, 77, 103, 101, 16, 180, 147, 36, 32, 139, 233, 189, 180, 60, 43, 190, 72, 78, 8, 17, 228, 47, 118, 167, 122, 88, 185, 16, 50, 93, 107, 154, 207, 172, 87, 226, 181, 207, 86, 7, 29, 128, 233, 10, 224, 63, 5, 7, 124, 83, 140, 118, 143, 124, 187, 31, 24, 235, 81, 69, 186, 192, 191, 246, 185, 188, 20, 93, 218, 72, 116, 149, 228, 238, 41, 106, 63, 179, 16, 50, 67, 3, 36, 241, 213, 135, 95, 186, 124, 40, 14, 90, 186, 82, 19, 101, 233, 148, 107, 223, 9, 44, 235, 20, 80, 159, 67, 229, 40, 5, 54, 209, 149, 50, 6, 189 }, "064 765 9876", null, null, 2, "08AEC9B2FA9840AE5210194CEE83A851321A1579DBFA27CC03E735B68B13CCA050073A58382FFEFD5A3180653FDB8A7DBF1532EDC04CCC93BE64FB236C58E8EC", new DateTime(2024, 1, 27, 22, 44, 6, 889, DateTimeKind.Local).AddTicks(6035) },
                    { new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), "boka0404002.ba@gmail.com", "Bojana", "Aleksijević", new byte[] { 96, 76, 61, 65, 28, 48, 85, 35, 16, 177, 96, 41, 216, 31, 241, 100, 145, 229, 45, 175, 77, 28, 21, 244, 210, 36, 234, 18, 192, 43, 159, 167, 245, 30, 245, 59, 220, 21, 86, 124, 200, 179, 101, 100, 20, 134, 242, 123, 182, 120, 184, 169, 206, 190, 80, 172, 173, 209, 122, 228, 186, 167, 93, 171 }, null, new byte[] { 227, 74, 156, 231, 104, 205, 154, 63, 19, 193, 224, 180, 92, 77, 103, 101, 16, 180, 147, 36, 32, 139, 233, 189, 180, 60, 43, 190, 72, 78, 8, 17, 228, 47, 118, 167, 122, 88, 185, 16, 50, 93, 107, 154, 207, 172, 87, 226, 181, 207, 86, 7, 29, 128, 233, 10, 224, 63, 5, 7, 124, 83, 140, 118, 143, 124, 187, 31, 24, 235, 81, 69, 186, 192, 191, 246, 185, 188, 20, 93, 218, 72, 116, 149, 228, 238, 41, 106, 63, 179, 16, 50, 67, 3, 36, 241, 213, 135, 95, 186, 124, 40, 14, 90, 186, 82, 19, 101, 233, 148, 107, 223, 9, 44, 235, 20, 80, 159, 67, 229, 40, 5, 54, 209, 149, 50, 6, 189 }, "064 784 5668", null, null, 2, "8F775AEFF80BF81F8660F54D7FDF09E957AC03ACA7EC8E9A06B50910240339C09E417B2839A311BB7446B47CE5628E799BA9D46387579A22A596BC93B0BF1338", new DateTime(2024, 1, 27, 22, 44, 6, 889, DateTimeKind.Local).AddTicks(6116) },
                    { new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), "fjovanovic284@gmail.com", "Filip", "Jovanović", new byte[] { 96, 76, 61, 65, 28, 48, 85, 35, 16, 177, 96, 41, 216, 31, 241, 100, 145, 229, 45, 175, 77, 28, 21, 244, 210, 36, 234, 18, 192, 43, 159, 167, 245, 30, 245, 59, 220, 21, 86, 124, 200, 179, 101, 100, 20, 134, 242, 123, 182, 120, 184, 169, 206, 190, 80, 172, 173, 209, 122, 228, 186, 167, 93, 171 }, null, new byte[] { 227, 74, 156, 231, 104, 205, 154, 63, 19, 193, 224, 180, 92, 77, 103, 101, 16, 180, 147, 36, 32, 139, 233, 189, 180, 60, 43, 190, 72, 78, 8, 17, 228, 47, 118, 167, 122, 88, 185, 16, 50, 93, 107, 154, 207, 172, 87, 226, 181, 207, 86, 7, 29, 128, 233, 10, 224, 63, 5, 7, 124, 83, 140, 118, 143, 124, 187, 31, 24, 235, 81, 69, 186, 192, 191, 246, 185, 188, 20, 93, 218, 72, 116, 149, 228, 238, 41, 106, 63, 179, 16, 50, 67, 3, 36, 241, 213, 135, 95, 186, 124, 40, 14, 90, 186, 82, 19, 101, 233, 148, 107, 223, 9, 44, 235, 20, 80, 159, 67, 229, 40, 5, 54, 209, 149, 50, 6, 189 }, "061 755 8995", null, null, 2, "6785118FB5FCD7D8FA40AB12435F8925F1AD2925E2746C149F73DF478F4C5FC527FFA3DA9BED5F8368557DC6837DC87DF1A0364BA2FB66199567A4BFAFA79CA0", new DateTime(2024, 1, 27, 22, 44, 6, 889, DateTimeKind.Local).AddTicks(5730) }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "CourseId", "Title" },
                values: new object[,]
                {
                    { new Guid("1695a6e9-f4a8-45f2-8cc9-5ff0c01dad26"), "Content 6", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), "Title 6" },
                    { new Guid("75f0edab-103a-46ac-aff0-1f17aca2d279"), "Content 7", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), "Title 7" },
                    { new Guid("961fa56e-71a8-4186-8a8d-488143d6a260"), "Content 1", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), "Title 1" },
                    { new Guid("b2d89a70-2ac5-4d26-94b9-ef99b746a388"), "Content 3", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), "Title 3" },
                    { new Guid("c1c2c043-1385-47d0-9ec8-c8a6085bf0bc"), "Content 8", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), "Title 8" },
                    { new Guid("e0c98ea1-2644-4ff0-8f2e-e7d30a3f2a6b"), "Content 2", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), "Title 2" },
                    { new Guid("e19c5e3e-c5aa-411d-b540-0597307daf79"), "Content 5", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), "Title 5" },
                    { new Guid("ed0632a3-1b00-421c-be7f-044253c2d820"), "Content 4", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), "Title 4" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CourseId", "CourseId1", "PostDate", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("4abf8e23-8fbd-46a6-80dc-fedd31814e24"), "Content 2", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(8992), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4bc9fece-54dc-4cca-a31b-de8a0557f8da"), "Content 7", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9036), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"), "Content 3", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9003), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5f632e40-9f96-4c75-9afa-59dc6460d8e0"), "Content 5", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9020), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"), "Content 4", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9012), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("a4b12efb-c878-4a43-9197-1efdf1d33e4a"), "Content 1", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(8950), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"), "Content 6", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9028), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("e8a67c2e-3943-44ed-9d6c-7a56565302e9"), "Content 8", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 27, 22, 44, 6, 891, DateTimeKind.Local).AddTicks(9044), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CourseId",
                table: "Posts",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CourseId1",
                table: "Posts",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
