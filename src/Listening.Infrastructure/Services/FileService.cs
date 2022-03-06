using Listening.Core;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Utilities;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Security;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;
using Listening.Core.Entities.Specialized.ServiceModels;
using Listening.Infrastructure.Services;
using Listening.Core.ViewModels;

namespace Listening.Server.Services
{
    public class FileService : BaseFileService, IFileService
    {
        private const string Zip = "zip";
        private const string Audio = "audio";
        private const string Video = "video";
        private const string Spec = "spec";
        private const string Blog = "blog";
        private const string Texts = "texts.json";

        private readonly ILogger<FileService> _logger;
        private readonly Dictionary<char, string> _specTypes;
        private readonly Dictionary<char, string> _specAuthors;
        private readonly Dictionary<char, string> _specInnerTypes;
        private readonly Dictionary<FileContentType, string> _typePathDictionary;
        private readonly string _listeningAudioPath;
        private readonly string _listeningVideoPath;
        private readonly string _blogVideoPath;
        private readonly string _specVideoPath;
        private readonly string _ocrImagePath;
        private readonly string _ocrResultPath;
        private readonly string _captchaPath;
        // private readonly string _tempPath;
        private readonly string _archivePath;
        private readonly string _stegPicturePath;
        private readonly string _stegAudioPath;
        private readonly string _stegVideoPath;

        //private readonly string _relativeVideoPath;
        private readonly string _externalSoftPath;
        private readonly int _videoTTL;
        private readonly int _captchaTTL;
        private readonly int _stegTTL;

        //private readonly int _captchaPwd;

        public FileService(
            IConfiguration configuration,
            ILogger<FileService> logger,
            IWebHostEnvironment env) : base(configuration)
        {
            _listeningAudioPath = $"{env.WebRootPath}{configuration["Data:FileStorage:AudioPath"]}";
            _listeningVideoPath = $"{env.WebRootPath}{configuration["Data:FileStorage:VideoPath"]}";
            _blogVideoPath = $"{env.WebRootPath}{configuration["Data:FileStorage:Blog:Video"]}";
            _specVideoPath = $"{env.WebRootPath}{configuration["Data:FileStorage:Spec:Video"]}";
            _ocrImagePath = $"{env.WebRootPath}{configuration["Data:FileStorage:PictureForRecognition"]}";
            _ocrResultPath = $"{env.WebRootPath}{configuration["Data:FileStorage:RecognitionResults"]}";
            _captchaPath = $"{env.WebRootPath}{configuration["Data:FileStorage:CaptchaPath"]}";
            // _tempPath = $"{Directory.GetCurrentDirectory()}/bin{configuration["Data:FileStorage:Temp"]}";
            _archivePath = $"{env.WebRootPath}{configuration["Data:FileStorage:Archive"]}";
            _stegPicturePath = $"{env.WebRootPath}{configuration["Data:FileStorage:Steg:Picture"]}";
            _stegAudioPath = $"{env.WebRootPath}{configuration["Data:FileStorage:Steg:Audio"]}";
            _stegVideoPath = $"{env.WebRootPath}{configuration["Data:FileStorage:Steg:Video"]}";
            _externalSoftPath = $"{env.WebRootPath}/{configuration["ExternalSoft:Path"]}";
            _videoTTL = Convert.ToInt32(configuration["Data:TimeToLive"]);
            _captchaTTL = Convert.ToInt32(configuration["Data:CaptchaTimeToLive"]);
            _stegTTL = Convert.ToInt32(configuration["Data:StegTimeToLive"]);
            _logger = logger;

            _specTypes = new Dictionary<char, string>() {
                { 'e', "ecobuild" },
                { 'w', "woodland" },
            };

            _specAuthors = new Dictionary<char, string>() {
                { 'k', "kovalenko" },
                { 's', "shirokov" },
            };

            _specInnerTypes = new Dictionary<char, string>() {
                { 'b', "base" },
                { 'a', "additional" },
            };

            _typePathDictionary = new Dictionary<FileContentType, string>()
            {
                { FileContentType.Audio, _listeningAudioPath },
                { FileContentType.Video, _listeningVideoPath },
                { FileContentType.Captcha, _captchaPath },
                { FileContentType.StegPict, _stegPicturePath },
                { FileContentType.StegAud, _stegAudioPath},
                { FileContentType.StegVid, _stegVideoPath },
                { FileContentType.BlogVid, _blogVideoPath },
            };

            CreateFoldersIfNotExist();
        }

