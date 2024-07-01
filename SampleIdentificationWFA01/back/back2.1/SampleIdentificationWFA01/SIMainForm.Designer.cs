
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIMainForm));
            this.MsSplIndf = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiExportPrj = new System.Windows.Forms.ToolStripMenuItem();
            this.打开工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tfrecord转工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DgvClasses = new System.Windows.Forms.DataGridView();
            this.ColNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColClasses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColClassesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TsSplIndf = new System.Windows.Forms.ToolStrip();
            this.TsbtnOpenPrj = new System.Windows.Forms.ToolStripButton();
            this.TsbtnExportPrj = new System.Windows.Forms.ToolStripButton();
            this.TsbtnTimerPlay = new System.Windows.Forms.ToolStripButton();
            this.TstxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.GbImShow = new System.Windows.Forms.GroupBox();
            this.PClassColor = new System.Windows.Forms.Panel();
            this.Hengshizi = new System.Windows.Forms.Panel();
            this.ShuShizi = new System.Windows.Forms.Panel();
            this.ImbShow = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RtbSplInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtClasses = new System.Windows.Forms.TextBox();
            this.BtnPrior = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TmrPlay = new System.Windows.Forms.Timer(this.components);
            this.MsSplIndf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClasses)).BeginInit();
            this.TsSplIndf.SuspendLayout();
            this.GbImShow.SuspendLayout();
            this.PClassColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImbShow)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsSplIndf
            // 
            this.MsSplIndf.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MsSplIndf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.MsSplIndf.Location = new System.Drawing.Point(0, 0);
            this.MsSplIndf.Name = "MsSplIndf";
            this.MsSplIndf.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MsSplIndf.Size = new System.Drawing.Size(1163, 28);
            this.MsSplIndf.TabIndex = 0;
            this.MsSplIndf.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiExportPrj,
            this.打开工程ToolStripMenuItem,
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
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tfrecord转工程ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // tfrecord转工程ToolStripMenuItem
            // 
            this.tfrecord转工程ToolStripMenuItem.Name = "tfrecord转工程ToolStripMenuItem";
            this.tfrecord转工程ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.tfrecord转工程ToolStripMenuItem.Text = "Tfrecord转工程";
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
            this.DgvClasses.Size = new System.Drawing.Size(340, 226);
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
            // TsSplIndf
            // 
            this.TsSplIndf.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TsSplIndf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnOpenPrj,
            this.TsbtnExportPrj,
            this.TsbtnTimerPlay,
            this.TstxtSearch});
            this.TsSplIndf.Location = new System.Drawing.Point(0, 28);
            this.TsSplIndf.Name = "TsSplIndf";
            this.TsSplIndf.Size = new System.Drawing.Size(1163, 28);
            this.TsSplIndf.TabIndex = 3;
            this.TsSplIndf.Text = "toolStrip1";
            // 
            // TsbtnOpenPrj
            // 
            this.TsbtnOpenPrj.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnOpenPrj.Image")));
            this.TsbtnOpenPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnOpenPrj.Name = "TsbtnOpenPrj";
            this.TsbtnOpenPrj.Size = new System.Drawing.Size(94, 25);
            this.TsbtnOpenPrj.Text = "打开工程";
            this.TsbtnOpenPrj.Click += new System.EventHandler(this.TsbtnOpenPrj_Click);
            // 
            // TsbtnExportPrj
            // 
            this.TsbtnExportPrj.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnExportPrj.Image")));
            this.TsbtnExportPrj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnExportPrj.Name = "TsbtnExportPrj";
            this.TsbtnExportPrj.Size = new System.Drawing.Size(94, 25);
            this.TsbtnExportPrj.Text = "导入工程";
            this.TsbtnExportPrj.Click += new System.EventHandler(this.TsbtnExportPrj_Click);
            // 
            // TsbtnTimerPlay
            // 
            this.TsbtnTimerPlay.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnTimerPlay.Image")));
            this.TsbtnTimerPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnTimerPlay.Name = "TsbtnTimerPlay";
            this.TsbtnTimerPlay.Size = new System.Drawing.Size(94, 25);
            this.TsbtnTimerPlay.Text = "计时播放";
            this.TsbtnTimerPlay.Click += new System.EventHandler(this.TsbtnTimerPlay_Click);
            // 
            // TstxtSearch
            // 
            this.TstxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TstxtSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TstxtSearch.Name = "TstxtSearch";
            this.TstxtSearch.Size = new System.Drawing.Size(150, 28);
            // 
            // GbImShow
            // 
            this.GbImShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbImShow.Controls.Add(this.PClassColor);
            this.GbImShow.Location = new System.Drawing.Point(12, 56);
            this.GbImShow.Name = "GbImShow";
            this.GbImShow.Size = new System.Drawing.Size(776, 776);
            this.GbImShow.TabIndex = 4;
            this.GbImShow.TabStop = false;
            this.GbImShow.Text = "图像显示";
            // 
            // PClassColor
            // 
            this.PClassColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PClassColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PClassColor.Controls.Add(this.Hengshizi);
            this.PClassColor.Controls.Add(this.ShuShizi);
            this.PClassColor.Controls.Add(this.ImbShow);
            this.PClassColor.Location = new System.Drawing.Point(13, 22);
            this.PClassColor.Name = "PClassColor";
            this.PClassColor.Size = new System.Drawing.Size(749, 749);
            this.PClassColor.TabIndex = 2;
            // 
            // Hengshizi
            // 
            this.Hengshizi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hengshizi.BackColor = System.Drawing.Color.Black;
            this.Hengshizi.Location = new System.Drawing.Point(0, 373);
            this.Hengshizi.Name = "Hengshizi";
            this.Hengshizi.Size = new System.Drawing.Size(747, 1);
            this.Hengshizi.TabIndex = 1;
            // 
            // ShuShizi
            // 
            this.ShuShizi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ShuShizi.BackColor = System.Drawing.Color.Black;
            this.ShuShizi.Location = new System.Drawing.Point(374, 0);
            this.ShuShizi.Name = "ShuShizi";
            this.ShuShizi.Size = new System.Drawing.Size(1, 747);
            this.ShuShizi.TabIndex = 1;
            // 
            // ImbShow
            // 
            this.ImbShow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ImbShow.Image = global::SampleIdentificationWFA01.Properties.Resources.驴子;
            this.ImbShow.Location = new System.Drawing.Point(20, 20);
            this.ImbShow.Name = "ImbShow";
            this.ImbShow.Size = new System.Drawing.Size(708, 708);
            this.ImbShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImbShow.TabIndex = 0;
            this.ImbShow.TabStop = false;
            this.ImbShow.Paint += new System.Windows.Forms.PaintEventHandler(this.ImbShow_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RtbSplInfo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TxtClasses);
            this.groupBox2.Controls.Add(this.BtnPrior);
            this.groupBox2.Controls.Add(this.BtnNext);
            this.groupBox2.Controls.Add(this.DgvClasses);
            this.groupBox2.Location = new System.Drawing.Point(799, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 776);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "样本点信息";
            // 
            // RtbSplInfo
            // 
            this.RtbSplInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RtbSplInfo.Location = new System.Drawing.Point(6, 268);
            this.RtbSplInfo.Name = "RtbSplInfo";
            this.RtbSplInfo.Size = new System.Drawing.Size(340, 438);
            this.RtbSplInfo.TabIndex = 7;
            this.RtbSplInfo.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 717);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "类别";
            // 
            // TxtClasses
            // 
            this.TxtClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtClasses.Location = new System.Drawing.Point(63, 712);
            this.TxtClasses.Name = "TxtClasses";
            this.TxtClasses.Size = new System.Drawing.Size(283, 23);
            this.TxtClasses.TabIndex = 5;
            this.TxtClasses.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtClasses_KeyPress);
            // 
            // BtnPrior
            // 
            this.BtnPrior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrior.Location = new System.Drawing.Point(6, 741);
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
            this.BtnNext.Location = new System.Drawing.Point(271, 741);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 844);
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
            // TmrPlay
            // 
            this.TmrPlay.Tick += new System.EventHandler(this.TmrPlay_Tick);
            // 
            // SIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 866);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GbImShow);
            this.Controls.Add(this.TsSplIndf);
            this.Controls.Add(this.MsSplIndf);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MsSplIndf;
            this.Name = "SIMainForm";
            this.Text = "样本解译工具 - v1.0";
            this.MsSplIndf.ResumeLayout(false);
            this.MsSplIndf.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClasses)).EndInit();
            this.TsSplIndf.ResumeLayout(false);
            this.TsSplIndf.PerformLayout();
            this.GbImShow.ResumeLayout(false);
            this.PClassColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImbShow)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MsSplIndf;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.DataGridView DgvClasses;
        private System.Windows.Forms.ToolStrip TsSplIndf;
        private System.Windows.Forms.ToolStripButton TsbtnOpenPrj;
        private System.Windows.Forms.GroupBox GbImShow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripTextBox TstxtSearch;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.PictureBox ImbShow;
        private System.Windows.Forms.ToolStripMenuItem TsmiExportPrj;
        private System.Windows.Forms.ToolStripMenuItem 打开工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Button BtnPrior;
        private System.Windows.Forms.TextBox TxtClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 图像列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TsbtnExportPrj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColClasses;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColClassesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumber;
        private System.Windows.Forms.Panel ShuShizi;
        private System.Windows.Forms.Panel Hengshizi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox RtbSplInfo;
        private System.Windows.Forms.ToolStripButton TsbtnTimerPlay;
        private System.Windows.Forms.Timer TmrPlay;
        private System.Windows.Forms.ToolStripMenuItem tfrecord转工程ToolStripMenuItem;
        private System.Windows.Forms.Panel PClassColor;
    }
}

