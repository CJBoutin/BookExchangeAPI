using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbookTradingApp.BusinessLogic
{
    public static class SessionState
    {
        public static int LoggedInId { get; set; }

        public static string LoggedInName { get; set; }
        public static bool LoggedIn { get => loggedIn; set => loggedIn = value; }

        private static bool loggedIn = false;


    }
}
