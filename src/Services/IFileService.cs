using i18nEditor.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace i18nEditor.Services
{
    public interface IFileService
    {
        IEnumerable<FileKeyPathDto> GetJsonFiles();
        Task<Dictionary<string, string>> GetJsonData(FileKeyPathDto fileData);
    }
}
