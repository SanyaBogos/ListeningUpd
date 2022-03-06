using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class Added_Fix_For_Knowledge_Names_And_Path_To_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Book_Knowledge_Course_CourseId",
                table: "Knowledge_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Book_Knowledge_FileType_FileTypeId",
                table: "Knowledge_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Course_Authors_AuthorId",
                table: "Knowledge_Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Course_Knowledge_Type_TypeId",
                table: "Knowledge_Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Folder_Knowledge_Course_CourseId",
                table: "Knowledge_Folder");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_TimeStamps_Knowledge_Video_VideoId",
                table: "Knowledge_TimeStamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Video_Knowledge_Folder_FolderId",
                table: "Knowledge_Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Video",
                table: "Knowledge_Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Type",
                table: "Knowledge_Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Folder",
                table: "Knowledge_Folder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_FileType",
                table: "Knowledge_FileType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Course",
                table: "Knowledge_Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Book",
                table: "Knowledge_Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Knowledge_Video",
                newName: "Knowledge_Videos");

            migrationBuilder.RenameTable(
                name: "Knowledge_Type",
                newName: "Knowledge_Types");

            migrationBuilder.RenameTable(
                name: "Knowledge_Folder",
                newName: "Knowledge_Folders");

            migrationBuilder.RenameTable(
                name: "Knowledge_FileType",
                newName: "Knowledge_FileTypes");

            migrationBuilder.RenameTable(
                name: "Knowledge_Course",
                newName: "Knowledge_Courses");

            migrationBuilder.RenameTable(
                name: "Knowledge_Book",
                newName: "Knowledge_Books");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Knowledge_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Video_FolderId",
                table: "Knowledge_Videos",
                newName: "IX_Knowledge_Videos_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Folder_CourseId",
                table: "Knowledge_Folders",
                newName: "IX_Knowledge_Folders_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Course_TypeId",
                table: "Knowledge_Courses",
                newName: "IX_Knowledge_Courses_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Course_AuthorId",
                table: "Knowledge_Courses",
                newName: "IX_Knowledge_Courses_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Book_FileTypeId",
                table: "Knowledge_Books",
                newName: "IX_Knowledge_Books_FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Book_CourseId",
                table: "Knowledge_Books",
                newName: "IX_Knowledge_Books_CourseId");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Knowledge_Books",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Videos",
                table: "Knowledge_Videos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Types",
                table: "Knowledge_Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Folders",
                table: "Knowledge_Folders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_FileTypes",
                table: "Knowledge_FileTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Courses",
                table: "Knowledge_Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Books",
                table: "Knowledge_Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Authors",
                table: "Knowledge_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Books_Knowledge_Courses_CourseId",
                table: "Knowledge_Books",
                column: "CourseId",
                principalTable: "Knowledge_Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Books_Knowledge_FileTypes_FileTypeId",
                table: "Knowledge_Books",
                column: "FileTypeId",
                principalTable: "Knowledge_FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Courses_Knowledge_Authors_AuthorId",
                table: "Knowledge_Courses",
                column: "AuthorId",
                principalTable: "Knowledge_Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Courses_Knowledge_Types_TypeId",
                table: "Knowledge_Courses",
                column: "TypeId",
                principalTable: "Knowledge_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Folders_Knowledge_Courses_CourseId",
                table: "Knowledge_Folders",
                column: "CourseId",
                principalTable: "Knowledge_Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_TimeStamps_Knowledge_Videos_VideoId",
                table: "Knowledge_TimeStamps",
                column: "VideoId",
                principalTable: "Knowledge_Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Videos_Knowledge_Folders_FolderId",
                table: "Knowledge_Videos",
                column: "FolderId",
                principalTable: "Knowledge_Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Books_Knowledge_Courses_CourseId",
                table: "Knowledge_Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Books_Knowledge_FileTypes_FileTypeId",
                table: "Knowledge_Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Courses_Knowledge_Authors_AuthorId",
                table: "Knowledge_Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Courses_Knowledge_Types_TypeId",
                table: "Knowledge_Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Folders_Knowledge_Courses_CourseId",
                table: "Knowledge_Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_TimeStamps_Knowledge_Videos_VideoId",
                table: "Knowledge_TimeStamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledge_Videos_Knowledge_Folders_FolderId",
                table: "Knowledge_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Videos",
                table: "Knowledge_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Types",
                table: "Knowledge_Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Folders",
                table: "Knowledge_Folders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_FileTypes",
                table: "Knowledge_FileTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Courses",
                table: "Knowledge_Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Books",
                table: "Knowledge_Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Knowledge_Authors",
                table: "Knowledge_Authors");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Knowledge_Books");

            migrationBuilder.RenameTable(
                name: "Knowledge_Videos",
                newName: "Knowledge_Video");

            migrationBuilder.RenameTable(
                name: "Knowledge_Types",
                newName: "Knowledge_Type");

            migrationBuilder.RenameTable(
                name: "Knowledge_Folders",
                newName: "Knowledge_Folder");

            migrationBuilder.RenameTable(
                name: "Knowledge_FileTypes",
                newName: "Knowledge_FileType");

            migrationBuilder.RenameTable(
                name: "Knowledge_Courses",
                newName: "Knowledge_Course");

            migrationBuilder.RenameTable(
                name: "Knowledge_Books",
                newName: "Knowledge_Book");

            migrationBuilder.RenameTable(
                name: "Knowledge_Authors",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Videos_FolderId",
                table: "Knowledge_Video",
                newName: "IX_Knowledge_Video_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Folders_CourseId",
                table: "Knowledge_Folder",
                newName: "IX_Knowledge_Folder_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Courses_TypeId",
                table: "Knowledge_Course",
                newName: "IX_Knowledge_Course_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Courses_AuthorId",
                table: "Knowledge_Course",
                newName: "IX_Knowledge_Course_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Books_FileTypeId",
                table: "Knowledge_Book",
                newName: "IX_Knowledge_Book_FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Knowledge_Books_CourseId",
                table: "Knowledge_Book",
                newName: "IX_Knowledge_Book_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Video",
                table: "Knowledge_Video",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Type",
                table: "Knowledge_Type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Folder",
                table: "Knowledge_Folder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_FileType",
                table: "Knowledge_FileType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Course",
                table: "Knowledge_Course",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Knowledge_Book",
                table: "Knowledge_Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Book_Knowledge_Course_CourseId",
                table: "Knowledge_Book",
                column: "CourseId",
                principalTable: "Knowledge_Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Book_Knowledge_FileType_FileTypeId",
                table: "Knowledge_Book",
                column: "FileTypeId",
                principalTable: "Knowledge_FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Course_Authors_AuthorId",
                table: "Knowledge_Course",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Course_Knowledge_Type_TypeId",
                table: "Knowledge_Course",
                column: "TypeId",
                principalTable: "Knowledge_Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Folder_Knowledge_Course_CourseId",
                table: "Knowledge_Folder",
                column: "CourseId",
                principalTable: "Knowledge_Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_TimeStamps_Knowledge_Video_VideoId",
                table: "Knowledge_TimeStamps",
                column: "VideoId",
                principalTable: "Knowledge_Video",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledge_Video_Knowledge_Folder_FolderId",
                table: "Knowledge_Video",
                column: "FolderId",
                principalTable: "Knowledge_Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
