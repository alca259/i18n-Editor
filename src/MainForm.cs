using i18nEditor.DTOs;
using i18nEditor.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class MainForm : Form
    {
        private static Regex ParametersRegex = new Regex(@"\{[0-9]\}", RegexOptions.Multiline);

        private int _lastRowIndex = -1;
        private bool _loopFlag = false;
        private readonly IFileService _fileService;
        private readonly ILogger<MainForm> _logger;

        private IEnumerable<FileKeyPathDto> FilesFound { get; set; } = new List<FileKeyPathDto>();
        private FileKeyPathDto FileSet { get; set; }

        private InputBox SearchInputBox { get; }
        private int _lastSearchedIndex;

        public MainForm(IFileService fileService, ILogger<MainForm> logger)
        {
            InitializeComponent();
            _fileService = fileService;
            _logger = logger;

            currentFile.AutoCompleteMode = AutoCompleteMode.None;
            currentLanguage.AutoCompleteMode = AutoCompleteMode.None;

            currentFile.SelectedIndexChanged += CurrentFile_SelectedIndexChanged;
            currentLanguage.SelectedIndexChanged += CurrentLanguage_SelectedIndexChanged;

            btnReloadFromDisk.Click += BtnReloadFromDisk_Click;
            btnNewFile.Click += BtnNewFile_Click;
            btnNewKey.Click += BtnNewKey_Click;
            btnSaveToDisk.Click += BtnSaveToDisk_Click;

            dataGridKeys.SelectionChanged += DataGridKeys_SelectionChanged;
            contentBox.Leave += ContentBox_Leave;
            contentBox.KeyUp += ContentBox_KeyUp;
            contentBox.TextChanged += ContentBox_TextChanged;

            dataGridKeys.CellMouseDown += DataGridKeys_CellMouseDown;

            dataGridKeys.KeyDown += Search_KeyDown;

            SearchInputBox = new InputBox();

            RefreshFiles();
        }

        #region Search events
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                var result = SearchInputBox.ShowDialog();
                if (result != DialogResult.OK) return;
                if (string.IsNullOrEmpty(SearchInputBox.SearchedText)) return;

                int searchIndex = SearchInDatagrid(SearchInputBox.SearchedText);
                if (searchIndex < 0) return;
                dataGridKeys.CurrentCell = dataGridKeys.Rows[searchIndex].Cells[0];
                _lastSearchedIndex = searchIndex;
            }

            // Next search
            if (e.KeyCode == Keys.F3)
            {
                if (string.IsNullOrEmpty(SearchInputBox.SearchedText)) return;
                int searchIndex = SearchInDatagrid(SearchInputBox.SearchedText);
                if (searchIndex < 0) return;
                dataGridKeys.CurrentCell = dataGridKeys.Rows[searchIndex].Cells[0];
                _lastSearchedIndex = searchIndex;
            }
        }

        private int SearchInDatagrid(string text)
        {
            foreach (DataGridViewRow row in dataGridKeys.Rows)
            {
                var value = row.Cells[2].Value?.ToString();
                if (value != null && value.ToLower().Contains(text.ToLower()))
                {
                    if (_lastSearchedIndex == row.Index) continue;
                    return row.Index;
                }
            }

            return -1;
        }
        #endregion

        #region Copy key cell event
        private void DataGridKeys_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex <= -1) return;
            if (e.Button != MouseButtons.Right)
            {
                if (e.Button == MouseButtons.Left) _lastSearchedIndex = e.RowIndex;
                return;
            }
            var key = dataGridKeys.Rows[e.RowIndex].Cells[1].Value?.ToString();
            Clipboard.SetDataObject(key, false);
        }
        #endregion

        #region Content box events
        private void ContentBox_TextChanged(object sender, EventArgs e)
        {
            SetLabelCountCharacters();
        }

        private void ContentBox_KeyUp(object sender, KeyEventArgs e)
        {
            SetLabelCountCharacters();
        }

        private void ContentBox_Leave(object sender, EventArgs e)
        {
            SetContentBoxToValue();
        }
        #endregion

        #region Grid events
        private void DataGridKeys_SelectionChanged(object sender, EventArgs e)
        {
            SetContentBoxToValue();
            Reset(resetContent: true);
            SetValueToContentBox();
        }
        #endregion

        #region Button events
        private void BtnSaveToDisk_Click(object sender, EventArgs e)
        {
            if (FileSet == null)
            {
                MessageBox.Show("Selecciona un fichero y un idioma");
                return;
            }

            SaveToDisk();
        }

        private void BtnNewFile_Click(object sender, EventArgs e)
        {
            var newFileForm = new NewFileForm();
            var result = newFileForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var name = newFileForm.FileName.Trim();
                var language = newFileForm.Language.Trim();
                Task.Factory.StartNew(async () =>
                {
                    await _fileService.NewFile(name, language);
                    Invoke((MethodInvoker)delegate
                    {
                        currentFile.Text = name;
                        currentLanguage.Text = language;
                    });
                    RefreshFiles();
                    ReloadFromDisk();
                });
            }
        }

        private void BtnNewKey_Click(object sender, EventArgs e)
        {
            if (FileSet == null)
            {
                MessageBox.Show("Selecciona un fichero y un idioma");
                return;
            }

            var newFileForm = new NewKeyForm();
            var result = newFileForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var name = newFileForm.KeyName.Trim();

                // Comprobamos que no esté ya
                foreach (DataGridViewRow row in dataGridKeys.Rows)
                {
                    var key = row.Cells[1].Value?.ToString();
                    if (key != null && key.ToLower() == name.ToLower())
                    {
                        MessageBox.Show("Esa clave ya existe");
                        return;
                    }
                }

                dataGridKeys.Rows.Add("0", name, "");
                var data = ConvertDataGridToDictionary();
                Task.Factory.StartNew(async () =>
                {
                    await _fileService.SaveJsonData(FileSet, data);
                    ReloadFromDisk();
                });
            }
        }

        private void BtnReloadFromDisk_Click(object sender, EventArgs e)
        {
            if (FileSet == null)
            {
                MessageBox.Show("Selecciona un fichero y un idioma");
                return;
            }

            ReloadFromDisk();
        }
        #endregion

        #region Combo events
        private void CurrentFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reset(resetContent: true, resetGrid: true, resetLanguage: true);

            if (string.IsNullOrEmpty(currentFile.Text)) return;

            var grouped = FilesFound
                .Where(w => w.DisplayName == currentFile.Text)
                .ToList();

            var currentLanguageSource = new List<string>();
            currentLanguageSource.AddRange(grouped.Select(s => s.Language).Distinct().OrderBy(o => o));
            currentLanguage.DataSource = currentLanguageSource;
            currentLanguage.Text = currentLanguageSource.FirstOrDefault();
        }

        private void CurrentLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reset(resetContent: true, resetGrid: true);
            ReloadFromDisk();
        }
        #endregion

        #region Aux functions
        private void RefreshFiles()
        {
            Reset(resetContent: true, resetGrid: true, resetLanguage: true, resetFile: true);

            FilesFound = _fileService.GetJsonFiles().ToArray();
            if (!FilesFound.Any()) return;

            var currentFileSource = new List<string>
            {
                string.Empty
            };
            currentFileSource.AddRange(FilesFound.Select(s => s.DisplayName).Distinct().OrderBy(o => o));

            if (currentFile.InvokeRequired)
            {
                currentFile.Invoke((MethodInvoker)delegate
                {
                    currentFile.DataSource = currentFileSource;
                });
            }
            else
            {
                currentFile.DataSource = currentFileSource;
            }
        }

        private void ReloadFromDisk()
        {
            SetFileSet();
            if (FileSet == null) return;

            Task.Factory.StartNew(async () =>
            {
                var data = await _fileService.GetJsonData(FileSet);
                dataGridKeys.Invoke((MethodInvoker)delegate
                {
                    Reset(resetContent: true, resetGrid: true);

                    foreach (var item in data)
                    {
                        var row = dataGridKeys.Rows.Add();
                        dataGridKeys.Rows[row].Cells[0].Value = CountTextParameters(item.Value);
                        dataGridKeys.Rows[row].Cells[1].Value = item.Key;
                        dataGridKeys.Rows[row].Cells[2].Value = item.Value;
                    }

                    SetValueToContentBox();
                });
            });
        }

        private void SaveToDisk()
        {
            if (FileSet == null) return;

            var data = ConvertDataGridToDictionary();
            Task.Factory.StartNew(async () =>
            {
                await _fileService.SaveJsonData(FileSet, data);
            });
        }

        private IDictionary<string, string> ConvertDataGridToDictionary()
        {
            var data = new Dictionary<string, string>();

            foreach (DataGridViewRow row in dataGridKeys.Rows)
            {
                var key = row.Cells[1].Value?.ToString();
                var value = row.Cells[2].Value?.ToString();
                data.Add(key, value);
            }

            return data;
        }

        private void Reset(bool resetContent = false, bool resetGrid = false, bool resetLanguage = false, bool resetFile = false)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ResetOut(resetContent, resetGrid, resetLanguage, resetFile);
                });
                return;
            }

            ResetOut(resetContent, resetGrid, resetLanguage, resetFile);
        }

        private void ResetOut(bool resetContent = false, bool resetGrid = false, bool resetLanguage = false, bool resetFile = false)
        {
            if (_loopFlag) return;
            _loopFlag = true;

            if (resetContent)
            {
                contentBox.Text = string.Empty;
            }

            if (resetGrid)
            {
                _lastRowIndex = -1;
                if (dataGridKeys.Rows.Count > 0) dataGridKeys.Rows.Clear();
            }

            if (resetLanguage)
            {
                currentLanguage.DataSource = new List<string>();
                currentLanguage.Text = string.Empty;
            }

            if (resetFile)
            {
                currentFile.Text = string.Empty;
            }

            _loopFlag = false;
        }

        private void SetFileSet()
        {
            string currentFileText = "";
            string currentLanguageText = "";

            // Set current file set
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    currentFileText = currentFile.Text;
                    currentLanguageText = currentLanguage.Text;
                });
            }
            else
            {
                currentFileText = currentFile.Text;
                currentLanguageText = currentLanguage.Text;
            }

            FileSet = FilesFound.FirstOrDefault(f => f.DisplayName == currentFileText && f.Language == currentLanguageText);
        }

        private void SetValueToContentBox()
        {
            if (dataGridKeys.SelectedRows.Count <= 0) return;

            var row = dataGridKeys.SelectedRows[0];
            _lastRowIndex = row.Index;
            var cellValue = row.Cells[2].Value?.ToString();
            cellValue = cellValue?.Replace("\n", Environment.NewLine);
            contentBox.Text = cellValue;
        }

        private void SetContentBoxToValue()
        {
            if (_lastRowIndex >= 0)
            {
                var settedText = contentBox.Text?.Replace(@"\n", "\n").Replace(Environment.NewLine, "\n");
                dataGridKeys.Rows[_lastRowIndex].Cells[0].Value = CountTextParameters(settedText);
                dataGridKeys.Rows[_lastRowIndex].Cells[2].Value = settedText;
            }
        }

        private string CountTextParameters(string text)
        {
            if (string.IsNullOrEmpty(text)) return "0";
            var matches = ParametersRegex.Matches(text);
            var uniqueMatches = matches
                .OfType<Match>()
                .Select(m => m.Value)
                .Distinct()
                .Count();

            return uniqueMatches.ToString();
        }

        private void SetLabelCountCharacters()
        {
            lblCharacterCount.Text = $"{contentBox.Text?.Length ?? 0} / 2048";
        }
        #endregion
    }
}
