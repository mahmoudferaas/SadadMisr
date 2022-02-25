using Microsoft.EntityFrameworkCore.Migrations;

namespace SadadMisr.DAL.Migrations
{
    public partial class removeColumnsFromInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_ShippingLines_ShippingLineId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ShippingLineId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ShippingLineId",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceTypeId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingLineId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ShippingLineId",
                table: "Invoices",
                column: "ShippingLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_ShippingLines_ShippingLineId",
                table: "Invoices",
                column: "ShippingLineId",
                principalTable: "ShippingLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
