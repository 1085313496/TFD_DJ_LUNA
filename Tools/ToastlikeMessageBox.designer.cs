namespace TFD_DJ_LUNA
{
    partial class ToastlikeMessageBox
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
            this.components = new System.ComponentModel.Container();
            this.timer_AutoHide = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_AutoHide
            // 
            this.timer_AutoHide.Interval = 2000;
            this.timer_AutoHide.Tick += new System.EventHandler(this.timer_AutoHide_Tick);
            // 
            // ToastlikeMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(120, 25);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ToastlikeMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToastlikeMessageBox";
            this.Load += new System.EventHandler(this.ToastlikeMessageBox_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ToastlikeMessageBox_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_AutoHide;
    }
}