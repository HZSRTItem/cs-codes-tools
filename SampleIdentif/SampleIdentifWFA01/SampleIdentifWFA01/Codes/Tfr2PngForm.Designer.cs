
namespace SampleIdentifWFA01
{
    partial class Tfr2PngForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.DgvTfrFiles = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnTfrFilesAdd = new System.Windows.Forms.Button();
            this.BtnTfrFilesDelete = new System.Windows.Forms.Button();
            this.BtnTfrFilesUp = new System.Windows.Forms.Button();
            this.BtnTfrFilesDown = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DgvTfrInfo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CbRName = new System.Windows.Forms.ComboBox();
            this.CbGName = new System.Windows.Forms.ComboBox();
            this.CbBName = new System.Windows.Forms.ComboBox();
            this.TxtRMin = new System.Windows.Forms.TextBox();
            this.TxtRMax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtGMin = new System.Windows.Forms.TextBox();
            this.TxtGMax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtBMin = new System.Windows.Forms.TextBox();
            this.TxtBMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtImageSize = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CbSrt = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.RtbRun = new System.Windows.Forms.RichTextBox();
            this.BtnRemoveAll = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtSaveDir = new System.Windows.Forms.TextBox();
            this.BtnSaveDir = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTfrFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTfrInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "TFR文件";
            // 
            // DgvTfrFiles
            // 
            this.DgvTfrFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvTfrFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTfrFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.DgvTfrFiles.Location = new System.Drawing.Point(12, 59);
            this.DgvTfrFiles.Name = "DgvTfrFiles";
            this.DgvTfrFiles.RowTemplate.Height = 23;
            this.DgvTfrFiles.Size = new System.Drawing.Size(605, 202);
            this.DgvTfrFiles.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "TFR文件";
            this.Column1.Name = "Column1";
            this.Column1.Width = 500;
            // 
            // BtnTfrFilesAdd
            // 
            this.BtnTfrFilesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTfrFilesAdd.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnTfrFilesAdd.Location = new System.Drawing.Point(623, 56);
            this.BtnTfrFilesAdd.Name = "BtnTfrFilesAdd";
            this.BtnTfrFilesAdd.Size = new System.Drawing.Size(35, 35);
            this.BtnTfrFilesAdd.TabIndex = 3;
            this.BtnTfrFilesAdd.Text = "+";
            this.BtnTfrFilesAdd.UseVisualStyleBackColor = true;
            this.BtnTfrFilesAdd.Click += new System.EventHandler(this.BtnTfrFilesAdd_Click);
            // 
            // BtnTfrFilesDelete
            // 
            this.BtnTfrFilesDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTfrFilesDelete.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnTfrFilesDelete.Location = new System.Drawing.Point(623, 94);
            this.BtnTfrFilesDelete.Name = "BtnTfrFilesDelete";
            this.BtnTfrFilesDelete.Size = new System.Drawing.Size(35, 35);
            this.BtnTfrFilesDelete.TabIndex = 3;
            this.BtnTfrFilesDelete.Text = "-";
            this.BtnTfrFilesDelete.UseVisualStyleBackColor = true;
            this.BtnTfrFilesDelete.Click += new System.EventHandler(this.BtnTfrFilesDelete_Click);
            // 
            // BtnTfrFilesUp
            // 
            this.BtnTfrFilesUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTfrFilesUp.Location = new System.Drawing.Point(623, 132);
            this.BtnTfrFilesUp.Name = "BtnTfrFilesUp";
            this.BtnTfrFilesUp.Size = new System.Drawing.Size(35, 35);
            this.BtnTfrFilesUp.TabIndex = 3;
            this.BtnTfrFilesUp.Text = "上";
            this.BtnTfrFilesUp.UseVisualStyleBackColor = true;
            this.BtnTfrFilesUp.Click += new System.EventHandler(this.BtnTfrFilesUp_Click);
            // 
            // BtnTfrFilesDown
            // 
            this.BtnTfrFilesDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTfrFilesDown.Location = new System.Drawing.Point(623, 170);
            this.BtnTfrFilesDown.Name = "BtnTfrFilesDown";
            this.BtnTfrFilesDown.Size = new System.Drawing.Size(35, 35);
            this.BtnTfrFilesDown.TabIndex = 3;
            this.BtnTfrFilesDown.Text = "下";
            this.BtnTfrFilesDown.UseVisualStyleBackColor = true;
            this.BtnTfrFilesDown.Click += new System.EventHandler(this.BtnTfrFilesDown_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(12, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "TFR文件";
            // 
            // DgvTfrInfo
            // 
            this.DgvTfrInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvTfrInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTfrInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DgvTfrInfo.Location = new System.Drawing.Point(12, 282);
            this.DgvTfrInfo.Name = "DgvTfrInfo";
            this.DgvTfrInfo.RowTemplate.Height = 23;
            this.DgvTfrInfo.Size = new System.Drawing.Size(646, 195);
            this.DgvTfrInfo.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 60;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(12, 515);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "R 波段（编号或名称）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(12, 541);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "G 波段（编号或名称）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(12, 567);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "B 波段（编号或名称）";
            // 
            // CbRName
            // 
            this.CbRName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbRName.FormattingEnabled = true;
            this.CbRName.Location = new System.Drawing.Point(176, 512);
            this.CbRName.Name = "CbRName";
            this.CbRName.Size = new System.Drawing.Size(207, 20);
            this.CbRName.TabIndex = 4;
            // 
            // CbGName
            // 
            this.CbGName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbGName.FormattingEnabled = true;
            this.CbGName.Location = new System.Drawing.Point(176, 538);
            this.CbGName.Name = "CbGName";
            this.CbGName.Size = new System.Drawing.Size(207, 20);
            this.CbGName.TabIndex = 4;
            // 
            // CbBName
            // 
            this.CbBName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbBName.FormattingEnabled = true;
            this.CbBName.Location = new System.Drawing.Point(176, 564);
            this.CbBName.Name = "CbBName";
            this.CbBName.Size = new System.Drawing.Size(207, 20);
            this.CbBName.TabIndex = 4;
            // 
            // TxtRMin
            // 
            this.TxtRMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRMin.Location = new System.Drawing.Point(418, 511);
            this.TxtRMin.Name = "TxtRMin";
            this.TxtRMin.Size = new System.Drawing.Size(100, 21);
            this.TxtRMin.TabIndex = 5;
            this.TxtRMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtRMax
            // 
            this.TxtRMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRMax.Location = new System.Drawing.Point(552, 564);
            this.TxtRMax.Name = "TxtRMax";
            this.TxtRMax.Size = new System.Drawing.Size(106, 21);
            this.TxtRMax.TabIndex = 5;
            this.TxtRMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(389, 515);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "min";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 570);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "max";
            // 
            // TxtGMin
            // 
            this.TxtGMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtGMin.Location = new System.Drawing.Point(417, 537);
            this.TxtGMin.Name = "TxtGMin";
            this.TxtGMin.Size = new System.Drawing.Size(100, 21);
            this.TxtGMin.TabIndex = 5;
            this.TxtGMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtGMax
            // 
            this.TxtGMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtGMax.Location = new System.Drawing.Point(552, 512);
            this.TxtGMax.Name = "TxtGMax";
            this.TxtGMax.Size = new System.Drawing.Size(107, 21);
            this.TxtGMax.TabIndex = 5;
            this.TxtGMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(388, 541);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "min";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(523, 519);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "max";
            // 
            // TxtBMin
            // 
            this.TxtBMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBMin.Location = new System.Drawing.Point(417, 563);
            this.TxtBMin.Name = "TxtBMin";
            this.TxtBMin.Size = new System.Drawing.Size(100, 21);
            this.TxtBMin.TabIndex = 5;
            this.TxtBMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtBMax
            // 
            this.TxtBMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBMax.Location = new System.Drawing.Point(552, 538);
            this.TxtBMax.Name = "TxtBMax";
            this.TxtBMax.Size = new System.Drawing.Size(107, 21);
            this.TxtBMax.TabIndex = 5;
            this.TxtBMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(388, 567);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "min";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(523, 545);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "max";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 11F);
            this.label12.Location = new System.Drawing.Point(11, 485);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(259, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "图像大小 `int,int`: width,height";
            // 
            // TxtImageSize
            // 
            this.TxtImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtImageSize.Location = new System.Drawing.Point(276, 485);
            this.TxtImageSize.Name = "TxtImageSize";
            this.TxtImageSize.Size = new System.Drawing.Size(383, 21);
            this.TxtImageSize.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11F);
            this.label13.Location = new System.Drawing.Point(12, 596);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 15);
            this.label13.TabIndex = 1;
            this.label13.Text = "唯一标识符";
            // 
            // CbSrt
            // 
            this.CbSrt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbSrt.FormattingEnabled = true;
            this.CbSrt.Location = new System.Drawing.Point(100, 594);
            this.CbSrt.Name = "CbSrt";
            this.CbSrt.Size = new System.Drawing.Size(559, 20);
            this.CbSrt.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11F);
            this.label14.Location = new System.Drawing.Point(13, 647);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 15);
            this.label14.TabIndex = 1;
            this.label14.Text = "运行";
            // 
            // RtbRun
            // 
            this.RtbRun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RtbRun.Location = new System.Drawing.Point(12, 665);
            this.RtbRun.Name = "RtbRun";
            this.RtbRun.Size = new System.Drawing.Size(647, 87);
            this.RtbRun.TabIndex = 7;
            this.RtbRun.Text = "";
            // 
            // BtnRemoveAll
            // 
            this.BtnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRemoveAll.Location = new System.Drawing.Point(583, 758);
            this.BtnRemoveAll.Name = "BtnRemoveAll";
            this.BtnRemoveAll.Size = new System.Drawing.Size(75, 23);
            this.BtnRemoveAll.TabIndex = 8;
            this.BtnRemoveAll.Text = "清空";
            this.BtnRemoveAll.UseVisualStyleBackColor = true;
            this.BtnRemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(502, 758);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 8;
            this.BtnOK.Text = "确定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11F);
            this.label15.Location = new System.Drawing.Point(11, 622);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 15);
            this.label15.TabIndex = 1;
            this.label15.Text = "保存文件夹";
            // 
            // TxtSaveDir
            // 
            this.TxtSaveDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSaveDir.Location = new System.Drawing.Point(99, 622);
            this.TxtSaveDir.Name = "TxtSaveDir";
            this.TxtSaveDir.Size = new System.Drawing.Size(518, 21);
            this.TxtSaveDir.TabIndex = 5;
            // 
            // BtnSaveDir
            // 
            this.BtnSaveDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaveDir.Location = new System.Drawing.Point(625, 622);
            this.BtnSaveDir.Name = "BtnSaveDir";
            this.BtnSaveDir.Size = new System.Drawing.Size(34, 21);
            this.BtnSaveDir.TabIndex = 9;
            this.BtnSaveDir.Text = "...";
            this.BtnSaveDir.UseVisualStyleBackColor = true;
            this.BtnSaveDir.Click += new System.EventHandler(this.BtnSaveDir_Click);
            // 
            // Tfr2PngForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 787);
            this.Controls.Add(this.BtnSaveDir);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnRemoveAll);
            this.Controls.Add(this.RtbRun);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtBMax);
            this.Controls.Add(this.TxtBMin);
            this.Controls.Add(this.TxtGMax);
            this.Controls.Add(this.TxtGMin);
            this.Controls.Add(this.TxtRMax);
            this.Controls.Add(this.TxtSaveDir);
            this.Controls.Add(this.TxtImageSize);
            this.Controls.Add(this.TxtRMin);
            this.Controls.Add(this.CbBName);
            this.Controls.Add(this.CbGName);
            this.Controls.Add(this.CbSrt);
            this.Controls.Add(this.CbRName);
            this.Controls.Add(this.BtnTfrFilesDown);
            this.Controls.Add(this.BtnTfrFilesUp);
            this.Controls.Add(this.BtnTfrFilesDelete);
            this.Controls.Add(this.BtnTfrFilesAdd);
            this.Controls.Add(this.DgvTfrInfo);
            this.Controls.Add(this.DgvTfrFiles);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tfr2PngForm";
            this.Text = "Tfreocrd文件转Png图片";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTfrFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTfrInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DgvTfrFiles;
        private System.Windows.Forms.Button BtnTfrFilesAdd;
        private System.Windows.Forms.Button BtnTfrFilesDelete;
        private System.Windows.Forms.Button BtnTfrFilesUp;
        private System.Windows.Forms.Button BtnTfrFilesDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DgvTfrInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CbRName;
        private System.Windows.Forms.ComboBox CbGName;
        private System.Windows.Forms.ComboBox CbBName;
        private System.Windows.Forms.TextBox TxtRMin;
        private System.Windows.Forms.TextBox TxtRMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtGMin;
        private System.Windows.Forms.TextBox TxtGMax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtBMin;
        private System.Windows.Forms.TextBox TxtBMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TxtImageSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox CbSrt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox RtbRun;
        private System.Windows.Forms.Button BtnRemoveAll;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TxtSaveDir;
        private System.Windows.Forms.Button BtnSaveDir;
    }
}