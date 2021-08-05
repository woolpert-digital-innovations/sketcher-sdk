using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        public SketchViewModel SketchWindow { get; private set; }

        public void Launch(object _)
        {
            SketchWindow = new SketchViewModel
            {
                Input = Input,
                Config = Config,
                Url = @"https://app.sketcher.camacloud.com/0.9/?channel=webview2"
            };

            SketchWindow.PropertyChanged -= SketchWindow_PropertyChanged;
            SketchWindow.PropertyChanged += SketchWindow_PropertyChanged;
            NotifyPropertyChanged(nameof(SketchWindow));
        }

        private void SketchWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SketchViewModel.IsOpen))
            {
                if (!SketchWindow.IsOpen)
                {
                    SketchWindow.PropertyChanged -= SketchWindow_PropertyChanged;
                }

                SketchWindow = null;
                NotifyPropertyChanged(nameof(SketchWindow));
            }
            else if (e.PropertyName == nameof(SketchViewModel.Output))
            {
                Output = SketchWindow.Output;
            }
        }

        private bool _CanExecute(object _) => SketchWindow == null;

        public void ResetSample(string sampleName = "sample1")
        {
            Input = _GetResource($"{sampleName}.json");
            Config = _GetResource($"{sampleName}.config.json");
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
    }

}
