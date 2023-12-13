using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FineArt.Migrations
{
    public partial class AddCompetitionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NotShowHide",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompetitionId",
                table: "Notifications",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Competitions_CompetitionId",
                table: "Notifications",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "CompetitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Competitions_CompetitionId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CompetitionId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "NotShowHide",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
