using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class IncreaseKnowledgeCoursesDescriptionSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "Knowledge_Courses");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Knowledge_Courses",
                maxLength: 5055,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Knowledge_Courses");

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "Knowledge_Courses",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: true);
        }
    }
}
