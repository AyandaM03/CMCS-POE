using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMCS.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimDocumentAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Claims_ClaimID",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ClaimID",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "ClaimID",
                table: "Documents",
                newName: "ClaimId");

            migrationBuilder.RenameColumn(
                name: "DocumentID",
                table: "Documents",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Documents",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "SupportingDocument",
                table: "Claims",
                newName: "SupportingDocumentFileName");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Claims",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "ClaimId",
                table: "Documents",
                newName: "ClaimID");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Documents",
                newName: "DocumentID");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "Documents",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "SupportingDocumentFileName",
                table: "Claims",
                newName: "SupportingDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClaimID",
                table: "Documents",
                column: "ClaimID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Claims_ClaimID",
                table: "Documents",
                column: "ClaimID",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
