using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class AddTableForUniqueAppIdGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UniqueAppIdGenerator",
                columns: table => new
                {
                    CurrentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueAppIdGenerator", x => x.CurrentId);
                });

            migrationBuilder.Sql(@"INSERT INTO public.""UniqueAppIdGenerator""(""CurrentId"") VALUES(1); ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniqueAppIdGenerator");
        }
    }
}
