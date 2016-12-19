using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab14
{
    public partial class MainWindow : Window
    {
        public static bool isAdmin { get; set; }
        public static bool isLoged { get; set; }
        private List<Resource> Resources;
        private List<Good> Goods;
        private List<Sales> Saleses;
        private int SelectedTabIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            Logout();
            Update();
        }

        private void Update()
        {
            using (var context = new MyDbContext())
            {
                Resources = context.Resources
                    .Include(nameof(Resource.Goods))
                    .ToList();
                resourcesGrid.ItemsSource = Resources;

                Goods = context.Goods
                    .Include(nameof(Good.Resources))
                    .ToList();
                goodsGrid.ItemsSource = Goods;

                Saleses = context.Saleses
                    .Include(nameof(Sales.Good))
                    .ToList();
                salesesGrid.ItemsSource = Saleses;
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            LoginWindow dialog = new LoginWindow() {Owner = this};
            if (dialog.ShowDialog() == true)
            {
                if (dialog.DialogResult == true)
                {
                    Login();
                }
            }
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void Login()
        {
            resourcesGrid.Visibility = Visibility.Visible;
            goodsGrid.Visibility = Visibility.Visible;
            salesesGrid.Visibility = Visibility.Visible;
            if (!isAdmin)
            {
                return;
            }
            ActionsColumn.Visibility = Visibility.Visible;
            GoodsActionsColumn.Visibility = Visibility.Visible;
            SalesesActionsColumn.Visibility = Visibility.Visible;
            resourcesGrid.IsReadOnly = false;
            goodsGrid.IsReadOnly = false;
            MenuLogin.Visibility = Visibility.Collapsed;
            MenuLogout.Visibility = Visibility.Visible;
            MenuSaleAdd.Visibility = Visibility.Visible;
        }

        private void Logout()
        {
            isAdmin = false;
            isLoged = false;
            resourcesGrid.Visibility = Visibility.Hidden;
            goodsGrid.Visibility = Visibility.Hidden;
            salesesGrid.Visibility = Visibility.Hidden;
            ActionsColumn.Visibility = Visibility.Collapsed;
            GoodsActionsColumn.Visibility = Visibility.Collapsed;
            SalesesActionsColumn.Visibility = Visibility.Collapsed;
            resourcesGrid.IsReadOnly = true;
            goodsGrid.IsReadOnly = true;
            MenuLogin.Visibility = Visibility.Visible;
            MenuLogout.Visibility = Visibility.Collapsed;
            MenuSaleAdd.Visibility = Visibility.Collapsed;
        }

        private void resourcesGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var resource = context.Resources.Add(new Resource());
                context.SaveChanges();
                e.NewItem = resource;
            }
        }

        private void resourcesGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var text = (e.EditingElement as TextBox)?.Text;
            var id = (e.Row.Item as Resource)?.Id;
            using (var context = new MyDbContext())
            {
                var resource = context.Resources.Find(id);
                resource.Name = text;
                context.SaveChanges();
            }
        }

        private void RemoveResource_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((sender as Button).DataContext as Resource)?.Id;
            if (id == null)
            {
                return;
            }
            Resources.RemoveAt(Resources.FindIndex(r => r.Id == id));
            resourcesGrid.Items.Refresh();
            using (var context = new MyDbContext())
            {
                var res = context.Resources.Find(id);
                context.Resources.Remove(res);
                context.SaveChanges();
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tc = sender as TabControl;
            var index = tc.SelectedIndex;
            if (index != SelectedTabIndex)
            {
                SelectedTabIndex = index;
                Update();
            }
        }

        private void RemoveGood_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((sender as Button).DataContext as Good)?.Id;
            if (id == null)
            {
                return;
            }
            Goods.RemoveAt(Goods.FindIndex(g => g.Id == id));
            goodsGrid.Items.Refresh();
            using (var context = new MyDbContext())
            {
                var good = context.Goods.Find(id);
                var saleses = context.Saleses.Where(s => s.Good.Id == good.Id).ToList();
                context.Saleses.RemoveRange(saleses);
                context.Goods.Remove(good);
                context.SaveChanges();
            }
        }

        private void GoodsGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var good = context.Goods.Add(new Good());
                context.SaveChanges();
                e.NewItem = good;
            }
        }

        private void goodsGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var text = (e.EditingElement as TextBox)?.Text;
            var id = (e.Row.Item as Good)?.Id;
            using (var context = new MyDbContext())
            {
                var good = context.Goods.Find(id);
                good.Name = text;
                context.SaveChanges();
            }
        }

        private void SelectResource_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((sender as Button).DataContext as Good)?.Id;
            if (id == null)
            {
                return;
            }

            SelectResourceWindow dialog = new SelectResourceWindow(id.Value) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                if (dialog.DialogResult == true)
                {
                    Update();
                }
            }
        }

        private void SalesGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var sale = context.Saleses.Add(new Sales());
                context.SaveChanges();
                e.NewItem = sale;
            }
        }

        private void SalesGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var textbox = (e.EditingElement as TextBox);
            var id = (e.Row.Item as Sales)?.Id;
            using (var context = new MyDbContext())
            {
                var sale = context.Saleses.Find(id);
                var x = e.Column.Header.Equals("Customer name");
                if (x)
                {
                    sale.CustomerName = textbox.Text;
                }
                else
                {
                    try
                    {
                        sale.Count = Convert.ToInt32(textbox.Text);
                    }
                    catch
                    {
                        return;
                    }
                }
                context.SaveChanges();
            }
        }

        private void RemoveSales_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((sender as Button).DataContext as Sales)?.Id;
            if (id == null)
            {
                return;
            }
            Saleses.RemoveAt(Saleses.FindIndex(s => s.Id == id));
            salesesGrid.Items.Refresh();
            using (var context = new MyDbContext())
            {
                var sale = context.Saleses.Find(id);
                context.Saleses.Remove(sale);
                context.SaveChanges();
            }
        }

        private void MenuSaleAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddSaleWindow();
            if (dialog.ShowDialog() == true)
            {
                Update();
            }
        }
    }
}
