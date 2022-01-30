using System.Configuration;

namespace Candal
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            string? configFileLocation = ConfigurationManagerFile.ChooseConfigFileLocation();
            string? appTitle = ConfigurationManager.AppSettings["Title"];

            MessageBox.Show("Hello World !!! : " + appTitle);
        }
    }

}


