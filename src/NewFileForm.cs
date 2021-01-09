using System;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class NewFileForm : Form
    {
        public string FileName { get; private set; }
        public string Language { get; private set; }

        public NewFileForm()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            FileName = txtName.Text;
            Language = txtLanguage.Text;
            if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(Language)) return;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
