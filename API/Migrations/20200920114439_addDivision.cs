using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionsId",
                table: "tb_m_employee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_DivisionsId",
                table: "tb_m_employee",
                column: "DivisionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_division_DivisionsId",
                table: "tb_m_employee",
                column: "DivisionsId",
                principalTable: "tb_m_division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_division_DivisionsId",
                table: "tb_m_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_employee_DivisionsId",
                table: "tb_m_employee");

            migrationBuilder.DropColumn(
                name: "DivisionsId",
                table: "tb_m_employee");
        }
    }
}
