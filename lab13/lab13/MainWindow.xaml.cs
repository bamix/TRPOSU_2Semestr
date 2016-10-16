using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace lab13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 1;
        public int N { get; set; } = 1;
        public double H => (Max - Min) / N;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ShowModal_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialog(this);
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            slider.IsEnabled = false;
            button.IsEnabled = false;
            button1.IsEnabled = false;
            if (Max < Min)
            {
                var t = Max;
                Max = Min;
                Min = t;
            }
            double summa = 0;
            progress.Value = 0;
            progress.Maximum = N;
            for (double i = Min; i <= Max; i += H)
            {
                await Task.Delay(100);
                summa +=  Math.Pow(i,4) * H;
                progress.Value++;
            }

            slider.IsEnabled = true;
            button.IsEnabled = true;
            button1.IsEnabled = true;
            textBlock.Text += $"Integral from {Min} to {Max} with step {H:F5} is {summa:F5}\n";
        }
    }
}
