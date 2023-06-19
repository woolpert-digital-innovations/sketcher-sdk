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
using WebView2Demo.Helpers;
using WebView2Demo.ViewModels;

namespace WebView2Demo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new ViewModels.MainViewModel();
            var svm = new ViewModels.SketchViewModel();
            this.DataContext = vm;

            vm.PropertyChanged += ViewModel_PropertyChanged;

            lblStatus = (TextBlock)this.FindName("lblStatus");
            SetStatus(vm.Status);

        }

        private void SetStatus(string status)
        {
            if (status == Enums.Status.Open.ToString())
            {
                lblStatus.Foreground = Brushes.Green;
            }
            else if (status == Enums.Status.Closed.ToString())
            {
                lblStatus.Foreground = Brushes.Red;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = this.DataContext as ViewModels.MainViewModel;
            if (e.PropertyName == nameof(ViewModels.MainViewModel.SketchWindow))
            {
                if (vm.SketchWindow != null)
                {
                    var w = new SketchWindow(vm.SketchWindow);
                    w.Show();
                }
            }
            if(e.PropertyName == nameof(ViewModels.MainViewModel.Status))
            {
                SetStatus(vm.Status);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
