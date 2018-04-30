using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextbookTradingApp.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TextbookTradingApp.BusinessLogic
{
    public static class NavigateToPage
    {
        public static void Navigate(Type frameType)
        {
            var rootFrame = Window.Current.Content as Frame;
            var mainPage = rootFrame.Content as MainPage;
            mainPage.NavigationFrame.Navigate(frameType);
        }
    }
}
