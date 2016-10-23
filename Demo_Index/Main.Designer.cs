namespace Demo_Index
{
    partial class Main
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
            this.btnCreateIndex1 = new System.Windows.Forms.Button();
            this.btnCreateIndex2 = new System.Windows.Forms.Button();
            this.btnSearch1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateIndex1
            // 
            this.btnCreateIndex1.Location = new System.Drawing.Point(32, 27);
            this.btnCreateIndex1.Name = "btnCreateIndex1";
            this.btnCreateIndex1.Size = new System.Drawing.Size(91, 23);
            this.btnCreateIndex1.TabIndex = 0;
            this.btnCreateIndex1.Text = "创建索引-单";
            this.btnCreateIndex1.UseVisualStyleBackColor = true;
            this.btnCreateIndex1.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // btnCreateIndex2
            // 
            this.btnCreateIndex2.Location = new System.Drawing.Point(150, 27);
            this.btnCreateIndex2.Name = "btnCreateIndex2";
            this.btnCreateIndex2.Size = new System.Drawing.Size(91, 23);
            this.btnCreateIndex2.TabIndex = 0;
            this.btnCreateIndex2.Text = "创建索引-多";
            this.btnCreateIndex2.UseVisualStyleBackColor = true;
            this.btnCreateIndex2.Click += new System.EventHandler(this.btnCreateIndex2_Click);
            // 
            // btnSearch1
            // 
            this.btnSearch1.Location = new System.Drawing.Point(32, 102);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(91, 23);
            this.btnSearch1.TabIndex = 0;
            this.btnSearch1.Text = "查询1";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 338);
            this.Controls.Add(this.btnSearch1);
            this.Controls.Add(this.btnCreateIndex2);
            this.Controls.Add(this.btnCreateIndex1);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateIndex1;
        private System.Windows.Forms.Button btnCreateIndex2;
        private System.Windows.Forms.Button btnSearch1;
    }
}