        public CaptchaDto GenerateCaptcha()
        {
            var bmp = ImageCaptcha.Generate(440, 180, out string validate);

            string fullPath;
            string fileName;

            do
            {
                fileName = $"{Guid.NewGuid()}.png";
                fullPath = $"{_captchaPath}{fileName}";
            } while (File.Exists(fullPath));


            using (var fs = File.Create(fullPath))
            {
                bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                SetAutoClearResource(fileName, FileContentType.Captcha, _captchaTTL);
                var result = new CaptchaDto
                {
                    Name = fileName,
                    Hash = CaptchaHelper.GenerateHash(validate, _captchaTTL)
                };

                return result;
            }
        }

        public string SaveListeningAudioFile(string fileName, IFormFile file)
        {
            return SaveFile(file, _listeningAudioPath, fileName);
        }

        public string SaveListeningVideoFile(string fileName, IFormFile file)
        {
            return SaveFile(file, _listeningVideoPath, fileName);
        }


        public string SaveBlogVideoFile(string fileName, IFormFile file)
        {
            return SaveFile(file, _blogVideoPath, fileName);
        }

        public string SaveSpecVideoFile(string fileName, IFormFile file)
        {
            return SaveFile(file, _specVideoPath, fileName);
        }

        public string SaveStegFile(string fileName, FileContentType type, IFormFile file)
        {
            var resultFile = SaveFile(file, _typePathDictionary[type], fileName);
            SetAutoClearResource(resultFile, type, _stegTTL);
            return resultFile;
        }

        public string SaveBackupFile(IFormFile file)
        {
            var fileName = SaveFile(file, _tempPath);
            var fullPath = $"{_tempPath}{fileName}";
            return fullPath;
        }

        public async Task<VideoFileViewModel> SaveVideoFile(string link)
        {
            var client = new YoutubeClient();
            var linkString = link.Split("=").Last();
            var vid = await client.GetVideoAsync(linkString);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(linkString);

            // Select one of the highest quality & highest framerate MP4 video stream
            var streamInfo = streamInfoSet.Video
               .Where(s => s.Container == Container.Mp4)
               .OrderByDescending(s => s.VideoQuality)
               .ThenByDescending(s => s.Framerate)
               .First();
            var ext = streamInfo.Container.GetFileExtension();
            var preparedName = vid.Title.RemoveSpecialCharacters();

            if (string.IsNullOrEmpty(preparedName))
                preparedName = "newFile";

            var fileName = GenerateUniqueFileName(_listeningVideoPath, preparedName);
            var fullName = $"{fileName}.{ext}";
            await client.DownloadMediaStreamAsync(streamInfo, $"{_listeningVideoPath}{fullName}");

            SetAutoClearResource(fullName, FileContentType.Video, _videoTTL);
            return new VideoFileViewModel(fullName, _videoTTL / 1000 - 2);
        }

        public FileStream GetSpecVideoStream(SpecVideoDescription description)
        {
            var path = $"{_archivePath}/{_specTypes[description.Type]}/{_specAuthors[description.Author]}/{_specInnerTypes[description.InnerType]}/{description.Name}";
            var stream = new FileStream(path, FileMode.Open);
            return stream;
        }

        public string SaveOCRImage(string base64)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            var splitted = base64.Split(";base64,");
            var type = splitted.First();
            type = type.Substring(type.IndexOf('/') + 1);
            var clearBase64 = splitted.Last();
            var fullFilePath = $"{_ocrImagePath}{fileName}.{type}";

            //using (var file = File.Create(fullFilePath))
            File.WriteAllBytes(fullFilePath, Convert.FromBase64String(clearBase64));

            return $"{fileName}.{type}";
        }

