using DevGym_Project3_IntroWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DevGym_Project3_IntroWPF
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<SettingsDataEdit> _Applications = new ObservableCollection<SettingsDataEdit>();
        private SettingsDataEdit _SelectedItem = null;
        #endregion

        #region Properties
        public ObservableCollection<SettingsDataEdit> Applications
        {
            get { return _Applications; }
            set
            {
                if (_Applications != value)
                {
                    _Applications = value ?? new ObservableCollection<SettingsDataEdit>();
                    OnPropertyChanged();
                }
            }
        }

        public SettingsDataEdit SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged();
                    OnPropertyChanged("InputEnabled");
                }
            }
        }

        public bool InputEnabled
        {
            get { return (SelectedItem != null); }
        }
        #endregion

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

        public Settings()
        {
            InitializeComponent();

            this.DataContext = this;

            MainWindow.instance.ViewModel.Applications.ToList().ForEach(s => Applications.Add(new SettingsDataEdit() { Name = s.Name, Target = s.Target }));
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddNew_Click(object sender, RoutedEventArgs e)
        {
            Applications.Add(new SettingsDataEdit());
            
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.ViewModel.Applications.Clear();
            Applications.ToList().ForEach(app => MainWindow.instance.ViewModel.Applications.Add(new SettingsData() { Name = app.Name, Target = app.Target }));
            SettingsManager.Save(MainWindow.instance.ViewModel.Applications);

            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem == null) { return; }
            int a = 1;
        }
    }
}
