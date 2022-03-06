using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class MakeUsersEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_TimeCode_TimeStamps",
            //    table: "TimeCode_TimeStamps");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_TimeCode_TimeStamps",
            //    table: "TimeCode_TimeStamps",
            //    column: "VideoId");

            migrationBuilder.Sql($"UPDATE public.\"AspNetUsers\" SET \"IsEnabled\"=TRUE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"UPDATE public.\"AspNetUsers\" SET \"IsEnabled\"=FALSE");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_TimeCode_TimeStamps",
            //    table: "TimeCode_TimeStamps");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_TimeCode_TimeStamps",
            //    table: "TimeCode_TimeStamps",
            //    columns: new[] { "VideoId", "UserId", "Seconds" });
        }
    }
}
