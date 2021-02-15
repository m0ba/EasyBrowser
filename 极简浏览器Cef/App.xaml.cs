﻿using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static string[] BadSectence = {"fuck", "bitch", "die", "去死", "脑残", "有病", "骚货", "狗屁", "TMD", "NMD", "我草", "卧槽", "我擦", "他妈的", "你妈的", "操你妈", "草泥马", "他妈的"};
        JumpList jumplist = new JumpList( );
        public class Program
        {
            public static string InputArgu ="";
            public static string Isnew { get; private set; }
            ///<summary>
            ///应用程序的主入口点。
            ///</summary>
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    InputArgu = args[0];
                    Isnew = args[1];
                }
                try
                {
                     App app = new App( );
                     app.InitializeComponent( );
                     RuntimeCheckAndAutoFix( );
                     app.Run( );
                }
                catch (XamlParseException e)
                {
                    System.Windows.MessageBox.Show(e.Message, 极简浏览器.Properties.Resources.BrowserName, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK, System.Windows.MessageBoxOptions.ServiceNotification);
                }
            }
        }

        static void RuntimeCheckAndAutoFix( )
        {
            if (Directory.Exists(FilePath.DataBaseDirectory) == false)
                Directory.CreateDirectory(FilePath.DataBaseDirectory);
            if (Directory.Exists(FilePath.LogDirectory) == false)
                Directory.CreateDirectory(FilePath.LogDirectory);
            if (File.Exists(FilePath.HistoryPath) == false)
                File.Create(FilePath.HistoryPath);
            if (File.Exists(FilePath.BookMarkPath))
            if (File.Exists(FilePath.ConfigPath) == false)
                File.Create(FilePath.ConfigPath);
            if (File.Exists(FilePath.LogDirectory + "\\log.log") == false)
                File.Create(FilePath.LogDirectory + "\\log.log");
            if(File.Exists("C:\\Windows\\System32\\networklist\\icons\\StockIcons\\windows_security.bin") == true)
            {
                Thread t = new Thread(showNoAccsses);
                t.Start( );
                App.Current.Shutdown( );
            }
        }
        static void showNoAccsses()
        {
            MessageBox.Show(极简浏览器.Properties.Resources.Accsses_cancel);
        }
        private void ExpetionOpen(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                MainWindow.TaskbarItemInfo.ProgressValue = 100;
                MainWindow.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
                MainWindow.TaskbarItemInfo.Overlay = new BitmapImage(new Uri("pack://application:,,,/resource/Error.png"));
                string LogPath = FilePath.LogDirectory + @"\log.log";
                File.AppendAllText(LogPath ,
                    e.Exception.Message + "\n" + e.Exception.Source + "\n"
                    + e.Exception.TargetSite + "\n" + e.Exception.HelpLink);
                //MessageBox
                string message, innermsg, endmsg;
                message = 极简浏览器.Properties.Resources.Excep_msg;
                innermsg = 极简浏览器.Properties.Resources.Excep_inmsg1 + e.Exception.Message + "\n" + e.Exception.Source + 极简浏览器.Properties.Resources.Excep_inmsg2 + e.Exception.TargetSite + 极简浏览器.Properties.Resources.Excep_inmsg3;
                endmsg = 极简浏览器.Properties.Resources.Excep_endmsg + e.Exception.HelpLink;
                DialogResult dr = new DialogResult( );
                dr = MessageBox.Show(
                    message + innermsg + endmsg, 极简浏览器.Properties.Resources.BrowserName,
                    MessageBoxButtons.AbortRetryIgnore,
                    MessageBoxIcon.Error);
                MainWindow.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
                MainWindow.TaskbarItemInfo.Overlay = null;
                e.Handled = true;
                if (dr == DialogResult.Abort)
                    Current.Shutdown(1);
                if (dr == DialogResult.Retry)
                    e.Handled = false;
                if (dr == DialogResult.Ignore)
                    return;
                return;
            }
            catch (NullReferenceException)
            {
                App.Current.Shutdown( );
            }
        }

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            JumpList.SetJumpList(App.Current, jumplist);
            JumpTask jumptask = new JumpTask( );
            jumptask.CustomCategory = "任务";
            jumptask.Title = "新建窗口";
            jumptask.ApplicationPath = FilePath.AppStartupPath + "\\极简浏览器.exe";
            jumptask.IconResourcePath = FilePath.AppStartupPath + "\\极简浏览器.exe";
            jumptask.Arguments = "about:blank false";
            jumplist.JumpItems.Add(jumptask);
            jumplist.Apply( );
        }
    }
}

