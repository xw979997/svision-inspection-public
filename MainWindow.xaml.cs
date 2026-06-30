using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SVisionInspection
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer = new();
        private readonly Random _rng = new();
        private int _production, _pass, _error;

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += (_, _) => InspectOnce();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            Log($"获取本地IP地址: {GetLocalIp()}");
            Log("服务器开启成功");
            UpdateStats();
        }

        private void InspectOnce()
        {
            _production++;
            bool ok = _rng.NextDouble() > 0.15;
            if (ok) _pass++; else _error++;
            Log(ok ? $"第 {_production} 件: 合格 (OK)" : $"第 {_production} 件: 不合格 (NG)");
            UpdateStats();
        }

        private void UpdateStats()
        {
            prodCount.Text = _production.ToString();
            passCount.Text = _pass.ToString();
            errCount.Text  = _error.ToString();
            double rate = _production == 0 ? 0 : (double)_pass / _production * 100.0;
            rateText.Text = rate.ToString("0.000", CultureInfo.InvariantCulture);
        }

        private void Log(string msg)
        {
            logBox.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {msg}{Environment.NewLine}");
            logBox.ScrollToEnd();
        }

        private static string GetLocalIp()
        {
            try
            {
                foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();
            }
            catch { }
            return "127.0.0.1";
        }

        private void BtnSimulate_Click(object sender, RoutedEventArgs e) => InspectOnce();
        private void BtnLoad_Click(object sender, RoutedEventArgs e) => Log($"加载规格: {(cmbSpec.SelectedItem as ComboBoxItem)?.Content}");
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled) { _timer.Stop(); Log("已停止连续检测"); }
            else { _timer.Start(); Log("已启动连续检测"); }
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            _production = _pass = _error = 0;
            UpdateStats();
            Log("复位完成");
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void Menu_Click(object sender, RoutedEventArgs e) => Log($"打开: {((Button)sender).Content}");
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
