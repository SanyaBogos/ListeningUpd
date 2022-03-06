nohup mongod --dbpath ../../data_listening/ --port 27033 &
cd ../
nohup dotnet Listening.Web.dll &
cd EasyRTC
nohup npm start &
