using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
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
using WebView2Demo.Models;
using WebView2Demo.ViewModels;

namespace WebView2Demo.Views
{
    /// <summary>
    /// Interaction logic for SketchWindow.xaml
    /// </summary>
    public partial class SketchWindow : Window
    {
        private readonly SketchViewModel ViewModel;
        private JsonSerializerSettings _useCamelCase => new JsonSerializerSettings {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
        };


        public SketchWindow(SketchViewModel viewModel)
        {
            InitializeComponent();

            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void SetSample(string data, string config)
        {
            _ = Dispatcher.BeginInvoke(new Action(() =>
            {
                System.Diagnostics.Trace.WriteLine($"Posting message data: {data}");
                System.Diagnostics.Trace.WriteLine($"Posting message config: {config}");
                WebView.CoreWebView2.PostWebMessageAsString(JsonConvert.SerializeObject(new
                {
                    type = "load",
                    data = new
                    {
                        data = JsonConvert.DeserializeObject(data),
                        config = JsonConvert.DeserializeObject(config)
                    }
                }));
            }));
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SketchViewModel.Input))
            {
                SetSample(ViewModel.Input, ViewModel.Config);
            }
            else if (e.PropertyName == nameof(SketchViewModel.IsOpen))
            {
                if (!ViewModel.IsOpen)
                {
                    Close();
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await WebView.EnsureCoreWebView2Async();
            WebView.CoreWebView2.WindowCloseRequested += CoreWebView2_WindowCloseRequested;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.IsOpen = false;
        }

        private void CoreWebView2_WindowCloseRequested(object sender, object e)
        {
            ViewModel.IsOpen = false;
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //set initial sample
            if (!string.IsNullOrWhiteSpace(ViewModel.Input))
            {
                SetSample(ViewModel.Input, ViewModel.Config);
            }

            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var s = e.TryGetWebMessageAsString();
            var message = JsonConvert.DeserializeObject<Message>(s, _useCamelCase);
            ViewModel.ProcessMessage(message);
            if (message.Type == "save")
            {
                WebView.CoreWebView2.PostWebMessageAsString(JsonConvert.SerializeObject(new
                {
                    type = "save"
                }));
            }
        }
    }
}
    