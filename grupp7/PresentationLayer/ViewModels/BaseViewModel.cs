using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationLayer.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _pageColor = Colors.Page;
        public string PageColor
        {
            get { return _pageColor; }
            set
            {
                _pageColor = value;
                OnPropertyChanged(null);
            }
        }

        private string _textColor = Colors.Text;
        public string TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                OnPropertyChanged(null);
            }
        }
    }
}
