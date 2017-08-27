using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevGym_Project3_IntroWPF.Models
{
    public class SettingsDataEdit : SettingsData
    {
        public ICommand Browse { get; set; } 

        public SettingsDataEdit()
        {
            Browse = new CommandHandler(() => ExecuteBrowse(), true);
        }

        private void ExecuteBrowse()
        {
            var FileDialog = new Microsoft.Win32.OpenFileDialog();
            var Result = FileDialog.ShowDialog();

            if (Result == true)
            {
                Target = FileDialog.FileName;
            }
        }
    }
}
