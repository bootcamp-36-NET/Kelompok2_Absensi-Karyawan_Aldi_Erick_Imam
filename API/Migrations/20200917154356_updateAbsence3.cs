using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateAbsence3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_tb_m_user_Id",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Absence",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Absence_UserId",
                table: "Absence",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_tb_m_user_UserId",
                table: "Absence",
                column: "UserId",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_tb_m_user_UserId",
                table: "Absence");

            migrationBuilder.DropIndex(
                name: "IX_Absence_UserId",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Absence",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_tb_m_user_Id",
                table: "Absence",
                column: "Id",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
