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

        public MainForm(IFileService fileService, ILogger<MainForm> logger)
        {
            InitializeComponent();
            _fileService = fileService;
            _logger = logger;

            var files = _fileService.GetJsonFiles().ToArray();
        }


    }
}
