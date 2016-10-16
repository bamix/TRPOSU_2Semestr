using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var str = InputValue.Text;
            if (!IsValid(str))
            {
                MessageBox.Show("Incorrect value", "Error", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            var value = Convert.ToDouble(str.Replace('.',','));
            var result1 = Function1(value);
            var result2 = Function2(value);
            MessageBox.Show($"F1(m) = {result1}; F2(m) =  {result2}", "Result", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private bool IsValid(string value)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            return regex.IsMatch(value);
        }

        private double Function1(double value)
        {
            var numerator = Math.Sqrt(Math.Pow(3 * value + 2, 2) - 24 * value);
            var denominator = 3 * Math.Sqrt(value) - 2 / Math.Sqrt(value);
            return numerator / denominator;
        }

        private double Function2(double value)
        {
            return Math.Sqrt(value);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
