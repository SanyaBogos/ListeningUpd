# usually restart is necessary after run application
service nginx restart

# open ports are also necessary after reboot
iptables -A INPUT -p tcp --dport 443 --jump ACCEPT
iptables -A INPUT -p tcp --dport 80 --jump ACCEPT
iptables-save