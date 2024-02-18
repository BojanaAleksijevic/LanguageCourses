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
                    { new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), true, "Description for Course 2", 3, "Engleski", "C1", "Course 2", null, 21m, new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), 0 },
                    { new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), true, "Description for Course 1", 9, "Engleski", "B1", "Course 1", null, 25.87m, new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), 1 },
                    { new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), true, "Description for Course 4", 40, "Engleski", "C2", "Course 4", null, 10.87m, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), 1 },
                    { new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), true, "Description for Course 3", 20, "Engleski", "B2", "Course 3", null, 14.5m, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordResetToken", "PasswordSalt", "Phone", "Picture", "ResetTokenExpires", "Role", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), "lule19@gmail.com", "Luka", "Petrović", new byte[] { 83, 178, 88, 103, 205, 186, 20, 234, 217, 199, 212, 91, 83, 65, 187, 201, 141, 95, 199, 177, 113, 215, 40, 78, 133, 25, 152, 139, 33, 230, 126, 238, 180, 49, 170, 214, 142, 166, 1, 4, 236, 245, 203, 39, 197, 127, 20, 244, 163, 196, 54, 243, 166, 206, 239, 231, 152, 193, 9, 128, 134, 211, 233, 120 }, null, new byte[] { 194, 113, 68, 5, 23, 199, 132, 37, 26, 134, 244, 235, 12, 244, 74, 15, 187, 81, 229, 233, 45, 184, 188, 210, 228, 80, 204, 10, 240, 218, 255, 45, 2, 169, 23, 109, 216, 114, 169, 0, 137, 253, 239, 182, 59, 8, 103, 43, 17, 142, 179, 156, 156, 190, 34, 186, 127, 168, 108, 30, 70, 154, 174, 67, 200, 164, 111, 204, 68, 31, 218, 187, 230, 244, 199, 147, 196, 227, 21, 122, 238, 116, 98, 62, 187, 170, 152, 250, 63, 255, 91, 101, 185, 112, 186, 64, 152, 232, 160, 177, 140, 21, 154, 121, 147, 55, 111, 189, 111, 112, 127, 94, 64, 160, 63, 251, 98, 58, 132, 253, 6, 188, 0, 14, 188, 13, 120, 91 }, "064 765 9876", "viber_image_2024-02-04_17-38-12-871.jpg", null, 2, "5A520523656A7490328D52BFC97FDA0D0D872A40FB9A4CC5E24E935D80B6BD1E1D3A54054A4CB9EB5AB40C3E159A010A0C9BB0AB13E73BAB53E8FCE17CA3FB8D", new DateTime(2024, 2, 18, 3, 20, 31, 67, DateTimeKind.Local).AddTicks(8647) },
                    { new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), "boka0404002.ba@gmail.com", "Bojana", "Aleksijević", new byte[] { 83, 178, 88, 103, 205, 186, 20, 234, 217, 199, 212, 91, 83, 65, 187, 201, 141, 95, 199, 177, 113, 215, 40, 78, 133, 25, 152, 139, 33, 230, 126, 238, 180, 49, 170, 214, 142, 166, 1, 4, 236, 245, 203, 39, 197, 127, 20, 244, 163, 196, 54, 243, 166, 206, 239, 231, 152, 193, 9, 128, 134, 211, 233, 120 }, null, new byte[] { 194, 113, 68, 5, 23, 199, 132, 37, 26, 134, 244, 235, 12, 244, 74, 15, 187, 81, 229, 233, 45, 184, 188, 210, 228, 80, 204, 10, 240, 218, 255, 45, 2, 169, 23, 109, 216, 114, 169, 0, 137, 253, 239, 182, 59, 8, 103, 43, 17, 142, 179, 156, 156, 190, 34, 186, 127, 168, 108, 30, 70, 154, 174, 67, 200, 164, 111, 204, 68, 31, 218, 187, 230, 244, 199, 147, 196, 227, 21, 122, 238, 116, 98, 62, 187, 170, 152, 250, 63, 255, 91, 101, 185, 112, 186, 64, 152, 232, 160, 177, 140, 21, 154, 121, 147, 55, 111, 189, 111, 112, 127, 94, 64, 160, 63, 251, 98, 58, 132, 253, 6, 188, 0, 14, 188, 13, 120, 91 }, "064 784 5668", null, null, 2, "F9A4646CB28DBDCBB089A86B9A7B94648A07CB65C931372D280D9580CA7C24B213061FF00FEE85CB7B0A18F1A0CCB91695F7A1B727BBC6A6B805F90F01F719B5", new DateTime(2024, 2, 18, 3, 20, 31, 67, DateTimeKind.Local).AddTicks(8737) },
                    { new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), "fjovanovic284@gmail.com", "Filip", "Jovanović", new byte[] { 83, 178, 88, 103, 205, 186, 20, 234, 217, 199, 212, 91, 83, 65, 187, 201, 141, 95, 199, 177, 113, 215, 40, 78, 133, 25, 152, 139, 33, 230, 126, 238, 180, 49, 170, 214, 142, 166, 1, 4, 236, 245, 203, 39, 197, 127, 20, 244, 163, 196, 54, 243, 166, 206, 239, 231, 152, 193, 9, 128, 134, 211, 233, 120 }, null, new byte[] { 194, 113, 68, 5, 23, 199, 132, 37, 26, 134, 244, 235, 12, 244, 74, 15, 187, 81, 229, 233, 45, 184, 188, 210, 228, 80, 204, 10, 240, 218, 255, 45, 2, 169, 23, 109, 216, 114, 169, 0, 137, 253, 239, 182, 59, 8, 103, 43, 17, 142, 179, 156, 156, 190, 34, 186, 127, 168, 108, 30, 70, 154, 174, 67, 200, 164, 111, 204, 68, 31, 218, 187, 230, 244, 199, 147, 196, 227, 21, 122, 238, 116, 98, 62, 187, 170, 152, 250, 63, 255, 91, 101, 185, 112, 186, 64, 152, 232, 160, 177, 140, 21, 154, 121, 147, 55, 111, 189, 111, 112, 127, 94, 64, 160, 63, 251, 98, 58, 132, 253, 6, 188, 0, 14, 188, 13, 120, 91 }, "061 755 8995", "9150584F-EB77-4A84-A13F-698A581985D8.jpg", null, 2, "C4646E95A5E0E9611C797722B399316298B179042481A4D78728F9D2FED75CC17109EE0CE8DDEDD9A4EADB897D23416B8DD1A35A9B346CDEBD705D512566BEDA", new DateTime(2024, 2, 18, 3, 20, 31, 67, DateTimeKind.Local).AddTicks(8557) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CourseId", "CourseId1", "PostDate", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("4abf8e23-8fbd-46a6-80dc-fedd31814e24"), "Content 2", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2500), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4bc9fece-54dc-4cca-a31b-de8a0557f8da"), "Content 7", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2526), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"), "Content 3", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2507), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5f632e40-9f96-4c75-9afa-59dc6460d8e0"), "Content 5", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2518), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"), "Content 4", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2514), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("a4b12efb-c878-4a43-9197-1efdf1d33e4a"), "Content 1", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2243), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"), "Content 6", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2522), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("e8a67c2e-3943-44ed-9d6c-7a56565302e9"), "Content 8", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 2, 18, 3, 20, 31, 69, DateTimeKind.Local).AddTicks(2530), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") }
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
