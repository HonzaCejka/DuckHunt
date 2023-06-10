using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuckHunt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Target target;
        public MainWindow()
        {
            InitializeComponent();
            target = new Target(MyCanvas,MainGrid, ActualWidth, ActualHeight,Scor,Shot);
            Loaded += MainWindow_Loaded;
            
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            target.UpdateSize(ActualWidth, ActualHeight);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            target.UpdateSize(ActualWidth, ActualHeight); 
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            target.vystrel(e);
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            target.prebit();
        }
    }
}
