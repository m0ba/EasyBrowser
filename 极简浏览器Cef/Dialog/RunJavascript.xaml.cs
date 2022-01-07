﻿using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// RunJavascript.xaml 的交互逻辑
    /// </summary>
    public partial class RunJavaScript : Window
    {
        public RunJavaScript()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.RunJavaSript(JsCode.Text);
        }
    }
}
