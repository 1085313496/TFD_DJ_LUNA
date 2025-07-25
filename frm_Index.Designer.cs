namespace TFD_DJ_LUNA
{
    partial class frm_Index
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Index));
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.rg_SWT_1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rg_SWT_2 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckb_ManualSPSKILL = new System.Windows.Forms.CheckBox();
            this.swb_EditPage = new TFD_DJ_LUNA.SwitchBar();
            this.gb_Noise = new System.Windows.Forms.GroupBox();
            this.gb_Assist = new System.Windows.Forms.GroupBox();
            this.btn_Switch = new System.Windows.Forms.Button();
            this.tb_HotKey_Switch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rb_CandV = new System.Windows.Forms.RadioButton();
            this.rb_CorV = new System.Windows.Forms.RadioButton();
            this.gb_R = new System.Windows.Forms.GroupBox();
            this.btn_Recog_R = new System.Windows.Forms.Button();
            this.lb_THD_R = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_RY = new System.Windows.Forms.TextBox();
            this.tb_RX = new System.Windows.Forms.TextBox();
            this.tb_RH = new System.Windows.Forms.TextBox();
            this.tb_RW = new System.Windows.Forms.TextBox();
            this.btn_PickArea_R = new System.Windows.Forms.Button();
            this.tkb_R = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gb_MB = new System.Windows.Forms.GroupBox();
            this.btn_Recog_MB = new System.Windows.Forms.Button();
            this.lb_THD_MB = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_MBY = new System.Windows.Forms.TextBox();
            this.tb_MBX = new System.Windows.Forms.TextBox();
            this.tb_MBH = new System.Windows.Forms.TextBox();
            this.tb_MBW = new System.Windows.Forms.TextBox();
            this.btn_PickArea_MB = new System.Windows.Forms.Button();
            this.tkb_MB = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ckb_ShowLogFrm = new System.Windows.Forms.CheckBox();
            this.tb_ScreenShotInterval = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_Run = new System.Windows.Forms.Button();
            this.tb_HotKey_Global = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.rb_Mode_Noise = new System.Windows.Forms.RadioButton();
            this.rb_Mode_Assist = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckb_SaveScreenImg = new System.Windows.Forms.CheckBox();
            this.gb_Noise.SuspendLayout();
            this.gb_Assist.SuspendLayout();
            this.gb_R.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_R)).BeginInit();
            this.gb_MB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_MB)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.BackColor = System.Drawing.Color.White;
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(3, 19);
            this.rtb.Margin = new System.Windows.Forms.Padding(4);
            this.rtb.Name = "rtb";
            this.rtb.ReadOnly = true;
            this.rtb.Size = new System.Drawing.Size(285, 388);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // rg_SWT_1
            // 
            this.rg_SWT_1.AutoSize = true;
            this.rg_SWT_1.Checked = true;
            this.rg_SWT_1.Location = new System.Drawing.Point(94, 29);
            this.rg_SWT_1.Margin = new System.Windows.Forms.Padding(4);
            this.rg_SWT_1.Name = "rg_SWT_1";
            this.rg_SWT_1.Size = new System.Drawing.Size(97, 21);
            this.rg_SWT_1.TabIndex = 4;
            this.rg_SWT_1.TabStop = true;
            this.rg_SWT_1.Text = "CVZ交替打点";
            this.rg_SWT_1.UseVisualStyleBackColor = true;
            this.rg_SWT_1.CheckedChanged += new System.EventHandler(this.Rb_SwitchType_Noise_CHKChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "打点方式：";
            // 
            // rg_SWT_2
            // 
            this.rg_SWT_2.AutoSize = true;
            this.rg_SWT_2.Location = new System.Drawing.Point(93, 58);
            this.rg_SWT_2.Margin = new System.Windows.Forms.Padding(4);
            this.rg_SWT_2.Name = "rg_SWT_2";
            this.rg_SWT_2.Size = new System.Drawing.Size(98, 21);
            this.rg_SWT_2.TabIndex = 4;
            this.rg_SWT_2.Text = "按照映射打点";
            this.toolTip1.SetToolTip(this.rg_SWT_2, "图案0按下C 1按下V 2按下Z 3按下C");
            this.rg_SWT_2.UseVisualStyleBackColor = true;
            // 
            // ckb_ManualSPSKILL
            // 
            this.ckb_ManualSPSKILL.AutoSize = true;
            this.ckb_ManualSPSKILL.Location = new System.Drawing.Point(20, 97);
            this.ckb_ManualSPSKILL.Name = "ckb_ManualSPSKILL";
            this.ckb_ManualSPSKILL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_ManualSPSKILL.Size = new System.Drawing.Size(135, 21);
            this.ckb_ManualSPSKILL.TabIndex = 9;
            this.ckb_ManualSPSKILL.Text = "：手动使用强化技能";
            this.toolTip1.SetToolTip(this.ckb_ManualSPSKILL, "灵感条充满后等待手动使用强化技能");
            this.ckb_ManualSPSKILL.UseVisualStyleBackColor = true;
            this.ckb_ManualSPSKILL.CheckedChanged += new System.EventHandler(this.ckb_ManualSPSKILL_CheckedChanged);
            // 
            // swb_EditPage
            // 
            this.swb_EditPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swb_EditPage.Location = new System.Drawing.Point(16, 13);
            this.swb_EditPage.Margin = new System.Windows.Forms.Padding(4);
            this.swb_EditPage.Name = "swb_EditPage";
            this.swb_EditPage.Size = new System.Drawing.Size(44, 20);
            this.swb_EditPage.swb_BackColorOff = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.swb_EditPage.swb_BackColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.swb_EditPage.swb_BarColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(222)))), ((int)(((byte)(227)))));
            this.swb_EditPage.swb_BarColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swb_EditPage.swb_BarSpacing = 2F;
            this.swb_EditPage.swb_BorderCorlor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(186)))), ((int)(((byte)(195)))));
            this.swb_EditPage.swb_BorderRadius = 8;
            this.swb_EditPage.swb_BorderThickness = 1;
            this.swb_EditPage.swb_OffText = "查看";
            this.swb_EditPage.swb_OnText = "编辑";
            this.swb_EditPage.swb_TextDistance = 5F;
            this.swb_EditPage.swb_TextPosition = TFD_DJ_LUNA.SwitchBar.StateTextPositon.None;
            this.swb_EditPage.SwitchState = false;
            this.swb_EditPage.TabIndex = 10;
            this.toolTip1.SetToolTip(this.swb_EditPage, "切换页面【查看/编辑】状态");
            this.swb_EditPage.StateChanged += new TFD_DJ_LUNA.SwitchBar.ClickedBarDelegate(this.swb_EditPage_StateChanged);
            // 
            // gb_Noise
            // 
            this.gb_Noise.Controls.Add(this.label1);
            this.gb_Noise.Controls.Add(this.rg_SWT_1);
            this.gb_Noise.Controls.Add(this.rg_SWT_2);
            this.gb_Noise.Location = new System.Drawing.Point(16, 249);
            this.gb_Noise.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Noise.Name = "gb_Noise";
            this.gb_Noise.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Noise.Size = new System.Drawing.Size(339, 200);
            this.gb_Noise.TabIndex = 6;
            this.gb_Noise.TabStop = false;
            this.gb_Noise.Text = "噪音涌动输出流";
            // 
            // gb_Assist
            // 
            this.gb_Assist.Controls.Add(this.btn_Switch);
            this.gb_Assist.Controls.Add(this.tb_HotKey_Switch);
            this.gb_Assist.Controls.Add(this.label8);
            this.gb_Assist.Controls.Add(this.ckb_ManualSPSKILL);
            this.gb_Assist.Controls.Add(this.label6);
            this.gb_Assist.Controls.Add(this.rb_CandV);
            this.gb_Assist.Controls.Add(this.rb_CorV);
            this.gb_Assist.Location = new System.Drawing.Point(16, 249);
            this.gb_Assist.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Assist.Name = "gb_Assist";
            this.gb_Assist.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Assist.Size = new System.Drawing.Size(339, 200);
            this.gb_Assist.TabIndex = 6;
            this.gb_Assist.TabStop = false;
            this.gb_Assist.Text = "辅助流";
            // 
            // btn_Switch
            // 
            this.btn_Switch.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Switch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Switch.FlatAppearance.BorderSize = 0;
            this.btn_Switch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Switch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btn_Switch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Switch.ForeColor = System.Drawing.Color.White;
            this.btn_Switch.Location = new System.Drawing.Point(208, 143);
            this.btn_Switch.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Switch.Name = "btn_Switch";
            this.btn_Switch.Size = new System.Drawing.Size(60, 24);
            this.btn_Switch.TabIndex = 13;
            this.btn_Switch.Text = "切换";
            this.btn_Switch.UseVisualStyleBackColor = false;
            this.btn_Switch.Click += new System.EventHandler(this.btn_Switch_Click);
            // 
            // tb_HotKey_Switch
            // 
            this.tb_HotKey_Switch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_HotKey_Switch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_HotKey_Switch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_HotKey_Switch.Location = new System.Drawing.Point(131, 148);
            this.tb_HotKey_Switch.Name = "tb_HotKey_Switch";
            this.tb_HotKey_Switch.Size = new System.Drawing.Size(60, 16);
            this.tb_HotKey_Switch.TabIndex = 12;
            this.tb_HotKey_Switch.Text = "F6";
            this.tb_HotKey_Switch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HotKey_Switch.TextChanged += new System.EventHandler(this.tb_HotKey_Switch_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "切换按键快捷键 ：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "识别到菱形图案后按键形式：";
            // 
            // rb_CandV
            // 
            this.rb_CandV.AutoSize = true;
            this.rb_CandV.Checked = true;
            this.rb_CandV.Location = new System.Drawing.Point(191, 61);
            this.rb_CandV.Margin = new System.Windows.Forms.Padding(4);
            this.rb_CandV.Name = "rb_CandV";
            this.rb_CandV.Size = new System.Drawing.Size(90, 21);
            this.rb_CandV.TabIndex = 6;
            this.rb_CandV.TabStop = true;
            this.rb_CandV.Text = "CV交替打点";
            this.rb_CandV.UseVisualStyleBackColor = true;
            this.rb_CandV.CheckedChanged += new System.EventHandler(this.Rb_SwitchType_Assist_CHKChanged);
            // 
            // rb_CorV
            // 
            this.rb_CorV.AutoSize = true;
            this.rb_CorV.Location = new System.Drawing.Point(191, 32);
            this.rb_CorV.Margin = new System.Windows.Forms.Padding(4);
            this.rb_CorV.Name = "rb_CorV";
            this.rb_CorV.Size = new System.Drawing.Size(126, 21);
            this.rb_CorV.TabIndex = 7;
            this.rb_CorV.Text = "手动切换只按C或V";
            this.rb_CorV.UseVisualStyleBackColor = true;
            // 
            // gb_R
            // 
            this.gb_R.Controls.Add(this.btn_Recog_R);
            this.gb_R.Controls.Add(this.lb_THD_R);
            this.gb_R.Controls.Add(this.label3);
            this.gb_R.Controls.Add(this.tb_RY);
            this.gb_R.Controls.Add(this.tb_RX);
            this.gb_R.Controls.Add(this.tb_RH);
            this.gb_R.Controls.Add(this.tb_RW);
            this.gb_R.Controls.Add(this.btn_PickArea_R);
            this.gb_R.Controls.Add(this.tkb_R);
            this.gb_R.Controls.Add(this.label5);
            this.gb_R.Controls.Add(this.label4);
            this.gb_R.Controls.Add(this.label2);
            this.gb_R.Controls.Add(this.label7);
            this.gb_R.Location = new System.Drawing.Point(363, 41);
            this.gb_R.Margin = new System.Windows.Forms.Padding(4);
            this.gb_R.Name = "gb_R";
            this.gb_R.Padding = new System.Windows.Forms.Padding(4);
            this.gb_R.Size = new System.Drawing.Size(376, 200);
            this.gb_R.TabIndex = 7;
            this.gb_R.TabStop = false;
            this.gb_R.Text = "菱形图案识别区域";
            // 
            // btn_Recog_R
            // 
            this.btn_Recog_R.BackColor = System.Drawing.Color.SlateGray;
            this.btn_Recog_R.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Recog_R.FlatAppearance.BorderSize = 0;
            this.btn_Recog_R.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Recog_R.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Recog_R.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recog_R.ForeColor = System.Drawing.Color.White;
            this.btn_Recog_R.Location = new System.Drawing.Point(288, 130);
            this.btn_Recog_R.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Recog_R.Name = "btn_Recog_R";
            this.btn_Recog_R.Size = new System.Drawing.Size(69, 25);
            this.btn_Recog_R.TabIndex = 15;
            this.btn_Recog_R.Text = "手动识图";
            this.btn_Recog_R.UseVisualStyleBackColor = false;
            this.btn_Recog_R.Visible = false;
            this.btn_Recog_R.Click += new System.EventHandler(this.btn_Recog_R_Click);
            // 
            // lb_THD_R
            // 
            this.lb_THD_R.AutoSize = true;
            this.lb_THD_R.Location = new System.Drawing.Point(328, 160);
            this.lb_THD_R.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_THD_R.Name = "lb_THD_R";
            this.lb_THD_R.Size = new System.Drawing.Size(32, 17);
            this.lb_THD_R.TabIndex = 13;
            this.lb_THD_R.Text = "0.56";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "匹配的相似度要求：";
            // 
            // tb_RY
            // 
            this.tb_RY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_RY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_RY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_RY.Location = new System.Drawing.Point(184, 36);
            this.tb_RY.Name = "tb_RY";
            this.tb_RY.Size = new System.Drawing.Size(60, 16);
            this.tb_RY.TabIndex = 11;
            this.tb_RY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_RY.TextChanged += new System.EventHandler(this.Tb_PA_R_Changed);
            // 
            // tb_RX
            // 
            this.tb_RX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_RX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_RX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_RX.Location = new System.Drawing.Point(117, 36);
            this.tb_RX.Name = "tb_RX";
            this.tb_RX.Size = new System.Drawing.Size(60, 16);
            this.tb_RX.TabIndex = 11;
            this.tb_RX.Text = "0";
            this.tb_RX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_RX.TextChanged += new System.EventHandler(this.Tb_PA_R_Changed);
            // 
            // tb_RH
            // 
            this.tb_RH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_RH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_RH.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_RH.Location = new System.Drawing.Point(117, 88);
            this.tb_RH.Name = "tb_RH";
            this.tb_RH.Size = new System.Drawing.Size(60, 16);
            this.tb_RH.TabIndex = 10;
            this.tb_RH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_RH.TextChanged += new System.EventHandler(this.Tb_PA_R_Changed);
            // 
            // tb_RW
            // 
            this.tb_RW.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_RW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_RW.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_RW.Location = new System.Drawing.Point(117, 62);
            this.tb_RW.Name = "tb_RW";
            this.tb_RW.Size = new System.Drawing.Size(60, 16);
            this.tb_RW.TabIndex = 10;
            this.tb_RW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_RW.TextChanged += new System.EventHandler(this.Tb_PA_R_Changed);
            // 
            // btn_PickArea_R
            // 
            this.btn_PickArea_R.BackColor = System.Drawing.Color.Teal;
            this.btn_PickArea_R.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PickArea_R.FlatAppearance.BorderSize = 0;
            this.btn_PickArea_R.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_PickArea_R.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_PickArea_R.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PickArea_R.ForeColor = System.Drawing.Color.White;
            this.btn_PickArea_R.Location = new System.Drawing.Point(288, 36);
            this.btn_PickArea_R.Margin = new System.Windows.Forms.Padding(4);
            this.btn_PickArea_R.Name = "btn_PickArea_R";
            this.btn_PickArea_R.Size = new System.Drawing.Size(72, 26);
            this.btn_PickArea_R.TabIndex = 9;
            this.btn_PickArea_R.Text = "选择区域";
            this.btn_PickArea_R.UseVisualStyleBackColor = false;
            this.btn_PickArea_R.Click += new System.EventHandler(this.btn_PickArea_R_Click);
            // 
            // tkb_R
            // 
            this.tkb_R.AutoSize = false;
            this.tkb_R.Location = new System.Drawing.Point(7, 157);
            this.tkb_R.Margin = new System.Windows.Forms.Padding(4);
            this.tkb_R.Maximum = 100;
            this.tkb_R.Name = "tkb_R";
            this.tkb_R.Size = new System.Drawing.Size(313, 20);
            this.tkb_R.TabIndex = 8;
            this.tkb_R.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tkb_R.Value = 56;
            this.tkb_R.ValueChanged += new System.EventHandler(this.tkb_R_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "识别区域高度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "识别区域宽度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "区域左上角坐标：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(174, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "，";
            // 
            // gb_MB
            // 
            this.gb_MB.Controls.Add(this.btn_Recog_MB);
            this.gb_MB.Controls.Add(this.lb_THD_MB);
            this.gb_MB.Controls.Add(this.label9);
            this.gb_MB.Controls.Add(this.tb_MBY);
            this.gb_MB.Controls.Add(this.tb_MBX);
            this.gb_MB.Controls.Add(this.tb_MBH);
            this.gb_MB.Controls.Add(this.tb_MBW);
            this.gb_MB.Controls.Add(this.btn_PickArea_MB);
            this.gb_MB.Controls.Add(this.tkb_MB);
            this.gb_MB.Controls.Add(this.label10);
            this.gb_MB.Controls.Add(this.label11);
            this.gb_MB.Controls.Add(this.label12);
            this.gb_MB.Controls.Add(this.label13);
            this.gb_MB.Location = new System.Drawing.Point(363, 249);
            this.gb_MB.Margin = new System.Windows.Forms.Padding(4);
            this.gb_MB.Name = "gb_MB";
            this.gb_MB.Padding = new System.Windows.Forms.Padding(4);
            this.gb_MB.Size = new System.Drawing.Size(376, 200);
            this.gb_MB.TabIndex = 8;
            this.gb_MB.TabStop = false;
            this.gb_MB.Text = "灵感条识别区域";
            // 
            // btn_Recog_MB
            // 
            this.btn_Recog_MB.BackColor = System.Drawing.Color.SlateGray;
            this.btn_Recog_MB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Recog_MB.FlatAppearance.BorderSize = 0;
            this.btn_Recog_MB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Recog_MB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Recog_MB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recog_MB.ForeColor = System.Drawing.Color.White;
            this.btn_Recog_MB.Location = new System.Drawing.Point(291, 128);
            this.btn_Recog_MB.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Recog_MB.Name = "btn_Recog_MB";
            this.btn_Recog_MB.Size = new System.Drawing.Size(69, 25);
            this.btn_Recog_MB.TabIndex = 16;
            this.btn_Recog_MB.Text = "手动识图";
            this.btn_Recog_MB.UseVisualStyleBackColor = false;
            this.btn_Recog_MB.Visible = false;
            this.btn_Recog_MB.Click += new System.EventHandler(this.btn_Recog_MB_Click);
            // 
            // lb_THD_MB
            // 
            this.lb_THD_MB.AutoSize = true;
            this.lb_THD_MB.Location = new System.Drawing.Point(328, 161);
            this.lb_THD_MB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_THD_MB.Name = "lb_THD_MB";
            this.lb_THD_MB.Size = new System.Drawing.Size(32, 17);
            this.lb_THD_MB.TabIndex = 13;
            this.lb_THD_MB.Text = "0.56";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 132);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "匹配的相似度要求：";
            // 
            // tb_MBY
            // 
            this.tb_MBY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_MBY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_MBY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_MBY.Location = new System.Drawing.Point(184, 34);
            this.tb_MBY.Name = "tb_MBY";
            this.tb_MBY.Size = new System.Drawing.Size(60, 16);
            this.tb_MBY.TabIndex = 11;
            this.tb_MBY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_MBY.TextChanged += new System.EventHandler(this.Tb_PA_MB_Changed);
            // 
            // tb_MBX
            // 
            this.tb_MBX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_MBX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_MBX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_MBX.Location = new System.Drawing.Point(117, 34);
            this.tb_MBX.Name = "tb_MBX";
            this.tb_MBX.Size = new System.Drawing.Size(60, 16);
            this.tb_MBX.TabIndex = 11;
            this.tb_MBX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_MBX.TextChanged += new System.EventHandler(this.Tb_PA_MB_Changed);
            // 
            // tb_MBH
            // 
            this.tb_MBH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_MBH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_MBH.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_MBH.Location = new System.Drawing.Point(117, 86);
            this.tb_MBH.Name = "tb_MBH";
            this.tb_MBH.Size = new System.Drawing.Size(60, 16);
            this.tb_MBH.TabIndex = 10;
            this.tb_MBH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_MBH.TextChanged += new System.EventHandler(this.Tb_PA_MB_Changed);
            // 
            // tb_MBW
            // 
            this.tb_MBW.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_MBW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_MBW.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_MBW.Location = new System.Drawing.Point(117, 60);
            this.tb_MBW.Name = "tb_MBW";
            this.tb_MBW.Size = new System.Drawing.Size(60, 16);
            this.tb_MBW.TabIndex = 10;
            this.tb_MBW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_MBW.TextChanged += new System.EventHandler(this.Tb_PA_MB_Changed);
            // 
            // btn_PickArea_MB
            // 
            this.btn_PickArea_MB.BackColor = System.Drawing.Color.Teal;
            this.btn_PickArea_MB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PickArea_MB.FlatAppearance.BorderSize = 0;
            this.btn_PickArea_MB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_PickArea_MB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_PickArea_MB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PickArea_MB.ForeColor = System.Drawing.Color.White;
            this.btn_PickArea_MB.Location = new System.Drawing.Point(288, 34);
            this.btn_PickArea_MB.Margin = new System.Windows.Forms.Padding(4);
            this.btn_PickArea_MB.Name = "btn_PickArea_MB";
            this.btn_PickArea_MB.Size = new System.Drawing.Size(72, 26);
            this.btn_PickArea_MB.TabIndex = 9;
            this.btn_PickArea_MB.Text = "选择区域";
            this.btn_PickArea_MB.UseVisualStyleBackColor = false;
            this.btn_PickArea_MB.Click += new System.EventHandler(this.btn_PickArea_MB_Click);
            // 
            // tkb_MB
            // 
            this.tkb_MB.AutoSize = false;
            this.tkb_MB.Location = new System.Drawing.Point(8, 159);
            this.tkb_MB.Margin = new System.Windows.Forms.Padding(4);
            this.tkb_MB.Maximum = 100;
            this.tkb_MB.Name = "tkb_MB";
            this.tkb_MB.Size = new System.Drawing.Size(312, 20);
            this.tkb_MB.TabIndex = 8;
            this.tkb_MB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tkb_MB.Value = 56;
            this.tkb_MB.ValueChanged += new System.EventHandler(this.tkb_MB_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 86);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "识别区域高度：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 60);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "识别区域宽度：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 34);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 17);
            this.label12.TabIndex = 6;
            this.label12.Text = "区域左上角坐标：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(174, 34);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 17);
            this.label13.TabIndex = 14;
            this.label13.Text = "，";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ckb_SaveScreenImg);
            this.groupBox5.Controls.Add(this.ckb_ShowLogFrm);
            this.groupBox5.Controls.Add(this.tb_ScreenShotInterval);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.btn_Run);
            this.groupBox5.Controls.Add(this.tb_HotKey_Global);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.rb_Mode_Noise);
            this.groupBox5.Controls.Add(this.rb_Mode_Assist);
            this.groupBox5.Location = new System.Drawing.Point(16, 41);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(339, 200);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "全局设置";
            // 
            // ckb_ShowLogFrm
            // 
            this.ckb_ShowLogFrm.AutoSize = true;
            this.ckb_ShowLogFrm.Checked = true;
            this.ckb_ShowLogFrm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_ShowLogFrm.Location = new System.Drawing.Point(20, 168);
            this.ckb_ShowLogFrm.Name = "ckb_ShowLogFrm";
            this.ckb_ShowLogFrm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_ShowLogFrm.Size = new System.Drawing.Size(111, 21);
            this.ckb_ShowLogFrm.TabIndex = 19;
            this.ckb_ShowLogFrm.Text = "：显示日志弹窗";
            this.ckb_ShowLogFrm.UseVisualStyleBackColor = true;
            this.ckb_ShowLogFrm.CheckedChanged += new System.EventHandler(this.ckb_ShowLogFrm_CheckedChanged);
            // 
            // tb_ScreenShotInterval
            // 
            this.tb_ScreenShotInterval.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_ScreenShotInterval.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ScreenShotInterval.Location = new System.Drawing.Point(141, 79);
            this.tb_ScreenShotInterval.Name = "tb_ScreenShotInterval";
            this.tb_ScreenShotInterval.Size = new System.Drawing.Size(60, 16);
            this.tb_ScreenShotInterval.TabIndex = 18;
            this.tb_ScreenShotInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ScreenShotInterval.TextChanged += new System.EventHandler(this.tb_ScreenShotInterval_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 79);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 17);
            this.label16.TabIndex = 17;
            this.label16.Text = "截屏时间间隔(ms) ：";
            // 
            // btn_Run
            // 
            this.btn_Run.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Run.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Run.FlatAppearance.BorderSize = 0;
            this.btn_Run.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Run.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btn_Run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Run.ForeColor = System.Drawing.Color.White;
            this.btn_Run.Location = new System.Drawing.Point(218, 29);
            this.btn_Run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(60, 24);
            this.btn_Run.TabIndex = 16;
            this.btn_Run.Text = "启动";
            this.btn_Run.UseVisualStyleBackColor = false;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // tb_HotKey_Global
            // 
            this.tb_HotKey_Global.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_HotKey_Global.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_HotKey_Global.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_HotKey_Global.Location = new System.Drawing.Point(141, 34);
            this.tb_HotKey_Global.Name = "tb_HotKey_Global";
            this.tb_HotKey_Global.Size = new System.Drawing.Size(60, 16);
            this.tb_HotKey_Global.TabIndex = 15;
            this.tb_HotKey_Global.Text = "F5";
            this.tb_HotKey_Global.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HotKey_Global.TextChanged += new System.EventHandler(this.tb_HotKey_Global_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 33);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(117, 17);
            this.label15.TabIndex = 14;
            this.label15.Text = "启用/停止 快捷键 ：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 117);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "打点方式：";
            // 
            // rb_Mode_Noise
            // 
            this.rb_Mode_Noise.AutoSize = true;
            this.rb_Mode_Noise.Checked = true;
            this.rb_Mode_Noise.Location = new System.Drawing.Point(93, 115);
            this.rb_Mode_Noise.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mode_Noise.Name = "rb_Mode_Noise";
            this.rb_Mode_Noise.Size = new System.Drawing.Size(110, 21);
            this.rb_Mode_Noise.TabIndex = 4;
            this.rb_Mode_Noise.TabStop = true;
            this.rb_Mode_Noise.Text = "噪音涌动输出流";
            this.rb_Mode_Noise.UseVisualStyleBackColor = true;
            this.rb_Mode_Noise.CheckedChanged += new System.EventHandler(this.Rb_Mode_CheckedChanged);
            // 
            // rb_Mode_Assist
            // 
            this.rb_Mode_Assist.AutoSize = true;
            this.rb_Mode_Assist.Location = new System.Drawing.Point(93, 136);
            this.rb_Mode_Assist.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mode_Assist.Name = "rb_Mode_Assist";
            this.rb_Mode_Assist.Size = new System.Drawing.Size(62, 21);
            this.rb_Mode_Assist.TabIndex = 4;
            this.rb_Mode_Assist.Text = "辅助流";
            this.rb_Mode_Assist.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtb);
            this.groupBox1.Location = new System.Drawing.Point(746, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 410);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行日志";
            // 
            // ckb_SaveScreenImg
            // 
            this.ckb_SaveScreenImg.AutoSize = true;
            this.ckb_SaveScreenImg.Location = new System.Drawing.Point(191, 168);
            this.ckb_SaveScreenImg.Name = "ckb_SaveScreenImg";
            this.ckb_SaveScreenImg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_SaveScreenImg.Size = new System.Drawing.Size(87, 21);
            this.ckb_SaveScreenImg.TabIndex = 20;
            this.ckb_SaveScreenImg.Text = "：保存截屏";
            this.ckb_SaveScreenImg.UseVisualStyleBackColor = true;
            this.ckb_SaveScreenImg.CheckedChanged += new System.EventHandler(this.ckb_SaveScreenImg_CheckedChanged);
            // 
            // frm_Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1052, 462);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.swb_EditPage);
            this.Controls.Add(this.gb_MB);
            this.Controls.Add(this.gb_R);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gb_Assist);
            this.Controls.Add(this.gb_Noise);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frm_Index";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自己的碟请自己打";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb_Noise.ResumeLayout(false);
            this.gb_Noise.PerformLayout();
            this.gb_Assist.ResumeLayout(false);
            this.gb_Assist.PerformLayout();
            this.gb_R.ResumeLayout(false);
            this.gb_R.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_R)).EndInit();
            this.gb_MB.ResumeLayout(false);
            this.gb_MB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_MB)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.RadioButton rg_SWT_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rg_SWT_2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gb_Noise;
        private System.Windows.Forms.GroupBox gb_Assist;
        private System.Windows.Forms.GroupBox gb_R;
        private System.Windows.Forms.Label lb_THD_R;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_RY;
        private System.Windows.Forms.TextBox tb_RX;
        private System.Windows.Forms.TextBox tb_RH;
        private System.Windows.Forms.TextBox tb_RW;
        private System.Windows.Forms.Button btn_PickArea_R;
        private System.Windows.Forms.TrackBar tkb_R;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gb_MB;
        private System.Windows.Forms.Label lb_THD_MB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_MBY;
        private System.Windows.Forms.TextBox tb_MBX;
        private System.Windows.Forms.TextBox tb_MBH;
        private System.Windows.Forms.TextBox tb_MBW;
        private System.Windows.Forms.Button btn_PickArea_MB;
        private System.Windows.Forms.TrackBar tkb_MB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rb_CandV;
        private System.Windows.Forms.RadioButton rb_CorV;
        private System.Windows.Forms.CheckBox ckb_ManualSPSKILL;
        private System.Windows.Forms.TextBox tb_HotKey_Switch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Switch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.TextBox tb_HotKey_Global;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rb_Mode_Noise;
        private System.Windows.Forms.RadioButton rb_Mode_Assist;
        private System.Windows.Forms.TextBox tb_ScreenShotInterval;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btn_Recog_R;
        private System.Windows.Forms.Button btn_Recog_MB;
        private SwitchBar swb_EditPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckb_ShowLogFrm;
        private System.Windows.Forms.CheckBox ckb_SaveScreenImg;
    }
}

