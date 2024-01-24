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
                columns: new[] { "Id", "Available", "Description", "Language", "Level", "Name", "ProfessorId" },
                values: new object[,]
                {
                    { new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), true, "Description for Course 2", "Engleski", "C1", "Course 2", new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), true, "Description for Course 1", "Engleski", "B1", "Course 1", new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), true, "Description for Course 4", "Engleski", "C2", "Course 4", new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), true, "Description for Course 3", "Engleski", "B2", "Course 3", new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "Phone", "Picture", "ResetTokenExpires", "Role", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), "lule19@gmail.com", "Luka", "Petrović", new byte[] { 74, 155, 235, 179, 70, 251, 71, 176, 58, 55, 142, 204, 6, 122, 36, 195, 167, 6, 20, 155, 141, 215, 79, 44, 182, 171, 179, 234, 15, 161, 109, 244, 117, 75, 76, 19, 43, 56, 204, 197, 226, 11, 189, 206, 97, 43, 221, 151, 221, 157, 154, 158, 90, 211, 238, 177, 52, 31, 27, 207, 140, 186, 73, 98 }, null, new byte[] { 126, 74, 106, 48, 73, 212, 64, 153, 245, 226, 74, 254, 107, 33, 174, 29, 166, 105, 61, 243, 126, 37, 118, 193, 112, 80, 224, 231, 124, 232, 47, 91, 122, 242, 8, 160, 100, 240, 174, 59, 168, 141, 212, 67, 238, 149, 244, 44, 202, 51, 234, 233, 210, 181, 161, 159, 237, 5, 84, 114, 238, 194, 187, 28, 145, 96, 206, 174, 197, 175, 214, 7, 206, 40, 119, 52, 42, 49, 83, 126, 183, 99, 113, 154, 171, 19, 52, 44, 11, 90, 25, 173, 210, 188, 58, 168, 154, 35, 115, 169, 166, 206, 37, 188, 126, 174, 215, 158, 226, 41, 63, 222, 189, 197, 101, 8, 72, 233, 86, 202, 213, 159, 131, 99, 137, 32, 110, 4 }, "064 765 9876", null, null, 2, "E99884F381EFF52E1365B3EB429BD9C1CCA1FFCF4A50D118F161BA8188CE4D6D52C3E83EA6D222E838DC95ADBD4AE9294E55669B5DE7A2369E50C72DF1312129", new DateTime(2024, 1, 23, 20, 40, 19, 140, DateTimeKind.Local).AddTicks(129) },
                    { new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), "boka0404002.ba@gmail.com", "Bojana", "Aleksijević", new byte[] { 74, 155, 235, 179, 70, 251, 71, 176, 58, 55, 142, 204, 6, 122, 36, 195, 167, 6, 20, 155, 141, 215, 79, 44, 182, 171, 179, 234, 15, 161, 109, 244, 117, 75, 76, 19, 43, 56, 204, 197, 226, 11, 189, 206, 97, 43, 221, 151, 221, 157, 154, 158, 90, 211, 238, 177, 52, 31, 27, 207, 140, 186, 73, 98 }, null, new byte[] { 126, 74, 106, 48, 73, 212, 64, 153, 245, 226, 74, 254, 107, 33, 174, 29, 166, 105, 61, 243, 126, 37, 118, 193, 112, 80, 224, 231, 124, 232, 47, 91, 122, 242, 8, 160, 100, 240, 174, 59, 168, 141, 212, 67, 238, 149, 244, 44, 202, 51, 234, 233, 210, 181, 161, 159, 237, 5, 84, 114, 238, 194, 187, 28, 145, 96, 206, 174, 197, 175, 214, 7, 206, 40, 119, 52, 42, 49, 83, 126, 183, 99, 113, 154, 171, 19, 52, 44, 11, 90, 25, 173, 210, 188, 58, 168, 154, 35, 115, 169, 166, 206, 37, 188, 126, 174, 215, 158, 226, 41, 63, 222, 189, 197, 101, 8, 72, 233, 86, 202, 213, 159, 131, 99, 137, 32, 110, 4 }, "064 784 5668", null, null, 2, "FCB1B88171F72C787685291F59C3306D9E50974718FBCBBBEBEFA7919EBEF9A7267DA909C2DFD1350CC4959D1E689943E0E14F3EF43E09C01A44DBC0B6D05E52", new DateTime(2024, 1, 23, 20, 40, 19, 140, DateTimeKind.Local).AddTicks(161) },
                    { new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), "fjovanovic284@gmail.com", "Filip", "Jovanović", new byte[] { 74, 155, 235, 179, 70, 251, 71, 176, 58, 55, 142, 204, 6, 122, 36, 195, 167, 6, 20, 155, 141, 215, 79, 44, 182, 171, 179, 234, 15, 161, 109, 244, 117, 75, 76, 19, 43, 56, 204, 197, 226, 11, 189, 206, 97, 43, 221, 151, 221, 157, 154, 158, 90, 211, 238, 177, 52, 31, 27, 207, 140, 186, 73, 98 }, null, new byte[] { 126, 74, 106, 48, 73, 212, 64, 153, 245, 226, 74, 254, 107, 33, 174, 29, 166, 105, 61, 243, 126, 37, 118, 193, 112, 80, 224, 231, 124, 232, 47, 91, 122, 242, 8, 160, 100, 240, 174, 59, 168, 141, 212, 67, 238, 149, 244, 44, 202, 51, 234, 233, 210, 181, 161, 159, 237, 5, 84, 114, 238, 194, 187, 28, 145, 96, 206, 174, 197, 175, 214, 7, 206, 40, 119, 52, 42, 49, 83, 126, 183, 99, 113, 154, 171, 19, 52, 44, 11, 90, 25, 173, 210, 188, 58, 168, 154, 35, 115, 169, 166, 206, 37, 188, 126, 174, 215, 158, 226, 41, 63, 222, 189, 197, 101, 8, 72, 233, 86, 202, 213, 159, 131, 99, 137, 32, 110, 4 }, "061 755 8995", null, null, 2, "8D2AA1E004958D5958785C2D9CDB9514AE273326C5A1D79500A348E848DCF2CCEF8BB34E5A723B8A2323B37ED6593F92717F9C20973B04EE8C3408C37A971594", new DateTime(2024, 1, 23, 20, 40, 19, 140, DateTimeKind.Local).AddTicks(41) }
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
                    { new Guid("4abf8e23-8fbd-46a6-80dc-fedd31814e24"), "Content 2", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(526), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4bc9fece-54dc-4cca-a31b-de8a0557f8da"), "Content 7", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(546), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"), "Content 3", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(530), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5f632e40-9f96-4c75-9afa-59dc6460d8e0"), "Content 5", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(538), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"), "Content 4", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(534), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("a4b12efb-c878-4a43-9197-1efdf1d33e4a"), "Content 1", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(504), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"), "Content 6", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(543), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("e8a67c2e-3943-44ed-9d6c-7a56565302e9"), "Content 8", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 23, 20, 40, 19, 141, DateTimeKind.Local).AddTicks(550), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") }
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
