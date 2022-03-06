using Listening.Core;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels.File;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Listening.Core.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;

namespace Listening.Server.Services.Contracts
{
    public interface IFileService
    {
        string CutVideo(CuttingOptionsViewModel options);
        void DeleteFile(FileDescription[] fileDescription);
        string GenerateUniqueFileName(string path, string fileName);
        string GenerateUniqueFileName(string[] filesInFolder, string fileName);
        string SaveListeningAudioFile(string fileName, IFormFile file);
        string SaveListeningVideoFile(string fileName, IFormFile file);
        string SaveBlogVideoFile(string fileName, IFormFile file);
        string SaveSpecVideoFile(string fileName, IFormFile file);
        string SaveBackupFile(IFormFile file);
        string SaveOCRImage(string base64);
        void DeleteOCR(string fileName);
        void SetAutoClearResource(string fileName, FileContentType contentType, int timeToLiveMiliseconds);
        //VideoFileViewModel SaveVideoFile(string link);
        Task<VideoFileViewModel> SaveVideoFile(string link);
        string[] GetFiles(FileContentType type);
        string GetSavedListeningBackupPath(string textsDataSerialized = "");
        string GetSavedFullBackupPath(string textsDataSerialized = "");
        string GetSavedSpecFilesBackupPath();
        string GetSavedBlogFilesBackupPath();        
        Task<string> GetExtractedSerializedData(string path);
        Task RestoreBlogData(string path);
        Task RestoreSpecData(string path);
        void RemoveFilesFromTemp();
        //void SetAutoClearResource(string fileName, int timeToLiveMiliseconds);
        CaptchaDto GenerateCaptcha();
        FileStream GetSpecVideoStream(SpecVideoDescription description);
        string SaveStegFile(string fileName, FileContentType type, IFormFile file);
    }
}