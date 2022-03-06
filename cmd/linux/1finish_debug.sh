nohup mongo --eval "db.getSiblingDB('admin').shutdownServer()" --port 27033 &
nohup kill $(ps aux | grep 'Listening.Web.dll' | awk '{print $2}')

nohup kill $(ps aux | grep 'npm' | awk '{print $2}')
nohup kill $(ps aux | grep 'ng serve' | awk '{print $2}')
nohup kill $(ps aux | grep 'node-easyrtc' | awk '{print $2}')
