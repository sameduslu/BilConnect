using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class PostReportAddedToUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_AspNetUsers_ApplicationUserId",
                table: "PostReports");

            migrationBuilder.DropIndex(
                name: "IX_PostReports_ApplicationUserId",
                table: "PostReports");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PostReports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PostReports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_ApplicationUserId",
                table: "PostReports",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_AspNetUsers_ApplicationUserId",
                table: "PostReports",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
