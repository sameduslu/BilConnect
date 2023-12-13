using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class PostReportStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PostReports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostReports");
        }
    }
}
