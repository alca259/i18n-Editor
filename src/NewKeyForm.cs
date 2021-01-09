using System;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class NewKeyForm : Form
    {
        public string KeyName { get; private set; }

        public NewKeyForm()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            KeyName = txtName.Text;
            if (string.IsNullOrEmpty(KeyName)) return;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
