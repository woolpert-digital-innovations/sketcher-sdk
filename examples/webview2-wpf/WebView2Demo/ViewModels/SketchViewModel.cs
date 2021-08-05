using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebView2Demo.Helpers;
using WebView2Demo.Models;

namespace WebView2Demo.ViewModels
{
    public class SketchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _Url = "about:blank";
        public string Url
        {
            get { return _Url; }
            set
            {
                if (value != _Url)
                {
                    _Url = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Input;
        public string Input
        {
            get { return _Input; }
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
            get { return _Config; }
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
            get { return _Output; }
            set
            {
                if (value != _Output)
                {
                    _Output = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _IsOpen = true;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                if (value != _IsOpen)
                {
                    _IsOpen = value;
                    NotifyPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand LaunchCommand
        {
            get => new RelayCommand(_ => true, Launch);
        }

        public void ProcessMessage(Message message)
        {
            if (message.Type == "closed")
            {
                IsOpen = false;
            }
            else if (message.Type == "save")
            {
                Output = message.Data.ToString();
            }
        }

        private void Launch(object o)
        {
            IsOpen = true;
        }
    }
}
