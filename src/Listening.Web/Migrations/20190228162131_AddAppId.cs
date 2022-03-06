using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class AddAppId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Dialogues",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Dialogues",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime));
        }
    }
}
