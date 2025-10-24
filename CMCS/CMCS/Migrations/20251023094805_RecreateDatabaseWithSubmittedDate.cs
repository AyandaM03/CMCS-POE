using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMCS.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabaseWithSubmittedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupportingDocumentFileName",
                table: "Claims",
                newName: "SupportingDocument");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Claims",
                newName: "SubmittedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupportingDocument",
                table: "Claims",
                newName: "SupportingDocumentFileName");

            migrationBuilder.RenameColumn(
                name: "SubmittedDate",
                table: "Claims",
                newName: "SubmittedAt");
        }
    }
}
