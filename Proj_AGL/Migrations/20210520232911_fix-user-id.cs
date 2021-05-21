using Microsoft.EntityFrameworkCore.Migrations;

namespace Proj_AGL.Migrations
{
    public partial class fixuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_UserId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId1",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Grades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_UserId",
                table: "Grades",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_UserId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId",
                table: "Grades");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Grades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Grades",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId1",
                table: "Grades",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_UserId1",
                table: "Grades",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
