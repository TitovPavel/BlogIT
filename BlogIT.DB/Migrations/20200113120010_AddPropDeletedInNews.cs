using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogIT.DB.Migrations
{
    public partial class AddPropDeletedInNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "News",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "News");
        }
    }
}
