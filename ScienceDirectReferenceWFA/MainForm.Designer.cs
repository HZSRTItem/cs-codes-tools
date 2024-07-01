
namespace ScienceDirectReferenceWFA
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
            this.BtnQK = new System.Windows.Forms.Button();
            this.BtnCal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtJsonSavePath = new System.Windows.Forms.TextBox();
            this.BtnSavePath = new System.Windows.Forms.Button();
            this.TxtRefPath = new System.Windows.Forms.TextBox();
            this.BtnRefPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnQK
            // 
            this.BtnQK.Location = new System.Drawing.Point(80, 78);
            this.BtnQK.Name = "BtnQK";
            this.BtnQK.Size = new System.Drawing.Size(75, 23);
            this.BtnQK.TabIndex = 15;
            this.BtnQK.Text = "清空";
            this.BtnQK.UseVisualStyleBackColor = true;
            this.BtnQK.Click += new System.EventHandler(this.BtnQK_Click);
            // 
            // BtnCal
            // 
            this.BtnCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCal.Location = new System.Drawing.Point(366, 78);
            this.BtnCal.Name = "BtnCal";
            this.BtnCal.Size = new System.Drawing.Size(75, 22);
            this.BtnCal.TabIndex = 14;
            this.BtnCal.Text = "计算";
            this.BtnCal.UseVisualStyleBackColor = true;
            this.BtnCal.Click += new System.EventHandler(this.BtnCal_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "json";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "参考文献";
            // 
            // JsonSavePath
            // 
            this.TxtJsonSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtJsonSavePath.Location = new System.Drawing.Point(80, 51);
            this.TxtJsonSavePath.Name = "JsonSavePath";
            this.TxtJsonSavePath.Size = new System.Drawing.Size(361, 21);
            this.TxtJsonSavePath.TabIndex = 8;
            // 
            // BtnSavePath
            // 
            this.BtnSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSavePath.Location = new System.Drawing.Point(447, 49);
            this.BtnSavePath.Name = "BtnSavePath";
            this.BtnSavePath.Size = new System.Drawing.Size(33, 23);
            this.BtnSavePath.TabIndex = 5;
            this.BtnSavePath.Text = "...";
            this.BtnSavePath.UseVisualStyleBackColor = true;
            this.BtnSavePath.Click += new System.EventHandler(this.BtnSavePath_Click);
            // 
            // TxtRefPath
            // 
            this.TxtRefPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRefPath.Location = new System.Drawing.Point(80, 24);
            this.TxtRefPath.Name = "TxtRefPath";
            this.TxtRefPath.Size = new System.Drawing.Size(361, 21);
            this.TxtRefPath.TabIndex = 10;
            // 
            // BtnRefPath
            // 
            this.BtnRefPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRefPath.Location = new System.Drawing.Point(447, 22);
            this.BtnRefPath.Name = "BtnRefPath";
            this.BtnRefPath.Size = new System.Drawing.Size(33, 23);
            this.BtnRefPath.TabIndex = 7;
            this.BtnRefPath.Text = "...";
            this.BtnRefPath.UseVisualStyleBackColor = true;
            this.BtnRefPath.Click += new System.EventHandler(this.BtnRefPath_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 115);
            this.Controls.Add(this.BtnQK);
            this.Controls.Add(this.BtnCal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtJsonSavePath);
            this.Controls.Add(this.BtnSavePath);
            this.Controls.Add(this.TxtRefPath);
            this.Controls.Add(this.BtnRefPath);
            this.Name = "MainForm";
            this.Text = "Science Direct 参考文献";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnQK;
        private System.Windows.Forms.Button BtnCal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtJsonSavePath;
        private System.Windows.Forms.Button BtnSavePath;
        private System.Windows.Forms.TextBox TxtRefPath;
        private System.Windows.Forms.Button BtnRefPath;
    }
}

