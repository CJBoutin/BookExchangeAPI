using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class BuyPage : Page
    {
        public BuyPage()
        {
            this.InitializeComponent();
        }

        private void SearchListings_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = GenerateSearchJson(Search_TextBox.Text);
                // Call the API for a list of items
                var resp = new RequestSender().SendPost("SearchTransactions", json);
                dynamic desResp = JsonConvert.DeserializeObject(resp);
                desResp = JsonConvert.DeserializeObject(desResp);
                List<SimpleListing> sListings = new List<SimpleListing>();
                foreach (var t in desResp)
                {
                    SimpleListing s = new SimpleListing();
                    s.Author = t["Author"].ToString();
                    s.Title = t["Name"].ToString();
                    s.ISBN = t["ISBN"].ToString();
                    s.Price = Convert.ToDouble(t["ListPrice"]);
                    s.Quality = t["Condition"].ToString();
                    s.ListingId = Convert.ToInt32(t["TransactionId"]);

                    sListings.Add(s);
                }
                new Listings().GenerateListingList(sListings, Listings_StackPanel, true);
                // Generate Listing List
            }
            catch(Exception ex)
            {

            }
        }

        private string GenerateSearchJson(string criteria)
        {
            Dictionary<string, string> crit = new Dictionary<string, string>();
            crit.Add("Title", criteria);
            crit.Add("ISBN", criteria);
            crit.Add("Author", criteria);

            return JsonConvert.SerializeObject(crit);
        }

        private void AcceptBid_Button_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, int> bidDict = new Dictionary<string, int>();
            bidDict.Add("PurchaserId", SessionState.LoggedInId);
            //bidDict.Add("TransactionId", )
            // Create a new bid with the value in BidAmount_TextBox
            //var response = new RequestSender().SendPost("MakeBid")
        }

    }
}
