using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab14
{
    public partial class SelectResourceWindow : Window
    {
        private int GoodId;
        public SelectResourceWindow(int goodId)
        {
            GoodId = goodId;
            InitializeComponent();
            using (var context = new MyDbContext())
            {
                var good = context.Goods
                    .Include(nameof(Good.Resources))
                    .First(g => g.Id == GoodId);
                listBox.SelectionMode = SelectionMode.Multiple;
                var resources = context.Resources.ToList();
                listBox.ItemsSource = resources;
                foreach (var res in good.Resources)
                {
                    listBox.SelectedItems.Add(resources.First(r => r.Id == res.Id));
                }
            }
            listBox.Focus();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            var ids = listBox.SelectedItems.Cast<Resource>().Select(r => r.Id).ToList();
            using (var context = new MyDbContext())
            {
                var good = context.Goods
                    .Include(nameof(Good.Resources))
                    .First(g => g.Id == GoodId);
                var resources = context.Resources.Where(r => ids.Contains(r.Id)).ToList();
                good.Resources = resources;
                context.SaveChanges();
            }
            DialogResult = true;
            Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
