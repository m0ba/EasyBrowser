﻿using System.Windows;
using CefSharp;
using 极简浏览器Cef.Api;

namespace 极简浏览器Cef
{
    partial class MainWindow
    {
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About( );
            about.Show( );
        }
        private void AddNewPageButton_Click(object sender, RoutedEventArgs e)
        {
            NewInstance.StartNewInstance("about:blank");
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.GoBack( );
        }
        private void GoForwardButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.GoForward( );
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help( );
            help.Show( );
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.Refresh( );
        }
        private void SetBookMark_Click(object sender, RoutedEventArgs e)
        {
            FileApi.Write(BrowserCore.GetInstance( ).cwb.Address, FileType.BookMark);
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting( );
            setting.Show( );
        }
        private void ViewBookMark_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            cwb.WebBrowser.ViewSource( );
        }
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
    }
}
