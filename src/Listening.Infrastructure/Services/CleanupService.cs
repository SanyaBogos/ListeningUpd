using Listening.Core;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Repositories.Duplicates;
using Listening.Server.Services.Contracts;
using Listening.Server.Services.Duplicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Services
{
    public class CleanupService : ICleanupService
    {
        private readonly IFileServiceDuplicate _fileService;
        private readonly ITextMongoRepoDuplicate _textRepository;

        public CleanupService(
            IFileServiceDuplicate fileService,
            ITextMongoRepoDuplicate textRepository)
        {
            _fileService = fileService;
            _textRepository = textRepository;
        }

        public async Task RemoveFilesForUnexistedTexts(FileContentType contentType)
        {
            IQueryable<string> fileNames;

            if (FileContentType.Audio == contentType)
                fileNames = _textRepository.Get()
                    .Where(x => !string.IsNullOrEmpty(x.AudioName))
                    .Select(x => x.AudioName);
            else
                fileNames = _textRepository.Get()
                    .Where(x => !string.IsNullOrEmpty(x.VideoName))
                    .Select(x => x.VideoName);

            await Task.Run(() => Remove(fileNames, contentType));
        }

        private void Remove(IEnumerable<string> namesFromDb, FileContentType type)
        {
            var filenames = _fileService.GetFiles(type)
                .Select(x => x.Split(new string[] { @"\", @"/" },
                        StringSplitOptions.RemoveEmptyEntries)
                .Last());
            var toDelete = filenames.Except(namesFromDb).ToArray();
            var descriptions = toDelete.Select(x => new FileDescription(x, type));

            _fileService.DeleteFile(descriptions.ToArray());
        }
    }
}
