using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class AddBlogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog_MediaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_MediaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Posts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    Header = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 3500, nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Posts_Blog_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Blog_Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Path = table.Column<string>(nullable: true),
                    MediatypeId = table.Column<int>(nullable: false),
                    PostId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Attachments_Blog_MediaTypes_MediatypeId",
                        column: x => x.MediatypeId,
                        principalTable: "Blog_MediaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Attachments_Blog_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Blog_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog_PostTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<long>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_PostTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_PostTopics_Blog_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Blog_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_PostTopics_Blog_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Blog_Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Attachments_MediatypeId",
                table: "Blog_Attachments",
                column: "MediatypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Attachments_PostId",
                table: "Blog_Attachments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Posts_PriorityId",
                table: "Blog_Posts",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Posts_UserId",
                table: "Blog_Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_PostTopics_PostId",
                table: "Blog_PostTopics",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_PostTopics_TopicId",
                table: "Blog_PostTopics",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog_Attachments");

            migrationBuilder.DropTable(
                name: "Blog_PostTopics");

            migrationBuilder.DropTable(
                name: "Blog_MediaTypes");

            migrationBuilder.DropTable(
                name: "Blog_Posts");

            migrationBuilder.DropTable(
                name: "Blog_Topics");

            migrationBuilder.DropTable(
                name: "Blog_Priorities");
        }
    }
}
