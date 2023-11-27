using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FineArt.Migrations
{
    public partial class AddColumnApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedStatus",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedStatus",
                table: "AdminManager",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedStatus",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ApprovedStatus",
                table: "AdminManager");
        }
    }
}
