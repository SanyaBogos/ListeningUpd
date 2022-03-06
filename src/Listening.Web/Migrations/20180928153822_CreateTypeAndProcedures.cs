using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class CreateTypeAndProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // TODO: implement according 
            // to this article 
            // https://stackoverflow.com/questions/29615445/dapper-bulk-insert-returning-serial-ids
            migrationBuilder.Sql(@"
                CREATE TYPE ""ResultsInsertType"" as (
                  ""UserId"" bigint,
                  ""TextId"" character varying(25),
                  ""ResultsEncodedString"" boolean[],
                  ""Mode"" character(1),
                  ""Started"" timestamp without time zone,
                  ""Finished"" timestamp without time zone,
                  ""IsStarted"" boolean,
                  ""IsCompleted"" boolean
                )");

            migrationBuilder.Sql(@"
                CREATE TYPE ""ResultsUpdateType"" as
                (
                    ""Id"" bigint,
                    ""UserId"" bigint,
                    ""TextId"" character varying(25),
                    ""ResultsEncodedString"" boolean[],
                    ""Mode"" character(1),
                    ""Started"" timestamp without time zone,
                    ""Finished"" timestamp without time zone,
                    ""IsStarted"" boolean,
                    ""IsCompleted"" boolean
                )");

            migrationBuilder.Sql(@"
                    CREATE TYPE ""ResultsInsertWithIdType"" AS
                    (
                        ""Id"" bigint,
                        ""UserId"" bigint,
                        ""TextId"" character varying(25),
                        ""ResultsEncodedString"" boolean[],
                        ""Mode"" character(1),
                        ""Started"" timestamp without time zone,
                        ""Finished"" timestamp without time zone,
                        ""IsStarted"" boolean,
                        ""IsCompleted"" boolean
                    );");

            migrationBuilder.Sql(@"
                    CREATE FUNCTION ""InsertIntoResultsTableReturningId""(
                        entries ""ResultsInsertType""[]) RETURNS SETOF BIGINT AS $$
                    
                        INSERT INTO public.""Results""(""UserId"", ""TextId"", 
                            ""ResultsEncodedString"", ""Mode"", ""Started"", ""Finished"", ""IsStarted"", ""IsCompleted"")
                            SELECT a.* FROM UNNEST(entries) a RETURNING ""Id"";
                    $$ LANGUAGE SQL;");

            migrationBuilder.Sql(@"
                    CREATE FUNCTION ""InsertIntoResultsTable""(
                        entries ""ResultsInsertType""[]) RETURNS void as $$
                    
                        INSERT INTO public.""Results""(""UserId"", ""TextId"", 
                            ""ResultsEncodedString"", ""Mode"", ""Started"", ""Finished"", ""IsStarted"", ""IsCompleted"")
                            SELECT a.* FROM UNNEST(entries) a;
                    $$ LANGUAGE SQL;");
            
            migrationBuilder.Sql(@"
                    CREATE FUNCTION ""InsertIntoResultsTableWithId""(
                        entries ""ResultsInsertWithIdType""[]) RETURNS void as $$
                    
                        INSERT INTO ""Results""(""Id"", ""UserId"", ""TextId"",
                            ""ResultsEncodedString"", ""Mode"", ""Started"", ""Finished"", ""IsStarted"", ""IsCompleted"")
                            SELECT a.*FROM UNNEST(entries) a;
                    $$ LANGUAGE SQL;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION ""InsertIntoResultsTableWithId""");
            migrationBuilder.Sql(@"DROP FUNCTION ""InsertIntoResultsTable""");
            migrationBuilder.Sql(@"DROP FUNCTION ""InsertIntoResultsTableReturningId""");

            migrationBuilder.Sql(@"DROP TYPE ""ResultsUpdateType""");
            migrationBuilder.Sql(@"DROP TYPE ""ResultsInsertWithIdType""");
            migrationBuilder.Sql(@"DROP TYPE ""ResultsInsertType""");
        }
    }
}
