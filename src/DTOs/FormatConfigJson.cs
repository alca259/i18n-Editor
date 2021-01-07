namespace i18nEditor.DTOs
{
    public sealed class FormatConfigJson
    {
        /// <summary>
        /// Folder name where are stored
        /// </summary>
        public string FolderName { get; set; } = "i18n";

        /// <summary>
        /// 0 = global language (es, en, fr, it, etc...)
        /// 1 = specific culture (es-ES, es-MX, en-US, etc...)
        /// </summary>
        public string NameFormat { get; set; } = "{0}-{1}";

        /// <summary>
        /// Indented save
        /// </summary>
        public bool Indent { get; set; } = true;

        /// <summary>
        /// Only apply if no use tabs
        /// </summary>
        public int IndentSize { get; set; } = 4;

        /// <summary>
        /// If indent with tabs, IndentSize is ignored
        /// </summary>
        public bool IndentWithTabs { get; set; } = false;
    }
}
