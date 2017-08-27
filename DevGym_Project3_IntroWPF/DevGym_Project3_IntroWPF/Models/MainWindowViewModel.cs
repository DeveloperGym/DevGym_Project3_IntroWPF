using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGym_Project3_IntroWPF.Models
{
    public class MainWindowViewModel : BaseNotify
    {
        #region Fields
        private ObservableCollection<SettingsData> _Applications = new ObservableCollection<SettingsData>();

        private SettingsData _SelectedApplication = null;
        #endregion

        #region Properties
        public ObservableCollection<SettingsData> Applications
        {
            get { return _Applications; }
            set
            {
                if (_Applications != value)
                {
                    _Applications = value ?? new ObservableCollection<SettingsData>();
                    OnPropertyChanged();
                }
            }
        }

        public SettingsData SelectedApplication
        {
            get { return _SelectedApplication; }
            set
            {
                if (_SelectedApplication != value)
                {
                    _SelectedApplication = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
