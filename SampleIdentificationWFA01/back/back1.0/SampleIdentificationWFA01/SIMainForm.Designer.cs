
namespace SampleIdentificationWFA01
{
    partial class SIMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExportPrj = new System.Windows.Forms.ToolStripMenuItem();
            this.打开工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DgvClasses = new System.Windows.Forms.DataGridView();
            this.ColNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColClasses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColClassesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbtnOpenPrj = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.TstxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.TsbtnExportPrj = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImbShow = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtClasses = new System.Windows.Forms.TextBox();
            this.BtnPrior = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.RtbRun = new System.Windows.Forms.RichTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClasses)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImbShow)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1163, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiExportPrj,
            this.打开工程ToolStripMenuItem,
            this.保存工程ToolStripMenuItem,
            this.另存为工程ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // TsmiExportPrj
            // 
            this.TsmiExportPrj.Name = "TsmiExportPrj";
            this.TsmiExportPrj.Size = new System.Drawing.Size(148, 24);
            this.TsmiExportPrj.Text = "导入工程";
            // 
            // 打开工程ToolStripMenuItem
            // 
            this.打开工程ToolStripMenuItem.Name = "打开工程ToolStripMenuItem";
            this.打开工程ToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
            this.打开工程ToolStripMenuItem.Text = "打开工程";
            // 
            // 保存工程ToolStripMenuItem
            // 
            this.保存工程ToolStripMenuItem.Name = "保存工程ToolStripMenuItem";
            this.保存工程ToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
            this.保存工程ToolStripMenuItem.Text = "保存工程";
            // 
            // 另存为工程ToolStripMenuItem
            // 
            this.另存为工程ToolStripMenuItem.Name = "另存为工程ToolStripMenuItem";
            this.另存为工程ToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
            this.另存为工程ToolStripMenuItem.Text = "另存为工程";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图像列表ToolStripMenuItem});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // 图像列表ToolStripMenuItem
            // 
            this.图像列表ToolStripMenuItem.Name = "图像列表ToolStripMenuItem";
            this.图像列表ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.图像列表ToolStripMenuItem.Text = "图像列表";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // DgvClasses
            // 
            this.DgvClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvClasses.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DgvClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvClasses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNum,
            this.ColClasses,
            this.ColClassesName,
            this.ColNumber});
            this.DgvClasses.Location = new System.Drawing.Point(6, 22);
            this.DgvClasses.Name = "DgvClasses";
            this.DgvClasses.RowTemplate.Height = 30;
            this.DgvClasses.Size = new System.Drawing.Size(309, 317);
            this.DgvClasses.TabIndex = 2;
            // 
            // ColNum
            // 
            this.ColNum.HeaderText = "";
            this.ColNum.Name = "ColNum";
            this.ColNum.ReadOnly = true;
            this.ColNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColNum.Width = 30;
            // 
            // ColClasses
            // 
            this.ColClasses.HeaderText = "No.";
            this.ColClasses.Name = "ColClasses";
            this.ColClasses.Width = 30;
            // 
            // ColClassesName
            // 
            this.ColClassesName.HeaderText = "类别名";
            this.ColClassesName.Name = "ColClassesName";
            this.ColClassesName.Width = 120;
            // 
            // ColNumber
            // 
            this.ColNumber.HeaderText = "数量";
            this.ColNumber.Name = "ColNumber";
            this.ColNumber.Width = 80;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnOpenPrj,
            this.TsbtnExportPrj,
            this.toolStripButton2,
            this.toolStripLabel1,
            this.TstxtSearch,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1163, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbtnOpenPrj
            // 
            this.TsbtnOpenPrj.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnOpenPrj.Image")));
            this.TsbtnOpenPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnOpenPrj.Name = "TsbtnOpenPrj";
            this.TsbtnOpenPrj.Size = new System.Drawing.Size(85, 24);
            this.TsbtnOpenPrj.Text = "打开工程";
            this.TsbtnOpenPrj.Click += new System.EventHandler(this.TsbtnOpenPrj_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 24);
            this.toolStripLabel1.Text = "搜索";
            // 
            // TstxtSearch
            // 
            this.TstxtSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TstxtSearch.Name = "TstxtSearch";
            this.TstxtSearch.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(85, 24);
            this.toolStripButton2.Text = "保存工程";
            // 
            // TsbtnExportPrj
            // 
            this.TsbtnExportPrj.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnExportPrj.Image")));
            this.TsbtnExportPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnExportPrj.Name = "TsbtnExportPrj";
            this.TsbtnExportPrj.Size = new System.Drawing.Size(85, 24);
            this.TsbtnExportPrj.Text = "导入工程";
            this.TsbtnExportPrj.Click += new System.EventHandler(this.TsbtnExportPrj_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.ImbShow);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 727);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像显示";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(382, 362);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(16, 16);
            this.panel1.TabIndex = 1;
            // 
            // ImbShow
            // 
            this.ImbShow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ImbShow.Image = global::SampleIdentificationWFA01.Properties.Resources.驴子;
            this.ImbShow.Location = new System.Drawing.Point(55, 25);
            this.ImbShow.Name = "ImbShow";
            this.ImbShow.Size = new System.Drawing.Size(690, 690);
            this.ImbShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImbShow.TabIndex = 0;
            this.ImbShow.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TxtClasses);
            this.groupBox2.Controls.Add(this.BtnPrior);
            this.groupBox2.Controls.Add(this.BtnNext);
            this.groupBox2.Controls.Add(this.DgvClasses);
            this.groupBox2.Location = new System.Drawing.Point(830, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 727);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 668);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "类别";
            // 
            // TxtClasses
            // 
            this.TxtClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtClasses.Location = new System.Drawing.Point(63, 663);
            this.TxtClasses.Name = "TxtClasses";
            this.TxtClasses.Size = new System.Drawing.Size(252, 23);
            this.TxtClasses.TabIndex = 5;
            this.TxtClasses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtClasses_KeyUp);
            // 
            // BtnPrior
            // 
            this.BtnPrior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrior.Location = new System.Drawing.Point(6, 692);
            this.BtnPrior.Name = "BtnPrior";
            this.BtnPrior.Size = new System.Drawing.Size(75, 23);
            this.BtnPrior.TabIndex = 3;
            this.BtnPrior.Text = "上一个";
            this.BtnPrior.UseVisualStyleBackColor = true;
            this.BtnPrior.Click += new System.EventHandler(this.BtnPrior_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNext.Location = new System.Drawing.Point(240, 692);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(75, 23);
            this.BtnNext.TabIndex = 3;
            this.BtnNext.Text = "下一个";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 971);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1163, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // RtbRun
            // 
            this.RtbRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RtbRun.Location = new System.Drawing.Point(12, 789);
            this.RtbRun.Name = "RtbRun";
            this.RtbRun.ReadOnly = true;
            this.RtbRun.Size = new System.Drawing.Size(1139, 179);
            this.RtbRun.TabIndex = 6;
            this.RtbRun.Text = "";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(55, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 5);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(387, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 690);
            this.panel3.TabIndex = 1;
            // 
            // SIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 993);
            this.Controls.Add(this.RtbRun);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SIMainForm";
            this.Text = "样本解译工具 - v1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClasses)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImbShow)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.DataGridView DgvClasses;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbtnOpenPrj;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox TstxtSearch;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.PictureBox ImbShow;
        private System.Windows.Forms.ToolStripMenuItem TsmiExportPrj;
        private System.Windows.Forms.ToolStripMenuItem 打开工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button BtnPrior;
        private System.Windows.Forms.TextBox TxtClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 图像列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TsbtnExportPrj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColClasses;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColClassesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumber;
        private System.Windows.Forms.RichTextBox RtbRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}

