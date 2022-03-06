export DOTNET_CLI_TELEMETRY_OPTOUT=1
cd ../../src/Listening.Web
sed -i -e 's/localhost:8443/listening.pp.ua:8443/g' ClientApp/src/assets/easyrtc/easyrtc.js
dotnet publish -c Release
#cp -r bin/Release/netcoreapp2.1/publish/ClientApp/dist/listening/. bin/Release/netcoreapp2.1/wwwroot/
cp -r bin/Release/netcoreapp3.1/publish/ClientApp/dist/listening/. bin/Release/netcoreapp3.1/ClientApp/dist/listening
cp -r bin/Release/netcoreapp3.1/publish/wwwroot/audio/. bin/Release/netcoreapp3.1/wwwroot/audio
cp -r bin/Release/netcoreapp3.1/publish/wwwroot/video/. bin/Release/netcoreapp3.1/wwwroot/video

rm -rf bin/Release/netcoreapp3.1/publish/ClientApp/dist/listening
rm -rf bin/Release/netcoreapp3.1/publish/wwwroot/audio
rm -rf bin/Release/netcoreapp3.1/publish/wwwroot/video