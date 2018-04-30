using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TextbookTradingApp.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TextbookTradingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Frame NavigationFrame => CurrentFrame;
        public MainPage()
        {
            this.InitializeComponent();
            NavigationFrame.Navigate(typeof(HomePage));
        }

        /// <summary>
        /// Fires when the Hamburger button gets clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hamburger_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainSplitView.IsPaneOpen == false)
                MainSplitView.IsPaneOpen = true;
            else
                MainSplitView.IsPaneOpen = false;
        }
        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentFrame.Navigate(typeof(HomePage));
        }

        private void Buy_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentFrame.Navigate(typeof(BuyPage));
        }

        private void Sell_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentFrame.Navigate(typeof(SellPage));
        }
    }
}
