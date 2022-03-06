using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.DebianFAI
{
    public enum PartitionType 
    {
        boot,
        swap,
        root,
        tmp,
        var,
        opt,
        varLog,
        varTmp,
        home,
        rootPart
    }
}