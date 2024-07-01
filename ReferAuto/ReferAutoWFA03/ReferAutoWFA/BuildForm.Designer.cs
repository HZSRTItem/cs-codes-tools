
namespace ReferAutoWFA
{
    partial class BuildForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPrjFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtRefListFile = new System.Windows.Forms.TextBox();
            this.BtnPrjFile = new System.Windows.Forms.Button();
            this.BtnRefListFile = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文献项目文件";
            // 
            // TxtPrjFile
            // 
            this.TxtPrjFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrjFile.Location = new System.Drawing.Point(99, 60);
            this.TxtPrjFile.Name = "TxtPrjFile";
            this.TxtPrjFile.Size = new System.Drawing.Size(432, 21);
            this.TxtPrjFile.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "参考文献列表";
            // 
            // TxtRefListFile
            // 
            this.TxtRefListFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRefListFile.Location = new System.Drawing.Point(99, 33);
            this.TxtRefListFile.Name = "TxtRefListFile";
            this.TxtRefListFile.Size = new System.Drawing.Size(432, 21);
            this.TxtRefListFile.TabIndex = 1;
            // 
            // BtnPrjFile
            // 
            this.BtnPrjFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrjFile.Location = new System.Drawing.Point(538, 57);
            this.BtnPrjFile.Name = "BtnPrjFile";
            this.BtnPrjFile.Size = new System.Drawing.Size(40, 23);
            this.BtnPrjFile.TabIndex = 3;
            this.BtnPrjFile.Text = "...";
            this.BtnPrjFile.UseVisualStyleBackColor = true;
            this.BtnPrjFile.Click += new System.EventHandler(this.BtnPrjFile_Click);
            // 
            // BtnRefListFile
            // 
            this.BtnRefListFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRefListFile.Location = new System.Drawing.Point(537, 31);
            this.BtnRefListFile.Name = "BtnRefListFile";
            this.BtnRefListFile.Size = new System.Drawing.Size(40, 23);
            this.BtnRefListFile.TabIndex = 3;
            this.BtnRefListFile.Text = "...";
            this.BtnRefListFile.UseVisualStyleBackColor = true;
            this.BtnRefListFile.Click += new System.EventHandler(this.BtnRefListFile_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(99, 100);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(88, 23);
            this.BtnOk.TabIndex = 3;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(443, 100);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(88, 23);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnRemAll_Click);
            // 
            // BuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 135);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnRefListFile);
            this.Controls.Add(this.BtnPrjFile);
            this.Controls.Add(this.TxtRefListFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtPrjFile);
            this.Controls.Add(this.label1);
            this.Name = "BuildForm";
            this.Text = "BuildForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPrjFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtRefListFile;
        private System.Windows.Forms.Button BtnPrjFile;
        private System.Windows.Forms.Button BtnRefListFile;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
    }
}