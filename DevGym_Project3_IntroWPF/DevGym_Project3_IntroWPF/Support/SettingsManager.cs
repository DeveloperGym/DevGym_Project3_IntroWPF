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
    /// <summary>
    /// Used to save settings to the hard drive, and load back
    /// </summary>
    public static class SettingsManager
    {
        // Main folder in "My Documents"
        public const string GYM_FOLDER = "DevGym";
        // Sub folder under the main folder
        public const string SETTINGS_FOLDER = "ShortCutter";
        // File name where the JSON formatted settings are stored
        public const string SETTINGS_FILE = "ShortCutterSettings.jset";

        /// <summary>
        /// Load from hard drive
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Save to hard drive
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
