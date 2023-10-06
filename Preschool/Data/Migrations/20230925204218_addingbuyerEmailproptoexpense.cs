using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingbuyerEmailproptoexpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Childern");

            migrationBuilder.AddColumn<string>(
                name: "BuyerEmail",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerEmail",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Childern",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
