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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Window
    {
        public Pocetna()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow2().Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow3().Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new MainWindow4().Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MainWindow5().Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new MainWindow6().Show();
        }
    }
}
