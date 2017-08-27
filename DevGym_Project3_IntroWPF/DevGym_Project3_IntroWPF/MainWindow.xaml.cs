using DevGym_Project3_IntroWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DevGym_Project3_IntroWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Properties
        public static MainWindow instance { get; private set; }

        private DispatcherTimer EyeUpdater { get; set; }

        private List<EyeMotion> Motions { get; set; }

        public MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
        #endregion

        #region Construct / Destruct
        public MainWindow() : this(null)
        {
        }

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            instance = this;

            // Simple Eye Animation
            Motions = new List<EyeMotion>();
            Motions.Add(new EyeMotion(EyePiece0, TheEye));
            Motions.Add(new EyeMotion(EyePiece1, TheEye));

            EyeUpdater = new DispatcherTimer();
            EyeUpdater.Tick += EyeUpdater_Tick;
            EyeUpdater.Interval = new TimeSpan(0, 0, 0, 0, 10);
            EyeUpdater.Start();

            // Final Set Up
            ViewModel = viewModel ?? new MainWindowViewModel();

            var TmpSettings = SettingsManager.Load().ToList();

            if (TmpSettings.Count > 0)
            {
                TmpSettings.ForEach(s => ViewModel.Applications.Add(s));
                ViewModel.SelectedApplication = ViewModel.Applications[0];
            }
        }
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Applications.Count > 0)
            {
                ViewModel.SelectedApplication = ViewModel.Applications[0];
            }
        }

        private void EyeUpdater_Tick(object sender, EventArgs e)
        {
            Motions.ForEach(eyePiece => eyePiece.Update());
        }

        private void Excute_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedApplication == null) { return; }

            try
            {
                if (!System.IO.File.Exists(ViewModel.SelectedApplication.Target))
                {
                    //ErrorManager.Report("Missing App Launcher Target: " + OneApp.Target, ErrorSeverity.Critical);
                    return;
                }

                ProcessStartInfo PSI = new ProcessStartInfo(ViewModel.SelectedApplication.Target) { UseShellExecute = false };
                //if (!String.IsNullOrWhiteSpace(OneApp.Arguments)) { PSI.Arguments = OneApp.Arguments; }
                //if (!String.IsNullOrWhiteSpace(OneApp.StartIn)) { PSI.WorkingDirectory = OneApp.StartIn; }
                Process.Start(PSI);
            }
            catch //(Exception ex)
            {
                //ErrorManager.Report(ex.ToString(), ErrorSeverity.Critical);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var SettingsWindow = new Settings();
            SettingsWindow.ShowDialog();
        }
        #endregion
    }
}
