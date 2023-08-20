using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemCase.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "DeletedDate", "DeletedUserId", "Email", "IsActive", "ModifiedDate", "ModifiedUserId", "Name", "Password", "RefreshToken", "Surname" },
                values: new object[] { new Guid("d0bfa391-a604-4049-a868-359091461e46"), new DateTime(2023, 8, 19, 21, 49, 43, 233, DateTimeKind.Utc).AddTicks(1680), new Guid("d0bfa391-a604-4049-a868-359091461e46"), null, null, "admin@gmail.com", true, null, null, "Admin", "AP6JD2wRV7KezsxGpLfXTW5x1ahzwAXuODka244V4nLBstXmwmjIMga9vgyieuHCXQ==", "APa+sQgUWdZXkDZ9qwxAhBUMCQcE87d1MgSc6hVZ2C12ILF5exJhFYjwek5803wbdg==", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
