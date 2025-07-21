namespace TFD_DJ_LUNA
{
    partial class frm_PickArea
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
            this.SuspendLayout();
            // 
            // frm_PickArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_PickArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_SelectArea";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_PickArea_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frm_PickArea_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frm_PickArea_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_PickArea_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_PickArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_PickArea_MouseUp);
            this.Resize += new System.EventHandler(this.frm_PickArea_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}