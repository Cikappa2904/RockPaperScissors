using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace RockPaperScissors
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    /// 


    //TODO: Pareggio carta

    public sealed partial class MultiPlayer : Page
    {

        string[] movesEmojis = new string[3] { "🪨", "📄", "✂️" };
        string[] movesText = new string[3] { "Rock", "Paper", "Scissors" };
        int playerWinStreak = 0, enemyWinStreak = 0;
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings; //Local storage


        public MultiPlayer()
        {
            this.InitializeComponent();
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(); //Localization
            if(localSettings.Values["playerStreakMultiplayer"]==null) //Initializing to 0 localstorage variables
            {
                localSettings.Values["playerStreakMultiplayer"] = 0;
            }
            if (localSettings.Values["pcStreakMultiplayer"] == null)
            {
                localSettings.Values["pcStreakMultiplayer"] = 0;
            }

            if (!IsWindows11())
            {
                movesEmojis[0] = "✊";
            }



            rockRadioButton.Content = resourceLoader.GetString("Rock");
            
            paperRadioButton.Content = resourceLoader.GetString("Paper");
            scissorsRadioButton.Content = resourceLoader.GetString("Scissors");
            //showComputerThought.Text = resourceLoader.GetString("I'm thinking");
            button.Content = resourceLoader.GetString("Play");
            playerStreak.Text = resourceLoader.GetString("Your streak") + ": " + localSettings.Values["playerStreakMultiplayer"];
            enemyStreak.Text = resourceLoader.GetString("Enemy Streak") + ": " + localSettings.Values["pcStreakMultiplayer"];
            addressIP.Text = "Server: " + localSettings.Values["ipAddress"].ToString();

            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;


        }


        private void CoreWindow_CharacterReceived(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CharacterReceivedEventArgs args)
        {
            //Make keyboard shortcuts work for stuff that doesn't work as KeyboardAccelerator
            switch (args.KeyCode)
            {
                case 53:
                    if(addressIP.Visibility == Visibility.Collapsed)
                    {
                        addressIP.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        addressIP.Visibility = Visibility.Collapsed;
                    }
                    
                    break;


            }
        }

        private void PlayerMove_Changed(object sender, SelectionChangedEventArgs e)
        {
            int playerMove = playerMove_RadioButtons.SelectedIndex;
            switch (playerMove)
            {
                case 0: //Rock
                    if(IsWindows11())
                    {
                        showPlayerMove.Text = "🪨";
                    }
                    else
                    {
                        showPlayerMove.Text = "✊";
                    }
                    break;
                case 1: //Paper
                    showPlayerMove.Text = "📄";
                    break;
                case 2: //Scissors
                    showPlayerMove.Text = "✂️";
                    break;
            }


        }

        private async void DisplayResultDialog(string title, string content, string move, string content2)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string tempContent = content + " " + resourceLoader.GetString(move) + content2;

            ContentDialog1.Title = title;
            ContentDialog1.Content = tempContent;
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }


        private async void DisplayErrorDialog(string title, string content)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            ContentDialog1.Title = title;
            ContentDialog1.Content = content;
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }
        async static Task<string> HttpGetAsync(string uri)
        {
            string content = "";
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }

        async static Task<string> HttpPostAsync(string uri, string line, string key)
        {
            var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { key, line },
            };

            var content = new FormUrlEncodedContent(values);

            try
            {
                var response = await client.PostAsync(uri, content);

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;


            }
            catch (HttpRequestException e)
            {

                return "Request error";

            }




        }

        async Task PlayGame(string get_request, int player_move, string whoami)
        { 

            string opponent_move = "-1";
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            while (true)
            {
                await Task.Delay(100);
                opponent_move = await HttpPostAsync(get_request, whoami, "player_number");
                if(opponent_move == "Request error")
                {
                    DisplayErrorDialog("An error occurred", "An error occurred while trying to connect to the server");
                    return;
                }

                if(opponent_move != "-1")
                {
                    break;
                }


            }

            switch (opponent_move)
            {
                case "0":
                    switch (player_move)
                    {

                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so it's a draw"));
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so you won"));
                            enemyWinStreak = 0;
                            playerWinStreak++;
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so you lost"));
                            playerWinStreak = 0;
                            enemyWinStreak++;
                            break;

                    }
                    break;
                case "1":
                    switch (player_move)
                    {
                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so you lost"));
                            enemyWinStreak++;
                            playerWinStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so it's a draw"));
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so you won"));
                            enemyWinStreak = 0;
                            playerWinStreak++;
                            break;

                    }
                    break;
                case "2":
                    switch (playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so you won"));
                            playerWinStreak++;
                            enemyWinStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so you lost"));
                            enemyWinStreak++;
                            playerWinStreak = 0;
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so it's a draw"));
                            break;

                    }
                    break;
            }

            enemyStreak.Text = enemyWinStreak.ToString();
            playerStreak.Text = playerWinStreak.ToString();
            button.IsEnabled = true;

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        { 

            button.IsEnabled = false;
            playerMove_RadioButtons.IsEnabled = false;

            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            
            string post_request = "http://" + localSettings.Values["ipAddress"].ToString() + "/send_result";
            string get_request  = "http://" + localSettings.Values["ipAddress"].ToString() + "/get_result";

            string whoami = await HttpPostAsync(post_request, playerMove_RadioButtons.SelectedIndex.ToString(), "move");

            int playerMove = playerMove_RadioButtons.SelectedIndex;

            await PlayGame(get_request, playerMove, whoami);

            

 
        }

        private void GameContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["playerStreakMultiplayer"] = playerWinStreak.ToString();
            localSettings.Values["enemyStreakMultiplayer"] = enemyWinStreak.ToString();

            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            playerStreak.Text = resourceLoader.GetString("Your streak") + ": " + playerWinStreak.ToString();
            enemyStreak.Text = resourceLoader.GetString("My streak") + ": " + enemyWinStreak.ToString();
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public bool IsWindows11()
        {
            var v = Int64.Parse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            var build = (v & 0x00000000FFFF0000) >> 16;

            if (build >= 22000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
