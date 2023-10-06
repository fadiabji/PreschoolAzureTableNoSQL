using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionTypeforChildModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Childern_ChildId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ChildId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ChildId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Childern");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "SubscriptionTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Subscriptions",
                newName: "IsActive");

            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ChildId",
                table: "Subscriptions",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Childern_ChildId",
                table: "Subscriptions",
                column: "ChildId",
                principalTable: "Childern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Childern_ChildId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ChildId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubscriptionTypes",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Subscriptions",
                newName: "Active");

            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ChildId1",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Childern",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ChildId1",
                table: "Subscriptions",
                column: "ChildId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Childern_ChildId1",
                table: "Subscriptions",
                column: "ChildId1",
                principalTable: "Childern",
                principalColumn: "Id");
        }
    }
}
