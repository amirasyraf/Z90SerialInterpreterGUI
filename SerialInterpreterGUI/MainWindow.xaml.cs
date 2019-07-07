using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerialInterpreterGUI
{
    public partial class MainWindow : Window
    {
        public static SerialPort Port = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);
        public MainWindow()
        {
            InitializeComponent();
            RefreshComPortList();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Terminal_Text.Inlines.Add(new Run("Started!\n") { Foreground = Brushes.Green });
            RunStatus.status = true;
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            Port.Open();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            RunStatus.status = false;
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshComPortList();
        }
    }
    public static class RunStatus
    {
        public static Boolean status { get; set; }
    }
}
