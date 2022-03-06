using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class AddedTimeSpent_To_Results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSpentMiliSeconds",
                table: "Listening_Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpentMiliSeconds",
                table: "Listening_Results");
        }
    }
}
