using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SubscriptionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherTelephone",
                table: "Childern",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherTelephone",
                table: "Childern",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Childern",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    LogoFileAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRCodeFileAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Childern_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Childern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_InvoiceId",
                table: "SubscriptionTypes",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ChildId",
                table: "Invoices",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTypes_Invoices_InvoiceId",
                table: "SubscriptionTypes",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTypes_Invoices_InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionTypes_InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "FatherTelephone",
                table: "Childern");

            migrationBuilder.DropColumn(
                name: "MotherTelephone",
                table: "Childern");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Childern");
        }
    }
}
