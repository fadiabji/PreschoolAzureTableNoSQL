using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixinginvoicesubtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceSubscriptionType");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceSubscriptionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSubscriptionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionTypes_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionTypes_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SubscriptionTypeId",
                table: "Invoices",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionTypes_InvoiceId",
                table: "InvoiceSubscriptionTypes",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionTypes_SubscriptionTypeId",
                table: "InvoiceSubscriptionTypes",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_SubscriptionTypes_SubscriptionTypeId",
                table: "Invoices",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_SubscriptionTypes_SubscriptionTypeId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceSubscriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SubscriptionTypeId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "InvoiceSubscriptionType",
                columns: table => new
                {
                    InvoicesId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSubscriptionType", x => new { x.InvoicesId, x.SubscriptionTypesId });
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionType_Invoices_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceSubscriptionType_SubscriptionTypes_SubscriptionTypesId",
                        column: x => x.SubscriptionTypesId,
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionType_SubscriptionTypesId",
                table: "InvoiceSubscriptionType",
                column: "SubscriptionTypesId");
        }
    }
}
