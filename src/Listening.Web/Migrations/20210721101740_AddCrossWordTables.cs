using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class AddCrossWordTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crossword_CrosswordDescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<long>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossword_CrosswordDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crossword_CrosswordDescriptions_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crossword_QuestionDescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    Question = table.Column<string>(maxLength: 3000, nullable: false),
                    Answer = table.Column<string>(maxLength: 300, nullable: false),
                    Length = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossword_QuestionDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crossword_WordDescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartPointX = table.Column<int>(nullable: false),
                    StartPointY = table.Column<int>(nullable: false),
                    Direction = table.Column<byte>(nullable: false),
                    QuestionDescriptionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossword_WordDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crossword_WordDescriptions_Crossword_QuestionDescriptions_Q~",
                        column: x => x.QuestionDescriptionId,
                        principalTable: "Crossword_QuestionDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crossword_Crosswords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CrosswordDescriptionId = table.Column<long>(nullable: false),
                    WordDescriptionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossword_Crosswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crossword_Crosswords_Crossword_CrosswordDescriptions_Crossw~",
                        column: x => x.CrosswordDescriptionId,
                        principalTable: "Crossword_CrosswordDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crossword_Crosswords_Crossword_WordDescriptions_WordDescrip~",
                        column: x => x.WordDescriptionId,
                        principalTable: "Crossword_WordDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crossword_CrosswordResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: true),
                    IsStarted = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CrosswordId = table.Column<long>(nullable: false),
                    WordDescriptionId = table.Column<long>(nullable: false),
                    ResultsEncodedString = table.Column<bool[]>(maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossword_CrosswordResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crossword_CrosswordResults_Crossword_Crosswords_CrosswordId",
                        column: x => x.CrosswordId,
                        principalTable: "Crossword_Crosswords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crossword_CrosswordResults_Crossword_WordDescriptions_WordD~",
                        column: x => x.WordDescriptionId,
                        principalTable: "Crossword_WordDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_CrosswordDescriptions_AssigneeId",
                table: "Crossword_CrosswordDescriptions",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_CrosswordResults_CrosswordId",
                table: "Crossword_CrosswordResults",
                column: "CrosswordId");

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_CrosswordResults_WordDescriptionId",
                table: "Crossword_CrosswordResults",
                column: "WordDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_Crosswords_CrosswordDescriptionId",
                table: "Crossword_Crosswords",
                column: "CrosswordDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_Crosswords_WordDescriptionId",
                table: "Crossword_Crosswords",
                column: "WordDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Crossword_WordDescriptions_QuestionDescriptionId",
                table: "Crossword_WordDescriptions",
                column: "QuestionDescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crossword_CrosswordResults");

            migrationBuilder.DropTable(
                name: "Crossword_Crosswords");

            migrationBuilder.DropTable(
                name: "Crossword_CrosswordDescriptions");

            migrationBuilder.DropTable(
                name: "Crossword_WordDescriptions");

            migrationBuilder.DropTable(
                name: "Crossword_QuestionDescriptions");
        }
    }
}
