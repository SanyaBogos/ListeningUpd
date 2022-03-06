using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.SignalR
{
    public class UserConnections
    {
        public HashSet<string> ConnectedIds { get; set; } = new HashSet<string>();
    }
}
