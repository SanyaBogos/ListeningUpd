cd ../../src/Listening.Web
dotnet build -c Development

cd ClientApp
npm i --force
npm run build:release

cd ../../AdditionalProjects/EasyRTC
npm i --force

cd ../../../cmd/linux
