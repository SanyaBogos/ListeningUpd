using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Steg
{
    public class PointsFromFuncParams
    {
        public FuncEnhancedDto FuncDto { get; set; }
        public System.Drawing.Size PictureSize { get; set; }
        public int Length { get; set; }
        public bool IsEdit { get; set; } = false;
    }
}
