using CasualRacer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CasualRacer.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
            this.Loaded += GamePage_Loaded;
            this.Unloaded += GamePage_Unloaded;
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            theGameControl.Start();
        }

        private void GamePage_Unloaded(object sender, RoutedEventArgs e)
        {
            theGameControl.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
