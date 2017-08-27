using DevGym_Project3_IntroWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGym_Project3_IntroWPF
{
    public static class SettingsManager
    {
        public const string GYM_FOLDER = "DevGym";
        public const string SETTINGS_FOLDER = "ShortCutter";
        public const string SETTINGS_FILE = "ShortCutterSettings.jset";

        public static IEnumerable<SettingsData> Load()
        {
            string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GYM_FOLDER, SETTINGS_FOLDER, SETTINGS_FILE);
            try
            {
                if (File.Exists(SettingsPath))
                {
                    return JsonConvert.DeserializeObject<List<SettingsData>>(File.ReadAllText(SettingsPath));
                }
            }
            catch { }
            return new List<SettingsData>();
        }

        public static string Save(IEnumerable<SettingsData> data)
        {
            string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GYM_FOLDER, SETTINGS_FOLDER);
            try
            {
                if (!Directory.Exists(SettingsPath))
                {
                    Directory.CreateDirectory(SettingsPath);
                }

                SettingsPath = Path.Combine(SettingsPath, SETTINGS_FILE);
                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return string.Format("Unable to save settings file ({0}): {1}", SettingsPath, ex.ToString());
            }
            return null;
        }
    }
}
