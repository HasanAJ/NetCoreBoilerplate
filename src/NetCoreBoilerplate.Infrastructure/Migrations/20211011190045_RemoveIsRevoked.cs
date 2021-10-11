using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreBoilerplate.Infrastructure.Migrations
{
    public partial class RemoveIsRevoked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "RefreshToken",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
