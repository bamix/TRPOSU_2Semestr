using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace lab10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate double function(double x);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = GetDoubleValue(inputX);
                var y = GetDoubleValue(inputY);
                var branch = GetBranch(x, y);
                branch.Run(x, y, GetFunction());
                TextBlock.Text += branch + "\n";
            }
            catch (ValueUnavailableException exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unexpected error ocured", "Error", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private IBranch GetBranch(double x, double y)
        {
            if (x - y == 0)
            {
                return new Branch1();
            }

            if (x - y > 0)
            {
                return new Branch2();
            }

            if (x - y < 0)
            {
                return new Branch3();
            }

            throw new Exception();
        }

        private function GetFunction()
        {
            var checkedButton = Grid.Children.OfType<RadioButton>().First(r => r.IsChecked == true);
            switch (checkedButton.Name)
            {
                case "Function1":
                    return d => Math.Cos(d);
                case "Function2":
                    return d => Math.Sin(d);
                case "Function3":
                    return d => Math.Tan(d);
                default:
                    return d => d;
            }
        }

        private double GetDoubleValue(TextBox textBox)
        {
            try
            {
                return Convert.ToDouble(textBox.Text.Replace('.', ','));
            }
            catch (Exception e)
            {
                throw new ValueUnavailableException($"Wrong data format in {textBox.Name}");
            }
        }

        private void checkBoxMin_Click(object sender, RoutedEventArgs e)
        {
            StorageService.RememberMin = ((CheckBox) sender).IsChecked.Value;
        }

        private void checkBoxMax_Click(object sender, RoutedEventArgs e)
        {
            StorageService.RememberMax = ((CheckBox)sender).IsChecked.Value;
        }
    }
}
