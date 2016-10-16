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

namespace lab12
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-1]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                return;
            }
            comboBox.Items.Add(textBox.Text);
            comboBox.SelectedItem = comboBox.Items[comboBox.Items.Count - 1];
            textBox.Text = "";
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var item = comboBox.SelectedItem;
            if (item == null)
            {
                return;
            }

            var array = ((String)item).Split(' ');
            var maxLength = array.OrderByDescending(s => s.Length).First().Length;
            textBlock.Text += $"Max length of group in '{item}' is {maxLength}\n";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
        }
    }
}
