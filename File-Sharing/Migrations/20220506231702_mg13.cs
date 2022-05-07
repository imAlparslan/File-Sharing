using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace File_Sharing.Migrations
{
    public partial class mg13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FileSize",
                table: "Documents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Documents");
        }
    }
}
