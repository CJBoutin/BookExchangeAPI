using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class SellPage : Page
    {
        public SellPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Fires when the user is trying to send a new transaction to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SubmitTransaction_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SessionState.LoggedIn == false)
                {
                    MessageDialog message = new MessageDialog("You are not signed in. Please proceed to the sign in page.");
                    await message.ShowAsync();
                }
                //Build the POST request to the server
                SimpleListing newListing = new SimpleListing();
                newListing.Author = Author_TextBox.Text;
                newListing.ISBN = ISBN_TextBox.Text;
                newListing.Price = Convert.ToInt32(Price_TextBox.Text);
                newListing.Title = Title_TextBox.Text;
                newListing.Description = Description_TextBox.Text;

                string json = new Listings().GenerateRequestJson(newListing);
                dynamic desResponse = JsonConvert.DeserializeObject((string)JsonConvert.DeserializeObject(json));
                var listingId = desResponse.NewListingId;
                MessageDialog successMessage = new MessageDialog("The transaction has been created.");
                await successMessage.ShowAsync();
                
            }
            catch(Exception ex)
            {

            }
        }
    }
}
