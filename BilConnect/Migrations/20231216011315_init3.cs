using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubEvent_AspNetUsers_ApplicationUserId",
                table: "ClubEvent");

            migrationBuilder.DropIndex(
                name: "IX_ClubEvent_ApplicationUserId",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ClubEvent");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClubEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GE250_251Points",
                table: "ClubEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GE250_251Status",
                table: "ClubEvent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "ClubEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ClubEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "ClubEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "endTime",
                table: "ClubEvent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ownerClubId",
                table: "ClubEvent",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "quota",
                table: "ClubEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "ClubEvent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abbrevation",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<int>(
                name: "ClubEventId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubEvent_ownerClubId",
                table: "ClubEvent",
                column: "ownerClubId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClubEvent_ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubEvent_AspNetUsers_ownerClubId",
                table: "ClubEvent");

            migrationBuilder.DropIndex(
                name: "IX_ClubEvent_ownerClubId",
                table: "ClubEvent");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "GE250_251Points",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "GE250_251Status",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "endTime",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "ownerClubId",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "quota",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "ClubEvent");

            migrationBuilder.DropColumn(
                name: "ClubEventId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ClubEvent",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abbrevation",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubEvent_ApplicationUserId",
                table: "ClubEvent",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubEvent_AspNetUsers_ApplicationUserId",
                table: "ClubEvent",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
