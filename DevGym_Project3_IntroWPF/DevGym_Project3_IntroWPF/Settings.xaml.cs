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
        private ObservableCollection<SettingsData> _Applications = new ObservableCollection<SettingsData>();
        private SettingsData _SelectedItem = null;
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

        public SettingsData SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged();
                    OnPropertyChanged("InputEnabled");
                    OnPropertyChanged("SortUpEnabled");
                    OnPropertyChanged("SortDownEnabled");
                }
            }
        }

        public bool InputEnabled
        {
            get { return (SelectedItem != null); }
        }

        public bool SortUpEnabled
        {
            get
            {
                if (InputEnabled)
                {
                    if (Applications.IndexOf(SelectedItem) > 0) { return true; }
                }
                return false;
            }
        }

        public bool SortDownEnabled
        {
            get
            {
                if (InputEnabled)
                {
                    if (Applications.IndexOf(SelectedItem) < Applications.Count - 1) { return true; }
                }
                return false;
            }
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

            MainWindow.instance.ViewModel.Applications.ToList().ForEach(s => Applications.Add(new SettingsData() { Name = s.Name, Target = s.Target }));
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddNew_Click(object sender, RoutedEventArgs e)
        {
            Applications.Add(new SettingsData());
            SelectedItem = Applications.Last();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.ViewModel.Applications.Clear();
            if (Applications.Count > 0)
            {
                Applications.ToList().ForEach(app => MainWindow.instance.ViewModel.Applications.Add(new SettingsData() { Name = app.Name, Target = app.Target }));
                MainWindow.instance.ViewModel.SelectedApplication = MainWindow.instance.ViewModel.Applications[0];
            }
            SettingsManager.Save(MainWindow.instance.ViewModel.Applications);

            this.Close();
        }

        public void Browse_Click(object sender, RoutedEventArgs e)
        {
            if (!InputEnabled) { return; }

            var FileDialog = new Microsoft.Win32.OpenFileDialog();
            var Result = FileDialog.ShowDialog();

            if (Result == true)
            {
                SelectedItem.Target = FileDialog.FileName;
            }
        }

        public void SortUp_Click(object sender, RoutedEventArgs e)
        {
            if (!InputEnabled) { return; }

            var index = Applications.IndexOf(SelectedItem);
            var MovingItem = SelectedItem;
            Applications.RemoveAt(index);
            Applications.Insert(index - 1, MovingItem);
            SelectedItem = MovingItem;
        }

        public void SortDown_Click(object sender, RoutedEventArgs e)
        {
            if (!InputEnabled) { return; }

            var index = Applications.IndexOf(SelectedItem);
            var MovingItem = SelectedItem;
            Applications.RemoveAt(index);
            Applications.Insert(index + 1, MovingItem);
            SelectedItem = MovingItem;
        }

        public void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (!InputEnabled) { return; }

            Applications.Remove(SelectedItem);
            SelectedItem = null;
        }
    }
}
