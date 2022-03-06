using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class Added_Tables_Author_And_Books_For_Knowledge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Knowledge_Course",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OriginalLink",
                table: "Knowledge_Course",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalSite",
                table: "Knowledge_Course",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_FileType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_FileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_Book",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileTypeId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knowledge_Book_Knowledge_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Knowledge_Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knowledge_Book_Knowledge_FileType_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "Knowledge_FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Course_AuthorId",
                table: "Knowledge_Course",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Book_CourseId",
                table: "Knowledge_Book",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Book_FileTypeId",
                table: "Knowledge_Book",
                column: "FileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Course_Authors_AuthorId",
                table: "Knowledge_Course",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Course_Authors_AuthorId",
                table: "Knowledge_Course");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Knowledge_Book");

            migrationBuilder.DropTable(
                name: "Knowledge_FileType");

            migrationBuilder.DropIndex(
                name: "IX_Knowledge_Course_AuthorId",
                table: "Knowledge_Course");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Knowledge_Course");

            migrationBuilder.DropColumn(
                name: "OriginalLink",
                table: "Knowledge_Course");

            migrationBuilder.DropColumn(
                name: "OriginalSite",
                table: "Knowledge_Course");
        }
    }
}
