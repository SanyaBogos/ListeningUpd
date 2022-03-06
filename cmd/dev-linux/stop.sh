nohup mongo --eval "db.getSiblingDB('admin').shutdownServer()" --port 27033 &
kill $(ps aux | grep 'Listening.Web.dll' | awk '{print $2}')
kill $(ps aux | grep 'node server_ssl.js' | awk '{print $2}')
