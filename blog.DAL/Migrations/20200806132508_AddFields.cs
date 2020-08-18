using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.DAL.Migrations
{
    public partial class AddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Questions");
        }
    }
}
