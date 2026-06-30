SVISION 智能检测机 (WPF 示例)
================================
运行环境: Windows 10/11 + .NET 8 SDK

运行方式(任选其一):
  1) 命令行: 文件夹内执行  dotnet run
  2) Visual Studio 2022: 双击 SVisionInspection.csproj 后按 F5
  3) 无需 .NET 看效果: 直接双击 preview.html

文件:
  SVisionInspection.csproj  项目文件
  App.xaml / App.xaml.cs    应用入口
  MainWindow.xaml           界面布局
  MainWindow.xaml.cs        交互逻辑
  preview.html              功能等效的可交互预览

功能: 6 路相机网格 / 生产-通过-错误统计 / 合格率圆 / 时间戳日志 /
      模拟一次 / 启动(连续检测) / 复位 / 最小化 / 规格下拉 / 存图开关
