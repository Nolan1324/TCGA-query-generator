namespace TCGAQueryGenerator
{
    partial class Main
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openButton = new System.Windows.Forms.Button();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.detailsLabel = new System.Windows.Forms.LinkLabel();
            this.manifestButton = new System.Windows.Forms.Button();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(26, 26);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(210, 81);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.Enabled = false;
            this.fileNameBox.Location = new System.Drawing.Point(254, 48);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(704, 38);
            this.fileNameBox.TabIndex = 2;
            this.fileNameBox.Text = "Please open a TCGA list file";
            // 
            // copyButton
            // 
            this.copyButton.Enabled = false;
            this.copyButton.Location = new System.Drawing.Point(26, 194);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(210, 82);
            this.copyButton.TabIndex = 3;
            this.copyButton.Text = "Copy Query";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.query_Click);
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(855, 266);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(103, 32);
            this.detailsLabel.TabIndex = 4;
            this.detailsLabel.TabStop = true;
            this.detailsLabel.Text = "Details";
            this.detailsLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.detailsLabel_LinkClicked);
            // 
            // manifestButton
            // 
            this.manifestButton.Enabled = false;
            this.manifestButton.Location = new System.Drawing.Point(254, 194);
            this.manifestButton.Name = "manifestButton";
            this.manifestButton.Size = new System.Drawing.Size(290, 82);
            this.manifestButton.TabIndex = 5;
            this.manifestButton.Text = "Download Manifest and Metadata";
            this.manifestButton.UseVisualStyleBackColor = true;
            this.manifestButton.Click += new System.EventHandler(this.manifest_Click);
            // 
            // typeBox
            // 
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.Enabled = false;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "mRNA",
            "miRNA",
            "Both"});
            this.typeBox.Location = new System.Drawing.Point(218, 128);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(169, 39);
            this.typeBox.TabIndex = 6;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.typeBox_SelectedIndexChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(39, 131);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(170, 32);
            this.typeLabel.TabIndex = 7;
            this.typeLabel.Text = "Query Type:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(970, 314);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.manifestButton);
            this.Controls.Add(this.detailsLabel);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.openButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "TCGA Query Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.LinkLabel detailsLabel;
        private System.Windows.Forms.Button manifestButton;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label typeLabel;
    }
}

