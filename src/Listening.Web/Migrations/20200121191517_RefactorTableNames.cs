using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class RefactorTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsersInGroups_ChatGroups_ChatGroupId",
                table: "ChatUsersInGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsersInGroups_AspNetUsers_UserId",
                table: "ChatUsersInGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_FromUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_ToGroupId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogues_AspNetUsers_FromUserId",
                table: "Dialogues");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogues_AspNetUsers_ToUserId",
                table: "Dialogues");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorsForJoined_Results_ResultId",
                table: "ErrorsForJoined");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorsForSeparated_Results_ResultId",
                table: "ErrorsForSeparated");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Cultures_CultureId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UniqueAppIdGenerator",
                table: "UniqueAppIdGenerator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorsForSeparated",
                table: "ErrorsForSeparated");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorsForJoined",
                table: "ErrorsForJoined");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dialogues",
                table: "Dialogues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cultures",
                table: "Cultures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUsersInGroups",
                table: "ChatUsersInGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups");

            migrationBuilder.RenameTable(
                name: "UniqueAppIdGenerator",
                newName: "System_UniqueAppIdGenerator");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "Listening_Results");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Translate_Resources");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Listening_Feedbacks");

            migrationBuilder.RenameTable(
                name: "ErrorsForSeparated",
                newName: "Listening_ErrorsForSeparated");

            migrationBuilder.RenameTable(
                name: "ErrorsForJoined",
                newName: "Listening_ErrorsForJoined");

            migrationBuilder.RenameTable(
                name: "Dialogues",
                newName: "Chat_Dialogues");

            migrationBuilder.RenameTable(
                name: "Cultures",
                newName: "Translate_Cultures");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Chat_Conversations");

            migrationBuilder.RenameTable(
                name: "ChatUsersInGroups",
                newName: "Chat_UsersInGroups");

            migrationBuilder.RenameTable(
                name: "ChatGroups",
                newName: "Chat_Groups");

            migrationBuilder.RenameIndex(
                name: "IX_Results_UserId",
                table: "Listening_Results",
                newName: "IX_Listening_Results_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CultureId",
                table: "Translate_Resources",
                newName: "IX_Translate_Resources_CultureId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_UserId",
                table: "Listening_Feedbacks",
                newName: "IX_Listening_Feedbacks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ErrorsForSeparated_ResultId",
                table: "Listening_ErrorsForSeparated",
                newName: "IX_Listening_ErrorsForSeparated_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_ErrorsForJoined_ResultId",
                table: "Listening_ErrorsForJoined",
                newName: "IX_Listening_ErrorsForJoined_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogues_ToUserId",
                table: "Chat_Dialogues",
                newName: "IX_Chat_Dialogues_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogues_FromUserId",
                table: "Chat_Dialogues",
                newName: "IX_Chat_Dialogues_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_ToGroupId",
                table: "Chat_Conversations",
                newName: "IX_Chat_Conversations_ToGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_FromUserId",
                table: "Chat_Conversations",
                newName: "IX_Chat_Conversations_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsersInGroups_ChatGroupId",
                table: "Chat_UsersInGroups",
                newName: "IX_Chat_UsersInGroups_ChatGroupId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictTokens",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictTokens",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictTokens",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "OpenIddictTokens",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictTokens",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictScopes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictAuthorizations",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictAuthorizations",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictAuthorizations",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictAuthorizations",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictApplications",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictApplications",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "OpenIddictApplications",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_System_UniqueAppIdGenerator",
                table: "System_UniqueAppIdGenerator",
                column: "CurrentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listening_Results",
                table: "Listening_Results",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translate_Resources",
                table: "Translate_Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listening_Feedbacks",
                table: "Listening_Feedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listening_ErrorsForSeparated",
                table: "Listening_ErrorsForSeparated",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listening_ErrorsForJoined",
                table: "Listening_ErrorsForJoined",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Dialogues",
                table: "Chat_Dialogues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translate_Cultures",
                table: "Translate_Cultures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Conversations",
                table: "Chat_Conversations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_UsersInGroups",
                table: "Chat_UsersInGroups",
                columns: new[] { "UserId", "ChatGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Groups",
                table: "Chat_Groups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Conversations_AspNetUsers_FromUserId",
                table: "Chat_Conversations",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Conversations_AspNetUsers_ToGroupId",
                table: "Chat_Conversations",
                column: "ToGroupId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Dialogues_AspNetUsers_FromUserId",
                table: "Chat_Dialogues",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Dialogues_AspNetUsers_ToUserId",
                table: "Chat_Dialogues",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_UsersInGroups_Chat_Groups_ChatGroupId",
                table: "Chat_UsersInGroups",
                column: "ChatGroupId",
                principalTable: "Chat_Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_UsersInGroups_AspNetUsers_UserId",
                table: "Chat_UsersInGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listening_ErrorsForJoined_Listening_Results_ResultId",
                table: "Listening_ErrorsForJoined",
                column: "ResultId",
                principalTable: "Listening_Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listening_ErrorsForSeparated_Listening_Results_ResultId",
                table: "Listening_ErrorsForSeparated",
                column: "ResultId",
                principalTable: "Listening_Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listening_Feedbacks_AspNetUsers_UserId",
                table: "Listening_Feedbacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listening_Results_AspNetUsers_UserId",
                table: "Listening_Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translate_Resources_Translate_Cultures_CultureId",
                table: "Translate_Resources",
                column: "CultureId",
                principalTable: "Translate_Cultures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Conversations_AspNetUsers_FromUserId",
                table: "Chat_Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Conversations_AspNetUsers_ToGroupId",
                table: "Chat_Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Dialogues_AspNetUsers_FromUserId",
                table: "Chat_Dialogues");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Dialogues_AspNetUsers_ToUserId",
                table: "Chat_Dialogues");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_UsersInGroups_Chat_Groups_ChatGroupId",
                table: "Chat_UsersInGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_UsersInGroups_AspNetUsers_UserId",
                table: "Chat_UsersInGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Listening_ErrorsForJoined_Listening_Results_ResultId",
                table: "Listening_ErrorsForJoined");

            migrationBuilder.DropForeignKey(
                name: "FK_Listening_ErrorsForSeparated_Listening_Results_ResultId",
                table: "Listening_ErrorsForSeparated");

            migrationBuilder.DropForeignKey(
                name: "FK_Listening_Feedbacks_AspNetUsers_UserId",
                table: "Listening_Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Listening_Results_AspNetUsers_UserId",
                table: "Listening_Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Translate_Resources_Translate_Cultures_CultureId",
                table: "Translate_Resources");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translate_Resources",
                table: "Translate_Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translate_Cultures",
                table: "Translate_Cultures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System_UniqueAppIdGenerator",
                table: "System_UniqueAppIdGenerator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listening_Results",
                table: "Listening_Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listening_Feedbacks",
                table: "Listening_Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listening_ErrorsForSeparated",
                table: "Listening_ErrorsForSeparated");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listening_ErrorsForJoined",
                table: "Listening_ErrorsForJoined");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_UsersInGroups",
                table: "Chat_UsersInGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Groups",
                table: "Chat_Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Dialogues",
                table: "Chat_Dialogues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Conversations",
                table: "Chat_Conversations");

            migrationBuilder.RenameTable(
                name: "Translate_Resources",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "Translate_Cultures",
                newName: "Cultures");

            migrationBuilder.RenameTable(
                name: "System_UniqueAppIdGenerator",
                newName: "UniqueAppIdGenerator");

            migrationBuilder.RenameTable(
                name: "Listening_Results",
                newName: "Results");

            migrationBuilder.RenameTable(
                name: "Listening_Feedbacks",
                newName: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Listening_ErrorsForSeparated",
                newName: "ErrorsForSeparated");

            migrationBuilder.RenameTable(
                name: "Listening_ErrorsForJoined",
                newName: "ErrorsForJoined");

            migrationBuilder.RenameTable(
                name: "Chat_UsersInGroups",
                newName: "ChatUsersInGroups");

            migrationBuilder.RenameTable(
                name: "Chat_Groups",
                newName: "ChatGroups");

            migrationBuilder.RenameTable(
                name: "Chat_Dialogues",
                newName: "Dialogues");

            migrationBuilder.RenameTable(
                name: "Chat_Conversations",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Translate_Resources_CultureId",
                table: "Resources",
                newName: "IX_Resources_CultureId");

            migrationBuilder.RenameIndex(
                name: "IX_Listening_Results_UserId",
                table: "Results",
                newName: "IX_Results_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Listening_Feedbacks_UserId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Listening_ErrorsForSeparated_ResultId",
                table: "ErrorsForSeparated",
                newName: "IX_ErrorsForSeparated_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Listening_ErrorsForJoined_ResultId",
                table: "ErrorsForJoined",
                newName: "IX_ErrorsForJoined_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UsersInGroups_ChatGroupId",
                table: "ChatUsersInGroups",
                newName: "IX_ChatUsersInGroups_ChatGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Dialogues_ToUserId",
                table: "Dialogues",
                newName: "IX_Dialogues_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Dialogues_FromUserId",
                table: "Dialogues",
                newName: "IX_Dialogues_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Conversations_ToGroupId",
                table: "Conversations",
                newName: "IX_Conversations_ToGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Conversations_FromUserId",
                table: "Conversations",
                newName: "IX_Conversations_FromUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "OpenIddictTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictScopes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictAuthorizations",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictAuthorizations",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictAuthorizations",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictAuthorizations",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictApplications",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "OpenIddictApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cultures",
                table: "Cultures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UniqueAppIdGenerator",
                table: "UniqueAppIdGenerator",
                column: "CurrentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorsForSeparated",
                table: "ErrorsForSeparated",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorsForJoined",
                table: "ErrorsForJoined",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUsersInGroups",
                table: "ChatUsersInGroups",
                columns: new[] { "UserId", "ChatGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatGroups",
                table: "ChatGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dialogues",
                table: "Dialogues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsersInGroups_ChatGroups_ChatGroupId",
                table: "ChatUsersInGroups",
                column: "ChatGroupId",
                principalTable: "ChatGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsersInGroups_AspNetUsers_UserId",
                table: "ChatUsersInGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_FromUserId",
                table: "Conversations",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_ToGroupId",
                table: "Conversations",
                column: "ToGroupId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogues_AspNetUsers_FromUserId",
                table: "Dialogues",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogues_AspNetUsers_ToUserId",
                table: "Dialogues",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorsForJoined_Results_ResultId",
                table: "ErrorsForJoined",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorsForSeparated_Results_ResultId",
                table: "ErrorsForSeparated",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Cultures_CultureId",
                table: "Resources",
                column: "CultureId",
                principalTable: "Cultures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
