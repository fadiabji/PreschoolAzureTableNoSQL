using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeinvoiceitemsTosubtypsProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSubscriptionType_SubscriptionTypes_InvoiceItemsId",
                table: "InvoiceSubscriptionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSubscriptionType",
                table: "InvoiceSubscriptionType");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceSubscriptionType_InvoicesId",
                table: "InvoiceSubscriptionType");

            migrationBuilder.RenameColumn(
                name: "InvoiceItemsId",
                table: "InvoiceSubscriptionType",
                newName: "SubscriptionTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSubscriptionType",
                table: "InvoiceSubscriptionType",
                columns: new[] { "InvoicesId", "SubscriptionTypesId" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionType_SubscriptionTypesId",
                table: "InvoiceSubscriptionType",
                column: "SubscriptionTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSubscriptionType_SubscriptionTypes_SubscriptionTypesId",
                table: "InvoiceSubscriptionType",
                column: "SubscriptionTypesId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSubscriptionType_SubscriptionTypes_SubscriptionTypesId",
                table: "InvoiceSubscriptionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSubscriptionType",
                table: "InvoiceSubscriptionType");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceSubscriptionType_SubscriptionTypesId",
                table: "InvoiceSubscriptionType");

            migrationBuilder.RenameColumn(
                name: "SubscriptionTypesId",
                table: "InvoiceSubscriptionType",
                newName: "InvoiceItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSubscriptionType",
                table: "InvoiceSubscriptionType",
                columns: new[] { "InvoiceItemsId", "InvoicesId" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSubscriptionType_InvoicesId",
                table: "InvoiceSubscriptionType",
                column: "InvoicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSubscriptionType_SubscriptionTypes_InvoiceItemsId",
                table: "InvoiceSubscriptionType",
                column: "InvoiceItemsId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
