kill $(ps aux | grep 'Listening.Web.dll' | awk '{print $2}')
