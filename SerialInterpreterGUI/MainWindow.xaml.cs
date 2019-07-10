using System;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace SerialInterpreterGUI
{
    public partial class MainWindow : Window
    {
        [DllImport("Z90.dll")]
        public static extern int Z90SendRecv(string cmd, ref string response);
        static string strHostCommand = String.Empty;
        static string strResponse = String.Empty;
        public static SerialPort Port;
        public MainWindow()
        {
            InitializeComponent();
            ComPortList.ItemsSource = OrderedPortNames();
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
        private void Start_Listen()
        {
            Port = new SerialPort(ComPortList.Text, 19200, Parity.None, 8, StopBits.One);
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run("Listening at port: " + ComPortList.Text + "\n") { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                Start_Button.IsEnabled = false;
                Stop_Button.IsEnabled = true;
            });
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            Port.Open();
        }
        private void Stop_Listen()
        {
            Port.Close();
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run("Stopped!\n") { Foreground = Brushes.Red, FontWeight = FontWeights.Bold });
                Start_Button.IsEnabled = true;
                Stop_Button.IsEnabled = false;
            });
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            strHostCommand = Port.ReadExisting();

            Z90SendRecv(strHostCommand, ref strResponse);
            Port.WriteLine(strResponse);

            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run("Command Received: " + strHostCommand + "\n") { Foreground = Brushes.DarkOrange, FontWeight = FontWeights.Normal });
                Terminal_Text.Inlines.Add(new Run("Z90 Response: " + strResponse + "\n") { Foreground = Brushes.Green, FontWeight = FontWeights.Normal });
                Terminal_Text_Scroll.ScrollToBottom();
            });
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }
    }

}
