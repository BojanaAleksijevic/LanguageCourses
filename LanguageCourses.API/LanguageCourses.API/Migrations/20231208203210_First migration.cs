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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { new Guid("998902da-c58d-4963-ae0d-39079971e5cd"), "bojanaa@gmail.com", "Bojana", "Aleksijevic", "password3", "060 9603 672", 1 },
                    { new Guid("af19ba81-1376-4a55-b2f3-a0cb6782f491"), "lukap@gmail.com", "Luka", "Petrovic", "password2", "065 9934 376", 2 },
                    { new Guid("b0f6f6f3-9077-4186-a22e-8f9df4198d71"), "andrijanam@gmail.com", "Andrijana", "Mihailovic", "password4", "060 9603 672", 1 },
                    { new Guid("c8d54e95-6d30-4667-b07e-cd30c4160d59"), "aleksandardj@gmail.com", "Aleksandar", "Djordjevic", "password5", "060 4622 672", 1 },
                    { new Guid("cfeb0e5e-5d09-464a-82dd-fe5d39e45ad9"), "filipj@gmail.com", "Filip", "Jovanovic", "password1", "062 3856 142", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
