using i18nEditor.DTOs;
using i18nEditor.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class MainForm : Form
    {
        private int _lastRowIndex = -1;
        private bool _loopFlag = false;
        private readonly IFileService _fileService;
        private readonly ILogger<MainForm> _logger;

        private IEnumerable<FileKeyPathDto> FilesFound { get; set; } = new List<FileKeyPathDto>();
        private FileKeyPathDto FileSet { get; set; }

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

            RefreshFiles();
        }

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
                    var key = row.Cells[0].Value?.ToString();
                    if (key != null && key.ToLower() == name.ToLower())
                    {
                        MessageBox.Show("Esa clave ya existe");
                        return;
                    }
                }

                dataGridKeys.Rows.Add(name, "");
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
                        dataGridKeys.Rows[row].Cells[0].Value = item.Key;
                        dataGridKeys.Rows[row].Cells[1].Value = item.Value;
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
                var key = row.Cells[0].Value?.ToString();
                var value = row.Cells[1].Value?.ToString();
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
            var cellValue = row.Cells[1].Value?.ToString();
            cellValue = cellValue?.Replace("\n", Environment.NewLine);
            contentBox.Text = cellValue;
        }

        private void SetContentBoxToValue()
        {
            if (_lastRowIndex >= 0)
            {
                dataGridKeys.Rows[_lastRowIndex].Cells[1].Value = contentBox.Text?.Replace(Environment.NewLine, "\n");
            }
        }
    }
}
