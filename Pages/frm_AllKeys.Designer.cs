namespace TFD_DJ_LUNA
{
    partial class frm_Allkeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Allkeys));
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.dicItem2 = new TFD_DJ_LUNA.DicItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flp.SuspendLayout();
            this.SuspendLayout();
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.flp.Controls.Add(this.dicItem2);
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.Location = new System.Drawing.Point(0, 0);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(821, 480);
            this.flp.TabIndex = 0;
            // 
            // dicItem2
            // 
            this.dicItem2.ContentColor = System.Drawing.SystemColors.ControlText;
            this.dicItem2.DescriptionColor = System.Drawing.Color.DimGray;
            this.dicItem2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dicItem2.KeyContent = "NUMPAD1";
            this.dicItem2.KeyDescription = "数字键盘1键";
            this.dicItem2.Location = new System.Drawing.Point(4, 4);
            this.dicItem2.Margin = new System.Windows.Forms.Padding(4);
            this.dicItem2.Name = "dicItem2";
            this.dicItem2.Size = new System.Drawing.Size(120, 46);
            this.dicItem2.TabIndex = 1;
            // 
            // frm_Allkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(821, 480);
            this.Controls.Add(this.flp);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Allkeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择按键";
            this.Load += new System.EventHandler(this.frm_Allkeys_Load);
            this.flp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp;
        private DicItem dicItem2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}