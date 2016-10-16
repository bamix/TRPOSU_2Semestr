using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace lab9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawCircle()
        {
            Ellipse el = new Ellipse()
            {
                Width = 100,
                Height = 100,
                VerticalAlignment = VerticalAlignment.Center,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black
            };
            Grid.Children.Add(el);
        }

        private void DrawLines()
        {
            var height =((Grid) this.Content).RenderSize.Height;
            var width =((Grid) this.Content).RenderSize.Width;
            var line = CreateLine(width, height);
            var line2 = CreateLine(width, height);
            
            line.RenderTransform = new RotateTransform(45, width / 2, height /2 );
            line2.RenderTransform = new RotateTransform(-45, width / 2, height /2 );

            Grid.Children.Add(line);
            Grid.Children.Add(line2);
        }

        private static Line CreateLine(double width, double height)
        {
            return new Line()
            {
                Stroke = Brushes.Black,
                X1 = width / 2 - 50,
                Y1 = height / 2,
                X2 = width / 2 + 50,
                Y2 = height / 2
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawCircle();
            DrawLines();
        }
    }
}
