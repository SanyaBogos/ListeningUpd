using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class Added_Knowledge_Video_Courses_Time_codes_moved_into_Knowledge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeCode_TimeStamps");

            migrationBuilder.DropTable(
                name: "TimeCode_Videos");

            migrationBuilder.CreateTable(
                name: "Knowledge_Type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Decription = table.Column<string>(maxLength: 2500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knowledge_Course_Knowledge_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Knowledge_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_Folder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Path = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Folder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knowledge_Folder_Knowledge_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Knowledge_Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_Video",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FolderId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Path = table.Column<string>(maxLength: 500, nullable: true),
                    Decription = table.Column<string>(maxLength: 5500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knowledge_Video_Knowledge_Folder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Knowledge_Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_TimeStamps",
                columns: table => new
                {
                    VideoId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Seconds = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_TimeStamps", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Knowledge_TimeStamps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knowledge_TimeStamps_Knowledge_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Knowledge_Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Course_TypeId",
                table: "Knowledge_Course",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Folder_CourseId",
                table: "Knowledge_Folder",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_TimeStamps_UserId",
                table: "Knowledge_TimeStamps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Video_FolderId",
                table: "Knowledge_Video",
                column: "FolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Knowledge_TimeStamps");

            migrationBuilder.DropTable(
                name: "Knowledge_Video");

            migrationBuilder.DropTable(
                name: "Knowledge_Folder");

            migrationBuilder.DropTable(
                name: "Knowledge_Course");

            migrationBuilder.DropTable(
                name: "Knowledge_Type");

            migrationBuilder.CreateTable(
                name: "TimeCode_Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCode_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeCode_TimeStamps",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Seconds = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCode_TimeStamps", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_TimeCode_TimeStamps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeCode_TimeStamps_TimeCode_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "TimeCode_Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeCode_TimeStamps_UserId",
                table: "TimeCode_TimeStamps",
                column: "UserId");
        }
    }
}
