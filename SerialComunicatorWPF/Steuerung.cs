using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SerialComunicatorWPF
{
    static class Steuerung
    {
        public static MainWindow mainWindow;
        public static AdvancedSettings advancedSettings;

        static List<string> avaiiblePorts = new List<string>();

        static SerialPort sp = new SerialPort();

        public delegate void delUpdateUItb_SerialRead(string text);
        
        static public void main()
        {
            refreshAvailiblePortList();
            mainWindow.combBox_Baudrate.SelectedIndex = 0;
            switch (avaiiblePorts.Count)
            {
                case 0:
                    
                    MessageBox.Show("There is no Serial Port availible! Please connect a propper device and try again!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 1:
                    mainWindow.combBox_Port.SelectedIndex = 0;
                    break;
                default:
                    mainWindow.combBox_Port.IsDropDownOpen = true;
                    break;
            }
        }

        static void getAvaliblePorts()
        {
            avaiiblePorts = new List<string>(SerialPort.GetPortNames());
        }

        public static void refreshAvailiblePortList()
        {
            mainWindow.combBox_Port.Items.Clear();
            getAvaliblePorts();
            avaiiblePorts.ForEach(port =>
            {
                mainWindow.combBox_Port.Items.Add(port);
            });
        }

        public static void settings_applied()
        {
            if (!sp.IsOpen)
            {
                sp.PortName = mainWindow.combBox_Port.Text;
                sp.BaudRate = Convert.ToInt32(mainWindow.combBox_Baudrate.Text);
                if (mainWindow.combBox_Port.Text == null || Convert.ToInt32(mainWindow.combBox_Baudrate.Text) == 0)
                {
                    MessageBox.Show("Please select Port and Baudrate!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                sp.PortName = mainWindow.combBox_Port.SelectedItem.ToString();
                mainWindow.btn_Apply.Content = "Disconnect";
                
                sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                sp.Open();
                enableSettings(false);
                mainWindow.tb_SerialWrite.Focus();
            }
            else
            {
                sp.Close();
                mainWindow.btn_Apply.Content = "Connect";
                enableSettings(true);
                mainWindow.btn_Apply.Focus();
            }
        }

        private static void enableSettings(bool state)
        {
            mainWindow.combBox_Port.IsEnabled = state;
            mainWindow.combBox_Baudrate.IsEnabled = state;
            mainWindow.bt_Send.IsEnabled = !state;
            mainWindow.tb_SerialWrite.IsEnabled = !state;

        }

        private static void serialReadAddText(string text)
        {
            mainWindow.tb_SerialRead.Text += text;
            mainWindow.tb_SerialRead.ScrollToEnd();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _sp = (SerialPort)sender;
            mainWindow.tb_SerialRead.Dispatcher.Invoke(() =>
            {
                serialReadAddText(_sp.ReadExisting());
            });
        }
        public static void sendData()
        {
            sp.WriteLine(mainWindow.tb_SerialWrite.Text);
            mainWindow.tb_SerialWrite.Text = string.Empty;
            mainWindow.tb_SerialWrite.Focus();
        }

        public static void applyAdvancedSettings()
        {
            sp.StopBits = (StopBits)advancedSettings.combBox_StopBits.SelectedIndex;
            sp.DataBits = Convert.ToInt32(advancedSettings.tb_DataBits.Text);
            sp.Parity = (Parity)advancedSettings.combBox_Parity.SelectedIndex;
            sp.Handshake = (Handshake)advancedSettings.combBox_Handshake.SelectedIndex;
        }

        public static void openAdvencedSettings()
        {
            advancedSettings.combBox_Port.Text = mainWindow.combBox_Port.Text;
            advancedSettings.combBox_BaudRate.SelectedIndex = mainWindow.combBox_Port.SelectedIndex;
            advancedSettings.combBox_StopBits.SelectedIndex = (int)sp.StopBits;
            advancedSettings.tb_DataBits.Text = sp.DataBits.ToString();
            advancedSettings.combBox_Parity.SelectedIndex = (int)sp.Parity;
            advancedSettings.combBox_Handshake.SelectedIndex = (int)sp.Handshake;
        }
    }
}
