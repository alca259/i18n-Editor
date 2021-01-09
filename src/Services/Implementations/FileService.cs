using i18nEditor.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Dictionary<string, string>> GetJsonData(FileKeyPathDto fileData)
        {
            if (!File.Exists(fileData.FilePath)) return new Dictionary<string, string>();

            var fileString = await File.ReadAllTextAsync(fileData.FilePath, Encoding.Latin1);
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileString);
            return data.OrderBy(o => o.Key).ToDictionary(k => k.Key, v => v.Value);
        }

        public async Task SaveJsonData(FileKeyPathDto fileData, IDictionary<string, string> data)
        {
            var saveData = data.OrderBy(o => o.Key).ToList();
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
            };

            var jsonData = CustomJsonSerialize(data);
            await File.WriteAllTextAsync(fileData.FilePath, jsonData, Encoding.Latin1);
        }

        public async Task NewFile(string name, string language)
        {
            var fileName = $"{name}.{language}.json";
            var filePath = Path.Combine(Options.FolderPath, fileName);
            if (File.Exists(filePath)) return;

            await File.WriteAllTextAsync(filePath, "{}", Encoding.Latin1);
        }

        private string CustomJsonSerialize<T>(T value, char indentChar = ' ', int indentation = 4)
        {
            var sb = new StringBuilder();
            var jsonSerializer = JsonSerializer.CreateDefault();

            using var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using var jw = new JsonTextWriter(sw)
            {
                Formatting = Formatting.Indented,
                IndentChar = indentChar,
                Indentation = indentation
            };

            jsonSerializer.Serialize(jw, value, typeof(T));

            return sw.ToString();
        }

    }
}
