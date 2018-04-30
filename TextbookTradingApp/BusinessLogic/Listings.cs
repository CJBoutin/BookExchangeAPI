using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextbookTradingApp.Pages;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace TextbookTradingApp.BusinessLogic
{
    public class Listings
    {
        StackPanel ParentPanel = new StackPanel();
        public void GenerateListingList(List<SimpleListing> lists, StackPanel parentPanel, bool includeButton)
        {
            try
            {
                ParentPanel = parentPanel;
                foreach(var item in lists)
                {
                    var newGListing = GenerateListing(item, parentPanel, includeButton);
                    newGListing.Margin = new Windows.UI.Xaml.Thickness(10, 10, 10, 10);

                    parentPanel.Children.Add(newGListing);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private Grid GenerateListing(SimpleListing sl, StackPanel panel, bool IncludeButton)
        {
            Grid listingGrid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            ColumnDefinition c2 = new ColumnDefinition();
            ColumnDefinition c3 = new ColumnDefinition();
            ColumnDefinition c4 = new ColumnDefinition();
            c1.Width = new Windows.UI.Xaml.GridLength(2, Windows.UI.Xaml.GridUnitType.Star);
            c2.Width = new Windows.UI.Xaml.GridLength(4, Windows.UI.Xaml.GridUnitType.Star);
            c3.Width = new Windows.UI.Xaml.GridLength(1, Windows.UI.Xaml.GridUnitType.Star);
            c4.Width = new Windows.UI.Xaml.GridLength(1, Windows.UI.Xaml.GridUnitType.Star);
            listingGrid.ColumnDefinitions.Add(c1); // Left column has the author details
            listingGrid.ColumnDefinitions.Add(c2); // Center grid has title of the book
            listingGrid.ColumnDefinitions.Add(c3); // Right Column has the price
            listingGrid.ColumnDefinitions.Add(c4);

            TextBlock title = new TextBlock();
            title.Text = sl.Title;

            TextBlock auth = new TextBlock();
            auth.Text = "Written by: " +  sl.Author;

            TextBlock isbn = new TextBlock();
            isbn.Text = "ISBN: " + sl.ISBN;

            TextBlock price = new TextBlock();
            price.Text = "$" + sl.Price.ToString();

            TextBlock quality = new TextBlock();
            quality.Text = "Quality: " + sl.Quality;

            StackPanel col1Sp = new StackPanel();
            col1Sp.Children.Add(auth);
            col1Sp.Children.Add(quality);

            StackPanel col2Sp = new StackPanel();
            col2Sp.Children.Add(title);
            col2Sp.Children.Add(isbn);


            TextBox BidBox = new TextBox();
            BidBox.Visibility = Visibility.Collapsed;
            BidBox.PlaceholderText = "Bid";

            StackPanel buyStack = new StackPanel();
            buyStack.Orientation = Orientation.Horizontal;


            if (IncludeButton == true)
            {
                    // Create buy button unique to that entry
                    Button buyButton = new Button();
                    buyButton.Content = "Place Bid";
                    buyButton.Tag = new Dictionary<string, dynamic>()
                            {
                                { "TransactionId", sl.ListingId },
                                { "BidBox", BidBox }
                            };
                    buyButton.Click += BuyButton_Click;
                    buyStack.Children.Add(buyButton);

            }
            else
            {
                // Create buy button unique to that entry
                Button checkBids = new Button();
                checkBids.Content = "Check Bids";
                checkBids.Tag = new Dictionary<string, dynamic>()
                            {
                                { "TransactionId", sl.ListingId }
                            };
                checkBids.Click += CheckBids_Click; ;
                buyStack.Children.Add(checkBids);

            }

            buyStack.Children.Add(BidBox);

            // Add the stackpanels to the grid
            Grid.SetColumn(col1Sp, 0);
            Grid.SetColumn(col2Sp, 1);
            Grid.SetColumn(price, 2);
            Grid.SetColumn(buyStack, 3);

            listingGrid.Children.Add(col1Sp);
            listingGrid.Children.Add(col2Sp);
            listingGrid.Children.Add(price);
            listingGrid.Children.Add(buyStack);

            return listingGrid;

        }

        private async void CheckBids_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("This feature isnt fully supported yet! Come back soon.");
            await msg.ShowAsync();
        }

        /// <summary>
        /// The 'Place bid' button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            try
            {
                // Bring up popup to place bid
                Button btn = (Button)sender;
                Dictionary<string, dynamic> tagDict = (Dictionary<string, dynamic>)btn.Tag;
                tagDict["BidBox"].Visibility = Visibility.Visible;
                // Change the button to accept the bid
                btn.Content = "Accept Bid";
                // Change the click event listener to a new listener that actually sends the request
                btn.Click += AcceptBid_Button_Click;
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// The Accept Bid button used after a bid is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AcceptBid_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn = (Button)sender;
            var transactionId = (int)((Dictionary<string, dynamic>)btn.Tag)["TransactionId"];
            var bidBox = (TextBox)((Dictionary<string, dynamic>)btn.Tag)["BidBox"];
            // This is where we actually call the api to add a bid.
            Dictionary<string, int> bidReqData = new Dictionary<string, int>();
            bidReqData.Add("PurchaserId", SessionState.LoggedInId);
            bidReqData.Add("TransactionId", transactionId);
            bidReqData.Add("ProposedPrice", Convert.ToInt32(bidBox.Text));

            string bidIdReqJson = JsonConvert.SerializeObject(bidReqData);
            var response = new RequestSender().SendPost("MakeBid", bidIdReqJson);

            // At this point the bid has been created
            MessageDialog msg = new MessageDialog("The bid has been placed.");
            await msg.ShowAsync();

            btn.Click += Btn_Click_AlreadyClicked;
        }

        private async void Btn_Click_AlreadyClicked(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("This button has already been used to make a bid.");
            await msg.ShowAsync();
        }
        public string GenerateRequestJson(SimpleListing listingDetails)
        {
            try
            {

                Dictionary<string, dynamic> jsonDictionary = new Dictionary<string, dynamic>();
                jsonDictionary.Add("UserId", SessionState.LoggedInId);
                jsonDictionary.Add("Name", listingDetails.Title);
                jsonDictionary.Add("ISBN", listingDetails.ISBN);
                jsonDictionary.Add("Images", listingDetails.Images);
                jsonDictionary.Add("Author", listingDetails.Author);
                jsonDictionary.Add("Publisher", listingDetails.Publisher);
                jsonDictionary.Add("ListPrice", listingDetails.Price);
                jsonDictionary.Add("Negotiable", listingDetails.Negotiable);
                jsonDictionary.Add("Description", listingDetails.Description);


                var json = JsonConvert.SerializeObject(jsonDictionary);

                // Send this to the server
                dynamic response = new RequestSender().SendPost("NewListing", json);


                return response;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }


    public class SimpleListing
    {
        public int ListingId { get; set; }
        public string Title { get; set; }

        public double Price { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Quality { get; set; }

        public string Description { get; set; }
        public string Publisher { get; set; }
        public List<string> Images { get => images; set => images = value; }

        private List<string> images = new List<string>();

        public int Negotiable { get; set; }



    }
}
