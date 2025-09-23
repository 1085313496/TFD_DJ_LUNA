using System;
using System.Drawing;
using System.Windows.Forms;

namespace TFD_DJ_LUNA.Tools
{
    public partial class RecognizeAreaSetting : UserControl
    {
        /// <summary>
        /// 显示标题
        /// </summary>
        public string TitleText
        {
            get { return gb.Text; }
            set { gb.Text = value; }
        }

        #region 识别区域
        /// <summary>
        /// 识别区域左上角顶点X坐标
        /// </summary>
        public int AREA_X
        {
            get { return int.TryParse(tb_X.Text, out int _x) ? _x : 0; }
            set { tb_X.Text = value.ToString(); }
        }
        /// <summary>
        /// 识别区域左上角顶点Y坐标
        /// </summary>
        public int AREA_Y
        {
            get { return int.TryParse(tb_Y.Text, out int _x) ? _x : 0; }
            set { tb_Y.Text = value.ToString(); }
        }
        /// <summary>
        /// 识别区域宽度
        /// </summary>
        public int AREA_W
        {
            get { return int.TryParse(tb_W.Text, out int _x) ? _x : 0; }
            set { tb_W.Text = value.ToString(); }
        }
        /// <summary>
        /// 识别区域高度
        /// </summary>
        public int AREA_H
        {
            get { return int.TryParse(tb_H.Text, out int _x) ? _x : 0; }
            set { tb_H.Text = value.ToString(); }
        }
        #endregion

        /// <summary>
        /// opencv识图时的图片格式 【-1：纯色块； 0：BGR； 1：灰度图； 2：HSV仅H通道； 3：HSV仅S通道；4：HSV_H+S通道】
        /// </summary>
        public int ImgMode
        {
            get
            {
                int _imgMode = 0;
                if (rg_ColorBlock.Checked)
                    _imgMode = -1;
                else if (rg_Gray.Checked)
                    _imgMode = 1;
                else if (rg_HSV_H.Checked)
                    _imgMode = 2;
                else if (rg_HSV_S.Checked)
                    _imgMode = 3;
                else if (rg_HSV_HS.Checked)
                    _imgMode = 4;
                else if (rg_BGR.Checked)
                    _imgMode = 0;

                return _imgMode;
            }
            set
            {
                switch (value)
                {
                    case -1: rg_ColorBlock.Checked = true; break;
                    case 1: rg_Gray.Checked = true; break;
                    case 2: rg_HSV_H.Checked = true; break;
                    case 3: rg_HSV_S.Checked = true; break;
                    case 4: rg_HSV_HS.Checked = true; break;
                    case 0:
                    default: rg_BGR.Checked = true; break;
                }
            }
        }
        /// <summary>
        /// 掩码设置 【1：自动生成掩码 2：使用手动生成的掩码 其他值：不使用掩码】
        /// </summary>
        public int MaskMode
        {
            get
            {
                int _maskMode = 0;
                if (rb_MaskMode_0.Checked)
                    _maskMode = 0;
                else if (rb_MaskMode_1.Checked)
                    _maskMode = 1;
                else if (rb_MaskMode_2.Checked)
                    _maskMode = 2;
                return _maskMode;
            }
            set
            {
                switch (value)
                {
                    case 1: rb_MaskMode_1.Checked = true; break;
                    case 2: rb_MaskMode_2.Checked = true; break;
                    case 0:
                    default: rb_MaskMode_0.Checked = true; break;
                }
            }
        }

