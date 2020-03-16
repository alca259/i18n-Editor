namespace i18nEditor.Models
{
    public abstract class FormatConfig
    {
        /// <summary>
        /// Default format to save
        /// </summary>
        public SaveFormat Format { get; set; } = SaveFormat.Json;

        /// <summary>
        /// Folder name where are stored
        /// </summary>
        public string FolderName { get; set; } = "i18n";

        /// <summary>
        /// 0 = global language (es, en, fr, it, etc...)
        /// 1 = specific culture (es-ES, es-MX, es-CA, etc...)
        /// </summary>
        public string NameFormat { get; set; } = "{0}-{1}";
    }
}