        public void DeleteOCR(string fileName)
        {
            var fullPath = $"{_ocrImagePath}{fileName}";
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public string CutVideo(CuttingOptionsViewModel options)
        {
            var newName = GenerateUniqueFileName(_listeningVideoPath, options.FileName);
            var fromFile = $"{_listeningVideoPath}{options.FileName}";
            var toFile = $"{_listeningVideoPath}{newName}";
            var fromTime = options.From + 4;
            var toTime = options.To - options.From - 3;

            var pathToProcess = GetVideoCutterProcessName();
            var command = $@"-y -ss {fromTime} -i ""{fromFile}"" -c copy -t {toTime} -avoid_negative_ts make_zero -fflags +genpts ""{toFile}""";

            using (var process = Process.Start(pathToProcess, command))
                process.WaitForExit();

            return newName;
        }

        public string GetSavedListeningBackupPath(string textsDataSerialized)
        {
            var dateFormat = DateTime.Now.ToString().Replace("/", "_")
                .Replace(":", "-");
            var path = $"{_tempPath}lsn-backup-{dateFormat}";
            var result = $"{path}.{Zip}";

            Directory.CreateDirectory(path);
            File.AppendAllText($"{path}/{Texts}", textsDataSerialized);

            SaveMediaFiles(_listeningAudioPath, $"{path}/{Audio}");
            SaveMediaFiles(_listeningVideoPath, $"{path}/{Video}");

            ZipFile.CreateFromDirectory(path, result);

            return result;
        }

        public string GetSavedFullBackupPath(string textsDataSerialized)
        {
            var dateFormat = DateTime.Now.ToString().Replace("/", "_")
                .Replace(":", "-");
            var path = $"{_tempPath}full-backup-{dateFormat}";
            var result = $"{path}.{Zip}";

            Directory.CreateDirectory(path);
            File.AppendAllText($"{path}/{Texts}", textsDataSerialized);

            SaveMediaFiles(_listeningAudioPath, $"{path}/{Audio}");
            SaveMediaFiles(_listeningVideoPath, $"{path}/{Video}");
            SaveMediaFiles(_specVideoPath, $"{path}/{Spec}");
            SaveMediaFiles(_blogVideoPath, $"{path}/{Blog}");

            ZipFile.CreateFromDirectory(path, result);

            return result;
        }

        public string GetSavedSpecFilesBackupPath()
        {
            var result = GetSavedFilesBackupPath("spec-files-backup", _specVideoPath, Spec);
            return result;
        }

        public string GetSavedBlogFilesBackupPath()
        {
            var result = GetSavedFilesBackupPath("blog-files-backup", _blogVideoPath, Blog);
            return result;
        }

        public async Task<string> GetExtractedSerializedData(string path)
        {
            if (path.GetFileTypeFromPath() != Zip)
                throw new FileUploadException("Incorrect type of backup file");

            var tempPath = $"{_tempPath}{path.GetPureFileName()}";
            ZipFile.ExtractToDirectory($"{path}", tempPath, true);

            SaveMediaFiles($"{tempPath}/{Audio}", _listeningAudioPath);
            SaveMediaFiles($"{tempPath}/{Video}", _listeningVideoPath);

            var texts = await File.ReadAllTextAsync($"{tempPath}/{Texts}");
            return texts;
        }

        public async Task RestoreSpecData(string path)
        {
            if (path.GetFileTypeFromPath() != Zip)
                throw new FileUploadException("Incorrect type of backup file");

            var tempPath = $"{_tempPath}{path.GetPureFileName()}";
            ZipFile.ExtractToDirectory($"{path}", tempPath, true);

            SaveMediaFiles($"{tempPath}/{Spec}", _specVideoPath);
        }

        public async Task RestoreBlogData(string path)
        {
            if (path.GetFileTypeFromPath() != Zip)
                throw new FileUploadException("Incorrect type of backup file");

            var tempPath = $"{_tempPath}{path.GetPureFileName()}";
            ZipFile.ExtractToDirectory($"{path}", tempPath, true);

            SaveMediaFiles($"{tempPath}/{Blog}", _blogVideoPath);
        }

        public void RemoveFilesFromTemp()
        {
            var dirInfo = new DirectoryInfo(_tempPath);

            foreach (FileInfo file in dirInfo.GetFiles())
                file.Delete();

            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                dir.Delete(true);
        }

        public string[] GetFiles(FileContentType type)
        {
            var path = type == FileContentType.Audio ? _listeningAudioPath : _listeningVideoPath;
            return Directory.GetFiles(path);
        }

        public void DeleteFile(FileDescription[] fileDescriptions)
        {
            var errorResults = new List<string>();

            foreach (var fileDescription in fileDescriptions)
            {
                if (string.IsNullOrEmpty(fileDescription.Name))
                    continue;

                var path = _typePathDictionary[fileDescription.Type];
                var fullPath = $"{path}/{fileDescription.Name}";

                if (File.Exists(fullPath))
                    File.Delete(fullPath);
                else
                    errorResults.Add(
                        $"Can`t remove file with name {fileDescription.Name} due to his inexistence.");
            }

            if (errorResults.Any())
                throw new FileUploadException(string.Join('\n', errorResults));
        }

        public string GenerateUniqueFileName(string path, string fileName)
        {
            var filesInFolder = Directory.GetFiles(path);
            filesInFolder = filesInFolder
                .Select(x => x.Split('/').Last()).ToArray();

            if (filesInFolder.Contains(fileName))
                fileName = GenerateUniqueFileName(filesInFolder, fileName);

            return fileName;
        }

        public string GenerateUniqueFileName(string[] filesInFolder, string fileName)
        {
            var splitter = fileName.LastIndexOf('.');
            var type = fileName.Substring(splitter, fileName.Length - splitter);
            var name = fileName.Substring(0, splitter);
            string newFileName;
            var index = 0;
            var additionalSymbolsCount = 1;

            // this magic checks possible file name, tries 5 times to generate random symbols
            // and increase random symbols count if no luck
            do
            {
                newFileName = $"{name}{Helpers.GenerateRandomSymbols(additionalSymbolsCount)}{type}";
                index++;
                if (index % 5 == 0)
                    additionalSymbolsCount++;
            }
            while (filesInFolder.Contains(newFileName));

            return newFileName;
        }

        public void SetAutoClearResource(string fileName, FileContentType contentType, int timeToLiveMiliseconds)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(timeToLiveMiliseconds);
                try
                {
                    var file = new FileDescription(fileName, contentType);
                    DeleteFile(new FileDescription[] { file });
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        $"Issue with video resource cleaning. Internal error: '{ex.Message}' Stack trace: {ex.StackTrace}");
                }
            });
        }

        private void SaveMediaFiles(string from, string to /*, string folderName*/)
        {
            //var destination = $"{to}/{folderName}";
            var destination = $"{to}";

            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            var files = Directory.GetFiles(from, "*.*",
                            SearchOption.AllDirectories);

            foreach (string file in files)
                File.Copy(file,
                    $"{destination}/{file.GetFileNameFromPath()}", true);
        }

        private string SaveFile(IFormFile file, string toPath, string fileName = "")
        {
            if (file == null)
                throw new FileUploadException("File should not be empty!");

            if (file.Length > 0)
            {
                if (string.IsNullOrEmpty(fileName))
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                        .FileName.Value.Trim('"').Replace("\\", "").Replace("/", "");

                fileName = GenerateUniqueFileName(toPath, fileName);

                using (var fs = new FileStream(
                    Path.Combine(toPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            return fileName;
        }



        private string GetSavedFilesBackupPath(string fileName, string pathFrom, string type)
        {
            var dateFormat = DateTime.Now.ToString().Replace("/", "_")
                .Replace(":", "-");
            var path = $"{_tempPath}{fileName}-{dateFormat}";
            var result = $"{path}.{Zip}";

            Directory.CreateDirectory(path);

            SaveMediaFiles(pathFrom, $"{path}/{type}");

            ZipFile.CreateFromDirectory(path, result);

            return result;
        }

        private void CreateFoldersIfNotExist()
        {
            if (!Directory.Exists(_listeningAudioPath))
                Directory.CreateDirectory(_listeningAudioPath);

            if (!Directory.Exists(_listeningVideoPath))
                Directory.CreateDirectory(_listeningVideoPath);

            if (!Directory.Exists(_captchaPath))
                Directory.CreateDirectory(_captchaPath);

            if (!Directory.Exists(_ocrImagePath))
                Directory.CreateDirectory(_ocrImagePath);

            if (!Directory.Exists(_ocrResultPath))
                Directory.CreateDirectory(_ocrResultPath);

            if (!Directory.Exists(_stegPicturePath))
                Directory.CreateDirectory(_stegPicturePath);

            if (!Directory.Exists(_stegAudioPath))
                Directory.CreateDirectory(_stegAudioPath);

            if (!Directory.Exists(_stegVideoPath))
                Directory.CreateDirectory(_stegVideoPath);

            if (!Directory.Exists(_blogVideoPath))
                Directory.CreateDirectory(_blogVideoPath);

            if (!Directory.Exists(_specVideoPath))
                Directory.CreateDirectory(_specVideoPath);

            // if (!Directory.Exists(_tempPath))
            //     Directory.CreateDirectory(_tempPath);
        }

        private string GetVideoCutterProcessName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return $"{_externalSoftPath}/ffmpeg.exe";
            else
                return "ffmpeg";
        }
    }
}
