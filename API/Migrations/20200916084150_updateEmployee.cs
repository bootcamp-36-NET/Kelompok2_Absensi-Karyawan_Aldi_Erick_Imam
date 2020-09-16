using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Employee_tb_m_user_EmployeeId",
                table: "Tb_Employee",
                column: "EmployeeId",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Employee_tb_m_user_EmployeeId",
                table: "Tb_Employee");
        }
    }
}
