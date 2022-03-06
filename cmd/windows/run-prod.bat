cd ..\..\src\Listening.Web
set DOTNET_CLI_TELEMETRY_OPTOUT=1
set ASPNETCORE_ENVIRONMENT=Production
dotnet run --launch-profile "Production"