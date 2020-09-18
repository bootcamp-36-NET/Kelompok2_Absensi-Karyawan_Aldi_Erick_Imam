using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class absenceV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAbsence",
                table: "Absence",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAbsence",
                table: "Absence");
        }
    }
}
