#!/bin/bash

export DOTNET_CLI_TELEMETRY_OPTOUT=1
cd ../../src/Listening.Web
rm -rf lstng.zip
rm -rf bin/Release
sed -i -e 's/localhost:8443/listening.pp.ua:8443/g' ClientApp/src/assets/easyrtc/easyrtc.js
dotnet publish -c Release

cp -r bin/Release/netcoreapp3.1/publish/ClientApp/dist/listening/. bin/Release/netcoreapp3.1/ClientApp/dist/listening

rm bin/Release/netcoreapp3.1/publish/wwwroot/debianResults/*
rm bin/Release/netcoreapp3.1/publish/wwwroot/audio/*
rm bin/Release/netcoreapp3.1/publish/wwwroot/video/*
#rm -rf bin/Release/netcoreapp3.1/wwwroot

mv -r bin/Release/netcoreapp3.1/publish/wwwroot bin/Release/netcoreapp3.1/

rm -rf bin/Release/netcoreapp3.1/publish/
#rm -rf bin/Release/netcoreapp3.1/wwwroot
sed -i -e 's/listening.pp.ua:8443/localhost:8443/g' ClientApp/src/assets/easyrtc/easyrtc.js

cp -r ../AdditionalProjects/EasyRTC bin/Release/netcoreapp3.1
cp -r ../../cmd/deploy-cmd-linux bin/Release/netcoreapp3.1/cmd
#cp ../../security.json bin/Release/netcoreapp3.1
cp ../../../../security.json bin/Release/netcoreapp3.1

cd bin/Release/
mv netcoreapp3.1 lstng
zip -r lstng.zip lstng
