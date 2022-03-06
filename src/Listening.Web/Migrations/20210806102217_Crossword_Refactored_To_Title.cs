using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class Crossword_Refactored_To_Title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Crossword_CrosswordDescriptions");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Crossword_CrosswordDescriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Crossword_CrosswordDescriptions",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Crossword_CrosswordDescriptions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Crossword_CrosswordDescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Crossword_CrosswordDescriptions",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
