using Passport_login.Models;
using Passport_login.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Passport_login.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private Account _account;

        public Login()
        {
            this.InitializeComponent();
            SignInPassport();
          
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check Microsoft Passport is setup and available on this machine
            if (await MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync())
            {
            }
            else
            {
                // Microsoft Passport is not setup so inform the user
                PassportStatus.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                PassportStatusText.Text = "Microsoft Passport is not setup!\nPlease go to Windows Settings and set up a PIN to use it.";
                PassportSignInButton.IsEnabled = false;
            }
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            SignInPassport();
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            Frame.Navigate(typeof(PassportRegister));
        }

        private async void SignInPassport()
        {
            //if (AccountHelper.ValidateAccountCredentials(UsernameTextBox.Text))
            //{
                // Create and add a new local account
                //_account = AccountHelper.AddAccount(UsernameTextBox.Text);
                Debug.WriteLine("Successfully signed in with traditional credentials and created local account instance!");
                
                if (await MicrosoftPassportHelper.CreatePassportKeyAsync(UsernameTextBox.Text))
                {


                Debug.WriteLine("Successfully signed in with Microsoft Passport!");
               
                
            }
                
            //}
            else
            {
                ErrorMessage.Text = "Invalid Credentials";
            }
              
            AccountHelper.AddAccount(UsernameTextBox.Text);
            //CoreApplication.Exit();
        }
    }
}