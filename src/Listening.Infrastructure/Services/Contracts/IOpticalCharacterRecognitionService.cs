using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IOpticalCharacterRecognitionService
    {
        string GetRecognitionResult(string base64, string language = "eng");
    }
}
