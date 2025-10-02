namespace TFD_DJ_LUNA.Tools
{
    partial class RecognizeAreaSetting
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gb = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rg_BGR = new System.Windows.Forms.RadioButton();
            this.rg_HSV_HS = new System.Windows.Forms.RadioButton();
            this.rg_Gray = new System.Windows.Forms.RadioButton();
            this.rg_HSV_S = new System.Windows.Forms.RadioButton();
            this.rg_HSV_H = new System.Windows.Forms.RadioButton();
            this.rg_ColorBlock = new System.Windows.Forms.RadioButton();
            this.tb_RealScreenWidth = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rb_MaskMode_0 = new System.Windows.Forms.RadioButton();
            this.rb_MaskMode_1 = new System.Windows.Forms.RadioButton();
            this.rb_MaskMode_2 = new System.Windows.Forms.RadioButton();
            this.ckb_ScaleMode = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_thd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Y = new System.Windows.Forms.TextBox();
            this.tb_X = new System.Windows.Forms.TextBox();
            this.tb_H = new System.Windows.Forms.TextBox();
            this.tb_W = new System.Windows.Forms.TextBox();
            this.btn_PickArea = new System.Windows.Forms.Button();
            this.tkb_thd = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_thd)).BeginInit();
            this.SuspendLayout();
            // 
            // gb
            // 
            this.gb.Controls.Add(this.groupBox1);
            this.gb.Controls.Add(this.tb_RealScreenWidth);
            this.gb.Controls.Add(this.groupBox6);
            this.gb.Controls.Add(this.ckb_ScaleMode);
            this.gb.Controls.Add(this.label6);
            this.gb.Controls.Add(this.lb_thd);
            this.gb.Controls.Add(this.label3);
            this.gb.Controls.Add(this.tb_Y);
            this.gb.Controls.Add(this.tb_X);
            this.gb.Controls.Add(this.tb_H);
            this.gb.Controls.Add(this.tb_W);
            this.gb.Controls.Add(this.btn_PickArea);
            this.gb.Controls.Add(this.tkb_thd);
            this.gb.Controls.Add(this.label5);
            this.gb.Controls.Add(this.label4);
            this.gb.Controls.Add(this.label2);
            this.gb.Controls.Add(this.label7);
            this.gb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb.Location = new System.Drawing.Point(1, 1);
            this.gb.Margin = new System.Windows.Forms.Padding(4);
            this.gb.Name = "gb";
            this.gb.Padding = new System.Windows.Forms.Padding(4);
            this.gb.Size = new System.Drawing.Size(455, 223);
            this.gb.TabIndex = 8;
            this.gb.TabStop = false;
            this.gb.Text = "菱形图案识别区域";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rg_BGR);
            this.groupBox1.Controls.Add(this.rg_HSV_HS);
            this.groupBox1.Controls.Add(this.rg_Gray);
            this.groupBox1.Controls.Add(this.rg_HSV_S);
            this.groupBox1.Controls.Add(this.rg_HSV_H);
            this.groupBox1.Controls.Add(this.rg_ColorBlock);
            this.groupBox1.Location = new System.Drawing.Point(12, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 67);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "截图处理格式";
            // 
            // rg_BGR
            // 
            this.rg_BGR.AutoSize = true;
            this.rg_BGR.Location = new System.Drawing.Point(10, 19);
            this.rg_BGR.Margin = new System.Windows.Forms.Padding(4);
            this.rg_BGR.Name = "rg_BGR";
            this.rg_BGR.Size = new System.Drawing.Size(51, 21);
            this.rg_BGR.TabIndex = 16;
            this.rg_BGR.Tag = "0";
            this.rg_BGR.Text = "BGR";
            this.toolTip1.SetToolTip(this.rg_BGR, "原图，不做多余处理");
            this.rg_BGR.UseVisualStyleBackColor = true;
            this.rg_BGR.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // rg_HSV_HS
            // 
            this.rg_HSV_HS.AutoSize = true;
            this.rg_HSV_HS.Location = new System.Drawing.Point(154, 40);
            this.rg_HSV_HS.Margin = new System.Windows.Forms.Padding(4);
            this.rg_HSV_HS.Name = "rg_HSV_HS";
            this.rg_HSV_HS.Size = new System.Drawing.Size(71, 21);
            this.rg_HSV_HS.TabIndex = 30;
            this.rg_HSV_HS.Tag = "4";
            this.rg_HSV_HS.Text = "HSV_HS";
            this.toolTip1.SetToolTip(this.rg_HSV_HS, "将图片转换为HSV模式，将H通道和S通道混合后处理");
            this.rg_HSV_HS.UseVisualStyleBackColor = true;
            this.rg_HSV_HS.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // rg_Gray
            // 
            this.rg_Gray.AutoSize = true;
            this.rg_Gray.Location = new System.Drawing.Point(82, 19);
            this.rg_Gray.Margin = new System.Windows.Forms.Padding(4);
            this.rg_Gray.Name = "rg_Gray";
            this.rg_Gray.Size = new System.Drawing.Size(62, 21);
            this.rg_Gray.TabIndex = 16;
            this.rg_Gray.Tag = "1";
            this.rg_Gray.Text = "灰度图";
            this.toolTip1.SetToolTip(this.rg_Gray, "把截图和模板转换为灰度图，然后再对比");
            this.rg_Gray.UseVisualStyleBackColor = true;
            this.rg_Gray.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // rg_HSV_S
            // 
            this.rg_HSV_S.AutoSize = true;
            this.rg_HSV_S.Location = new System.Drawing.Point(82, 40);
            this.rg_HSV_S.Margin = new System.Windows.Forms.Padding(4);
            this.rg_HSV_S.Name = "rg_HSV_S";
            this.rg_HSV_S.Size = new System.Drawing.Size(62, 21);
            this.rg_HSV_S.TabIndex = 30;
            this.rg_HSV_S.Tag = "3";
            this.rg_HSV_S.Text = "HSV_S";
            this.toolTip1.SetToolTip(this.rg_HSV_S, "将图片转换为HSV模式，只采用S通道处理");
            this.rg_HSV_S.UseVisualStyleBackColor = true;
            this.rg_HSV_S.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // rg_HSV_H
            // 
            this.rg_HSV_H.AutoSize = true;
            this.rg_HSV_H.Location = new System.Drawing.Point(10, 40);
            this.rg_HSV_H.Margin = new System.Windows.Forms.Padding(4);
            this.rg_HSV_H.Name = "rg_HSV_H";
            this.rg_HSV_H.Size = new System.Drawing.Size(64, 21);
            this.rg_HSV_H.TabIndex = 17;
            this.rg_HSV_H.Tag = "2";
            this.rg_HSV_H.Text = "HSV_H";
            this.toolTip1.SetToolTip(this.rg_HSV_H, "将图片转换为HSV模式，只采用H通道处理");
            this.rg_HSV_H.UseVisualStyleBackColor = true;
            this.rg_HSV_H.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // rg_ColorBlock
            // 
            this.rg_ColorBlock.AutoSize = true;
            this.rg_ColorBlock.Location = new System.Drawing.Point(154, 19);
            this.rg_ColorBlock.Margin = new System.Windows.Forms.Padding(4);
            this.rg_ColorBlock.Name = "rg_ColorBlock";
            this.rg_ColorBlock.Size = new System.Drawing.Size(62, 21);
            this.rg_ColorBlock.TabIndex = 29;
            this.rg_ColorBlock.Tag = "-1";
            this.rg_ColorBlock.Text = "纯色块";
            this.toolTip1.SetToolTip(this.rg_ColorBlock, "截图与模板都是纯颜色方块，没有多余的纹理图案时用");
            this.rg_ColorBlock.UseVisualStyleBackColor = true;
            this.rg_ColorBlock.CheckedChanged += new System.EventHandler(this.rg_ImgMode_Changed);
            // 
            // tb_RealScreenWidth
            // 
            this.tb_RealScreenWidth.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_RealScreenWidth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_RealScreenWidth.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_RealScreenWidth.Location = new System.Drawing.Point(390, 53);
            this.tb_RealScreenWidth.Name = "tb_RealScreenWidth";
            this.tb_RealScreenWidth.Size = new System.Drawing.Size(55, 16);
            this.tb_RealScreenWidth.TabIndex = 26;
            this.tb_RealScreenWidth.Text = "1920";
            this.tb_RealScreenWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tb_RealScreenWidth, "游戏实际运行时的画面宽度，用于计算缩放比例");
            this.tb_RealScreenWidth.TextChanged += new System.EventHandler(this.tb_RealScreenWidth_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rb_MaskMode_0);
            this.groupBox6.Controls.Add(this.rb_MaskMode_1);
            this.groupBox6.Controls.Add(this.rb_MaskMode_2);
            this.groupBox6.Location = new System.Drawing.Point(309, 80);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(136, 100);
            this.groupBox6.TabIndex = 28;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "掩码设置";
            // 
            // rb_MaskMode_0
            // 
            this.rb_MaskMode_0.AutoSize = true;
            this.rb_MaskMode_0.Location = new System.Drawing.Point(15, 74);
            this.rb_MaskMode_0.Name = "rb_MaskMode_0";
            this.rb_MaskMode_0.Size = new System.Drawing.Size(74, 21);
            this.rb_MaskMode_0.TabIndex = 0;
            this.rb_MaskMode_0.TabStop = true;
            this.rb_MaskMode_0.Tag = "0";
            this.rb_MaskMode_0.Text = "禁用掩码";
            this.toolTip1.SetToolTip(this.rb_MaskMode_0, "不使用掩码，适用于形状规整的模板图案");
            this.rb_MaskMode_0.UseVisualStyleBackColor = true;
            this.rb_MaskMode_0.CheckedChanged += new System.EventHandler(this.rg_MaskMode_Changed);
            // 
            // rb_MaskMode_1
            // 
            this.rb_MaskMode_1.AutoSize = true;
            this.rb_MaskMode_1.Location = new System.Drawing.Point(15, 48);
            this.rb_MaskMode_1.Name = "rb_MaskMode_1";
            this.rb_MaskMode_1.Size = new System.Drawing.Size(98, 21);
            this.rb_MaskMode_1.TabIndex = 0;
            this.rb_MaskMode_1.TabStop = true;
            this.rb_MaskMode_1.Tag = "1";
            this.rb_MaskMode_1.Text = "自动生成掩码";
            this.toolTip1.SetToolTip(this.rb_MaskMode_1, "程序根据模板自动生成掩码，准确度视模板状况而定");
            this.rb_MaskMode_1.UseVisualStyleBackColor = true;
            this.rb_MaskMode_1.CheckedChanged += new System.EventHandler(this.rg_MaskMode_Changed);
            // 
            // rb_MaskMode_2
            // 
            this.rb_MaskMode_2.AutoSize = true;
            this.rb_MaskMode_2.Location = new System.Drawing.Point(15, 22);
            this.rb_MaskMode_2.Name = "rb_MaskMode_2";
            this.rb_MaskMode_2.Size = new System.Drawing.Size(98, 21);
            this.rb_MaskMode_2.TabIndex = 0;
            this.rb_MaskMode_2.TabStop = true;
            this.rb_MaskMode_2.Tag = "2";
            this.rb_MaskMode_2.Text = "手动制作掩码";
            this.toolTip1.SetToolTip(this.rb_MaskMode_2, "需要根据模板人工制作对应的模板图片，准确度较高，推荐");
            this.rb_MaskMode_2.UseVisualStyleBackColor = true;
            this.rb_MaskMode_2.CheckedChanged += new System.EventHandler(this.rg_MaskMode_Changed);
            // 
            // ckb_ScaleMode
            // 
            this.ckb_ScaleMode.AutoSize = true;
            this.ckb_ScaleMode.Location = new System.Drawing.Point(306, 26);
            this.ckb_ScaleMode.Name = "ckb_ScaleMode";
            this.ckb_ScaleMode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_ScaleMode.Size = new System.Drawing.Size(75, 21);
            this.ckb_ScaleMode.TabIndex = 27;
            this.ckb_ScaleMode.Text = "缩放模板";
            this.toolTip1.SetToolTip(this.ckb_ScaleMode, "启用后，将根据下面填写的游戏画面宽度与1920计算出缩放比例，把模板及掩码缩放后，再去和截图对比，用于非1080P分辨率下使用标准模板图片");
            this.ckb_ScaleMode.UseVisualStyleBackColor = true;
            this.ckb_ScaleMode.CheckedChanged += new System.EventHandler(this.ckb_ScaleMode_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "游戏画面宽度：";
            // 
            // lb_thd
            // 
            this.lb_thd.AutoSize = true;
            this.lb_thd.Location = new System.Drawing.Point(413, 194);
            this.lb_thd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_thd.Name = "lb_thd";
            this.lb_thd.Size = new System.Drawing.Size(32, 17);
            this.lb_thd.TabIndex = 13;
            this.lb_thd.Text = "0.56";
            this.toolTip1.SetToolTip(this.lb_thd, "程序根据模板和截图计算出来的匹配度大于这个值时，视为在截图中检测到了模板图");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 194);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "匹配度：";
            this.toolTip1.SetToolTip(this.label3, "程序根据模板和截图计算出来的匹配度大于这个值时，视为在截图中检测到了模板图");
            // 
            // tb_Y
            // 
            this.tb_Y.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_Y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_Y.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Y.Location = new System.Drawing.Point(181, 26);
            this.tb_Y.Name = "tb_Y";
            this.tb_Y.Size = new System.Drawing.Size(60, 16);
            this.tb_Y.TabIndex = 11;
            this.tb_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Y.TextChanged += new System.EventHandler(this.tb_Area_Changed);
            // 
            // tb_X
            // 
            this.tb_X.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_X.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_X.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_X.Location = new System.Drawing.Point(92, 26);
            this.tb_X.Name = "tb_X";
            this.tb_X.Size = new System.Drawing.Size(60, 16);
            this.tb_X.TabIndex = 11;
            this.tb_X.Text = "0";
            this.tb_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_X.TextChanged += new System.EventHandler(this.tb_Area_Changed);
            // 
            // tb_H
            // 
            this.tb_H.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_H.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_H.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_H.Location = new System.Drawing.Point(92, 78);
            this.tb_H.Name = "tb_H";
            this.tb_H.Size = new System.Drawing.Size(60, 16);
            this.tb_H.TabIndex = 10;
            this.tb_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_H.TextChanged += new System.EventHandler(this.tb_Area_Changed);
            // 
            // tb_W
            // 
            this.tb_W.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_W.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_W.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_W.Location = new System.Drawing.Point(92, 52);
            this.tb_W.Name = "tb_W";
            this.tb_W.Size = new System.Drawing.Size(60, 16);
            this.tb_W.TabIndex = 10;
            this.tb_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_W.TextChanged += new System.EventHandler(this.tb_Area_Changed);
            // 
            // btn_PickArea
            // 
            this.btn_PickArea.BackColor = System.Drawing.Color.Teal;
            this.btn_PickArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PickArea.FlatAppearance.BorderSize = 0;
            this.btn_PickArea.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_PickArea.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_PickArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PickArea.ForeColor = System.Drawing.Color.White;
            this.btn_PickArea.Location = new System.Drawing.Point(181, 53);
            this.btn_PickArea.Margin = new System.Windows.Forms.Padding(4);
            this.btn_PickArea.Name = "btn_PickArea";
            this.btn_PickArea.Size = new System.Drawing.Size(60, 42);
            this.btn_PickArea.TabIndex = 9;
            this.btn_PickArea.Text = "选择\r\n区域";
            this.btn_PickArea.UseVisualStyleBackColor = false;
            this.btn_PickArea.Click += new System.EventHandler(this.btn_PickArea_Click);
            // 
            // tkb_thd
            // 
            this.tkb_thd.AutoSize = false;
            this.tkb_thd.BackColor = System.Drawing.Color.White;
            this.tkb_thd.Location = new System.Drawing.Point(92, 194);
            this.tkb_thd.Margin = new System.Windows.Forms.Padding(4);
            this.tkb_thd.Maximum = 100;
            this.tkb_thd.Name = "tkb_thd";
            this.tkb_thd.Size = new System.Drawing.Size(313, 20);
            this.tkb_thd.TabIndex = 8;
            this.tkb_thd.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.tkb_thd, "程序根据模板和截图计算出来的匹配度大于这个值时，视为在截图中检测到了模板图");
            this.tkb_thd.Value = 56;
            this.tkb_thd.ValueChanged += new System.EventHandler(this.tkb_thd_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "区域高度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "区域宽度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "左上角坐标：";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(154, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = ",";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecognizeAreaSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gb);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RecognizeAreaSetting";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(457, 225);
            this.Load += new System.EventHandler(this.RecognizeAreaSetting_Load);
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_thd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.TextBox tb_RealScreenWidth;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rb_MaskMode_0;
        private System.Windows.Forms.RadioButton rb_MaskMode_1;
        private System.Windows.Forms.RadioButton rb_MaskMode_2;
        private System.Windows.Forms.CheckBox ckb_ScaleMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rg_HSV_H;
        private System.Windows.Forms.RadioButton rg_Gray;
        private System.Windows.Forms.RadioButton rg_BGR;
        private System.Windows.Forms.Label lb_thd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Y;
        private System.Windows.Forms.TextBox tb_X;
        private System.Windows.Forms.TextBox tb_H;
        private System.Windows.Forms.TextBox tb_W;
        private System.Windows.Forms.Button btn_PickArea;
        private System.Windows.Forms.TrackBar tkb_thd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rg_ColorBlock;
        private System.Windows.Forms.RadioButton rg_HSV_HS;
        private System.Windows.Forms.RadioButton rg_HSV_S;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
