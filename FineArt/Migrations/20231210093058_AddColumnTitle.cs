using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FineArt.Migrations
{
    public partial class AddColumnTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExhibitionTitle",
                table: "Exhibitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExhibitionTitle",
                table: "Exhibitions");
        }
    }
}
