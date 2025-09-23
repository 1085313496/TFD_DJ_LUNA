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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckb_ManualSPSKILL = new System.Windows.Forms.CheckBox();
            this.rb_BeatMode_8 = new System.Windows.Forms.RadioButton();
            this.tb_HotKey_SwitchShootMode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckb_SaveScreenImg = new System.Windows.Forms.CheckBox();
            this.btn_SetKeyMatch = new System.Windows.Forms.Button();
            this.btn_SetLogForm = new System.Windows.Forms.Button();
            this.gb_Noise = new System.Windows.Forms.GroupBox();
            this.gb_Assist = new System.Windows.Forms.GroupBox();
            this.btn_Switch = new System.Windows.Forms.Button();
            this.tb_HotKey_Switch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ckb_ShowLogFrm = new System.Windows.Forms.CheckBox();
            this.tb_ScreenShotInterval = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_Run = new System.Windows.Forms.Button();
            this.tb_HotKey_Global = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.rb_Mode_Noise = new System.Windows.Forms.RadioButton();
            this.rb_Mode_BFS = new System.Windows.Forms.RadioButton();
            this.rb_Mode_Assist = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabp_BasicSetting = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_BeatMode_7 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_6 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_5 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_4 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_3 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_2 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_1 = new System.Windows.Forms.RadioButton();
            this.rb_BeatMode_0 = new System.Windows.Forms.RadioButton();
            this.gb_BFS = new System.Windows.Forms.GroupBox();
            this.ckb_BFL_AutoShot = new System.Windows.Forms.CheckBox();
            this.rb_BFS_APF_Enable = new System.Windows.Forms.RadioButton();
            this.rb_BFS_APF_Disable = new System.Windows.Forms.RadioButton();
            this.label34 = new System.Windows.Forms.Label();
            this.tb_BFS_PowerfulTime = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tb_BFS_CVZ_CX_time = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tabp_ImgSetting = new System.Windows.Forms.TabPage();
            this.RAS_MuseBar = new TFD_DJ_LUNA.Tools.RecognizeAreaSetting();
            this.RAS_Normal_Block = new TFD_DJ_LUNA.Tools.RecognizeAreaSetting();
            this.tabp_BFS = new System.Windows.Forms.TabPage();
            this.RAS_BFL_R = new TFD_DJ_LUNA.Tools.RecognizeAreaSetting();
            this.gb_Assist.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabp_BasicSetting.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_BFS.SuspendLayout();
            this.tabp_ImgSetting.SuspendLayout();
            this.tabp_BFS.SuspendLayout();
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
            this.rtb.Size = new System.Drawing.Size(400, 461);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // ckb_ManualSPSKILL
            // 
            this.ckb_ManualSPSKILL.AutoSize = true;
            this.ckb_ManualSPSKILL.Location = new System.Drawing.Point(10, 84);
            this.ckb_ManualSPSKILL.Name = "ckb_ManualSPSKILL";
            this.ckb_ManualSPSKILL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_ManualSPSKILL.Size = new System.Drawing.Size(135, 21);
            this.ckb_ManualSPSKILL.TabIndex = 9;
            this.ckb_ManualSPSKILL.Text = "：手动使用强化技能";
            this.toolTip1.SetToolTip(this.ckb_ManualSPSKILL, "灵感条充满后等待手动使用强化技能");
            this.ckb_ManualSPSKILL.UseVisualStyleBackColor = true;
            this.ckb_ManualSPSKILL.CheckedChanged += new System.EventHandler(this.ckb_ManualSPSKILL_CheckedChanged);
            // 
            // rb_BeatMode_8
            // 
            this.rb_BeatMode_8.AutoSize = true;
            this.rb_BeatMode_8.Location = new System.Drawing.Point(12, 238);
            this.rb_BeatMode_8.Name = "rb_BeatMode_8";
            this.rb_BeatMode_8.Size = new System.Drawing.Size(74, 21);
            this.rb_BeatMode_8.TabIndex = 0;
            this.rb_BeatMode_8.TabStop = true;
            this.rb_BeatMode_8.Tag = "8";
            this.rb_BeatMode_8.Text = "按照映射";
            this.toolTip1.SetToolTip(this.rb_BeatMode_8, "检测到Q按C,C按V,V按Z,Z按C");
            this.rb_BeatMode_8.UseVisualStyleBackColor = true;
            this.rb_BeatMode_8.Visible = false;
            this.rb_BeatMode_8.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // tb_HotKey_SwitchShootMode
            // 
            this.tb_HotKey_SwitchShootMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_HotKey_SwitchShootMode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_HotKey_SwitchShootMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_HotKey_SwitchShootMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_HotKey_SwitchShootMode.Location = new System.Drawing.Point(163, 179);
            this.tb_HotKey_SwitchShootMode.Name = "tb_HotKey_SwitchShootMode";
            this.tb_HotKey_SwitchShootMode.ReadOnly = true;
            this.tb_HotKey_SwitchShootMode.Size = new System.Drawing.Size(80, 16);
            this.tb_HotKey_SwitchShootMode.TabIndex = 25;
            this.tb_HotKey_SwitchShootMode.Text = "F6";
            this.tb_HotKey_SwitchShootMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tb_HotKey_SwitchShootMode, "将打点方式临时切换为只按左键，再次使用则切换为原方式");
            this.tb_HotKey_SwitchShootMode.Click += new System.EventHandler(this.tb_HotKey_SwitchShootMode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 179);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "临时切换打点方式快捷键 ：";
            this.toolTip1.SetToolTip(this.label1, "将打点方式临时切换为只按左键，再次使用则切换为原方式");
            // 
            // ckb_SaveScreenImg
            // 
            this.ckb_SaveScreenImg.AutoSize = true;
            this.ckb_SaveScreenImg.Location = new System.Drawing.Point(167, 182);
            this.ckb_SaveScreenImg.Name = "ckb_SaveScreenImg";
            this.ckb_SaveScreenImg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_SaveScreenImg.Size = new System.Drawing.Size(87, 21);
            this.ckb_SaveScreenImg.TabIndex = 20;
            this.ckb_SaveScreenImg.Text = "：保存截屏";
            this.toolTip1.SetToolTip(this.ckb_SaveScreenImg, "调试用，正式使用请勿勾选");
            this.ckb_SaveScreenImg.UseVisualStyleBackColor = true;
            this.ckb_SaveScreenImg.CheckedChanged += new System.EventHandler(this.ckb_SaveScreenImg_CheckedChanged);
            // 
            // btn_SetKeyMatch
            // 
            this.btn_SetKeyMatch.BackColor = System.Drawing.Color.SlateGray;
            this.btn_SetKeyMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SetKeyMatch.FlatAppearance.BorderSize = 0;
            this.btn_SetKeyMatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_SetKeyMatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_SetKeyMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SetKeyMatch.ForeColor = System.Drawing.Color.White;
            this.btn_SetKeyMatch.Location = new System.Drawing.Point(346, 12);
            this.btn_SetKeyMatch.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SetKeyMatch.Name = "btn_SetKeyMatch";
            this.btn_SetKeyMatch.Size = new System.Drawing.Size(69, 23);
            this.btn_SetKeyMatch.TabIndex = 16;
            this.btn_SetKeyMatch.Text = "按键设置";
            this.toolTip1.SetToolTip(this.btn_SetKeyMatch, "若你的游戏键位改过，可在此处设置映射");
            this.btn_SetKeyMatch.UseVisualStyleBackColor = false;
            this.btn_SetKeyMatch.Click += new System.EventHandler(this.btn_SetKeyMatch_Click);
            // 
            // btn_SetLogForm
            // 
            this.btn_SetLogForm.BackColor = System.Drawing.Color.SlateGray;
            this.btn_SetLogForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SetLogForm.FlatAppearance.BorderSize = 0;
            this.btn_SetLogForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_SetLogForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_SetLogForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SetLogForm.ForeColor = System.Drawing.Color.White;
            this.btn_SetLogForm.Location = new System.Drawing.Point(419, 12);
            this.btn_SetLogForm.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SetLogForm.Name = "btn_SetLogForm";
            this.btn_SetLogForm.Size = new System.Drawing.Size(67, 23);
            this.btn_SetLogForm.TabIndex = 16;
            this.btn_SetLogForm.Text = "弹窗位置";
            this.toolTip1.SetToolTip(this.btn_SetLogForm, "设置日志弹窗的大小及位置");
            this.btn_SetLogForm.UseVisualStyleBackColor = false;
            this.btn_SetLogForm.Click += new System.EventHandler(this.btn_SetLogForm_Click);
            // 
            // gb_Noise
            // 
            this.gb_Noise.Location = new System.Drawing.Point(5, 242);
            this.gb_Noise.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Noise.Name = "gb_Noise";
            this.gb_Noise.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Noise.Size = new System.Drawing.Size(276, 223);
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
            this.gb_Assist.Location = new System.Drawing.Point(5, 242);
            this.gb_Assist.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Assist.Name = "gb_Assist";
            this.gb_Assist.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Assist.Size = new System.Drawing.Size(276, 223);
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
            this.btn_Switch.Location = new System.Drawing.Point(213, 39);
            this.btn_Switch.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Switch.Name = "btn_Switch";
            this.btn_Switch.Size = new System.Drawing.Size(41, 24);
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
            this.tb_HotKey_Switch.Location = new System.Drawing.Point(128, 43);
            this.tb_HotKey_Switch.Name = "tb_HotKey_Switch";
            this.tb_HotKey_Switch.ReadOnly = true;
            this.tb_HotKey_Switch.Size = new System.Drawing.Size(80, 16);
            this.tb_HotKey_Switch.TabIndex = 12;
            this.tb_HotKey_Switch.Text = "F6";
            this.tb_HotKey_Switch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HotKey_Switch.Click += new System.EventHandler(this.tb_HotKey_Switch_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "切换C/V快捷键 ：";
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
            this.groupBox5.Controls.Add(this.rb_Mode_BFS);
            this.groupBox5.Controls.Add(this.rb_Mode_Assist);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(5, 7);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(276, 229);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "全局设置";
            // 
            // ckb_ShowLogFrm
            // 
            this.ckb_ShowLogFrm.AutoSize = true;
            this.ckb_ShowLogFrm.Checked = true;
            this.ckb_ShowLogFrm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_ShowLogFrm.Location = new System.Drawing.Point(10, 182);
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
            this.tb_ScreenShotInterval.ForeColor = System.Drawing.Color.Maroon;
            this.tb_ScreenShotInterval.Location = new System.Drawing.Point(128, 61);
            this.tb_ScreenShotInterval.Name = "tb_ScreenShotInterval";
            this.tb_ScreenShotInterval.Size = new System.Drawing.Size(80, 16);
            this.tb_ScreenShotInterval.TabIndex = 18;
            this.tb_ScreenShotInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ScreenShotInterval.TextChanged += new System.EventHandler(this.tb_ScreenShotInterval_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(11, 61);
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
            this.btn_Run.Location = new System.Drawing.Point(213, 27);
            this.btn_Run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(46, 24);
            this.btn_Run.TabIndex = 16;
            this.btn_Run.Text = "启动";
            this.btn_Run.UseVisualStyleBackColor = false;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // tb_HotKey_Global
            // 
            this.tb_HotKey_Global.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_HotKey_Global.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_HotKey_Global.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_HotKey_Global.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_HotKey_Global.Location = new System.Drawing.Point(128, 31);
            this.tb_HotKey_Global.Name = "tb_HotKey_Global";
            this.tb_HotKey_Global.ReadOnly = true;
            this.tb_HotKey_Global.Size = new System.Drawing.Size(80, 16);
            this.tb_HotKey_Global.TabIndex = 15;
            this.tb_HotKey_Global.Text = "F5";
            this.tb_HotKey_Global.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HotKey_Global.Click += new System.EventHandler(this.tb_HotKey_Global_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 31);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(117, 17);
            this.label15.TabIndex = 14;
            this.label15.Text = "启用/停止 快捷键 ：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 96);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "运行模式：";
            // 
            // rb_Mode_Noise
            // 
            this.rb_Mode_Noise.AutoSize = true;
            this.rb_Mode_Noise.Checked = true;
            this.rb_Mode_Noise.Location = new System.Drawing.Point(80, 94);
            this.rb_Mode_Noise.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mode_Noise.Name = "rb_Mode_Noise";
            this.rb_Mode_Noise.Size = new System.Drawing.Size(74, 21);
            this.rb_Mode_Noise.TabIndex = 4;
            this.rb_Mode_Noise.TabStop = true;
            this.rb_Mode_Noise.Text = "噪音涌动";
            this.rb_Mode_Noise.UseVisualStyleBackColor = true;
            this.rb_Mode_Noise.CheckedChanged += new System.EventHandler(this.Rb_Mode_CheckedChanged);
            // 
            // rb_Mode_BFS
            // 
            this.rb_Mode_BFS.AutoSize = true;
            this.rb_Mode_BFS.Location = new System.Drawing.Point(80, 138);
            this.rb_Mode_BFS.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mode_BFS.Name = "rb_Mode_BFS";
            this.rb_Mode_BFS.Size = new System.Drawing.Size(86, 21);
            this.rb_Mode_BFS.TabIndex = 4;
            this.rb_Mode_BFS.Text = "战地演唱会";
            this.rb_Mode_BFS.UseVisualStyleBackColor = true;
            this.rb_Mode_BFS.CheckedChanged += new System.EventHandler(this.Rb_Mode_CheckedChanged);
            // 
            // rb_Mode_Assist
            // 
            this.rb_Mode_Assist.AutoSize = true;
            this.rb_Mode_Assist.Location = new System.Drawing.Point(80, 116);
            this.rb_Mode_Assist.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mode_Assist.Name = "rb_Mode_Assist";
            this.rb_Mode_Assist.Size = new System.Drawing.Size(62, 21);
            this.rb_Mode_Assist.TabIndex = 4;
            this.rb_Mode_Assist.Text = "辅助流";
            this.rb_Mode_Assist.UseVisualStyleBackColor = true;
            this.rb_Mode_Assist.CheckedChanged += new System.EventHandler(this.Rb_Mode_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox1.Controls.Add(this.rtb);
            this.groupBox1.Location = new System.Drawing.Point(492, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 483);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行日志";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabp_BasicSetting);
            this.tabControl1.Controls.Add(this.tabp_ImgSetting);
            this.tabControl1.Controls.Add(this.tabp_BFS);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(477, 499);
            this.tabControl1.TabIndex = 17;
            // 
            // tabp_BasicSetting
            // 
            this.tabp_BasicSetting.Controls.Add(this.groupBox2);
            this.tabp_BasicSetting.Controls.Add(this.groupBox5);
            this.tabp_BasicSetting.Controls.Add(this.gb_Noise);
            this.tabp_BasicSetting.Controls.Add(this.gb_Assist);
            this.tabp_BasicSetting.Controls.Add(this.gb_BFS);
            this.tabp_BasicSetting.Location = new System.Drawing.Point(4, 26);
            this.tabp_BasicSetting.Name = "tabp_BasicSetting";
            this.tabp_BasicSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_BasicSetting.Size = new System.Drawing.Size(469, 469);
            this.tabp_BasicSetting.TabIndex = 0;
            this.tabp_BasicSetting.Text = "打碟设置";
            this.tabp_BasicSetting.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_BeatMode_8);
            this.groupBox2.Controls.Add(this.rb_BeatMode_7);
            this.groupBox2.Controls.Add(this.rb_BeatMode_6);
            this.groupBox2.Controls.Add(this.rb_BeatMode_5);
            this.groupBox2.Controls.Add(this.rb_BeatMode_4);
            this.groupBox2.Controls.Add(this.rb_BeatMode_3);
            this.groupBox2.Controls.Add(this.rb_BeatMode_2);
            this.groupBox2.Controls.Add(this.rb_BeatMode_1);
            this.groupBox2.Controls.Add(this.rb_BeatMode_0);
            this.groupBox2.Location = new System.Drawing.Point(287, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 458);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "打点方式";
            // 
            // rb_BeatMode_7
            // 
            this.rb_BeatMode_7.AutoSize = true;
            this.rb_BeatMode_7.Location = new System.Drawing.Point(12, 211);
            this.rb_BeatMode_7.Name = "rb_BeatMode_7";
            this.rb_BeatMode_7.Size = new System.Drawing.Size(102, 21);
            this.rb_BeatMode_7.TabIndex = 0;
            this.rb_BeatMode_7.TabStop = true;
            this.rb_BeatMode_7.Tag = "7";
            this.rb_BeatMode_7.Text = "切换只按C或V";
            this.rb_BeatMode_7.UseVisualStyleBackColor = true;
            this.rb_BeatMode_7.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_6
            // 
            this.rb_BeatMode_6.AutoSize = true;
            this.rb_BeatMode_6.Location = new System.Drawing.Point(12, 184);
            this.rb_BeatMode_6.Name = "rb_BeatMode_6";
            this.rb_BeatMode_6.Size = new System.Drawing.Size(87, 21);
            this.rb_BeatMode_6.TabIndex = 0;
            this.rb_BeatMode_6.TabStop = true;
            this.rb_BeatMode_6.Tag = "6";
            this.rb_BeatMode_6.Text = "C\\V 交替按";
            this.rb_BeatMode_6.UseVisualStyleBackColor = true;
            this.rb_BeatMode_6.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_5
            // 
            this.rb_BeatMode_5.AutoSize = true;
            this.rb_BeatMode_5.Location = new System.Drawing.Point(12, 160);
            this.rb_BeatMode_5.Name = "rb_BeatMode_5";
            this.rb_BeatMode_5.Size = new System.Drawing.Size(124, 21);
            this.rb_BeatMode_5.TabIndex = 0;
            this.rb_BeatMode_5.TabStop = true;
            this.rb_BeatMode_5.Tag = "5";
            this.rb_BeatMode_5.Text = "左键\\C\\V\\Z轮流按";
            this.rb_BeatMode_5.UseVisualStyleBackColor = true;
            this.rb_BeatMode_5.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_4
            // 
            this.rb_BeatMode_4.AutoSize = true;
            this.rb_BeatMode_4.Location = new System.Drawing.Point(12, 133);
            this.rb_BeatMode_4.Name = "rb_BeatMode_4";
            this.rb_BeatMode_4.Size = new System.Drawing.Size(155, 21);
            this.rb_BeatMode_4.TabIndex = 0;
            this.rb_BeatMode_4.TabStop = true;
            this.rb_BeatMode_4.Tag = "4";
            this.rb_BeatMode_4.Text = "左键与C\\V\\Z之一轮流按";
            this.rb_BeatMode_4.UseVisualStyleBackColor = true;
            this.rb_BeatMode_4.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_3
            // 
            this.rb_BeatMode_3.AutoSize = true;
            this.rb_BeatMode_3.Location = new System.Drawing.Point(12, 106);
            this.rb_BeatMode_3.Name = "rb_BeatMode_3";
            this.rb_BeatMode_3.Size = new System.Drawing.Size(85, 21);
            this.rb_BeatMode_3.TabIndex = 0;
            this.rb_BeatMode_3.TabStop = true;
            this.rb_BeatMode_3.Tag = "3";
            this.rb_BeatMode_3.Text = "CVZ同时按";
            this.rb_BeatMode_3.UseVisualStyleBackColor = true;
            this.rb_BeatMode_3.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_2
            // 
            this.rb_BeatMode_2.AutoSize = true;
            this.rb_BeatMode_2.Location = new System.Drawing.Point(12, 79);
            this.rb_BeatMode_2.Name = "rb_BeatMode_2";
            this.rb_BeatMode_2.Size = new System.Drawing.Size(74, 21);
            this.rb_BeatMode_2.TabIndex = 0;
            this.rb_BeatMode_2.TabStop = true;
            this.rb_BeatMode_2.Tag = "2";
            this.rb_BeatMode_2.Text = "只按左键";
            this.rb_BeatMode_2.UseVisualStyleBackColor = true;
            this.rb_BeatMode_2.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_1
            // 
            this.rb_BeatMode_1.AutoSize = true;
            this.rb_BeatMode_1.Location = new System.Drawing.Point(12, 52);
            this.rb_BeatMode_1.Name = "rb_BeatMode_1";
            this.rb_BeatMode_1.Size = new System.Drawing.Size(153, 21);
            this.rb_BeatMode_1.TabIndex = 0;
            this.rb_BeatMode_1.TabStop = true;
            this.rb_BeatMode_1.Tag = "1";
            this.rb_BeatMode_1.Text = "左键与 同时按CVZ 交替";
            this.rb_BeatMode_1.UseVisualStyleBackColor = true;
            this.rb_BeatMode_1.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // rb_BeatMode_0
            // 
            this.rb_BeatMode_0.AutoSize = true;
            this.rb_BeatMode_0.Location = new System.Drawing.Point(12, 27);
            this.rb_BeatMode_0.Name = "rb_BeatMode_0";
            this.rb_BeatMode_0.Size = new System.Drawing.Size(99, 21);
            this.rb_BeatMode_0.TabIndex = 0;
            this.rb_BeatMode_0.TabStop = true;
            this.rb_BeatMode_0.Tag = "0";
            this.rb_BeatMode_0.Text = "C\\V\\Z 交替按";
            this.rb_BeatMode_0.UseVisualStyleBackColor = true;
            this.rb_BeatMode_0.CheckedChanged += new System.EventHandler(this.rb_BeatMode_Changed);
            // 
            // gb_BFS
            // 
            this.gb_BFS.Controls.Add(this.tb_HotKey_SwitchShootMode);
            this.gb_BFS.Controls.Add(this.label1);
            this.gb_BFS.Controls.Add(this.ckb_BFL_AutoShot);
            this.gb_BFS.Controls.Add(this.rb_BFS_APF_Enable);
            this.gb_BFS.Controls.Add(this.rb_BFS_APF_Disable);
            this.gb_BFS.Controls.Add(this.label34);
            this.gb_BFS.Controls.Add(this.tb_BFS_PowerfulTime);
            this.gb_BFS.Controls.Add(this.label33);
            this.gb_BFS.Controls.Add(this.tb_BFS_CVZ_CX_time);
            this.gb_BFS.Controls.Add(this.label27);
            this.gb_BFS.Location = new System.Drawing.Point(5, 242);
            this.gb_BFS.Margin = new System.Windows.Forms.Padding(4);
            this.gb_BFS.Name = "gb_BFS";
            this.gb_BFS.Padding = new System.Windows.Forms.Padding(4);
            this.gb_BFS.Size = new System.Drawing.Size(276, 223);
            this.gb_BFS.TabIndex = 6;
            this.gb_BFS.TabStop = false;
            this.gb_BFS.Text = "战地演唱会";
            // 
            // ckb_BFL_AutoShot
            // 
            this.ckb_BFL_AutoShot.AutoSize = true;
            this.ckb_BFL_AutoShot.Location = new System.Drawing.Point(14, 138);
            this.ckb_BFL_AutoShot.Name = "ckb_BFL_AutoShot";
            this.ckb_BFL_AutoShot.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckb_BFL_AutoShot.Size = new System.Drawing.Size(183, 21);
            this.ckb_BFL_AutoShot.TabIndex = 23;
            this.ckb_BFL_AutoShot.Text = "自动使用强化技能后自动开枪";
            this.ckb_BFL_AutoShot.UseVisualStyleBackColor = true;
            this.ckb_BFL_AutoShot.CheckedChanged += new System.EventHandler(this.ckb_BFL_AutoShot_CheckedChanged);
            // 
            // rb_BFS_APF_Enable
            // 
            this.rb_BFS_APF_Enable.AutoSize = true;
            this.rb_BFS_APF_Enable.Location = new System.Drawing.Point(188, 109);
            this.rb_BFS_APF_Enable.Name = "rb_BFS_APF_Enable";
            this.rb_BFS_APF_Enable.Size = new System.Drawing.Size(50, 21);
            this.rb_BFS_APF_Enable.TabIndex = 22;
            this.rb_BFS_APF_Enable.TabStop = true;
            this.rb_BFS_APF_Enable.Text = "启用";
            this.rb_BFS_APF_Enable.UseVisualStyleBackColor = true;
            this.rb_BFS_APF_Enable.CheckedChanged += new System.EventHandler(this.rb_BFS_APF_Disable_CheckedChanged);
            // 
            // rb_BFS_APF_Disable
            // 
            this.rb_BFS_APF_Disable.AutoSize = true;
            this.rb_BFS_APF_Disable.Location = new System.Drawing.Point(136, 109);
            this.rb_BFS_APF_Disable.Name = "rb_BFS_APF_Disable";
            this.rb_BFS_APF_Disable.Size = new System.Drawing.Size(50, 21);
            this.rb_BFS_APF_Disable.TabIndex = 22;
            this.rb_BFS_APF_Disable.TabStop = true;
            this.rb_BFS_APF_Disable.Text = "禁用";
            this.rb_BFS_APF_Disable.UseVisualStyleBackColor = true;
            this.rb_BFS_APF_Disable.CheckedChanged += new System.EventHandler(this.rb_BFS_APF_Disable_CheckedChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(15, 111);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(116, 17);
            this.label34.TabIndex = 21;
            this.label34.Text = "自动使用强化技能：";
            // 
            // tb_BFS_PowerfulTime
            // 
            this.tb_BFS_PowerfulTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_BFS_PowerfulTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_BFS_PowerfulTime.Location = new System.Drawing.Point(163, 71);
            this.tb_BFS_PowerfulTime.Name = "tb_BFS_PowerfulTime";
            this.tb_BFS_PowerfulTime.Size = new System.Drawing.Size(70, 16);
            this.tb_BFS_PowerfulTime.TabIndex = 20;
            this.tb_BFS_PowerfulTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_BFS_PowerfulTime.TextChanged += new System.EventHandler(this.tb_BFS_PowerfulTime_TextChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.OrangeRed;
            this.label33.Location = new System.Drawing.Point(15, 71);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(145, 17);
            this.label33.TabIndex = 19;
            this.label33.Text = "强化技能持续时间(ms) ：";
            // 
            // tb_BFS_CVZ_CX_time
            // 
            this.tb_BFS_CVZ_CX_time.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_BFS_CVZ_CX_time.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_BFS_CVZ_CX_time.Location = new System.Drawing.Point(163, 41);
            this.tb_BFS_CVZ_CX_time.Name = "tb_BFS_CVZ_CX_time";
            this.tb_BFS_CVZ_CX_time.Size = new System.Drawing.Size(70, 16);
            this.tb_BFS_CVZ_CX_time.TabIndex = 20;
            this.tb_BFS_CVZ_CX_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_BFS_CVZ_CX_time.TextChanged += new System.EventHandler(this.tb_BFS_CVZ_CX_time_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 41);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(145, 17);
            this.label27.TabIndex = 19;
            this.label27.Text = "普通技能持续时间(ms) ：";
            // 
            // tabp_ImgSetting
            // 
            this.tabp_ImgSetting.Controls.Add(this.RAS_MuseBar);
            this.tabp_ImgSetting.Controls.Add(this.RAS_Normal_Block);
            this.tabp_ImgSetting.Location = new System.Drawing.Point(4, 26);
            this.tabp_ImgSetting.Name = "tabp_ImgSetting";
            this.tabp_ImgSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_ImgSetting.Size = new System.Drawing.Size(469, 469);
            this.tabp_ImgSetting.TabIndex = 1;
            this.tabp_ImgSetting.Text = "识图设置";
            this.tabp_ImgSetting.UseVisualStyleBackColor = true;
            // 
            // RAS_MuseBar
            // 
            this.RAS_MuseBar.AREA_H = 0;
            this.RAS_MuseBar.AREA_W = 0;
            this.RAS_MuseBar.AREA_X = 0;
            this.RAS_MuseBar.AREA_Y = 0;
            this.RAS_MuseBar.BackColor = System.Drawing.Color.White;
            this.RAS_MuseBar.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.RAS_MuseBar.ImgMode = 0;
            this.RAS_MuseBar.Location = new System.Drawing.Point(5, 236);
            this.RAS_MuseBar.Margin = new System.Windows.Forms.Padding(4);
            this.RAS_MuseBar.MaskMode = 0;
            this.RAS_MuseBar.Name = "RAS_MuseBar";
            this.RAS_MuseBar.Padding = new System.Windows.Forms.Padding(1);
            this.RAS_MuseBar.RealScreenWidth = 1920;
            this.RAS_MuseBar.ScaleMode = 0;
            this.RAS_MuseBar.Size = new System.Drawing.Size(457, 225);
            this.RAS_MuseBar.TabIndex = 18;
            this.RAS_MuseBar.Threshold = 0.56D;
            this.RAS_MuseBar.TitleText = "灵感条识别区域";
            this.RAS_MuseBar.P_ImgModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_ImgModeChanged);
            this.RAS_MuseBar.P_MaskModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_MaskModeChanged);
            this.RAS_MuseBar.P_ScaleModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_ScaleModeChanged);
            this.RAS_MuseBar.P_RealScreenWidthChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_RealScreenWidthChanged);
            this.RAS_MuseBar.P_ThresholdChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_ThresholdChanged);
            this.RAS_MuseBar.P_RecognizeAreaChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_MuseBar_P_RecognizeAreaChanged);
            // 
            // RAS_Normal_Block
            // 
            this.RAS_Normal_Block.AREA_H = 0;
            this.RAS_Normal_Block.AREA_W = 0;
            this.RAS_Normal_Block.AREA_X = 0;
            this.RAS_Normal_Block.AREA_Y = 0;
            this.RAS_Normal_Block.BackColor = System.Drawing.Color.White;
            this.RAS_Normal_Block.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.RAS_Normal_Block.ImgMode = 0;
            this.RAS_Normal_Block.Location = new System.Drawing.Point(5, 7);
            this.RAS_Normal_Block.Margin = new System.Windows.Forms.Padding(4);
            this.RAS_Normal_Block.MaskMode = 0;
            this.RAS_Normal_Block.Name = "RAS_Normal_Block";
            this.RAS_Normal_Block.Padding = new System.Windows.Forms.Padding(1);
            this.RAS_Normal_Block.RealScreenWidth = 1920;
            this.RAS_Normal_Block.ScaleMode = 0;
            this.RAS_Normal_Block.Size = new System.Drawing.Size(457, 225);
            this.RAS_Normal_Block.TabIndex = 18;
            this.RAS_Normal_Block.Threshold = 0.56D;
            this.RAS_Normal_Block.TitleText = "菱形图案识别区域";
            this.RAS_Normal_Block.P_ImgModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_ImgModeChanged);
            this.RAS_Normal_Block.P_MaskModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_MaskModeChanged);
            this.RAS_Normal_Block.P_ScaleModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_ScaleModeChanged);
            this.RAS_Normal_Block.P_RealScreenWidthChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_RealScreenWidthChanged);
            this.RAS_Normal_Block.P_ThresholdChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_ThresholdChanged);
            this.RAS_Normal_Block.P_RecognizeAreaChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_Normal_Block_P_RecognizeAreaChanged);
            // 
            // tabp_BFS
            // 
            this.tabp_BFS.Controls.Add(this.RAS_BFL_R);
            this.tabp_BFS.Location = new System.Drawing.Point(4, 26);
            this.tabp_BFS.Name = "tabp_BFS";
            this.tabp_BFS.Size = new System.Drawing.Size(469, 469);
            this.tabp_BFS.TabIndex = 2;
            this.tabp_BFS.Text = "战地演唱会识图设置";
            this.tabp_BFS.UseVisualStyleBackColor = true;
            // 
            // RAS_BFL_R
            // 
            this.RAS_BFL_R.AREA_H = 0;
            this.RAS_BFL_R.AREA_W = 0;
            this.RAS_BFL_R.AREA_X = 0;
            this.RAS_BFL_R.AREA_Y = 0;
            this.RAS_BFL_R.BackColor = System.Drawing.Color.White;
            this.RAS_BFL_R.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.RAS_BFL_R.ImgMode = 0;
            this.RAS_BFL_R.Location = new System.Drawing.Point(5, 7);
            this.RAS_BFL_R.Margin = new System.Windows.Forms.Padding(4);
            this.RAS_BFL_R.MaskMode = 0;
            this.RAS_BFL_R.Name = "RAS_BFL_R";
            this.RAS_BFL_R.Padding = new System.Windows.Forms.Padding(1);
            this.RAS_BFL_R.RealScreenWidth = 1920;
            this.RAS_BFL_R.ScaleMode = 0;
            this.RAS_BFL_R.Size = new System.Drawing.Size(457, 225);
            this.RAS_BFL_R.TabIndex = 11;
            this.RAS_BFL_R.Threshold = 0.56D;
            this.RAS_BFL_R.TitleText = "右箭头识别区域";
            this.RAS_BFL_R.P_ImgModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_ImgModeChanged);
            this.RAS_BFL_R.P_MaskModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_MaskModeChanged);
            this.RAS_BFL_R.P_ScaleModeChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_ScaleModeChanged);
            this.RAS_BFL_R.P_RealScreenWidthChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_RealScreenWidthChanged);
            this.RAS_BFL_R.P_ThresholdChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_ThresholdChanged);
            this.RAS_BFL_R.P_RecognizeAreaChanged += new TFD_DJ_LUNA.Tools.RecognizeAreaSetting.ParamsValueChangedDelegate(this.RAS_BFL_R_P_RecognizeAreaChanged);
            // 
            // frm_Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(910, 518);
            this.Controls.Add(this.btn_SetLogForm);
            this.Controls.Add(this.btn_SetKeyMatch);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frm_Index";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TFD露娜自动打碟";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb_Assist.ResumeLayout(false);
            this.gb_Assist.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabp_BasicSetting.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_BFS.ResumeLayout(false);
            this.gb_BFS.PerformLayout();
            this.tabp_ImgSetting.ResumeLayout(false);
            this.tabp_BFS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gb_Noise;
        private System.Windows.Forms.GroupBox gb_Assist;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckb_ShowLogFrm;
        private System.Windows.Forms.CheckBox ckb_SaveScreenImg;
        private System.Windows.Forms.Button btn_SetKeyMatch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabp_BasicSetting;
        private System.Windows.Forms.TabPage tabp_ImgSetting;
        private System.Windows.Forms.RadioButton rb_Mode_BFS;
        private System.Windows.Forms.GroupBox gb_BFS;
        private System.Windows.Forms.TabPage tabp_BFS;
        private System.Windows.Forms.TextBox tb_BFS_PowerfulTime;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tb_BFS_CVZ_CX_time;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.RadioButton rb_BFS_APF_Enable;
        private System.Windows.Forms.RadioButton rb_BFS_APF_Disable;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btn_SetLogForm;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_BeatMode_8;
        private System.Windows.Forms.RadioButton rb_BeatMode_7;
        private System.Windows.Forms.RadioButton rb_BeatMode_6;
        private System.Windows.Forms.RadioButton rb_BeatMode_5;
        private System.Windows.Forms.RadioButton rb_BeatMode_4;
        private System.Windows.Forms.RadioButton rb_BeatMode_3;
        private System.Windows.Forms.RadioButton rb_BeatMode_2;
        private System.Windows.Forms.RadioButton rb_BeatMode_1;
        private System.Windows.Forms.RadioButton rb_BeatMode_0;
        private System.Windows.Forms.CheckBox ckb_BFL_AutoShot;
        private Tools.RecognizeAreaSetting RAS_BFL_R;
        private Tools.RecognizeAreaSetting RAS_Normal_Block;
        private Tools.RecognizeAreaSetting RAS_MuseBar;
        private System.Windows.Forms.TextBox tb_HotKey_SwitchShootMode;
        private System.Windows.Forms.Label label1;
    }
}

