using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Listening.Web.Migrations
{
    public partial class AddTimeCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Translate_Resources",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Translate_Cultures",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_Results",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_Feedbacks",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_ErrorsForSeparated",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_ErrorsForJoined",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Groups",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Dialogues",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Conversations",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Topics",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Priorities",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_PostTopics",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Blog_Posts",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_MediaTypes",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Attachments",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "AspNetUsers",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetUserClaims",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "AspNetRoles",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetRoleClaims",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "ApplicationUserPhotos",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateTable(
                name: "TimeCode_Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCode_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeCode_TimeStamps",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Seconds = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCode_TimeStamps", x => new { x.VideoId, x.UserId, x.Seconds });
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeCode_TimeStamps");

            migrationBuilder.DropTable(
                name: "TimeCode_Videos");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Translate_Resources",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Translate_Cultures",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_Results",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_Feedbacks",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_ErrorsForSeparated",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Listening_ErrorsForJoined",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Groups",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Dialogues",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Chat_Conversations",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Topics",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Priorities",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_PostTopics",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "Blog_Posts",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_MediaTypes",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Blog_Attachments",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "AspNetUsers",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetUserClaims",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "AspNetRoles",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetRoleClaims",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    table: "ApplicationUserPhotos",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long))
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
