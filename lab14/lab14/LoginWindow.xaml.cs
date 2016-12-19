using System.Windows;

namespace lab14
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Error.Visibility = Visibility.Collapsed;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Ok_OnClick(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Equals("admin") && Password.Password.Equals("admin"))
            {
                MainWindow.isLoged = true;
                MainWindow.isAdmin = true;
                DialogResult = true;
                Close();
            }
            if (Login.Text.Equals("user") && Password.Password.Equals("user"))
            {
                MainWindow.isLoged = true;
                MainWindow.isAdmin = false;
                DialogResult = true;
                Close();
            }
            Error.Visibility = Visibility.Visible;
        }
    }
}
