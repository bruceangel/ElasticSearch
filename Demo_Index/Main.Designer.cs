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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnDeleted1 = new System.Windows.Forms.Button();
            this.btnDeleted2 = new System.Windows.Forms.Button();
            this.btnCreateIndex3 = new System.Windows.Forms.Button();
            this.btnDeleted3 = new System.Windows.Forms.Button();
            this.btnUpdate1 = new System.Windows.Forms.Button();
            this.btnUpdate2 = new System.Windows.Forms.Button();
            this.btnGetID1 = new System.Windows.Forms.Button();
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
            this.btnSearch1.Location = new System.Drawing.Point(32, 168);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(91, 23);
            this.btnSearch1.TabIndex = 0;
            this.btnSearch1.Text = "查询1";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(32, 219);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(519, 191);
            this.txtResult.TabIndex = 1;
            // 
            // btnDeleted1
            // 
            this.btnDeleted1.Location = new System.Drawing.Point(32, 72);
            this.btnDeleted1.Name = "btnDeleted1";
            this.btnDeleted1.Size = new System.Drawing.Size(91, 23);
            this.btnDeleted1.TabIndex = 0;
            this.btnDeleted1.Text = "删除-范围";
            this.btnDeleted1.UseVisualStyleBackColor = true;
            this.btnDeleted1.Click += new System.EventHandler(this.btnDeleted1_Click);
            // 
            // btnDeleted2
            // 
            this.btnDeleted2.Location = new System.Drawing.Point(150, 72);
            this.btnDeleted2.Name = "btnDeleted2";
            this.btnDeleted2.Size = new System.Drawing.Size(91, 23);
            this.btnDeleted2.TabIndex = 0;
            this.btnDeleted2.Text = "删除-索引";
            this.btnDeleted2.UseVisualStyleBackColor = true;
            this.btnDeleted2.Click += new System.EventHandler(this.btnDeleted2_Click);
            // 
            // btnCreateIndex3
            // 
            this.btnCreateIndex3.Location = new System.Drawing.Point(261, 27);
            this.btnCreateIndex3.Name = "btnCreateIndex3";
            this.btnCreateIndex3.Size = new System.Drawing.Size(91, 23);
            this.btnCreateIndex3.TabIndex = 0;
            this.btnCreateIndex3.Text = "只创建索引";
            this.btnCreateIndex3.UseVisualStyleBackColor = true;
            this.btnCreateIndex3.Click += new System.EventHandler(this.btnCreateIndex3_Click);
            // 
            // btnDeleted3
            // 
            this.btnDeleted3.Location = new System.Drawing.Point(261, 72);
            this.btnDeleted3.Name = "btnDeleted3";
            this.btnDeleted3.Size = new System.Drawing.Size(91, 23);
            this.btnDeleted3.TabIndex = 0;
            this.btnDeleted3.Text = "删除-唯一ID";
            this.btnDeleted3.UseVisualStyleBackColor = true;
            this.btnDeleted3.Click += new System.EventHandler(this.btnDeleted3_Click);
            // 
            // btnUpdate1
            // 
            this.btnUpdate1.Location = new System.Drawing.Point(32, 119);
            this.btnUpdate1.Name = "btnUpdate1";
            this.btnUpdate1.Size = new System.Drawing.Size(91, 23);
            this.btnUpdate1.TabIndex = 0;
            this.btnUpdate1.Text = "更新-全部";
            this.btnUpdate1.UseVisualStyleBackColor = true;
            this.btnUpdate1.Click += new System.EventHandler(this.btnUpdate1_Click);
            // 
            // btnUpdate2
            // 
            this.btnUpdate2.Location = new System.Drawing.Point(150, 119);
            this.btnUpdate2.Name = "btnUpdate2";
            this.btnUpdate2.Size = new System.Drawing.Size(91, 23);
            this.btnUpdate2.TabIndex = 0;
            this.btnUpdate2.Text = "更新-部分";
            this.btnUpdate2.UseVisualStyleBackColor = true;
            this.btnUpdate2.Click += new System.EventHandler(this.btnUpdate2_Click);
            // 
            // btnGetID1
            // 
            this.btnGetID1.Location = new System.Drawing.Point(261, 119);
            this.btnGetID1.Name = "btnGetID1";
            this.btnGetID1.Size = new System.Drawing.Size(91, 23);
            this.btnGetID1.TabIndex = 0;
            this.btnGetID1.Text = "根据唯一ID获取";
            this.btnGetID1.UseVisualStyleBackColor = true;
            this.btnGetID1.Click += new System.EventHandler(this.btnGetID1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 422);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnDeleted2);
            this.Controls.Add(this.btnDeleted3);
            this.Controls.Add(this.btnDeleted1);
            this.Controls.Add(this.btnGetID1);
            this.Controls.Add(this.btnUpdate2);
            this.Controls.Add(this.btnUpdate1);
            this.Controls.Add(this.btnSearch1);
            this.Controls.Add(this.btnCreateIndex3);
            this.Controls.Add(this.btnCreateIndex2);
            this.Controls.Add(this.btnCreateIndex1);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateIndex1;
        private System.Windows.Forms.Button btnCreateIndex2;
        private System.Windows.Forms.Button btnSearch1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnDeleted1;
        private System.Windows.Forms.Button btnDeleted2;
        private System.Windows.Forms.Button btnCreateIndex3;
        private System.Windows.Forms.Button btnDeleted3;
        private System.Windows.Forms.Button btnUpdate1;
        private System.Windows.Forms.Button btnUpdate2;
        private System.Windows.Forms.Button btnGetID1;
    }
}

