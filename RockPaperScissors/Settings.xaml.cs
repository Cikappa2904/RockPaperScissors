using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace RockPaperScissors
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Settings()
        {
            this.InitializeComponent();
            serverIP.Text = localSettings.Values["ipAddress"].ToString();
        }

        private async void DisplayResultDialog(string title, string content)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            
            ContentDialog1.Title = resourceLoader.GetString(title);
            ContentDialog1.Content = resourceLoader.GetString(content);
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(); //Localization

            Regex ipRegex = new Regex("([0-9]{1,3}(?:\\.[0-9]{1,3}){3}|[a-zA-Z]+):([0-9]{1,5})"); //regex stolen from https://stackoverflow.com/questions/57288837/regex-if-valid-websocket-address 
            if (ipRegex.IsMatch(serverIP.Text))
            {
                localSettings.Values["ipAddress"] = serverIP.Text;
            }
            else
            {
                

                DisplayResultDialog("Invalid IP", "The IP is not valid");
            }
            

        }

    }
}
