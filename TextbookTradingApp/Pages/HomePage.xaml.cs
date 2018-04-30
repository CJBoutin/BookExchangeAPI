using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using TextbookTradingApp.BusinessLogic;
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

namespace TextbookTradingApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            try
            {
                this.InitializeComponent();
                if (SessionState.LoggedIn == true)
                {
                    LoginControls_StackPanel.Visibility = Visibility.Collapsed;
                    // We need to get the data for the user now.
                    var response = new RequestSender().SendGet(string.Format("GetProfile?Uid={0}", SessionState.LoggedInId));
                    dynamic des = JsonConvert.DeserializeObject(response);
                    des = JsonConvert.DeserializeObject((string)des);

                    UserInfo_UserName_TextBlock.Text = "User Name: " + des["Data"]["UserName"].ToString();
                    UserInfo_PhoneNumber_TextBlock.Text = "Phone Number " + des["Data"]["PhoneNumber"].ToString();
                    UserInfo_EmailAddress_TextBlock.Text = "Email Address: " + des["Data"]["EmailAddress"].ToString();

                    // Next we load the open transactions for the current user
                    var userTransactionsResponse = (string)JsonConvert.DeserializeObject(new RequestSender().SendGet("GetUserTransactions?UserId=" + SessionState.LoggedInId));
                    dynamic userTDeserialized = JsonConvert.DeserializeObject(userTransactionsResponse);
                    // Organize
                    List<SimpleListing> simpleList = new List<SimpleListing>();
                    foreach(var item in userTDeserialized.Data.Transactions)
                    {
                        SimpleListing s = new SimpleListing();
                        s.Author = item.Author;
                        s.ISBN = item.ISBN;
                        s.ListingId = item.TransactionId;
                        s.Price = item.ListPrice;
                        s.Title = item.Name;
                        simpleList.Add(s);
                    }
                    if(simpleList.Count > 0)
                    new Listings().GenerateListingList(simpleList, UserTransactions_StackPanel, false);
                    UserInfo_StackPanel.Visibility = Visibility.Visible;
                }
            }
            catch(Exception e)
            {
                
            }
        }

        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the Username of the user
            // Attempt to sign them in using the fields

            Dictionary<string, string> authDict = new Dictionary<string, string>();
            authDict.Add("UserName", UserName_TextBox.Text);

            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Hash.GetMd5Hash(md5Hash, Password_TextBox.Password);
            }
            authDict.Add("PasswordHash", hash);
            var response = (string)JsonConvert.DeserializeObject(new RequestSender().SendPost("Authenticate", JsonConvert.SerializeObject(authDict)));
            dynamic responseDeserialized = JsonConvert.DeserializeObject(response);
            // Set session status to logged in
            if(responseDeserialized["Status"] == "1")// If true, then the deserialization was successful
            {
                SessionState.LoggedInId = responseDeserialized.UserId;
                SessionState.LoggedIn = true;
            }

            NavigateToPage.Navigate(typeof(HomePage));
        }

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the sign up page
            NavigateToPage.Navigate(typeof(SignUpPage));
        }
    }
}
