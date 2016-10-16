using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab14
{
    public partial class AddSaleWindow : Window
    {
        public AddSaleWindow()
        {
            InitializeComponent();
            using (var db = new MyDbContext())
            {
                listBox.ItemsSource = db.Goods.ToList();
                listBox.SelectionMode = SelectionMode.Single;
            }
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && !string.IsNullOrEmpty(count.Text) &&
                listBox.SelectedIndex >= 0)
            {
                using (var db = new MyDbContext())
                {
                    var goodId = (listBox.SelectedItems[0] as Good)?.Id;
                    db.Saleses.Add(new Sales()
                    {
                        Count = Convert.ToInt32(count.Text),
                        CustomerName = textBox.Text,
                        Good = db.Goods.First(g => g.Id == goodId)
                    });
                    db.SaveChanges();
                    DialogResult = true;
                    Close();
                }
            }
            error.Visibility = Visibility.Visible;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
