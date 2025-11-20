using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMCS.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Lecturer" },
                    { 2, "Coordinator" },
                    { 3, "Manager" },
                    { 4, "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "RoleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "RoleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "RoleID",
                keyValue: 4);
        }
    }
}
