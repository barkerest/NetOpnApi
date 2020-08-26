using Microsoft.EntityFrameworkCore.Migrations;

namespace NetOpnApiBuilder.Migrations
{
    public partial class AddSkipFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Skip",
                table: "ApiModules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Skip",
                table: "ApiControllers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Skip",
                table: "ApiCommands",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skip",
                table: "ApiModules");

            migrationBuilder.DropColumn(
                name: "Skip",
                table: "ApiControllers");

            migrationBuilder.DropColumn(
                name: "Skip",
                table: "ApiCommands");
        }
    }
}
