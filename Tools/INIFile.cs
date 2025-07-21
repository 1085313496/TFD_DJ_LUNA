using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TFD_DJ_LUNA.Tools
{
    public class INIFile
    {
        public string path;

        public INIFile(string INIPath) { path = INIPath; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, Byte[] retVal, int size, string filePath);

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value) { WritePrivateProfileString(Section, Key, Value, this.path); }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.path);
            return temp;

        }
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection() { IniWriteValue(null, null, null); }
        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        public void ClearSection(string Section) { IniWriteValue(Section, null, null); }

        #region 读取所有段落名
        /// <summary>
        /// 读取所有段落名
        /// </summary>
        /// <returns></returns>
        public List<string> ReadSections()
        {
            return ReadSections(path);
        }

        public List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }
        #endregion

        #region 读取指定段落名下所有键
        /// <summary>
        /// 读取指定段落名下所有键名
        /// </summary>
        /// <param name="SectionName">段落名</param>
        /// <returns></returns>
        public List<string> ReadKeys(string SectionName) { return ReadKeys(SectionName, path); }

        /// <summary>
        /// 读取指定段落名下所有键值
        /// </summary>
        /// <param name="SectionName">段落名</param>
        /// <returns></returns>
        public List<string> ReadKeysValue(string SectionName)
        {
            List<string> keys = ReadKeys(SectionName, path);
            return ReadKeysValue(SectionName, keys);
        }
        /// <summary>
        /// 读取指定段落名下所有键值对
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> ReadKeyValuePairs(string SectionName)
        {
            List<string> keys = ReadKeys(SectionName, path);
            return ReadKeyValuePairs(SectionName, keys);
        }
        /// <summary>
        /// 读取指定段落名下所有键值对
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadKeyValuePairsDic(string SectionName)
        {
            List<string> keys = ReadKeys(SectionName, path);
            return ReadKeyValuePairsDic(SectionName, keys);
        }

        public List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        public List<string> ReadKeysValue(string SectionName, List<string> lsKeys)
        {
            List<string> result = new List<string>();
            foreach (string k in lsKeys)
            {
                string str = IniReadValue(SectionName, k);
                result.Add(str);
            }
            return result;
        }

        public List<KeyValuePair<string, string>> ReadKeyValuePairs(string SectionName, List<string> lsKeys)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            foreach (string k in lsKeys)
            {
                string str = IniReadValue(SectionName, k);
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(k, str);
                result.Add(kvp);
            }
            return result;
        }

        public Dictionary<string, string> ReadKeyValuePairsDic(string SectionName, List<string> lsKeys)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string k in lsKeys)
            {
                string str = IniReadValue(SectionName, k);
                result.Add(k, str);
            }
            return result;
        }
        #endregion
    }
}
