
namespace PdfMergeWFA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.BtnOutFile = new System.Windows.Forms.Button();
            this.DgvPdfList = new System.Windows.Forms.DataGridView();
            this.TsbtnExport = new System.Windows.Forms.ToolStripButton();
            this.TsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TsmiUp = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiDown = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbtnUp = new System.Windows.Forms.ToolStripButton();
            this.TsbtnDown = new System.Windows.Forms.ToolStripButton();
            this.TsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPdfList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(513, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiAdd,
            this.TsmiExport,
            this.TsmiExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiUp,
            this.TsmiDown,
            this.TsmiDelete});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(513, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnAdd,
            this.TsbtnExport,
            this.TsbtnUp,
            this.TsbtnDown,
            this.TsbtnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(513, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbtnAdd
            // 
            this.TsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnAdd.Image")));
            this.TsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnAdd.Name = "TsbtnAdd";
            this.TsbtnAdd.Size = new System.Drawing.Size(74, 22);
            this.TsbtnAdd.Text = "添加PDF";
            this.TsbtnAdd.Click += new System.EventHandler(this.TsbtnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "PDF 文件列表";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "输出文件";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(71, 520);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(384, 21);
            this.textBox2.TabIndex = 4;
            // 
            // BtnOutFile
            // 
            this.BtnOutFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOutFile.Location = new System.Drawing.Point(461, 519);
            this.BtnOutFile.Name = "BtnOutFile";
            this.BtnOutFile.Size = new System.Drawing.Size(40, 23);
            this.BtnOutFile.TabIndex = 5;
            this.BtnOutFile.Text = "...";
            this.BtnOutFile.UseVisualStyleBackColor = true;
            this.BtnOutFile.Click += new System.EventHandler(this.BtnOutFile_Click);
            // 
            // DgvPdfList
            // 
            this.DgvPdfList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPdfList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.DgvPdfList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPdfList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column4,
            this.Column3});
            this.DgvPdfList.Location = new System.Drawing.Point(14, 80);
            this.DgvPdfList.Name = "DgvPdfList";
            this.DgvPdfList.RowTemplate.Height = 23;
            this.DgvPdfList.Size = new System.Drawing.Size(487, 433);
            this.DgvPdfList.TabIndex = 6;
            // 
            // TsbtnExport
            // 
            this.TsbtnExport.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnExport.Image")));
            this.TsbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnExport.Name = "TsbtnExport";
            this.TsbtnExport.Size = new System.Drawing.Size(74, 22);
            this.TsbtnExport.Text = "导出PDF";
            this.TsbtnExport.Click += new System.EventHandler(this.TsbtnExport_Click);
            // 
            // TsmiAdd
            // 
            this.TsmiAdd.Name = "TsmiAdd";
            this.TsmiAdd.Size = new System.Drawing.Size(180, 22);
            this.TsmiAdd.Text = "添加PDF";
            this.TsmiAdd.Click += new System.EventHandler(this.TsmiAdd_Click);
            // 
            // TsmiExport
            // 
            this.TsmiExport.Name = "TsmiExport";
            this.TsmiExport.Size = new System.Drawing.Size(180, 22);
            this.TsmiExport.Text = "导出PDF";
            this.TsmiExport.Click += new System.EventHandler(this.TsmiExport_Click);
            // 
            // TsmiExit
            // 
            this.TsmiExit.Name = "TsmiExit";
            this.TsmiExit.Size = new System.Drawing.Size(180, 22);
            this.TsmiExit.Text = "退出";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "  ";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 30;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "文件";
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "文件夹";
            this.Column4.Name = "Column4";
            this.Column4.Width = 160;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "文件名";
            this.Column3.Name = "Column3";
            // 
            // TsmiUp
            // 
            this.TsmiUp.Name = "TsmiUp";
            this.TsmiUp.Size = new System.Drawing.Size(180, 22);
            this.TsmiUp.Text = "上移";
            this.TsmiUp.Click += new System.EventHandler(this.TsmiUp_Click);
            // 
            // TsmiDown
            // 
            this.TsmiDown.Name = "TsmiDown";
            this.TsmiDown.Size = new System.Drawing.Size(180, 22);
            this.TsmiDown.Text = "下移";
            this.TsmiDown.Click += new System.EventHandler(this.TsmiDown_Click);
            // 
            // TsbtnUp
            // 
            this.TsbtnUp.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnUp.Image")));
            this.TsbtnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnUp.Name = "TsbtnUp";
            this.TsbtnUp.Size = new System.Drawing.Size(74, 22);
            this.TsbtnUp.Text = "PDF上移";
            this.TsbtnUp.Click += new System.EventHandler(this.TsbtnUp_Click);
            // 
            // TsbtnDown
            // 
            this.TsbtnDown.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnDown.Image")));
            this.TsbtnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnDown.Name = "TsbtnDown";
            this.TsbtnDown.Size = new System.Drawing.Size(74, 22);
            this.TsbtnDown.Text = "PDF下移";
            this.TsbtnDown.Click += new System.EventHandler(this.TsbtnDown_Click);
            // 
            // TsmiDelete
            // 
            this.TsmiDelete.Name = "TsmiDelete";
            this.TsmiDelete.Size = new System.Drawing.Size(180, 22);
            this.TsmiDelete.Text = "删除";
            this.TsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // TsbtnDelete
            // 
            this.TsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnDelete.Image")));
            this.TsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnDelete.Name = "TsbtnDelete";
            this.TsbtnDelete.Size = new System.Drawing.Size(74, 22);
            this.TsbtnDelete.Text = "删除PDF";
            this.TsbtnDelete.Click += new System.EventHandler(this.TsbtnDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 566);
            this.Controls.Add(this.DgvPdfList);
            this.Controls.Add(this.BtnOutFile);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PDF 合并";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPdfList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbtnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button BtnOutFile;
        private System.Windows.Forms.DataGridView DgvPdfList;
        private System.Windows.Forms.ToolStripButton TsbtnExport;
        private System.Windows.Forms.ToolStripMenuItem TsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem TsmiExport;
        private System.Windows.Forms.ToolStripMenuItem TsmiExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ToolStripMenuItem TsmiUp;
        private System.Windows.Forms.ToolStripMenuItem TsmiDown;
        private System.Windows.Forms.ToolStripButton TsbtnUp;
        private System.Windows.Forms.ToolStripButton TsbtnDown;
        private System.Windows.Forms.ToolStripMenuItem TsmiDelete;
        private System.Windows.Forms.ToolStripButton TsbtnDelete;
    }
}

