using Microsoft.EntityFrameworkCore.Migrations;

namespace SadadMisr.DAL.Migrations
{
    public partial class ShippingLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingAgencyId",
                table: "ShippingLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLines_ShippingAgencyId",
                table: "ShippingLines",
                column: "ShippingAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingLines_ShippingAgencies_ShippingAgencyId",
                table: "ShippingLines",
                column: "ShippingAgencyId",
                principalTable: "ShippingAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingLines_ShippingAgencies_ShippingAgencyId",
                table: "ShippingLines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ShippingLines_ShippingAgencyId",
                table: "ShippingLines");

            migrationBuilder.DropColumn(
                name: "ShippingAgencyId",
                table: "ShippingLines");
        }
    }
}
