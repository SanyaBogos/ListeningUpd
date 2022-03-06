using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Listening.Server.Services
{
    public class OpticalCharacterRecognitionService : IOpticalCharacterRecognitionService
    {
        private readonly string _pictureToRecognitionPath;
        private readonly string _resultOfRecognitionPath;
        private readonly IFileService _fileService;

        public OpticalCharacterRecognitionService(
            IConfiguration configuration,
            IFileService fileService,
            IWebHostEnvironment env)
        {
            _pictureToRecognitionPath = $"{env.WebRootPath}{configuration["Data:FileStorage:PictureForRecognition"]}";
            _resultOfRecognitionPath = $"{env.WebRootPath}{configuration["Data:FileStorage:RecognitionResults"]}";
            _fileService = fileService;
            Init();
        }

        public string GetRecognitionResult(string base64, string language = "eng")
        {
            var inputFileName = _fileService.SaveOCRImage(base64);
            var inputName = $"{_pictureToRecognitionPath}{inputFileName}";
            var resultName = $"{_resultOfRecognitionPath}{inputFileName.Split(".").First()}";
            var command = $@"tesseract ""{inputName}"" ""{resultName}"" -l {language}";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                command.CommandPrompt();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                command.Bash();

            var resultFile = $"{resultName}.txt";
            _fileService.DeleteOCR(inputFileName);

            if (!File.Exists(resultFile))
                throw new OCRException("Error during recognition");

            var result = File.ReadAllText(resultFile);
            File.Delete(resultFile);
            return result;
        }

        private void Init()
        {
            if (!Directory.Exists(_pictureToRecognitionPath))
                Directory.CreateDirectory(_pictureToRecognitionPath);

            if (!Directory.Exists(_resultOfRecognitionPath))
                Directory.CreateDirectory(_resultOfRecognitionPath);
        }
    }
}
