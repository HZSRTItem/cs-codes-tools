
namespace DcTfrWFA01
{
    partial class DTMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DTMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.TscbTfrFileList = new System.Windows.Forms.ToolStripComboBox();
            this.TsbtnCalTong = new System.Windows.Forms.ToolStripButton();
            this.TsbtnExp = new System.Windows.Forms.ToolStripButton();
            this.TstbExpSize = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.RtbRunInfo = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DgvOneTfrInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnClXia = new System.Windows.Forms.Button();
            this.BtnExpXia = new System.Windows.Forms.Button();
            this.BtnExpShang = new System.Windows.Forms.Button();
            this.BtnClShang = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ClbExpData = new System.Windows.Forms.CheckedListBox();
            this.ClbClData = new System.Windows.Forms.CheckedListBox();
            this.ClbAllTfrFile = new System.Windows.Forms.CheckedListBox();
            this.TsbtnRun = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvOneTfrInfo)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(692, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.TscbTfrFileList,
            this.TsbtnCalTong,
            this.TsbtnExp,
            this.TstbExpSize,
            this.TsbtnRun});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(692, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "文件列表";
            // 
            // TscbTfrFileList
            // 
            this.TscbTfrFileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TscbTfrFileList.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.TscbTfrFileList.Name = "TscbTfrFileList";
            this.TscbTfrFileList.Size = new System.Drawing.Size(200, 25);
            this.TscbTfrFileList.SelectedIndexChanged += new System.EventHandler(this.TscbTfrFileList_SelectedIndexChanged);
            this.TscbTfrFileList.TextChanged += new System.EventHandler(this.TscbTfrFileList_TextChanged);
            // 
            // TsbtnCalTong
            // 
            this.TsbtnCalTong.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnCalTong.Image")));
            this.TsbtnCalTong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnCalTong.Name = "TsbtnCalTong";
            this.TsbtnCalTong.Size = new System.Drawing.Size(100, 22);
            this.TsbtnCalTong.Text = "计算共同要素";
            this.TsbtnCalTong.Click += new System.EventHandler(this.TsbtnCalTong_Click);
            // 
            // TsbtnExp
            // 
            this.TsbtnExp.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnExp.Image")));
            this.TsbtnExp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnExp.Name = "TsbtnExp";
            this.TsbtnExp.Size = new System.Drawing.Size(52, 22);
            this.TsbtnExp.Text = "导出";
            this.TsbtnExp.Click += new System.EventHandler(this.TsbtnExp_Click);
            // 
            // TstbExpSize
            // 
            this.TstbExpSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TstbExpSize.Name = "TstbExpSize";
            this.TstbExpSize.Size = new System.Drawing.Size(100, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 740);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // RtbRunInfo
            // 
            this.RtbRunInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RtbRunInfo.Location = new System.Drawing.Point(12, 539);
            this.RtbRunInfo.Name = "RtbRunInfo";
            this.RtbRunInfo.Size = new System.Drawing.Size(668, 198);
            this.RtbRunInfo.TabIndex = 4;
            this.RtbRunInfo.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 480);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DgvOneTfrInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文件信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DgvOneTfrInfo
            // 
            this.DgvOneTfrInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvOneTfrInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DgvOneTfrInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvOneTfrInfo.Location = new System.Drawing.Point(3, 3);
            this.DgvOneTfrInfo.Name = "DgvOneTfrInfo";
            this.DgvOneTfrInfo.RowTemplate.Height = 25;
            this.DgvOneTfrInfo.Size = new System.Drawing.Size(654, 444);
            this.DgvOneTfrInfo.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "数量";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "类型";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "信息";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BtnClXia);
            this.tabPage2.Controls.Add(this.BtnExpXia);
            this.tabPage2.Controls.Add(this.BtnExpShang);
            this.tabPage2.Controls.Add(this.BtnClShang);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.ClbExpData);
            this.tabPage2.Controls.Add(this.ClbClData);
            this.tabPage2.Controls.Add(this.ClbAllTfrFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "导出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnClXia
            // 
            this.BtnClXia.Location = new System.Drawing.Point(545, 58);
            this.BtnClXia.Name = "BtnClXia";
            this.BtnClXia.Size = new System.Drawing.Size(37, 23);
            this.BtnClXia.TabIndex = 2;
            this.BtnClXia.Text = "下";
            this.BtnClXia.UseVisualStyleBackColor = true;
            this.BtnClXia.Click += new System.EventHandler(this.BtnClXia_Click);
            // 
            // BtnExpXia
            // 
            this.BtnExpXia.Location = new System.Drawing.Point(545, 201);
            this.BtnExpXia.Name = "BtnExpXia";
            this.BtnExpXia.Size = new System.Drawing.Size(37, 23);
            this.BtnExpXia.TabIndex = 2;
            this.BtnExpXia.Text = "下";
            this.BtnExpXia.UseVisualStyleBackColor = true;
            this.BtnExpXia.Click += new System.EventHandler(this.BtnExpXia_Click);
            // 
            // BtnExpShang
            // 
            this.BtnExpShang.Location = new System.Drawing.Point(545, 172);
            this.BtnExpShang.Name = "BtnExpShang";
            this.BtnExpShang.Size = new System.Drawing.Size(37, 23);
            this.BtnExpShang.TabIndex = 2;
            this.BtnExpShang.Text = "上";
            this.BtnExpShang.UseVisualStyleBackColor = true;
            this.BtnExpShang.Click += new System.EventHandler(this.BtnExpShang_Click);
            // 
            // BtnClShang
            // 
            this.BtnClShang.Location = new System.Drawing.Point(545, 29);
            this.BtnClShang.Name = "BtnClShang";
            this.BtnClShang.Size = new System.Drawing.Size(37, 23);
            this.BtnClShang.TabIndex = 2;
            this.BtnClShang.Text = "上";
            this.BtnClShang.UseVisualStyleBackColor = true;
            this.BtnClShang.Click += new System.EventHandler(this.BtnClShang_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "导出数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "标记数据";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "所有文件";
            // 
            // ClbExpData
            // 
            this.ClbExpData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ClbExpData.FormattingEnabled = true;
            this.ClbExpData.Location = new System.Drawing.Point(247, 172);
            this.ClbExpData.Name = "ClbExpData";
            this.ClbExpData.Size = new System.Drawing.Size(292, 274);
            this.ClbExpData.TabIndex = 0;
            // 
            // ClbClData
            // 
            this.ClbClData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ClbClData.FormattingEnabled = true;
            this.ClbClData.Location = new System.Drawing.Point(247, 29);
            this.ClbClData.Name = "ClbClData";
            this.ClbClData.Size = new System.Drawing.Size(292, 112);
            this.ClbClData.TabIndex = 0;
            // 
            // ClbAllTfrFile
            // 
            this.ClbAllTfrFile.FormattingEnabled = true;
            this.ClbAllTfrFile.Location = new System.Drawing.Point(6, 29);
            this.ClbAllTfrFile.Name = "ClbAllTfrFile";
            this.ClbAllTfrFile.Size = new System.Drawing.Size(235, 418);
            this.ClbAllTfrFile.TabIndex = 0;
            // 
            // TsbtnRun
            // 
            this.TsbtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbtnRun.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnRun.Image")));
            this.TsbtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnRun.Name = "TsbtnRun";
            this.TsbtnRun.Size = new System.Drawing.Size(23, 22);
            this.TsbtnRun.Text = "toolStripButton1";
            this.TsbtnRun.Click += new System.EventHandler(this.TsbtnRun_Click);
            // 
            // DTMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 762);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.RtbRunInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DTMainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvOneTfrInfo)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox TscbTfrFileList;
        private System.Windows.Forms.RichTextBox RtbRunInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DgvOneTfrInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox ClbAllTfrFile;
        private System.Windows.Forms.ToolStripButton TsbtnCalTong;
        private System.Windows.Forms.CheckedListBox ClbExpData;
        private System.Windows.Forms.CheckedListBox ClbClData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnClXia;
        private System.Windows.Forms.Button BtnExpXia;
        private System.Windows.Forms.Button BtnExpShang;
        private System.Windows.Forms.Button BtnClShang;
        private System.Windows.Forms.ToolStripButton TsbtnExp;
        private System.Windows.Forms.ToolStripTextBox TstbExpSize;
        private System.Windows.Forms.ToolStripButton TsbtnRun;
    }
}

