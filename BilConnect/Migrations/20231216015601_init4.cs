using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClubEvent_ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubEvent_AspNetUsers_ownerClubId",
                table: "ClubEvent");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubEvent",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ClubEvent",
                newName: "ClubEvents");

            migrationBuilder.RenameIndex(
                name: "IX_ClubEvent_ownerClubId",
                table: "ClubEvents",
                newName: "IX_ClubEvents_ownerClubId");

            migrationBuilder.AlterColumn<int>(
                name: "GE250_251Points",
                table: "ClubEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubEvents",
                table: "ClubEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubEvents_AspNetUsers_ownerClubId",
                table: "ClubEvents",
                column: "ownerClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubEvents_AspNetUsers_ownerClubId",
                table: "ClubEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubEvents",
                table: "ClubEvents");

            migrationBuilder.RenameTable(
                name: "ClubEvents",
                newName: "ClubEvent");

            migrationBuilder.RenameIndex(
                name: "IX_ClubEvents_ownerClubId",
                table: "ClubEvent",
                newName: "IX_ClubEvent_ownerClubId");

            migrationBuilder.AddColumn<int>(
                name: "ClubEventId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GE250_251Points",
                table: "ClubEvent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubEvent",
                table: "ClubEvent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClubEventId",
                table: "AspNetUsers",
                column: "ClubEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClubEvent_ClubEventId",
                table: "AspNetUsers",
                column: "ClubEventId",
                principalTable: "ClubEvent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubEvent_AspNetUsers_ownerClubId",
                table: "ClubEvent",
                column: "ownerClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
