using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Extensions
{
    internal static class FileExtensions
    {
        internal static void CreateStandartFolderIfNotExists(this DatabaseFixture fixture)
        {
            if (!Directory.Exists(fixture.AudioPath))
                Directory.CreateDirectory(fixture.AudioPath);

            if (!Directory.Exists(fixture.VideoPath))
                Directory.CreateDirectory(fixture.VideoPath);

            if (!Directory.Exists(fixture.OcrImagePath))
                Directory.CreateDirectory(fixture.OcrImagePath);

            if (!Directory.Exists(fixture.OcrResultPath))
                Directory.CreateDirectory(fixture.OcrResultPath);
        }

        internal static void PrepareExistedMediaFiles(this DatabaseFixture fixture)
        {
            foreach (var audioText in fixture.AudioCheckTexts)
                File.Copy($@"{fixture.ResourcesPath}/{DatabaseFixture.AudioNameBase}.{DatabaseFixture.AudioType}",
                    $@"{fixture.AudioPath}{audioText.AudioName}",
                    true);

            foreach (var videoText in fixture.VideoCheckTexts)
                File.Copy($@"{fixture.ResourcesPath}/{DatabaseFixture.VideoNameBase}.{DatabaseFixture.VideoType}",
                    $@"{fixture.VideoPath}{videoText.VideoName}",
                    true);
        }
        internal static void PrepareUnexistedMediaFiles(this DatabaseFixture fixture)
        {
            var audioName = $"{DatabaseFixture.UnexistedAudioNameBase}.{DatabaseFixture.AudioType}";
            var videoName = $"{DatabaseFixture.UnexistedVideoNameBase}.{DatabaseFixture.VideoType}";

            File.Copy($@"{fixture.ResourcesPath}/{audioName}", $@"{fixture.AudioPath}{audioName}", true);
            File.Copy($@"{fixture.ResourcesPath}/{videoName}", $@"{fixture.VideoPath}{videoName}", true);
        }

        internal static string PrepareOCRFile(this DatabaseFixture fixture)
        {
            var newFileName = $"{DatabaseFixture.TestLabel}{DatabaseFixture.OcrFileName}";
            File.Copy($@"{fixture.ResourcesPath}/{DatabaseFixture.OcrFileName}",
                    $@"{fixture.OcrImagePath}/{newFileName}",
                    true);
            return newFileName;
        }

        internal static void CleanFiles(this DatabaseFixture fixture)
        {
            var existedAudioFiles = Directory.GetFiles(fixture.AudioPath, $"{DatabaseFixture.AudioNameBase}*.*");
            var unexistedAudioFiles = Directory.GetFiles(fixture.AudioPath, $"{DatabaseFixture.UnexistedAudioNameBase}*.*");
            var existedVideoFiles = Directory.GetFiles(fixture.VideoPath, $"{DatabaseFixture.VideoNameBase}*.*");
            var unexistedVideoFiles = Directory.GetFiles(fixture.VideoPath, $"{DatabaseFixture.UnexistedVideoNameBase}*.*");
            var ocrImageFiles = Directory.GetFiles(fixture.OcrImagePath, $"{DatabaseFixture.TestLabel}*.*");
            var ocrResultFiles = Directory.GetFiles(fixture.OcrResultPath, $"{DatabaseFixture.TestLabel}*.*");
            var all = existedAudioFiles.Concat(unexistedAudioFiles).Concat(existedVideoFiles).Concat(unexistedVideoFiles)
                .Concat(ocrImageFiles).Concat(ocrResultFiles);

            foreach (var file in all)
                File.Delete(file);
        }
    }
}
