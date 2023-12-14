using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class PriceRemovedBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Posts");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SellingPosts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "SellingPosts");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Posts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
