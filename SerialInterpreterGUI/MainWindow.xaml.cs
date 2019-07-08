using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace SerialInterpreterGUI
{
    public partial class MainWindow : Window
    {
        public static SerialPort Port;
        public MainWindow()
        {
            InitializeComponent();
            ComPortList.ItemsSource = OrderedPortNames();
            RunStatus.status = false;
            Stop_Button.IsEnabled = false;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Start_Listen();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            Stop_Listen();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            ComPortList.ItemsSource = OrderedPortNames();
        }
    }
    public static class RunStatus
    {
        public static Boolean status { get; set; }
    }
}
