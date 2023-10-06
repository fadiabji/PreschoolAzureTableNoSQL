using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Attendences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Childern_ChildId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Attendances");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_ChildId",
                table: "Attendances",
                newName: "IX_Attendances_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Childern_ChildId",
                table: "Attendances",
                column: "ChildId",
                principalTable: "Childern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Childern_ChildId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "Attendance");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_ChildId",
                table: "Attendance",
                newName: "IX_Attendance_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Childern_ChildId",
                table: "Attendance",
                column: "ChildId",
                principalTable: "Childern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
