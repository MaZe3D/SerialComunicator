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

namespace SerialComunicatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Steuerung.mainWindow = this;
            Steuerung.main();
        }

        private void combBox_Port_DropDownOpened(object sender, EventArgs e)
        {
            Steuerung.refreshAvailiblePortList();
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            Steuerung.connect();
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            Steuerung.sendData();
        }

        private void tb_SerialWrite_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Steuerung.sendData();
                    break;
                default:
                    break;
            }
        }

        private void btn_AdvSettings_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSettings advancedSettings = new AdvancedSettings();
            advancedSettings.ShowDialog();
            Steuerung.openAdvencedSettings();
        }
    }
}
