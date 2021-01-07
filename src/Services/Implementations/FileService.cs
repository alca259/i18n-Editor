using i18nEditor.DTOs;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace i18nEditor.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IOptionsMonitor<EditorConfigDto> _options;
        private EditorConfigDto Options => _options.CurrentValue;

        public FileService(IOptionsMonitor<EditorConfigDto> options)
        {
            _options = options;
        }

        public IEnumerable<FileKeyPathDto> GetJsonFiles()
        {
            var dir = new DirectoryInfo(Options.FolderPath);
            if (!dir.Exists) yield break;

            var files = dir.GetFiles("*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var displayName = Path.GetFileNameWithoutExtension(file.FullName);
                var language = "es-ES";

                int lastIndexOfDot = displayName.LastIndexOf('.');
                if (lastIndexOfDot != -1)
                {
                    var parts = displayName.Split('.');
                    displayName = parts[0];
                    language = parts[1];
                }

                var dtoItem = new FileKeyPathDto
                {
                    Key = Path.GetFileName(file.FullName),
                    FilePath = file.FullName,
                    DisplayName = displayName,
                    Language = language
                };

                yield return dtoItem;
            }
        }
    }
}
