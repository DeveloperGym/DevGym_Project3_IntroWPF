using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGym_Project3_IntroWPF.Models
{
    public class SettingsData : BaseNotify
    {
        private string _Target = "";
        private string _Name = "";

        public string Target
        {
            get { return _Target; }
            set
            {
                if (_Target != value)
                {
                    _Target = value ?? "";
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value ?? "";
                    OnPropertyChanged();
                }
            }
        }
    }
}
