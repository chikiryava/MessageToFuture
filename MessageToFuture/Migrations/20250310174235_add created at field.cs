using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageToFuture.Migrations
{
    /// <inheritdoc />
    public partial class addcreatedatfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Messages",
                newName: "DeliveryDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "DeliveryDateTime",
                table: "Messages",
                newName: "SendDate");
        }
    }
}
