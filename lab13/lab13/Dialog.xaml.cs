using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace lab13
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public Dialog(Window parent)
        {
            InitializeComponent();
            DataContext = parent;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
