using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class Change_Direction_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Direction",
                table: "Crossword_WordDescriptions",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.Sql($@"ALTER TABLE public.""Crossword_WordDescriptions"" ADD CONSTRAINT CW_WordDescription_Direction_Check CHECK (""Direction"" in ('r', 'd', 'l', 'u'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Direction",
                table: "Crossword_WordDescriptions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(char));

            // this doesn't work fine, however, that's not necessary, I hope...
            // migrationBuilder.Sql($@"ALTER TABLE public.""Crossword_WordDescriptions"" DROP CONSTRAINT CW_WordDescription_Direction_Check");
            // migrationBuilder.Sql($@"    ");
        }
    }
}
