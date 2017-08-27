using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevGym_Project3_IntroWPF
{
    public class BaseNotify : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propChanged = null)
        {
            if (PropertyChanged != null && !String.IsNullOrEmpty(propChanged))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propChanged));
            }
        }
        #endregion
    }
}
