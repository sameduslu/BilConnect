using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilConnect.Migrations
{
    /// <inheritdoc />
    public partial class lastseenupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastSeen",
                table: "Chats",
                newName: "SenderLastSeen");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiverLastSeen",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverLastSeen",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "SenderLastSeen",
                table: "Chats",
                newName: "lastSeen");
        }
    }
}
