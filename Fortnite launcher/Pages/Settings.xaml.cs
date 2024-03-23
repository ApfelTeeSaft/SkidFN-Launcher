using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfApp6.Pages
{
    public partial class Settings : UserControl
    {
        private DispatcherTimer timer;
        public Settings()
        {
            InitializeComponent();
            LoadSettings();


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
            timer.Start();


            this.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            clock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void DiscordLinkCard_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://cdn.discordapp.com/attachments/1176535201661337700/1221194199668621322/Cobalt.dll?ex=6611b0b9&is=65ff3bb9&hm=ed96b7551fb9f58e8d2890a830e6ef0a986a152958ece80feee78164b8ebd8ee&";
            process.Start();
        }

        private void DiscordLinkCard_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void DiscordLinkCard_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists("credentials.json"))
                {
                    string json = File.ReadAllText("credentials.json");
                    dynamic credentials = JsonConvert.DeserializeObject(json);

                    string email = credentials.Email;
                    string password = credentials.Password;
                    bool rememberMe = credentials.RememberMe;

                    EmailTextBox.Text = email;
                    PasswordBox.Password = password;
                    RememberMeCheckBox.IsChecked = rememberMe;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;
                bool rememberMe = RememberMeCheckBox.IsChecked ?? false;

                dynamic credentials = new
                {
                    Email = email,
                    Password = password,
                    RememberMe = rememberMe
                };

                string json = JsonConvert.SerializeObject(credentials);
                File.WriteAllText("credentials.json", json);

                MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == "Email")
            {
                EmailTextBox.Text = "";
                EmailTextBox.Foreground = Brushes.White;
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Email";
                EmailTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}