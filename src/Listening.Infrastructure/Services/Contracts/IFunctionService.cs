using Listening.Core.ViewModels.Steg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IFunctionService
    {
        PointsFromFuncResultDto GetPointsFromFunction(PointsFromFuncParams pffParams);
    }
}
