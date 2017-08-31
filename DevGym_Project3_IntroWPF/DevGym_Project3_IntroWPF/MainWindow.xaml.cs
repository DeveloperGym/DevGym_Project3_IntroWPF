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

        private DispatcherTimer ErrorUpdater { get; set; }

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

            // Error Message
            ErrorUpdater = new DispatcherTimer();
            ErrorUpdater.Tick += (o, i) => { ErrorOutput.Visibility = Visibility.Hidden; ErrorUpdater.Stop(); };
            // THis controls how long an error message is shown, originally 5 seconds. TimeSpan(Hours, Minutes, Seconds)
            ErrorUpdater.Interval = new TimeSpan(0, 0, 5);

            // Simple Eye Animation
            Motions = new List<EyeMotion>();
            Motions.Add(new EyeMotion(EyePiece0, TheEye));
            Motions.Add(new EyeMotion(EyePiece1, TheEye));

            EyeUpdater = new DispatcherTimer();
            EyeUpdater.Tick += EyeUpdater_Tick;
            // This controls how often the "eye" animation is updated, about 60 FPS. TimeSpan(Hours, Minutes, Seconds, Milliseconds)
            // A lower number will make the animation faster, but will hurt performance
            EyeUpdater.Interval = new TimeSpan(0, 0, 0, 0, 16);
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
        /// <summary>
        /// When the window is finished loading, do these commands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Applications.Count > 0)
            {
                ViewModel.SelectedApplication = ViewModel.Applications[0];
            }
        }

        /// <summary>
        /// When the timed event is triggered, call the update routine to move the rectangles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EyeUpdater_Tick(object sender, EventArgs e)
        {
            Motions.ForEach(eyePiece => eyePiece.Update());
        }

        /// <summary>
        /// When you click the arrow button, launch the program, or show an error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excute_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedApplication == null) { return; }

            try
            {
                if (!System.IO.File.Exists(ViewModel.SelectedApplication.Target))
                {
                    ShowError("Not Found: " + ViewModel.SelectedApplication.Target);
                    return;
                }

                ProcessStartInfo PSI = new ProcessStartInfo(ViewModel.SelectedApplication.Target) { UseShellExecute = false };
                //if (!String.IsNullOrWhiteSpace(ViewModel.SelectedApplication.Arguments)) { PSI.Arguments = ViewModel.SelectedApplication.Arguments; }
                //if (!String.IsNullOrWhiteSpace(ViewModel.SelectedApplication.StartIn)) { PSI.WorkingDirectory = ViewModel.SelectedApplication.StartIn; }
                Process.Start(PSI);
            }
            catch (Exception ex)
            {
                //ErrorManager.Report(ex.ToString(), ErrorSeverity.Critical);
                ShowError("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// When you click the gear button, open the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var SettingsWindow = new Settings();
            SettingsWindow.ShowDialog();
        }
        #endregion

        #region Methods
        /// <summary>
        /// This updates the error message, shows it, and sets the timer to hide the message again
        /// </summary>
        /// <param name="error"></param>
        public void ShowError(string error)
        {
            ViewModel.ErrorMessage = error;
            ErrorOutput.Visibility = Visibility.Visible;
            ErrorUpdater.Start();
        }
        #endregion
    }
}
