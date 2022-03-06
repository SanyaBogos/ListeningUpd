del ..\..\listening.zip
cd ..\src\Listening.Web\bin\Release\netcoreapp2.2
"C:\Program Files\7-Zip\7z.exe" a -tzip ..\..\..\..\..\listening.zip .
cd ..\..\..\..\..\
"D:\Programs\Putty\pscp.exe" -scp -P 22 -r "listening.zip" root@192.168.1.48:/home/projects/
rem "D:\Programs\Putty\pscp.exe" -scp -P 22 -r "D:\Projects\My Internal Projects\Listening2018\security.json" root@192.168.1.48:/home/projects/listening2018/