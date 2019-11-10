using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aepetria_vue_api.Migrations
{
    public partial class RemoteImagesBlobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "RemoteImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "RemoteImages",
                type: "mediumblob",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "RemoteImages");

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "RemoteImages",
                nullable: false,
                defaultValue: "");
        }
    }
}
