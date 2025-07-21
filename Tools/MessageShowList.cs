using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFD_DJ_LUNA.Tools
{
    public static class MessageShowList
    {
        public delegate void SendNoticeMsgDelegate(string msg, int noticeLevel);
        /// <summary>
        /// 往外发送消息，用于主动往其他界面log控件写入信息
        /// </summary>
        public static event SendNoticeMsgDelegate SendNotice;
        /// <summary>
        /// 往外发送信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="noticeLevel">信息提醒等级,默认1级</param>
        public static void SendEventMsg(string msg, int noticeLevel = 1)
        {
            if (SendNotice != null)
                SendNotice(msg, noticeLevel);
        }

        /// <summary>
        /// 通知某一对象自身某操作已完成
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="SaveSucceed"></param>
        /// <param name="Receiver"></param>
        /// <param name="ExtraInfo"></param>
        public delegate void SaveCompletedDelegate(object Sender, bool SaveSucceed, object Receiver = null, string ExtraInfo = "");
        /// <summary>
        /// 通知某一对象自身某操作已完成
        /// </summary>
        public static event SaveCompletedDelegate SaveCompleted;
        /// <summary>
        /// 通知某一对象自身某操作已完成
        /// </summary>
        /// <param name="Sender">消息源控件</param>
        /// <param name="SaveSucceed">操作成功状态</param>
        /// <param name="Receiver">要通知的目标控件，null即为广播</param>
        /// <param name="ExtraInfo">额外信息</param>
        public static void SendSaveCompletedMsg(object Sender, bool SaveSucceed, object Receiver = null, string ExtraInfo = "")
        {
            if (SaveCompleted != null)
                SaveCompleted(Sender, SaveSucceed, Receiver, ExtraInfo);
        }
    }
}
