using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class Added_Courses_Access_Roles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Knowledge_Access",
                columns: table => new
                {
                    // Id = table.Column<int>(nullable: false)
                    //     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Access", x => new { x.CourseId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Knowledge_Access_Knowledge_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Knowledge_Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knowledge_Access_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Access_CourseId",
                table: "Knowledge_Access",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Access_RoleId",
                table: "Knowledge_Access",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Knowledge_Access");
        }
    }
}
