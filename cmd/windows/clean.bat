cd ..\..\src\AdditionalProjects\libvideo
rmdir /S /Q bin obj

cd ..\YoutubeExtractor
rmdir /S /Q bin obj

cd ..\YoutubeExtractorTests
rmdir /S /Q bin obj

cd ..\..\Listening.Core
rmdir /S /Q bin obj

cd ..\Listening.Infrastructure
rmdir /S /Q bin obj

cd ..\Listening.Web
rmdir /S /Q bin obj

cd ClientApp
rmdir /S /Q node_modules

del package-lock.json

cd ..\..\..\tests\Infrastructure
rmdir /S /Q bin obj

cd ..\Integration
rmdir /S /Q bin obj

cd ..\Unit
rmdir /S /Q bin obj

cd ..\..\src\AdditionalProjects\EasyRTC
rmdir /S /Q node_modules
cd ..\..\..\cmd\windows

call npm cache clear --force