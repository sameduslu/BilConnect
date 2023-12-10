using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class emre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellingPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellingPosts_Posts_Id",
                        column: x => x.Id,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellingPosts");
        }
    }
}
