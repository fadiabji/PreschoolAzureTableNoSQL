using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixinginvoicesubtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTypes_Invoices_InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionTypes_InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SubscriptionTypes");

            migrationBuilder.CreateTable(
                name: "InvoiceSubscriptionType",
                columns: table => new
                {
                    InvoiceItemsId = table.Column<int>(type: "int", nullable: false),
                    InvoicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSubscriptionType", x => new { x.InvoiceItemsId, x.InvoicesId });
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionType_Invoices_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionType_SubscriptionTypes_InvoiceItemsId",
                        column: x => x.InvoiceItemsId,
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionType_InvoicesId",
                table: "InvoiceSubscriptionType",
                column: "InvoicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceSubscriptionType");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SubscriptionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_InvoiceId",
                table: "SubscriptionTypes",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTypes_Invoices_InvoiceId",
                table: "SubscriptionTypes",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
