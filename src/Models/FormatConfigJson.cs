namespace i18nEditor.Models
{
    public sealed class FormatConfigJson : FormatConfig
    {
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
