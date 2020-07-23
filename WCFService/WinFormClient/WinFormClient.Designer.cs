namespace WinFormClient
{
    partial class WinFormClient
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTestServerError = new System.Windows.Forms.Button();
            this.btnXL = new System.Windows.Forms.Button();
            this.btnSingleAutoCloseAbortProxy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTestServerError);
            this.flowLayoutPanel1.Controls.Add(this.btnXL);
            this.flowLayoutPanel1.Controls.Add(this.btnSingleAutoCloseAbortProxy);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(678, 405);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnTestServerError
            // 
            this.btnTestServerError.Location = new System.Drawing.Point(3, 3);
            this.btnTestServerError.Name = "btnTestServerError";
            this.btnTestServerError.Size = new System.Drawing.Size(180, 47);
            this.btnTestServerError.TabIndex = 0;
            this.btnTestServerError.Text = "服务异常未处理";
            this.btnTestServerError.UseVisualStyleBackColor = true;
            this.btnTestServerError.Click += new System.EventHandler(this.btnTestServerError_Click);
            // 
            // btnXL
            // 
            this.btnXL.Location = new System.Drawing.Point(189, 3);
            this.btnXL.Name = "btnXL";
            this.btnXL.Size = new System.Drawing.Size(153, 47);
            this.btnXL.TabIndex = 0;
            this.btnXL.Text = "限流";
            this.btnXL.UseVisualStyleBackColor = true;
            this.btnXL.Click += new System.EventHandler(this.btnXL_Click);
            // 
            // btnSingleAutoCloseAbortProxy
            // 
            this.btnSingleAutoCloseAbortProxy.Location = new System.Drawing.Point(348, 3);
            this.btnSingleAutoCloseAbortProxy.Name = "btnSingleAutoCloseAbortProxy";
            this.btnSingleAutoCloseAbortProxy.Size = new System.Drawing.Size(153, 47);
            this.btnSingleAutoCloseAbortProxy.TabIndex = 0;
            this.btnSingleAutoCloseAbortProxy.Text = "自动关闭和中断的代理";
            this.btnSingleAutoCloseAbortProxy.UseVisualStyleBackColor = true;
            this.btnSingleAutoCloseAbortProxy.Click += new System.EventHandler(this.btnSingleAutoCloseAbortProxy_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(507, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "ces";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WinFormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 405);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "WinFormClient";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnTestServerError;
        private System.Windows.Forms.Button btnXL;
        private System.Windows.Forms.Button btnSingleAutoCloseAbortProxy;
        private System.Windows.Forms.Button button1;
    }
}

