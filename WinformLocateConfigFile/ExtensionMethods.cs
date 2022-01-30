
using System.Configuration;
using System.Reflection;

namespace Candal
{
    internal static class ConfigurationManagerFile
    {
        public static string? ChooseConfigFileLocation()
        {
            //Search for file
            string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            if (assemblyName is null)
                throw new Exception($"ERROR: Assembly.GetExecutingAssembly().GetName().Name return null.");

            bool found = false;

            string efectiveFile = $"{ AppContext.BaseDirectory}\\{assemblyName}.config";
            found = File.Exists(efectiveFile);
            if (!found)
            {
                efectiveFile = $"{ AppContext.BaseDirectory}\\{assemblyName}.exe.config";
                found = File.Exists(efectiveFile);
            }

            if (!found)
            {
                efectiveFile = $"{ AppContext.BaseDirectory}\\{assemblyName}.dll.config";
                found = File.Exists(efectiveFile);
            }

            if (!found)
                return null;

            //search AppSetting element 'file location'
            string searchItem = "ConfigFileLocation";
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = efectiveFile };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            string? fileLocation = null;
            KeyValueConfigurationElement keyValue = config.AppSettings.Settings[searchItem];
            if (keyValue != null)
                fileLocation = keyValue.Value;

            if (fileLocation == null)
                return efectiveFile;

            if (!File.Exists(fileLocation))
                throw new Exception($"Config File has the parameter '{searchItem}' but the value points to invalid file");
            else
                efectiveFile = fileLocation;

            //set config file
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", efectiveFile);

            return efectiveFile;

        }
    }
}
