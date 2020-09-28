using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class removeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_absence_UserId",
                table: "tb_m_absence");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_absence_UserId",
                table: "tb_m_absence",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_absence_UserId",
                table: "tb_m_absence");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_absence_UserId",
                table: "tb_m_absence",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
