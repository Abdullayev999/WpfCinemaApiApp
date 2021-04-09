using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfCinemaApp.ViewModels;

namespace WpfCinemaApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainViewModel MainViewModel { get; set; }
        public MainView()
        {

            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainViewModel.Close();
        }
    }
}
