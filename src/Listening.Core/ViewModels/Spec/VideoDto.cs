using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Spec
{
    public class VideoDto
    {
        public long Id { get; set; }
        //public int FolderId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Ext { get; set; }
        public int Repeat { get; set; } = 0;
    }
}
