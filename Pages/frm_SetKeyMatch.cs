using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public partial class frm_SetKeyMatch : Form
    {
        public frm_SetKeyMatch()
        {
            InitializeComponent();
        }

        private void frm_SetKeyMatch_Load(object sender, EventArgs e)
        {
            LoadKeys();
        }

        private void LoadKeys()
        {
            try
            {
                TFD_LUNA.LoaddicSkillKey();
                if (TFD_LUNA.dicSkillKey == null || TFD_LUNA.dicSkillKey.Count < 1)
                    return;

                tb_K_Q.Text = TFD_LUNA.dicSkillKey.ContainsKey("Q") ? TFD_LUNA.dicSkillKey["Q"] : "Q";
                tb_K_C.Text = TFD_LUNA.dicSkillKey.ContainsKey("C") ? TFD_LUNA.dicSkillKey["C"] : "C";
                tb_K_V.Text = TFD_LUNA.dicSkillKey.ContainsKey("V") ? TFD_LUNA.dicSkillKey["V"] : "V";
                tb_K_Z.Text = TFD_LUNA.dicSkillKey.ContainsKey("Z") ? TFD_LUNA.dicSkillKey["Z"] : "Z";
            }
            catch (Exception ex)
            {
                ToastMessageBox.Show("加载按键映射失败！" + ex.Message, 0);
            }
        }

        private void btn_K_Q_Click(object sender, EventArgs e) { OpenKeysFrom(sender); }

        private void OpenKeysFrom(object obj)
        {
            if (obj == null)
                return;

            Button btn = obj as Button;
            string _k = Common.GetObjStr(btn.Tag, false);

            string _oldkey = "";
            switch (_k)
            {
                case "Q": _oldkey = tb_K_Q.Text; break;
                case "C": _oldkey = tb_K_C.Text; break;
                case "V": _oldkey = tb_K_V.Text; break;
                case "Z": _oldkey = tb_K_Z.Text; break;
            }

            frm_Allkeys f = new frm_Allkeys(_oldkey);
            if (f.ShowDialog() == DialogResult.OK)
            {
                string newKey = f.SelectedKey;
                switch (_k)
                {
                    case "Q": tb_K_Q.Text = newKey; break;
                    case "C": tb_K_C.Text = newKey; break;
                    case "V": tb_K_V.Text = newKey; break;
                    case "Z": tb_K_Z.Text = newKey; break;
                }

                f.Dispose();
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e) { Save(true); }
        private void btn_Save_Click(object sender, EventArgs e) { Save(); }

        private void Save(bool reset = false)
        {
            try
            {
                string k_Q = reset ? "Q" : string.IsNullOrWhiteSpace(tb_K_Q.Text) ? "Q" : tb_K_Q.Text;
                string k_C = reset ? "C" : string.IsNullOrWhiteSpace(tb_K_C.Text) ? "C" : tb_K_C.Text;
                string k_V = reset ? "V" : string.IsNullOrWhiteSpace(tb_K_V.Text) ? "V" : tb_K_V.Text;
                string k_Z = reset ? "Z" : string.IsNullOrWhiteSpace(tb_K_Z.Text) ? "Z" : tb_K_Z.Text;

                Common.SaveIniParamVal("技能按键映射", "Q", k_Q);
                Common.SaveIniParamVal("技能按键映射", "C", k_C);
                Common.SaveIniParamVal("技能按键映射", "V", k_V);
                Common.SaveIniParamVal("技能按键映射", "Z", k_Z);

                LoadKeys();
                ToastMessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                ToastMessageBox.Show("保存失败！" + ex.Message, 0);
            }
        }
    }
}
