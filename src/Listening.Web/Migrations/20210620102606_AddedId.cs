using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class AddedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Access",
                table: "Knowledge_Access");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Knowledge_Access",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Access",
                table: "Knowledge_Access",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Access",
                table: "Knowledge_Access");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Knowledge_Access");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Access",
                table: "Knowledge_Access",
                columns: new[] { "CourseId", "RoleId" });
        }
    }
}
