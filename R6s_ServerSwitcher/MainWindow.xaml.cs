using System;
using System.IO;
using System.Windows;
namespace R6s_ChangeServer {
    public partial class MainWindow : Window {
        string prefix = "playfab/";
        readonly string[] areas = { "default (ping based)", "eastus", "centralus", "southcentralus", "westus", "brazilsouth", "northeurope", "westeurope", "southafricanorth", "eastasia", "southeastasia", "australiaeast", "australiasoutheast", "japanwest" };
        string url_document;
        string[] directories;


        public MainWindow() {
            InitializeComponent();
            url_document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Rainbow Six - Siege";

            if (!Directory.Exists(url_document)) {
                Console.WriteLine("Game does not exist!!");
                this.Close();
                Environment.Exit(Environment.ExitCode);
            }

            directories = Directory.GetDirectories(url_document);

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            switchArea(ComboBox_Server.SelectedIndex);
            MessageBox.Show("Change server successfully!");
        }

        private void switchArea(int uid) {
            foreach (string dir in directories) {
                string url = dir + "\\GameSettings.ini";
                if (checkIfFileExist(url)) editGameSettings(url, uid);
            }
        }

        private Boolean checkIfFileExist(string url) {
            if (!File.Exists(url)) {
                MessageBox.Show("GameSettings.ini does not exist!!");
                return false;
            }
            return true;
        }

        private void editGameSettings(string url, int uid) {
            string[] contentInGameSetting;
            int length = 14;
            contentInGameSetting = File.ReadAllLines(url);

            for (byte i = 1; i < contentInGameSetting.Length; ++i) {
                if (contentInGameSetting[contentInGameSetting.Length - i].Length > 10) {
                    string lineString = contentInGameSetting[contentInGameSetting.Length - i].Substring(0, length);
                    if (lineString == "DataCenterHint") {
                        contentInGameSetting[contentInGameSetting.Length - i] = "DataCenterHint=" + prefix + areas[uid];
                        File.WriteAllLines(url, contentInGameSetting);
                        break;
                    }
                }
            }
        }
    }
}
