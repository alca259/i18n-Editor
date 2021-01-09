
namespace i18nEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCharacterCount = new System.Windows.Forms.Label();
            this.btnNewFile = new FontAwesome.Sharp.IconButton();
            this.btnNewKey = new FontAwesome.Sharp.IconButton();
            this.btnReloadFromDisk = new FontAwesome.Sharp.IconButton();
            this.btnSaveToDisk = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.currentLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentFile = new System.Windows.Forms.ComboBox();
            this.contentBox = new System.Windows.Forms.TextBox();
            this.dataGridKeys = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Parametros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridKeys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.lblCharacterCount);
            this.panelTop.Controls.Add(this.btnNewFile);
            this.panelTop.Controls.Add(this.btnNewKey);
            this.panelTop.Controls.Add(this.btnReloadFromDisk);
            this.panelTop.Controls.Add(this.btnSaveToDisk);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.currentLanguage);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.currentFile);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 70);
            this.panelTop.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(399, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Longitud texto:";
            // 
            // lblCharacterCount
            // 
            this.lblCharacterCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCharacterCount.AutoSize = true;
            this.lblCharacterCount.Location = new System.Drawing.Point(493, 39);
            this.lblCharacterCount.Name = "lblCharacterCount";
            this.lblCharacterCount.Size = new System.Drawing.Size(66, 15);
            this.lblCharacterCount.TabIndex = 13;
            this.lblCharacterCount.Text = "2048 / 2048";
            this.lblCharacterCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNewFile
            // 
            this.btnNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNewFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewFile.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.btnNewFile.IconColor = System.Drawing.Color.MediumSeaGreen;
            this.btnNewFile.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnNewFile.IconSize = 24;
            this.btnNewFile.Location = new System.Drawing.Point(753, 25);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(32, 32);
            this.btnNewFile.TabIndex = 11;
            this.btnNewFile.TabStop = false;
            this.btnNewFile.UseVisualStyleBackColor = true;
            // 
            // btnNewKey
            // 
            this.btnNewKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNewKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewKey.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnNewKey.IconColor = System.Drawing.Color.MediumSeaGreen;
            this.btnNewKey.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnNewKey.IconSize = 24;
            this.btnNewKey.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewKey.Location = new System.Drawing.Point(563, 25);
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Size = new System.Drawing.Size(108, 32);
            this.btnNewKey.TabIndex = 10;
            this.btnNewKey.TabStop = false;
            this.btnNewKey.Text = "Nueva clave";
            this.btnNewKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewKey.UseVisualStyleBackColor = true;
            // 
            // btnReloadFromDisk
            // 
            this.btnReloadFromDisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadFromDisk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReloadFromDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadFromDisk.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            this.btnReloadFromDisk.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(149)))), ((int)(((byte)(220)))));
            this.btnReloadFromDisk.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnReloadFromDisk.IconSize = 24;
            this.btnReloadFromDisk.Location = new System.Drawing.Point(715, 25);
            this.btnReloadFromDisk.Name = "btnReloadFromDisk";
            this.btnReloadFromDisk.Size = new System.Drawing.Size(32, 32);
            this.btnReloadFromDisk.TabIndex = 9;
            this.btnReloadFromDisk.TabStop = false;
            this.btnReloadFromDisk.UseVisualStyleBackColor = true;
            // 
            // btnSaveToDisk
            // 
            this.btnSaveToDisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveToDisk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSaveToDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveToDisk.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnSaveToDisk.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(149)))), ((int)(((byte)(220)))));
            this.btnSaveToDisk.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSaveToDisk.IconSize = 24;
            this.btnSaveToDisk.Location = new System.Drawing.Point(677, 25);
            this.btnSaveToDisk.Name = "btnSaveToDisk";
            this.btnSaveToDisk.Size = new System.Drawing.Size(32, 32);
            this.btnSaveToDisk.TabIndex = 8;
            this.btnSaveToDisk.TabStop = false;
            this.btnSaveToDisk.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Idioma";
            // 
            // currentLanguage
            // 
            this.currentLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.currentLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentLanguage.FormattingEnabled = true;
            this.currentLanguage.Location = new System.Drawing.Point(257, 31);
            this.currentLanguage.Name = "currentLanguage";
            this.currentLanguage.Size = new System.Drawing.Size(136, 23);
            this.currentLanguage.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fichero actual";
            // 
            // currentFile
            // 
            this.currentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.currentFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentFile.FormattingEnabled = true;
            this.currentFile.Location = new System.Drawing.Point(12, 31);
            this.currentFile.Name = "currentFile";
            this.currentFile.Size = new System.Drawing.Size(239, 23);
            this.currentFile.TabIndex = 0;
            // 
            // contentBox
            // 
            this.contentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentBox.Location = new System.Drawing.Point(0, 0);
            this.contentBox.MaxLength = 2048;
            this.contentBox.Multiline = true;
            this.contentBox.Name = "contentBox";
            this.contentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentBox.Size = new System.Drawing.Size(773, 234);
            this.contentBox.TabIndex = 0;
            // 
            // dataGridKeys
            // 
            this.dataGridKeys.AllowUserToAddRows = false;
            this.dataGridKeys.AllowUserToDeleteRows = false;
            this.dataGridKeys.AllowUserToResizeColumns = false;
            this.dataGridKeys.AllowUserToResizeRows = false;
            this.dataGridKeys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridKeys.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridKeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridKeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Parametros,
            this.Key,
            this.Content});
            this.dataGridKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridKeys.Location = new System.Drawing.Point(0, 0);
            this.dataGridKeys.MultiSelect = false;
            this.dataGridKeys.Name = "dataGridKeys";
            this.dataGridKeys.ReadOnly = true;
            this.dataGridKeys.RowTemplate.Height = 25;
            this.dataGridKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridKeys.ShowCellErrors = false;
            this.dataGridKeys.ShowCellToolTips = false;
            this.dataGridKeys.ShowEditingIcon = false;
            this.dataGridKeys.ShowRowErrors = false;
            this.dataGridKeys.Size = new System.Drawing.Size(773, 235);
            this.dataGridKeys.TabIndex = 0;
            this.dataGridKeys.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 76);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridKeys);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.contentBox);
            this.splitContainer1.Panel2MinSize = 150;
            this.splitContainer1.Size = new System.Drawing.Size(773, 473);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // Parametros
            // 
            this.Parametros.FillWeight = 40F;
            this.Parametros.HeaderText = "Num Par";
            this.Parametros.MaxInputLength = 2000;
            this.Parametros.MinimumWidth = 40;
            this.Parametros.Name = "Parametros";
            this.Parametros.ReadOnly = true;
            this.Parametros.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Parametros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Parametros.Width = 60;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Key.FillWeight = 40F;
            this.Key.HeaderText = "Clave";
            this.Key.MaxInputLength = 1000;
            this.Key.MinimumWidth = 250;
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Content
            // 
            this.Content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Content.FillWeight = 60F;
            this.Content.HeaderText = "Contenido";
            this.Content.MaxInputLength = 2048;
            this.Content.MinimumWidth = 300;
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridKeys)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox currentFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox currentLanguage;
        private FontAwesome.Sharp.IconButton btnNewFile;
        private FontAwesome.Sharp.IconButton btnReloadFromDisk;
        private FontAwesome.Sharp.IconButton btnSaveToDisk;
        private System.Windows.Forms.TextBox contentBox;
        private System.Windows.Forms.DataGridView dataGridKeys;
        private FontAwesome.Sharp.IconButton btnNewKey;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblCharacterCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parametros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
    }
}