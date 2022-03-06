cd ..\..\src\Listening.Web
rmdir /S /Q bin obj
dotnet publish -c Release
cd bin\Release\netcoreapp2.2
rem rmdir /S /Q ClientApp
 xcopy ..\..\..\ClientApp\dist\listening wwwroot /s /e