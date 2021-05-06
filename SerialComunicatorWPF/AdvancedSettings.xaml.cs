using System;
using System.Collections.Generic;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void chkBox_SavePort_Checked(object sender, RoutedEventArgs e)
        {
            combBox_Port.IsEnabled = true;
        }

        private void chkBox_SavePort_Unchecked(object sender, RoutedEventArgs e)
        {
            
            combBox_Port.IsEnabled = true;
        }
    }
}
