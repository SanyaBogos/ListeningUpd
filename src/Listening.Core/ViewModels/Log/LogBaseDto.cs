using System;

namespace Listening.Core.ViewModels.Log
{
    public class LogBaseDto
    {
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
    }
}
