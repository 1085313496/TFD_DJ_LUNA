using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TFD_DJ_LUNA.Tools
{
    public static class Common
    {
        #region 写日志记录
        public static void CreateLogDir(string dir)
        {
            string logDir = Application.StartupPath + "\\" + dir;
            if (!System.IO.Directory.Exists(logDir))
            {
                System.IO.Directory.CreateDirectory(logDir);
            }
        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="ErrorType">错误类型</param>
        /// <param name="e">错误对象</param>
        /// <param name="ExtraMsg">额外信息</param>
        public static void LogErr(string ErrorType, Exception e, string ExtraMsg = "")
        {
            if (e == null)
                return;

            WriteSysLog(e, ExtraMsg);

            try
            {
                string strfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Config.ini";
                INIFile getinifile = new INIFile(strfilepath);
                string AllowWriteError = getinifile.IniReadValue("系统配置", "AllowWriteError");     //是否允许写错误日志文件
                AllowWriteError = ObjIsNull(AllowWriteError) ? "0" : AllowWriteError;

                if (AllowWriteError == "1")
                {
                    string dirName = "Log";
                    string fileName = "";
                    CreateLogDir(dirName);
                    string newFilePath = string.Format("{0}\\{1}\\{2}{3}.log", Application.StartupPath, dirName, fileName, DateTime.Now.ToString("yyyy-MM-dd"));

                    using (System.IO.StreamWriter sw = new StreamWriter(newFilePath, true))
                    {
                        sw.WriteLine("错误类型：" + ErrorType);
                        sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine("错误信息：" + e.Message);
                        sw.WriteLine("堆栈信息：" + e.StackTrace);
                        sw.WriteLine("------------------------");
                        sw.WriteLine();
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// /// 写错误日志
        /// </summary>
        /// <param name="ErrorType">错误类型</param>
        /// <param name="content">错误信息</param>
        public static void LogErr(string ErrorType, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return;

            try
            {
                string strfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Config.ini";
                INIFile getinifile = new INIFile(strfilepath);
                string AllowWriteError = getinifile.IniReadValue("系统配置", "AllowWriteError");     //是否允许写错误日志文件
                AllowWriteError = ObjIsNull(AllowWriteError) ? "0" : AllowWriteError;
                if (AllowWriteError == "1")
                {
                    string dirName = "Log";
                    string fileName = "";
                    CreateLogDir(dirName);
                    string newFilePath = string.Format("{0}\\{1}\\{2}{3}.log", Application.StartupPath, dirName, fileName, DateTime.Now.ToString("yyyy-MM-dd"));
                    using (System.IO.StreamWriter sw = new StreamWriter(newFilePath, true))
                    {
                        sw.WriteLine("错误类型：" + ErrorType);
                        sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine("错误信息：" + content);
                        sw.WriteLine("------------------------");
                        sw.WriteLine();
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        /// <param name="dirName">所处文件夹</param>
        public static void WriteFile(string fileName, string content, string dirName = "MyFolder")
        {
            if (string.IsNullOrWhiteSpace(content))
                return;

            try
            {
                string strfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Config.ini";
                INIFile getinifile = new INIFile(strfilepath);
                string AllowWriteError = getinifile.IniReadValue("系统配置", "AllowWriteError");     //是否允许写错误日志文件
                AllowWriteError = ObjIsNull(AllowWriteError) ? "0" : AllowWriteError;
                AllowWriteError = "1";
                if (AllowWriteError == "1")
                {
                    CreateLogDir(dirName);
                    string newFilePath = string.Format("{0}\\{1}\\{2}{3}.txt", Application.StartupPath, dirName, fileName, DateTime.Now.ToString("yyyy-MM-dd"));
                    using (System.IO.StreamWriter sw = new StreamWriter(newFilePath, true))
                    {
                        sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
                        sw.WriteLine(content);
                        sw.WriteLine("------------------------");
                        sw.WriteLine();
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 将错误日志写入系统日志
        /// </summary>
        /// <param name="e">错误</param>
        /// <param name="ExtraMsg">额外信息</param>
        public static void WriteSysLog(Exception e, string ExtraMsg = "")
        {
            try
            {
                EventLog log = new EventLog();
                log.Source = "新排单通知程序";

                string str = string.IsNullOrWhiteSpace(ExtraMsg) ? "" : "ExtraMsg:\r\n" + ExtraMsg;
                log.WriteEntry(e.StackTrace + "\r\n" + e.Message + "\r\n" + str, EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 取非空值
        /// <summary>
        /// 传入的参数是否为空值/空字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ObjIsNull(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
                return true;
            return false;
        }
        /// <summary>
        /// 若传入的obj值为空值，根据isNum参数[是否为数字]返回0或空字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isNum"></param>
        /// <returns></returns>
        public static object GetObj(object obj, bool isNum = false)
        {
            if (ObjIsNull(obj))
                return isNum ? "0" : "";
            return obj;
        }
        public static string GetObjStr(object obj, bool isNum = false)
        {
            if (ObjIsNull(obj))
                return isNum ? "0" : "";
            return obj.ToString();
        }
        #endregion

        #region 打开框架子窗体
        /// <summary>
        /// 打开窗体
        /// </summary>
        /// <param name="frmName">窗体路径</param>
        /// <param name="ParentControl">窗体的父级控件【要在什么控件内打开目标窗体】</param>
        /// <param name="isFullpath">第一个参数是否为完整路径，默认值false，默认路径 UnattendedWeighSys.NewFunctionForms.{0} </param>
        public static void OpenFrmPage(string frmName, Object ParentControl, bool isFullpath = false)
        {
            try
            {
                //窗体类在项目中的完整路径
                string ClassPath = isFullpath ? frmName : string.Format(@"UnattendedWeighSys.NewFunctionForms.{0}", frmName);
                var fl = Assembly.GetExecutingAssembly().CreateInstance(ClassPath);
                Form f = fl as Form;

                if (!frmIsOpen(frmName))
                    SetFromParas(f, ParentControl);
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 设置窗体参数
        /// </summary>
        /// <param name="f">窗体</param>
        /// <param name="ParentControl">父级控件</param>
        /// <returns></returns>
        public static Form SetFromParas(Form f, Object ParentControl)
        {
            if (ParentControl != null)
            {
                Control ParentPanel = ParentControl as Control;
                f.Size = ParentPanel.Size;
                f.TopLevel = false;
                f.Parent = ParentPanel;
                f.Dock = DockStyle.Fill;
                f.FormBorderStyle = FormBorderStyle.None;
                f.BringToFront();
                f.Show();
            }
            else
            {
                f.BringToFront();
                f.Show();
            }
            return f;
        }
        /// <summary>
        /// 检查指定名称的窗体是否已打开，如已打开，则最大化显示
        /// </summary>
        /// <param name="formName">窗体类名</param>
        /// <returns></returns>
        public static bool frmIsOpen(string formName)
        {
            try
            {
                List<Form> ls = new List<Form>();
                foreach (Form f in Application.OpenForms)
                    ls.Add(f);
                foreach (Form frm in ls.ToArray())
                {
                    if (frm.Name == formName)
                    {
                        frm.Show();
                        frm.BringToFront();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogErr("SysException", ex);
                return false;
            }
        }
        #endregion

        #region  时间戳转换
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="digit">精度 10位秒；13位毫秒</param>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int digit = 10)
        {
            long ts = ConvertDateTimeToInt(time, digit);
            return ts.ToString();
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <param name="digit">精度 10位秒；13位毫秒</param>
        /// <returns></returns>
        public static long ConvertDateTimeToInt(System.DateTime time, int digit = 10)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks);
            switch (digit)
            {
                case 10: t /= 10000000; break;
                case 13: t /= 10000; break;
                default: t /= 10000000; break;
            }
            //long t = (time.Ticks - startTime.Ticks)/10000;  //除10000调整为13位  
            //long t = (time.Ticks - startTime.Ticks) / 10000000;  //除10000000调整为10位  
            return t;
        }
        /// <summary>   
        /// 时间戳转为C#格式时间   
        /// </summary>   
        /// <param name=”timeStamp”></param>   
        /// <returns></returns>   
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion


        /// <summary>
        /// 读取配置参数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="IniPatn"></param>
        /// <returns></returns>
        public static string GetIniParamVal(string section, string key, string IniPatn = "")
        {
            try
            {
                string iniPath = IniPatn == "" ? System.AppDomain.CurrentDomain.BaseDirectory + "Config\\Config.ini" : IniPatn;
                INIFile iniConfig = new INIFile(iniPath);
                return iniConfig.IniReadValue(section, key);
            }
            catch (Exception ex) { return ""; }
        }
        /// <summary>
        /// 保存配置参数值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="IniPatn"></param>
        public static void SaveIniParamVal(string section, string key, string val, string IniPatn = "")
        {
            try
            {
                string iniPath = IniPatn == "" ? System.AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Config.ini" : IniPatn;
                INIFile iniConfig = new INIFile(iniPath);
                iniConfig.IniWriteValue(section, key, val);
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 安全散列算法1 转换字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string GetSHA1(string str)
        {
            //建立SHA1对象bai
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成duzhibyte[]
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结dao果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }


        /// <summary>
        /// 文件转byte[]
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static byte[] ConvertFileStreamToByteArray(FileStream fileStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 壓縮圖片
        /// </summary>
        /// <param name="fileStream">圖片流</param>
        /// <param name="quality">壓縮質量0-100之間 數值越大質量越高</param>
        /// <returns></returns>
        public static byte[] CompressionImage(Stream fileStream, long quality)
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileStream))
            {
                using (Bitmap bitmap = new Bitmap(img))
                {
                    ImageCodecInfo CodecInfo = GetEncoder(img.RawFormat);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, CodecInfo, myEncoderParameters);
                        myEncoderParameters.Dispose();
                        myEncoderParameter.Dispose();
                        return ms.ToArray();
                    }
                }
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }


        /// <summary>
        /// 为传入的列表按元素的某一个数字类型的字段排序
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="orderFieldName"></param>
        public static void SortListByDecimalField(List<Dictionary<string, object>> ls, string orderFieldName = "Order")
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
    }
}
