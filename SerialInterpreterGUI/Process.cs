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
        static string strHostCommand;
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!RunStatus.status)
            {
                Port.Close();
                return;
            }
            strHostCommand = Port.ReadExisting();
            this.Dispatcher.Invoke(() =>
            {
                Terminal_Text.Inlines.Add(new Run(strHostCommand + "\n") { Foreground = Brushes.Black });
                Terminal_Text_Scroll.ScrollToBottom();
            });
        }
    }
}
