using Microsoft.EntityFrameworkCore.Migrations;

namespace SadadMisr.DAL.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineID",
                table: "Manifests",
                newName: "LineManifestId");

            migrationBuilder.RenameColumn(
                name: "EstimatedArrivalDate",
                table: "Manifests",
                newName: "EstimatedDate");

            migrationBuilder.RenameColumn(
                name: "LineBillID",
                table: "Bills",
                newName: "LineBillId");

            migrationBuilder.RenameColumn(
                name: "ConsigneeTaxNumber",
                table: "Bills",
                newName: "SCAC");

            migrationBuilder.RenameColumn(
                name: "ConsigneeName",
                table: "Bills",
                newName: "CustomerTaxNumber");

            migrationBuilder.RenameColumn(
                name: "ConsigneeMobileNumber",
                table: "Bills",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "ConsigneeID",
                table: "Bills",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ConsigneeEMail",
                table: "Bills",
                newName: "CustomerMobileNumber");

            migrationBuilder.AddColumn<string>(
                name: "Containers20",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Containers40",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Containers20",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Containers40",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "LineManifestId",
                table: "Manifests",
                newName: "LineID");

            migrationBuilder.RenameColumn(
                name: "EstimatedDate",
                table: "Manifests",
                newName: "EstimatedArrivalDate");

            migrationBuilder.RenameColumn(
                name: "LineBillId",
                table: "Bills",
                newName: "LineBillID");

            migrationBuilder.RenameColumn(
                name: "SCAC",
                table: "Bills",
                newName: "ConsigneeTaxNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerTaxNumber",
                table: "Bills",
                newName: "ConsigneeName");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Bills",
                newName: "ConsigneeMobileNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerMobileNumber",
                table: "Bills",
                newName: "ConsigneeEMail");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Bills",
                newName: "ConsigneeID");
        }
    }
}
