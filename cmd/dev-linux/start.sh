nohup mongod --dbpath ../../../data_listening/ --port 27033 &
cd ../../src/Listening.Web/bin/Development/netcoreapp3.1/
nohup dotnet Listening.Web.dll &
cd ../../src/AdditionalProjects/EasyRTC
nohup npm start &
