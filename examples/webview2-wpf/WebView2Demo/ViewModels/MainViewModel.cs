using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebView2Demo.Helpers;
using WebView2Demo.Models;
using WebView2Demo.Views;

namespace WebView2Demo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            ResetSample();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _GetResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var rs = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(name, StringComparison.OrdinalIgnoreCase));
            using var stream = assembly.GetManifestResourceStream(rs);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private string _Status;
        public string Status
        {
            get => _Status;
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Input;
        public string Input
        {
            get => _Input;
            set
            {
                if (value != _Input)
                {
                    _Input = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Logs;
        public string Logs
        {
            get => _Logs;
            set
            {
                if (value != _Logs)
                {
                    _Logs += value + "\n";
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Config;
        public string Config
        {
            get => _Config;
            set
            {
                if (value != _Config)
                {
                    _Config = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Output;
        public string Output
        {
            get => _Output;
            set
            {
                if (value != _Output)
                {
                    _Output = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _SVGExport = "";
        public string SVGExport
        {
            get { return _SVGExport; }
            set
            {
                if (value != _SVGExport)
                {
                    _SVGExport = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SketchViewModel SketchWindow { get; private set; }

        public void Launch(object _)
        {
            try
            {
                SketchWindow = new SketchViewModel
                {
                    Input = Input,
                    Config = Config,
                    Url = @"https://app.sketcher.camacloud.com/1.0/3/?channel=webview2"
                };
                SketchWindow.PropertyChanged -= SketchWindow_PropertyChanged;
                SketchWindow.PropertyChanged += SketchWindow_PropertyChanged;
                NotifyPropertyChanged(nameof(SketchWindow));

            }
            catch(Exception ex)
            {
                Logs = $"Error: {ex.Message}";       
            }
        }

        public void ExportToSVG(object _)
        {
            if (!String.IsNullOrEmpty(Output))
            {
                try
                {
                    Output = SVGExport;

                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(Output);

                    for(int i = 0; i < myDeserializedClass.sketches.Count; i++) 
                    {
                        var data = myDeserializedClass.sketches[i].data;
                        var name = myDeserializedClass.sketches[i].name;
                        byte[]  deserializedData = Convert.FromBase64String(data);
              
                        File.WriteAllBytes($"{name}.svg", deserializedData);
                    }

                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    string messageBoxText = $"The sketch has been exported to SVG. The file(s) can be found under {path} ";
                    string caption = "Sketch exported to SVG";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBox.Show(Application.Current.MainWindow,messageBoxText, caption, button);
                }
                catch(Exception ex)
                {
                    Logs = $"Error: {ex.Message}";
                }
            }

        }

        private void SketchWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SketchViewModel.IsOpen))
            {
                if (!SketchWindow.IsOpen)
                {
                    SketchWindow.PropertyChanged -= SketchWindow_PropertyChanged;
                    Status = Enums.Status.Closed.ToString();
                }

                SketchWindow = null;
                NotifyPropertyChanged(nameof(SketchWindow));
            }
            else if (e.PropertyName == nameof(SketchViewModel.Output))
            {
                Output = SketchWindow.Output;
            }
            else if (e.PropertyName == nameof(SketchViewModel.SVGExport))
            {
                SVGExport = SketchWindow.SVGExport;
            }
            else if (e.PropertyName == nameof(SketchViewModel.Status))
            {
                Status = SketchWindow.Status;
            }
        }

        private bool _CanExecute(object _) => SketchWindow == null;

        public void ResetSample(string sampleName = "sample1")
        {
            Input = _GetResource($"{sampleName}.json");
            Config = _GetResource($"{sampleName}.config.json");
            Status = Enums.Status.Closed.ToString();
        }

        private RelayCommand _ResetSampleCommand;
        public ICommand ResetSampleCommand
        {
            get => _ResetSampleCommand = new RelayCommand(_CanExecute, _ => ResetSample());
        }

        private RelayCommand _LaunchCommand;
        public ICommand LaunchCommand
        {
            get => _LaunchCommand = new RelayCommand(_CanExecute, Launch);
        }

        private RelayCommand _ExportToSVGCommand;
        public ICommand ExportToSVGCommand
        {
            get => _ExportToSVGCommand = new RelayCommand(_CanExecute, ExportToSVG);
        }
    }

}
