using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;


namespace SerialInterpreterGUI
{
    public partial class MainWindow : Window
    {
        static string strHostCommand;
        private void Start_Listen()
        {
            Port = new SerialPort(ComPortList.Text, 19200, Parity.None, 8, StopBits.One);
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run("Listening...\n") { Foreground = Brushes.Green });
            });
            RunStatus.status = true;
            Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            Port.Open();
        }
        private void Stop_Listen()
        {
            RunStatus.status = false;
            Port.Close();
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run("Stopped!\n") { Foreground = Brushes.Red });
            });
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            strHostCommand = Port.ReadExisting();
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run(strHostCommand + "\n") { Foreground = Brushes.Black });
                Terminal_Text_Scroll.ScrollToBottom();
            });
            Port.WriteLine("Command Received: " + strHostCommand);
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
