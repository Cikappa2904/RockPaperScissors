using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private void MultiplayerSettings_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(clientOrServer.SelectedIndex == 0)
            {
                serverIP.IsEnabled = true;
                localSettings.Values["multiplayerSettings"] = "client";

            }
            else
            {
                serverIP.IsEnabled = false;
                localSettings.Values["multiplayerSettings"] = "server";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            localSettings.Values["ipAddress"] = serverIP.Text;
        }

    }
}
