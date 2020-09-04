using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.DAL.Migrations
{
    public partial class AddStatusFieldInQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Questions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Questions");
        }
    }
}
