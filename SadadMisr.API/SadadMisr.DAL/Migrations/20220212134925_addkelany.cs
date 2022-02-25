using Microsoft.EntityFrameworkCore.Migrations;

namespace SadadMisr.DAL.Migrations
{
    public partial class addkelany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kelany",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kelany",
                table: "Bills");
        }
    }
}
