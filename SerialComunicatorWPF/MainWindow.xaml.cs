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
using LiveCharts;
using LiveCharts.Wpf;



namespace SerialComunicatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public SeriesCollection seriesCollection = new SeriesCollection
        //{
        //    new LineSeries
        //    {
        //        Values = new ChartValues<double> { 3, 5, 7, 4 }
        //    },
        //    new ColumnSeries
        //    {
        //        Values = new ChartValues<decimal> { 5, 6, 2, 7 }
        //    }
        //};

        Timer timer;
        double ctr = 0;
        public MainWindow()
        {
            InitializeComponent();

            var series = new LineSeries() { Values = new ChartValues<double> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } };

            myChart.Series = new SeriesCollection() { series };
            //myChart.AnimationsSpeed = new TimeSpan(0);

            timer = new Timer(10);
            timer.Elapsed += time;
            timer.AutoReset = true;
            timer.Enabled = true;


            Steuerung.mainWindow = this;
            Steuerung.main();
        }


        public void time(Object source, ElapsedEventArgs e)
        {
            myChart.Series.ElementAt(0).Values.Add(Math.Sin(ctr++ / 2 / Math.PI)) ;
            
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

       

        private void draw()
        {
        }
    }
}
