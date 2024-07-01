
namespace QgisJYBuildWFA
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
            this.打开CSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbtnExportQinfoFile = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DgvCategoryInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CbSRTField = new System.Windows.Forms.ComboBox();
            this.CbCategoryField = new System.Windows.Forms.ComboBox();
            this.CbLField = new System.Windows.Forms.ComboBox();
            this.CbBField = new System.Windows.Forms.ComboBox();
            this.BtnAddAll = new System.Windows.Forms.Button();
            this.BtnCDelete = new System.Windows.Forms.Button();
            this.BtnCAdd = new System.Windows.Forms.Button();
            this.BtnCDown = new System.Windows.Forms.Button();
            this.BtnOpenQJYFile = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnCUp = new System.Windows.Forms.Button();
            this.BtnOpenCsvFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtQJYFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCsvFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DgvRecords = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCategoryInfo)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvRecords)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(512, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开CSVToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开CSVToolStripMenuItem
            // 
            this.打开CSVToolStripMenuItem.Name = "打开CSVToolStripMenuItem";
            this.打开CSVToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.打开CSVToolStripMenuItem.Text = "打开CSV";
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(512, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnExportQinfoFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(512, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbtnExportQinfoFile
            // 
            this.TsbtnExportQinfoFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbtnExportQinfoFile.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnExportQinfoFile.Image")));
            this.TsbtnExportQinfoFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnExportQinfoFile.Name = "TsbtnExportQinfoFile";
            this.TsbtnExportQinfoFile.Size = new System.Drawing.Size(23, 22);
            this.TsbtnExportQinfoFile.Text = "toolStripButton1";
            this.TsbtnExportQinfoFile.Click += new System.EventHandler(this.TsbtnExportQinfoFile_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 514);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DgvCategoryInfo);
            this.tabPage1.Controls.Add(this.CbSRTField);
            this.tabPage1.Controls.Add(this.CbCategoryField);
            this.tabPage1.Controls.Add(this.CbLField);
            this.tabPage1.Controls.Add(this.CbBField);
            this.tabPage1.Controls.Add(this.BtnAddAll);
            this.tabPage1.Controls.Add(this.BtnCDelete);
            this.tabPage1.Controls.Add(this.BtnCAdd);
            this.tabPage1.Controls.Add(this.BtnCDown);
            this.tabPage1.Controls.Add(this.BtnOpenQJYFile);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.BtnCUp);
            this.tabPage1.Controls.Add(this.BtnOpenCsvFile);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.TxtQJYFile);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.TxtCsvFile);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "导出信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DgvCategoryInfo
            // 
            this.DgvCategoryInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvCategoryInfo.BackgroundColor = System.Drawing.Color.White;
            this.DgvCategoryInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCategoryInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column3});
            this.DgvCategoryInfo.Location = new System.Drawing.Point(10, 297);
            this.DgvCategoryInfo.Name = "DgvCategoryInfo";
            this.DgvCategoryInfo.RowTemplate.Height = 23;
            this.DgvCategoryInfo.Size = new System.Drawing.Size(449, 185);
            this.DgvCategoryInfo.TabIndex = 4;
            this.DgvCategoryInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCategoryInfo_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "  ";
            this.Column2.Name = "Column2";
            this.Column2.Width = 20;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "数量";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "类别名";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // CbSRTField
            // 
            this.CbSRTField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbSRTField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbSRTField.FormattingEnabled = true;
            this.CbSRTField.Location = new System.Drawing.Point(10, 254);
            this.CbSRTField.Name = "CbSRTField";
            this.CbSRTField.Size = new System.Drawing.Size(486, 20);
            this.CbSRTField.TabIndex = 3;
            // 
            // CbCategoryField
            // 
            this.CbCategoryField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbCategoryField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbCategoryField.FormattingEnabled = true;
            this.CbCategoryField.Location = new System.Drawing.Point(10, 210);
            this.CbCategoryField.Name = "CbCategoryField";
            this.CbCategoryField.Size = new System.Drawing.Size(486, 20);
            this.CbCategoryField.TabIndex = 3;
            // 
            // CbLField
            // 
            this.CbLField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbLField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbLField.FormattingEnabled = true;
            this.CbLField.Location = new System.Drawing.Point(10, 166);
            this.CbLField.Name = "CbLField";
            this.CbLField.Size = new System.Drawing.Size(486, 20);
            this.CbLField.TabIndex = 3;
            // 
            // CbBField
            // 
            this.CbBField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbBField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbBField.FormattingEnabled = true;
            this.CbBField.Location = new System.Drawing.Point(10, 122);
            this.CbBField.Name = "CbBField";
            this.CbBField.Size = new System.Drawing.Size(486, 20);
            this.CbBField.TabIndex = 3;
            // 
            // BtnAddAll
            // 
            this.BtnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddAll.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnAddAll.Location = new System.Drawing.Point(465, 441);
            this.BtnAddAll.Name = "BtnAddAll";
            this.BtnAddAll.Size = new System.Drawing.Size(30, 30);
            this.BtnAddAll.TabIndex = 2;
            this.BtnAddAll.Text = "Add";
            this.BtnAddAll.UseVisualStyleBackColor = true;
            this.BtnAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            // 
            // BtnCDelete
            // 
            this.BtnCDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCDelete.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCDelete.Location = new System.Drawing.Point(465, 405);
            this.BtnCDelete.Name = "BtnCDelete";
            this.BtnCDelete.Size = new System.Drawing.Size(30, 30);
            this.BtnCDelete.TabIndex = 2;
            this.BtnCDelete.Text = "-";
            this.BtnCDelete.UseVisualStyleBackColor = true;
            // 
            // BtnCAdd
            // 
            this.BtnCAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCAdd.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCAdd.Location = new System.Drawing.Point(465, 369);
            this.BtnCAdd.Name = "BtnCAdd";
            this.BtnCAdd.Size = new System.Drawing.Size(30, 30);
            this.BtnCAdd.TabIndex = 2;
            this.BtnCAdd.Text = "+";
            this.BtnCAdd.UseVisualStyleBackColor = true;
            // 
            // BtnCDown
            // 
            this.BtnCDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCDown.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCDown.Location = new System.Drawing.Point(465, 333);
            this.BtnCDown.Name = "BtnCDown";
            this.BtnCDown.Size = new System.Drawing.Size(30, 30);
            this.BtnCDown.TabIndex = 2;
            this.BtnCDown.Text = "↓";
            this.BtnCDown.UseVisualStyleBackColor = true;
            this.BtnCDown.Click += new System.EventHandler(this.BtnCDown_Click);
            // 
            // BtnOpenQJYFile
            // 
            this.BtnOpenQJYFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOpenQJYFile.Location = new System.Drawing.Point(463, 75);
            this.BtnOpenQJYFile.Name = "BtnOpenQJYFile";
            this.BtnOpenQJYFile.Size = new System.Drawing.Size(33, 23);
            this.BtnOpenQJYFile.TabIndex = 2;
            this.BtnOpenQJYFile.Text = "...";
            this.BtnOpenQJYFile.UseVisualStyleBackColor = true;
            this.BtnOpenQJYFile.Click += new System.EventHandler(this.BtnOpenQJYFile_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "类别信息";
            this.label7.Click += new System.EventHandler(this.label1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "SRT(唯一标识符) 字段";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Category(类别) 字段";
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // BtnCUp
            // 
            this.BtnCUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCUp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCUp.Location = new System.Drawing.Point(465, 297);
            this.BtnCUp.Name = "BtnCUp";
            this.BtnCUp.Size = new System.Drawing.Size(30, 30);
            this.BtnCUp.TabIndex = 2;
            this.BtnCUp.Text = "↑";
            this.BtnCUp.UseVisualStyleBackColor = true;
            this.BtnCUp.Click += new System.EventHandler(this.BtnCUp_Click);
            // 
            // BtnOpenCsvFile
            // 
            this.BtnOpenCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOpenCsvFile.Location = new System.Drawing.Point(463, 31);
            this.BtnOpenCsvFile.Name = "BtnOpenCsvFile";
            this.BtnOpenCsvFile.Size = new System.Drawing.Size(33, 23);
            this.BtnOpenCsvFile.TabIndex = 2;
            this.BtnOpenCsvFile.Text = "...";
            this.BtnOpenCsvFile.UseVisualStyleBackColor = true;
            this.BtnOpenCsvFile.Click += new System.EventHandler(this.BtnOpenCsvFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Longitude(经度) 字段";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // TxtQJYFile
            // 
            this.TxtQJYFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtQJYFile.Location = new System.Drawing.Point(10, 77);
            this.TxtQJYFile.Name = "TxtQJYFile";
            this.TxtQJYFile.Size = new System.Drawing.Size(449, 21);
            this.TxtQJYFile.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Latitude(纬度) 字段";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "QJY 文件";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // TxtCsvFile
            // 
            this.TxtCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtCsvFile.Location = new System.Drawing.Point(10, 32);
            this.TxtCsvFile.Name = "TxtCsvFile";
            this.TxtCsvFile.Size = new System.Drawing.Size(449, 21);
            this.TxtCsvFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSV 文件";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DgvRecords);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(487, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "表格展示";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DgvRecords
            // 
            this.DgvRecords.BackgroundColor = System.Drawing.Color.White;
            this.DgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvRecords.Location = new System.Drawing.Point(3, 3);
            this.DgvRecords.Name = "DgvRecords";
            this.DgvRecords.RowTemplate.Height = 23;
            this.DgvRecords.Size = new System.Drawing.Size(481, 484);
            this.DgvRecords.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 586);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Qgis 解译 .v1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCategoryInfo)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开CSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbtnExportQinfoFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BtnOpenCsvFile;
        private System.Windows.Forms.TextBox TxtCsvFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnOpenQJYFile;
        private System.Windows.Forms.TextBox TxtQJYFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbBField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CbLField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CbCategoryField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CbSRTField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView DgvCategoryInfo;
        private System.Windows.Forms.Button BtnCDown;
        private System.Windows.Forms.Button BtnCUp;
        private System.Windows.Forms.Button BtnCAdd;
        private System.Windows.Forms.Button BtnCDelete;
        private System.Windows.Forms.DataGridView DgvRecords;
        private System.Windows.Forms.Button BtnAddAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}

