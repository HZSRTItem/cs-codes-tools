
namespace ShowPointWFA
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.Sc0 = new System.Windows.Forms.SplitContainer();
            this.Sc1 = new System.Windows.Forms.SplitContainer();
            this.ChartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ClbCategory = new System.Windows.Forms.CheckedListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc0)).BeginInit();
            this.Sc0.Panel1.SuspendLayout();
            this.Sc0.Panel2.SuspendLayout();
            this.Sc0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc1)).BeginInit();
            this.Sc1.Panel1.SuspendLayout();
            this.Sc1.Panel2.SuspendLayout();
            this.Sc1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1023, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1023, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "TsbtnDraw";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Sc0
            // 
            this.Sc0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sc0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc0.Location = new System.Drawing.Point(0, 50);
            this.Sc0.Name = "Sc0";
            // 
            // Sc0.Panel1
            // 
            this.Sc0.Panel1.Controls.Add(this.ChartMain);
            // 
            // Sc0.Panel2
            // 
            this.Sc0.Panel2.Controls.Add(this.Sc1);
            this.Sc0.Size = new System.Drawing.Size(1023, 665);
            this.Sc0.SplitterDistance = 736;
            this.Sc0.TabIndex = 2;
            // 
            // Sc1
            // 
            this.Sc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc1.Location = new System.Drawing.Point(0, 0);
            this.Sc1.Name = "Sc1";
            this.Sc1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Sc1.Panel1
            // 
            this.Sc1.Panel1.Controls.Add(this.ClbCategory);
            // 
            // Sc1.Panel2
            // 
            this.Sc1.Panel2.Controls.Add(this.richTextBox1);
            this.Sc1.Size = new System.Drawing.Size(283, 665);
            this.Sc1.SplitterDistance = 349;
            this.Sc1.TabIndex = 0;
            // 
            // ChartMain
            // 
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.ChartMain.ChartAreas.Add(chartArea1);
            this.ChartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.ChartMain.Legends.Add(legend1);
            this.ChartMain.Location = new System.Drawing.Point(0, 0);
            this.ChartMain.Name = "ChartMain";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Black;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            this.ChartMain.Series.Add(series1);
            this.ChartMain.Size = new System.Drawing.Size(734, 663);
            this.ChartMain.TabIndex = 0;
            this.ChartMain.Text = "chart1";
            // 
            // ClbCategory
            // 
            this.ClbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClbCategory.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClbCategory.FormattingEnabled = true;
            this.ClbCategory.Items.AddRange(new object[] {
            "t01"});
            this.ClbCategory.Location = new System.Drawing.Point(0, 0);
            this.ClbCategory.Name = "ClbCategory";
            this.ClbCategory.Size = new System.Drawing.Size(281, 347);
            this.ClbCategory.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(281, 310);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 715);
            this.Controls.Add(this.Sc0);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "散点图";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Sc0.Panel1.ResumeLayout(false);
            this.Sc0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sc0)).EndInit();
            this.Sc0.ResumeLayout(false);
            this.Sc1.Panel1.ResumeLayout(false);
            this.Sc1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sc1)).EndInit();
            this.Sc1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer Sc0;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartMain;
        private System.Windows.Forms.SplitContainer Sc1;
        private System.Windows.Forms.CheckedListBox ClbCategory;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

