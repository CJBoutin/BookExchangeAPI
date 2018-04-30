using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TextbookTradingApp.BusinessLogic;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TextbookTradingApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private async void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestSender request = new RequestSender();
                var userJson = TBUser.Create(UserName_TextBox.Text, Password_TextBox.Password, PhoneNumber_TextBox.Text, Email_TextBox.Text);
                var result = request.SendPost("NewUser", userJson);
                dynamic resDetailed = JsonConvert.DeserializeObject(result);
                resDetailed = JsonConvert.DeserializeObject((string)resDetailed);
                if ((int)resDetailed["Status"] == 1)
                {
                    SessionState.LoggedInId = Convert.ToInt32(resDetailed["Data"]["UserId"]);
                    SessionState.LoggedInName = resDetailed["Data"]["UserName"].ToString();
                    SessionState.LoggedIn = true;
                }
                else
                {
                    MessageDialog msg = new MessageDialog("There is a user already associated with that username.");
                    await msg.ShowAsync();
                }

                // Then log in with those credentials
                NavigateToPage.Navigate(typeof(HomePage));
            }
            catch(Exception ex)
            {
                MessageDialog msg = new MessageDialog("There is a user already associated with that username.");
                await msg.ShowAsync();

            }
        }

        private void PasswordVerify_TextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                // Compare to the password box to check if equal
                PasswordBox pb = (PasswordBox)sender;
                if (pb.Password == Password_TextBox.Password)
                {
                    // If the passwords are equal, set the border color to green
                    PasswordVerify_TextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                }
                else
                {
                    PasswordVerify_TextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
