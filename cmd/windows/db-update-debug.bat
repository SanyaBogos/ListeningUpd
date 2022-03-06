cd ..\Listening
set DOTNET_CLI_TELEMETRY_OPTOUT=1
set ASPNETCORE_ENVIRONMENT=Development
dotnet run dropdb migratedb seeddb migratemongodb