        /// <summary>
        /// 缩放模板以便于在其他分辨率使用 【1：使用缩放 其他值：不使用缩放】
        /// </summary>
        public int ScaleMode
        {
            get { return ckb_ScaleMode.Checked ? 1 : 0; }
            set { ckb_ScaleMode.Checked = value == 1; }
        }
        /// <summary>
        /// 实际的游戏画面宽度 用于计算基于1080P的缩放倍率
        /// </summary>
        public int RealScreenWidth
        {
            get { return int.TryParse(tb_RealScreenWidth.Text, out int _i) ? _i : 1920; }
            set { tb_RealScreenWidth.Text = value.ToString(); }
        }
        /// <summary>
        /// 图案识别精度 【取值范围0-1】 缺省值0.55
        /// </summary>
        public double Threshold
        {
            get
            {
                double thd = tkb_thd.Value / 100.0;
                return thd;
            }
            set { tkb_thd.Value = (int)(value * 100); }
        }

        #region 事件
        public delegate void ParamsValueChangedDelegate(string ParamName, object ParamValue, object ExtraData = null);
        /// <summary>
        /// 图片处理格式变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_ImgModeChanged;
        /// <summary>
        /// 掩码设置变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_MaskModeChanged;
        /// <summary>
        /// 缩放模式变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_ScaleModeChanged;
        /// <summary>
        /// 游戏画面宽度变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_RealScreenWidthChanged;
        /// <summary>
        /// 匹配度变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_ThresholdChanged;
        /// <summary>
        /// 识别区域变更事件
        /// </summary>
        public event ParamsValueChangedDelegate P_RecognizeAreaChanged;
        #endregion

        /// <summary>
        /// 页面是否正在加载
        /// </summary>
        private bool PageLoading { get; set; } = false;

        public RecognizeAreaSetting() { InitializeComponent(); }
        public RecognizeAreaSetting(RecognizeSetting _rsobj)
        {
            InitializeComponent();

            if (_rsobj != null)
            {
                try
                {
                    PageLoading = true;

                    if (_rsobj.Area != null)
                    {
                        AREA_X = _rsobj.Area.X;
                        AREA_Y = _rsobj.Area.Y;
                        AREA_W = _rsobj.Area.Width;
                        AREA_H = _rsobj.Area.Height;
                    }

                    ImgMode = _rsobj.OpencvMode;
                    MaskMode = _rsobj.MaskMode;
                    ScaleMode = _rsobj.ScaleMode;
                    RealScreenWidth = _rsobj.RealScreenWidth;
                    Threshold = _rsobj.Threshold;
                }
                catch { }
                finally { PageLoading = false; }
            }
        }

        private void RecognizeAreaSetting_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_rsobj"></param>
        public void Init(RecognizeSetting _rsobj)
        {
            if (_rsobj != null)
            {
                try
                {
                    PageLoading = true;

                    if (_rsobj.Area != null)
                    {
                        AREA_X = _rsobj.Area.X;
                        AREA_Y = _rsobj.Area.Y;
                        AREA_W = _rsobj.Area.Width;
                        AREA_H = _rsobj.Area.Height;
                    }

                    ImgMode = _rsobj.OpencvMode;
                    MaskMode = _rsobj.MaskMode;
                    ScaleMode = _rsobj.ScaleMode;
                    RealScreenWidth = _rsobj.RealScreenWidth;
                    Threshold = _rsobj.Threshold;
                }
                catch { }
                finally { PageLoading = false; }
            }
        }

