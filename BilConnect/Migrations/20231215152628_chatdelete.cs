using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class chatdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Posts_RelatedPostId",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Posts_RelatedPostId",
                table: "Chats",
                column: "RelatedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Posts_RelatedPostId",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Posts_RelatedPostId",
                table: "Chats",
                column: "RelatedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
