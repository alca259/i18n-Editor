using i18nEditor.DTOs;
using System.Collections.Generic;

namespace i18nEditor.Services
{
    public interface IFileService
    {
        IEnumerable<FileKeyPathDto> GetJsonFiles();
    }
}
