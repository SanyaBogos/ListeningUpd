using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class Add_Repeat_To_Video : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Repeat",
                table: "Knowledge_Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoTypeId",
                table: "Knowledge_Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Knowledge_VideoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_VideoTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Videos_VideoTypeId",
                table: "Knowledge_Videos",
                column: "VideoTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Videos_Knowledge_VideoTypes_VideoTypeId",
                table: "Knowledge_Videos",
                column: "VideoTypeId",
                principalTable: "Knowledge_VideoTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Videos_Knowledge_VideoTypes_VideoTypeId",
                table: "Knowledge_Videos");

            migrationBuilder.DropTable(
                name: "Knowledge_VideoTypes");

            migrationBuilder.DropIndex(
                name: "IX_Knowledge_Videos_VideoTypeId",
                table: "Knowledge_Videos");

            migrationBuilder.DropColumn(
                name: "Repeat",
                table: "Knowledge_Videos");

            migrationBuilder.DropColumn(
                name: "VideoTypeId",
                table: "Knowledge_Videos");
        }
    }
}
