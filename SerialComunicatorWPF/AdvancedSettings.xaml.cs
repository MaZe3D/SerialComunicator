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
using System.Windows.Shapes;

namespace SerialComunicatorWPF
{
    /// <summary>
    /// Interaktionslogik für AdvancedSettings.xaml
    /// </summary>
    public partial class AdvancedSettings : Window
    {
        public AdvancedSettings()
        {
            InitializeComponent();
            Steuerung.advancedSettings = this;
            List<string> parity = new()
            {
                "None",
                "Odd",
                "Even",
                "Mark",
                "Even"
            };

            parity.ForEach(a => combBox_Parity.Items.Add(a));

            List<string> handshake = new()
            {
                "None",
                "XOnXOff",
                "RequestToSend",
                "RequestToSendXOnXOff"
            };
            handshake.ForEach(a => combBox_Handshake.Items.Add(a));

            List<string> stopBits = new()
            {
                "None",
                "One",
                "Two",
                "OnePointFive"
            };
            handshake.ForEach(a => combBox_StopBits.Items.Add(a));
            Steuerung.baudRateList.ForEach(rate => combBox_BaudRate.Items.Add(rate));
            Steuerung.openAdvencedSettings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Steuerung.applyAdvancedSettings();
            this.Close();
        }

        private void chkBox_SavePort_Checked(object sender, RoutedEventArgs e)
        {
            tb_Port.IsEnabled = true;
        }

        private void chkBox_SavePort_Unchecked(object sender, RoutedEventArgs e)
        {
            
            tb_Port.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
