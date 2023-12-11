using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FineArt.Migrations
{
    public partial class AddExStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExStatus",
                table: "Exhibitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExStatus",
                table: "Exhibitions");
        }
    }
}
