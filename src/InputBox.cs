using System;
using System.Windows.Forms;

namespace i18nEditor
{
    public partial class InputBox : Form
    {
        public string SearchedText { get; private set; }

        public InputBox()
        {
            InitializeComponent();

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            SearchedText = textBoxSearch.Text ?? "";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
