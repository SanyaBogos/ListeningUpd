set DOTNET_CLI_TELEMETRY_OPTOUT=1

cd ..\..\src\Listening.Web
dotnet build

cd ClientApp
call npm i

cd ..\..\AdditionalProjects\EasyRTC
call npm i

cd ..\..\..\cmd\windows