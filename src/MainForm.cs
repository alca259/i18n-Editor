using i18nEditor.DTOs;
using i18nEditor.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class MainForm : Form
    {
        private int PageSize = 10; // rows per page
        private int CurrentPageIndex = 1;
        private int TotalPage;

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

            currentFile.DataSourceChanged += CurrentFile_DataSourceChanged;
            currentFile.SelectedIndexChanged += CurrentFile_SelectedIndexChanged;
            currentLanguage.SelectedIndexChanged += CurrentLanguage_SelectedIndexChanged;

            btnReloadFromDisk.Click += BtnReloadFromDisk_Click;
            btnNewLanguage.Click += BtnNewLanguage_Click;
            btnNewFile.Click += BtnNewFile_Click;
            btnSaveToDisk.Click += BtnSaveToDisk_Click;

            dataGridKeys.SelectionChanged += DataGridKeys_SelectionChanged;

            RefreshFiles();
        }

        #region Grid events
        private void DataGridKeys_SelectionChanged(object sender, EventArgs e)
        {
            Setter(100);

            if (dataGridKeys.SelectedRows.Count <= 0) return;

            var cellValue = dataGridKeys.SelectedRows[0].Cells[1].Value?.ToString();
            cellValue = cellValue?.Replace("\n", Environment.NewLine);
            contentBox.Text = cellValue;
        }
        #endregion

        #region Button events
        private void BtnSaveToDisk_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnNewFile_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnNewLanguage_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnReloadFromDisk_Click(object sender, EventArgs e)
        {
            if (FileSet == null)
            {
                MessageBox.Show("Selecciona un fichero y un idioma");
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                var data = await _fileService.GetJsonData(FileSet);
                dataGridKeys.Invoke((MethodInvoker)delegate
                {
                    Setter(30);

                    foreach (var item in data)
                    {
                        var row = dataGridKeys.Rows.Add();
                        dataGridKeys.Rows[row].Cells[0].Value = item.Key;
                        dataGridKeys.Rows[row].Cells[1].Value = item.Value;
                    }
                });
            });
        }
        #endregion

        #region Combo events
        private void CurrentFile_DataSourceChanged(object sender, EventArgs e)
        {
            Setter(0);
        }

        private void CurrentFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setter(20);

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
            Setter(30);
        }
        #endregion

        private void RefreshFiles()
        {
            Setter(0);

            FilesFound = _fileService.GetJsonFiles().ToArray();
            if (!FilesFound.Any()) return;

            var currentFileSource = new List<string>
            {
                string.Empty
            };
            currentFileSource.AddRange(FilesFound.Select(s => s.DisplayName).Distinct().OrderBy(o => o));
            currentFile.DataSource = currentFileSource;
        }

        private void Setter(int level = 0)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetterOut(level);
                });
                return;
            }

            SetterOut(level);
        }

        private void SetterOut(int level)
        {
            if (level <= 100) contentBox.Text = string.Empty;

            if (level <= 90) dataGridKeys.Rows.Clear();

            if (level <= 20) currentLanguage.DataSource = new List<string>();
            if (level <= 20) currentLanguage.Text = string.Empty;

            if (level <= 10) currentFile.Text = string.Empty;

            FileSet = FilesFound.FirstOrDefault(f => f.DisplayName == currentFile.Text && f.Language == currentLanguage.Text);
        }
    }
}
