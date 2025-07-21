namespace TFD_DJ_LUNA
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
            this.components = new System.ComponentModel.Container();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tkb = new System.Windows.Forms.TrackBar();
            this.lb_threshold = new System.Windows.Forms.Label();
            this.rg_SWT_1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rg_SWT_2 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tkb)).BeginInit();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Location = new System.Drawing.Point(12, 81);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(537, 251);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(474, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tkb
            // 
            this.tkb.Location = new System.Drawing.Point(12, 12);
            this.tkb.Maximum = 100;
            this.tkb.Name = "tkb";
            this.tkb.Size = new System.Drawing.Size(418, 45);
            this.tkb.TabIndex = 2;
            this.tkb.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tkb.Value = 56;
            this.tkb.ValueChanged += new System.EventHandler(this.tkb_ValueChanged);
            // 
            // lb_threshold
            // 
            this.lb_threshold.AutoSize = true;
            this.lb_threshold.Location = new System.Drawing.Point(427, 25);
            this.lb_threshold.Name = "lb_threshold";
            this.lb_threshold.Size = new System.Drawing.Size(29, 12);
            this.lb_threshold.TabIndex = 3;
            this.lb_threshold.Text = "0.56";
            // 
            // rg_SWT_1
            // 
            this.rg_SWT_1.AutoSize = true;
            this.rg_SWT_1.Checked = true;
            this.rg_SWT_1.Location = new System.Drawing.Point(86, 60);
            this.rg_SWT_1.Name = "rg_SWT_1";
            this.rg_SWT_1.Size = new System.Drawing.Size(89, 16);
            this.rg_SWT_1.TabIndex = 4;
            this.rg_SWT_1.TabStop = true;
            this.rg_SWT_1.Text = "CVZ交替打点";
            this.rg_SWT_1.UseVisualStyleBackColor = true;
            this.rg_SWT_1.CheckedChanged += new System.EventHandler(this.rg_SWT_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "打点方式：";
            // 
            // rg_SWT_2
            // 
            this.rg_SWT_2.AutoSize = true;
            this.rg_SWT_2.Location = new System.Drawing.Point(181, 60);
            this.rg_SWT_2.Name = "rg_SWT_2";
            this.rg_SWT_2.Size = new System.Drawing.Size(95, 16);
            this.rg_SWT_2.TabIndex = 4;
            this.rg_SWT_2.Text = "按照映射打点";
            this.toolTip1.SetToolTip(this.rg_SWT_2, "图案0按下C 1按下V 2按下Z 3按下C");
            this.rg_SWT_2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 348);
            this.Controls.Add(this.rg_SWT_2);
            this.Controls.Add(this.rg_SWT_1);
            this.Controls.Add(this.lb_threshold);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.tkb);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tkb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar tkb;
        private System.Windows.Forms.Label lb_threshold;
        private System.Windows.Forms.RadioButton rg_SWT_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rg_SWT_2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

