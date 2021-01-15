using System;
using System.Windows.Forms;

namespace i18nEditor
{
    public enum InputBy
    {
        None = 0,
        Key = 1,
        Content = 2
    }

    public partial class InputBox : Form
    {
        public string SearchedText { get; private set; }
        public int SearchBy { get; private set; }

        public InputBox()
        {
            InitializeComponent();

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;

            ddContent.SelectedIndex = 1;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            switch (ddContent.SelectedItem.ToString())
            {
                case "Clave":
                    SearchBy = (int)InputBy.Key;
                    break;
                case "Contenido":
                    SearchBy = (int)InputBy.Content;
                    break;
            }
            SearchedText = textBoxSearch.Text ?? string.Empty;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
