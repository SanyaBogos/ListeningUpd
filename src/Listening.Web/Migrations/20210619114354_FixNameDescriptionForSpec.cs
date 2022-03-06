using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class FixNameDescriptionForSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "Knowledge_Videos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Knowledge_Videos",
                maxLength: 5500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Knowledge_Folders",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Knowledge_Videos");

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "Knowledge_Videos",
                type: "character varying(5500)",
                maxLength: 5500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Knowledge_Folders",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);
        }
    }
}