        /// <summary>
        /// 找到程序主页面，方便隐藏
        /// </summary>
        /// <returns></returns>
        public Form GetMainForm()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frm_Index")
                {
                    return f;
                }
            }
            return null;
        }

        /// <summary>
        /// 识别区域的值正在更改时不处罚事件 防止框选和手动填写 互相套娃  【1框选修改  2手动修改  0空闲】
        /// </summary>
        private int AreaValueUpdating { get; set; } = 0;

        private void btn_PickArea_Click(object sender, EventArgs e) { PickArea(); }
        private void PickArea()
        {
            Form fIndex = null;
            try
            {
                fIndex = GetMainForm();
                if (fIndex != null)
                    fIndex.Hide();

                frm_PickArea f = new frm_PickArea();
                f.TopLevel = true;
                f.Opacity = 0.35;
                f.PickedArea = new Rectangle(AREA_X, AREA_Y, AREA_W, AREA_H);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (fIndex != null)
                    {
                        fIndex.Show();
                        fIndex.BringToFront();
                    }

                    AreaValueUpdating = 1;
                    AREA_X = f.PickedArea.X;
                    AREA_Y = f.PickedArea.Y;
                    AREA_W = f.PickedArea.Width;
                    AREA_H = f.PickedArea.Height;
                    AreaValueUpdating = 0;

                    Rectangle rec = new Rectangle(AREA_X, AREA_Y, AREA_W, AREA_H);

                    if (!PageLoading && P_RecognizeAreaChanged != null)
                        P_RecognizeAreaChanged("Area", rec, 1);
                }
                else
                {
                    if (fIndex != null)
                    {
                        fIndex.Show();
                        fIndex.BringToFront();
                    }
                }
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("框选{0}识别区域时出现异常！{1}", TitleText, ex.Message)); }
            finally
            {
                if (fIndex != null)
                {
                    fIndex.Show();
                    fIndex.BringToFront();
                }
            }
        }



        private void tkb_thd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                double thd = tkb_thd.Value / 100.0;
                lb_thd.Text = string.Format("{0:0.00}", thd);

                if (!PageLoading && P_ThresholdChanged != null)
                    P_ThresholdChanged("Threshold", thd);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("修改{0}识别匹配度时出现异常！{1}", TitleText, ex.Message)); }
        }

        private void rg_ImgMode_Changed(object sender, EventArgs e)
        {
            try
            {
                RadioButton rb = sender as RadioButton;
                if (!rb.Checked)
                    return;

                int _val = Convert.ToInt32(Common.GetObj(rb.Tag, true));
                string _txt = rb.Text;

                if (!PageLoading && P_ImgModeChanged != null)
                    P_ImgModeChanged("ImgMode", _val, _txt);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("设置{0}图片处理模式时出现异常！{1}", TitleText, ex.Message)); }
        }

        private void rg_MaskMode_Changed(object sender, EventArgs e)
        {
            try
            {
                RadioButton rb = sender as RadioButton;
                if (!rb.Checked)
                    return;

                int _val = Convert.ToInt32(Common.GetObj(rb.Tag, true));
                string _txt = rb.Text;

                if (!PageLoading && P_MaskModeChanged != null)
                    P_MaskModeChanged("MaskMode", _val, _txt);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("设置{0}掩码模式时出现异常！{1}", TitleText, ex.Message)); }
        }

        private void ckb_ScaleMode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox ckb = sender as CheckBox;
                int _val = ckb.Checked ? 1 : 0;
                string _txt = ckb.Checked ? "使用缩放" : "禁用缩放";

                if (!PageLoading && P_ScaleModeChanged != null)
                    P_ScaleModeChanged("ScaleMode", _val, _txt);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("设置{0}缩放模式时出现异常！{1}", TitleText, ex.Message)); }
        }

        private void tb_RealScreenWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                int _val = int.TryParse(tb.Text, out int _i) ? _i : 1920;

                if (!PageLoading && P_RealScreenWidthChanged != null)
                    P_RealScreenWidthChanged("RealScreenWidth", _val);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("设置{0}游戏画面宽度时出现异常！{1}", TitleText, ex.Message)); }
        }

        private void tb_Area_Changed(object sender, EventArgs e)
        {
            if (AreaValueUpdating != 0)
                return;

            try
            {
                int x = AREA_X < 0 ? 0 : AREA_X;
                int y = AREA_Y < 0 ? 0 : AREA_Y;
                int w = AREA_W < 0 ? 50 : AREA_W;
                int h = AREA_H < 0 ? 50 : AREA_H;
                Rectangle rec = new Rectangle(x, y, w, h);

                if (!PageLoading && P_RecognizeAreaChanged != null)
                    P_RecognizeAreaChanged("Area", rec, 2);
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("修改{0}识别区域时出现异常！{1}", TitleText, ex.Message)); }
        }
    }
}
