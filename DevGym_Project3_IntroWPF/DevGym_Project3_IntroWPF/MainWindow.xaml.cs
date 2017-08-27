using System;
using System.Collections.Generic;
using System.Linq;
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
        public DispatcherTimer EyeUpdater { get; set; }

        public List<EyeMotion> Motions { get; set; }
        #endregion

        #region Construct / Destruct
        public MainWindow()
        {
            InitializeComponent();

            // Simple Eye Animation
            Motions = new List<EyeMotion>();
            Motions.Add(new EyeMotion(Eye0, TheEye));
            Motions.Add(new EyeMotion(Eye1, TheEye));

            EyeUpdater = new DispatcherTimer();
            EyeUpdater.Tick += EyeUpdater_Tick;
            EyeUpdater.Interval = new TimeSpan(0, 0, 0, 0, 10);
            EyeUpdater.Start();
        }
        #endregion

        #region Events
        private void EyeUpdater_Tick(object sender, EventArgs e)
        {
            Motions.ForEach(eye => eye.Update());
        }

        private void Excute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var SettingsWindow = new Settings();
            SettingsWindow.ShowDialog();
        }
        #endregion
    }
}
