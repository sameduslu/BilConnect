using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class deleteCasscade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_AspNetUsers_ReporterId",
                table: "PostReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_Posts_ReportedPostId",
                table: "PostReports");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_AspNetUsers_ReporterId",
                table: "PostReports",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_Posts_ReportedPostId",
                table: "PostReports",
                column: "ReportedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_AspNetUsers_ReporterId",
                table: "PostReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_Posts_ReportedPostId",
                table: "PostReports");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_AspNetUsers_ReporterId",
                table: "PostReports",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_Posts_ReportedPostId",
                table: "PostReports",
                column: "ReportedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
