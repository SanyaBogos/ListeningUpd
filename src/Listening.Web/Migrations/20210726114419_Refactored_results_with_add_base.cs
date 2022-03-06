using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class Refactored_results_with_add_base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Crossword_QuestionDescriptions");

            migrationBuilder.AddColumn<int>(
                name: "TimeSpentMiliSeconds",
                table: "Crossword_CrosswordResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpentMiliSeconds",
                table: "Crossword_CrosswordResults");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Crossword_QuestionDescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
