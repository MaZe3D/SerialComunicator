using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScottPlot;

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

            draw();

            Steuerung.mainWindow = this;
            Steuerung.main();
        }

        private void combBox_Port_DropDownOpened(object sender, EventArgs e)
        {
            Steuerung.refreshAvailiblePortList();
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            Steuerung.settings_applied();
        }

        private void bt_Send_Click(object sender, RoutedEventArgs e)
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

        private void tb_SerialRead_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        Timer aTimer;
        double[] values;
        int idx = 0;
        private void draw()
        {
            //ALternativen: Oxyplot, LiveCharts

            //https://github.com/ScottPlot/ScottPlot/issues/37
            WpfPlot1.Plot.Title("Serielle Datan An COMX");

            values = new double[1000];
            WpfPlot1.Plot.AddSignal(values, sampleRate: 10);
            WpfPlot1.Plot.SetAxisLimitsX(-1,100);
            WpfPlot1.Plot.SetAxisLimitsY(-1,11);
            aTimer = new System.Timers.Timer(100);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;


        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            values[idx++ % 1000] = (double)(idx % 100) / 10;
        }

    }
}
