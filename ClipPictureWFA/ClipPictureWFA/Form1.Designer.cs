
namespace ClipPictureWFA
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbtnBuild = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.DgvFiles = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnHou = new System.Windows.Forms.Button();
            this.BtnQian = new System.Windows.Forms.Button();
            this.TxtOutDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtInDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ShangKuang = new System.Windows.Forms.Panel();
            this.ZuoKuang = new System.Windows.Forms.Panel();
            this.YouKuang = new System.Windows.Forms.Panel();
            this.XiaKuang = new System.Windows.Forms.Panel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvFiles)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(1014, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 编辑ToolStripMenuItem
            // 
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnBuild,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1014, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbtnBuild
            // 
            this.TsbtnBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbtnBuild.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnBuild.Image")));
            this.TsbtnBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnBuild.Name = "TsbtnBuild";
            this.TsbtnBuild.Size = new System.Drawing.Size(23, 22);
            this.TsbtnBuild.Text = "toolStripButton1";
            this.TsbtnBuild.Click += new System.EventHandler(this.TsbtnBuild_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.XiaKuang);
            this.splitContainer1.Panel1.Controls.Add(this.YouKuang);
            this.splitContainer1.Panel1.Controls.Add(this.ZuoKuang);
            this.splitContainer1.Panel1.Controls.Add(this.ShangKuang);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1014, 668);
            this.splitContainer1.SplitterDistance = 692;
            this.splitContainer1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(690, 666);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.DgvFiles);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.BtnHou);
            this.splitContainer2.Panel2.Controls.Add(this.BtnQian);
            this.splitContainer2.Panel2.Controls.Add(this.TxtOutDir);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.TxtInDir);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(318, 668);
            this.splitContainer2.SplitterDistance = 554;
            this.splitContainer2.TabIndex = 0;
            // 
            // DgvFiles
            // 
            this.DgvFiles.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.DgvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvFiles.Location = new System.Drawing.Point(0, 0);
            this.DgvFiles.Name = "DgvFiles";
            this.DgvFiles.RowTemplate.Height = 23;
            this.DgvFiles.Size = new System.Drawing.Size(316, 552);
            this.DgvFiles.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "N";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "文件名";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // BtnHou
            // 
            this.BtnHou.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnHou.Location = new System.Drawing.Point(243, 71);
            this.BtnHou.Name = "BtnHou";
            this.BtnHou.Size = new System.Drawing.Size(62, 23);
            this.BtnHou.TabIndex = 2;
            this.BtnHou.Text = "后一个";
            this.BtnHou.UseVisualStyleBackColor = true;
            this.BtnHou.Click += new System.EventHandler(this.BtnHou_Click);
            // 
            // BtnQian
            // 
            this.BtnQian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnQian.Location = new System.Drawing.Point(17, 71);
            this.BtnQian.Name = "BtnQian";
            this.BtnQian.Size = new System.Drawing.Size(75, 23);
            this.BtnQian.TabIndex = 2;
            this.BtnQian.Text = "前一个";
            this.BtnQian.UseVisualStyleBackColor = true;
            this.BtnQian.Click += new System.EventHandler(this.BtnQian_Click);
            // 
            // TxtOutDir
            // 
            this.TxtOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtOutDir.Location = new System.Drawing.Point(86, 44);
            this.TxtOutDir.Name = "TxtOutDir";
            this.TxtOutDir.Size = new System.Drawing.Size(219, 21);
            this.TxtOutDir.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出文件夹";
            // 
            // TxtInDir
            // 
            this.TxtInDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtInDir.Location = new System.Drawing.Point(86, 17);
            this.TxtInDir.Name = "TxtInDir";
            this.TxtInDir.Size = new System.Drawing.Size(219, 21);
            this.TxtInDir.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入文件夹";
            // 
            // ShangKuang
            // 
            this.ShangKuang.BackColor = System.Drawing.Color.Red;
            this.ShangKuang.Location = new System.Drawing.Point(114, 24);
            this.ShangKuang.Name = "ShangKuang";
            this.ShangKuang.Size = new System.Drawing.Size(490, 10);
            this.ShangKuang.TabIndex = 1;
            // 
            // ZuoKuang
            // 
            this.ZuoKuang.BackColor = System.Drawing.Color.Red;
            this.ZuoKuang.Location = new System.Drawing.Point(88, 156);
            this.ZuoKuang.Name = "ZuoKuang";
            this.ZuoKuang.Size = new System.Drawing.Size(10, 435);
            this.ZuoKuang.TabIndex = 1;
            // 
            // YouKuang
            // 
            this.YouKuang.BackColor = System.Drawing.Color.Red;
            this.YouKuang.Location = new System.Drawing.Point(559, 161);
            this.YouKuang.Name = "YouKuang";
            this.YouKuang.Size = new System.Drawing.Size(10, 435);
            this.YouKuang.TabIndex = 1;
            // 
            // XiaKuang
            // 
            this.XiaKuang.BackColor = System.Drawing.Color.Red;
            this.XiaKuang.Location = new System.Drawing.Point(104, 589);
            this.XiaKuang.Name = "XiaKuang";
            this.XiaKuang.Size = new System.Drawing.Size(449, 17);
            this.XiaKuang.TabIndex = 1;
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "左上角",
            "右下角"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 718);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbtnBuild;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView DgvFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox TxtOutDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtInDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnHou;
        private System.Windows.Forms.Button BtnQian;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel ShangKuang;
        private System.Windows.Forms.Panel XiaKuang;
        private System.Windows.Forms.Panel YouKuang;
        private System.Windows.Forms.Panel ZuoKuang;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}

