using Microsoft.EntityFrameworkCore.Migrations;

namespace aepetria_vue_api.Migrations
{
    public partial class RemoteImagesExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "RemoteImages",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "RemoteImages");
        }
    }
}
