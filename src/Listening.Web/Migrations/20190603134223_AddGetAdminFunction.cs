using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class AddGetAdminFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                CREATE OR REPLACE FUNCTION getAdmins(currentAdminId bigint)
                    RETURNS TABLE (""Id"" bigint, ""Email"" character varying(256)) AS
                    $func$
                    BEGIN
                    IF not EXISTS (SELECT 1 FROM public.""AspNetUsers"" U
                        	join public.""AspNetUserRoles"" UR on U.""Id"" = UR.""UserId""
                    		join public.""AspNetRoles"" R on R.""Id"" = UR.""RoleId"" 
                    		   WHERE U.""Id"" = currentAdminId and R.""Name"" = 'Super') 
                    THEN
                      RETURN QUERY
                           SELECT distinct U.""Id"", U.""Email"" FROM public.""AspNetUsers"" U
                        	join public.""AspNetUserRoles"" UR on U.""Id"" = UR.""UserId""
                    		join public.""AspNetRoles"" R on R.""Id"" = UR.""RoleId""
                    			where R.""Name"" = 'Admin'
                    		order by U.""Email"";
                    else
                      RETURN QUERY
                           SELECT distinct U.""Id"", U.""Email"" FROM public.""AspNetUsers"" U
                        	join public.""AspNetUserRoles"" UR on U.""Id"" = UR.""UserId""
                    		join public.""AspNetRoles"" R on R.""Id"" = UR.""RoleId""
                    			where R.""Name"" = 'Admin' or R.""Name"" = 'Super'
                    		order by U.""Email"";
                    END IF;
                       
                    END
                    $func$  LANGUAGE plpgsql;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop function getAdmins");
        }
    }
}
