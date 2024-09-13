using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DasignoTest.Migrations
{
    /// <inheritdoc />
    public partial class InitMIG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondSurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
