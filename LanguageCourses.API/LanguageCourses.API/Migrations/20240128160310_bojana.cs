using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LanguageCourses.API.Migrations
{
    /// <inheritdoc />
    public partial class bojana : Migration
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
                    { new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed"), "lule19@gmail.com", "Luka", "Petrović", new byte[] { 15, 72, 173, 19, 229, 62, 54, 60, 43, 15, 95, 167, 76, 99, 23, 106, 221, 29, 141, 144, 40, 124, 200, 218, 19, 181, 136, 188, 65, 147, 29, 114, 88, 135, 147, 249, 3, 70, 145, 118, 217, 204, 167, 33, 11, 149, 246, 249, 123, 199, 115, 107, 59, 178, 189, 105, 196, 12, 166, 155, 25, 195, 46, 191 }, null, new byte[] { 15, 31, 7, 13, 53, 32, 251, 4, 103, 176, 92, 173, 67, 232, 122, 161, 78, 67, 57, 65, 170, 20, 168, 132, 57, 58, 22, 154, 197, 39, 194, 195, 196, 149, 102, 25, 19, 72, 237, 206, 54, 199, 202, 183, 138, 233, 213, 229, 143, 7, 129, 188, 75, 247, 49, 60, 241, 136, 110, 200, 54, 38, 159, 151, 82, 47, 116, 121, 95, 54, 51, 143, 21, 75, 30, 20, 126, 120, 136, 40, 164, 13, 130, 222, 116, 181, 12, 193, 229, 142, 63, 4, 69, 76, 133, 126, 24, 35, 213, 251, 82, 72, 192, 201, 21, 79, 11, 112, 157, 169, 167, 193, 230, 169, 176, 249, 220, 141, 99, 79, 125, 12, 94, 1, 44, 84, 10, 192 }, "064 765 9876", null, null, 2, "330277E981F544B4CF252C70AC73BACC92ECD9FA8A2CBA6280B7D636882E60926410110209AB0947FFFBE47440DC73743D61C80A8CC84D32355D4FF4D995499C", new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(213) },
                    { new Guid("73fb19cd-7b56-459e-8b84-be0d05dd67b6"), "boka0404002.ba@gmail.com", "Bojana", "Aleksijević", new byte[] { 15, 72, 173, 19, 229, 62, 54, 60, 43, 15, 95, 167, 76, 99, 23, 106, 221, 29, 141, 144, 40, 124, 200, 218, 19, 181, 136, 188, 65, 147, 29, 114, 88, 135, 147, 249, 3, 70, 145, 118, 217, 204, 167, 33, 11, 149, 246, 249, 123, 199, 115, 107, 59, 178, 189, 105, 196, 12, 166, 155, 25, 195, 46, 191 }, null, new byte[] { 15, 31, 7, 13, 53, 32, 251, 4, 103, 176, 92, 173, 67, 232, 122, 161, 78, 67, 57, 65, 170, 20, 168, 132, 57, 58, 22, 154, 197, 39, 194, 195, 196, 149, 102, 25, 19, 72, 237, 206, 54, 199, 202, 183, 138, 233, 213, 229, 143, 7, 129, 188, 75, 247, 49, 60, 241, 136, 110, 200, 54, 38, 159, 151, 82, 47, 116, 121, 95, 54, 51, 143, 21, 75, 30, 20, 126, 120, 136, 40, 164, 13, 130, 222, 116, 181, 12, 193, 229, 142, 63, 4, 69, 76, 133, 126, 24, 35, 213, 251, 82, 72, 192, 201, 21, 79, 11, 112, 157, 169, 167, 193, 230, 169, 176, 249, 220, 141, 99, 79, 125, 12, 94, 1, 44, 84, 10, 192 }, "064 784 5668", null, null, 2, "0138C7F7AB5D5E248B35267EFE83B0D81335FC74A44DD87BA39E64F2F9C413D3233EDC4C55AB2EB59828691FB19BA41553CAAE5573740C86601B2AE30E07A2A0", new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(242) },
                    { new Guid("9150584f-eb77-4a84-a13f-698a581985d8"), "fjovanovic284@gmail.com", "Filip", "Jovanović", new byte[] { 15, 72, 173, 19, 229, 62, 54, 60, 43, 15, 95, 167, 76, 99, 23, 106, 221, 29, 141, 144, 40, 124, 200, 218, 19, 181, 136, 188, 65, 147, 29, 114, 88, 135, 147, 249, 3, 70, 145, 118, 217, 204, 167, 33, 11, 149, 246, 249, 123, 199, 115, 107, 59, 178, 189, 105, 196, 12, 166, 155, 25, 195, 46, 191 }, null, new byte[] { 15, 31, 7, 13, 53, 32, 251, 4, 103, 176, 92, 173, 67, 232, 122, 161, 78, 67, 57, 65, 170, 20, 168, 132, 57, 58, 22, 154, 197, 39, 194, 195, 196, 149, 102, 25, 19, 72, 237, 206, 54, 199, 202, 183, 138, 233, 213, 229, 143, 7, 129, 188, 75, 247, 49, 60, 241, 136, 110, 200, 54, 38, 159, 151, 82, 47, 116, 121, 95, 54, 51, 143, 21, 75, 30, 20, 126, 120, 136, 40, 164, 13, 130, 222, 116, 181, 12, 193, 229, 142, 63, 4, 69, 76, 133, 126, 24, 35, 213, 251, 82, 72, 192, 201, 21, 79, 11, 112, 157, 169, 167, 193, 230, 169, 176, 249, 220, 141, 99, 79, 125, 12, 94, 1, 44, 84, 10, 192 }, "061 755 8995", null, null, 2, "A29DBA7903F9F358E1B917A254D1C0AD367651BC3C3609E0C32EC7E6FBFB48C2CC32ED0406CE210569490EE930F675DDEB7C234BCC9DFADCBF51C7BFA3AF0E7C", new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(113) }
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
                    { new Guid("4abf8e23-8fbd-46a6-80dc-fedd31814e24"), "Content 2", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9377), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("4bc9fece-54dc-4cca-a31b-de8a0557f8da"), "Content 7", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9395), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5e6fdd45-3ba6-4019-af72-665d2e1a39aa"), "Content 3", new Guid("d1b4704a-5a5a-4b51-ab72-68b5db496d96"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9381), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("5f632e40-9f96-4c75-9afa-59dc6460d8e0"), "Content 5", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9388), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("87c1eb43-6bc9-4f0d-ab08-bf54a708c88c"), "Content 4", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9385), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("a4b12efb-c878-4a43-9197-1efdf1d33e4a"), "Content 1", new Guid("7fca3cd1-6d04-4ac1-bff6-57cb3a23e34a"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9355), 5, new Guid("9150584f-eb77-4a84-a13f-698a581985d8") },
                    { new Guid("d2d5b555-e8af-478b-a144-0ea40f7d4ed6"), "Content 6", new Guid("053504c1-bfad-4ec1-9932-1e7b5e536ce8"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9392), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") },
                    { new Guid("e8a67c2e-3943-44ed-9d6c-7a56565302e9"), "Content 8", new Guid("c8b98b9e-a370-4c71-b899-ad558f4124b8"), null, new DateTime(2024, 1, 28, 17, 3, 10, 758, DateTimeKind.Local).AddTicks(9399), 5, new Guid("4f96f59a-a880-4f17-955a-c7d94f36f6ed") }
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
