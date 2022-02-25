using Microsoft.EntityFrameworkCore.Migrations;

namespace SadadMisr.DAL.Migrations
{
    public partial class deletekelany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kelany",
                table: "Bills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kelany",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
