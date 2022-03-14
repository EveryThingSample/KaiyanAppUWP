using KaiYan.Core;
using KaiYan.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KaiYan.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            remindCheckBox.IsChecked = (bool)(Windows.Storage.ApplicationData.Current.LocalSettings.Values["isremind"] ?? false);
            if (remindCheckBox.IsChecked == true)
            {
                usernameTextBox.Text = (string)(Windows.Storage.ApplicationData.Current.LocalSettings.Values["username"] ?? "");
                passwordTextBox.Password = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["password"] ?? "";
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (sender as Control).IsEnabled = false;
                var username = usernameTextBox.Text;
                var password = passwordTextBox.Password;
                var isremin = remindCheckBox.IsChecked;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["isremind"] = isremin;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["username"] = username;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["password"] = password;
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var build = new AccountBuilder();

                    var account = await build
                        .WithUsername(username)
                        .WithPassword(password)
                        .BuildAsync();
                    MainContentControl.Current.ReLoad();
                }
            }
            finally
            {
                (sender as Control).IsEnabled = true;
            }
        }
    }
}
