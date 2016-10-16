using System;
using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace lab11
{
    public partial class MainWindow : Window
    {
        public double XStart { get; set; } = 0.2;

        public double XFinish { get; set; } = 0.8;

        public double Step { get; set; } = 0.2;

        public int N { get; set; } = 10;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid())
            {
                textBlock.Text += $"Input values is in incorrect range\n";
                return;
            }
            textBlock.Text += $"X1 = {XStart:F3}; X2 = {XFinish:F3}; h = {Step:F3}; n = {N}\n";
            for (double x = XStart; x <= XFinish; x += Step)
            {
                double sum = 0;
                for (int k = 0; k <= N; k++)
                {
                    sum += Math.Pow(-1, k) * Math.Pow(x, 2 * k + 1) / Factorial(2 * k + 1);
                }
                double Y = Math.Sin(x);
                textBlock.Text += $"x={x}: Summa={sum:F3}, Y={Y:F3}\n";
            }
        }

        private bool IsValid()
        {
            if (XStart < 0.1 || XStart > 0.3)
            {
                return false;
            }

            if (XFinish < 0.7 || XFinish > 0.9)
            {
                return false;
            }

            if (Step < 0.1 || Step > 0.2)
            {
                return false;
            }

            if (N < 5 || N > 20)
            {
                return false;
            }
            return true;
        }

        private int Factorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
        }
    }
}
