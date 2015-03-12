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
using MahApps.Metro.Controls;
using Poo_Final.ViewModel;

namespace Poo_Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(this);
            this.DataContext = viewModel;
        }

        private void EntrarClick(object sender, RoutedEventArgs e)
        {
            viewModel.ExecuteLogin();
        }

        private void EditarTag(object sender, RoutedEventArgs e)
        {
            viewModel.EditTag();
        }

        private void RealizeBackup(object sender, RoutedEventArgs e)
        {
            viewModel.ExecuteBackup();
        }
    }
}
