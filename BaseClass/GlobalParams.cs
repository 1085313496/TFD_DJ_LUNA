using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    /// <summary>
    /// 全局参数
    /// </summary>
    public static class GlobalParams
    {
        /// <summary>
        /// 快捷键列表
        /// </summary>
        public static List<string> lsShortKey = new List<string>() { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };
        /// <summary>
        /// 匹配点名称列表
        /// </summary>
        public static List<string> lsMatchedPointName = new List<string>() { "MatchedPt", "MatchedPoint", "MATCHEDPT", "MATCHEDPOINT", "匹配点" };

        #region 虚拟按键代码
        #region
        /// <summary>
        /// 虚拟按键代码字典
        /// </summary>
        public static List<Dictionary<string, object>> lsVKeys = new List<Dictionary<string, object>>()
        {
            new Dictionary<string, object>(){{ "VKey", "LBUTTON" },{ "KeyCode", 0x01 },{ "Description", "鼠标左键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "RBUTTON" },{ "KeyCode", 0x02 },{ "Description", "鼠标右键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "CANCEL" },{ "KeyCode", 0x03 },{ "Description", "控制中断处理" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "MBUTTON" },{ "KeyCode", 0x04 },{ "Description", "鼠标中键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "XBUTTON1" },{ "KeyCode", 0x05 },{ "Description", "鼠标按钮X1" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "XBUTTON2" },{ "KeyCode", 0x06 },{ "Description", "鼠标按钮X2" },{ "Enable",true} },                                                  
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x07 },{ "Description", "保留" } },                                                  
            new Dictionary<string, object>(){{ "VKey", "BACK" },{ "KeyCode", 0x08 },{ "Description", "退格键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "TAB" },{ "KeyCode", 0x09 },{ "Description", "Tab键" } ,{ "Enable",true}},
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x0A },{ "Description", "保留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x0B },{ "Description", "保留" } },
            new Dictionary<string, object>(){{ "VKey", "CLEAR" },{ "KeyCode", 0x0C },{ "Description", "清除键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "RETURN" },{ "KeyCode", 0x0D },{ "Description", "Enter键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "SHIFT" },{ "KeyCode", 0x10 },{ "Description", "Shift键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "CONTROL" },{ "KeyCode", 0x11 },{ "Description", "Ctrl键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "MENU" },{ "KeyCode", 0x12 },{ "Description", "Alt键" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "PAUSE" },{ "KeyCode", 0x13 },{ "Description", "暂停键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "CAPITAL" },{ "KeyCode", 0x14 },{ "Description", "CAPSLOCK" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "KANA" },{ "KeyCode", 0x15 },{ "Description", "IME Kana 模式" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "HANGUL" },{ "KeyCode", 0x15 },{ "Description", "IME 韩语" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "IME_ON" },{ "KeyCode", 0x16 },{ "Description", "IME 打开" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "JUNJA" },{ "KeyCode", 0x17 },{ "Description", "IME 举字" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "FINAL" },{ "KeyCode", 0x18 },{ "Description", "IME 最终" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "HANJA" },{ "KeyCode", 0x19 },{ "Description", "IME 漢字" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "KANJI" },{ "KeyCode", 0x19 },{ "Description", "IME 日语" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "IME_OFF" },{ "KeyCode", 0x1A },{ "Description", "IME 关闭" } ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "ESCAPE" },{ "KeyCode", 0x1B },{ "Description", "ESC键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "CONVERT" },{ "KeyCode", 0x1C },{ "Description", "IME 转换" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "NONCONVERT" },{ "KeyCode", 0x1D },{ "Description", "IME 非转换" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "ACCEPT" },{ "KeyCode", 0x1E },{ "Description", "IME 接受" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "MODECHANGE" },{ "KeyCode", 0x1F },{ "Description", "IME 模式切换" } ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "SPACE" },{ "KeyCode", 0x20 },{ "Description", "空格键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "PRIOR" },{ "KeyCode", 0x21 },{ "Description", "Page Up键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NEXT" },{ "KeyCode", 0x22 },{ "Description", "Page Down键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "END" },{ "KeyCode", 0x23 },{ "Description", "End键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "HOME" },{ "KeyCode", 0x24 },{ "Description", "Home键" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "LEFT" },{ "KeyCode", 0x25 },{ "Description", "左箭头" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "UP" },{ "KeyCode", 0x26 },{ "Description", "上箭头" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "RIGHT" },{ "KeyCode", 0x27 },{ "Description", "右箭头" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "DOWN" },{ "KeyCode", 0x28 },{ "Description", "下箭头" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "SELECT" },{ "KeyCode", 0x29 },{ "Description", "选择键" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "PRINT" },{ "KeyCode", 0x2A },{ "Description", "PRINT键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "EXECUTE" },{ "KeyCode", 0x2B },{ "Description", "EXECUTE键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "SNAPSHOT" },{ "KeyCode", 0x2C },{ "Description", "PrintScreen键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "INSERT" },{ "KeyCode", 0x2D },{ "Description", "Insert键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "DELETE" },{ "KeyCode", 0x2E },{ "Description", "Delete键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "HELP" },{ "KeyCode", 0x2F },{ "Description", "帮助键" } ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "0" },{ "KeyCode", 0x30 },{ "Description", "0键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "1" },{ "KeyCode", 0x31 },{ "Description", "1键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "2" },{ "KeyCode", 0x32 },{ "Description", "2键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "3" },{ "KeyCode", 0x33 },{ "Description", "3键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "4" },{ "KeyCode", 0x34 },{ "Description", "4键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "5" },{ "KeyCode", 0x35 },{ "Description", "5键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "6" },{ "KeyCode", 0x36 },{ "Description", "6键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "7" },{ "KeyCode", 0x37 },{ "Description", "7键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "8" },{ "KeyCode", 0x38 },{ "Description", "8键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "9" },{ "KeyCode", 0x39 },{ "Description", "9键" },{ "Enable",true} },

            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3A },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3B },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3C },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3D },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3E },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x3F },{ "Description", "未定义" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x40 },{ "Description", "未定义" } },

            new Dictionary<string, object>(){{ "VKey", "A" },{ "KeyCode", 0x41 },{ "Description", "A键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "B" },{ "KeyCode", 0x42 },{ "Description", "B键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "C" },{ "KeyCode", 0x43 },{ "Description", "C键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "D" },{ "KeyCode", 0x44 },{ "Description", "D键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "E" },{ "KeyCode", 0x45 },{ "Description", "E键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F" },{ "KeyCode", 0x46 },{ "Description", "F键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "G" },{ "KeyCode", 0x47 },{ "Description", "G键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "H" },{ "KeyCode", 0x48 },{ "Description", "H键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "I" },{ "KeyCode", 0x49 },{ "Description", "I键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "J" },{ "KeyCode", 0x4A },{ "Description", "J键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "K" },{ "KeyCode", 0x4B },{ "Description", "K键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "L" },{ "KeyCode", 0x4C },{ "Description", "L键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "M" },{ "KeyCode", 0x4D },{ "Description", "M键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "N" },{ "KeyCode", 0x4E },{ "Description", "N键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "O" },{ "KeyCode", 0x4F },{ "Description", "O键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "P" },{ "KeyCode", 0x50 },{ "Description", "P键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "Q" },{ "KeyCode", 0x51 },{ "Description", "Q键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "R" },{ "KeyCode", 0x52 },{ "Description", "R键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "S" },{ "KeyCode", 0x53 },{ "Description", "S键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "T" },{ "KeyCode", 0x54 },{ "Description", "T键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "U" },{ "KeyCode", 0x55 },{ "Description", "U键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "V" },{ "KeyCode", 0x56 },{ "Description", "V键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "W" },{ "KeyCode", 0x57 },{ "Description", "W键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "X" },{ "KeyCode", 0x58 },{ "Description", "X键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "Y" },{ "KeyCode", 0x59 },{ "Description", "Y键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "Z" },{ "KeyCode", 0x5A },{ "Description", "Z键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "LWIN" },{ "KeyCode", 0x5B },{ "Description", "左Windows键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "RWIN" },{ "KeyCode", 0x5C },{ "Description", "右Windows键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "APPS" },{ "KeyCode", 0x5D },{ "Description", "应用程序密钥" }  ,{ "Enable",true}},
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x5E },{ "Description", "预留" } },
            new Dictionary<string, object>(){{ "VKey", "SLEEP" },{ "KeyCode", 0x5F },{ "Description", "睡眠键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "NUMPAD0" },{ "KeyCode", 0x60 },{ "Description", "数字键盘0键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NUMPAD1" },{ "KeyCode", 0x61 },{ "Description", "数字键盘1键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NUMPAD2" },{ "KeyCode", 0x62 },{ "Description", "数字键盘2键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NUMPAD3" },{ "KeyCode", 0x63 },{ "Description", "数字键盘3键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "NUMPAD4" },{ "KeyCode", 0x64 },{ "Description", "数字键盘4键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "NUMPAD5" },{ "KeyCode", 0x65 },{ "Description", "数字键盘5键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "NUMPAD6" },{ "KeyCode", 0x66 },{ "Description", "数字键盘6键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NUMPAD7" },{ "KeyCode", 0x67 },{ "Description", "数字键盘7键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "NUMPAD8" },{ "KeyCode", 0x68 },{ "Description", "数字键盘8键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "NUMPAD9" },{ "KeyCode", 0x69 },{ "Description", "数字键盘9键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "MULTIPLY" },{ "KeyCode", 0x6A },{ "Description", "数字键盘乘号键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "ADD" },{ "KeyCode", 0x6B },{ "Description", "数字键盘加号键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "SEPARATOR" },{ "KeyCode", 0x6C },{ "Description", "数字键盘分隔符键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "SUBTRACT" },{ "KeyCode", 0x6D },{ "Description", "数字键盘减号键" }  ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "DECIMAL" },{ "KeyCode", 0x6E },{ "Description", "数字键盘小数点键" } ,{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "DIVIDE" },{ "KeyCode", 0x6F },{ "Description", "数字键盘除号键" }  ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "F1" },{ "KeyCode", 0x70 },{ "Description", "F1键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F2" },{ "KeyCode", 0x71 },{ "Description", "F2键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F3" },{ "KeyCode", 0x72 },{ "Description", "F3键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F4" },{ "KeyCode", 0x73 },{ "Description", "F4键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F5" },{ "KeyCode", 0x74 },{ "Description", "F5键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F6" },{ "KeyCode", 0x75 },{ "Description", "F6键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F7" },{ "KeyCode", 0x76 },{ "Description", "F7键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F8" },{ "KeyCode", 0x77 },{ "Description", "F8键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F9" },{ "KeyCode", 0x78 },{ "Description", "F9键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F10" },{ "KeyCode", 0x79 },{ "Description", "F10键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F11" },{ "KeyCode", 0x7A },{ "Description", "F11键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F12" },{ "KeyCode", 0x7B },{ "Description", "F12键" } ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "F13" },{ "KeyCode", 0x7C },{ "Description", "F13键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F14" },{ "KeyCode", 0x7D },{ "Description", "F14键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F15" },{ "KeyCode", 0x7E },{ "Description", "F15键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F16" },{ "KeyCode", 0x7F },{ "Description", "F16键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "F17" },{ "KeyCode", 0x80 },{ "Description", "F17键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F18" },{ "KeyCode", 0x81 },{ "Description", "F18键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F19" },{ "KeyCode", 0x82 },{ "Description", "F19键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "F20" },{ "KeyCode", 0x83 },{ "Description", "F20键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "F21" },{ "KeyCode", 0x84 },{ "Description", "F21键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "F22" },{ "KeyCode", 0x85 },{ "Description", "F22键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "F23" },{ "KeyCode", 0x86 },{ "Description", "F23键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "F24" },{ "KeyCode", 0x87 },{ "Description", "F24键" },{ "Enable",true} },

            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x88 },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x89 },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8A },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8B },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8C },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8D },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8E },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x8F },{ "Description", "预留" } },

            new Dictionary<string, object>(){{ "VKey", "NUMLOCK" },{ "KeyCode", 0x90 },{ "Description", "数字键盘锁定键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "SCROLL" },{ "KeyCode", 0x91 },{ "Description", "滚动锁定键" },{ "Enable",true} },

            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x92 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x93 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x94 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x95 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x96 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x97 },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x98 },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x99 },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9A },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9B },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9C },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9D },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9E },{ "Description", "未分配" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0x9F },{ "Description", "未分配" } },

            new Dictionary<string, object>(){{ "VKey", "LSHIFT" },{ "KeyCode", 0xA0 },{ "Description", "左Shift键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "RSHIFT" },{ "KeyCode", 0xA1 },{ "Description", "右Shift键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "LCONTROL" },{ "KeyCode", 0xA2 },{ "Description", "左Ctrl键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "RCONTROL" },{ "KeyCode", 0xA3 },{ "Description", "右Ctrl键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "LMENU" },{ "KeyCode", 0xA4 },{ "Description", "左Alt键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "RMENU" },{ "KeyCode", 0xA5 },{ "Description", "右Alt键" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "BROWSER_BACK" },{ "KeyCode", 0xA6 },{ "Description", "浏览器后退键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "BROWSER_FORWARD" },{ "KeyCode", 0xA7 },{ "Description", "浏览器前进键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "BROWSER_REFRESH" },{ "KeyCode", 0xA8 },{ "Description", "浏览器刷新键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "BROWSER_STOP" },{ "KeyCode", 0xA9 },{ "Description", "浏览器停止键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "BROWSER_SEARCH" },{ "KeyCode", 0xAA },{ "Description", "浏览器搜索键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "BROWSER_FAVORITES" },{ "KeyCode", 0xAB },{ "Description", "浏览器收藏键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "BROWSER_HOME" },{ "KeyCode", 0xAC },{ "Description", "浏览器主页键" } ,{ "Enable",true}},

            new Dictionary<string, object>(){{ "VKey", "VOLUME_MUTE" },{ "KeyCode", 0xAD },{ "Description", "静音键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "VOLUME_DOWN" },{ "KeyCode", 0xAE },{ "Description", "音量减键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "VOLUME_UP" },{ "KeyCode", 0xAF },{ "Description", "音量加键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "MEDIA_NEXT_TRACK" },{ "KeyCode", 0xB0 },{ "Description", "下一首音乐键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "MEDIA_PREV_TRACK" },{ "KeyCode", 0xB1 },{ "Description", "上一首音乐键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "MEDIA_STOP" },{ "KeyCode", 0xB2 },{ "Description", "停止播放键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "MEDIA_PLAY_PAUSE" },{ "KeyCode", 0xB3 },{ "Description", "播放/暂停键" },{ "Enable",true} },

            new Dictionary<string, object>(){{ "VKey", "LAUNCH_MAIL" },{ "KeyCode", 0xB4 },{ "Description", "打开邮件键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "LAUNCH_MEDIA_SELECT" },{ "KeyCode", 0xB5 },{ "Description", "打开媒体选择键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "LAUNCH_APP1" },{ "KeyCode", 0xB6 },{ "Description", "打开应用程序1键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "LAUNCH_APP2" },{ "KeyCode", 0xB7 },{ "Description", "打开应用程序2键" } ,{ "Enable",true}},

            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xB8 },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xB9 },{ "Description", "预留" } },

            new Dictionary<string, object>(){{ "VKey", "OEM_1" },{ "KeyCode", 0xBA },{ "Description", "用于US标准键盘的逗号和小数点" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "OEM_PLUS" },{ "KeyCode", 0xBB },{ "Description", "用于US标准键盘的加号键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "OEM_COMMA" },{ "KeyCode", 0xBC },{ "Description", "用于US标准键盘的逗号键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "OEM_MINUS" },{ "KeyCode", 0xBD },{ "Description", "用于US标准键盘的减号键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_PERIOD" },{ "KeyCode", 0xBE },{ "Description", "用于US标准键盘的句号键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_2" },{ "KeyCode", 0xBF },{ "Description", "用于US标准键盘的问号和斜线键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_3" },{ "KeyCode", 0xC0 },{ "Description", "用于US标准键盘的波浪线键" },{ "Enable",true} },
           
            //0xC1~0xDA 预留
            
            new Dictionary<string, object>(){{ "VKey", "OEM_4" },{ "KeyCode", 0xDB },{ "Description", "用于US标准键盘的大括号和左方括号键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_5" },{ "KeyCode", 0xDC },{ "Description", "用于US标准键盘的反斜杠和下划线键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_6" },{ "KeyCode", 0xDD },{ "Description", "用于US标准键盘的大括号和右方括号键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "OEM_7" },{ "KeyCode", 0xDE },{ "Description", "用于US标准键盘的单引号和双引号键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "OEM_8" },{ "KeyCode", 0xDF },{ "Description", "用于US标准键盘的GRAVE键" },{ "Enable",true} },
             
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE0 },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE1 },{ "Description", "OEM 特有" } },
            
            new Dictionary<string, object>(){{ "VKey", "OEM_102" },{ "KeyCode", 0xE2 },{ "Description", "用于US标准键盘的“”和“”键" } ,{ "Enable",true}},
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE3 },{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE4 },{ "Description", "OEM 特有" } },
            new Dictionary<string, object>(){{ "VKey", "PROCESSKEY" },{ "KeyCode", 0xE5 },{ "Description", "IME PROCESS键" } ,{ "Enable",true}},
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE6 },{ "Description", "OEM 特有" } },
            new Dictionary<string, object>(){{ "VKey", "PACKET" },{ "KeyCode", 0xE7 },{ "Description", "用于将 Unicode 字符当作键击传递" },{ "Enable",true} },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE8 },{ "Description", "预留" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xE9},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xEA},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xEB},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xEC},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xED},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xEE},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xEF},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF0},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF1},{ "Description", "OEM 特有" } }, 
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF2},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF3},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF4},{ "Description", "OEM 特有" } },
            //new Dictionary<string, object>(){{ "VKey", "" },{ "KeyCode", 0xF5},{ "Description", "OEM 特有" } },
            new Dictionary<string, object>(){{ "VKey", "ATTN" },{ "KeyCode", 0xF6 },{ "Description", "ATTN键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "CRSEL" },{ "KeyCode", 0xF7 },{ "Description", "CRSEL键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "EXSEL" },{ "KeyCode", 0xF8 },{ "Description", "EXSEL键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "EREOF" },{ "KeyCode", 0xF9 },{ "Description", "EREOF键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "PLAY" },{ "KeyCode", 0xFA },{ "Description", "PLAY键" },{ "Enable",true} },
            new Dictionary<string, object>(){{ "VKey", "ZOOM" },{ "KeyCode", 0xFB },{ "Description", "ZOOM键" },{ "Enable",true} },
            //new Dictionary<string, object>(){{ "VKey", "NONAME" },{ "KeyCode", 0xFC },{ "Description", "NONAME键" } },
            new Dictionary<string, object>(){{ "VKey", "PA1" },{ "KeyCode", 0xFD },{ "Description", "PA1键" } ,{ "Enable",true}},
            new Dictionary<string, object>(){{ "VKey", "OEM_CLEAR" },{ "KeyCode", 0xFE },{ "Description", "清除键" }, { "Enable", true } }
        };

        /// <summary>
        /// 获取键盘按键信息
        /// </summary>
        /// <param name="vkey"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetKeyInfo(string vkey)
        {
            foreach (Dictionary<string, object> dic in lsVKeys)
            {
                if (dic["VKey"].ToString() == vkey)
                {
                    return dic;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取键盘按键码
        /// </summary>
        /// <param name="vkey"></param>
        /// <returns></returns>
        public static byte GetKeyCode(string vkey)
        {
            Dictionary<string, object> dic = GetKeyInfo(vkey);
            if (dic == null)
                return 0;
            object obj = dic["KeyCode"];
            int i = (int)obj;
            return (byte)i;
        }
        /// <summary>
        /// 获取键盘按键描述
        /// </summary>
        /// <param name="vkey"></param>
        /// <returns></returns>
        public static string GetDescription(string vkey)
        {
            Dictionary<string, object> dic = GetKeyInfo(vkey);
            if (dic == null)
                return "";
            return dic["Description"].ToString();
        }
        #endregion

        #endregion

        #region 关键词样式
        #region 关键词样式字典列表
        /// <summary>
        /// 关键词样式字典列表
        /// </summary>
        public static List<Dictionary<string, object>> lsWordsStyle = new List<Dictionary<string, object>>()
        {
            new Dictionary<string, object>()
            {
                { "Word", "KeyPress" },
                { "Color", Color.RoyalBlue },
                { "Bold", true },
                { "Type",1},
                { "Description", "按下按键并立即释放" },
                { "Order",1}
            },
            new Dictionary<string, object>()
            {
                { "Word", "KeyDown" },
                { "Color", Color.Green },
                { "Bold", true },
                { "Type",1},
                { "Description", "按住按键" },
                { "Order",2}
            },
            new Dictionary<string, object>()
            {
                { "Word", "KeyUp" },
                { "Color", Color.Red },
                { "Bold", true },
                { "Type",1},
                { "Description", "释放按键" },
                { "Order",3}
            },

            new Dictionary<string, object>()
            {
                { "Word", "Delay" },
                { "Color", Color.DarkGray },
                { "Bold", true },
                { "Type",1},
                { "Description", "延迟X毫秒" },
                { "Order",0}
            },

            new Dictionary<string, object>()
            {
                { "Word", "MouseClick" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "按下鼠标按键并立即释放" },
                { "Order",4}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseDown" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "按下鼠标按键" },
                { "Order",5}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseUp" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "释放鼠标按键" },
                { "Order",6}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseWhell" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "滑动鼠标滚轮" },
                { "Order",7}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseMove" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "移动鼠标" },
                { "Order",8}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseMove_A" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "移动鼠标带轨迹" },
                { "Order",9}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseMove_R" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "移动鼠标相对位置" },
                { "Order",10}
            }            ,
            new Dictionary<string, object>()
            {
                { "Word", "MouseMove_RA" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "移动鼠标相对位置带轨迹" },
                { "Order",11}
            },
            new Dictionary<string, object>()
            {
                { "Word", "MouseMoveToCenter" },
                { "Color", Color.DarkCyan },
                { "Bold", true },
                { "Type",1},
                { "Description", "移动鼠标到屏幕中间" },
                { "Order",12}
            },

            new Dictionary<string, object>()
            {
                { "Word", "(" },
                { "Color", Color.DarkSlateGray },
                { "Bold", false },
                { "Type",2},
                { "Description", "" },
                { "Order",999}
            },
            new Dictionary<string, object>()
            {
                { "Word", ")" },
                { "Color", Color.DarkSlateGray },
                { "Bold", false },
                { "Type",2},
                { "Description", "" },
                { "Order",9998}
            },
            new Dictionary<string, object>()
            {
                { "Word", "//" },
                { "Color", Color.Green },
                { "Bold", false },
                { "Type",2},
                { "Description", "" },
                { "Order",9999}
            }
        };
        #endregion

        public static string LocalDicKeysName = "dicKeys.txt";
        public static string LocalDicWordsStyleName = "dicWordsStyle.txt";
        /// <summary>
        /// 从txt文件读取字典  lsdictype: 1按键；2操作关键词
        /// </summary>
        /// <param name="lsdictype"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ReadlsDicFromTxt(int lsdictype, bool autoSort = true)
        {
            string npath = "";
            switch (lsdictype)
            {
                case 1: npath = Application.StartupPath + "\\" + LocalDicKeysName; break;
                case 2: npath = Application.StartupPath + "\\" + LocalDicWordsStyleName; break;
            }
            if (string.IsNullOrWhiteSpace(npath))
                return null;

            using (StreamReader sr = new StreamReader(npath, true))
            {
                string str = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();

                if (string.IsNullOrEmpty(str))
                    return null;

                try
                {
                    List<Dictionary<string, object>> ls = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(str);
                    GlobalParams.SortWordsStyleByOrder(ls);
                    return ls;
                }
                catch (Exception ex) { return null; }
            }
        }

        /// <summary>
        /// 将字典存为txt文件
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="txtName"></param>
        public static bool LsDicToJsonTxt(List<Dictionary<string, object>> ls, string txtName)
        {
            try
            {
                var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                string sJson = JsonConvert.SerializeObject(ls, settings);
                string npath = Application.StartupPath + "\\" + txtName;
                using (System.IO.StreamWriter sw = new StreamWriter(npath, false))
                {
                    sw.Write(sJson);
                    sw.Close();
                    sw.Dispose();
                }

                return true;
            }
            catch (Exception ex) { return false; }
        }

        public static bool LsDicToCSharpClassTxt(List<Dictionary<string, object>> ls, string txtName)
        {
            try
            {
                string str = "";
                foreach (Dictionary<string, object> dic in ls)
                {
                    str += "new Dictionary<string, object>()\r\n{\r\n";
                    foreach (KeyValuePair<string, object> kv in dic)
                    {
                        str += string.Format("{{\"{0}\", \"{1}\"}},\r\n", kv.Key, kv.Value);
                    }
                    str += "},\r\n";
                }
                if (string.IsNullOrWhiteSpace(str))
                    return false;

                string npath = Application.StartupPath + "\\" + txtName;
                using (System.IO.StreamWriter sw = new StreamWriter(npath, false))
                {
                    sw.Write(str);
                    sw.Close();
                    sw.Dispose();
                }

                return true;
            }
            catch (Exception ex) { return false; }
        }
        /// <summary>
        /// 为传入的列表按元素的某一个字段排序
        /// </summary>
        /// <param name="ls"></param>
        public static void SortWordsStyleByOrder(List<Dictionary<string, object>> ls, string orderFieldName = "Order")
        {
            try
            {
                ls.Sort((x, y) =>
                {
                    decimal orderX = Convert.ToDecimal(x[orderFieldName]);
                    decimal orderY = Convert.ToDecimal(y[orderFieldName]);
                    return orderX.CompareTo(orderY);
                });
            }
            catch { }
        }


        /// <summary>
        /// 设置文本样式
        /// </summary>
        /// <param name="_rtb"></param>
        public static void SetTextStyle(RichTextBox _rtb)
        {
            try
            {
                _rtb.Select(0, 0);
                _rtb.SelectAll();
                _rtb.SelectionFont = _rtb.Font;
                _rtb.SelectionColor = Color.Black;

                foreach (Dictionary<string, object> dic in lsWordsStyle)
                {
                    string word = dic["Word"].ToString();
                    Color cl = (Color)dic["Color"];
                    bool bold = (bool)dic["Bold"];
                    SetTextStyleRun(_rtb, word, cl, bold);
                }
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 设置文本样式
        /// </summary>
        /// <param name="_rtb"> </param>
        /// <param name="wordToHighlight"> </param>
        /// <param name="cl"></param>
        /// <param name="bold"></param>
        public static void SetTextStyleRun(RichTextBox _rtb, string wordToHighlight, Color cl, bool bold = false)
        {
            int startIndex = 0; // 查找的起始位置            
            _rtb.Select(0, 0);// 将光标移动到文本开始

            // 查找所有出现的单词并设置样式
            while (startIndex < _rtb.TextLength)
            {
                // 查找单词
                int wordIndex = _rtb.Find(wordToHighlight, startIndex, RichTextBoxFinds.None);

                // 如果找到了单词
                if (wordIndex != -1)
                {
                    _rtb.Select(wordIndex, wordToHighlight.Length);// 选择单词

                    // 设置字体
                    Font ft = _rtb.SelectionFont;
                    Font newFt = new Font(ft.FontFamily, ft.Size, bold ? FontStyle.Bold : FontStyle.Regular);
                    _rtb.SelectionFont = newFt;

                    _rtb.SelectionColor = cl; // 设置字体颜色

                    startIndex = wordIndex + wordToHighlight.Length;// 更新起始查找位置
                }
                else
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 从Resources清单提取图片
        /// </summary>
        /// <param name="imgName">图片名，无需扩展名</param>
        /// <returns></returns>
        public static Image GetImgFromRes(string imgName)
        {
            try
            {
                ///命名空间
                ResourceManager rm = new ResourceManager("InputSimulator_SendInput.Properties.Resources", Assembly.GetExecutingAssembly());
                Image img = (Image)rm.GetObject(imgName);
                return img;
            }
            catch { return null; }
        }

        /// <summary>
        /// 从文件加载指令内容
        /// </summary>
        /// <param name="_ScriptPath"></param>
        /// <returns></returns>
        public static string LoadScriptStr(string _ScriptPath)
        {
            try
            {
                if (string.IsNullOrEmpty(_ScriptPath))
                    return "";

                string _FilePath = string.Format("{0}\\{1}", Application.StartupPath, _ScriptPath);
                string str = "";
                using (System.IO.StreamReader sr = new StreamReader(_FilePath, true))
                {
                    str = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }

                return str;
            }
            catch (Exception ex) { return ""; }
        }

    }

    public class CustomEventArgs : EventArgs
    {
        public string Message { get; }
        public int Number { get; }

        public CustomEventArgs(string message, int number)
        {
            Message = message;
            Number = number;
        }
    }
}
