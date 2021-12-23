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
        }

        private async void DisplayResultDialog(string title, string content)
        {
            //var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            //string tempContent = content + " " + resourceLoader.GetString(move) + content2;

            ContentDialog1.Title = title;
            ContentDialog1.Content = content;
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex ipRegex = new Regex("([0-9]{1,3}(?:\\.[0-9]{1,3}){3}|[a-zA-Z]+):([0-9]{1,5})"); //regex stolen from https://stackoverflow.com/questions/57288837/regex-if-valid-websocket-address 
            if (ipRegex.IsMatch(serverIP.Text))
            {
                localSettings.Values["ipAddress"] = serverIP.Text;
            }
            else
            {
                DisplayResultDialog("not valid IP", "The IP you inserted is not valid");
            }
            

        }

    }
}
