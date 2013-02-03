using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GoldMarket
{
    public partial class ErrorPage : PhoneApplicationPage
    {
        public ErrorPage()
        {
            InitializeComponent();
        }

        public static Exception Exception;
        // Executes when the user navigates to this page. 
        
    }
}